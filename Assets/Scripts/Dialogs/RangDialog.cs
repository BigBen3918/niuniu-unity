using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangDialog : Dialog
{
    public static RangDialog instance;
    public List<RankItem> items = new List<RankItem>();
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
    void AddItem(int num, string ID, string alias, string avatar, string exp) {
        
        GameObject rankItem = GameObject.Instantiate(Resources.Load("items/rankItem"), contentTransform) as GameObject;
        rankItem.GetComponent<RankItem>().SetInfo(num, ID, alias, avatar, exp);
        rankItem.transform.SetParent(contentTransform);
        items.Add(rankItem.GetComponent<RankItem>());
    }

    public void Show(){
        noneDataUI.SetActive(true);
        GetComponent<Animator>().SetBool("show", true);
        UI.instance.ShowLoading(true);
        NetworkManager.instance.Send("get-exps", new string[]{});
    }

    public void Hide(){
        GetComponent<Animator>().SetBool("show", false);
    }

    public void SetData(List<string> data) {
        StartCoroutine(Reset());
        
        int num = int.Parse(base.popString(data));
        for(int i = 0; i < num; i ++){
            AddItem(i, popString(data), popString(data), popString(data), popString(data));
        }
        noneDataUI.SetActive(num==0);
    }
}
