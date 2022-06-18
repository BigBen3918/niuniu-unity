using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Restart : MonoBehaviour {

    public MainLoop mainloop;

    void Start ()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            mainloop.Restart();
        });
    }

}
