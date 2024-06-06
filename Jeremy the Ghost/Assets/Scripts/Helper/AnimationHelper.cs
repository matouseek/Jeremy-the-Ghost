using System.Collections;
using UnityEngine;

/// <summary>
/// Helper class for things related to animations and animators.
/// </summary>
public class AnimationHelper : MonoBehaviour
{
    /// <summary>
    /// Sets the value of a given boolean parameter on an animator after a specified delay.
    /// </summary>
    public void SetAnimatorBoolWithDelay(Animator animator, float delay, string boolName, bool boolValue)
    {
        IEnumerator WaitForDelayAndStartAnimation()
        {
            yield return new WaitForSeconds(delay);
            animator.SetBool(boolName, boolValue);
        }
        
        StartCoroutine(WaitForDelayAndStartAnimation());
    }
    
}
