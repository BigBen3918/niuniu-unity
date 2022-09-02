using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shapes2D;


public class PoolViwerUserItem : MonoBehaviour
{
    public Shape avataUI;
    public Text expUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Delete(){
        Destroy(gameObject);
    }
    public void SetData(string avatar, string exp){
        avataUI.settings.fillTexture = Helper.ParseTexture(avatar);
        expUI.text = exp;
    }
}
