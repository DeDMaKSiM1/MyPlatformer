using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyPlatform.Components
{
    public class ExitLevel : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        public void Exit()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}

