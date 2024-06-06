using UnityEngine;

public class DamagingObjectController : MonoBehaviour
{
    private readonly string _jeremyTag = "Jeremy";
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_jeremyTag))
        {
            other.GetComponentInChildren<JeremyController>().Reset();
        }
    }
}
