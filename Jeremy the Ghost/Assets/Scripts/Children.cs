using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Children : MonoBehaviour
{
    [SerializeField] private GameObject scareChildrenText;
    [SerializeField] private JeremyController jeremy;

    [SerializeField] private Cinemachine.CinemachineVirtualCamera previousCamera;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera jeremyAndChildrenCamera;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        previousCamera.Priority = 10;
        jeremyAndChildrenCamera.Priority = 100;
        scareChildrenText.SetActive(true);
        jeremy.CanScare = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        previousCamera.Priority = 100;
        jeremyAndChildrenCamera.Priority = 10;
        scareChildrenText.SetActive(false);
        jeremy.CanScare = false;
    }
}
