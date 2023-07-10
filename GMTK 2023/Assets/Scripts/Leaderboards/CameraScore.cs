using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class CameraScore : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private CameraZoom _zoom;
    [SerializeField] private BlurManager _blurManager;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private float _scorePerSecond, _focusMultiplier, _zoomMultiplier;
    [SerializeField] private TextMeshProUGUI _zoomTextbox, _focusTextbox, _frameTextbox, _comboTextbox, _comboFinishedTextbox;
    [SerializeField] private AudioClip _comboSound;
    private float _comboTimer, _comboScore, _longestCombo, _timerLength = 0f;
    private int _overlappingColliders = 0;
    private bool _skaterInFocus;

    public float LongestCombo { get => _longestCombo; }

    private void Update()
    {
        CalculateScore();
        _comboTextbox.text = $"Combo: {(int)_comboScore}";
        if (_overlappingColliders == 0)
        {
            _comboTimer += Time.deltaTime;
            if (_comboTimer >= _timerLength && _comboScore != 0)
            {
                if (_comboScore > _longestCombo)
                    _longestCombo = _comboScore;
                AddScore(_comboScore);
                _comboScore = 0;
            }
        }
        else
        {
            _comboTimer = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Skater skater))
        {
            RateSkater(skater);
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
        if (score > 100)
            AudioManager.PlaySoundEffect(_comboSound);
        _scoreManager.AddScore(score);
        StartCoroutine(ComboFinishedCoroutine());

        IEnumerator ComboFinishedCoroutine()
        {
            var startingY = _comboFinishedTextbox.transform.localPosition.y;
            var startingScale = _comboFinishedTextbox.transform.localScale.x;
            _comboFinishedTextbox.enabled = true;
            _comboFinishedTextbox.text = $"+{(int)score}";
            _comboFinishedTextbox.transform.DOLocalMoveY(startingY - 1f, 1f);
            _comboFinishedTextbox.transform.DOScale(startingScale * 1.2f, 1f);
            yield return new WaitForSeconds(1f);
            _comboFinishedTextbox.enabled = false;
            _comboFinishedTextbox.transform.localPosition = new Vector2(_comboFinishedTextbox.transform.localPosition.x, startingY);
            _comboFinishedTextbox.transform.DOScale(startingScale, 0f);
        }
    }

    public void EndCombo()
    {
        if (_comboScore > _longestCombo)
            _longestCombo = _comboScore;
        AddScore(_comboScore);
    }
}