using UnityEngine;

/// <summary>
/// Provides the API through which a hammer animation delay can be set
/// as well as functions that are called as events during the hammer animation.
/// </summary>
public class HammerController : MonoBehaviour
{
    private Animator _hammerAnimator;
    [SerializeField] private float _animationStartDelay;
    private const string _boolName = "StartSmashing";
    [SerializeField] private Collider2D _damagingCollider;

    private void Start()
    {
        _hammerAnimator = gameObject.GetComponent<Animator>();
        gameObject.AddComponent<AnimationHelper>()
            .SetAnimatorBoolWithDelay(_hammerAnimator, _animationStartDelay, _boolName, true);
    }

    /// <summary>
    /// Enables the hammer collider that deals damage to Jeremy.
    /// This function is called as an event during the hammer animation.
    /// </summary>
    private void EnableDamagingCollider() { _damagingCollider.enabled = true; }
    
    /// <summary>
    /// Disables the hammer collider that deals damage to Jeremy.
    /// This function is called as an event during the hammer animation.
    /// </summary>
    private void DisableDamagingCollider() { _damagingCollider.enabled = false; }
}
