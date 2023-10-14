using Struct;
using TMPro;
using UnityEngine;

public class InputFieldVector3
{
    public Vector3 GetVector3 => new(m_inputPropertyX.GetInput, m_inputPropertyY.GetInput, m_inputPropertyZ.GetInput);

    public bool GetVector3Change =>
        m_inputPropertyX.GetInputDown || m_inputPropertyY.GetInputDown || m_inputPropertyZ.GetInputDown;
    
    private TMP_InputField m_inputFieldX;

    private TMP_InputField m_inputFieldY;
    
    private TMP_InputField m_inputFieldZ; 
    
    private InputProperty<float> m_inputPropertyX;
    
    private InputProperty<float> m_inputPropertyY;
    
    private InputProperty<float> m_inputPropertyZ;

    public InputFieldVector3(TMP_InputField inputFieldX,TMP_InputField inputFieldY,TMP_InputField inputFieldZ)
    {
        InitComponent(inputFieldX,inputFieldY,inputFieldZ);
        InitEvent();
        InitValue();
    }

    private void InitValue()
    {
        SetInputField(Vector3.zero);
    }

    void InitComponent(TMP_InputField inputFieldX,TMP_InputField inputFieldY,TMP_InputField inputFieldZ)
    {
        m_inputFieldX = inputFieldX;
        m_inputFieldY = inputFieldY;
        m_inputFieldZ = inputFieldZ;
    }

    void InitEvent()
    {
        m_inputFieldX.onEndEdit.AddListener((data) =>
        {
            (bool canChange,Vector3 newInput) = StringToVector3(data, m_inputFieldY.text, m_inputFieldZ.text);
            if (canChange) SetPosition = newInput;
            else SetInputField(GetVector3);
        });
        m_inputFieldY.onEndEdit.AddListener((data) =>
        {
            (bool canChange,Vector3 newInput) = StringToVector3(m_inputFieldX.text, data, m_inputFieldZ.text);
            if (canChange) SetPosition = newInput;
            else SetInputField(GetVector3);
        });
        m_inputFieldZ.onEndEdit.AddListener((data) =>
        {
            (bool canChange,Vector3 newInput) = StringToVector3(m_inputFieldX.text, m_inputFieldY.text, data);
            if (canChange) SetPosition = newInput;
            else SetInputField(GetVector3);
        });
    }
    
    private void SetInputField(Vector3 input)
    {
        m_inputFieldX.text = input.x.ToString().Replace(" ","");
        m_inputFieldY.text = input.y.ToString().Replace(" ","");
        m_inputFieldZ.text = input.z.ToString().Replace(" ","");
    }
    
    private Vector3 SetPosition
    {
        set
        {
            m_inputPropertyX.SetInput = value.x;
            m_inputPropertyY.SetInput = value.y;
            m_inputPropertyZ.SetInput = value.z;
        }
    }
    
    private (bool,Vector3) StringToVector3(string x,string y,string z)
    {
        if (!float.TryParse(x, out float floatX)) return (false, Vector3.zero);
        if (!float.TryParse(y, out float floatY)) return (false, Vector3.zero);
        if (!float.TryParse(z, out float floatZ)) return (false, Vector3.zero);
        return (true, new Vector3(floatX, floatY, floatZ));
    }
}
