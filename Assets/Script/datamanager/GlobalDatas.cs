using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalDatas
{
    public static int myIndex;
    public static AuthInfo authdata;
    public static UserLists users;
    public static RoomLists rooms;
    public static Room croom;

    public static string defaultUserImage = "";

    public static void SetAuth(AuthInfo _authdata)
    {
        authdata = new AuthInfo(_authdata);
    }
}

[Serializable]
public class UserLists
{
    public User[] users;
}

[Serializable]
public class RoomLists
{
    public Room[] rooms;
}

[Serializable]
public class PlayerStatus
{
    public User player;
    public string roll;
    public int grab;
    public int doubles;
    public int[] cards;
    public float score;
}

/** Global Main Class */

[Serializable]
public class Room
{
    public string id;
    public string creator;
    public float cost;
    public string setting;
    public string name;
    public int maxPlayer;
    public int gameStatus;
    public int roller;
    public User[] players;
    public PlayerStatus[] playerStatus;

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

[Serializable]
public class User
{
    public string username;
    public string score;
    public string id;
    public string image;
    public string phonenumber;
    public int grab = 5;
    public int doubles = 5;
    public string roll;

    public User(string username, string score, string id, string image = "", string phonenumber = "", int grab = 5, int doubles = 5,string roll = "")
    {
        this.username = username;
        this.score = score;
        this.id = id;
        this.image = image;
        this.phonenumber = phonenumber;
        this.grab = grab;
        this.doubles = doubles;
        this.roll = roll;
    }

    public User(User _userinfo)
    {
        this.username = _userinfo.username;
        this.score = _userinfo.score;
        this.id = _userinfo.id;
        this.image = _userinfo.image;
        this.phonenumber = _userinfo.phonenumber;
        this.grab = _userinfo.grab;
        this.doubles = _userinfo.doubles;
        this.roll = _userinfo.roll;
    }
}