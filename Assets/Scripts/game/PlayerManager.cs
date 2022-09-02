// by Leo Pawel <https://github.com/galaxy126>
// at 10/07/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes2D;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerManager instance;
    
    // session token
    public string cookie;

    // user avatar raw iamge
    public Shape avata;

    Texture2D noneAvata;

    // user name
    public string username;
    public Text usernameTxt;
    // unique account id
    public int id;

    // balance
    public float balance;
    public Text balanceTxt;

    // card holding status
    public List<Card> cards = new List<Card>();

    // position in room
    public int position;

    public RankText ranktext;

    public ResultText resultText;

    public ProfileUI profileUI;
    

    
    void Start() {
        instance = this;
        balanceTxt.text = "";
        usernameTxt.text = "";
    }

    void OnEnable() {
        Reset();
        RoundReset();
    }
    public void Reset(){
        noneAvata = Resources.Load<Texture2D>("noneAvata");
        avata.settings.fillTexture = noneAvata;
        balanceTxt.text = "";
        usernameTxt.text = "";
    }

    public void RoundReset(){
        foreach(var card in cards){
            card.Reset();
        }
        ranktext.Reset();
        resultText.Reset();
        profileUI.Reset();
    }

    public void SetEnterRoomData(List<string> data){
        Reset();
        if(data[0] == "0") return;
        usernameTxt.text = popString(data);
        avata.settings.fillTexture = ParseTexture(popString(data));
        balanceTxt.text = float.Parse(popString(data)).ToString("0.00");
    }

    // Update is called once per frame
    void Update() {
            
    }

    public void ShowCard(int position){
        cards[position].Show();
    }

    public void SetFocus(){
        profileUI.Focus();
    }

    public void SetCardsInfo(List<int> IdList, List<int> restCards = null){
        if(IdList.Count == 4){
            if(position != 0) return;
            for(int i = 0; i < 4; i++){
                cards[i].SetId(IdList[i], this);
            }
        
        }else if(IdList.Count == 1){
            if(position != 0) return;
            cards[4].SetId(IdList[0], this);
        
        }else if(IdList.Count == 5){
            for(int i = 0; i < 5; i++){
                cards[i].SetId(IdList[i], this, restCards);
            }
        }
    }

    public void SetBankerMark(){
        profileUI.SetBankMark();
    }

    public void ShowbankerNum(int id){
        ranktext.ShowNumber(id);
    }

    public void FilpCards(){
        foreach(var card in cards){
            card.FilpCard();
        }
    }

    public void UpCard(){
        foreach(var card in cards){
            card.UpCard();
        }
    }
    public void ShowPaeResult(int result, int num){
        resultText.ShowPaeResult(result, num);
    }

    public void SetBankMark(){
       
    }

    string popString(List<string> list){
        string result = list[0];
        list.RemoveAt(0);
        return result;
    }

    public Texture2D ParseTexture(string txtureString){
        /*string b64_string = loadFromSomewhere;*/
        byte[] b64_bytes = System.Convert.FromBase64String(txtureString);
        Texture2D tex = new Texture2D(100,100);
        tex.LoadImage( b64_bytes);
        return tex;
        
    }

}
