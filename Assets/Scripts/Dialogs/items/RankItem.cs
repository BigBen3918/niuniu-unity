using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shapes2D;


public class RankItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Text aliasUI;
    public Text IDUI;
    public Shape avatarUI;
    public Text expUI;
    public Image rangNumUI;
    public List<Sprite> numImages = new List<Sprite>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInfo(int num, string id, string alias, string avatar, string exp){
        rangNumUI.sprite = numImages[num];
        IDUI.text = id;
        aliasUI.text = alias;
        avatarUI.settings.fillTexture = Helper.ParseTexture(avatar);
        expUI.text = exp;
    }

    public void Delete(){
        Destroy(gameObject);
    }
}
