using UnityEngine;

public class ThornBulletController : MonoBehaviour
{
    [SerializeField] private float _flyingSpeed = 30f;
    private readonly string _jeremyTag = "Jeremy";
    private readonly string _thornBulletTag = "ThornBullet";
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * (_flyingSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // We don't want the bullets to get stuck on another midair bullets
        if (other.CompareTag(_thornBulletTag))
        {
            return;
        }
        
        _flyingSpeed = 0;
        if (other.CompareTag(_jeremyTag))
        {
            Destroy(gameObject);
        }
        else
        {
            Rot();
        }
    }

    /// <summary>
    /// Sets the bool that transitions the thorn into rotting animation.
    /// </summary>
    void Rot()
    {
        _animator.SetBool("Collided", true);
    }

    /// <summary>
    /// Function for the animation event.
    /// Gets called after one iteration of rot animation.
    /// </summary>
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
