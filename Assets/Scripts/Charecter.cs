using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charecter : MonoBehaviour
{
    public Image Eyes, Mouth, Outfit;
    Item CurrentEyes, CurrentMouth, CurrentOutfit;
    // Start is called before the first frame update
    void Start()
    {
        Clear();
    }

    void SetTransperent(float alpah, Section section)
    {
        var tempColor = Eyes.color;
        tempColor.a = alpah;
        switch (section)
        {
            case Section.EYE:
                Eyes.color = tempColor;
                break;
            case Section.MOUTH:
                Mouth.color = tempColor;
                break;
            case Section.OUTFIT:
                Outfit.color = tempColor;
                break;
            case Section.ALL:
                Eyes.color = tempColor;
                Mouth.color = tempColor;
                Outfit.color = tempColor;
                break;
        }
        
    }


    public void ApplyItem(Item item)
    {
        ApplySprite(LoadSprite(item), item.Section);

        switch (item.Section)
        {
            case Section.EYE:
                if(CurrentEyes != null)
                    CurrentEyes.ItemUnselected();
                CurrentEyes = item;
                break;
            case Section.MOUTH:
                if(CurrentMouth != null)
                    CurrentMouth.ItemUnselected();
                CurrentMouth = item;
                break;
            case Section.OUTFIT:
                if(CurrentOutfit != null)
                    CurrentOutfit.ItemUnselected();
                CurrentOutfit = item;
                break;
        }
        item.ItemBought();
        
    }
    public void Clear()
    {
        SetTransperent(0f, Section.ALL);
    }

    void ApplySprite(Sprite sprite,Section section)
    {
        switch (section)
        {
            case Section.EYE:
                Eyes.sprite = sprite;
                Eyes.SetNativeSize();
                break;
            case Section.MOUTH:
                Mouth.sprite = sprite;
                Mouth.SetNativeSize();
                break;
            default:
                Outfit.sprite = sprite;
                Outfit.SetNativeSize();
                break;
        }
        SetTransperent(1, section);
    }

    Sprite LoadSprite(Item item)
    {
       
            switch (item.Section)
            {
                case Section.EYE:
                    return Resourcer.SpriteLoader(Constants.EYEPATH + item.Name);
                case Section.MOUTH:
                    return Resourcer.SpriteLoader(Constants.MOUTHPATH + item.Name);
                default:
                    return Resourcer.SpriteLoader(Constants.OUTFITPATH + item.Name);

            }
        
        
    }
}
