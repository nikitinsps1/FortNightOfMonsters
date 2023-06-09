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

    private Dictionary<int, AssaultMediator> _assaultMediators;

    private PlayerHeroLogic _player;
    private SaveData _saveData;
    private LevelSettings _setting;
    private MainHouse _mainHouse;

    private int
        _defenseCounter,
        _amountLevels;

    private bool _isHaveSideAssault;

    [Inject]
    private void Construct(SaveData saveData, PlayerHeroLogic player, MainHouse mainHouse)
    {
        _saveData = saveData;
        _player = player;
        _mainHouse = mainHouse;
    }

    private void StartInvasions()
    {
        CloseMagazines();

        foreach (var setting in _setting.InvasionsSettings)
        {
            _assaultMediators[setting.Key]
                .StartInvasion(setting.Value);
        }
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
        _player.ThisDamageable.OnDead -= Fail;
        _mainHouse.ThisDamageable.OnDead -= Fail;
    }

    public void Init(LevelSettings levelSettings, int amountLevels)
    {
        _setting = levelSettings;
        _amountLevels = amountLevels;

        _player.ThisDamageable.OnDead += Fail;
        _mainHouse.ThisDamageable.OnDead += Fail;

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
        _endLevelPanels.OnEnd(TypeEndLevel.Fail, "Заново");
    }

    public void ReadyInvasion()
    {
        _taskPanel.SetNewTask("Нажмите, чтобы начать вторжение!", StartInvasions);
    }

    public void SetNewTask(string taskText)
    {
        _taskPanel.SetNewTask(taskText);
    }

    public void SetNewTask(
        string text,
        Directions direction,
        WaveEnemies[] waveMonsters)
    {
        SetNewTask(text);
        _isHaveSideAssault = true;

        _assaultMediators[(int)direction].StartInvasion(waveMonsters);

        CloseMagazines();
    }

    public void InvasionComplete()
    {
        if (_isHaveSideAssault)
        {
            _isHaveSideAssault = false;

            OpenMagazines();
            ReadyInvasion();
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