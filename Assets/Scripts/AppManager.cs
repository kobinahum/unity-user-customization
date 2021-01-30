using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AppManager : MonoBehaviour
{

    private static AppManager _instance;
    public static AppManager I { get { return _instance; } }
    GameState gameState = new GameState();
    ItemsData itemsData = new ItemsData();

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void SaveGame()
    {
       var itemsNames = User.I.GetUserStuffs();
       StringBuilder itemsString = new StringBuilder();
       for(int i=0; i<itemsNames.Length; i++)
        {
            itemsString.Append(itemsNames[i]+",");
        }
        string items = itemsString.ToString(); gameState.Save(User.I.GetUserData(), items);
        User.I.OnUserSavedState();
    }
    public void LoadGame()
    {
        GameFields loadedGamesState = gameState.Load();
        string[] names = loadedGamesState.items.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        Item[] items = itemsData.GetItemsByNames(names);
        User.I.OnUserLoadedState(loadedGamesState.userData, items);

    }

    
}