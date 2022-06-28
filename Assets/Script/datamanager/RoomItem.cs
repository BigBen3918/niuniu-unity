using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Room roominfo;

    public Text id_field, setting_field, cost_field;
    public Transform[] players;

    void Start()
    {
        this.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void setRoomInfo(Room _userinfo)
    {
        // set room info
        roominfo = new Room(_userinfo);
        id_field.text = Random.Range(30000, 80000).ToString();
        setting_field.text = roominfo.setting;
        cost_field.text = (roominfo.cost).ToString();

        Debug.Log(roominfo.players.Length);

        for (int i = 0; i < roominfo.players.Length; i++)
        {
            Debug.Log(roominfo.players[i].image);
            StartCoroutine(ExtensionMethods.GetTextureFromURL(roominfo.players[i].image, (Texture2D coverImage, bool isSuccess) =>
            {
                if (!isSuccess) return;
                players[i].GetChild(0).GetComponent<RawImage>().texture = coverImage;
            }));
        }
    }

    public void getRoomInfo()
    {
        RobbyManager robby = FindObjectOfType<RobbyManager>();
        robby.joinRoom(roominfo);
    }
}