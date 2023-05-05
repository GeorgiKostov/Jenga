using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Behaviour
{
    public class RestartButton:MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene(0);
        }
    }
}