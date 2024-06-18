using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JeremyController : MonoBehaviour
{
    // ---------- Movement ----------
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private EnergyBarController _energyBar;
    private bool _moved;
    private Vector2 _normalizedInput;
    private const float _upMult = 1.4f; // Because we're using RB2D we need to
                                        // fight gravity while moving up, so we multiply the upwards movement
    private bool _dashingDown = false;
    [SerializeField] private float _downDashSpeedIncrease;
        
    public Transform PSRespawn;

    // ---------- Scaring Children ----------
    public bool CanScare { get; set; }
    [SerializeField] private AudioClip _breatheAudioClip;
    [SerializeField] private AudioClip _booAudioClip;
    [SerializeField] private Sprite _angryJeremySprite;
    [SerializeField] private Sprite _normalJeremySprite;
    private AudioSource _audioSource;
    private bool _currentlyScaring = false;
    [SerializeField] private GameObject _cantScareChildrenTextBox;
    private bool _showingCantScareChildrenText = false;

    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        TryScareChildren();
        ResolveMoveInput();
        ResolveDownDashInput();
    }

    private void FixedUpdate()
    {
        MoveOnInput();
    }

    private void ResolveMoveInput()
    {
        if (_energyBar.CurrentEnergy < _energyBar.MoveCost) return; // Not enough energy for a move
        
        var input = Vector2.zero;
        
        if (Input.GetKeyDown(KeyCode.D)) input += Vector2.right;
        if (Input.GetKeyDown(KeyCode.A)) input += Vector2.left;
        if (Input.GetKeyDown(KeyCode.W)) input += _upMult*Vector2.up;

        if (input == Vector2.zero) return; // No move input from player
        if (!MoveManager.Instance.DecreaseAvailableMoves())
        {
            // Player has used more than max moves
            Reset();
            return;
        }
        
        // Player moved successfully
        _moved = true;
        _normalizedInput = input;
        _energyBar.DecreaseEnergy();
    }

    private void ResolveDownDashInput()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            _energyBar.SlowEnergyRecharge();
            _dashingDown = true;
        }
        else if(Input.GetKeyUp(KeyCode.S))
        {
            _energyBar.PutEnergyRechargeToNormal();
            _dashingDown = false;
        }
    }

    private void MoveOnInput()
    {
        // Act based on S hold
        if (_dashingDown) _rb.AddForce(Vector2.down * _downDashSpeedIncrease);
        
        // Act based on W, A, D moves
        if (!_moved) return;
        _rb.AddForce(_normalizedInput * _movementSpeed);
        _moved = false;
    }

    public void Reset()
    {
        _rb.position= PSRespawn.position;
        _rb.velocity = Vector2.zero;
        MoveManager.Instance.ResetMoves();
        _energyBar.ResetEnergy();
    }

    private void TryScareChildren()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if (!CanScare) { // Player pressed space but is not in scaring range;
            StartCoroutine(ShowCantScareText());
            return;
        }
        if (!_currentlyScaring) StartCoroutine(ScareChildren());
    }
    
    /// <summary>
    /// Plays a sequence of sounds and changes Jeremy sprite for a brief moment
    /// to make him look like he's scaring the children. 
    /// </summary>
    private IEnumerator ScareChildren()
    {
        _currentlyScaring = true;
        _audioSource.PlayOneShot(_breatheAudioClip); // Jeremy breathes in
        yield return new WaitForSeconds(_breatheAudioClip.length);
        
        _audioSource.PlayOneShot(_booAudioClip); // Jeremy makes the boo sound
        yield return new WaitForSeconds(0.15f); // Time is set so that sprite change syncs with the audio
        
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _angryJeremySprite;
        
        yield return new WaitForSeconds(0.5f); // After this time passes Jeremys sprite changes back
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _normalJeremySprite;
        
        yield return new WaitForSeconds(1);
        // Scaring children finishes the level
        MenuManager.CompleteLevel();
        MenuManager.showPlayMenuOnLoad = true;
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator ShowCantScareText()
    {
        if(_showingCantScareChildrenText) yield break;
        
        _showingCantScareChildrenText = true;
        _cantScareChildrenTextBox.SetActive(true);
        yield return new WaitForSeconds(3);
        _cantScareChildrenTextBox.SetActive(false);
        _showingCantScareChildrenText = false;
    }
}
