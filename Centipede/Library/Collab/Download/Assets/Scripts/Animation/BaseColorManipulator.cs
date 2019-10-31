using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseColorManipulator : BaseImplementer
{
    [SerializeField]
    private Color startColor;
    [SerializeField]
    private Color targetColor;
    private bool calculated;

    private Color colorDifference;
    private Color colorStep;

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
        GetComponent<MeshRenderer>().sharedMaterial.color = startColor;
        colorDifference = startColor - targetColor;
        colorStep = colorDifference / stepsAmount;
    }

    public void SetColorBySteps(int stepsAmount)
    {
        if (calculated)
            GetComponent<MeshRenderer>().sharedMaterial.color = targetColor + (colorStep * stepsAmount);
    }
}
