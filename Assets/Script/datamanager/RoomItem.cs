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

    void Start()
    {
        this.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void setRoomInfo(Room _userinfo)
    {
        //set room info
        roominfo = new Room(_userinfo);
        id_field.text = roominfo.id;
        setting_field.text = roominfo.setting;
        cost_field.text = (roominfo.cost).ToString();
    }

    public void getRoomInfo()
    {
        RobbyManager robby = FindObjectOfType<RobbyManager>();
        robby.joinRoom(roominfo);
    }
}