using System.Collections.Generic;
using UnityEngine;

public class FadeInThornCannons : MonoBehaviour
{
    [SerializeField] private List<Animator> _animators;
    private readonly string _jeremyTag = "Jeremy";

    private void Start()
    {
        foreach (var animator in _animators)
        {
            animator.SetTrigger("IsInvis");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(_jeremyTag)) return;
        foreach (var animator in _animators)
        {
            animator.SetTrigger("FadingIn");
        }
    }
}
