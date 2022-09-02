using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WithdrawBankDialog : Dialog
{
    public static WithdrawBankDialog instance;
    public InputField bankUI;
    public InputField accountUI;
    public InputField ownerUI;
    void Start(){
        instance = this;
    }

    public void OnSubmit() {
        var bank = bankUI.text.Trim();
        var account = accountUI.text.Trim();
        var owner = ownerUI.text.Trim();
        if (bank=="") {
            UI.instance.ErrorMessage(20500);	
            return;
        }
        if (account=="") {
            UI.instance.ErrorMessage(20501);	
            return;
        }
        if (owner=="") {
            UI.instance.ErrorMessage(20502);	
            return;
        }
        WithdrawDialog.instance.SetData("bank", bank, account, owner);
        base.Hide();
    }
}
