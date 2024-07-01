using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInObjects : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _spritesToFadeIn;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LerpAlphaChannel());
    }

    private IEnumerator LerpAlphaChannel()
    {
        //todo: NOT WORKING - REDO
        int numOfIterations = 1000;
        for (int i = 0; i < numOfIterations; ++i)
        {
            Debug.Log(i);
            foreach(var sprite in _spritesToFadeIn)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b,
                    sprite.color.a + 1f/numOfIterations);
            }
            yield return new WaitForSeconds(1f / numOfIterations);
        }
    }

}
