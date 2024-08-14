using System;
using System.Collections.Generic;
using Frame.StateMachine;
using LevelEditor.Command;
using LevelEditor.Item;
using UnityEngine;

namespace LevelEditor
{
    public class ItemTransformPanelShowState : AdditiveState
    {
        private bool m_isUndoOrRedo;

        private List<Vector3>    m_lastPositon  = new();
        private List<Quaternion> m_lastRotation = new();
        private List<Vector3>    m_lastScale    = new();

        /// <summary>
        ///     Default constructor
        /// </summary>
        public ItemTransformPanelShowState(Information baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            InitState();
        }

        private InputManager       InputManager          => m_information.InputManager;
        private List<ItemBase>     Items                 => m_information.Controller.SelectedItems;
        private ItemTransformPanel GetItemTransformPanel => m_information.UIManager.GetItemTransformPanel;
        private bool               GetPositionChange     => m_information.UIManager.GetItemTransformPanel.GetPositionChange;
        private bool               GetRotationChange     => m_information.UIManager.GetItemTransformPanel.GetRotationChange;
        private bool               GetScaleChange        => m_information.UIManager.GetItemTransformPanel.GetScaleChange;

        /// <inheritdoc />
        public override void Motion(Information information)
        {
            CheckChange();
            CheckPositionChange();
            CheckRotationChange();
            CheckScaleChange();
            ShowTransformPanel();
        }

        private void InitState()
        {
            GetItemTransformPanel.GetPanelObj.SetActive(true);
            CommandInvoker.RedoAdditiveEvent += SetDo;
            CommandInvoker.UndoAdditiveEvent += SetDo;
        }

        /// <inheritdoc />
        protected override void RemoveState()
        {
            base.RemoveState();
            GetItemTransformPanel.GetPanelObj.SetActive(false);
            CommandInvoker.RedoAdditiveEvent -= SetDo;
            CommandInvoker.UndoAdditiveEvent -= SetDo;
        }

        private void CheckChange()
        {
            if (m_isUndoOrRedo)
                if (GetPositionChange)
                    if (GetRotationChange)
                        if (GetScaleChange)
                            m_isUndoOrRedo = false;
        }

        private void CheckPositionChange()
        {
            if (!GetPositionChange) return;
            var nextPosition = new List<Vector3>();
            var value        = GetItemTransformPanel.GetPosition;
            for (var i = 0; i < Items.Count; i++)
            {
                var target = Items[i];
                nextPosition.Add(new Vector3(float.IsNaN(value.x) ? target.Transform.position.x : value.x,
                                             float.IsNaN(value.y) ? target.Transform.position.y : value.y,
                                             float.IsNaN(value.z) ? target.Transform.position.z : value.z));
            }

            CommandInvoker.Execute(new PositionCommand(Items, nextPosition));
        }

        private void CheckRotationChange()
        {
            if (!GetRotationChange) return;
            var nextPosition = new List<Vector3>();
            var nextRotation = new List<Quaternion>();
            var value        = GetItemTransformPanel.GetRotation;
            nextPosition.AddRange(m_lastPositon);
            for (var i = 0; i < Items.Count; i++)
            {
                var target = Items[i];
                nextRotation.Add(Quaternion.Euler(new Vector3(float.IsNaN(value.x) ? target.Transform.position.x : value.x,
                                                              float.IsNaN(value.y) ? target.Transform.position.y : value.y,
                                                              float.IsNaN(value.z) ? target.Transform.position.z : value.z)));
            }

            var command = new Rotation(Items, nextRotation);
            CommandInvoker.Execute(command);
        }

        private void CheckScaleChange()
        {
            if (!GetScaleChange) return;
            var nextScale    = new List<Vector3>();
            var nextPosition = new List<Vector3>();
            nextPosition.AddRange(m_lastPositon);
            var value = GetItemTransformPanel.GetScale;
            for (var i = 0; i < Items.Count; i++)
            {
                var target = Items[i];
                nextScale.Add(new Vector3(float.IsNaN(value.x) ? target.Transform.localScale.x : value.x,
                                          float.IsNaN(value.y) ? target.Transform.localScale.y : value.y,
                                          float.IsNaN(value.z) ? target.Transform.localScale.z : value.z));
            }

            // CommandInvoker.Execute(new Scale(Items, m_lastPositon, nextPosition, m_lastScale, nextScale));
        }

