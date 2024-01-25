using Frame.Static.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RectTransform = UnityEngine.RectTransform;

namespace LevelEditor
{
    public class ItemTransformPanel
    {
        public Vector3 GetPosition => m_positionInputFieldVector3.GetVector3;
    
        public Vector3 GetRotation => m_rotationInputFieldVector3.GetVector3;
    
        public Vector3 GetScale => m_scaleInputFieldVector3.GetVector3;

        public GameObject GetPanelObj => m_transformPanelObj;
        
        public (string,string,string) GetPositionField => m_positionInputFieldVector3.GetVector3Field;
    
        public (string,string,string) GetRotationField => m_rotationInputFieldVector3.GetVector3Field;
    
        public (string,string,string) GetScaleField => m_scaleInputFieldVector3.GetVector3Field;
    
        public Vector3 SetPosition
        {
            set
            {
                m_positionInputFieldVector3.SetVector3 = value;
            }
        }
        
        public Vector3 SetRotation
        {
            set
            {
                m_rotationInputFieldVector3.SetVector3 = value;
            }
        }
        
        public Vector3 SetScale
        {
            set
            {
                m_scaleInputFieldVector3.SetVector3 = value;
            }
        }
    
        public bool GetPositionChange => m_positionInputFieldVector3.GetVector3Change;
    
        public bool GetRotationChange => m_rotationInputFieldVector3.GetVector3Change;
    
        public bool GetScaleChange => m_scaleInputFieldVector3.GetVector3Change;
    
        public bool GetOnSelect => m_positionInputFieldVector3.OnSelect || m_rotationInputFieldVector3.OnSelect ||
                                   m_scaleInputFieldVector3.OnSelect;
        
        private Button m_editButton;

        private GameObject m_transformPanelObj;
    
        private InputFieldVector3 m_positionInputFieldVector3;
        
        private InputFieldVector3 m_rotationInputFieldVector3;
        
        private InputFieldVector3 m_scaleInputFieldVector3;
        
        public ItemTransformPanel(RectTransform levelEditorCanvasRect,UIProperty levelEditorUIProperty)
        {
            InitComponent(levelEditorCanvasRect, levelEditorUIProperty);
        }
        
        private void InitComponent(RectTransform levelEditorCanvasRect,UIProperty levelEditorUIProperty)
        {
            UIProperty.ItemTransformPanelUIName property = levelEditorUIProperty.GetItemTransformPanelUI.GetItemTransformPanelUIName;
            m_editButton = levelEditorCanvasRect.FindPath(property.EDIT_BUTTON).GetComponent<Button>();
            m_transformPanelObj = levelEditorCanvasRect.FindPath(property.ROOT_PANEL).gameObject;
            m_positionInputFieldVector3 = new InputFieldVector3(
                levelEditorCanvasRect.FindPath(property.POSITION_INPUT_X).GetComponent<TMP_InputField>(),
                levelEditorCanvasRect.FindPath(property.POSITION_INPUT_Y).GetComponent<TMP_InputField>(),
                levelEditorCanvasRect.FindPath(property.POSITION_INPUT_Z).GetComponent<TMP_InputField>());
            m_rotationInputFieldVector3 = new InputFieldVector3(
                levelEditorCanvasRect.FindPath(property.ROTATION_INPUT_X).GetComponent<TMP_InputField>(),
                levelEditorCanvasRect.FindPath(property.ROTATION_INPUT_Y).GetComponent<TMP_InputField>(),
                levelEditorCanvasRect.FindPath(property.ROTATION_INPUT_Z).GetComponent<TMP_InputField>());
            m_scaleInputFieldVector3 = new InputFieldVector3(
                levelEditorCanvasRect.FindPath(property.SCALE_INPUT_X).GetComponent<TMP_InputField>(), 
                levelEditorCanvasRect.FindPath(property.SCALE_INPUT_Y).GetComponent<TMP_InputField>(),
                levelEditorCanvasRect.FindPath(property.SCALE_INPUT_Z).GetComponent<TMP_InputField>());
        }
    }

}