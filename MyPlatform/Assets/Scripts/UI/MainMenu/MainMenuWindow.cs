using System;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace MyPlatform.UI.MainMenu
{
    public class MainMenuWindow : AnimatedWindow
    {
        private Action _closeAction;
        public void OnShowSettings()
        {
            var window = Resources.Load<GameObject>("UI/SettingsWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }

        public void OnStartGame()
        {
            _closeAction = () => { SceneManager.LoadScene("Level1"); };
            Close();
        }
        public void OnExit()
        {
            _closeAction = () =>//анонимная функция
            {
                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            };
            Close();
        } 

        public override void OnCloseAnimationComplete()
        {            
            _closeAction?.Invoke(); ;
            base.OnCloseAnimationComplete();

        }
    }
}

