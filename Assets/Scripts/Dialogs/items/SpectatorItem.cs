using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shapes2D;


public class SpectatorItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Text aliasUI;
    public Text IDUI;
    public Shape avatarUI;
    public Text balanceUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInfo(string id, string alias, string avatar, string balance){
        IDUI.text = id;
        aliasUI.text = alias;
        avatarUI.settings.fillTexture = Helper.ParseTexture(avatar);
        balanceUI.text = float.Parse(balance).ToString("0.00");
    }

    public void Delete(){
        Destroy(gameObject);
    }
}
