using UnityEngine;

public class ThornBulletController : MonoBehaviour
{
    private float _flyingSpeed = 30f;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * (_flyingSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //TODO reduce flying speed to 0
        // start an animation to rot inside (change color/size) in whatever was hit
        // if player was hit -> reset him
    }
}
