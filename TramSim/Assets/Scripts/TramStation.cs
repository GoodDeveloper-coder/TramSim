using TMPro;
using UnityEngine;

public class TramStation : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _minDistance = 30f;
    [SerializeField] private float _waitTime = 10f;
    [SerializeField] private TramController _tram;

    [Header("UI")]
    [SerializeField] private TMP_Text _waitTimeText;
    private float _currentWaitTime = 0f;
    private bool _nextStation = false;
    private bool _isWaiting = false;

    void Start()
    {

    }

    void Update()
    {
        if (_nextStation)
            return;
            
        if (Vector3.Distance(_tram.transform.position, transform.position) <= _minDistance)
        {

            if (!_isWaiting)
            {
                _isWaiting = true;
                _waitTimeText.gameObject.SetActive(true);
            }

            UpdateWaitTime();
        }
        else
        {
            if (_isWaiting)
            {
                _tram.SetCanOpenDoor(false);
                _waitTimeText.gameObject.SetActive(false);
            }

            _isWaiting = false;
            _currentWaitTime = 0f;
        }
    }

    void UpdateWaitTime()
    {
        if (_nextStation)
            return;

        if (_currentWaitTime >= _waitTime)
        {
            _nextStation = true;
            _isWaiting = false;
            _currentWaitTime = 0f;
            _tram.SetCanOpenDoor(true);
            _waitTimeText.gameObject.SetActive(false);
        }
        else
        {
            _currentWaitTime += Time.deltaTime;
            string waitTimeText = string.Format("{0:#.0}", _currentWaitTime);
            if (_currentWaitTime < 1f)
                waitTimeText = "0" + waitTimeText;
            _waitTimeText.text = waitTimeText;
        }
    }
}
