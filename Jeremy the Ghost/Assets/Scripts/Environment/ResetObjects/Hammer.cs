using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField] private float animationOffset;
    [SerializeField] private Animator hammerAnimator;
    [SerializeField] private Collider2D killingCollider;
    
    void Start()
    {
        StartCoroutine(startAnimation(animationOffset));
    }

    IEnumerator startAnimation(float offsetToStart)
    {
        yield return new WaitForSeconds(offsetToStart);
        hammerAnimator.SetBool("StartSmashing", true);
    }

    void EnableCollider() { killingCollider.enabled = true; }
    void DisableCollider() { killingCollider.enabled = false; }
}
