using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyPlatform
{
    [RequireComponent(typeof(SpriteRenderer))]

    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField] private int _frameRate; //������� ����� ����� ������� ������ 
        [SerializeField] private bool _loop;     //����� �� ���������
        [SerializeField] private Sprite[] sprites;//������ ��������, ������� ����� �������� ��� ��������
        [SerializeField] UnityEvent _onComplete;  //�����, ����� � ��� ��� ����������
        //����� ��������� ����������
        private SpriteRenderer _renderer; //��� �������� � �������� �� ����� ������ �������
        private float _secondsPerFrame;   //������� ������ ������ �� ����� ������ �������
        private int _currentSpriteIndex;  //������� ������ ������� �� �������
        private float _nextFrameTime;     //����� ��� ���������� �������



        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>(); //�������� ��������� ��� ����������

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
            _secondsPerFrame = 1f / _frameRate;         //�����������, ������� ����� ������� ���� ���� �� �������
            _nextFrameTime = Time.time + _secondsPerFrame;//������ ��������� ������ ������ �����
            _currentSpriteIndex = 0;
        }
        private void Update()
        {

            if (_nextFrameTime > Time.time) return;//���������, ������������� �� � ��������� �� ����� ����� ����� - ���� ���, ������ ������� �� �������


            if (_currentSpriteIndex >= sprites.Length)//���� ��������� - ��������� �� ����� �� �� �� ������� �������
            {
                if (_loop) //���� ����������� �������� - �� ������ ������ ������� ���������� �� 0
                {
                    _currentSpriteIndex = 0;
                }
                else       //���� ��� - ����������� ������������ �������� � �������� CallBack ��� ��� �����������
                {
                    enabled = false;
                    _onComplete?.Invoke();
                    return;
                }
            }//�� ���� �� ����� �� ������� �������, �� ������ ������, ��������� ����� �� ���� ��������� � ������� ��� � ���� ��� ��������� ������ �� 1 ������.
            _renderer.sprite = sprites[_currentSpriteIndex];
            _nextFrameTime += _secondsPerFrame;
            _currentSpriteIndex++;

        }

    }
}

