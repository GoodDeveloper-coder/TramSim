using UnityEngine;

public class TramDepot : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _minDistance = 30f;
    [SerializeField] private float _waitTime = 10f;
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private Transform _tram;
    private float _currentWaitTime = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (Vector3.Distance(_tram.position, transform.position) <= _minDistance)
        {
            _currentWaitTime += Time.deltaTime;
            if (_currentWaitTime >= _waitTime)
            {
                _timeManager.NextDay();
                _currentWaitTime = 0f;
            }
        }
        else
        {
            _currentWaitTime = 0f;
        }
    }
}
