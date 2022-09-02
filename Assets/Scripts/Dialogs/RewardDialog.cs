using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardDialog : Dialog
{
    public static RewardDialog instance;
    public Text valueUI;
    public float rewards = 0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
    }

    public void Show(){
        GetComponent<Animator>().SetBool("show", true);
        UI.instance.ShowLoading(true);
        NetworkManager.instance.Send("get-rewards", new string[]{});
    }

    public void Hide(){
        GetComponent<Animator>().SetBool("show", false);
    }

    public void SetData(string value) {
        try {
            rewards = float.Parse(value);
            valueUI.text = value;
        } catch (System.Exception) {
        }
    }

    public void OnClimbRewards() {
        if (rewards>0) {
            UI.instance.ShowLoading(true);
            NetworkManager.instance.Send("climb-rewards", new string[]{});
            GetComponent<Animator>().SetBool("show", false);
            valueUI.text = "0";
        }
    }
}
