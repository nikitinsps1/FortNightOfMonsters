using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject _panel;

    public void Pause()
    {
        Time.timeScale = 0.0f;
        _panel.SetActive(true);
    }

    public void Play()
    {
        Time.timeScale = 1.0f;
        _panel.SetActive(false);
    }
}
