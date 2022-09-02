using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolViwerBalanceItem : MonoBehaviour
{
    public Image targetUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(Sprite sprite){
        targetUI.sprite = sprite;
        targetUI.SetNativeSize();
    }

    public void Delete(){
        Destroy(gameObject);
    }
}
