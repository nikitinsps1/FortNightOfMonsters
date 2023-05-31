using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject[]
        _waypoints,
        _mannequinsMelee,
        _mannequinsGun;

    [SerializeField]
    private GameObject
        _forceDoor,
        _pistolButton,
        _attackJoystick,
        _dialogPosition;

   [SerializeField] private ChangeWeaponButton _weaponButton;
    private InfoPanel _infoPanel;
    private SceneChanger _sceneChanger;
    private AudioContainer _audioEffects;

    private int _taskCounter;
    private int _stageCounter;

    private string _deviceType;
    private Player _player;

    [Inject]
    private void Construct(InfoPanel infoPanel, SceneChanger sceneChanger, AudioContainer audioEffects, Player player)
    {
        _infoPanel = infoPanel;
        _sceneChanger = sceneChanger;
        _audioEffects = audioEffects;
        _player = player;   
    }

    private void Start()
    {
        _deviceType =
            YandexGame.EnvironmentData.deviceType;

        _audioEffects.PlayMusic(TypeMusic.SciFi);

        NextStage(StagesTutorial.Walking);
    }

    private IEnumerator CompletingTasks(GameObject[] activatingObjects, string taskText)
    {
        _infoPanel.ShowMessage(taskText);

        ActivateObjects(activatingObjects);

        _taskCounter = activatingObjects.Length;

        yield return new WaitUntil(() => _taskCounter < 1);
        _stageCounter++;

        NextStage((StagesTutorial)_stageCounter);
    }

    private void ActivateObjects(GameObject[] objects)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(true);
        }
    }
    private void NextStage(StagesTutorial stagesTutorial)
    {
        switch (stagesTutorial)
        {
            case StagesTutorial.Walking:
                string text1;

                if (_deviceType != "desktop")
                {
                    text1 = TutorialTexts.Walk;
                }
                else
                {
                    text1 = TutorialTexts.WalkDestop;
                }

                StartCoroutine(CompletingTasks(_waypoints, text1));
                break;

            case StagesTutorial.MelleAttack:
                string text2;

                if (_deviceType != "desktop")
                {
                    text2 = TutorialTexts.MelleAttack;
                    _attackJoystick.SetActive(true);
                }
                else
                {
                    text2 = TutorialTexts.MelleAttackDestop;
                }
                StartCoroutine(CompletingTasks(_mannequinsMelee, text2));
                break;
            case StagesTutorial.Upgrade:
                _forceDoor.SetActive(false);
                _infoPanel.ShowMessage(TutorialTexts.Upgrade);
                break;
            case StagesTutorial.RangeAttack:
                StartCoroutine(CompletingTasks(_mannequinsGun, TutorialTexts.RangeAttack));
                _pistolButton.SetActive(true);
                _weaponButton.Init();


                _player.ThisArsenal.Init();

                break;
            case StagesTutorial.Dialog:
                _infoPanel.ShowMessage(TutorialTexts.Dialog);
                _dialogPosition.SetActive(true);
                break;
            case StagesTutorial.End:
                _infoPanel.ShowMessage(TutorialTexts.End);
                _infoPanel
                    .GetComponentInChildren<Button>()
                    .onClick.AddListener(
                    delegate { _sceneChanger.Change(TypeScene.MainMenu); });
                break;
            default:
                break;
        }
    }

    public void ClickCounter()
    {
        _taskCounter--;
    }

    public void BuyPistol()
    {
        NextStage(StagesTutorial.RangeAttack);
        _stageCounter++;
    }

    public void AllComplete()
    {
        NextStage(StagesTutorial.End);
    }
}