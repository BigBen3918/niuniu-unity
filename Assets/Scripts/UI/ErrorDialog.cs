using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorDialog : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public UnityEngine.UI.Text info;
    public float delay;
    Coroutine show;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator Hide(){
        yield return new WaitForSeconds(delay);
        animator.SetBool("Show", false);
        info.text = "";
    }

    public void Show(string txt){
        if(show != null)
            StopCoroutine(show);
        animator.SetBool("Show", true);
        info.text = txt;
        show = StartCoroutine(Hide());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
