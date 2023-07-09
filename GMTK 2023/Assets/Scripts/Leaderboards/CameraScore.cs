using System.Collections;
using TMPro;
using UnityEngine;

public class CameraScore : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private CameraZoom _zoom;
    [SerializeField] private BlurManager _blurManager;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private float _scorePerSecond, _focusMultiplier, _zoomMultiplier;
    [SerializeField] private TextMeshProUGUI _zoomTextbox, _focusTextbox, _frameTextbox, _comboTextbox;
    private float _comboTimer, _comboScore;
    private int _overlappingColliders = 0;
    private bool _isComboActive, _skaterInFocus;
    private Coroutine _comboEnd;

    private void Update()
    {
        CalculateScore();
        if (_isComboActive && _comboScore >= 100)
        {
            _comboTextbox.text = $"Combo: {(int)_comboScore}";
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Skater skater))
        {
            RateSkater(skater);
        }
        else
        {
            if (_isComboActive)
                _comboEnd = StartCoroutine(EndComboCoroutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out SkaterPart part))
        {
            _overlappingColliders++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out SkaterPart part))
        {
            if (_overlappingColliders > 0)
                _overlappingColliders--;
        }
    }

    private void CalculateScore()
    {
        if (_comboEnd != null)
        {
            StopCoroutine(_comboEnd);
        }
        _isComboActive = true;
        var baseScore = _scorePerSecond * _overlappingColliders * Time.deltaTime;
        switch (_overlappingColliders)
        {
            case 0:
                _frameTextbox.text = "Framing: Lame";
                _frameTextbox.color = Color.red;
                break;
            case 1:
                _frameTextbox.text = "Framing: OK";
                _frameTextbox.color = new Color(255, 165, 0);
                break;
            case 2:
                _frameTextbox.text = "Framing: Nice";
                _frameTextbox.color = Color.yellow;
                break;
            case 3:
                _frameTextbox.text = "Framing: Radical";
                _frameTextbox.color = Color.green;
                break;
            default:
                break;
        }
        if (_skaterInFocus)
        {
            baseScore *= _focusMultiplier;
            _focusTextbox.text = "Focus: Crisp";
            _focusTextbox.color = Color.green;
        }
        else
        {
            _focusTextbox.text = "Focus: Whack";
            _focusTextbox.color = Color.red;
        }
        baseScore *= _zoom.ZoomLevel() * _zoomMultiplier;
        if (_zoom.ZoomLevel() * _zoomMultiplier > 1.5f)
        {
            _zoomTextbox.text = "Zoom: Tight";
            _zoomTextbox.color = Color.green;
        }
        else
        {
            _zoomTextbox.text = "Zoom: Far Out";
            _zoomTextbox.color = Color.red;
        }
        _comboScore += baseScore;
    }

    private void RateSkater(Skater skater)
    {
        _skaterInFocus = skater.GetComponent<BlurrableObject>().FocusLayer == _blurManager.FocusLayer;
    }

    private void AddScore(float score)
    {
        _scoreManager.AddScore(score);
    }

    private IEnumerator EndComboCoroutine()
    {
        _isComboActive = false;
        yield return new WaitForSeconds(_comboTimer);
        AddScore(_comboScore);
        _comboScore = 0;
    }
}