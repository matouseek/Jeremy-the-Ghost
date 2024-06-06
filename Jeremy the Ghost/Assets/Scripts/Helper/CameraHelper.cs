using UnityEngine;

/// <summary>
/// Helper class for things related to cameras.
/// </summary>
public class CameraHelper : MonoBehaviour
{
    /// <summary>
    /// Swaps the priority of 2 cameras.
    /// </summary>
    public static void SwapCameraPriority(Cinemachine.CinemachineVirtualCamera previousCamera,
        Cinemachine.CinemachineVirtualCamera newCamera)
    {
        (previousCamera.Priority, newCamera.Priority) = (newCamera.Priority, previousCamera.Priority);
    }
}
