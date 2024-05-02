using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallReset : MonoBehaviour
{

    [SerializeField] private JeremyController jeremy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        jeremy.Reset();
    }
}
