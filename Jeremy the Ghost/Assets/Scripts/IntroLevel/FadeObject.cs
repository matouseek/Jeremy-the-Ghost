using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObject : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> renderersToFade;
    [SerializeField] private Transform movingObject;
    [SerializeField] private Transform endFade;
    [SerializeField] private Transform startFade;
    private float fadeDiff;

    private void Start()
    {
        fadeDiff = MathF.Abs(endFade.localPosition.x - startFade.localPosition.x);
    }

    private void Update()
    {
        float movingObjectX = movingObject.localPosition.x;
        float distanceToEnd = endFade.localPosition.x - movingObjectX;
        foreach (var renderer in renderersToFade)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b,
                MathF.Min(1, distanceToEnd / fadeDiff));
        }
    }
}
