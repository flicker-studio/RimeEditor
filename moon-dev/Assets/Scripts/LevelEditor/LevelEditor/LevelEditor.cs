using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    private LevelEditorCameraController m_editorCameraController;
    void Start()
    {
        m_editorCameraController = new LevelEditorCameraController(transform.Find("SelectionUI")
            .GetComponent<RectTransform>());
    }
    
    private void LateUpdate()
    {
        m_editorCameraController.LateUpdate();
    }
}
