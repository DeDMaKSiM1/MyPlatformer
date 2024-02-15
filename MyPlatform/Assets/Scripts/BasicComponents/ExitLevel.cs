using MyPlatform.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyPlatform.Components
{
    public class ExitLevel : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        public void Exit()
        {

            var session = FindObjectOfType<GameSession>();
            session.Save();
            SceneManager.LoadScene(_sceneName);
        }
    }
}

