using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Section
{
    EYE,
    MOUTH,
    OUTFIT,
    ALL
}

public class Item
{
    public string Name;
    public Section Section;
    public int Price;
    public int MinLevel;
	public bool CurrentlyUsed;

    public Item(string name, Section section, int price, int minLevel,bool currentlyUsed)
    {
        this.Name = name;
        this.Section = section;
        this.Price = price;
        this.MinLevel = minLevel;
		this.CurrentlyUsed = currentlyUsed;
    }



    public void ItemBought()
    {
        this.Price = 0;
		SetCurrentyUsed(true);

    }

    public void ItemUnselected()
	{
		SetCurrentyUsed(false);
	}

    void SetCurrentyUsed(bool used) { this.CurrentlyUsed = used; }
}

/// <summary>
/// Dummy data class
/// </summary>
public class ItemsData
{
    private static ItemsData _instance;
    public static ItemsData I { get { return _instance; } }

    public static List<Item> items;
    public ItemsData()
    {
        items = new List<Item>();
        items.Add(new Item("eyes_1", Section.EYE, 400,8,false));
        items.Add(new Item("eyes_2", Section.EYE, 0, 6,false));
        items.Add(new Item("eyes_3", Section.EYE, 700, 0,false));
        items.Add(new Item("eyes_4", Section.EYE, 0, 0,false));
        items.Add(new Item("eyes_5", Section.EYE, 0, 0,false));
        items.Add(new Item("eyes_6", Section.EYE, 0, 5,false));
        items.Add(new Item("eyes_7", Section.EYE, 0, 4,false));
        items.Add(new Item("eyes_8", Section.EYE, 0, 2,false));
        items.Add(new Item("eyes_9", Section.EYE, 620, 0,false));
        items.Add(new Item("eyes_10", Section.EYE, 70, 0,false));
        items.Add(new Item("mouth_01", Section.MOUTH, 0, 0,false));
        items.Add(new Item("mouth_02", Section.MOUTH, 0, 0,false));
        items.Add(new Item("mouth_03", Section.MOUTH, 0, 7,false));
        items.Add(new Item("mouth_04", Section.MOUTH, 0, 3,false));
        items.Add(new Item("mouth_05", Section.MOUTH, 0, 4,false));
        items.Add(new Item("mouth_06", Section.MOUTH, 700, 0,false));
        items.Add(new Item("mouth_07", Section.MOUTH, 260, 0,false));
        items.Add(new Item("mouth_08", Section.MOUTH, 1, 0,false));
        items.Add(new Item("mouth_09", Section.MOUTH, 0, 5,false));
        items.Add(new Item("mouth_10", Section.MOUTH, 0, 0,false));
        items.Add(new Item("mouth_11", Section.MOUTH, 555, 0,false));
        items.Add(new Item("outfit_01", Section.OUTFIT, 0, 5,false));
        items.Add(new Item("outfit_02", Section.OUTFIT, 0, 5,false));
        items.Add(new Item("outfit_03", Section.OUTFIT, 0, 6,false));
        items.Add(new Item("outfit_04", Section.OUTFIT, 0, 3,false));
        items.Add(new Item("outfit_05", Section.OUTFIT, 360, 0,false));
        items.Add(new Item("outfit_06", Section.OUTFIT, 0, 0,false));
        items.Add(new Item("outfit_07", Section.OUTFIT, 100, 0,false));
        items.Add(new Item("outfit_08", Section.OUTFIT, 0, 0,false));
        items.Add(new Item("outfit_09", Section.OUTFIT, 0, 0,false));
        items.Add(new Item("outfit_10", Section.OUTFIT, 1000, 0,false));
    }
    
    public Item[] GetItemsByType(Section section) {

        return items.FindAll(item => item.Section ==section).ToArray();
    }

    public Item[] GetItemsByNames(string[] names)
    {
        return items.FindAll(item => names.Any(item.Name.Contains)).ToArray();
    }
}
