using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeScreen : MonoBehaviour
{
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        UI.SetActive(false);
    }


    public void OnInputChanged(string name)
    {
        UI.SetActive(true);
        User.I.SetUserName(name);
        gameObject.SetActive(false);
        
    }
}
