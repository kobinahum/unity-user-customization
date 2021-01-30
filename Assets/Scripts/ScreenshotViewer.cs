using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotViewer : MonoBehaviour
{
    public Image image;
    public GameObject Holder;
    string lastImagePath;

    // Start is called before the first frame update
    void Start()
    {
        Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowImage()
    {
        if(lastImagePath == null)
        {
            Debug.LogError("No screenshot taken");
        }
        else
        {
            Sprite sprite = Resourcer.SpriteLoader(Constants.SCREENSHOTPATH + lastImagePath);
            if (sprite == null)
                return;
            Holder.SetActive(true);
            image.sprite = sprite;
        }
    }

    public void OnImageReady(string path)
    {
        this.lastImagePath = path;
    }
     void Close() { Holder.SetActive(false); }
    public void OnClick() { Close(); }
}
