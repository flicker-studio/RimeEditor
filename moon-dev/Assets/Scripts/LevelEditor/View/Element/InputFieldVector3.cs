using System.Globalization;
using Moon.Kernel.Struct;
using TMPro;
using UnityEngine;

public class InputFieldVector3
{
    public Vector3 Data => new(_inputX.GetInput, _inputY.GetInput, _inputZ.GetInput);
    
    public (string, string, string) GetVector3Field => (_inputFieldX.text, _inputFieldY.text, _inputFieldZ.text);

    public Vector3 SetVector3
    {
        set
        {
            SetInputField(
                          float.IsNaN(value.x) ? "-" : value.x.ToString(CultureInfo.InvariantCulture),
                          float.IsNaN(value.y) ? "-" : value.y.ToString(CultureInfo.InvariantCulture),
                          float.IsNaN(value.z) ? "-" : value.z.ToString(CultureInfo.InvariantCulture));
            
            ReSetPosition = new Vector3(value.x, value.y, value.z);
        }
    }

    public bool GetVector3Change =>
        _inputX.GetInputDown || _inputY.GetInputDown || _inputZ.GetInputDown;
    
    public bool OnSelect => _selectX || _selectY || _selectZ;
    
    private TMP_InputField _inputFieldX;
    
    private TMP_InputField _inputFieldY;
    
    private TMP_InputField _inputFieldZ;
    
    private Input<float> _inputX;
    
    private Input<float> _inputY;
    
    private Input<float> _inputZ;
    
    private bool _selectX;
    
    private bool _selectY;
    
    private bool _selectZ;

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
    
    private void InitComponent(TMP_InputField inputFieldX, TMP_InputField inputFieldY, TMP_InputField inputFieldZ)
    {
        _inputFieldX = inputFieldX;
        _inputFieldY = inputFieldY;
        _inputFieldZ = inputFieldZ;
    }
    
    private void InitEvent()
    {
        _inputFieldX.onEndEdit.AddListener(data =>
        {
            var (canChange, newInput) = StringToVector3(data, _inputFieldY.text, _inputFieldZ.text);
            
            if (canChange) SetPosition = newInput;
            else SetInputField(Data);
        });
        
        _inputFieldY.onEndEdit.AddListener(data =>
        {
            var (canChange, newInput) = StringToVector3(_inputFieldX.text, data, _inputFieldZ.text);
            
            if (canChange) SetPosition = newInput;
            else SetInputField(Data);
        });
        
        _inputFieldZ.onEndEdit.AddListener(data =>
        {
            var (canChange, newInput) = StringToVector3(_inputFieldX.text, _inputFieldY.text, data);
            
            if (canChange) SetPosition = newInput;
            else SetInputField(Data);
        });
        
        _inputFieldX.onSelect.AddListener(_ => { _selectX  = true; });
        _inputFieldX.onEndEdit.AddListener(_ => { _selectX = false; });
        _inputFieldY.onSelect.AddListener(_ => { _selectY  = true; });
        _inputFieldY.onEndEdit.AddListener(_ => { _selectY = false; });
        _inputFieldZ.onSelect.AddListener(_ => { _selectZ  = true; });
        _inputFieldZ.onEndEdit.AddListener(_ => { _selectZ = false; });
    }

    private void SetInputField(Vector3 input)
    {
        _inputFieldX.text = float.IsNaN(input.x) ? "-" : input.x.ToString(CultureInfo.InvariantCulture).Replace(" ", "");
        _inputFieldY.text = float.IsNaN(input.y) ? "-" : input.y.ToString(CultureInfo.InvariantCulture).Replace(" ", "");
        _inputFieldZ.text = float.IsNaN(input.z) ? "-" : input.z.ToString(CultureInfo.InvariantCulture).Replace(" ", "");
    }

    private void SetInputField(string x, string y, string z)
    {
        _inputFieldX.text = x.Replace(" ", "");
        _inputFieldY.text = y.Replace(" ", "");
        _inputFieldZ.text = z.Replace(" ", "");
    }

    private Vector3 SetPosition
    {
        set
        {
            _inputX.SetInput = value.x;
            _inputY.SetInput = value.y;
            _inputZ.SetInput = value.z;
        }
    }

    private Vector3 ReSetPosition
    {
        set
        {
            _inputX.ResetInput = value.x;
            _inputY.ResetInput = value.y;
            _inputZ.ResetInput = value.z;
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