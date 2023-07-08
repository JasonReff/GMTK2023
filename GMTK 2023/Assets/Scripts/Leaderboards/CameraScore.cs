using UnityEngine;

public class CameraScore : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private CameraZoom _zoom;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private float _scorePerSecond, _trickMultiplier, _focusMultiplier, _zoomMultiplier;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Skater skater))
        {
            ScorePerFrame(skater);
        }
    }

    private void ScorePerFrame(Skater skater)
    {
        var baseScore = _scorePerSecond * Time.deltaTime;
        if (skater.IsDoingTrick)
        {
            baseScore *= _trickMultiplier;
        }
        if (skater.IsInFocus())
        {
            baseScore *= _focusMultiplier;
        }
        baseScore *= _zoom.ZoomLevel() * _zoomMultiplier;
        //factor in overlap
        AddScore(baseScore);
    }

    private void AddScore(float score)
    {
        _scoreManager.AddScore(score);
    }
}