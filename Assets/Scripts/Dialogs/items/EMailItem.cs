using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMailItem : MonoBehaviour
{
    public Text IDTxtUI;
    public Text contentTxtUI;
    public Text dateTxtUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInfo(string IDTxt, string contentTxt, string dateTxt){
        IDTxtUI.text = IDTxt;
        contentTxtUI.text = contentTxt;
        dateTxtUI.text = Helper.UnixTimeStampToDateTime(int.Parse(dateTxt)).ToString("yyyy-MM-dd hh:mm");
    }

    public void Delete(){
        print("recive command");
        Destroy(gameObject);
    }
}
