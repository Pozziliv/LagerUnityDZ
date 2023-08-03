using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour, IEventable
{
    [SerializeField] private float _timeOfLevel = 120f;
    [SerializeField] private Image _img;
    private float _timerMultiplyer;
    private float _nowTime;

    private bool _notCaught = true;

    private void Start()
    {
        _timerMultiplyer = 1 / _timeOfLevel;
    }

    private void StopTime()
    {
        _notCaught = false;
    }

    public void OnEnable()
    {
        PlayerMovement.Caught += StopTime;
        VideoCollecting.OnAllVideo += StopTime;
    }

    public void OnDisable()
    {
        VideoCollecting.OnAllVideo -= StopTime;
        PlayerMovement.Caught -= StopTime;
    }

    private void Update()
    {
        if (_notCaught)
        {
            _nowTime += Time.deltaTime;
            _img.fillAmount = 1 - _nowTime * _timerMultiplyer;
            if (_nowTime > _timeOfLevel)
            {
                PlayerMovement.Caught.Invoke();
                _notCaught = false;
            }
        }
    }
}
