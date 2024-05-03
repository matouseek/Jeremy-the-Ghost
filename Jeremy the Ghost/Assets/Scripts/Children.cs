using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Children : MonoBehaviour
{
    [SerializeField] private GameObject scareChildrenText;
    [SerializeField] private JeremyController jeremy;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        scareChildrenText.SetActive(true);
        jeremy.CanScare = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        scareChildrenText.SetActive(false);
        jeremy.CanScare = false;
    }
}
