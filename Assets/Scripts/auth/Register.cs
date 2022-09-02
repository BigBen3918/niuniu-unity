// by Leo Pawel <https://github.com/galaxy126>
// at 10/07/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject registerPanel;

    // Start is called before the first frame update
    public static Register instance;
    // game ui 
    private string alias = "";
    private string email = "";
    private string code = "";
    private string password = "";
    private string confirm = "";

    private float timeOfEmail;
    private bool isWait = true;
    // ui collection

    void Start() {
        instance = this;
    }
    void Update() {
        updateSendButtonText();
    }
    void OnEnable() {
        GameManager.instance.currentPage = CURRENT_PAGE.Loggin;
    }
    public void setAlias(string alias) {
        this.alias = alias;
    }

    public void setEmail(string email) {
        this.email = email;
    }
    public void setCode(string code) {
        this.code = code;
    }

    public void setPassword(string password) {
        this.password = password;
    }

    public void setConfirm(string confirm) {
        this.confirm = confirm;
    }

    public void onSendCode() {
        // UpdateSendCode();
        if (email.Equals("")) {
            UI.instance.ErrorMessage("请输入邮箱地址");
        } else if (!Helper.validateEmail(email)) {
            print(Helper.getError(20001));
            UI.instance.ErrorMessage(NotificationWords.instance.errorList[1]);
            return;
        } else {
            NetworkManager.instance.Send("send-code", new string[]{email});
            UI.instance.ShowLoading(true);
        }
        
    }
    
    private void updateSendButtonText() {
        if (!isWait) {
            var sendObject = GameObject.Find("CodeGroup/send");
            if (sendObject!=null) {
                
                var sendInput = sendObject.GetComponentInChildren<Text>();
                // System.DateTime time = new System.DateTime();
                var time = Time.time - timeOfEmail;
                var remain = 60 - (int)time;
                if (remain<0) {
                    sendObject.GetComponent<Button>().interactable = true;
                    // var sendImage = sendObject.GetComponent<Image>();
                    // sendImage.color = new Color32(255, 255, 255, 255);
                    sendInput.text = "发送验证码";
                    isWait = true;
                } else {
                    sendInput.text = remain + "秒";
                }
            }
        }
        
    }

    public void UpdateSendCode() {
        print("updateSendCode");
        var codeObject = GameObject.Find("CodeGroup/code");
        if (codeObject!=null) {
            var codeImage = codeObject.GetComponent<Image>();
            codeImage.color = new Color32(217, 213, 213, 255);
            var codeInput = codeObject.GetComponent<InputField>();
            codeInput.readOnly = false;
        }
        var sendObject = GameObject.Find("CodeGroup/send");
        if (sendObject!=null) {
            sendObject.GetComponent<Button>().interactable = false;
            // var sendImage = sendObject.GetComponent<Image>();
            // sendImage.color = new Color32(217, 213, 213, 100); // new Color(217, 213, 213, 100);
            var sendInput = sendObject.GetComponentInChildren<Text>();
            // var sendInput = sendObject.GetComponent<InputField>();
            sendInput.text = "60秒";
            timeOfEmail = Time.time;
            isWait = false;
        }
    }

    public void UpdateSuccess() {
        registerPanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    public void onSubmit() {
        if ("".Equals(alias)) {
            // print(Helper.getError(20001));
            UI.instance.ErrorMessage("请输入昵称。");
            return;
        }
        if (!Helper.validateEmail(email)) {
            print(Helper.getError(20001));
            UI.instance.ErrorMessage(NotificationWords.instance.errorList[20000]);
            return;
        }
        if (code.Length!=6) {
            print(Helper.getError(20005));
            UI.instance.ErrorMessage("验证码是6个数字串。");
            return;
        }
        if (password.Length<6 || password.Length>32) {
            print(Helper.getError(20005));
            UI.instance.ErrorMessage(NotificationWords.instance.errorList[7]);
            return;
        }
        if (!password.Equals(confirm)) {
            print(Helper.getError(20032));
            UI.instance.ErrorMessage("密码不匹配");
            return;
        }
        print(alias + "," + email + "," + password);
        NetworkManager.instance.Send("register", new string[]{alias, email, code, password});
        UI.instance.ShowLoading(true);
    }
}
