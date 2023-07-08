using UnityEngine;

public class Skater : MonoBehaviour
{
    [SerializeField] private BlurrableObject _blurrable;
    [SerializeField] private SkaterLane _currentLane;
    [SerializeField] private float _speedLoss = 1f, _pumpRate = 2f, _pumpForce = 5f;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SkaterAnimations _animations;
    private float _pumpTimer;

    public void MoveToLane(LaneTransition transition)
    {
        _currentLane = transition.Lane2;
        _blurrable.ChangeLayer(_currentLane.FocusLayer);
    }

    private void Start()
    {
        _pumpTimer = _pumpRate;
    }

    private void Update()
    {
        _pumpTimer -= Time.deltaTime;
        if (_pumpTimer < 0)
        {
            Pump();
            _pumpTimer = _pumpRate;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(new Vector2(-_speedLoss, 0f), ForceMode2D.Impulse);
    }

    public void Pump()
    {
        _rigidbody.AddForce(new Vector2(_pumpForce, 0f), ForceMode2D.Impulse);
        _animations.Pump();
    }
}
