using System;
using UnityEngine;

namespace MyPlatform.Model
{
    [Serializable]
    public class PlayerData
    {
        public int Coins;
        public int Hp;
        public bool IsArmed;

        
        public PlayerData Clone()
        {
            return new PlayerData()//�������� ����������
            {
                Coins = Coins,
                Hp = Hp,
                IsArmed = IsArmed
            };
        }
        
        /* ������ ������
        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json); //��������� �� 05 ������ ������ 45 ������
        }
        */
    }
}

