using MyPlatform.Model;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace MyPlatform.Components
{
    public class ReloadLevel : MonoBehaviour
    {
        public void Reload()
        {
            var session = FindObjectOfType<GameSession>();
            Destroy(session);

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

}
