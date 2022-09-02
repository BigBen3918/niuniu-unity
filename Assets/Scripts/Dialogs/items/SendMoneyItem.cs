using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendMoneyItem : MonoBehaviour
{
    public Text IDTxtUI;
    public Text txStateTxtUI;
    public Text balanceTxtUI;
    public Text dateTxtUI;
    public Color inTxColor;
    public Color outTxColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInfo(string IDtxt, string txStatTxt, string balaceTxt, string dateTxt){
        IDTxtUI.text = IDtxt;
        Color color = txStatTxt == "1" ? inTxColor : outTxColor;
        string statTxt = txStatTxt == "1" ? "转入" : "转出"; 
        txStateTxtUI.text = statTxt;
        txStateTxtUI.color = color;
        balanceTxtUI.text = float.Parse(balaceTxt).ToString("0.00");
        dateTxtUI.text = Helper.UnixTimeStampToDateTime(int.Parse(dateTxt)).ToString("yyyy-MM-dd hh:mm");
    }

    public void Delete(){
        Destroy(gameObject);
    }
}
