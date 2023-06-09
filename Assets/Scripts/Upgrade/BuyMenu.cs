using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

public class BuyMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMesh;

    [SerializeField]
    private GameObject _panelMagazine;

    [SerializeField]
    private Button _rewardAdsButton;

    private SaveData _data;
    private Action _product;
    private InfoPanel _infoPanel;

    private int _costGameMoney;

    [Inject]
    private void Construct(InfoPanel infoPanel, SaveData saveData)
    {
        _data = saveData;
        _infoPanel = infoPanel;
    }

    private void Complete(int reward = 0)
    {
        _infoPanel.ShowMessage("Улучшено!");
        _product.Invoke();
    }

    public void Trade(int newCost, Action product)
    {
        _panelMagazine.SetActive(true);
        _costGameMoney = newCost;
        _product = product;
        _textMesh.text = _costGameMoney.ToString();
    }

    public void BuyGameMoney()
    {
        if (_data.Money >= _costGameMoney)
        {
            Complete();
            _data.RefreshAmountMoney(-_costGameMoney);
        }
        else
        {
            _infoPanel.ShowMessage("Не хватает монет");
        }

        _panelMagazine.SetActive(false);
    }

    private void OnEnable()
    {
        _rewardAdsButton
            .onClick
            .AddListener(() => YandexGame.RewVideoShow(0));

        _rewardAdsButton
            .onClick
            .AddListener(() => _panelMagazine.SetActive(false));

        YandexGame.RewardVideoEvent += Complete;
    }

    private void OnDisable()
    {
        _rewardAdsButton.onClick.RemoveAllListeners();
        YandexGame.RewardVideoEvent -= Complete;
    }
}