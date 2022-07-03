using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Firesplash.UnityAssets.SocketIO;
using TMPro;

public class GameRoom : MonoBehaviour
{
    public Animator Cards, win_lose;
    public SocketIOCommunicator sioCom;
    public Sprite[] spade, heart, club, diamond;
    public Text roomNumber, bonus, createTime;
    public TMP_Text poolBalance, poolTime;
    public GameObject multiplePanel, grabPanel, timer, coin;
    public bool grabFlag;
    public Transform coinParent;

    /*------------------- -------------------*/
    public UserPerson[] persons;

    void Awake()
    {
        sioCom =
            FindObjectOfType(typeof(SocketIOCommunicator)) as
            SocketIOCommunicator;
        createTime.text = System.DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm");
    }

    // Start is called before the first frame update
    void Start()
    {
        OnRoomData();
        grabFlag = false;
    }

    private void Update()
    {
        if (GlobalDatas.croom.players.Length == 1)
        {
            for (int i = 0; i < 6; i++)
            {
                persons[i].resetLoading();
                persons[i].resetGrab();
                persons[i].resetBanker();
                persons[i].resetAcionCard();
                persons[i].resetType();
                StartCoroutine(persons[i].cardClear());  
            }
            win_lose.SetBool("win_flag", false);
            win_lose.SetBool("lose_flag", false);
        }

        grabPanel.SetActive(false);
        multiplePanel.SetActive(false);

        if (GlobalDatas.croom.gameStatus == 1 && grabFlag == true && GlobalDatas.croom.playerStatus[GlobalDatas.myIndex].onRound == true)
        {
            int userIndex = getplayerIndex();
            if (GlobalDatas.croom.playerStatus[userIndex].grab == -1)
            {
                grabPanel.SetActive(true);
            }
        }
        else if (GlobalDatas.croom.gameStatus == 2 && GlobalDatas.croom.playerStatus[GlobalDatas.myIndex].onRound == true)
        {
            int userIndex = getplayerIndex();
            if (GlobalDatas.croom.playerStatus[userIndex].doubles == -1 && GlobalDatas.croom.playerStatus[userIndex].role != "banker")
            {
                multiplePanel.SetActive(true);
            }
        }
        else
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
        roomNumber.text = GlobalDatas.croom.roomNumber.ToString();
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
                persons[i].resetLoading();
            }
            Room room = JsonUtility.FromJson<Room>(data);
            GlobalDatas.croom = room;

