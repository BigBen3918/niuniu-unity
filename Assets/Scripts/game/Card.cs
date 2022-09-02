// by Leo Pawel <https://github.com/galaxy126>
// at 10/07/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card  : MonoBehaviour 
{

    // card index
    Animator animator;
    int id;
    Texture backImage;
    Texture frontImage;
    bool isRestCard;
    PlayerManager playerManager;
    void Start(){
        id = -1;
        animator = GetComponent<Animator>();
        backImage = Resources.Load<Texture2D>("Card-back/1");
        Reset();
    }

    public void Show(){
        GetComponent<RawImage>().enabled = true;
    }

    public void Reset(){
        if(animator != null){
            animator.SetBool("Return", true);
            animator.SetBool("Up", false);
            animator.SetBool("Filp", false);
        }
            
        GetComponent<RawImage>().texture = backImage;
        GetComponent<RawImage>().enabled = false;
        frontImage = null;
        id = -1;
        isRestCard = false;
        playerManager = null;
    }
    
    public void FilpCard(){
        if(id == -1) return;
        AudioManager.instance.ShotOneAudio("filpCard");
        animator.SetBool("Return", false);
        animator.SetBool("Filp", true);
        animator.SetBool("Up", false);
        //if(isRestCard)
            //UpCard();
    }

    public void UpCard(){
        if(id == -1 || !isRestCard) return;
        animator.SetBool("Return", false);
        animator.SetBool("Filp", false);
        animator.SetBool("Up", true);
    }

    public void UpCardAnimationEvent(){
        try
        {
            playerManager.UpCard();
        }
        catch (System.Exception)
        {
            throw;
        }
        
    }

    public void ShowFront(){
        if(id == -1) return;
        GetComponent<RawImage>().texture = frontImage;
    }

    public void SetId(int _id, PlayerManager _playerManager, List<int> restCards = null){
        isRestCard = false;
        playerManager = _playerManager;
        id = _id;
        frontImage = Resources.Load<Texture2D>("Card-1/" + _id);
        if(restCards == null) return;
        if(restCards[0] == -1 || restCards[1] == -1) return;
        if( restCards[0] == _id || restCards[1] == _id )
            isRestCard = true;
    }
}
