using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class Main : MonoBehaviour
{
    public Image img_CardBackside;
    public Image sA, s2, s3, s4, s5, s6, s7, s8, s9, s10, sJ, sQ, sK;
    public Image dA, d2, d3, d4, d5, d6, d7, d8, d9, d10, dJ, dQ, dK;
    public Image hA, h2, h3, h4, h5, h6, h7, h8, h9, h10, hJ, hQ, hK, JokerRed;
    public Image cA, c2, c3, c4, c5, c6, c7, c8, c9, c10, cJ, cQ, cK, JokerBlack;

    public Slider sldr_Deck, sldr_Backside;

    private string backside, deck;
    public Text txt_Backside, txt_Deck;

    public SpriteAtlas cardAtlas1;
    private SpriteRenderer spriteRenderer;

    //spriteRenderer.sprite = cardAtlas1.GetSprite(backName);
    //aIMG.GetComponent<Image>().sprite = spriteRenderer.sprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SLDR_BACKSIDE(1f);
        SLDR_DECK(1f);
    }

    public void SLDR_BACKSIDE(float value)
    {
        backside = "Backside" + value.ToString();
        spriteRenderer.sprite = cardAtlas1.GetSprite(backside);
        img_CardBackside.sprite = spriteRenderer.sprite;
        txt_Backside.text = "CARD BACK SIDES - BACK #" + value.ToString();
    }

    public void SLDR_DECK (float value)
    {
        txt_Deck.text = "DECK OF CARDS - DECK #" + value.ToString();

        // CLUB
        deck = value.ToString() + "CA";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        cA.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "C2";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        c2.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "C3";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        c3.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "C4";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        c4.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "C5";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        c5.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "C6";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        c6.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "C7";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        c7.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "C8";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        c8.sprite = spriteRenderer.sprite = cardAtlas1.GetSprite(deck);

        deck = value.ToString() + "C9";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        c9.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "C10";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        c10.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "CJ";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        cJ.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "CQ";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        cQ.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "CK";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        cK.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "BB";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        JokerBlack.sprite = spriteRenderer.sprite;

        // HEART
        deck = value.ToString() + "HA";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        hA.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "H2";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        h2.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "H3";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        h3.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "H4";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        h4.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "H5";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        h5.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "H6";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        h6.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "H7";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        h7.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "H8";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        h8.sprite = spriteRenderer.sprite = cardAtlas1.GetSprite(deck);

        deck = value.ToString() + "H9";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        h9.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "H10";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        h10.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "HJ";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        hJ.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "HQ";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        hQ.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "HK";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        hK.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "RR";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        JokerRed.sprite = spriteRenderer.sprite;


        // SPADE
        deck = value.ToString() + "SA";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        sA.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "S2";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        s2.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "S3";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        s3.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "S4";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        s4.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "S5";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        s5.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "S6";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        s6.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "S7";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        s7.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "S8";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        s8.sprite = spriteRenderer.sprite = cardAtlas1.GetSprite(deck);

        deck = value.ToString() + "S9";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        s9.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "S10";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        s10.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "SJ";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        sJ.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "SQ";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        sQ.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "SK";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        sK.sprite = spriteRenderer.sprite;


        // DIAMONDS
        deck = value.ToString() + "DA";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        dA.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "D2";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        d2.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "D3";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        d3.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "D4";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        d4.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "D5";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        d5.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "D6";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        d6.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "D7";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        d7.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "D8";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        d8.sprite = spriteRenderer.sprite = cardAtlas1.GetSprite(deck);

        deck = value.ToString() + "D9";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        d9.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "D10";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        d10.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "DJ";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        dJ.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "DQ";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        dQ.sprite = spriteRenderer.sprite;

        deck = value.ToString() + "DK";
        spriteRenderer.sprite = cardAtlas1.GetSprite(deck);
        dK.sprite = spriteRenderer.sprite;

    }

}
