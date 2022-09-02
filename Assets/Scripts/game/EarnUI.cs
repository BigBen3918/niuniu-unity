using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarnUI : MonoBehaviour
{
    public Vector2 startRange = new Vector2(150f, 150f);
    public Vector2 targetRange = new Vector2(100f, 100f);
    public int coinMax = 50;
    public float sendMinTime;

    public float sendMaxTime;

    public float currentSendTime;
    public RectTransform targetSlot;
    // Start is called before the first frame update
    void Start()
    {
        startRange = startRange * GameManager.instance.globalUIScale;
        targetRange = targetRange * GameManager.instance.globalUIScale;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SendCoin(RectTransform _startPosition, RectTransform _targetPosition, int balance, int fromIndex, int toIndex){
        
        //Random.Range(0.25f, 3f)
        //Vector2 to = Random.Range(new Vector2(0, 0), startRange);
        /**********
        Vector3 startPosition = _startPosition.position;
        Vector3 targetPosition = _targetPosition.position;
        *************/
        //Vector3 startPosition = Camera.main.ScreenToWorldPoint(_startPosition.position);
        //Vector3 targetPosition = Camera.main.ScreenToWorldPoint(_targetPosition.position);
        Vector3 startPosition = Camera.main.WorldToScreenPoint(_startPosition.position);
        Vector3 targetPosition = Camera.main.WorldToScreenPoint(_targetPosition.position);
        //Camera.main.ScreenToWorldPoint(rectTransform.transform.position)
        //targetSlot = _targetPosition;
        for(int i = 0; i < balance; i++){
            Vector3 fromOffset = RandomPostion(-startRange, startRange);
            float currentProgress = 1f - i/coinMax;
            Vector3 toOffset = RandomPostion(-targetRange, targetRange);
            if(i == balance - 1){
                StartCoroutine(MoveCoin(startPosition + fromOffset, targetPosition + toOffset, fromIndex, toIndex, balance, true));// + toOffset, time
            }else{
                StartCoroutine(MoveCoin(startPosition + fromOffset, targetPosition + toOffset));// + toOffset, time
            }
            
        }
        
    }

    Vector3 RandomPostion(Vector2 from, Vector2 to, float scale = 1f){
        float x = Random.Range(from.x, to.x);
        float y = Random.Range(from.y, to.y);
        return new Vector3(x, y, 0f);
    }

    IEnumerator MoveCoin(Vector3 from, Vector3 to, int fromIndex = 0, int toIndex = 0, int balance = 0, bool isEnd = false){
        currentSendTime += Random.Range(sendMinTime, sendMaxTime);
        if(isEnd){
            GameRoom.instance.LostCoin(fromIndex, balance * GameRoom.instance.antes);
        }
        yield return new WaitForSeconds(currentSendTime);
        if(isEnd){
            currentSendTime = 0;
        }
            
        GameObject coin = GameObject.Instantiate(Resources.Load("items/coin")) as GameObject;
        coin.transform.SetParent(targetSlot);
        coin.transform.localScale = new Vector3(1f, 1f, 1f);
        CMovingObject moveObject = coin.GetComponent<CMovingObject>();

        if(isEnd)
            moveObject.SetEndCoinInfo(true, toIndex, balance);
        moveObject.begin = from;
        moveObject.to = to;
        moveObject.run();
    }
}
