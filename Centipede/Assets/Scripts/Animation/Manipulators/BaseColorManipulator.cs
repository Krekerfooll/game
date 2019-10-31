using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Can slide between start and target color by int parameter
/// </summary>
public class BaseColorManipulator : BaseImplementer
{
    public bool autoCalculate;

    [SerializeField]
    private Color startColor;
    [SerializeField]
    private Color targetColor;
    private bool calculated;

    private Color colorDifference;
    private Color colorStep;

    private void Awake()
    {
        if (autoCalculate)
            Calculate(0);
    }

    public override void Calculate(int data)
    {
        CalculateColorStep(data);
        calculated = true;
    }

    protected override void ImplementData(int data)
    {
        SetColorBySteps(data);
    }

    public void CalculateColorStep(int stepsAmount)
    {
        GetComponent<MeshRenderer>().material.color = startColor;
        colorDifference = startColor - targetColor;
        colorStep = colorDifference / stepsAmount;
    }

    public void SetColorBySteps(int stepsAmount)
    {
        if (calculated)
            GetComponent<MeshRenderer>().material.color = targetColor + (colorStep * stepsAmount);
    }
}
