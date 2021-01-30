using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public Text PriceText, LevelText;
    //public Sprite Icon;
    public GameObject Lock;
    Item item;

    public void SetItem(Item item, Sprite icon)
    {
        this.item = item;
        if (item.Price != 0)
            PriceText.text = item.Price.ToString();
        else PriceText.transform.parent.gameObject.SetActive(false);

        if (item.MinLevel > User.I.GetLevel())
            LevelText.text = item.MinLevel.ToString();
        else
        {
            LevelText.transform.parent.gameObject.SetActive(false);
            Lock.SetActive(false);
        }
        GetComponent<Image>().sprite = icon;

    }

    void ItemAccuried()
    {
        item.ItemBought();
        PriceText.transform.parent.gameObject.SetActive(false);
        LevelText.transform.parent.gameObject.SetActive(false);
        Lock.SetActive(false);
    }

    public void OnClick()
    {
        Constants.BuyResult result = User.I.Buy(item);
        switch (result)
        {
            case Constants.BuyResult.Bought:
                if(item.Price > 0)
                    Popup.I.SetPopup("Let's start customize!",
                         item.Price.ToString() + " coins just withdrawn from your balance");
                ItemAccuried();
                break;
            case Constants.BuyResult.InsufficientFunds:
                Popup.I.SetPopup("Oh no! ", "You don't have enough coins for that");
                break;
            case Constants.BuyResult.LowerLevel:
                Popup.I.SetPopup("Oh no! ", "You need to level up to accuire this item!");
                break;
        }
        
    }

    public Item GetItem() { return this.item; }
}
