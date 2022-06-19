using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class table : MonoBehaviour
{
    public Texture[] images;
    private bool toggle1, toggle2;
    public GameObject btn1, btn2;
    public Sprite[] textures;

    // Start is called before the first frame update
    void Start()
    {
        Transform transform = this.transform;
        int childLen = transform.childCount;
        for(int i=0; i<childLen;i++)
        {
            Transform child = transform.GetChild(i);
            if(child.name == "person")
            {
                child.GetChild(0).gameObject.GetComponent<RawImage>().texture = images[Random.Range(0, 5)];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void setToggle(int index)
    {
        if(index == 1)
        {
            if(toggle1 == true)
            {
                toggle1 = false;
                btn1.GetComponent<Image>().sprite = textures[0];
            }
            else
            {
                toggle1 = true;
                btn1.GetComponent<Image>().sprite = textures[1];
            }
        }
        else
        {
            if(toggle2 == true)
            {
                toggle2 = false;
                btn2.GetComponent<Image>().sprite = textures[0];
            }
            else
            {
                toggle2 = true;
                btn2.GetComponent<Image>().sprite = textures[1];
            }
        }
    }
}
