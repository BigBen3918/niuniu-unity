using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackendDialog : Dialog
{
    public static BackendDialog instance;
    public Text urlUI;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
    }

    public void Show(){
        GetComponent<Animator>().SetBool("show", true);
        UI.instance.ShowLoading(true);
        NetworkManager.instance.Send("get-backendlink", new string[]{});
    }

    public void Hide(){
        GetComponent<Animator>().SetBool("show", false);
    }

    public void SetData(string uri) {
        urlUI.text = uri;
    }
}
