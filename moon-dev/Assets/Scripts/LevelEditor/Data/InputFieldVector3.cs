using Moon.Kernel.Struct;
using TMPro;
using UnityEngine;

public class InputFieldVector3
{
    public Vector3 GetVector3 => new(m_inputX.GetInput, m_inputY.GetInput, m_inputZ.GetInput);

    public (string, string, string) GetVector3Field => (m_inputFieldX.text, m_inputFieldY.text, m_inputFieldZ.text);

    public Vector3 SetVector3
    {
        set
        {
            SetInputField(
                float.IsNaN(value.x) ? "-" : value.x.ToString(),
                float.IsNaN(value.y) ? "-" : value.y.ToString(),
                float.IsNaN(value.z) ? "-" : value.z.ToString());

            ReSetPosition = new(value.x, value.y, value.z);
        }
    }

    public bool GetVector3Change =>
        m_inputX.GetInputDown || m_inputY.GetInputDown || m_inputZ.GetInputDown;

    public bool OnSelect => m_selectX || m_selectY || m_selectZ;

    private TMP_InputField m_inputFieldX;

    private TMP_InputField m_inputFieldY;

    private TMP_InputField m_inputFieldZ;

    private Input<float> m_inputX;

    private Input<float> m_inputY;

    private Input<float> m_inputZ;

    private bool m_selectX;

    private bool m_selectY;

    private bool m_selectZ;

    public InputFieldVector3(TMP_InputField inputFieldX, TMP_InputField inputFieldY, TMP_InputField inputFieldZ)
    {
        InitComponent(inputFieldX, inputFieldY, inputFieldZ);
        InitEvent();
        InitValue();
    }

    private void InitValue()
    {
        SetInputField(Vector3.zero);
    }

    void InitComponent(TMP_InputField inputFieldX, TMP_InputField inputFieldY, TMP_InputField inputFieldZ)
    {
        m_inputFieldX = inputFieldX;
        m_inputFieldY = inputFieldY;
        m_inputFieldZ = inputFieldZ;
    }

    void InitEvent()
    {
        m_inputFieldX.onEndEdit.AddListener((data) =>
        {
            (bool canChange, Vector3 newInput) = StringToVector3(data, m_inputFieldY.text, m_inputFieldZ.text);

            if (canChange) SetPosition = newInput;
            else SetInputField(GetVector3);
        });

        m_inputFieldY.onEndEdit.AddListener((data) =>
        {
            (bool canChange, Vector3 newInput) = StringToVector3(m_inputFieldX.text, data, m_inputFieldZ.text);

            if (canChange) SetPosition = newInput;
            else SetInputField(GetVector3);
        });

        m_inputFieldZ.onEndEdit.AddListener((data) =>
        {
            (bool canChange, Vector3 newInput) = StringToVector3(m_inputFieldX.text, m_inputFieldY.text, data);

            if (canChange) SetPosition = newInput;
            else SetInputField(GetVector3);
        });

        m_inputFieldX.onSelect.AddListener((data) => { m_selectX = true; });
        m_inputFieldX.onEndEdit.AddListener((data) => { m_selectX = false; });
        m_inputFieldY.onSelect.AddListener((data) => { m_selectY = true; });
        m_inputFieldY.onEndEdit.AddListener((data) => { m_selectY = false; });
        m_inputFieldZ.onSelect.AddListener((data) => { m_selectZ = true; });
        m_inputFieldZ.onEndEdit.AddListener((data) => { m_selectZ = false; });
    }

    private void SetInputField(Vector3 input)
    {
        m_inputFieldX.text = float.IsNaN(input.x) ? "-" : input.x.ToString().Replace(" ", "");
        m_inputFieldY.text = float.IsNaN(input.y) ? "-" : input.y.ToString().Replace(" ", "");
        m_inputFieldZ.text = float.IsNaN(input.z) ? "-" : input.z.ToString().Replace(" ", "");
    }

    private void SetInputField(string x, string y, string z)
    {
        m_inputFieldX.text = x.Replace(" ", "");
        m_inputFieldY.text = y.Replace(" ", "");
        m_inputFieldZ.text = z.Replace(" ", "");
    }

    private Vector3 SetPosition
    {
        set
        {
            m_inputX.SetInput = value.x;
            m_inputY.SetInput = value.y;
            m_inputZ.SetInput = value.z;
        }
    }

    private Vector3 ReSetPosition
    {
        set
        {
            m_inputX.ResetInput = value.x;
            m_inputY.ResetInput = value.y;
            m_inputZ.ResetInput = value.z;
        }
    }

    private (bool, Vector3) StringToVector3(string x, string y, string z)
    {
        float floatX, floatY, floatZ;

        if (x == "-") floatX = float.NaN;
        else if (!float.TryParse(x, out floatX)) return (false, Vector3.zero);

        if (y == "-") floatY = float.NaN;
        else if (!float.TryParse(y, out floatY)) return (false, Vector3.zero);

        if (z == "-") floatZ = float.NaN;
        else if (!float.TryParse(z, out floatZ)) return (false, Vector3.zero);

        return (true, new Vector3(floatX, floatY, floatZ));
    }
}