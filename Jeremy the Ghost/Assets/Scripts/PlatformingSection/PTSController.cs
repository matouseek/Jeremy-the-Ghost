using UnityEngine;

/// <summary>
/// This class serves as a controller for the platforming sections.
/// It handles the camera change when starting the section,
/// activates the collider that prevents the player from going back
/// from the section and sets up the MoveManager for the current PTS.
/// </summary>
public class PTSController : MonoBehaviour
{
    
    [SerializeField] private GameObject _noGoingBackCollider;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _previousCamera;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _ptsCamera;
    [SerializeField] private int _maxPtsMoves;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CameraHelper.SwapCameraPriority(_previousCamera, _ptsCamera);
        _noGoingBackCollider.SetActive(true);
        MoveManager.Instance.MaxMoves = _maxPtsMoves;
        MoveManager.Instance.EnableMoveCounter();
        gameObject.SetActive(false);
    }
}
