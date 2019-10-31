using UnityEngine;

/// <summary>
/// One action animation rule, that playing by given time
/// </summary>
[System.Serializable]
public class AnimationRule
{
    [HideInInspector]
    public bool animationPlayed;

    public int animationTime;

    [Space]

    public bool xAxis;
    [Range(-1, 1)]
    public float xAxisDeformationModifier;

    [Space]

    public bool yAxis;
    [Range(-1, 1)]
    public float yAxisDeformationModifier;

    [Space]

    public bool zAxis;
    [Range(-1, 1)]
    public float zAxisDeformationModifier;

    public AnimationRule GetСorrectTransform(Vector3 objectTransform)
    {
        AnimationRule ruleWithCorrectTransform = new AnimationRule();
        ruleWithCorrectTransform.animationTime = animationTime;
        ruleWithCorrectTransform.xAxis = xAxis;
        ruleWithCorrectTransform.yAxis = yAxis;
        ruleWithCorrectTransform.zAxis = zAxis;
        ruleWithCorrectTransform.xAxisDeformationModifier = objectTransform.x * xAxisDeformationModifier;
        ruleWithCorrectTransform.yAxisDeformationModifier = objectTransform.y * yAxisDeformationModifier;
        ruleWithCorrectTransform.zAxisDeformationModifier = objectTransform.z * zAxisDeformationModifier;

        return ruleWithCorrectTransform;
    }

    public AnimationRule GetСorrectTransform(float multiplier)
    {
        AnimationRule ruleWithCorrectTransform = new AnimationRule();
        ruleWithCorrectTransform.animationTime = animationTime;
        ruleWithCorrectTransform.xAxis = xAxis;
        ruleWithCorrectTransform.yAxis = yAxis;
        ruleWithCorrectTransform.zAxis = zAxis;
        ruleWithCorrectTransform.xAxisDeformationModifier = multiplier * xAxisDeformationModifier;
        ruleWithCorrectTransform.yAxisDeformationModifier = multiplier * yAxisDeformationModifier;
        ruleWithCorrectTransform.zAxisDeformationModifier = multiplier * zAxisDeformationModifier;

        return ruleWithCorrectTransform;
    }
}
