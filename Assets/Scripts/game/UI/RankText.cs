using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RankText : MonoBehaviour
{
    public RawImage[] images; // 0-*, 1-number, 2-text
    // Start is called before the first frame update
    void Start()
    {
        HideAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset(){
        HideAll();
    }

    public void HideAll(){
        foreach(var img in images){
            img.enabled = false;
        }
    }

    public void ShowNumber(int num){
        HideAll();
        if(num == -1) return;
        if(num == 0){
            images[2].enabled = true;
            return;
        }
        images[0].enabled = true;
        images[1].enabled = true;
        images[1].texture = UIRefer.instance.GetImage("" + num);
    }

    public void SetColor(){

    } 
}
