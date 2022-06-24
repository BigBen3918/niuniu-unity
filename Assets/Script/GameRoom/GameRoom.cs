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
        OnRoomData();
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
        else if (GlobalDatas.croom.gameStatus == 5)
        {
            multiplePanel.SetActive(false);
            grabPanel.SetActive(false);
        }
    }

    // set robby list data
    public void loadUserInfo()
    {
        int playerIndex = getplayerIndex();
        GlobalDatas.myIndex = playerIndex;
        bonus.text = GlobalDatas.croom.cost.ToString();
        roomNumber.text = GlobalDatas.croom.id.ToString();
        for (int i = playerIndex; i < GlobalDatas.croom.playerStatus.Length; i++)
        {
            persons[i - playerIndex].setUserInfo(GlobalDatas.croom.playerStatus[i].balance, GlobalDatas.croom.playerStatus[i].username);
            persons[i - playerIndex].setImage(GlobalDatas.croom.playerStatus[i].image);
        }
        for (int i = 0; i < playerIndex; i++)
        {
            persons[GlobalDatas.croom.playerStatus.Length - playerIndex + i].setUserInfo(GlobalDatas.croom.playerStatus[i].balance, GlobalDatas.croom.playerStatus[i].username);
            persons[GlobalDatas.croom.playerStatus.Length - playerIndex + i].setImage(GlobalDatas.croom.playerStatus[i].image);
        }
    }

    private void OnRoomData()
    {
        sioCom.Instance.On("round start", (string data) => 
        {
            sioCom.Instance.Emit("room status");
            GlobalDatas.isStarted = true;
        });

        sioCom.Instance.On("room status", (string data) =>
        {
            for (int i = 0; i < 6; i++)
            {
                persons[i].resetUserInfo();
            }
            Room room = JsonUtility.FromJson<Room>(data);
            GlobalDatas.croom = room;

            loadUserInfo();
            if (GlobalDatas.isStarted == true)
            {
                for (int i = 0; i < 6; i++)
                {
                    persons[i].resetGrab();
                    persons[i].resetBanker();
                    persons[i].resetAcionCard();
                    StartCoroutine(persons[i].cardClear());
                }

                GlobalDatas.isStarted = false;
                StartCoroutine(game_start_enum());
            }
            if (room.gameStatus == 2)
            {
                // reset grab
                for (int i = 0; i < 6; i++)
                {
                    persons[i].resetGrab();
                }
                 
                // set banker
                for (int i = GlobalDatas.myIndex; i < GlobalDatas.croom.players.Length; i++)
                {
                    if (GlobalDatas.croom.playerStatus[i].role == "banker")
                        persons[i - GlobalDatas.myIndex].setBanker();
                }
                for (int i = 0; i < GlobalDatas.myIndex; i++)
                {
                    if (GlobalDatas.croom.playerStatus[i].role == "banker")
                        persons[GlobalDatas.croom.playerStatus.Length - GlobalDatas.myIndex + i].setBanker();
                }
            }
            if(room.gameStatus == 5)
            {
                StartCoroutine(game_end_enum());
            }
        });

        sioCom.Instance.On("grabBank", (string data) =>
        {
            Multiple grabBank = JsonUtility.FromJson<Multiple>(data);
            for (int i = GlobalDatas.myIndex; i < GlobalDatas.croom.playerStatus.Length; i++)
            {
                if (GlobalDatas.croom.playerStatus[i].username == grabBank.player.username)
                {
                    GlobalDatas.croom.playerStatus[i].grab = grabBank.multiple;
                    persons[i - GlobalDatas.myIndex].setGrab(grabBank.multiple);
                    break;
                }
            }
            for (int i = 0; i < GlobalDatas.myIndex; i++)
            {
                if (GlobalDatas.croom.playerStatus[i].username == grabBank.player.username)
                {
                    GlobalDatas.croom.playerStatus[i].grab = grabBank.multiple;
                    persons[GlobalDatas.croom.playerStatus.Length - GlobalDatas.myIndex + i].setGrab(grabBank.multiple);
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
            for (int i = GlobalDatas.myIndex; i < GlobalDatas.croom.players.Length; i++)
            {
                if (GlobalDatas.croom.playerStatus[i].username == doubles.player.username)
                {
                    GlobalDatas.croom.playerStatus[i].grab = doubles.multiple;
                    persons[i - GlobalDatas.myIndex].setGrab(doubles.multiple);
                    break;
                }
            }
            for (int i = 0; i < GlobalDatas.myIndex; i++)
            {
                if (GlobalDatas.croom.playerStatus[i].username == doubles.player.username)
                {
                    GlobalDatas.croom.playerStatus[i].grab = doubles.multiple;
                    persons[GlobalDatas.croom.playerStatus.Length - GlobalDatas.myIndex + i].setGrab(doubles.multiple);
                    break;
                }
            }
        });

        sioCom.Instance.On("endRound", (string data) =>
        {
            sioCom.Instance.Emit("room status");
        });

        sioCom.Instance.On("outed room", (string data) =>
        {
            sioCom.Instance.Off("round start");
            sioCom.Instance.Off("room status");
            sioCom.Instance.Off("grabBank");
            sioCom.Instance.Off("endGrab");
            sioCom.Instance.Off("double");
            sioCom.Instance.Off("endRound");
            sioCom.Instance.Off("outed room");
            SceneManager.LoadScene(1);
        });

        sioCom.Instance.Emit("is ready");
        sioCom.Instance.Emit("room status");
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
        Cards.SetBool("flag", true);
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < GlobalDatas.croom.playerStatus.Length; i++)
        {
            StartCoroutine(persons[i].cardInitial());
            yield return new WaitForSeconds(0.8f);
        }
        Cards.SetBool("flag", false);
        yield return new WaitForSeconds(0.2f);

        for (int i = GlobalDatas.myIndex; i < GlobalDatas.croom.playerStatus.Length; i++)
        {
            StartCoroutine(persons[i - GlobalDatas.myIndex].setCardEnumerator(GlobalDatas.croom.playerStatus[i].cards));
        }
        for (int i = 0; i < GlobalDatas.myIndex; i++)
        {
            StartCoroutine(persons[GlobalDatas.croom.playerStatus.Length - GlobalDatas.myIndex + i].setCardEnumerator(GlobalDatas.croom.playerStatus[i].cards));
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
            StartCoroutine(persons[GlobalDatas.croom.playerStatus.Length - playerIndex + i].setCardEnumerator(GlobalDatas.croom.playerStatus[i].cards));
        }

        yield return new WaitForSeconds(0.8f);

        showEffect();
        yield return new WaitForSeconds(5f);
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

    public void outRoom()
    {
        sioCom.Instance.Emit("out room");
    }

    public void showEffect()
    {
        int bankerIndex = 0;
        float[] result = new float[6];
        for(int i = 0; i < GlobalDatas.croom.playerStatus.Length; i++)
        {
            if(GlobalDatas.croom.playerStatus[i].role == "banker")
            {
                bankerIndex = i;
                break;
            }
        }
        for (int i = 0; i < GlobalDatas.croom.playerStatus.Length; i++)
        {
            if (GlobalDatas.croom.playerStatus[i].role == "idler")
            {
                if(GlobalDatas.croom.playerStatus[bankerIndex].roundScore.score > GlobalDatas.croom.playerStatus[i].roundScore.score)
                {
                    float realMoney = GlobalDatas.croom.cost * GlobalDatas.croom.playerStatus[bankerIndex].roundScore.multiple * GlobalDatas.croom.playerStatus[i].doubles * GlobalDatas.croom.playerStatus[bankerIndex].grab;
                    result[bankerIndex] += realMoney;
                    result[i] += 0 - realMoney;
                }
                else
                {
                    float realMoney = GlobalDatas.croom.cost * GlobalDatas.croom.playerStatus[i].roundScore.multiple * GlobalDatas.croom.playerStatus[i].doubles * GlobalDatas.croom.playerStatus[bankerIndex].grab;
                    result[bankerIndex] += 0 - realMoney;
                    result[i] += realMoney;
                }
            }
        }
        for (int i = GlobalDatas.myIndex; i < GlobalDatas.croom.players.Length; i++)
        {
            if(result[i] > 0)
                persons[i - GlobalDatas.myIndex].setEarn("+" + result[i].ToString());
            else
                persons[i - GlobalDatas.myIndex].setEarn(result[i].ToString());
        }
        for (int i = 0; i < GlobalDatas.myIndex; i++)
        {
            if (result[i] > 0)
                persons[GlobalDatas.croom.playerStatus.Length - GlobalDatas.myIndex + i].setEarn("+" + result[i].ToString());
            else
                persons[GlobalDatas.croom.playerStatus.Length - GlobalDatas.myIndex + i].setEarn(result[i].ToString());
        }
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