            loadUserInfo();
            if (GlobalDatas.isStarted == true)
            {
                win_lose.SetBool("win_flag", false);
                win_lose.SetBool("lose_flag", false);

                for (int i = 0; i < 6; i++)
                {
                    persons[i].resetGrab();
                    persons[i].resetBanker();
                    persons[i].resetAcionCard();
                    persons[i].resetType();
                    StartCoroutine(persons[i].cardClear());
                }

                if (room.players.Length >= 2)
                {
                    GlobalDatas.isStarted = false;
                    StartCoroutine(game_start_enum());
                }
            }
            if(room.gameStatus == 1)
            {
                grabFlag = false;
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
            for (int i = 0; i < 6; i++)
            {
                persons[i].resetLoading();
            }
            sioCom.Instance.Emit("room status");
            timer.GetComponent<TImetrack>().expireTime = 10f;
            timer.SetActive(true);
        });
        sioCom.Instance.On("doubles", (string data) =>
        {
            Multiple doubles = JsonUtility.FromJson<Multiple>(data);
            for (int i = GlobalDatas.myIndex; i < GlobalDatas.croom.players.Length; i++)
            {
                if (GlobalDatas.croom.playerStatus[i].username == doubles.player.username)
                {
                    GlobalDatas.croom.playerStatus[i].doubles = doubles.multiple;
                    persons[i - GlobalDatas.myIndex].setGrab(doubles.multiple);
                    break;
                }
            }
            for (int i = 0; i < GlobalDatas.myIndex; i++)
            {
                if (GlobalDatas.croom.playerStatus[i].username == doubles.player.username)
                {
                    GlobalDatas.croom.playerStatus[i].doubles = doubles.multiple;
                    persons[GlobalDatas.croom.playerStatus.Length - GlobalDatas.myIndex + i].setGrab(doubles.multiple);
                    break;
                }
            }
        });
        sioCom.Instance.On("endRound", (string data) =>
        {
            for (int i = 0; i < 6; i++)
            {
                persons[i].resetLoading();
            }
            sioCom.Instance.Emit("room status");
            timer.SetActive(false);
        });
        sioCom.Instance.On("outed room", (string data) =>
        {
            sioCom.Instance.Off("round start");
            sioCom.Instance.Off("room status");
            sioCom.Instance.Off("grabBank");
            sioCom.Instance.Off("endGrab");
            sioCom.Instance.Off("doubles");
            sioCom.Instance.Off("endRound");
            sioCom.Instance.Off("outed room");
            sioCom.Instance.Off("get pool");
            SceneManager.LoadScene(1);
        });
        sioCom.Instance.On("get pool", (string data) =>
        {
            PoolCache pool = JsonUtility.FromJson<PoolCache>(data);
            poolBalance.text = pool.balance.ToString("0.###");
            int hours = pool.time / 3600;
            int mins = (pool.time - (pool.time / 3600) * 3600) / 60;
            int sec = pool.time - ((pool.time / 3600) * 3600) - (pool.time - (pool.time / 3600) * 3600) / 60 * 60;
            string remainTime = (hours < 10 ? "0" + hours.ToString() : hours.ToString()) + " : " + (mins < 10 ? "0" + mins.ToString() : mins.ToString()) + " : " + (sec < 10 ? "0" + sec.ToString() : sec.ToString());
            poolTime.text = remainTime;
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

        timer.GetComponent<TImetrack>().expireTime = 10f;
        timer.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        for (int i = GlobalDatas.myIndex; i < GlobalDatas.croom.playerStatus.Length; i++)
        {
            if (GlobalDatas.croom.playerStatus[i].onRound == true)
            {
                StartCoroutine(persons[i - GlobalDatas.myIndex].setCardEnumerator(GlobalDatas.croom.playerStatus[i].cards));
            }
        }
        for (int i = 0; i < GlobalDatas.myIndex; i++)
        {
            if (GlobalDatas.croom.playerStatus[i].onRound == true)
            {
                StartCoroutine(persons[GlobalDatas.croom.playerStatus.Length - GlobalDatas.myIndex + i].setCardEnumerator(GlobalDatas.croom.playerStatus[i].cards));
            }
        }
        
        yield return new WaitForSeconds(0.8f);
        grabFlag = true;
    }
    private IEnumerator game_end_enum()
    {
        // all cards show
        int playerIndex = getplayerIndex();
        GlobalDatas.myIndex = playerIndex;
        for (int i = playerIndex; i < GlobalDatas.croom.playerStatus.Length; i++)
        {
            if (GlobalDatas.croom.playerStatus[i].onRound == true)
            {
                StartCoroutine(persons[i - playerIndex].setCardEnumerator(GlobalDatas.croom.playerStatus[i].cards));
            }
        }
        for (int i = 0; i < playerIndex; i++)
        {
            if (GlobalDatas.croom.playerStatus[i].onRound == true)
            {
                StartCoroutine(persons[GlobalDatas.croom.playerStatus.Length - playerIndex + i].setCardEnumerator(GlobalDatas.croom.playerStatus[i].cards));
            }
        }
        yield return new WaitForSeconds(1f);

        // Show Activate Cards
        int[] activeCards;
        for (int i = playerIndex; i < GlobalDatas.croom.playerStatus.Length; i++)
        {
            if (GlobalDatas.croom.playerStatus[i].onRound == true)
            {
                int activityCardLen = GlobalDatas.croom.playerStatus[i].roundScore.activityCards.Length;
                activeCards = new int[activityCardLen];
                for (int j = 0; j < activityCardLen; j++)
                {
                    int index = System.Array.IndexOf(GlobalDatas.croom.playerStatus[i].roundScore.cards, GlobalDatas.croom.playerStatus[i].roundScore.activityCards[j]);
                    activeCards[j] = index;
                }
                persons[i - playerIndex].actionCard(activeCards);
            }
        }
        for (int i = 0; i < playerIndex; i++)
        {
            if (GlobalDatas.croom.playerStatus[i].onRound == true)
            {
                int activityCardLen = GlobalDatas.croom.playerStatus[i].roundScore.activityCards.Length;
                activeCards = new int[activityCardLen];
                for (int j = 0; j < activityCardLen; j++)
                {
                    int index = System.Array.IndexOf(GlobalDatas.croom.playerStatus[i].roundScore.cards, GlobalDatas.croom.playerStatus[i].roundScore.activityCards[j]);
                    activeCards[j] = index;
                }
                persons[GlobalDatas.croom.playerStatus.Length - playerIndex + i].actionCard(activeCards);
            }
        }
        yield return new WaitForSeconds(1f);

        showEffect();   // earn money show
        yield return new WaitForSeconds(1.5f);
        win_lose.SetBool("win_flag", false);
        win_lose.SetBool("lose_flag", false);
        yield return new WaitForSeconds(1f);
        timer.GetComponent<TImetrack>().expireTime = 5f;
        timer.SetActive(true);
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
        // calculate earn realmoneyA
        int bankerIndex = 0;
        float[] result = new float[6];

        // calculate win score
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

        // show win logo
        if (result[GlobalDatas.myIndex] > 0 && GlobalDatas.croom.playerStatus[GlobalDatas.myIndex].onRound == true)
        {
            win_lose.SetBool("win_flag", true);
        }
        else if(result[GlobalDatas.myIndex] < 0 && GlobalDatas.croom.playerStatus[GlobalDatas.myIndex].onRound == true)
        {
            win_lose.SetBool("lose_flag", true);
        }

        // show coin transfer effect
        for (int i = bankerIndex + 1; i < GlobalDatas.croom.playerStatus.Length; i++)
        {
            if (GlobalDatas.croom.playerStatus[i].onRound == true)
            {
                if(result[bankerIndex] > result[i])
                {
                    coinEffect(persons[i].transform, persons[bankerIndex].transform);
                }
                else
                {
                    coinEffect(persons[bankerIndex].transform, persons[i].transform);
                }
            }
        }
        for (int i = 0; i < bankerIndex; i++)
        {
            if (GlobalDatas.croom.playerStatus[i].onRound == true)
            {
                if (result[bankerIndex] > result[i])
                {
                    coinEffect(persons[i].transform, persons[bankerIndex].transform);
                }
                else
                {
                    coinEffect(persons[bankerIndex].transform, persons[i].transform);
                }
            }
        }

        // show earn balance
        for (int i = GlobalDatas.myIndex; i < GlobalDatas.croom.playerStatus.Length; i++)
        {
            if (GlobalDatas.croom.playerStatus[i].onRound == true)
            {
                persons[i - GlobalDatas.myIndex].setType(GlobalDatas.croom.playerStatus[i].roundScore.type.ToString());
                if (result[i] > 0)
                    persons[i - GlobalDatas.myIndex].setEarn("+" + result[i].ToString());
                else
                    persons[i - GlobalDatas.myIndex].setEarn(result[i].ToString());
            }
        }
        for (int i = 0; i < GlobalDatas.myIndex; i++)
        {
            if (GlobalDatas.croom.playerStatus[i].onRound == true)
            {
                persons[GlobalDatas.croom.playerStatus.Length - GlobalDatas.myIndex + i].setType(GlobalDatas.croom.playerStatus[i].roundScore.type.ToString());
                if (result[i] > 0)
                    persons[GlobalDatas.croom.playerStatus.Length - GlobalDatas.myIndex + i].setEarn("+" + result[i].ToString());
                else
                    persons[GlobalDatas.croom.playerStatus.Length - GlobalDatas.myIndex + i].setEarn(result[i].ToString());
            }
        }
    }

    public void coinEffect(Transform startPosition, Transform endPosition)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject newCoin = Instantiate(coin, new Vector2(startPosition.position.x, startPosition.position.y), Quaternion.identity);
            newCoin.transform.SetParent(coinParent.transform);
            newCoin.transform.localScale = new Vector3(1f, 1f, 1f);
            StartCoroutine(coinTransfer(newCoin, endPosition));
        }
    }

    private IEnumerator coinTransfer(GameObject newCoin, Transform endPosition)
    {
        float duration = 5f, timeElapsed = 0f;
        float randomx = Random.Range(-0.3f, 0.3f);
        float randomy = Random.Range(-0.3f, 0.3f);
        float randomTime = Random.Range(0.1f, 0.8f);
        Vector2 lastPosition = new Vector2(0f, 0f);

        yield return new WaitForSeconds(randomTime);

        while (timeElapsed < duration)
        {
            lastPosition = new Vector2(endPosition.position.x + randomx, endPosition.position.y + randomy);
            newCoin.transform.position = Vector2.Lerp(newCoin.transform.position, lastPosition, timeElapsed);
            timeElapsed += Time.deltaTime / 25;
            yield return null;
        }
        newCoin.transform.position = lastPosition;
        yield return new WaitForSeconds(1f);

        Destroy(newCoin);
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

public class PoolCache
{
    public float balance;
    public int time;

    public PoolCache(float _balance, int _time = 0)
    {
        this.balance = _balance;
        this.time = _time;
    }
}