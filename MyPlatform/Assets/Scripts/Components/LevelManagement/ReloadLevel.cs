using MyPlatform.Model;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace MyPlatform.Components.LevelManagement
{
    public class ReloadLevel : MonoBehaviour
    {
        public void Reload()
        {
            var session = FindObjectOfType<GameSession>();
            session.LoadLastSave();
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

}
