using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkaterLane : MonoBehaviour
{
    [SerializeField] private int _focusLayer;
    [SerializeField] private LaneObjectPool _objectPool;
    [SerializeField] private float _spawnRate;
    [SerializeField] private Transform _objectSpawnPoint;
    [SerializeField] private float _spawnTimer;
    [SerializeField] private float _objectSize;
    [SerializeField] private SkaterLane _upLane, _downLane;
    [SerializeField] private int _sortingLayer;
    [SerializeField] private float _skaterPosition;

    public int FocusLayer { get => _focusLayer; }
    public SkaterLane UpLane { get => _upLane;  }
    public SkaterLane DownLane { get => _downLane;  }
    public int SortingLayer { get => _sortingLayer; }
    public float SkaterPosition { get => _skaterPosition; }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            var laneObject = Instantiate(_objectPool.RandomObject(), _objectSpawnPoint.transform.position, Quaternion.identity);
            laneObject.transform.localScale = new Vector3(_objectSize, _objectSize, _objectSize);
            laneObject.SetLane(this);
            _spawnTimer = _spawnRate;
        }
    }
}
