using UnityEngine;
using YG;
using Zenject;

public class LoadApplication : MonoBehaviour
{
    private SaveData _saveData;
    private SceneChanger _sceneChanger;
    private AudioContainer _audioEffects;

    [Inject]
    private void Construct(SaveData saveData, SceneChanger sceneChanger, AudioContainer audio)
    {
        _saveData = saveData;
        _sceneChanger = sceneChanger;
        _audioEffects = audio;
    }

    private void StartGame()
    {
        _audioEffects.PlayMusic(TypeMusic.Main);

        if (YandexGame.savesData.Level == 0)
        {
            _saveData.NewGame();
        }
        else
        {
            _saveData.ConvertYandexData();
        }

        _sceneChanger.Change(TypeScene.MainMenu);
    }


    private void OnEnable()
    {
        YandexGame.GetDataEvent += StartGame;
    }


    private void OnDisable()
    {
        YandexGame.GetDataEvent -= StartGame;
    }
}
