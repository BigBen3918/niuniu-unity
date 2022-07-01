using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
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

        for (int i = 0; i < roominfo.players.Length; i++)
        {
            string url = roominfo.players[i].image;
            StartCoroutine(setImage(url, players[i]));
        }
    }

    IEnumerator setImage(string url, Transform player)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            player.GetChild(0).GetComponent<RawImage>().texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }

    public void getRoomInfo()
    {
        RobbyManager robby = FindObjectOfType<RobbyManager>();
        robby.joinRoom(roominfo);
    }
}