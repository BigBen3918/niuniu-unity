using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    public List<RectTransform> cardSlots = new List<RectTransform>();
    public DistributeManager distriManager;
    public GameRoom gameRoom;
    int playerPosition;
    int from;
    int to;
    int balance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
    public void CardDistribute(){
        //distriManager.StartDistribute(new int[]{0,4,5});
    }

    // public void SetCardsInfo(string num){
    //     if(int.Parse(num) == 4)
    //         gameRoom.SetCardsInfo(playerPosition, new int[]{12,34,1,17});
    //     if(int.Parse(num) == 1)
    //         gameRoom.SetCardsInfo(playerPosition, new int[]{15});
    //     if(int.Parse(num) == 5){
    //         gameRoom.SetCardsInfo(playerPosition, new int[]{1,5,6,8,19});
    //     }
    // }

    public void SetPlayerPosition(string position){
        playerPosition = int.Parse(position);
        
    }


    public void FilpCard(){
        gameRoom.FilpCard(playerPosition);
    }

    int paeResultNum = 0;
    public void SetPaeResultNum(string num){
        paeResultNum = int.Parse(num);
    }

    public void SetCardResult(string num){
        gameRoom.ShowCardResult(playerPosition, int.Parse(num), paeResultNum);
    }

    public void GotCoin(string balance){
        gameRoom.GotCoin(playerPosition, float.Parse(balance));
    }

    public void LostCoin(string balance){
        gameRoom.LostCoin(playerPosition, float.Parse(balance));
    }

    public void SetFrom(string _from){
        from = int.Parse(_from);
    }

    public void SetTo(string _to){
        to = int.Parse(_to);
    }

    public void SetBalance(string _balance){
        balance = int.Parse(_balance);
    }

    public void SendCoin(){
        gameRoom.SendCoin(from, to, balance);
    }

    //////////////////////net///////////////////////
    string testUserId;
    string testRoomId;
    public void SetUserId(string id){
        testUserId = id;
    }

    public void SetRoomId(string id){
        testRoomId = id;
    }

    public void CreateRoom(){
        NetworkManager.instance.Send("test-create-room", new string[]{testUserId, "0.999"});
    }

    public void JoinRoom(){
        NetworkManager.instance.Send("test-join-room", new string[]{testUserId, testRoomId});
    }

}
