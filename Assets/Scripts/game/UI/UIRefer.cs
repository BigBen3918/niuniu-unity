using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIRefer : MonoBehaviour
{
    public static UIRefer instance;
    [Serializable]
    public struct NamedImage {
        public string name;
        public Texture2D image;
    }
    public NamedImage[] pictures;

    public Color GetCoinColor;
    public Color LoseCoinColor;
    public Texture2D GetCoinTexture;
    public Texture2D LoseCoinTexture;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Texture2D GetImage(string key){
        foreach(var item in pictures){
            if(item.name == key)
                return item.image;
        }
        return null;
    }
}
