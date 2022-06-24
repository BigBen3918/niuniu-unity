using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserPerson : MonoBehaviour
{
    public Text username, balance, multiple;
    public Transform userImageObject, cardsObject;
    public Transform bankerMark;
    public RawImage image;
    public Sprite[] spade, heart, club, diamond;
    public Sprite back;
    public Texture userBack;
    public Animator cardAnimator;

    private void Start()
    {
        username = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        balance = transform.GetChild(0).GetChild(1).GetComponent<Text>();
        multiple = transform.GetChild(3).GetChild(0).GetComponent<Text>();
        userImageObject = transform.GetChild(1).GetChild(0);
        cardsObject = transform.GetChild(2);
        bankerMark = transform.GetChild(3).GetChild(1);
        cardAnimator = transform.GetChild(2).GetComponent<Animator>();
        image = transform.GetChild(1).GetChild(0).GetComponent<RawImage>();

        GameRoom roomManager = GameObject.Find("RoomManager").GetComponent<GameRoom>();
        spade = roomManager.spade;
        heart = roomManager.heart;
        club = roomManager.club;
        diamond = roomManager.diamond;
    }
    // before game
    public void setUserInfo(float _balance, string _username)
    {
        username.text = _balance.ToString();
        balance.text = _username;
    }
    public void resetUserInfo()
    {
        username.text = "点击坐下";
        balance.text = "点击坐下";
        image.texture = userBack;
    }

    // game actions
    public void setGrab(int i)
    {
        if (i == 0) multiple.text = "不抢";
        else multiple.text = i.ToString() + "x";
    }
    public void resetGrab()
    {
        multiple.text = "";
    }
    public void setBanker()
    {
        bankerMark.gameObject.SetActive(true);
    }
    public void resetBanker()
    {
        bankerMark.gameObject.SetActive(false);
    }
    public void setEarn(string earn)
    {
        multiple.text = earn;
    }
    public void setImage(string url)
    {
        StartCoroutine(ExtensionMethods.GetTextureFromURL(url, (Texture2D coverImage, bool isSuccess) =>
        {
            if (!isSuccess) return;
            image.texture = coverImage;
        }));
    }
    public void resetImage()
    {
        image.texture = userBack;
    }
    // activate cards
    public void actionCard(int[] arr)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            cardsObject.GetChild(arr[i]).GetComponent<Image>().color = new Color32(255, 97, 113, 255);
        }
    }

    public void resetAcionCard()
    {
        for (int i = 0; i < 5; i++)
        {
            cardsObject.GetChild(i).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    // Enumerator Controller
    public IEnumerator cardInitial()
    {
        cardAnimator.SetBool("flag", true);
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator setCardEnumerator(int[] _cards)
    {
        if(_cards.Length > 0)
            cardAnimator.SetBool("rotate_flag", true);
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < _cards.Length; i++)
        {
            if (GlobalDatas.croom.gameStatus == 5)
            {
                Debug.Log(i + " ===== " + _cards[i]);
            }
            switch (_cards[i] / 10)
            {
                case 0:     // diamond card
                    transform.GetChild(2).GetChild(i).GetComponent<Image>().sprite = diamond[_cards[i] % 10 - 1];
                    break;
                case 1:     // club card
                    transform.GetChild(2).GetChild(i).GetComponent<Image>().sprite = club[_cards[i] % 10 - 1];
                    break;
                case 2:     // heart card
                    transform.GetChild(2).GetChild(i).GetComponent<Image>().sprite = heart[_cards[i] % 10 - 1];
                    break;
                case 3:     // spade card
                    transform.GetChild(2).GetChild(i).GetComponent<Image>().sprite = spade[_cards[i] % 10 - 1];
                    break;
            }
        }
    }
    public IEnumerator cardClear()
    {
        cardAnimator.SetBool("rotate_flag", false);
        cardAnimator.SetBool("flag", false);
        for (int i = 0; i < 5; i++)
        {
            transform.GetChild(2).GetChild(i).GetComponent<Image>().sprite = back;
        }
        yield return new WaitForSeconds(0.5f);
    }
}

