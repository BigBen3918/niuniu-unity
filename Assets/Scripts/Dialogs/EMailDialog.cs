using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class EMailDialog : Dialog
{
    public Transform contentTransform;
    public static EMailDialog instance;
    public List<EMailItem> items = new List<EMailItem>();
    public GameObject notfoundLabel;
    // Start is called before the first frame update
    void Start() {
        instance = this;
    }

    // Update is called once per frame
    void Update() {
        
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

    void AddItem(string ID, string content, string date) {
        
        GameObject emailItem = GameObject.Instantiate(Resources.Load("items/emailItem"), contentTransform) as GameObject;
        emailItem.GetComponent<EMailItem>().SetInfo(ID, content, date);
        emailItem.transform.SetParent(contentTransform);
        items.Add(emailItem.GetComponent<EMailItem>());
    }

    public void Show() {
        GetComponent<Animator>().SetBool("show", true);
        StartCoroutine(Reset());
        UI.instance.ShowLoading(true);
        notfoundLabel.SetActive(false);
        NetworkManager.instance.Send("get-sysmsg", new string[]{});
    }

    public void Hide() {
        GetComponent<Animator>().SetBool("show", false);
    }

    public void SetData(List<string> data) {
        StartCoroutine(Reset());
        
        int num = int.Parse(base.popString(data));
        notfoundLabel.SetActive(num==0);
        for(int i = 0; i < num; i ++){
            AddItem(popString(data), popString(data), popString(data));
        }
    }
}
