using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base animator implementation, that work with rotation
/// </summary>
public class BaseRotationAnimator : BaseAnimator
{
    protected override Vector3 GetStartTransformAttribute()
    {
        return transform.localRotation.eulerAngles;
    }

    protected override void SetStartTransformAttribute(Vector3 attribute)
    {
        transform.localRotation = Quaternion.Euler(attribute);
    }

    protected override void OnXAxisPlay(AnimationRule animationRule)
    {
        transform.localRotation *= Quaternion.Euler(animationRule.xAxisDeformationModifier, 0, 0);
    }

    protected override void OnYAxisPlay(AnimationRule animationRule)
    {
        transform.localRotation *= Quaternion.Euler(0, animationRule.yAxisDeformationModifier, 0);
    }

    protected override void OnZAxisPlay(AnimationRule animationRule)
    {
        transform.localRotation *= Quaternion.Euler(0, 0, animationRule.zAxisDeformationModifier);
    }
}
