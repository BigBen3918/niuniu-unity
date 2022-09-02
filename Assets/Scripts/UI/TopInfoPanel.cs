using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopInfoPanel : MonoBehaviour {
    public Text notice;
    public Text name;
    public Text id;
    public Text balance;

    public Text exp;

    public Shapes2D.Shape photo;
    public Texture2D avataTexture;
    public static TopInfoPanel instance;

    public GameObject mailLabel;
    public GameObject rewardLabel;

    public string supportUrl = "";
    // Start is called before the first frame update
    void Start() {
        instance = this;
        mailLabel.SetActive(false);
        rewardLabel.SetActive(false);
        avataTexture = photo.settings.fillTexture;
    }

    // Update is called once per frame
    void Update() {
        
    }
    public void OnDeposit() {
        Application.OpenURL(supportUrl);
    }
    public void UpdateInfo(string _name, string _id, string _balance, string _exp, string _avatar, string mailCount, string rewards, string _notice, string _supportUrl) {
        _balance = (System.Math.Round(float.Parse(_balance) * 100f) * 0.01f).ToString("0.00");
        _exp = (System.Math.Round(float.Parse(_exp) * 100f) * 0.01f).ToString("0.00");
        UserInfo userInfo = new UserInfo(_id, _name, _balance, _exp, _avatar);
        GameManager.instance.userInfo = userInfo;
        if(SendMoneyDialog.instance != null){
            SendMoneyDialog.instance.SetInfo(_balance, 0f);
        }
        name.text = _name;
        id.text = _id;
        balance.text = _balance;
        exp.text = _exp;
        notice.text = _notice;

        if (rewards=="0") {
            rewardLabel.SetActive(false);
        } else {
            rewardLabel.SetActive(true);
            rewardLabel.GetComponentInChildren<Text>().text = Helper.FormatNumber(rewards);
        }
        
        if(_avatar != "0")
            avataTexture = Helper.ParseTexture(_avatar);
            photo.settings.fillTexture = avataTexture;
        if(int.Parse(mailCount) > 0){
            mailLabel.SetActive(true);
            mailLabel.GetComponentInChildren<Text>().text = mailCount;
        }else{
            mailLabel.SetActive(false);
        }
        supportUrl = _supportUrl;
    }
    
    
    public void UpdateBalance(float _balance, float _exp){
        GameManager.instance.userInfo.UpdateBalance(_balance, _exp);
        balance.text = (float.Parse(balance.text) + _balance).ToString();
        exp.text = (float.Parse(exp.text) + _exp).ToString();
    }

    public void OnQuit(){
        Application.Quit();
    }

    public void OnSetting(){
        
    }

    
}
