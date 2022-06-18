using UnityEngine;
using UnityEngine.UI;
using Firesplash.UnityAssets.SocketIO;
using UnityEngine.SceneManagement;

public class loginManager : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField username,
            password,
            username_s,
            password_s,
            phonenumber_s;

    public Text uiStatus;
    public GameObject loginPanel, signupPanel;

    public SocketAuthManager saManager;

    public SocketIOCommunicator sioCom;

    void Awake()
    {
        saManager =
            GameObject.FindObjectOfType(typeof(SocketAuthManager)) as
            SocketAuthManager;

        sioCom =
            GameObject.FindObjectOfType(typeof(SocketIOCommunicator)) as
            SocketIOCommunicator;
    }

    void Start()
    {
        sioCom.Instance.On("signupSuccess", (string data) =>
        {
            loginPanel.SetActive(true);
            signupPanel.SetActive(false);
        });
        sioCom.Instance.On("loginSuccess", (string data) =>
        {
            AuthInfo authdata = AuthInfo.CreateFromJSON(data);
            GlobalDatas.SetAuth(authdata);
            SceneManager.LoadScene(1);
        });

        sioCom.Instance.On("connect", (string data) =>
        {
            uiStatus.text = "connected";
        });
    }

    public void Login()
    {
        saManager.Login(username.text, password.text);
    }

    public void SignUp()
    {
        saManager.SignUp(username_s.text, password_s.text, phonenumber_s.text);
    }
}
