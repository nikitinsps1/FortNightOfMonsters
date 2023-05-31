using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private Canvas _canvas;

    private AsyncOperation _loading;

   private IEnumerator Load(TypeScene scene)
    {
        _loading = SceneManager.LoadSceneAsync(((int)scene));

        while (_loading.isDone == false)
        {
            _slider.value = _loading.progress;
            yield return null;
        }

        yield return new WaitUntil(() => _loading.isDone);
        _canvas.enabled = false;
    }

    public void Change(TypeScene scene)
    {
        if ( _canvas.enabled == false)
        {
            _canvas.enabled = true;
            StartCoroutine(Load(scene));
        }

    }
}