        private void ShowTransformPanel()
        {
            if (Items.Count == 0)
            {
                if (!CheckStates.Contains(typeof(LevelSettingPanelShowState)) && !CheckStates.Contains(typeof(ItemWarehousePanelShowState)))
                    throw new InvalidOperationException(); //InputManager.SetCanInput(true);
                RemoveState();
                return;
            }

            if (GetItemTransformPanel.GetOnSelect)
            {
                throw new InvalidOperationException(); //   InputManager.SetCanInput(false);
                SetItemValue();
                return;
            }

            throw new NotImplementedException();

            // if (!InputManager.GetCanInput && PopoverLauncher.Instance.CanInput && !CheckStates.Contains(typeof(LevelSettingPanelShowState)) &&
            //     !CheckStates.Contains(typeof(ItemWarehousePanelShowState)))
            //     InputManager.SetCanInput(true);
            (m_lastPositon, m_lastRotation, m_lastScale) = Items.GetTransforms();
            SetFieldUIValue();
        }

        private void SetFieldUIValue()
        {
            if (Items.Count == 0) return;
            var position = Items[0].Transform.position;
            var rotation = Items[0].Transform.rotation.eulerAngles;
            var scale    = Items[0].Transform.localScale;
            for (var i = 1; i < Items.Count; i++)
            {
                if (position.x != Items[i].Transform.position.x) position.x             = float.NaN;
                if (position.y != Items[i].Transform.position.y) position.y             = float.NaN;
                if (position.z != Items[i].Transform.position.z) position.z             = float.NaN;
                if (rotation.x != Items[i].Transform.rotation.eulerAngles.x) rotation.x = float.NaN;
                if (rotation.y != Items[i].Transform.rotation.eulerAngles.y) rotation.y = float.NaN;
                if (rotation.z != Items[i].Transform.rotation.eulerAngles.z) rotation.z = float.NaN;
                if (scale.x != Items[i].Transform.localScale.x) scale.x                 = float.NaN;
                if (scale.y != Items[i].Transform.localScale.y) scale.y                 = float.NaN;
                if (scale.z != Items[i].Transform.localScale.z) scale.z                 = float.NaN;
            }

            GetItemTransformPanel.SetPosition = position;
            GetItemTransformPanel.SetRotation = rotation;
            GetItemTransformPanel.SetScale    = scale;
        }

        private void SetItemValue()
        {
            SetPosition();
            SetRotation();
            SetScale();
        }

        private void SetPosition()
        {
            var (inputFieldX, inputFieldY, inputFieldZ) = GetItemTransformPanel.GetPositionField;
            var canParseX = float.TryParse(inputFieldX, out var valueX);
            var chnParseY = float.TryParse(inputFieldY, out var valueY);
            var chnParseZ = float.TryParse(inputFieldZ, out var valueZ);
            for (var i = 0; i < Items.Count; i++)
            {
                var target = Items[i];
                target.Transform.position = new Vector3(canParseX ? valueX : target.Transform.position.x,
                                                        chnParseY ? valueY : target.Transform.position.y,
                                                        chnParseZ ? valueZ : target.Transform.position.z);
            }
        }

        private void SetRotation()
        {
            var (inputFieldX, inputFieldY, inputFieldZ) = GetItemTransformPanel.GetRotationField;
            var canParseX = float.TryParse(inputFieldX, out var valueX);
            var chnParseY = float.TryParse(inputFieldY, out var valueY);
            var chnParseZ = float.TryParse(inputFieldZ, out var valueZ);
            for (var i = 0; i < Items.Count; i++)
            {
                var target = Items[i];
                target.Transform.rotation = Quaternion.Euler(new Vector3(canParseX ? valueX : target.Transform.rotation.x,
                                                                         chnParseY ? valueY : target.Transform.rotation.y,
                                                                         chnParseZ ? valueZ : target.Transform.rotation.z));
            }
        }

        private void SetScale()
        {
            var (inputFieldX, inputFieldY, inputFieldZ) = GetItemTransformPanel.GetScaleField;
            var canParseX = float.TryParse(inputFieldX, out var valueX);
            var chnParseY = float.TryParse(inputFieldY, out var valueY);
            var chnParseZ = float.TryParse(inputFieldZ, out var valueZ);
            for (var i = 0; i < Items.Count; i++)
            {
                var target = Items[i];
                target.Transform.localScale = new Vector3(canParseX ? valueX : target.Transform.localScale.x,
                                                          chnParseY ? valueY : target.Transform.localScale.y,
                                                          chnParseZ ? valueZ : target.Transform.localScale.z);
            }
        }

        private void SetDo()
        {
            m_isUndoOrRedo = true;
        }
    }
}