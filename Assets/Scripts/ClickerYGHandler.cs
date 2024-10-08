using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

[RequireComponent(typeof(ClickerMain))]
public class ClickerYGHandler : MonoBehaviour
{

    private ClickerMain main;

    // Подписываемся на событие GetDataEvent в OnEnable
    //private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() { 
        YandexGame.GetDataEvent -= GetLoad;
        YandexGame.RewardVideoEvent -= Rewarded;
    }
    private void OnEnable() {
        YandexGame.GetDataEvent += GetLoad;
        YandexGame.RewardVideoEvent += Rewarded;
    }

    // Подписанный метод получения награды
    void Rewarded(int id)
    {
        if (id == 0) { 
            Debug.Log("AdShown");
            main.CurrentGain += 1000;
        }
    }

    private void Awake() {
        main = GetComponent<ClickerMain>();      
    }

    private void Start()
    {
        // Проверяем запустился ли плагин
        if (YandexGame.SDKEnabled == true) {
            GetLoad();
        }
    }

    // Ваш метод для загрузки, который будет запускаться в старте
    public void GetLoad()
    {
        // Получаем данные из плагина и делаем с ними что хотим
            // Например, мы хотил записать в компонент UI.Text сколько у игрока монет:
        main.CurrentClickerIndex = YandexGame.savesData.CurrentIndex;
        main.CurrentEntry = main.ClickerEntries[YandexGame.savesData.CurrentIndex];
        main.CurrentGain = YandexGame.savesData.CurrentGain;
        main.EntriesIndexes = YandexGame.savesData.BoughtIndexes;
        //Debug.Log("::: " + YandexGame.savesData.BoughtIndexes[0]);
        main.UpdateUI();
    }

    // Допустим, это Ваш метод для сохранения
    public void MySave(int CurrentGain, int CurrentClickerIndex, List<int> toExclude)
    {
        // Записываем данные в плагин
            // Например, мы хотил сохранить количество монет игрока:
        YandexGame.savesData.CurrentIndex = CurrentClickerIndex;
        YandexGame.savesData.CurrentGain = CurrentGain;
        YandexGame.savesData.BoughtIndexes = toExclude.ToArray();
        YandexGame.NewLeaderboardScores("LeaderBoard", CurrentGain);
            // Теперь остаётся сохранить данные
        YandexGame.SaveProgress();
    }

    public void ShowAd() {


        YandexGame.RewVideoShow(0);
    }
}