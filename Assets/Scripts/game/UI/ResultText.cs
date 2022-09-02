using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultText : MonoBehaviour
{
    Animator animator;
    public RawImage[] letters;
    public RawImage[] numbers;

    public bool filped;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Reset();
    }

    private void Awake() {

    }

    public void Reset(){
        HideAnimation();
        filped = false;
    }

    void HideAnimation(){
        if(animator == null){
            animator = GetComponent<Animator>();
        }
        animator.SetBool("Hide", true);
        animator.SetBool("Letters", false);
        animator.SetBool("NoCastle", false);
    }

    void LettersAnimation(){
        animator.SetBool("Hide", false);
        animator.SetBool("Letters", true);
        animator.SetBool("NoCastle", false);
    }

    void NoCastleAnimaiton(){
        animator.SetBool("Hide", false);
        animator.SetBool("Letters", false);
        animator.SetBool("NoCastle", true);
        filped = true;
        
    }

    IEnumerator SetTexture(string[] imgNames, string num = "0"){
        filped = true;
        yield return new WaitForSeconds(0.2f);
        for(int i = 0; i < 4; i++){
            if(i >= imgNames.Length || imgNames[i] == null || UIRefer.instance.GetImage(imgNames[i]) == null){
                letters[i].gameObject.SetActive(false);
            }else{
                letters[i].gameObject.SetActive(true);
                letters[i].texture = UIRefer.instance.GetImage(imgNames[i]);
            }
        }

        if(num == "0" || UIRefer.instance.GetImage(num) == null){
            numbers[0].gameObject.SetActive(false);
            numbers[1].gameObject.SetActive(false);
        }else{
            numbers[0].gameObject.SetActive(true);
            numbers[1].gameObject.SetActive(true);
            numbers[1].texture = UIRefer.instance.GetImage(num);
        }
        LettersAnimation();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPaeResult(int result, int num){
        if(result == -1 || num == -1) return;
        if(filped) return;
        HideAnimation();
        switch (result)
        {
            // 无牛			牌型：五张牌中，任意3张牌的点数之和都不为10的整数倍
            case (int)JUDGETYPE.None:
                NoCastleAnimaiton();
                AudioManager.instance.ShotOneAudio("none");
                // return;
                break;
            // 牛1 			有牛牌型：5张牌中有3张牌的点数之和为10的整数倍，另外2张牌的点数之和mod（10）等于几，即为牛几
            case (int)JUDGETYPE.Cattle_1:
                StartCoroutine(SetTexture(new string[]{"castle", "one"}, num + ""));
                AudioManager.instance.ShotOneAudio("castle_1");
            break;
            // 牛2
            case (int)JUDGETYPE.Cattle_2:
                StartCoroutine(SetTexture(new string[]{"castle", "two"}, num + ""));
                AudioManager.instance.ShotOneAudio("castle_2");
            break;
            // 牛3
            case (int)JUDGETYPE.Cattle_3:
                StartCoroutine(SetTexture(new string[]{"castle", "three"}, num + ""));
                AudioManager.instance.ShotOneAudio("castle_3");
            break;
            // 牛4
            case (int)JUDGETYPE.Cattle_4:
                StartCoroutine(SetTexture(new string[]{"castle", "four"}, num + ""));
                AudioManager.instance.ShotOneAudio("castle_4");
            break;
            // 牛5
            case (int)JUDGETYPE.Cattle_5:
                StartCoroutine(SetTexture(new string[]{"castle", "five"}, num + ""));
                AudioManager.instance.ShotOneAudio("castle_5");
            break;
            // 牛6
            case (int)JUDGETYPE.Cattle_6:
                StartCoroutine(SetTexture(new string[]{"castle", "six"}, num + ""));
                AudioManager.instance.ShotOneAudio("castle_6");
            break;
            // 牛7
            case (int)JUDGETYPE.Cattle_7:
                StartCoroutine(SetTexture(new string[]{"castle", "seven"}, num + ""));
                AudioManager.instance.ShotOneAudio("castle_7");
            break;
            // 牛8
            case (int)JUDGETYPE.Cattle_8:
                StartCoroutine(SetTexture(new string[]{"castle", "eight"}, num + ""));
                AudioManager.instance.ShotOneAudio("castle_8");
                
            break;
            // 牛9
            case (int)JUDGETYPE.Cattle_9:
                StartCoroutine(SetTexture(new string[]{"castle", "nine"}, num + ""));
                AudioManager.instance.ShotOneAudio("castle_9");
            break;
            // 牛牛  		牌型：5张牌中有3张牌的点数之和为10的整数倍，并且，另外2张牌的点数之和为10的整数倍
            case (int)JUDGETYPE.Double:
                StartCoroutine(SetTexture(new string[]{"castle", "castle"}, num + ""));  
                AudioManager.instance.ShotOneAudio("double");
            break;
            // 金牌牛		5张牌中有3张一样
            case (int)JUDGETYPE.Gold_1:
                StartCoroutine(SetTexture(new string[]{"gold", "pai", "castle", "one"}, num + ""));
                AudioManager.instance.ShotOneAudio("gold_1");
            break;
            case (int)JUDGETYPE.Gold_2:
                StartCoroutine(SetTexture(new string[]{"gold", "pai", "castle", "two"}, num + ""));
                AudioManager.instance.ShotOneAudio("gold_2");
            break;
            case (int)JUDGETYPE.Gold_3:
                StartCoroutine(SetTexture(new string[]{"gold", "pai", "castle", "three"}, num + ""));
                AudioManager.instance.ShotOneAudio("gold_3");
            break;
            case (int)JUDGETYPE.Gold_4:
                StartCoroutine(SetTexture(new string[]{"gold", "pai", "castle", "four"}, num + ""));
                AudioManager.instance.ShotOneAudio("gold_4");
            break;
            case (int)JUDGETYPE.Gold_5:
                StartCoroutine(SetTexture(new string[]{"gold", "pai", "castle", "five"}, num + ""));
                AudioManager.instance.ShotOneAudio("gold_5");
            break;
            case (int)JUDGETYPE.Gold_6:
                StartCoroutine(SetTexture(new string[]{"gold", "pai", "castle", "six"}, num + ""));
                AudioManager.instance.ShotOneAudio("gold_6");
            break;
            case (int)JUDGETYPE.Gold_7:
                StartCoroutine(SetTexture(new string[]{"gold", "pai", "castle", "seven"}, num + ""));
                AudioManager.instance.ShotOneAudio("gold_7");
            break;
            case (int)JUDGETYPE.Gold_8:
                StartCoroutine(SetTexture(new string[]{"gold", "pai", "castle", "eight"}, num + ""));
                AudioManager.instance.ShotOneAudio("gold_8");
            break;
            case (int)JUDGETYPE.Gold_9:
                StartCoroutine(SetTexture(new string[]{"gold", "pai", "castle", "nine"}, num + ""));
                AudioManager.instance.ShotOneAudio("gold_9");
            break;
            
            // 金牌牛牛		5张牌中有3张一样，且两张之和为10
            case (int)JUDGETYPE.GoldDouble:
                StartCoroutine(SetTexture(new string[]{"gold", "dan", "castle", "castle"}, num + ""));
                AudioManager.instance.ShotOneAudio("golddouble");
            break;
            // 顺子			5张牌中的数字 为顺子（2，3，4，5，6）
            case (int)JUDGETYPE.Sequence:
                StartCoroutine(SetTexture(new string[]{"shun", "zi"}, num + ""));
                AudioManager.instance.ShotOneAudio("sequence");
            break;
            // 葫芦牛		5张牌的牌型为 AABBB （3张一样的牌加2张一样的牌）
            case (int)JUDGETYPE.Gourd:
                StartCoroutine(SetTexture(new string[]{"lu", "hu", "castle"}, num + ""));
                AudioManager.instance.ShotOneAudio("gourd");
            break;
            // 十小			无花中5张牌的点数相加之和小于等于10
            case (int)JUDGETYPE.Ten:
                StartCoroutine(SetTexture(new string[]{"ten", "xiao"}, num + ""));
                AudioManager.instance.ShotOneAudio("ten");
            break;
            // 四十			无花中5张牌的点数相加之和大于或者等于40
            case (int)JUDGETYPE.Forty:
                StartCoroutine(SetTexture(new string[]{"four", "ten"}, num + ""));
                AudioManager.instance.ShotOneAudio("forty");
            break;
            // 炸弹牛		5张牌中4张点数一样（ABBBB）
            case (int)JUDGETYPE.Bomb:
                StartCoroutine(SetTexture(new string[]{"zha", "dan", "castle"}, num + ""));
                AudioManager.instance.ShotOneAudio("bomb");
            break;
        }
    }

    
}
