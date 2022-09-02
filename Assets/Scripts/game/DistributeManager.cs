using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistributeManager : MonoBehaviour
{
    string[] animationParams = new string[]{"1P_distribute", "2P_distribute", "3P_distribute", "4P_distribute", "5P_distribute", "6P_distribute"};
    public GameObject baseCard;
    public GameObject[] cards;
    private Animator animator;
    public float distriInterval;
    int endCount;
    List<int> currentPositions = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        
        Reset();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable(){
        animator = gameObject.GetComponent<Animator>();
    }

    public void Reset(){
        HideAllCards();
        endCount = 0;
        currentPositions = null;
        for(int i = 1; i < 7; i++){
            QuitAnimation(i + "P_distribute");
        }
        animator.SetBool("Start", false);
        HideAllCards();

    } 

    public void StartDistribute(List<int> positions){
        currentPositions = positions;
        float delay = 0f;
        int lastPlayerPosition = 0;
        animator.SetBool("Start", true);
        delay += 0.6f;
        StartCoroutine(DelayShowcards(0.6f));
        foreach (int pos in positions)
        {
            lastPlayerPosition = pos;
            StartCoroutine(PlayAnimation(animationParams[pos], delay));
            delay += distriInterval;
            //item.
        }
    }
    

    void ShowAllCards(){
        foreach(var item in cards){
            item.GetComponent<RawImage>().enabled = true;
        }
    }

    void HideAllCards(){
        foreach(var item in cards){
            item.GetComponent<RawImage>().enabled = false;
        }
    }

    IEnumerator PlayAnimation(string animationName, float delay){
        yield return new WaitForSeconds(delay);
        ShowAllCards();
        animator.SetBool(animationName, true);
        
    }

    IEnumerator DelayShowcards(float delay){
        yield return new WaitForSeconds(delay);
        ShowAllCards();
    }

    void QuitAnimation(string animationName){
        animator.SetBool(animationName, false);
    }

    public void EndEvent(){
        GameRoom.instance.DistributeEvent(currentPositions[endCount/5], endCount%5);
        cards[currentPositions[endCount/5] * 5 + endCount%5].GetComponent<RawImage>().enabled = false;
        endCount ++;
        if(currentPositions.Count * 5 == endCount){
            HideAllCards();
            animator.SetBool("Start", false);
            endCount = 0;
        }
    }

    public void StartEvent(){
        AudioManager.instance.ShotOneAudio("distributeOneCard");
    }
}
