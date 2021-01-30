using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsHolderBehaviour : MonoBehaviour
{
    public GameObject ItemPrefab;
    ItemsData itemsData =new ItemsData();
    List<GameObject> currentItems = new List<GameObject>();
    

    //gap between each item on board
    public float prGapY = 57;
    //Anchors
    public Vector2[] anchors = new Vector2[3];
    // Start is called before the first frame update
    void Start()
    {
        OnSectionButtonClicked(0);
    }

 
    void InstantiateItems(Item[] items)
    {
        ClearItems();
        for(int i=0; i<items.Length; i++)
        {
            int anchorIndex = (i < 4) ? 0 : (i < 8) ? 1 : 2;
            GameObject item = Instantiate(ItemPrefab);
            item.transform.SetParent(gameObject.transform);
            RectTransform rt = item.GetComponent<RectTransform>();
            rt.localScale = new Vector3(1, 1, 0);
            rt.anchoredPosition = new Vector2(anchors[anchorIndex].x, anchors[anchorIndex].y - (i%(anchors.Length+1) * prGapY));
            rt.localPosition = new Vector3(rt.localPosition.x, rt.localPosition.y, 0);
            item.GetComponent<ItemController>().SetItem(items[i], IconLoader(items[i]));
            currentItems.Add(item);
        }
    }

    public void OnSectionButtonClicked(int num)
    {
        Section section = (num == (int)Section.EYE) ? Section.EYE : (num == (int)Section.MOUTH) ? Section.MOUTH : Section.OUTFIT;
        var itemsToDisplay = itemsData.GetItemsByType(section) as Item[];
        InstantiateItems(itemsToDisplay);
    }

    void ClearItems()
    {
        currentItems.ForEach(element =>
        {
            Destroy(element);
        });
        currentItems.Clear();
    }

    public Sprite IconLoader(Item item)
    {
         switch (item.Section) { 
            case Section.EYE:
                return Resourcer.SpriteLoader(Constants.EYEICONPATH + item.Name + Constants.ICONSUFFIX);
            case Section.MOUTH:
                return Resourcer.SpriteLoader(Constants.MOUTHICONPATH + item.Name + Constants.ICONSUFFIX);
            default:
                return Resourcer.SpriteLoader(Constants.OUTFITICONPATH + item.Name + Constants.ICONSUFFIX);

        }
    }
}
