using System;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectSpriteFade : MonoBehaviour
{
    /// <summary>
    /// This class takes a list of sprites, a moving object (usually the player)
    /// and a start and end location of the fade effect.
    /// When the moving object is before start location nothing happens.
    /// By moving from start to end the sprites fade out (lower alpha value).
    /// When the moving object is after the end location all sprites are faded out (alpha = 0).
    /// </summary>
    
    [SerializeField] private List<SpriteRenderer> _spritesToFade;
    [SerializeField] private Transform _movingObject;
    [SerializeField] private Transform _fadeEndLocation;
    [SerializeField] private Transform _fadeStartLocation;
    private float _fadeDiff;

    private void Start()
    {
        if (_fadeEndLocation.localPosition.x < _fadeStartLocation.localPosition.x)
        {
            throw new ArgumentException(
                "_fadeEndLocation X position has to be greater than or equal to _fadeStartLocation X position.");
        }
        _fadeDiff = MathF.Abs(_fadeEndLocation.localPosition.x 
                              - _fadeStartLocation.localPosition.x);
    }

    private void Update()
    {
        float movingObjectX = _movingObject.localPosition.x;
        float objectsDistanceToEnd = _fadeEndLocation.localPosition.x - movingObjectX;
        
        // objectsDistanceToEnd will be higher than _fadeDiff until moving object reaches _fadeStartLocation
        // resulting in newAlphaValue = 1, then after passing _fadeEndLocation the MathF.Min will produce
        // a value < 0 resulting in newAlphaValue = 0
        var newAlphaValue = MathF.Max(
            MathF.Min(1, objectsDistanceToEnd / _fadeDiff),
            0);
        
        foreach (var sprite in _spritesToFade)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, newAlphaValue);
        }
    }
}
