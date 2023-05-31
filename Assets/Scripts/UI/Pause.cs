using UnityEngine;

namespace Assets.Scripts.UI
{
    public class Pause : MonoBehaviour
    {
        [SerializeField]
        private GameObject _panel;

       public void OnPause()
        {
            Time.timeScale = 0.0f;
            _panel.SetActive(true);
        }

        public void OnPLay()
        {
            Time.timeScale = 1.0f;
            _panel.SetActive(false);
        }
    
    }
}