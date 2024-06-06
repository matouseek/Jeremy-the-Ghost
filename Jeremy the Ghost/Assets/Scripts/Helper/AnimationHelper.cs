using System.Collections;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    /// <summary>
    /// Sets the value of the given boolean parameter with specified delay.
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
