using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
public class RoomItem : MonoBehaviour
{
    public Room roominfo;

    public TextMeshProUGUI id_field;
    public TextMeshProUGUI setting_field;
    public TextMeshProUGUI cost_field;
    public Transform[] players;
    public void setRoomInfo(Room _userinfo)
    {
        //set room info
        roominfo = new Room(_userinfo);
        id_field.text = roominfo.id;
        setting_field.text = roominfo.setting;
        cost_field.text = (roominfo.cost).ToString();
        // set players info and max is 6
        //players = ExtensionMethods.FindChildren(transform, "user");
        //for (int i = 0; i < roominfo.players.Length && i < 6; i++)
        //{
        //    Transform playerImage = players[i].GetChild(0);
        //    StartCoroutine(ExtensionMethods.GetTextureFromURL(roominfo.players[i].image, (Texture2D image, bool isSuccess) =>
        //    {
        //        if (!isSuccess) return;
        //        playerImage.GetComponent<RawImage>().texture = image;
        //    }));
        //}
    }

    public void getRoomInfo()
    {
        RobbyManager robby = FindObjectOfType<RobbyManager>();
        robby.joinRoom(roominfo);
    }
}

[Serializable]
public class Room
{
    public string id;
    public string setting;
    public float cost;
    public User[] players;
    public int maxPlayer;

    public Room(string setting, float cost, string id = "", int maxPlayer = 6)
    {
        this.setting = setting;
        this.cost = cost;
        this.id = id;
        this.maxPlayer = maxPlayer;
    }

    public Room(Room _userinfo)
    {
        this.id = _userinfo.id;
        this.setting = _userinfo.setting;
        this.cost = _userinfo.cost;
        this.players = _userinfo.players;
        this.maxPlayer = _userinfo.maxPlayer;
    }
}