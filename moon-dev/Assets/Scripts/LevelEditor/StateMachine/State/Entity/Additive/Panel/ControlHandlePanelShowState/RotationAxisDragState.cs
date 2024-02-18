using System;
using System.Collections.Generic;
using Frame.StateMachine;
using LevelEditor.Command;
using Moon.Kernel.Extension;
using Moon.Kernel.Struct;
using UnityEngine;
using UnityEngine.InputSystem;
using RectTransform = UnityEngine.RectTransform;

namespace LevelEditor
{
    /// <summary>
    ///     This state is entered when the mouse is dragged
    /// </summary>
    public sealed class RotationAxisDragState : AdditiveState
    {
        private List<AbstractItem> Items                     => m_information.DataManager.TargetItems;
        private ControlHandlePanel ControlHandlePanel        => m_information.UIManager.GetControlHandlePanel;
        private RectTransform      RotationAxisRectTransform => ControlHandlePanel.GetRotationRect;
        private Vector2            MouseCursorCompensation   => ControlHandlePanel.GetMouseCursorProperty.CURSOR_BOUND_CHECK_COMPENSATION;
        private Vector3            MousePosition             => m_information.CameraManager.MousePosition;
        private bool               MouseLeftButtonUp         => m_information.InputManager.GetMouseLeftButtonUp;
        private bool               UseGrid                   => ControlHandlePanel.GetControlHandleAction.UseGrid;
        private float              RotationUnit              => ControlHandlePanel.GetGridSnappingProperty.ROTATION_UNIT;

        private          Vector3 _originMousePosition;
        private          Vector3 _currentMousePosition;
        private readonly Vector3 _originMouseToAxisDir;
        private readonly Vector3 _oriRotationAxisPos;
        private          Flag    _waitToNextFrame;

        private float     RotationSpeed              => m_information.UIManager.GetControlHandlePanel.GetRotationDragProperty.ROTATION_SPEED;
        private Transform CameraTransform            => Camera.main.transform;
        private Vector3   RotationAxisScreenPosition => RotationAxisRectTransform.anchoredPosition;

        private Vector3 RotationAxisWorldPosition => Camera.main.ScreenToWorldPoint
            (RotationAxisScreenPosition.NewZ(Mathf.Abs(CameraTransform.position.z)));

        private readonly List<Quaternion> _targetRotation     = new();
        private readonly List<Vector3>    _targetPosition     = new();
        private readonly List<Vector3>    _mousePosVectorList = new();

        private readonly Action _onLeftUp;
        private readonly Action _onUpdate;


        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="information"></param>
        /// <param name="motionCallBack"></param>
        public RotationAxisDragState(BaseInformation information, MotionCallBack motionCallBack)
            : base(information, motionCallBack)
        {
            _originMousePosition  = MousePosition;
            _originMouseToAxisDir = (_originMousePosition - RotationAxisScreenPosition).normalized;
            _oriRotationAxisPos   = RotationAxisRectTransform.position;

            _onLeftUp += OnLeftUp;
            _onUpdate += CheckMouseScreenPosition;
            _onUpdate += UpdateRotation;
        }

        /// <inheritdoc />
        public override void Motion(BaseInformation information)
        {
            // Triggers when the key is raised
            if (MouseLeftButtonUp)
                _onLeftUp.Invoke();
            else
                _onUpdate.Invoke();
        }

        private void OnLeftUp()
        {
            RotationAxisRectTransform.transform.rotation = Quaternion.identity;
            var command = new Rotation(Items, _targetRotation);
            CommandInvoker.Execute(command);
            RemoveState();
        }

        private void UpdateRotation()
        {
            if (_waitToNextFrame.GetFlag) return;

            var mouseSumVector = Vector3.zero;

            foreach (var posVector in _mousePosVectorList) mouseSumVector += posVector;

            _currentMousePosition =  MousePosition;
            mouseSumVector        += _currentMousePosition - _originMousePosition;

            if (mouseSumVector.magnitude == 0) return;

            var mouseDir                  = mouseSumVector.normalized;
            var mouseDis                  = mouseSumVector.magnitude;
            var dirCross                  = Vector3.Cross(_originMouseToAxisDir, mouseDir);
            var rotationDirAndMultiplying = dirCross.z;

            var rotationQuaternion = Quaternion
               .Euler(0, 0, (float)Math.Round(mouseDis * rotationDirAndMultiplying * RotationSpeed, 2));

            if (UseGrid && Items.Count > 1)
            {
                var clip = RotationUnit * Mathf.RoundToInt(rotationQuaternion.eulerAngles.z / RotationUnit);
                rotationQuaternion = Quaternion.Euler(rotationQuaternion.eulerAngles.NewZ(clip));
            }

            RotationAxisRectTransform.rotation = rotationQuaternion;
            RotationAxisRectTransform.position = _oriRotationAxisPos;

            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Transform.rotation == _targetRotation[i] * rotationQuaternion) continue;

                Items[i].Transform.rotation = _targetRotation[i]
                                            * rotationQuaternion;
                Items[i].Transform.position = RotationAxisWorldPosition
                                            + Quaternion.Euler(Vector3.forward * rotationQuaternion.eulerAngles.z).normalized
                                            * (_targetPosition[i] - RotationAxisWorldPosition);

                if (!UseGrid || Items.Count != 1) continue;

                Items[i].Transform.rotation =
                    Quaternion.Euler(
                                     Items[i].Transform.rotation.eulerAngles
                                             .NewZ(RotationUnit * Mathf.RoundToInt(Items[i].Transform.rotation.eulerAngles.z / RotationUnit)));
                RotationAxisRectTransform.rotation = Items[i].Transform.rotation;
            }
        }

        private void CheckMouseScreenPosition()
        {
            _currentMousePosition = MousePosition;

            if (_currentMousePosition.x >= Screen.width)
            {
                _mousePosVectorList.Add(_currentMousePosition - _originMousePosition);
                Mouse.current.WarpCursorPosition(new Vector2(MouseCursorCompensation.x, _currentMousePosition.y));
                _originMousePosition     = MousePosition.NewX(0);
                _waitToNextFrame.SetFlag = true;
            }

            if (_currentMousePosition.x <= 0)
            {
                _mousePosVectorList.Add(_currentMousePosition - _originMousePosition);
                Mouse.current.WarpCursorPosition(new Vector2(Screen.width - MouseCursorCompensation.x, _currentMousePosition.y));
                _originMousePosition     = MousePosition.NewX(Screen.width);
                _waitToNextFrame.SetFlag = true;
            }

            if (_currentMousePosition.y >= Screen.height)
            {
                _mousePosVectorList.Add(_currentMousePosition - _originMousePosition);
                Mouse.current.WarpCursorPosition(new Vector2(_currentMousePosition.x, MouseCursorCompensation.y));
                _originMousePosition     = MousePosition.NewY(0);
                _waitToNextFrame.SetFlag = true;
            }

            if (_currentMousePosition.y <= 0)
            {
                _mousePosVectorList.Add(_currentMousePosition - _originMousePosition);
                Mouse.current.WarpCursorPosition(new Vector2(_currentMousePosition.x, Screen.height - MouseCursorCompensation.y));
                _originMousePosition     = MousePosition.NewY(Screen.height);
                _waitToNextFrame.SetFlag = true;
            }
        }
    }
}