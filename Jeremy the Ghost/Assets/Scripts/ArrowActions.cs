using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ArrowActions : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera camera1;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera camera2;
    
    [SerializeField] private GameObject noGoingBackCollider;

    [SerializeField] private bool hideMoves = false;
    [SerializeField] private bool setNewMax = false;
    [SerializeField] private int newMovesMax = 0;

    [SerializeField] [CanBeNull] private GameObject newRespawn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        camera1.Priority = 0;
        camera2.Priority = 100;
        noGoingBackCollider.SetActive(true);
        if (hideMoves)
        {
            GameObject.Find("MoveController").GetComponent<MoveController>().DisableMoveCounterTMP();
        }
        else if (setNewMax)
        {
            GameObject.Find("MoveController").GetComponent<MoveController>().SetMax(newMovesMax);
        }
        if (newRespawn is not null)
        {
            GameObject.Find("JeremyParent").GetComponentInChildren<JeremyController>().puzzleRespawn = newRespawn.transform;
        }
    }
}
