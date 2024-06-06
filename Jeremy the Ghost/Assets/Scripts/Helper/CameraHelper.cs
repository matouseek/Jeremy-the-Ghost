using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHelper : MonoBehaviour
{
    public static void SwapCameraPriority(Cinemachine.CinemachineVirtualCamera previousCamera,
        Cinemachine.CinemachineVirtualCamera newCamera)
    {
        (previousCamera.Priority, newCamera.Priority) = (newCamera.Priority, previousCamera.Priority);
    }
}
