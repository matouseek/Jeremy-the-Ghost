using UnityEngine;

public class ThornCannonController : MonoBehaviour
{
    private const string _playerTag = "Jeremy";
    private GameObject _player;
    [SerializeField] private float _maxRange; // Maximum range at which the cannon will fire
    [SerializeField] private float _timeToShoot; // This is the max value of _shootCountdown
    private float _shootCountdown; // This decreases while aiming at the player
    private bool _playerInSight;
    [SerializeField] private GameObject _bulletObject;
    [SerializeField] private Transform _parentForBullets; // Bullets are created as children of this object
    
    private float _halfSpriteHeight; // Used to offset raycast, so it fires from the top of cannon

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _halfSpriteHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;
        _shootCountdown = _timeToShoot;
        _player = GameObject.FindGameObjectWithTag(_playerTag);
    }

    private void Update()
    {
        if (!DecreaseTimeToShoot()) return;
        TransitionToShooting();
    }

    private void FixedUpdate()
    {
        RaycastToPlayer();
    }

    private void RaycastToPlayer()
    {
        Vector2 dirToPlayer = (_player.transform.position - gameObject.transform.position).normalized;
        Vector2 originPos = new Vector2(transform.position.x, transform.position.y) + dirToPlayer*_halfSpriteHeight;
        
        transform.rotation = Quaternion.LookRotation(Vector3.forward, dirToPlayer);
        
        RaycastHit2D hit = Physics2D.Raycast(originPos, dirToPlayer, _maxRange);
        _playerInSight = hit && hit.collider.tag.Equals(_playerTag); // The raycast hits something and
                                                                        // that something is the player
                                                                        
        ShowRaycastGizmos(originPos, dirToPlayer);
    }
    
    /// <summary>
    /// Returns true if _shootCountdown is less than or equal to 0.
    /// (i.e. it's time to shoot)
    /// </summary>
    private bool DecreaseTimeToShoot()
    {
        if (_playerInSight)
        {
            _shootCountdown -= Time.deltaTime;
        }
        else
        {
            _shootCountdown = _timeToShoot;
        }

        if (_shootCountdown > 0) return false;
        _shootCountdown = _timeToShoot;
        return true;

    }

    /// <summary>
    /// Called as an event from the shooting animation.
    /// </summary>
    private void Shoot()
    {
        Instantiate(_bulletObject, transform.position, transform.rotation, _parentForBullets);
    }

    /// <summary>
    /// Sets the bool that transitions the animation to shooting.
    /// </summary>
    private void TransitionToShooting()
    {
        _animator.SetBool("Firing", true);
    }

    /// <summary>
    /// Called as an event at the end of shooting animation.
    /// </summary>
    private void TransitionToIdle()
    {
        _animator.SetBool("Firing", false);
    }

    /// <summary>
    /// Debugging function that shows the raycast of the cannon.
    /// Color is based on the player being in/out of cannon range.
    /// </summary>
    private void ShowRaycastGizmos(Vector2 originPos, Vector2 dirToPlayer)
    {
        Debug.DrawRay(originPos, dirToPlayer, _playerInSight ? Color.red : Color.green);
    }
}
