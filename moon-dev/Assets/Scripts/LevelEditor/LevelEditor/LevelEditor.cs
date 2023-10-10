using System;
using System.Collections;
using System.Collections.Generic;
using Frame.Tool;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    private LevelEditorCameraController m_editorCameraController;
    private LevelEditorCommandManager m_levelEditorCommandManager;
    void Start()
    {
        m_levelEditorCommandManager = new LevelEditorCommandManager();
        m_editorCameraController = new LevelEditorCameraController(transform as RectTransform,m_levelEditorCommandManager.Excute);
    }
    
    private void LateUpdate()
    {
        m_editorCameraController.LateUpdate();
    }

    private void Update()
    {
        if (InputManager.Instance.GetDebuggerNum1Down)
        {
            m_levelEditorCommandManager.Undo();
        }
        if (InputManager.Instance.GetDebuggerNum2Down)
        {
            m_levelEditorCommandManager.Redo();
        }
    }
}
