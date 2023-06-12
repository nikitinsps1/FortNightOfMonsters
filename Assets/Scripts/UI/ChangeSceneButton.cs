using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ChangeSceneButton : MonoBehaviour
{
    [SerializeField]
    private TypeScene _scene;

    [SerializeField]
    private Button _button;

    private SceneLoader _changer;
    private SaveData _saveData;

    [Inject]
    private void Construct(SceneLoader sceneChanger, SaveData saveData)
    {
        _changer = sceneChanger;
        _saveData = saveData;
        InitButton();
    }

    private void InitButton()
    {
        _button.onClick.AddListener(() => InitScene());
        _button.onClick.AddListener(() => _changer.Change(_scene));
    }

    private void InitScene()
    {
        if (_scene == TypeScene.Fort && _saveData.NumberLevel == 0)
        {
            _scene = TypeScene.VideoIntro;
        }
    }

}