using UnityEngine;
using YG;
using Zenject;

public class LoadYandex : MonoBehaviour
{
    private SaveData _saveData;
    private SceneLoader _sceneChanger;
    private AudioContainer _audio;

    [Inject]
    private void Construct(SaveData saveData, SceneLoader sceneChanger, AudioContainer audio)
    {
        _saveData = saveData;
        _sceneChanger = sceneChanger;
        _audio = audio;
    }

    private void StartGame()
    {
        _audio.PlayMusic(TypeMusic.Main);

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
