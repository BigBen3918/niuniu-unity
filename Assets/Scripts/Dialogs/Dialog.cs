using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddItem(){

    }

    public virtual void Show(){
        GetComponent<Animator>().SetBool("show", true);
    }

    public void Hide(){
        GetComponent<Animator>().SetBool("show", false);
    }

    public string popString(List<string> list){
        string result = list[0];
        list.RemoveAt(0);
        return result;
    }
}
