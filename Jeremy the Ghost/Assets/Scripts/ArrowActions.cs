using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowActions : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera camera1;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera camera2;
    
    [SerializeField] private GameObject noGoingBackCollider;

    [SerializeField] private bool hideMoves = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        camera1.Priority = 0;
        camera2.Priority = 100;
        noGoingBackCollider.SetActive(true);
        if (hideMoves)
        {
            GameObject.Find("MoveController").GetComponent<MoveController>().DisableMoveCounterTMP();
        }
    }
}
