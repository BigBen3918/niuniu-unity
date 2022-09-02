using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WithdrawAlipayDialog : Dialog
{
    public static WithdrawAlipayDialog instance;

    public InputField accountUI;
    public InputField ownerUI;

    void Start(){
        instance = this;
    }
    public void OnSubmit() {
        var account = accountUI.text.Trim();
        var owner = ownerUI.text.Trim();
        if (account=="") {
            UI.instance.ErrorMessage(20511);	
            return;
        }
        if (owner=="") {
            UI.instance.ErrorMessage(20512);	
            return;
        }
        WithdrawDialog.instance.SetData("alipay", "", account, owner);
        base.Hide();
    }
}
