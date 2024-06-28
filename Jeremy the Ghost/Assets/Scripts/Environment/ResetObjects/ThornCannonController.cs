using UnityEngine;

public class ThornCannonController : MonoBehaviour
{
    private const string _playerTag = "Jeremy";
    [SerializeField] private GameObject _player;
    [SerializeField] private float _maxRange; // Maximum range at which the cannon will fire
    [SerializeField] private float _timeToShoot; // This is the max value of _shootCountdown
    [SerializeField] private float _shootCountdown; // This decreases while aiming at the player
    private bool _playerInSight;

    private float _halfSpriteHeight;

    [SerializeField] private GameObject _bulletObject;

    private void Start()
    {
        _halfSpriteHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;
        _shootCountdown = _timeToShoot;
        _player = GameObject.FindGameObjectWithTag(_playerTag);
    }

    private void Update()
    {
        if (!DecreaseTimeToShoot()) return;
        Shoot();
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
        
        if (_playerInSight)
        {
            Debug.DrawRay(originPos, dirToPlayer, Color.red);
        }
        else
        {
            Debug.DrawRay(originPos, dirToPlayer, Color.green);
        }
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

    private void Shoot()
    {
        //TODO: optional - do the cool rotate/flip animation when shooting
        Instantiate(_bulletObject, transform.position, transform.rotation);
    }
}
