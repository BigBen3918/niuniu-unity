using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatDialog : Dialog
{

    public static ChatDialog instance;
    
    private List<string> list;
    public InputField inputUI;

    // Start is called before the first frame update
    void Start() {
        instance = this;
    }
    

    public void SetData(string type, string bank, string account, string owner) {
        
    }

    public void OnSend() {
        var msg = inputUI.text.Trim();
        if (msg=="") {
            return;
        }
        UI.instance.ShowLoading(true);
        NetworkManager.instance.Send("send-msg", new string[]{msg});
    }

    public override void Show()
    {
        base.Show();
    }

}
