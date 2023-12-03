using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyPlatform
{
    [RequireComponent(typeof(SpriteRenderer))]

    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField] private int _frameRate; //Указали какой будет частота кадров 
        [SerializeField] private bool _loop;     //Будет ли циклиться
        [SerializeField] private Sprite[] sprites;//Массив спрайтов, которые будут меняться для анимации
        [SerializeField] UnityEvent _onComplete;  //Ивент, когда у нас все закончится
        //Далее сервисные переменные
        private SpriteRenderer _renderer; //Наш рендерер у которого мы будем менять спрайты
        private float _secondsPerFrame;   //Сколько секунд уходит на показ одного спрайта
        private int _currentSpriteIndex;  //Текущий индекс спрайта из массива
        private float _nextFrameTime;     //Время для следующего апдейта



        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>(); //Забираем компонент для переменной

        }

        //private void OnBecameVisible()
        //{
        //    enabled = false;
        //}
        //private void OnBecameInvisible()
        //{
        //    enabled = false;
        //}

        private void OnEnable()
        {
            _secondsPerFrame = 1f / _frameRate;         //Расчитываем, сколько будет длиться один кадр по времени
            _nextFrameTime = Time.time + _secondsPerFrame;//Задаем следующий апдейт нашего кадра
            _currentSpriteIndex = 0;
        }
        private void Update()
        {

            if (_nextFrameTime > Time.time) return;//Проверяем, проигрывается ли и наступило ли время смены кадра - если нет, просто выходим из функции


            if (_currentSpriteIndex >= sprites.Length)//Если наступило - проверяем не вышли ли мы за пределы массива
            {
                if (_loop) //Если зацикленная анимация - то просто индекс спрайта сбрасываем на 0
                {
                    _currentSpriteIndex = 0;
                }
                else       //Если нет - заканчиваем проигрывание анимации и вызываем CallBack что она закончилась
                {
                    enabled = false;
                    _onComplete?.Invoke();
                    return;
                }
            }//Но если не вышли за пределы массива, то меняем спрайт, обновляем время до след изменения и говорим что в след раз установим спрайт на 1 больше.
            _renderer.sprite = sprites[_currentSpriteIndex];
            _nextFrameTime += _secondsPerFrame;
            _currentSpriteIndex++;

        }

    }
}

