using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TImetrack : MonoBehaviour
{
    public Text explain, second;
    public int flag = 0;
    public float expireTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (expireTime > 0)
        {
            expireTime -= Time.deltaTime;
        }
        else
        {
            expireTime = 0;
            gameObject.SetActive(false);
        }

        second.text = ((int)expireTime).ToString();
        switch (GlobalDatas.croom.gameStatus)
        {
            case 1:
                explain.text = "清选择抢庄倍数 :";
                break;
            case 2:
                explain.text = "等待其他玩家亮牌 :";
                break;
            case 5:
                explain.text = "下局开始剩余 :";
                break;
            default:
                gameObject.SetActive(false);
                break;
        }
    }
}
