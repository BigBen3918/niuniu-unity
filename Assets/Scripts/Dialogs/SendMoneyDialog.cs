using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendMoneyDialog : Dialog
{
    #region Sending panel
        public static SendMoneyDialog instance;
        public Text balanceTxtUI;
        public InputField sendIDInputUI;
        public InputField sendAmountInputUI;
        public Slider sliderUI;
        public Text sliderHandleTxtUI; 
        private float balance;
        private float sendAmount;
        private bool isActiveInput;
    #endregion

    #region history panel
        public Transform contentTransform;
        public List<SendMoneyItem> items = new List<SendMoneyItem>();

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        balance = 0;
        sendAmount = 0;
        instance = this;
    }

    public void SetInfo(string _balance, float amountPercent){
        balanceTxtUI.text = _balance;
        SetSliderValue(amountPercent);
        balance = float.Parse(_balance);
        balance = (float)System.Math.Round(balance * 100f) * 0.01f;
        isActiveInput = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Show()
    {
        base.Show();
        SetInfo(GameManager.instance.userInfo.balance, 0f);

        sendAmountInputUI.text = "0";
        sendIDInputUI.text = "0";
    }

    public void SetSliderValue(float value){
        try {
            if(value > 1f) value = 1f;
            
            sliderUI.value = value;
            sliderHandleTxtUI.text = (int)(value * 100) + "%";
            sendAmount = (float)Mathf.RoundToInt(value*balance*100f)*0.01f;
            if(!isActiveInput)
                sendAmountInputUI.text = sendAmount.ToString();    
        } catch (System.Exception) {
            
        }
    }

    public void OnMaxAmount(){
        SetSliderValue(1f);
    }

    public void OnSetAmount(string amount){
        
        if(amount == "") amount = "0";
        isActiveInput = true;
        if(float.Parse(amount) > balance){
            sendAmountInputUI.text = balance.ToString();
        }
        sendAmount = (float)(int)(float.Parse(amount)*100f)*0.01f;
        SetSliderValue(float.Parse(amount)/balance);
        isActiveInput = false;
    }

    public void OnSend(){
        try {
            var uid = int.Parse(sendIDInputUI.text);
            if (uid==0) {
                UI.instance.ErrorMessage(20403);	
                return;
            }
            var amount = float.Parse(sendAmount.ToString());
            var balance = float.Parse(GameManager.instance.userInfo.balance);
            if (amount<=0) {
                UI.instance.ErrorMessage(20402);	
                return;
            }
            if (amount>balance) {
                UI.instance.ErrorMessage(20401);	
                return;
            }
            NetworkManager.instance.Send("send-coin", new string[]{sendIDInputUI.text, sendAmount.ToString()});
        } catch (System.Exception) {
        }
    }
    public void OnDeleteAmount(){
        SetInfo(balance.ToString(), 0f);
    }
    #region  history Panel

    IEnumerator Reset(){
        int count = items.Count;
        for(int i = 0; i < count; i++)
        {
            items[i].Delete();
            //items.RemoveAt(i);
        }
        items.Clear();
        yield return new WaitForSeconds(0.1f);
    }

    public void SetHistoryData(List<string> data){
        int count = int.Parse(base.popString(data));
        for(int i = 0; i < count; i ++){
            AddItem(base.popString(data), base.popString(data), base.popString(data), base.popString(data));
        }
    }
    void AddItem(string IDTxt, string txStatTxt, string balaceTxt, string timeTxt){
        GameObject sendMoneyItem = GameObject.Instantiate(Resources.Load("items/sendMoneyItem"), contentTransform) as GameObject;
        items.Add(sendMoneyItem.GetComponent<SendMoneyItem>());
        sendMoneyItem.GetComponent<SendMoneyItem>().SetInfo(IDTxt, txStatTxt, balaceTxt, timeTxt);
        sendMoneyItem.transform.SetParent(contentTransform);
        items.Add(sendMoneyItem.GetComponent<SendMoneyItem>());
    }
    public void OnHistoryBt(){
        StartCoroutine(Reset());
        NetworkManager.instance.Send("get-moneylog", new string[]{""});
    }
    #endregion
}
