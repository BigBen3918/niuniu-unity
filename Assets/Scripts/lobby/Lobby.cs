// by Leo Pawel <https://github.com/galaxy126>
// at 10/07/2022
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    // Start is called before the first frame update
    public static Lobby instance;
    public Dictionary<string, LobbyRoom> rooms = new Dictionary<string, LobbyRoom>();
    public Transform content;
    public Text currentPageNumTxt;
    public Text allPageNumTxt;

    int currentPageNum;
    int allPageNum = 0;

    void Start() {
        instance = this;
    }

    async Task DeleteRoom(LobbyRoom room){
        print("delete rooms");
        Destroy(room.gameObject);
        await Task.Delay(10);
    }
    
    async Task Reset(){
        allPageNum = 0;
        currentPageNumTxt.text = currentPageNum.ToString();
        allPageNumTxt.text = allPageNum.ToString();
        List<Task> tasks = new List<Task>();
        LobbyRoom[] rooms1 = GetComponentsInChildren<LobbyRoom>();
        foreach(var room in rooms1){
            tasks.Add(DeleteRoom(room));
            //await DeleteRoom(room.Value, room.Key);
            //Destroy(room.gameObject);
            //rooms.Remove(room.Key);
        }
        await Task.WhenAll(tasks);

        return;
        
    }
    void OnEnable() {
        print("OnEnable");
        instance = this;
        GetCurrentPage();
        GameManager.instance.currentPage = CURRENT_PAGE.Lobby;
    }

    // async Task OnDisable() {
        
    // }

    // Update is called once per frame
    // void Update() {
            
    // }
    
    // void InstanceRoom(){
        
    // }

    public async Task UpdateLobby(List<string> lobbyInfo){
        await Reset();
        print("passed");
        allPageNum = int.Parse(popString(lobbyInfo));
        if(allPageNum == 0) {
            currentPageNum = 0;
            allPageNum = 0;
            currentPageNumTxt.text = currentPageNum.ToString();
            allPageNumTxt.text = allPageNum.ToString();
            return;
        }
        int roomNum = int.Parse(popString(lobbyInfo));
        // allPageNum = allPageNum;
        currentPageNumTxt.text = (currentPageNum + 1).ToString();
        allPageNumTxt.text = allPageNum.ToString();
        for(int i = 0; i < roomNum; i++){
            List<string> roomInfo = new List<string>();
            for(int ii = 0; ii < 10; ii ++){
                roomInfo.Add(popString(lobbyInfo));
            }
            UpdateRoom(roomInfo);
        }
    }
    void UpdateRoom(List<string> roomInfo){
        print("roomInfo" + roomInfo[0]);
        // if(rooms.ContainsKey(roomInfo[0])){
        //     rooms[roomInfo[0]].Init(roomInfo);
        // }else{
            GameObject lobbyRoom = GameObject.Instantiate(Resources.Load("items/LobbyRoom"), content) as GameObject;
            lobbyRoom.transform.SetParent(content);
            //rooms.Add(roomInfo[0], lobbyRoom.GetComponent<LobbyRoom>());
            lobbyRoom.GetComponent<LobbyRoom>().Init(roomInfo);
        //} 
    }

    void GetCurrentPage(){
        NetworkManager.instance.Send("enter-lobby", new string[]{currentPageNum.ToString()});
    }

    string popString(List<string> list){
        string result = list[0];
        list.RemoveAt(0);
        return result;
    }

    public void OnNextPage(){
        if(allPageNum > currentPageNum + 1 ){
            currentPageNum ++;
            GetCurrentPage();
        }
        return;
    }

    public void OnBackPage(){
        if(currentPageNum - 1 >= 0 ){
            currentPageNum --;
            GetCurrentPage();
        }
        return;
    }
}
