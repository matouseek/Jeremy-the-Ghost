using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sawblade : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponentInChildren<JeremyController>().Reset();
        }
    }
}
