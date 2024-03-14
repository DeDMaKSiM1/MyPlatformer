using MyPlatform.Model.Data.Properties;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatform.Model.Data
{
    [CreateAssetMenu(menuName = "Data/GameSettings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private FloatPersistentProperty _music;

        [SerializeField] private FloatPersistentProperty _sfx;

        public  FloatPersistentProperty Music => _music;
        public FloatPersistentProperty Sfx => _sfx;

        private static GameSettings _instance;
        public static GameSettings Instance => _instance == null ?  LoadGameSettings() : _instance;

        private static GameSettings LoadGameSettings()
        {
            return _instance = Resources.Load<GameSettings>("GameSettings");
        }

        private void OnEnable()
        {
            _music = new FloatPersistentProperty(1, SoundSetting.Music.ToString());
            _sfx = new FloatPersistentProperty(1, SoundSetting.SFX.ToString());
        }

        private void OnValidate()
        {
            Music.Validate();
            Sfx.Validate();
        }
    }

    public enum SoundSetting
    {
        Music,
        SFX
    }
}

