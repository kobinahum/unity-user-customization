using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Resourcer
{
  
    public static Sprite SpriteLoader(string path)
    {
        AssetDatabase.Refresh();
        var sprite = Resources.Load <Sprite>(path);
        return sprite;
    }
}
