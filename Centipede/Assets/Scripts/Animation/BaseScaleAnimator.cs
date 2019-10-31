using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base animator implementation, that work with scale
/// </summary>
public class BaseScaleAnimator : BaseAnimator
{
    protected override Vector3 GetStartTransformAttribute()
    {
        return transform.localScale;
    }

    protected override void SetStartTransformAttribute(Vector3 attribute)
    {
        transform.localScale = attribute;
    }

    protected override void OnXAxisPlay(AnimationRule animationRule)
    {
        transform.localScale = new Vector3(
            transform.localScale.x + animationRule.xAxisDeformationModifier,
            transform.localScale.y,
            transform.localScale.z);
    }

    protected override void OnYAxisPlay(AnimationRule animationRule)
    {
        transform.localScale = new Vector3(
            transform.localScale.x,
            transform.localScale.y + animationRule.yAxisDeformationModifier,
            transform.localScale.z);
    }

    protected override void OnZAxisPlay(AnimationRule animationRule)
    {
        transform.localScale = new Vector3(
            transform.localScale.x,
            transform.localScale.y,
            transform.localScale.z + animationRule.zAxisDeformationModifier);
    }
}
