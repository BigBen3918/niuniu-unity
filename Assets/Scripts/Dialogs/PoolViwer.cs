using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolViwer : MonoBehaviour
{
    public static PoolViwer instance;
    public Text[] timeUIs;
    public Transform banlanceTransfrom;
    public Transform userTransform;
    public Sprite[] numberTextures;
    public Animator animator;
    List<PoolViwerUserItem> userItems = new List<PoolViwerUserItem>();
    List<PoolViwerBalanceItem> balanceItems = new List<PoolViwerBalanceItem>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        
    }

    IEnumerator Reset(){
        int count = userItems.Count;
        int count1 = balanceItems.Count;
        for(int i = 0; i < count; i++)
        {
            userItems[i].Delete();
        }
        for(int i = 0; i < count1; i++)
        {
            balanceItems[i].Delete();
        }
        userItems.Clear();
        balanceItems.Clear();
        yield return new WaitForSeconds(0.1f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(){
        animator.SetBool("show", true);
        UI.instance.ShowLoading(true);
        NetworkManager.instance.Send("get-exps", new string[]{});
    }

    public void Hide(){
        animator.SetBool("show", false);
    }

    public void SetData(List<string> data) {
        StartCoroutine(Reset());
        SetBalance();
        int num = int.Parse(Helper.popString(data));
        for(int i = 0; i < num; i ++){
            AddUserItem(i, Helper.popString(data), Helper.popString(data), Helper.popString(data), Helper.popString(data));
        }
        //noneDataUI.SetActive(num==0);
    }

    void AddUserItem(int num, string ID, string alias, string avatar, string exp) {
        
        GameObject item = GameObject.Instantiate(Resources.Load("items/PoolviwerUserItme"), userTransform) as GameObject;
        item.GetComponent<PoolViwerUserItem>().SetData(avatar, exp);
        item.transform.SetParent(userTransform);
        userItems.Add(item.GetComponent<PoolViwerUserItem>());
    }

    void AddBalanceItem(int num) {
        print(num);
        if(numberTextures[num] == null) return;
        GameObject item = GameObject.Instantiate(Resources.Load("items/PoolviwerBalaceItem"), banlanceTransfrom) as GameObject;
        item.GetComponent<PoolViwerBalanceItem>().SetData(numberTextures[num]);
        item.transform.SetParent(banlanceTransfrom);
        balanceItems.Add(item.GetComponent<PoolViwerBalanceItem>());
    }

    public void SetTime(string time){
        print(time);
        int index = 0;
        for(int i = 0; i < time.Length; i++){
            if(time[i].ToString() != ":"){
                timeUIs[index].text = time[i].ToString();
                index ++;
            }
        }
    }

    public void SetBalance(){
        string balance = GameRoom.instance.poolAmountTxt.text;
        for(int i = 0; i < balance.Length; i++){
            if(balance[i].ToString() != "."){
                AddBalanceItem(int.Parse(balance[i].ToString()));
            }else{
                AddBalanceItem(10);
            }
        }
    }
}
