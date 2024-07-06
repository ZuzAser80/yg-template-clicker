using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ClickerUIHandler))]
public class ClickerMain : MonoBehaviour
{
    public ClickerEntry CurrentEntry;
    public int CurrentGain = 1000;
    public int CurrentClickerIndex = 0;
    public List<ClickerEntry> ClickerEntries = new List<ClickerEntry>();
    public int[] EntriesIndexes = new int[12];

    private ClickerUIHandler handler;

    // Start is called before the first frame update
    void Awake()
    {
        handler = GetComponent<ClickerUIHandler>();
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
            Debug.Log("SWITCH TO: " + ClickerEntries[ind]);
        } else if (CurrentGain >= entry.Cost) {
            CurrentGain -= entry.Cost;
            CurrentEntry = entry;
            EntriesIndexes[ind]=ind;
        }
        UpdateUI();
    }

    public void UpdateUI() {
        handler.UpdateShopButtons(ClickerEntries, EntriesIndexes.ToList(), CurrentEntry);
        handler.UpdateGainText(CurrentGain.ToString());
        handler.UpdateSprite(CurrentEntry);
    }
}