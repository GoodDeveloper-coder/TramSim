using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _startDayTime = 6;
    [SerializeField] private int _finishDayTime = 18;
    public int _days { get; private set; }
    public int _hours { get; private set; }
    public int _minutes { get; private set; }
    public float _seconds { get; private set; }

    [Header("UI")]
    [SerializeField] private TMP_Text _dayText;
    [SerializeField] private TMP_Text _timeText;
    private bool _nextDay = false;
    private bool _waitForNextDay = false;

    void Start()
    {
        _hours = _startDayTime;
    }

    void Update()
    {
        UpdateTime();
    }

    void UpdateTime()
    {
        if (!_waitForNextDay)
            _seconds += Time.deltaTime * 100;

        if (_seconds >= 60f)
        {
            _seconds = 0f;
            if (++_minutes >= 60)
            {
                _minutes = 0;
                if (++_hours >= _finishDayTime)
                {
                    _waitForNextDay = true;
                    if (_nextDay)
                    {
                        _hours = _startDayTime;
                        _days++;
                    }
                }
                else
                {
                    _nextDay = false;
                }
            }
        }

        _dayText.text = "Day: " + _days;
        _timeText.text = "Time: " + _hours + ":" + _minutes;
    }

    public void NextDay()
    {
        _nextDay = true;
        _waitForNextDay = false;
        _days++;
        _seconds = 0f;
        _minutes = 0;
        _hours = _startDayTime;
    }
}
