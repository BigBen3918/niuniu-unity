using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WithdrawDialog : Dialog
{
    public const float feeRate = 0.02f;
    public static WithdrawDialog instance;
    private string type = "bank";
    private string bank = "";
    private string account = "";
    private string owner = "";


    public Text noteLabel;
    public Text accountLabel;
    public InputField accountValueUI;
    public Slider sliderUI;
    public InputField withdrawInputUI;
    public Text sliderTxtUI;
    public Text balanceUI;
    public float withdrawAmount = 0;
    public float balance;
    private bool isActiveInput;
    List<WithDrawItem> items = new List<WithDrawItem>();
    public Transform contentTransform;
    public float itmeHight;



    // Start is called before the first frame update
    void Start() {
        instance = this;

            
    }

    // Update is called once per frame
    void Update() {
        
    }
    public void OnMax() {
        SetSliderValue(sliderUI.maxValue);
    }
    
    public void Reset() {
        SetSliderValue(0);

    }

    IEnumerator ResetLog(){
        int count = items.Count;
        for(int i = 0; i < count; i++)
        {
            items[i].Delete();
            //items.RemoveAt(i);
        }
        items.Clear();
        yield return new WaitForSeconds(0.1f);
    }

    void AddItem(string type, string account, string amount, string state){
        GameObject withDrawItem = GameObject.Instantiate(Resources.Load("items/withdrawItem"), contentTransform) as GameObject;
        items.Add(withDrawItem.GetComponent<WithDrawItem>());
        withDrawItem.GetComponent<WithDrawItem>().SetInfo(type, account, amount, state);
        withDrawItem.transform.SetParent(contentTransform);
    }
    public void SetLogData(List<string> data){
        StartCoroutine(ResetLog());
        int count = int.Parse(base.popString(data));
        contentTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, count * itmeHight);
        for(int i = 0; i < count; i ++){
            AddItem(base.popString(data), base.popString(data), base.popString(data), base.popString(data));
        }
    }
    public void SetSliderValue(float value){
        try {
            // if(value > 1f) value = 0f;
            
            sliderUI.value = value;
            sliderTxtUI.text = Mathf.RoundToInt(value / sliderUI.maxValue * 100f) + "%";
            withdrawAmount = Mathf.RoundToInt(value * 100f);
            withdrawInputUI.text = withdrawAmount.ToString() + " (手续费 " + (withdrawAmount * feeRate) + ")";
            //if(!isActiveInput)
                //sliderUI.text = withdrawAmount.ToString();    
        } catch (System.Exception) {
            
        }
    }

    private void ReadPrefs(string type) {
        if (type=="bank") {
            bank = PlayerPrefs.GetString("bank");
            account = PlayerPrefs.GetString("bankaccount");
            owner = PlayerPrefs.GetString("bankowner");
        } else {
            bank = "";
            account = PlayerPrefs.GetString("alipay");
            owner = PlayerPrefs.GetString("alipayowner");
        }
        accountValueUI.text = account;
    }
    public void OnLink() {
        if (type=="bank") {
            WithdrawBankDialog.instance.Show();
            WithdrawBankDialog.instance.bankUI.text = bank;
            WithdrawBankDialog.instance.accountUI.text = account;
            WithdrawBankDialog.instance.ownerUI.text = owner;
        } else {
            WithdrawAlipayDialog.instance.Show();
            WithdrawAlipayDialog.instance.accountUI.text = account;
            WithdrawAlipayDialog.instance.ownerUI.text = owner;
        }
    }
    public void OnBank() {
        type = "bank";
        ReadPrefs(type);
        accountLabel.text = "银行账户:";
        noteLabel.text = "提现3~5分钟到账，提现前请确认绑定的银行卡信息精准无误";
    }

    public void OnAlipay() {
        type = "alipay";
        ReadPrefs(type);
        accountLabel.text = "支付宝:";
        noteLabel.text = "提现3~5分钟到账，提现前请确认绑定的支付宝信息精准无误";
    }
    public void OnLog(){
        UI.instance.ShowLoading(true);
        NetworkManager.instance.Send("get-withdraw", new string[]{});
    }

    public void SetData(string type, string bank, string account, string owner) {
        this.type = type;
        this.bank = bank;
        this.account = account;
        this.owner = owner;

        if (type=="bank") {
            PlayerPrefs.SetString("bank", bank);
            PlayerPrefs.SetString("bankaccount", account);
            PlayerPrefs.SetString("bankowner", owner);
        } else {
            PlayerPrefs.SetString("alipay", account);
            PlayerPrefs.SetString("alipayowner", owner);
        }
        accountValueUI.text = account;
    }
    public void OnSubmit() {
        if (account=="") {
            UI.instance.ErrorMessage(type=="bank" ? 20520 : 20521);	
            return;
        }
        if (withdrawAmount==0) {
            UI.instance.ErrorMessage(20522);	
            return;
        }
        var fee = withdrawAmount * feeRate;
        if (balance < withdrawAmount + fee) {
            UI.instance.ErrorMessage(20401);
            return;
        }
        UI.instance.ShowLoading(true);
        NetworkManager.instance.Send("set-withdraw", new string[]{type, bank, account, owner, withdrawAmount.ToString()});

    }

    public override void Show()
    {
        base.Show();
        balance = float.Parse(GameManager.instance.userInfo.balance);
        balanceUI.text = balance.ToString();

        sliderUI.maxValue = (int)Mathf.Floor(balance/(100 * (1 + feeRate)));
        SetSliderValue(0);
        ReadPrefs(type);
        /* if (account=="") {
            NetworkManager.instance.Send("get-payment", new string[]{""});
        } */
        // sendAmountInputUI.text = "0";
        // sendIDInputUI.text = "0";
    }

}
