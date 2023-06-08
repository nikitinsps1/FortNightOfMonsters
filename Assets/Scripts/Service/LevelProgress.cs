using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class LevelProgress : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _magazines;

    [SerializeField]
    private AssaultMediator
    _leftFlank,
    _rightFlank;

    [SerializeField]
    private EndLevel 
        _endLevelPanels;

    [SerializeField]
    private TaskPanel _taskPanel;

    private Dictionary
    <int, AssaultMediator>
    _assaultMediators;

    private AudioContainer _audio;
    private Player _player;
    private SaveData _saveData;
    private LevelSettings _setting;
    private MainHouse _mainHouse;

    private int
        _defenseCounter,
        _amountLevels;

    private bool _isHaveSideAssault;

    [Inject]
    private void Construct(
        SaveData saveData,
        Player player,
        MainHouse mainHouse,
        AudioContainer audio)
    {
        _saveData = saveData;
        _player = player;
        _mainHouse = mainHouse;
        _audio = audio;
    }

    private void CloseMagazines()
    {
        for (int i = 0; i < _magazines.Length; i++)
        {
            _magazines[i].SetActive(false);
        }
    }

    private void OpenMagazines()
    {
        for (int i = 0; i < _magazines.Length; i++)
        {
            _magazines[i].SetActive(true);
        }
    }

    private void OnDisable()
    {
        _player.ThisDamageable
            .OnDead -= Fail;

        _mainHouse.ThisDamageable
            .OnDead -= Fail;
    }

    public void Init(LevelSettings levelSettings, int amountLevels)
    {
        _setting = levelSettings;
        _amountLevels = amountLevels;

        _player.ThisDamageable
            .OnDead += Fail;

        _mainHouse.ThisDamageable
            .OnDead += Fail;

        _defenseCounter = 0;

        _assaultMediators = new Dictionary<int, AssaultMediator>
        {
            {(int) Directions.LeftFlank, _leftFlank },
            {(int) Directions.RightFlank, _rightFlank }
        };
    }

    public void Victory()
    {
        TypeEndLevel typeVictory;

        if (_saveData.NumberLevel != _amountLevels - 1)
        {
            typeVictory = TypeEndLevel.Victory;
            _saveData.LevelComplete(_setting.Reward);

            _endLevelPanels.OnEnd
                (typeVictory, $"Забрать {_setting.Reward} монет ");
        }
        else
        {
            typeVictory = TypeEndLevel.GameComplete;
            _saveData.LevelComplete(_setting.Reward);
            _endLevelPanels.OnEnd(typeVictory, "Победа!");
        }
    }

    public void Fail()
    {
        _endLevelPanels.OnEnd
             (TypeEndLevel.Fail, "Заново");
    }

   public void ReadyInvasion()
    {
        _taskPanel.OnReadyForInvasions();
    }

    public void StartInvasions()
    {
        CloseMagazines();

        foreach (var setting in _setting.InvasionsSettings)
        {
            _assaultMediators[setting.Key]
                .StartInvasion( setting.Value);
        }
    }

    public void SetNewTask(string taskText)
    {
        _taskPanel.OnSetNewTask(taskText);
    }

    public void SetNewTask
        (string taskText, Directions direction, WaveEnemies[] waveMonsters)
    {
        SetNewTask(taskText);
        _isHaveSideAssault = true;

        _assaultMediators[(int)direction]
            .StartInvasion(waveMonsters);

        CloseMagazines();
    }

    public void InvasionComplete()
    {
        if (_isHaveSideAssault)
        {
            _isHaveSideAssault = false;
            OpenMagazines();
            _taskPanel.OnReadyForInvasions();
        }
        else
        {
            _defenseCounter++;

            if (_defenseCounter == _assaultMediators.Count)
            {
                Victory();
            }
        }
    }
}