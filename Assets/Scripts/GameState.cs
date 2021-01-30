using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFields
{
    public UserData userData;
    public string items;
}

public class GameState
{
    // Start is called before the first frame update
    public void Save(UserData userData, string items)
    {
        PlayerPrefs.SetString("Name", userData.Name);
        PlayerPrefs.SetInt("Coins", userData.Coins);
        PlayerPrefs.SetInt("Level", userData.Level);
        PlayerPrefs.SetString("Items", items);
    }

    public GameFields Load()
    {
        GameFields gameFields = new GameFields();
        if (PlayerPrefs.HasKey("Name"))
        {
            gameFields.userData.Name = PlayerPrefs.GetString("Name");
            gameFields.userData.Coins = PlayerPrefs.GetInt("Coins");
            gameFields.userData.Level = PlayerPrefs.GetInt("Level");
        }
        else gameFields.userData = UserDataGetter.GetUserData();
        gameFields.items = PlayerPrefs.GetString("Items", "");
        PlayerPrefs.DeleteAll();
        return gameFields;

    }
}
