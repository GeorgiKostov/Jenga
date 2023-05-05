using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public class QuitButton:MonoBehaviour
    {
        public void OnClick()
        {
            Application.Quit();
        }
    }
}