using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartUpMenu : MonoBehaviour
{
    [SerializeField] private float _fadeDuration, _fadeDelay, _moveDuration, _moveDelay, _logoSize = 0.5f;
    [SerializeField] private Vector2 _logoPosition;
    [SerializeField] private Image _backdrop, _logo;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_fadeDelay);
        _backdrop.DOFade(0f, _fadeDuration);
        yield return new WaitForSeconds(_moveDelay);
        _logo.transform.DOLocalMove(_logoPosition, _moveDuration);
        _logo.transform.DOScale(_logoSize, _moveDuration);
    }


}
