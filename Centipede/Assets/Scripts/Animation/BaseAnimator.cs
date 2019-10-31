using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class, that play animation rules one by one
/// </summary>
public abstract class BaseAnimator : MonoBehaviour
{
    [SerializeField]
    private List<AnimationRule> animationRules;

    [Space]

    [SerializeField]
    private bool dependsOnTheStartingTransform;
    [SerializeField]
    private float multiplier;

    private int counter;
    private Vector3 startTransformAttribute;

    private void Awake()
    {
        counter = 0;
        startTransformAttribute = GetStartTransformAttribute();
        if (dependsOnTheStartingTransform)
        {
            for (int i = 0; i < animationRules.Count; i++)
            {
                animationRules[i] = animationRules[i].GetСorrectTransform(startTransformAttribute);
            }
        }
        else
        {
            for (int i = 0; i < animationRules.Count; i++)
            {
                animationRules[i] = animationRules[i].GetСorrectTransform(multiplier);
            }
        }
    }

    void FixedUpdate()
    {
        foreach (AnimationRule rule in animationRules)
        {
            if (!rule.animationPlayed)
            {
                if (counter < rule.animationTime)
                {
                    if (rule.xAxis)
                    {
                        OnXAxisPlay(rule);
                    }
                    if (rule.yAxis)
                    {
                        OnYAxisPlay(rule);
                    }
                    if (rule.zAxis)
                    {
                        OnZAxisPlay(rule);
                    }

                }
                else
                {
                    counter = 0;
                    rule.animationPlayed = true;
                }

                break;
            }

            if (animationRules.IndexOf(rule) == animationRules.Count - 1)
            {
                SetStartTransformAttribute(startTransformAttribute);

                foreach (AnimationRule item in animationRules)
                {
                    item.animationPlayed = false;
                }
            }
        }

        counter++;
    }

    protected abstract Vector3 GetStartTransformAttribute();
    protected abstract void SetStartTransformAttribute(Vector3 attribute);

    protected abstract void OnXAxisPlay(AnimationRule animationRule);
    protected abstract void OnYAxisPlay(AnimationRule animationRule);
    protected abstract void OnZAxisPlay(AnimationRule animationRule);
}
