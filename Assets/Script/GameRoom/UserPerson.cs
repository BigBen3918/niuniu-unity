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
    public Sprite[] spade, heart, club, diamond, types;
    public Sprite back;
    public Texture userBack;
    public Animator cardAnimator, typeAnimator;

    private void Start()
    {
        username = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        balance = transform.GetChild(0).GetChild(1).GetComponent<Text>();
        multiple = transform.GetChild(3).GetChild(0).GetComponent<Text>();
        userImageObject = transform.GetChild(1).GetChild(0);
        cardsObject = transform.GetChild(2);
        bankerMark = transform.GetChild(3).GetChild(1);
        cardAnimator = transform.GetChild(2).GetComponent<Animator>();
        typeAnimator = transform.GetChild(3).GetComponent<Animator>();
        image = transform.GetChild(1).GetChild(0).GetComponent<RawImage>();

        GameRoom roomManager = GameObject.Find("RoomManager").GetComponent<GameRoom>();
        spade = roomManager.spade;
        heart = roomManager.heart;
        club = roomManager.club;
        diamond = roomManager.diamond;
    }
    // userinfos actions
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

    // grab actions
    public void setGrab(int i)
    {
        if (i == 0) multiple.text = "不抢";
        else multiple.text = i.ToString() + "x";
    }
    public void resetGrab()
    {
        multiple.text = "";
    }

    // banker mark actions
    public void setBanker()
    {
        bankerMark.gameObject.SetActive(true);
    }
    public void resetBanker()
    {
        bankerMark.gameObject.SetActive(false);
    }

    // earn money actions
    public void setEarn(string earn)
    {
        multiple.text = earn;
    }

    // card images actions
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

    // type show actions
    public void setType(string type)
    {
        Transform effect = transform.GetChild(3).GetChild(2);
        effect.GetChild(0).gameObject.SetActive(true);
        effect.GetChild(1).gameObject.SetActive(true);
        effect.GetChild(2).gameObject.SetActive(true);
        effect.GetChild(3).gameObject.SetActive(true);
        effect.GetChild(4).gameObject.SetActive(true);
        effect.GetChild(5).gameObject.SetActive(true);
        switch (type)
        {
            case "NoBull":
                effect.GetChild(0).gameObject.SetActive(false);
                effect.GetChild(1).gameObject.SetActive(false);
                effect.GetChild(2).GetComponent<Image>().sprite = types[27];
                effect.GetChild(3).GetComponent<Image>().sprite = types[28];
                effect.GetChild(4).gameObject.SetActive(false);
                effect.GetChild(5).gameObject.SetActive(false);
                break;
            case "Cattle1":
                effect.GetChild(0).gameObject.SetActive(false);
                effect.GetChild(1).GetComponent<Image>().sprite = types[26];
                effect.GetChild(2).GetComponent<Image>().sprite = types[10];
                effect.GetChild(3).gameObject.SetActive(false);
                effect.GetChild(4).gameObject.SetActive(true);
                effect.GetChild(5).GetComponent<Image>().sprite = types[1];
                break;
            case "Cattle2":
                effect.GetChild(0).gameObject.SetActive(false);
                effect.GetChild(1).GetComponent<Image>().sprite = types[26];
                effect.GetChild(2).GetComponent<Image>().sprite = types[11];
                effect.GetChild(3).gameObject.SetActive(false);
                effect.GetChild(4).gameObject.SetActive(true);
                effect.GetChild(5).GetComponent<Image>().sprite = types[1];
                break;
            case "Cattle3":
                effect.GetChild(0).gameObject.SetActive(false);
                effect.GetChild(1).GetComponent<Image>().sprite = types[26];
                effect.GetChild(2).GetComponent<Image>().sprite = types[12];
                effect.GetChild(3).gameObject.SetActive(false);
                effect.GetChild(4).gameObject.SetActive(true);
                effect.GetChild(5).GetComponent<Image>().sprite = types[1];
                break;
            case "Cattle4":
                effect.GetChild(0).gameObject.SetActive(false);
                effect.GetChild(1).GetComponent<Image>().sprite = types[26];
                effect.GetChild(2).GetComponent<Image>().sprite = types[13];
                effect.GetChild(3).gameObject.SetActive(false);
                effect.GetChild(4).gameObject.SetActive(true);
                effect.GetChild(5).GetComponent<Image>().sprite = types[1];
                break;
            case "Cattle5":
                effect.GetChild(0).gameObject.SetActive(false);
                effect.GetChild(1).GetComponent<Image>().sprite = types[26];
                effect.GetChild(2).GetComponent<Image>().sprite = types[14];
                effect.GetChild(3).gameObject.SetActive(false);
                effect.GetChild(4).gameObject.SetActive(true);
                effect.GetChild(5).GetComponent<Image>().sprite = types[1];
                break;
            case "Cattle6":
                effect.GetChild(0).gameObject.SetActive(false);
                effect.GetChild(1).GetComponent<Image>().sprite = types[26];
                effect.GetChild(2).GetComponent<Image>().sprite = types[15];
                effect.GetChild(3).gameObject.SetActive(false);
                effect.GetChild(4).gameObject.SetActive(true);
                effect.GetChild(5).GetComponent<Image>().sprite = types[1];
                break;
            case "Cattle7":
                effect.GetChild(0).gameObject.SetActive(false);
                effect.GetChild(1).GetComponent<Image>().sprite = types[26];
                effect.GetChild(2).GetComponent<Image>().sprite = types[16];
                effect.GetChild(3).gameObject.SetActive(false);
                effect.GetChild(4).gameObject.SetActive(true);
                effect.GetChild(5).GetComponent<Image>().sprite = types[2];
                break;
            case "Cattle8":
                effect.GetChild(0).gameObject.SetActive(false);
                effect.GetChild(1).GetComponent<Image>().sprite = types[26];
                effect.GetChild(2).GetComponent<Image>().sprite = types[17];
                effect.GetChild(3).gameObject.SetActive(false);
                effect.GetChild(4).gameObject.SetActive(true);
                effect.GetChild(5).GetComponent<Image>().sprite = types[2];
                break;
            case "Cattle9":
                effect.GetChild(0).gameObject.SetActive(false);
                effect.GetChild(1).GetComponent<Image>().sprite = types[26];
                effect.GetChild(2).GetComponent<Image>().sprite = types[18];
                effect.GetChild(3).gameObject.SetActive(false);
                effect.GetChild(4).gameObject.SetActive(true);
                effect.GetChild(5).GetComponent<Image>().sprite = types[3];
                break;
            case "NiuNiu":
                effect.GetChild(0).gameObject.SetActive(false);
                effect.GetChild(1).gameObject.SetActive(false);
                effect.GetChild(2).GetComponent<Image>().sprite = types[26];
                effect.GetChild(3).GetComponent<Image>().sprite = types[26];
                effect.GetChild(4).gameObject.SetActive(true);
                effect.GetChild(5).GetComponent<Image>().sprite = types[4];
                break;
            case "GoldBull":
                effect.GetChild(0).GetComponent<Image>().sprite = types[29];
                effect.GetChild(1).GetComponent<Image>().sprite = types[30];
                effect.GetChild(2).GetComponent<Image>().sprite = types[26];
                effect.GetChild(3).gameObject.SetActive(false);
                effect.GetChild(4).gameObject.SetActive(true);
                effect.GetChild(5).GetComponent<Image>().sprite = types[4];
                break;
            case "GoldBullion":
                effect.GetChild(0).GetComponent<Image>().sprite = types[29];
                effect.GetChild(1).GetComponent<Image>().sprite = types[30];
                effect.GetChild(2).GetComponent<Image>().sprite = types[26];
                effect.GetChild(3).GetComponent<Image>().sprite = types[26];
                effect.GetChild(4).gameObject.SetActive(true);
                effect.GetChild(5).GetComponent<Image>().sprite = types[4];
                break;
            case "Straight":
                effect.GetChild(0).GetComponent<Image>().sprite = types[23];
                effect.GetChild(1).GetComponent<Image>().sprite = types[25];
                effect.GetChild(2).GetComponent<Image>().sprite = types[26];
                effect.GetChild(3).gameObject.SetActive(false);
                effect.GetChild(4).gameObject.SetActive(true);
                effect.GetChild(5).GetComponent<Image>().sprite = types[5];
                break;
            case "FullHouse":
                effect.GetChild(0).GetComponent<Image>().sprite = types[22];
                effect.GetChild(1).GetComponent<Image>().sprite = types[21];
                effect.GetChild(2).GetComponent<Image>().sprite = types[26];
                effect.GetChild(3).gameObject.SetActive(false);
                effect.GetChild(4).gameObject.SetActive(true);
                effect.GetChild(5).GetComponent<Image>().sprite = types[6];
                break;
            case "TenSmall":
                effect.GetChild(0).gameObject.SetActive(false);
                effect.GetChild(1).GetComponent<Image>().sprite = types[19];
                effect.GetChild(2).GetComponent<Image>().sprite = types[31];
                effect.GetChild(3).gameObject.SetActive(false);
                effect.GetChild(4).gameObject.SetActive(true);
                effect.GetChild(5).GetComponent<Image>().sprite = types[7];
                break;
            case "Forty":
                effect.GetChild(0).gameObject.SetActive(false);
                effect.GetChild(1).GetComponent<Image>().sprite = types[13];
                effect.GetChild(2).GetComponent<Image>().sprite = types[26];
                effect.GetChild(3).gameObject.SetActive(false);
                effect.GetChild(4).gameObject.SetActive(true);
                effect.GetChild(5).GetComponent<Image>().sprite = types[7];
                break;
            case "BombBull":
                effect.GetChild(0).gameObject.SetActive(false);
                effect.GetChild(1).GetComponent<Image>().sprite = types[26];
                effect.GetChild(2).GetComponent<Image>().sprite = types[19];
                effect.GetChild(3).gameObject.SetActive(false);
                effect.GetChild(4).gameObject.SetActive(true);
                effect.GetChild(5).GetComponent<Image>().sprite = types[8];
                break;
            default:
                break;
        }
        typeAnimator.SetBool("flag", true);
        effect.gameObject.SetActive(true);
    }
    public void resetType()
    {
        transform.GetChild(3).GetChild(2).gameObject.SetActive(false);
        typeAnimator.SetBool("flag", false);
    }

    // activate cards actions
    public void actionCard(int[] arr)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            if (arr[i] != 5)
            {
                cardsObject.GetChild(arr[i]).GetComponent<Image>().color = new Color32(255, 100, 0, 255);
            }
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

