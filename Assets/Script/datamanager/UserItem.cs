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