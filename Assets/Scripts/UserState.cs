using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserState : MonoBehaviour
{
    public Text NAME, COINS, LEVEL;

    // Start is called before the first frame update
    void Awake()
    {
        User.onUserDataChanged+=UpdateUserState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUserState(string name,int coins, int level)
    {
        NAME.text = name;
        COINS.text = coins.ToString();
        LEVEL.text = level.ToString();
    }

    void UpdateUserState(UserData user)
    {
        SetUserState(user.Name, user.Coins, user.Level);
    }
    private void OnDestroy()
    {
        User.onUserDataChanged -= UpdateUserState;
    }
}
