using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    private static Popup _instance;
    public static Popup I { get { return _instance; } }

    public GameObject Holder;
    public Text Title, Body;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        Holder.SetActive(false);
    }

     public void SetPopup(string title, string body)
    {
        Title.text = title;
        Body.text = body;
        Holder.SetActive(true);
    }
    public void OnDismissClicked() { Holder.SetActive(false); }
}
