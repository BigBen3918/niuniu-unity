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
    public static bool isStarted=false;

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
    public int roleCount;
    public User[] players;
    public User[] playerStatus;

    public Room(string setting, float cost, string id = "", int maxPlayer = 6, int roleCount = -1, int gameStatus = 0)
    {
        this.setting = setting;
        this.cost = cost;
        this.id = id;
        this.maxPlayer = maxPlayer;
        this.roleCount = roleCount;
        this.gameStatus = gameStatus;
    }

    public Room(Room _userinfo)
    {
        this.id = _userinfo.id;
        this.setting = _userinfo.setting;
        this.cost = _userinfo.cost;
        this.players = _userinfo.players;
        this.maxPlayer = _userinfo.maxPlayer;
        this.roleCount = _userinfo.roleCount;
    }
}

[Serializable]
public class User
{
    public int[] cards;
    public int doubles = -1;
    public int grab = -1;
    public string id;
    public string role;
    public bool onRound;
    public RoundScore roundScore;

    // auth
    public string username;
    public string phonenumber;
    public string image;
    public float balance;

    public User(string username, string id, string image = "", string phonenumber = "", int grab = -1, int doubles = -1,string role = "", int[] cards = null, float balance = 0f, bool onRound = false)
    {
        this.username = username;
        this.id = id;
        this.image = image;
        this.phonenumber = phonenumber;
        this.grab = grab;
        this.doubles = doubles;
        this.role = role;
        this.cards = cards;
        this.balance = balance;
        this.onRound = onRound;
    }

    public User(User _userinfo)
    {
        this.username = _userinfo.username;
        this.id = _userinfo.id;
        this.image = _userinfo.image;
        this.phonenumber = _userinfo.phonenumber;
        this.grab = _userinfo.grab;
        this.doubles = _userinfo.doubles;
        this.role = _userinfo.role;
        this.balance = _userinfo.balance;
        this.onRound = _userinfo.onRound;
    }
}

[Serializable]
public class RoundScore
{
    public string type;
    public float score;
    public int multiple;
    public int[] cards;
    public int[] activityCards;

    public RoundScore(string _type = "", float _score = 0f, int _multiple = -1, int[] _cards = null, int[] _activityCards = null)
    {
        this.type = _type;
        this.score = _score;
        this.multiple = _multiple;
        this.cards = _cards;
        this.activityCards = _activityCards;
    }
}