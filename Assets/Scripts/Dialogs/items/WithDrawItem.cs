using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shapes2D;


public class WithDrawItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D bankImage;
    public Texture2D alipayImage;
    public RawImage typeUI;
    public Text accountUI;
    public Text amountUI;
    public Text stateUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInfo(string type, string account, string amount, string state){
        Color color = state=="0" ? Color.gray : new Color(0.05f, 0.55f, 0);
        typeUI.texture = type == "bank" ? bankImage : alipayImage;
        accountUI.text = account;
        amountUI.text = amount;
        stateUI.text = state=="0" ? "待机" : Helper.UnixTimeStampToDateTime(int.Parse(state)).ToString();
    }

    public void Delete(){
        Destroy(gameObject);
    }
}
