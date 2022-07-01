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
    public Text balance, score, username, userid;
    public RawImage userImage;

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

    public void screenInit()
    {
        userid.text = GlobalDatas.authdata.id.ToString();
        balance.text = GlobalDatas.authdata.balance.ToString();
        score.text = GlobalDatas.authdata.score.ToString();
        username.text = GlobalDatas.authdata.username;
        StartCoroutine(ExtensionMethods.GetTextureFromURL(GlobalDatas.authdata.image, (Texture2D coverImage, bool isSuccess) =>
        {
            if (!isSuccess) return;
            userImage.texture = coverImage;
        }));
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
        });

        // when user join to room
        sioCom.Instance.On("entered room", (string data) =>
        {
            Room room = JsonUtility.FromJson<Room>(data);
            GlobalDatas.croom = room;
            sioCom.Instance.Off("rooms");
            sioCom.Instance.Off("entered room");
            sioCom.Instance.Off("get user");
            SceneManager.LoadScene(2);
        });
        
        sioCom.Instance.On("get user", (string data) =>{
            AuthInfo authdata = AuthInfo.CreateFromJSON(data);
            GlobalDatas.SetAuth(authdata);
            screenInit();
        });

        sioCom.Instance.Emit("get user");
        sioCom.Instance.Emit("get rooms");
    }

    private void SetRoomList()
    {
        if(roomsContent.transform.childCount != 0)
        {
            ExtensionMethods.Clear(roomsContent.transform);
        }
        for (int i = 0; i < GlobalDatas.rooms.rooms.Length; i++)
        {
            Room roomInfo = GlobalDatas.rooms.rooms[i];
            GameObject _roomItem = Instantiate(roomItem);
            _roomItem.transform.parent = roomsContent.transform;

            _roomItem.GetComponent<RoomItem>().setRoomInfo(roomInfo);
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

    public void referesh()
    {
        SetRoomList();
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
                SetRoomList();
                break;
            case 2:     // enter in rooom
                Dialog.SetActive(true);
                break;
            case 3:
                Dialog.SetActive(false);
                break;
            case 4:
                sioCom.Instance.Off("rooms");
                sioCom.Instance.Off("entered room");
                sioCom.Instance.Off("get user");
                SceneManager.LoadScene(3);
                break;
        }
    }
}
