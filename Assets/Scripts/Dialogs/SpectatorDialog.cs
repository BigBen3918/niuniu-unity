using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpectatorDialog : Dialog
{
    public static SpectatorDialog instance;
    public List<SpectatorItem> items = new List<SpectatorItem>();
    public Transform contentTransform;
    public GameObject noneDataUI;
    // Start is called before the first frame update
    void Start() {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Reset(){
        int count = items.Count;
        for(int i = 0; i < count; i++)
        {

            items[i].Delete();
            //items.RemoveAt(i);
        }
        items.Clear();
        yield return new WaitForSeconds(0.1f);
        
    }
    void AddItem(string ID, string alias, string avatar, string balance) {
        
        GameObject rankItem = GameObject.Instantiate(Resources.Load("items/spectatorItem"), contentTransform) as GameObject;
        rankItem.GetComponent<SpectatorItem>().SetInfo(ID, alias, avatar, balance);
        rankItem.transform.SetParent(contentTransform);
        items.Add(rankItem.GetComponent<SpectatorItem>());
    }

    public void Show(){
        noneDataUI.SetActive(true);
        GetComponent<Animator>().SetBool("show", true);
        if(GameRoom.instance == null) return;
        UI.instance.ShowLoading(true);
        NetworkManager.instance.Send("get-spectators", new string[]{GameRoom.instance.roomIdRxt.text});
    }

    public void Hide(){
        GetComponent<Animator>().SetBool("show", false);
    }

    public void SetData(List<string> data) {
        StartCoroutine(Reset());
        
        int num = int.Parse(base.popString(data));
        for(int i = 0; i < num; i ++){
            AddItem(popString(data), popString(data), popString(data), popString(data));
        }
        noneDataUI.SetActive(num==0);
    }
}
