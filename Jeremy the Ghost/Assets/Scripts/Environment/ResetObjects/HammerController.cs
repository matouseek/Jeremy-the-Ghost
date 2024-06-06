using UnityEngine;

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

    // Both functions are used as animation events of the hammer (when it strikes the collider activates)
    private void EnableDamagingCollider() { _damagingCollider.enabled = true; }
    private void DisableDamagingCollider() { _damagingCollider.enabled = false; }
}
