using System.Collections;
using System.Collections.Generic;
using Frame.Static.Extensions;
using UnityEngine;

public class TestHash : MonoBehaviour
{
    void Start()
    {
        Debug.Log("TestLevel2024/1/19 19:52:06".ToSHA256());
    }
}
