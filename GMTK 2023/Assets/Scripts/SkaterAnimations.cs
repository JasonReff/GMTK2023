using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkaterAnimations : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _animationTime = 0.1f;
    [SerializeField] private List<Sprite> _pumpSprites, _kickflipSprites, _jumpSprites, _crouchSprites, _startGrindSprites, _endGrindSprites, _startRampSprites, _endRampSprites;
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

    public void Jump(bool kickflip)
    {
        if (_currentAnimation != null)
            StopCoroutine(_currentAnimation);
        if (kickflip)
        {
            _currentAnimation = StartCoroutine(AnimationCoroutine(_kickflipSprites));
        }   
        else
        {
            _currentAnimation = StartCoroutine(AnimationCoroutine(_jumpSprites));
        }
    }

    public void Crouch()
    {
        if (_currentAnimation != null)
            StopCoroutine(_currentAnimation);
        _currentAnimation = StartCoroutine(AnimationCoroutine(_crouchSprites));
    }

    public void StartGrind()
    {
        if (_currentAnimation != null)
            StopCoroutine(_currentAnimation);
        _currentAnimation = StartCoroutine(AnimationCoroutine(_startGrindSprites));
    }

    public void EndGrind()
    {
        if (_currentAnimation != null)
            StopCoroutine(_currentAnimation);
        _currentAnimation = StartCoroutine(AnimationCoroutine(_endGrindSprites));
    }

    public void StartRamp()
    {
        if (_currentAnimation != null)
            StopCoroutine(_currentAnimation);
        _currentAnimation = StartCoroutine(AnimationCoroutine(_startRampSprites));
    }

    public void EndRamp()
    {
        if (_currentAnimation != null)
            StopCoroutine(_currentAnimation);
        _currentAnimation = StartCoroutine(AnimationCoroutine(_endRampSprites));
    }
}