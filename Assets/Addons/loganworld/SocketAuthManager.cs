using Firesplash.UnityAssets.SocketIO;
using System;
using UnityEngine;
using UnityEngine.UI;

#if HAS_JSON_NET
//If Json.Net is installed, this is required for Example 6. See documentation for informaiton on how to install Json.NET
using Newtonsoft.Json;
#endif


public class SocketAuthManager : MonoBehaviour
{
    public SocketIOCommunicator sioCom;

    public AuthInfo authdata;

    public bool socketConnected = false;

    void Start()
    {
        sioCom
            .Instance
            .On("connect",
            (string data) =>
            {
                socketConnected = true;
            });

        sioCom
            .Instance
            .On("disconnect",
            (string data) =>
            {
                socketConnected = false;
            });

        sioCom
            .Instance
            .On("loginError",
            (string data) =>
            {
                Debug.Log(data);
            });

        sioCom
            .Instance
            .On("authError",
            (string data) =>
            {
                if (authdata.username != "" && authdata.password != "")
                    Login(authdata.username, authdata.password);
            });

        DontDestroyOnLoad(this.gameObject);
    }

    public void Login(string username, string password)
    {
        authdata.username = username;
        authdata.password = password;
        string json = JsonUtility.ToJson(authdata);
        sioCom.Instance.Emit("login", json, false);
    }

    public void SignUp(string username, string password, string phonenumber)
    {
        authdata.username = username;
        authdata.password = password;
        authdata.phonenumber = phonenumber;
        string json = JsonUtility.ToJson(authdata);
        sioCom.Instance.Emit("signup", json, false);
    }

}

[Serializable]
public class AuthInfo
{
    public string username;
    public string password;
    public string phonenumber;
    public AuthInfo(string username, string phonenumber, string password = "")
    {
        this.username = username;
        this.phonenumber = phonenumber;
        this.password = password;
    }

    public static AuthInfo CreateFromJSON(string data)
    {
        return JsonUtility.FromJson<AuthInfo>(data);
    }

    public AuthInfo(AuthInfo authdata)
    {
        this.username = authdata.username;
        this.phonenumber = authdata.phonenumber;
        this.password = authdata.password;
    }
}
