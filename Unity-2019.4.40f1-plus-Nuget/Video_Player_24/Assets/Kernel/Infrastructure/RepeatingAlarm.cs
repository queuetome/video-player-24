using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using ILogger = Kernel.Logger.ILogger;

namespace Kernel.Infrastructure
{
    public class RepeatingAlarm : MonoBehaviour
    {
        [SerializeField] private int _intervalInSeconds = 60;
        [SerializeField] private UnityEvent _callMethod;
        private bool _timeIsRunning;
        private float _elapsedTime;

        private void OnValidate()
        {
            _intervalInSeconds = Math.Max(0, _intervalInSeconds);
            string label = $"Call {_callMethod?.GetPersistentMethodName(0)} every ";
            
            label += _intervalInSeconds % 60 == 0
                ? $"{_intervalInSeconds / 60} min"
                : $"{_intervalInSeconds} sec";
            
            gameObject.name = label;
        }

        private void Start()
        {
            _timeIsRunning = true;
        }
        
        private void Update()
        {
            if (_timeIsRunning)
            {
                _elapsedTime += Time.unscaledDeltaTime;

                if (_elapsedTime > _intervalInSeconds)
                {
                    _callMethod.Invoke();
                    Repeat();
                }
            }
        }

        private void Set(int intervalInSeconds)
        {
            _intervalInSeconds = intervalInSeconds;
            _timeIsRunning = true;
        }

        private void TurnOff()
        {
            _elapsedTime = 0f;
            _timeIsRunning = false;
        }

        private void Repeat()
        {
            TurnOff();
            Set(_intervalInSeconds);
        }
    }
}