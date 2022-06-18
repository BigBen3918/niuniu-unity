using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalDatas
{
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