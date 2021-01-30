using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class User : MonoBehaviour
{
    //Events
    public delegate void OnUserDataChanged(UserData userData);
    public static event OnUserDataChanged onUserDataChanged;

    // Make this a Singleton 
    private static User _instance;
    public static User I { get { return _instance; } }

    //User Varibales
    UserData userData;
    Dictionary<string, Item> userStuffs = new Dictionary<string, Item>();
    Charecter charecter;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
        SetCharecter();
    }
        // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        userData = UserDataGetter.GetUserData();
        onUserDataChanged?.Invoke(userData);
    }

    void SetCharecter()
    {
        charecter = GameObject.FindGameObjectWithTag("Player").GetComponent<Charecter>();
        if (!charecter)
            Debug.LogError("Cannot find charecter in scene!");
    }

    public Constants.BuyResult Buy(Item item)
    {
        if (item.MinLevel > userData.Level)
            return Constants.BuyResult.LowerLevel;
        if (item.Price > userData.Coins)
            return Constants.BuyResult.InsufficientFunds;
        
        else
        {
            if (!userStuffs.ContainsKey(item.Name))
            {
                userData.Coins -= item.Price;
                userStuffs.Add(item.Name, item);
            }
            ApplyItem(item);
            onUserDataChanged?.Invoke(userData);
            return Constants.BuyResult.Bought;
        }
    }

    void ApplyItem(Item item)
    {
        charecter.ApplyItem(item);
    }

    //demostaretes a "new game"
    public void OnUserSavedState()
    {
        userStuffs.Clear();
        userData.Coins = 0;
        userData.Level = 0;
        userData.Name = "";
        onUserDataChanged?.Invoke(userData);
        charecter.Clear();
    }

    public void OnUserLoadedState(UserData userData, Item[] items)
    {
        SetUserData(userData);
        SetUserStuffs(items);

    }
    public UserData GetUserData() { return userData; }
    public int GetLevel() { return userData.Level; }
    public void SetUserName(string name) { this.userData.Name = name; onUserDataChanged?.Invoke(userData); }
    void SetUserData(UserData _userData) { this.userData = _userData; onUserDataChanged?.Invoke(_userData); }
    void SetUserStuffs(Item[] items)
    {
        userStuffs = new Dictionary<string, Item>();
        for (int i = 0; i < items.Length; i++)
        {
            userStuffs.Add(items[i].Name, items[i]);
            if (items[i].CurrentlyUsed)
                charecter.ApplyItem(items[i]);
        }
           
    }

    public string[] GetUserStuffs() { return userStuffs.Keys.ToArray(); }
}
