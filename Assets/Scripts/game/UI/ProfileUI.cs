using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shapes2D;
public class ProfileUI : MonoBehaviour
{
    
    Animator animator;
    public GameObject bankerMark;
    public Text earnText;
    public RawImage earnBack;

    public Text viweingTxt;
 
    public bool cardFilped;
    string[] viweingText = new string[]{"搓牌中", "搓牌中.", "搓牌中..", "搓牌中..."};
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        viweingTxt.text = "";
        cardFilped = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cardFilped){
            viweingTxt.text = "";
        }
    }

    private void OnEnable() {
        animator = GetComponent<Animator>();
        Reset();
    }
    public void Reset(){
        if(animator == null)
            animator = GetComponent<Animator>();
        earnText.text = "";
        bankerMark.SetActive(false);
        viweingTxt.text = "";
        cardFilped = false;
        animator.SetBool("return", true);
    }

    public void SetBankMark(){
        animator.SetBool("return", false);
        bankerMark.SetActive(true);
        animator.SetTrigger("focus");
    }

    public void HideMark(){
        bankerMark.SetActive(false);
    }

    public void Focus(){
        animator.SetBool("return", false);
        animator.SetTrigger("focus");
    }

    public void GotCoin(float balance){
        animator.SetBool("return", false);
        animator.SetTrigger("focus");
        earnText.text = "+" + balance + "";
        earnText.color = UIRefer.instance.GetCoinColor;
        earnBack.texture = UIRefer.instance.GetCoinTexture;
        animator.SetTrigger("balance");

    }

    public void LostCoin(float balance){
        animator.SetBool("return", false);
        earnText.text = "-" + balance + "";
        earnText.color = UIRefer.instance.LoseCoinColor;
        earnBack.texture = UIRefer.instance.LoseCoinTexture;
        animator.SetTrigger("balance");
    }
    public RectTransform GetPosition(){
        return GetComponent<RectTransform>();
    }

    public IEnumerator Viewing(){
        int delay = Random.Range(1, 7);
        yield return new WaitForSeconds((float)delay);
        for(int i = 0; i < (7 - delay) * 5; i ++){
            yield return new WaitForSeconds(0.2f);
            if(cardFilped){
                viweingTxt.text = "";
                continue;
            }
                
            viweingTxt.text = viweingText[i%4];

        }
    }
}
