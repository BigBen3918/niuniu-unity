using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Firesplash.UnityAssets.SocketIO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SimpleJSON;

public class GameRoom : MonoBehaviour
{
    public Animator Cards, person1, person2, person3, person4, person5, person6;
    public SocketIOCommunicator sioCom;
    public Transform[] card_persons;
    public Sprite[] spade, heart, club, diamond;
    public Text roomNumber, bonus;
    public GameObject betButton;
    public GameObject[] userinfos;
    public GameObject[] effects;

    void Awake()
    {
        sioCom =
            FindObjectOfType(typeof(SocketIOCommunicator)) as
            SocketIOCommunicator;
    }

    // Start is called before the first frame update
    void Start()
    {
        loadUesrInfo();
        OnRoomData();
    }

    ~GameRoom()
    {
        sioCom.Instance.Emit("out room");
    }

    // set robby list data
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
            if (room.roller == 0 && room.gameStatus == 1)
            {
                game_start(room);
            }
            else if(room.roller == 0 && room.gameStatus == 2)
            {
                checkRoll();
            }
            loadUesrInfo();
        });
        sioCom.Instance.On("grabBank", (string data) =>
        {
            Multiple grabBank = JsonUtility.FromJson<Multiple>(data);
            for (int i = 0; i < GlobalDatas.croom.players.Length; i++)
            {
                if (GlobalDatas.croom.players[i].username == grabBank.player.username)
                {
                    GlobalDatas.croom.players[i].grab = grabBank.multiple;
                    break;
                }
            }
            showMultiple(1);
            sioCom.Instance.Emit("room status");
        });
        sioCom.Instance.On("endGrab", (string data) =>
        {
            StartCoroutine(endGrab(1));
        });
        sioCom.Instance.On("double", (string data) =>
        {
            Multiple doubles = JsonUtility.FromJson<Multiple>(data);
            for (int i = 0; i < GlobalDatas.croom.players.Length; i++)
            {
                if (GlobalDatas.croom.players[i].username == doubles.player.username)
                {
                    GlobalDatas.croom.players[i].doubles = doubles.multiple;
                    break;
                }
            }
            showMultiple(2);
            sioCom.Instance.Emit("room status");
        });
        sioCom.Instance.On("endRound", (string data) =>
        {
            StartCoroutine(endGrab(2));
        });
        sioCom.Instance.On("outed room", (string data) =>
        {
            SceneManager.LoadScene(1);
            sioCom.Instance.Off("start game");
            sioCom.Instance.Off("room status");
            sioCom.Instance.Off("room update");
            sioCom.Instance.Off("outed room");
        });

        sioCom.Instance.Emit("is ready");
    }

    public void outRoom()
    {
        sioCom.Instance.Emit("out room");
    }

    public void checkRoll()
    {
        if(GlobalDatas.croom.playerStatus[GlobalDatas.myIndex].roll != "banker")
        {
            betButton.SetActive(true);
        }
    }

    public void bet(int multiple)
    {
        Grab grab = new Grab(multiple);
        if (GlobalDatas.croom.gameStatus == 1)
        {
            sioCom.Instance.Emit("grab bank", JsonUtility.ToJson(grab), false);
            betButton.SetActive(false);
        }
        else if(GlobalDatas.croom.gameStatus == 2)
        {
            sioCom.Instance.Emit("double", JsonUtility.ToJson(grab), false);
            betButton.SetActive(false);
        }
    }

    public int getplayerIndex(int len)
    {
        int count = 0;
        for (int i = 0; i < len; i++)
        {
            if (GlobalDatas.croom.players[i].username == GlobalDatas.authdata.username)
            {
                count = i;
                break;
            }
        }
        return count;
    }

    public void showResult()
    {
        int playerIndex = GlobalDatas.myIndex;
        for (int i = playerIndex; i < GlobalDatas.croom.players.Length; i++)
        {
            for(int j = 0; j < GlobalDatas.croom.playerStatus[i].cards.Length; j++)
            {
                Transform child = card_persons[i].GetChild(j);
                switch (GlobalDatas.croom.playerStatus[i].cards[j] / 10)
                {
                    case 0:     // diamond card
                        child.gameObject.GetComponent<Image>().sprite = diamond[GlobalDatas.croom.playerStatus[i].cards[j] % 10 - 1];
                        break;
                    case 1:     // club card
                        child.gameObject.GetComponent<Image>().sprite = club[GlobalDatas.croom.playerStatus[i].cards[j] % 10 - 1];
                        break;
                    case 2:     // heart card
                        child.gameObject.GetComponent<Image>().sprite = heart[GlobalDatas.croom.playerStatus[i].cards[j] % 10 - 1];
                        break;
                    case 3:     // spade card
                        child.gameObject.GetComponent<Image>().sprite = spade[GlobalDatas.croom.playerStatus[i].cards[j] % 10 - 1];
                        break;
                }
            }
        }
        for(int i = 0; i < playerIndex; i++)
        {
            for (int j = 0; j < GlobalDatas.croom.playerStatus[i].cards.Length; j++)
            {
                Transform child = card_persons[i].GetChild(j);
                switch (GlobalDatas.croom.playerStatus[i].cards[j] / 10)
                {
                    case 0:     // diamond card
                        child.gameObject.GetComponent<Image>().sprite = diamond[GlobalDatas.croom.playerStatus[i].cards[j] % 10 - 1];
                        break;
                    case 1:     // club card
                        child.gameObject.GetComponent<Image>().sprite = club[GlobalDatas.croom.playerStatus[i].cards[j] % 10 - 1];
                        break;
                    case 2:     // heart card
                        child.gameObject.GetComponent<Image>().sprite = heart[GlobalDatas.croom.playerStatus[i].cards[j] % 10 - 1];
                        break;
                    case 3:     // spade card
                        child.gameObject.GetComponent<Image>().sprite = spade[GlobalDatas.croom.playerStatus[i].cards[j] % 10 - 1];
                        break;
                }
            }
        }
    }

    // UI Management
    private void showMultiple(int flag)
    {
        int playerIndex = GlobalDatas.myIndex;
        for (int i = playerIndex; i < GlobalDatas.croom.players.Length; i++)
        {
            if (flag == 1)
            {
                if (GlobalDatas.croom.players[i].grab > 0 && GlobalDatas.croom.players[i].grab < 5)
                {
                    string txt = "x" + GlobalDatas.croom.players[i].grab.ToString();
                    effects[i - playerIndex].transform.GetChild(0).gameObject.GetComponent<Text>().text = txt;
                    effects[i - playerIndex].transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (GlobalDatas.croom.players[i].grab == 0)
                {
                    string txt = "不抢";
                    effects[i - playerIndex].transform.GetChild(0).gameObject.GetComponent<Text>().text = txt;
                    effects[i - playerIndex].transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    effects[i - playerIndex].transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            else
            {
                if (GlobalDatas.croom.players[i].doubles > 0 && GlobalDatas.croom.players[i].doubles < 5)
                {
                    string txt = "x" + GlobalDatas.croom.players[i].doubles.ToString();
                    effects[i - playerIndex].transform.GetChild(0).gameObject.GetComponent<Text>().text = txt;
                    effects[i - playerIndex].transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (GlobalDatas.croom.players[i].doubles == 0)
                {
                    string txt = "不抢";
                    effects[i - playerIndex].transform.GetChild(0).gameObject.GetComponent<Text>().text = txt;
                    effects[i - playerIndex].transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    effects[i - playerIndex].transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
        for (int i = 0; i < playerIndex; i++)
        {
            if (flag == 1)
            {
                if (GlobalDatas.croom.players[i].grab > 0 && GlobalDatas.croom.players[i].grab < 5)
                {
                    string txt = "x" + GlobalDatas.croom.players[i].grab.ToString();
                    effects[i - playerIndex].transform.GetChild(0).gameObject.GetComponent<Text>().text = txt;
                    effects[i - playerIndex].transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (GlobalDatas.croom.players[i].grab == 0)
                {
                    string txt = "不抢";
                    effects[i - playerIndex].transform.GetChild(0).gameObject.GetComponent<Text>().text = txt;
                    effects[i - playerIndex].transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    effects[i - playerIndex].transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            else
            {
                if (GlobalDatas.croom.players[i].doubles > 0 && GlobalDatas.croom.players[i].doubles < 5)
                {
                    string txt = "x" + GlobalDatas.croom.players[i].doubles.ToString();
                    effects[i - playerIndex].transform.GetChild(0).gameObject.GetComponent<Text>().text = txt;
                    effects[i - playerIndex].transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (GlobalDatas.croom.players[i].doubles == 0)
                {
                    string txt = "不抢";
                    effects[i - playerIndex].transform.GetChild(0).gameObject.GetComponent<Text>().text = txt;
                    effects[i - playerIndex].transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    effects[i - playerIndex].transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }

    private void loadUesrInfo()
    {
        bonus.text = GlobalDatas.croom.cost.ToString();
        roomNumber.text = GlobalDatas.croom.id.ToString();
        int playerIndex = getplayerIndex(GlobalDatas.croom.players.Length);
        for (int i = playerIndex; i < GlobalDatas.croom.players.Length; i++)
        {
            userinfos[i - playerIndex].transform.GetChild(0).gameObject.GetComponent<Text>().text = GlobalDatas.croom.players[i].score;
            userinfos[i - playerIndex].transform.GetChild(1).gameObject.GetComponent<Text>().text = GlobalDatas.croom.players[i].username;
        }
        for (int i = 0; i < playerIndex; i++)
        {
            userinfos[playerIndex + i].transform.GetChild(0).gameObject.GetComponent<Text>().text = GlobalDatas.croom.players[i].score;
            userinfos[playerIndex + i].transform.GetChild(1).gameObject.GetComponent<Text>().text = GlobalDatas.croom.players[i].username;
        }
    }

    public void game_start(Room room)
    {
        int playerIndex = getplayerIndex(room.players.Length);
        GlobalDatas.myIndex = playerIndex;

        StartCoroutine(card_init());
        StartCoroutine(card_anim_start(room.playerStatus[GlobalDatas.myIndex].cards, room.players.Length));
    }

    private IEnumerator card_anim_start(int[] arr, int param)
    {
        Cards.SetBool("flag", true);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < param; i++)
        {
            switch (i)
            {
                case 0:
                    person1.SetBool("flag", true);
                    yield return new WaitForSeconds(0.8f);            
                    break;
                case 1:
                    person3.SetBool("flag", true);
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 2:
                    person5.SetBool("flag", true);
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 3:
                    person6.SetBool("flag", true);
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 4:
                    person2.SetBool("flag", true);
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 5:
                    person4.SetBool("flag", true);
                    yield return new WaitForSeconds(0.8f);
                    break;
            }
        }
        yield return new WaitForSeconds(0.2f);

        Cards.SetBool("flag", false);
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(card_anim_rotate());
        yield return new WaitForSeconds(0.25f);

        StartCoroutine(card_anim_open(arr));
    }

    private IEnumerator card_anim_rotate()
    {
        person1.SetBool("rotate_flag", true);
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator card_anim_open(int[] arr)
    {
        for (int i = 0; i < arr.Length ; i++)
        {
            Transform child = card_persons[0].GetChild(i);
            switch(arr[i] / 10)
            {
                case 0:     // diamond card
                    child.gameObject.GetComponent<Image>().sprite = diamond[arr[i] % 10 - 1];
                    break;
                case 1:     // club card
                    child.gameObject.GetComponent<Image>().sprite = club[arr[i] % 10 - 1];
                    break;
                case 2:     // heart card
                    child.gameObject.GetComponent<Image>().sprite = heart[arr[i] % 10 - 1];
                    break;
                case 3:     // spade card
                    child.gameObject.GetComponent<Image>().sprite = spade[arr[i] % 10 - 1];
                    break;
            }
        }
        yield return new WaitForSeconds(0.2f);
        checkRoll();
    }

    private IEnumerator card_init()
    {
        person1.SetBool("flag", false);
        person2.SetBool("flag", false);
        person3.SetBool("flag", false);
        person4.SetBool("flag", false);
        person5.SetBool("flag", false);
        person6.SetBool("flag", false);
        Cards.SetBool("flag", false);
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator endGrab(int flag)
    {
        yield return new WaitForSeconds(1.25f);
        for (int i = 0; i < GlobalDatas.croom.players.Length; i++)
        {
            if (flag == 1)
            {
                GlobalDatas.croom.players[i].grab = 5;
            }
            else
            {
                GlobalDatas.croom.players[i].grab = 5;
            }
            effects[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
            effects[i].transform.GetChild(0).gameObject.SetActive(false);
        }

        if (flag == 1)
        {
            yield return new WaitForSeconds(0.25f);
            for (int i = 0; i < GlobalDatas.croom.players.Length; i++)
            {
                if (GlobalDatas.croom.players[i].roll == "banker")
                {
                    effects[i].transform.GetChild(1).gameObject.SetActive(true);
                    break;
                }
            }
        }

        yield return new WaitForSeconds(0.5f);
        sioCom.Instance.Emit("room status");

        if (flag == 2)
        {
            yield return new WaitForSeconds(1.5f);
            showResult();
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