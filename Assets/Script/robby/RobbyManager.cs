using UnityEngine;
using Firesplash.UnityAssets.SocketIO;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RobbyManager : MonoBehaviour
{
    public GameObject roomsContent, usersContent, hall, hall_item, lobby, lobby_item, Dialog;
    public GameObject roomItem, userItem;
    public SocketIOCommunicator sioCom;
    //room create
    public InputField cost_field;

    void Awake()
    {
        sioCom =
            FindObjectOfType(typeof(SocketIOCommunicator)) as
            SocketIOCommunicator;
    }

    void Start()
    {
        //sioCom.Instance.Emit("enter lobby");
        OnRoomData();
    }

    // set robby list data
    //private void OnRobbyData()
    //{
    //    sioCom.Instance.On("lobby", (string data) =>
    //    {
    //        GlobalDatas.users = JsonUtility.FromJson<UserLists>(data);
    //        setRobbyList();
    //    });
    //}

    //private void setRobbyList()
    //{
    //    ExtensionMethods.Clear(usersContent.transform);
    //    for (int i = 0; i < GlobalDatas.users.users.Length; i++)
    //    {
    //        User playerInfo = GlobalDatas.users.users[i];
    //        GameObject _userItem = Instantiate(userItem);
    //        _userItem.transform.parent = usersContent.transform;

    //        _userItem.GetComponent<UserItem>().setUserInfo(playerInfo);
    //    }
    //}

    // room data
    private void OnRoomData()
    {
        sioCom.Instance.On("rooms", (string data) =>
        {
            GlobalDatas.rooms = JsonUtility.FromJson<RoomLists>(data);
            SetRoomList();
        });

        // when user join to room
        sioCom.Instance.On("entered room", (string data) =>
        {
            Room room = JsonUtility.FromJson<Room>(data);
            GlobalDatas.croom = room;
            SceneManager.LoadScene(2);
            sioCom.Instance.Off("rooms");
            sioCom.Instance.Off("entered room");
        });

        sioCom.Instance.Emit("get rooms");
    }

    private void SetRoomList()
    {
        if(roomsContent.transform.childCount != 0)
        {
            ExtensionMethods.Clear(roomsContent.transform);
        }
        else
        {
            for (int i = 0; i < GlobalDatas.rooms.rooms.Length; i++)
            {
                Room roomInfo = GlobalDatas.rooms.rooms[i];
                GameObject _roomItem = Instantiate(roomItem);
                _roomItem.transform.parent = roomsContent.transform;

                _roomItem.GetComponent<RoomItem>().setRoomInfo(roomInfo);
            }
        }
    }

    public void createRoom()
    {
        float cost = float.Parse(cost_field.text);
        Room newRoom = new Room("test", cost);
        sioCom.Instance.Emit("create room", JsonUtility.ToJson(newRoom), false);
    }

    public void joinRoom(Room selectedRoom)
    {
        sioCom.Instance.Emit("enter room", JsonUtility.ToJson(selectedRoom), false);
    }

    // UI Management
    public void SwitchScreen(int flag)
    {
        switch (flag)
        {
            case 0:     // enter in hall
                hall.SetActive(true);
                hall_item.SetActive(true);
                lobby.SetActive(false);
                lobby_item.SetActive(false);
                break;
            case 1:     // enter in lobby
                hall.SetActive(false);
                hall_item.SetActive(false);
                lobby.SetActive(true);
                lobby_item.SetActive(true);
                break;
            case 2:     // enter in rooom
                Dialog.SetActive(true);
                break;
            case 3:
                Dialog.SetActive(false);
                break;
        }
    }
}
