using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyPlatform.Components
{
    public class TimerComponent : MonoBehaviour
    {
        [SerializeField] private TimerData[] _timers;

        public void SetTimer(int index)
        {
            var timer = _timers[index];

            StartCoroutine(StartTimer(timer));

        }

        private IEnumerator StartTimer(TimerData timer)
        {
            yield return new WaitForSeconds(timer.Delay);
            timer.OnTimesUp?.Invoke();
        }

        //��� ��� � ��� �������� ����� ���� >1 ������� TImerCollection
        [Serializable]
        public class TimerData
        {
            public float Delay;
            public UnityEvent OnTimesUp;

        }

    }
}

