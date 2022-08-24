using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaintAstic.Global
{

    public class SceneSwitcher : MonoBehaviour
    {
        public void LoadSceneIndex(int index) {
            SceneManager.LoadScene(index);
        }
    }
}
