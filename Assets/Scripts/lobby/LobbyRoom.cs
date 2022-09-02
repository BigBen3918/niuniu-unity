using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyRoom : MonoBehaviour
{
    // Start is called before the first frame update
    public Text roomId;

    public Text gameRule;

    public Text antes;
    public Shapes2D.Shape[] playerPhotos;

    void Start()
    {
        
    }
    
    public Texture2D ParseTexture(string txtureString){
        /*string b64_string = loadFromSomewhere;*/
        byte[] b64_bytes = System.Convert.FromBase64String(txtureString);
        Texture2D tex = new Texture2D(100,100);
        tex.LoadImage( b64_bytes);
        return tex;
        
    }
    
    public void UpdateRoom(List<string> info){
        for(int i = 0; i < 6; i ++){
            if(info[i] != "0"){
                playerPhotos[i].gameObject.SetActive(true);
                playerPhotos[i].settings.fillTexture = ParseTexture(info[i]);
            }else{
                playerPhotos[i].gameObject.SetActive(false);
            }
        }
    }

    public void Init(List<string> info){
        roomId.text = popString(info);

        //rule dump
        popString(info);

        antes.text = popString(info);
        //dump
        popString(info);
        UpdateRoom(info);
    }

    string popString(List<string> list){
        string result = list[0];
        list.RemoveAt(0);
        return result;
    }

    public void JoinRoom(){
        UI.instance.ShowLoading(true);
        NetworkManager.instance.Send("join-room", new string[]{roomId.text});
    }

    public void Delete(){
        Destroy(gameObject);
    }
}
