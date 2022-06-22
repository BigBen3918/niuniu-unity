using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class table : MonoBehaviour
{
    public string id;
    public GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        id = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(id != "")
        {
            showMultiple();
        }
    }

    public void setID(string param)
    {
        id = param;
    }
      
    private void showMultiple()
    {
        for (int i = 0; i < GlobalDatas.croom.players.Length; i++)
        {
            if (GlobalDatas.croom.players[i].id == id)
            {
                Debug.Log(GlobalDatas.croom.players[GlobalDatas.myIndex].grab);
                string txt = "";
                if (GlobalDatas.croom.players[i].grab > 0 && GlobalDatas.croom.players[i].grab < 5)
                {
                    txt = "x" + GlobalDatas.croom.players[i].grab.ToString();
                }
                else if (GlobalDatas.croom.players[i].grab == 0)
                {
                    txt = "不抢";
                }
                effect.transform.GetChild(0).gameObject.GetComponent<Text>().text = txt;

                if (GlobalDatas.croom.players[i].grab != 5)
                    effect.transform.GetChild(0).gameObject.SetActive(true);
                else
                    effect.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
