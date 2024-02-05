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

        //Так как у нас таймеров может быть >1 сделаем TImerCollection
        [Serializable]
        public class TimerData
        {
            public float Delay;
            public UnityEvent OnTimesUp;

        }

    }
}

