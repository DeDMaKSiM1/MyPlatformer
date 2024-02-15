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
            return new PlayerData()//наиболее эффективно
            {
                Coins = Coins,
                Hp = Hp,
                IsArmed = IsArmed
            };
        }
        
        /* другой способ
        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json); //подробнее на 05 пятнич стриме 45 минута
        }
        */
    }
}

