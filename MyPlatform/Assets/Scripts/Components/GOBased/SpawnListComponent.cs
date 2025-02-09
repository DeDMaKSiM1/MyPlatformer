using System;
using System.Linq;
using UnityEngine;
namespace MyPlatform.Components.GOBased
{
    public class SpawnListComponent : MonoBehaviour
    {
        //����� ������������ ��������� �����-�����������, ������� �� ����� �������� �� �� �����
        //��� ���� ������������ ������ ����� �� � ����������, � � ������ ����������� ������� SpawnData
        [SerializeField] private SpawnData[] _spawners;


        public void SpawnAll()
        {
            foreach(var spawnData in _spawners)
            {
                spawnData.Component.Spawn();
            } 
        }
        //�������� ����� Spawn, ������� ����� �������� ��, ��� �� ����� ����������
        public void Spawn(string id)
        {
            /*
            foreach(var data in _spawners)
            {
                if(data.id == id)
                {
                    data.Component.Spawn();
                    break;
                }
            }
            ������ �� �� �����, ��� � ���� �����������
            *///������ ����������, ����� �������
            //������������� ����������� Linq � �������� � ����� �������� � (). �������� - ������� � ����������� ����, �����
            //���������� �� ������ �������� _spawners � � ��� ������� ����� ���������� ������ �������(element) � ��� �������
            //�������� ����� �������� ���� id ����� element ����� id, ������� �� ��������, �� �� ������ ������ ������� �������
            var spawner = _spawners.FirstOrDefault(element => element.id == id);
            spawner?.Component.Spawn();//���������� ����� ������(?.), �.�. spawner ����� ���� = null
            //���� ����� ������� ���(������� ���������� � Update ��������) ����� �� ������������ ����������� Linq, �.�. �� �����
            //�������� �����, ������� ����� �������� �� �����������.
        }
        //������� ��������� �����, ��� ������� �����, ��� � ������??
        [Serializable]
        public class SpawnData
        {
            public string id;
            public SpawnComponent Component;

        }
    }

}

