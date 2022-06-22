using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Firesplash.UnityAssets.SocketIO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SimpleJSON;

public class GameRoom : MonoBehaviour
{
    public Animator Cards;
    public SocketIOCommunicator sioCom;
    public Sprite[] spade, heart, club, diamond;
    public Text roomNumber, bonus;
    public GameObject grabPanel;
    public GameObject multiplePanel;

    /*------------------- -------------------*/
    public UserPerson[] persons;
    public Sprite back;

    void Awake()
    {
        sioCom =
            FindObjectOfType(typeof(SocketIOCommunicator)) as
            SocketIOCommunicator;
    }

    // Start is called before the first frame update
    void Start()
    {
        loadUserInfo();
        OnRoomData();
    }

    ~GameRoom()
    {
        sioCom.Instance.Emit("out room");
        sioCom.Instance.Off("start game");
        sioCom.Instance.Off("room status");
        sioCom.Instance.Off("room update");
    }

    private void Update()
    {
        grabPanel.SetActive(false);
        multiplePanel.SetActive(false);
        if (GlobalDatas.croom.gameStatus == 1)
        {
            int userIndex = getplayerIndex();
            if (GlobalDatas.croom.playerStatus[userIndex].grab == -1)
            {
                grabPanel.SetActive(true);
            }
        }
        else if (GlobalDatas.croom.gameStatus == 2)
        {
            int userIndex = getplayerIndex();
            if (GlobalDatas.croom.playerStatus[userIndex].doubles == -1 && GlobalDatas.croom.playerStatus[userIndex].role != "banker")
            {
                multiplePanel.SetActive(true);
            }
        }
    }
    // set robby list data
    private void loadUserInfo()
    {
        bonus.text = GlobalDatas.croom.cost.ToString();
        roomNumber.text = GlobalDatas.croom.id.ToString();
        int playerIndex = getplayerIndex();
        for (int i = playerIndex; i < GlobalDatas.croom.players.Length; i++)
        {
            persons[i - playerIndex].setUserInfo(GlobalDatas.croom.players[i].score, GlobalDatas.croom.players[i].username);
        }
        for (int i = 0; i < playerIndex; i++)
        {
            persons[playerIndex + i].setUserInfo(GlobalDatas.croom.players[i].score, GlobalDatas.croom.players[i].username);
        }
    }

    private void OnRoomData()
    {
        sioCom.Instance.On("round start", (string data) => 
        {
            sioCom.Instance.Emit("room status");
        });

        sioCom.Instance.On("room status", (string data) =>
        {
            Room room = JsonUtility.FromJson<Room>(data);
            GlobalDatas.croom = room;
            if (room.roleCount == 0 && room.gameStatus == 1)
            {
                StartCoroutine(game_start_enum());
            }
            if(room.gameStatus == 5)
            {
                StartCoroutine(game_end_enum());
            }
            loadUserInfo();
        });

        sioCom.Instance.On("grabBank", (string data) =>
        {
            Multiple grabBank = JsonUtility.FromJson<Multiple>(data);
            for (int i = 0; i < GlobalDatas.croom.playerStatus.Length; i++)
            {
                if (GlobalDatas.croom.playerStatus[i].username == grabBank.player.username)
                {
                    GlobalDatas.croom.playerStatus[i].grab = grabBank.multiple;
                    persons[i].setGrab(grabBank.multiple);
                    break;
                }
            }
        });

        sioCom.Instance.On("endGrab", (string data) =>
        {
            sioCom.Instance.Emit("room status");
        });

        sioCom.Instance.On("double", (string data) =>
        {
            Multiple doubles = JsonUtility.FromJson<Multiple>(data);
            for (int i = 0; i < GlobalDatas.croom.playerStatus.Length; i++)
            {
                if (GlobalDatas.croom.playerStatus[i].username == doubles.player.username)
                {
                    GlobalDatas.croom.playerStatus[i].doubles = doubles.multiple;
                    persons[i].setGrab(doubles.multiple);
                    break;
                }
            }
        });

        sioCom.Instance.On("endRound", (string data) =>
        {
            GlobalDatas.croom.playerStatus[GlobalDatas.myIndex].doubles = -1;
            Debug.Log("my score: " + GlobalDatas.croom.playerStatus[GlobalDatas.myIndex].roundScore.score);
            Debug.Log("please wait 10sec...");
            StartCoroutine(game_end_enum());
        });

        sioCom.Instance.On("outed room", (string data) =>
        {
            SceneManager.LoadScene(1);
        });

        sioCom.Instance.Emit("is ready");
    }
  
    public void bet(int multiple)
    {
        Grab grab = new Grab(multiple);
        if (GlobalDatas.croom.gameStatus == 1)
        {
            sioCom.Instance.Emit("grab bank", JsonUtility.ToJson(grab), false);
        }
        else if(GlobalDatas.croom.gameStatus == 2)
        {
            sioCom.Instance.Emit("doubles", JsonUtility.ToJson(grab), false);
        }
    }

    private IEnumerator game_start_enum()
    {
        int playerIndex = getplayerIndex();
        GlobalDatas.myIndex = playerIndex;
        Cards.SetBool("flag", true);
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < GlobalDatas.croom.playerStatus.Length; i++)
        {
            StartCoroutine(persons[i].cardInitial());
            yield return new WaitForSeconds(0.8f);
        }
        Cards.SetBool("flag", false);
        yield return new WaitForSeconds(0.2f);

        for (int i = playerIndex; i < GlobalDatas.croom.players.Length; i++)
        {
            StartCoroutine(persons[i - playerIndex].setCardEnumerator(GlobalDatas.croom.playerStatus[i].cards));
        }
        for (int i = 0; i < playerIndex; i++)
        {
            StartCoroutine(persons[playerIndex + i].setCardEnumerator(GlobalDatas.croom.playerStatus[i].cards));
        }
        yield return new WaitForSeconds(0.8f);

    }
    private IEnumerator game_end_enum()
    {
        int playerIndex = getplayerIndex();
        GlobalDatas.myIndex = playerIndex;
        for (int i = playerIndex; i < GlobalDatas.croom.playerStatus.Length; i++)
        {
            StartCoroutine(persons[i - playerIndex].setCardEnumerator(GlobalDatas.croom.playerStatus[i].cards));
        }
        for (int i = 0; i < playerIndex; i++)
        {
            StartCoroutine(persons[i + playerIndex].setCardEnumerator(GlobalDatas.croom.playerStatus[i].cards));
        }
        yield return new WaitForSeconds(8f);

        for (int i = 0; i < GlobalDatas.croom.playerStatus.Length; i++)
        {
            StartCoroutine(persons[i].cardClear());
        }
    }

    // utils
    public int getplayerIndex()
    {
        int count = 0;
        for (int i = 0; i < GlobalDatas.croom.players.Length; i++)
        {
            if (GlobalDatas.croom.players[i].username == GlobalDatas.authdata.username)
            {
                count = i;
                break;
            }
        }
        return count;
    }
}

public class Grab
{
    public int multiple;

    public Grab(int setting)
    {
        this.multiple = setting;
    }
}

public class Multiple
{
    public User player;
    public int multiple;

    public Multiple(int multiple)
    {
        this.multiple = multiple;
    }
}