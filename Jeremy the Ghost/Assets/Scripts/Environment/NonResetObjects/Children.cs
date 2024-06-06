using UnityEngine;

public class Children : MonoBehaviour
{
    // Text that appears when jeremy is in range to scare children
    [SerializeField] private GameObject _scareChildrenText;
    [SerializeField] private JeremyController _jeremy;

    [SerializeField] private bool _changeCamerasOnTriggerEnter;
    // Whatever camera is used before triggering the collider
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _previousCamera;
    // Camera used when jeremy has the option to scare the children (triggers the collider)
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _newCamera;

    private void Start()
    {
        ValidateFields();
    }
    
    private void ValidateFields()
    {
        if (_changeCamerasOnTriggerEnter && (_previousCamera == null || _newCamera == null))
        {
            Debug.LogError($"{nameof(_changeCamerasOnTriggerEnter)} is set to true " +
                           $"but {nameof(_previousCamera)} or {nameof(_newCamera)} are null.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_changeCamerasOnTriggerEnter)
        {
            CameraHelper.SwapCameraPriority(_previousCamera, _newCamera);
        }
        _scareChildrenText.SetActive(true);
        _jeremy.CanScare = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_changeCamerasOnTriggerEnter)
        {
            CameraHelper.SwapCameraPriority(_newCamera, _previousCamera);
        }
        _scareChildrenText.SetActive(false);
        _jeremy.CanScare = false;
    }
}
