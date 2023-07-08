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

    public int FocusLayer { get => _focusLayer; }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            var laneObject = Instantiate(_objectPool.RandomObject(), _objectSpawnPoint.transform.position, Quaternion.identity);
            laneObject.Lane = this;
        }
    }
}
