using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Can change all key entry in TextMesh by int parameter
/// </summary>
public class BaseTextManipulator : BaseImplementer
{
    public bool autoCalculate;
    private bool calculated;

    [SerializeField]
    private TextMesh textComponent;

    public string key;
    private string startText;

    private void Awake()
    {
        if (autoCalculate)
            Calculate(0);
    }

    public override void Calculate(int data)
    {
        startText = textComponent.text;
        textComponent.text = startText.Replace(key, data.ToString());
        calculated = true;
    }

    protected override void ImplementData(int data)
    {
        textComponent.text = startText.Replace(key, data.ToString());
    }
}
