using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilpOneCard : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public RawImage card;
    public Image coverCard;

    Texture2D cardTexture;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Show(){
        animator.SetTrigger("show");
        animator.SetBool("start", false);
    }

    public void StartShow(int id){
        print("filp-one-card ---- end" + id);
        if(id == -1) 
            return;
        coverCard.enabled = true;
        cardTexture = Resources.Load<Texture2D>("Card-2/" + id);
        card.texture = cardTexture;
        animator.SetBool("start", true);
    }

    public void EndEvent(){
        NetworkManager.instance.Send("filp-card", new string[]{GameRoom.instance.roomIdRxt.text});
    }
    public void Hide(){
        animator.SetBool("start", false);
    }

}
