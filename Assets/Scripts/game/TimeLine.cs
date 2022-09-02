using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLine : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] childs;
    float lastUpdateTime;
    float timeOut = 1.7f;

    public Text infoTxt;
    void Start()
    {
        lastUpdateTime = Time.time;
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastUpdateTime > timeOut)
            Hide();
    }

    public void Show(){
        for(int i = 0; i < childs.Length; i++){
            childs[i].SetActive(true);
        }
        GetComponent<Image>().enabled = true;
        lastUpdateTime = Time.time;
    }

    public void Hide(){
        for(int i = 0; i < childs.Length; i++){
            childs[i].SetActive(false);
        }
        GetComponent<Image>().enabled = false;
    }

    public void UpdateInfo(List<string> info){
        infoTxt.text = info[0];
        lastUpdateTime = Time.time;
        Show();
    }
}
