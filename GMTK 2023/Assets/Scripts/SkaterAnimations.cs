using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkaterAnimations : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _animationTime = 0.1f;
    [SerializeField] private List<Sprite> _pumpSprites;
    private Coroutine _currentAnimation;
    public IEnumerator AnimationCoroutine(List<Sprite> sprites)
    {
        foreach (var sprite in sprites)
        {
            _spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(_animationTime);
        }
    }

    public void Pump()
    {
        if (_currentAnimation != null)
            StopCoroutine(_currentAnimation);
        _currentAnimation = StartCoroutine(AnimationCoroutine(_pumpSprites));
    }
}