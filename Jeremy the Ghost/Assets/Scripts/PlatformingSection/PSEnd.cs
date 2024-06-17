using UnityEngine;

/// <summary>
/// Used after a chain of platforming sections
/// to change cameras and hide move counter.
/// </summary>
public class PSEnd : MonoBehaviour
{
    [SerializeField] private GameObject _noGoingBackCollider;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _previousCamera;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _newCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CameraHelper.SwapCameraPriority(_previousCamera, _newCamera);
        _noGoingBackCollider.SetActive(true);
        MoveManager.Instance.DisableMoveCounter();
        gameObject.SetActive(false);
    }
}
