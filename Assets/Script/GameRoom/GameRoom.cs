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
    public List<int> cards;

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

    ~GameRoom()
    {
        sioCom.Instance.Emit("out room");
    }

    // set robby list data
    private void OnRoomData()
    {
        sioCom.Instance.On("start game", (string data) => 
        {
            JSONNode reqData = JSON.Parse(data);
            Debug.Log(reqData["cards"]);
            for (int i = 0; i < reqData["cards"].Count; i++)
            {
                cards.Add(reqData["cards"][i]);
            }
            int play_count = reqData["players"];
            game_start(cards, play_count);
        });
        sioCom.Instance.On("room status", (string data) =>
        {
            // game status
        });
        sioCom.Instance.On("room update", (string data) =>
        { 
            // game update such as new round, call or bet.. 
        });
        sioCom.Instance.On("outed room", (string data) =>
        {
            SceneManager.LoadScene(1);
            sioCom.Instance.Off("start game");
            sioCom.Instance.Off("room status");
            sioCom.Instance.Off("room update");
            sioCom.Instance.Off("outed room");
        });

        sioCom.Instance.Emit("get ready");
    }

    public void outRoom()
    {
        sioCom.Instance.Emit("out room");
    }

    public void bet(int index)
    {
        
        //sioCom.Instance.Emit("bet");
    }

    // UI Management
    public void game_start(List<int> arr, int players)
    {
        StartCoroutine(card_init());
        StartCoroutine(card_anim_start(arr, players));
    }

    private IEnumerator card_anim_start(List<int> arr, int param)
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
        yield return new WaitForSeconds(1f);
        StartCoroutine(card_anim_open(arr));
    }

    private IEnumerator card_anim_open(List<int> arr)
    {
        for (int i = 0; i < arr.Count; i++)
        {
            Transform child = card_persons[0].GetChild(i);
            switch(arr[i] / 10)
            {
                case 0:     // spade card
                    child.gameObject.GetComponent<Image>().sprite = spade[arr[i] % 10 -1];
                    break;
                case 1:     // heart card
                    child.gameObject.GetComponent<Image>().sprite = heart[arr[i] % 10 - 1];
                    break;
                case 2:     // club card
                    child.gameObject.GetComponent<Image>().sprite = club[arr[i] % 10 - 1];
                    break;
                case 3:     // diamond card
                    child.gameObject.GetComponent<Image>().sprite = diamond[arr[i] % 10 - 1];
                    break;
            }
        }
        yield return new WaitForSeconds(0.2f);
        sioCom.Instance.Emit("ready on");
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
}
