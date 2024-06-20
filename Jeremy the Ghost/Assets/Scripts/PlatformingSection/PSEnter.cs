using System;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// This class serves as an entry point for the platforming sections.
/// It handles the camera change when starting the section,
/// activates the collider that prevents the player from going back
/// from the section and sets up the MoveManager for the current PS.
/// </summary>
public class PSEnter : MonoBehaviour
{
    [SerializeField] private GameObject _noGoingBackCollider;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _previousCamera;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _newCamera;
    [SerializeField] private int _maxPsMoves;
    public int MaxPsMoves => _maxPsMoves;
    [SerializeField] private Transform _newRespawn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CameraHelper.SwapCameraPriority(_previousCamera, _newCamera);
        _noGoingBackCollider.SetActive(true);
        MoveManager.Instance.MaxMoves = _maxPsMoves;
        MoveManager.Instance.EnableMoveCounter();
        GameObject.Find("Jeremy").GetComponent<JeremyController>().PSRespawn = _newRespawn;
        gameObject.SetActive(false);
    }
}
