using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(ClickerUIHandler))]
public class ClickerMain : MonoBehaviour
{
    public ClickerEntry CurrentEntry;
    public int CurrentGain = 1000;
    public int CurrentClickerIndex = 0;
    public List<ClickerEntry> ClickerEntries = new List<ClickerEntry>();
    public int[] EntriesIndexes = new int[12];

    private ClickerUIHandler handler;
    private ClickerYGHandler YGHandler;

    // Start is called before the first frame update
    void Awake()
    {
        //YandexGame.ResetSaveProgress();
        EntriesIndexes[0] = 0;
        CurrentEntry = ClickerEntries[0];
        handler = GetComponent<ClickerUIHandler>();
        YGHandler = GetComponent<ClickerYGHandler>();
    }

    private void Start() {
        UpdateUI();
    }

    public void HandleClick() {
        handler.MainSprite.GetComponent<Animator>().SetTrigger("click");
        if(CurrentEntry == null) { 
            CurrentGain += 1;
        } else {
            CurrentGain += CurrentEntry.GainBonus;
        }
        //handler.DisplayPopup(CurrentEntry.GainBonus);
        UpdateUI(); 
    }

    public void BuyUpgrade(ClickerEntry entry) {
        var ind = ClickerEntries.IndexOf(entry);
        if (EntriesIndexes.Contains(ind)) { 
            CurrentEntry = ClickerEntries[ind];
            CurrentClickerIndex = ind;
            Debug.Log("SWITCH TO: " + ClickerEntries[ind]);
        } else if (CurrentGain >= entry.Cost) {
            CurrentGain -= entry.Cost;
            CurrentEntry = entry;
            EntriesIndexes[ind]=ind;
            CurrentClickerIndex = ind;
        }
        UpdateUI();
    }

    public void UpdateUI() {
        handler.UpdateShopButtons(ClickerEntries, EntriesIndexes.ToList(), CurrentEntry);
        handler.UpdateGainText(CurrentGain.ToString());
        handler.UpdateSprite(CurrentEntry);
        YGHandler.MySave(CurrentGain, CurrentClickerIndex, EntriesIndexes.ToList());
    }
}