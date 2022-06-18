using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class UserItem : MonoBehaviour
{
    public User userinfo;
    public TextMeshProUGUI username_field;
    public TextMeshProUGUI score_field;

    public void setUserInfo(User _userinfo)
    {
        userinfo = new User(_userinfo);
        username_field.text = userinfo.username;
        score_field.text = userinfo.score;
    }

    public void Onclick()
    {
        RobbyManager robbyManager = GameObject.FindObjectOfType(typeof(RobbyManager)) as RobbyManager;
    }

}

[Serializable]
public class User
{
    public string username;
    public string score;
    public string id;
    public string image;

    public User(string username, string score, string id, string image = "")
    {
        this.username = username;
        this.score = score;
        this.id = id;
        this.image = image;
    }

    public User(User _userinfo)
    {
        this.username = _userinfo.username;
        this.score = _userinfo.score;
        this.id = _userinfo.id;
        this.image = _userinfo.image;
    }
}