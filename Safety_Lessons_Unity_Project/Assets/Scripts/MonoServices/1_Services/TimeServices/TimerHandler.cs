using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.MonoTime
{
    public class TimerHandler : MonoService
    {
        [SerializeField] float _timer = 0;
        [SerializeField] bool _beginOnStart = false;
        [SerializeField] bool _pauseTimer = false;

        bool _timerCompleteCalled = false;
        IEnumerator _timerCoroutine;
        float _timeRemaining;

        protected override void Start()
        {
            base.Start();

            if (_beginOnStart)
                StartTimerCommand();
        }

        IEnumerator Countdown(float timeRemaining)
        {
            _timeRemaining = timeRemaining;
            _pauseTimer = false;

            while (timeRemaining > 0)
            {
                if (!_pauseTimer)
                {
                    timeRemaining -= Time.deltaTime;
                    _timeRemaining = timeRemaining;
                    GetTimeRemainingCommand();
                }
                yield return null;
            }

            _timeRemaining = 0;
            GetTimeRemainingCommand();
            _timerCompleteCalled = true;
            TimerCompleteCommand();
        }

        void TimerCompleteCommand() =>
            InvokeCommand(0);

        void ResetTimer()
        {
            if (_timerCoroutine != null)
            {
                if (_timerCompleteCalled)
                    StopCoroutine(_timerCoroutine);
            }

            StartCoroutine(_timerCoroutine = Countdown(_timer));
        }

        void GetTimeRemainingCommand()
        {
            float timeRemainingPercentage = _timeRemaining / _timer;
            InvokeCommand(1, timeRemainingPercentage);
            GetTimeRemainingAsTextCommand();
        }

        void SetTimeRemainingCommand(float time)
        {
            _timer = time;
            ResetTimer();
        }

        void StartTimerCommand() =>
            ActivateCoroutine(Countdown(_timer));

        void GetTimeRemainingAsTextCommand()
        {
            InvokeCommand(4, _timeRemaining.ToString());
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 2) SetTimeRemainingCommand((float)passedObj);
            if (methodNumb == 3) StartTimerCommand();
        }
    }
}