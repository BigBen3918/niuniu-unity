using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupportDialog : Dialog
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

    public void Show(){
        GetComponent<Animator>().SetBool("show", true);
    }

    public void Hide(){
        GetComponent<Animator>().SetBool("show", false);
    }
}
