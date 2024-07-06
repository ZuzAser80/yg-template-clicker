using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickerUIHandler : MonoBehaviour
{
    public Image MainSprite;
    public GameObject ShopPanel;
    public TextMeshProUGUI GainText;
    public List<Button> ShopButtons = new List<Button>();

    public void UpdateGainText(string newText) {
        GainText.text = newText;
    }

    public void UpdateShopButtons(List<ClickerEntry> entries, List<int> toExclude, ClickerEntry currentEntry) {
        foreach(var e in entries) {
            if (e == currentEntry) {
                ShopButtons[entries.IndexOf(currentEntry)].GetComponent<Image>().color = Color.cyan;
                ShopButtons[entries.IndexOf(currentEntry)].GetComponentInChildren<TextMeshProUGUI>().text = "ВЫБРАНО";
            } else {
                if(toExclude.Contains(entries.IndexOf(e))) {
                    ShopButtons[entries.IndexOf(e)].GetComponent<Image>().color = Color.gray;
                    ShopButtons[entries.IndexOf(e)].GetComponentInChildren<TextMeshProUGUI>().text = "КУПЛЕНО";
                } else {
                    ShopButtons[entries.IndexOf(e)].GetComponent<Image>().color = Color.green;
                    ShopButtons[entries.IndexOf(e)].GetComponentInChildren<TextMeshProUGUI>().text = "КУПИТЬ";
                }
            }
        }
    }

    public void FlipShop() {
        ShopPanel.SetActive(!ShopPanel.activeSelf);
    }

    public void UpdateSprite(ClickerEntry entry) {
        if(entry == null) { return;}
        MainSprite.sprite = entry.sprite;
    }
    
}
