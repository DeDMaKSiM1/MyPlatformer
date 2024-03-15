using MyPlatform.Model.Data;
using MyPlatform.UI.Widgets;
using UnityEngine;

namespace MyPlatform.UI.Settings
{
    public class SettingsWindow : AnimatedWindow
    {

        [SerializeField] private AudioSettingsWidget _music;
        [SerializeField] private AudioSettingsWidget _sfx;

        protected override void Start()
        {
            base.Start();

            _music.SetModel(GameSettings.Instance.Music);
            _sfx.SetModel(GameSettings.Instance.Sfx);
        }
    }
}

