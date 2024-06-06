using UnityEngine;

/// <summary>
/// Controller used by game objects that deal damage (= reset Jeremy).
/// The only purpose of this script is to reset Jeremy
/// upon triggering the collider attached to the damaging
/// game object. The damaging game object should have both this
/// script and the 2d collider components.
/// </summary>
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
