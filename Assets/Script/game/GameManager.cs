using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GoBang
{

    public class GameManager : MonoBehaviour
    {
        public TextMeshProUGUI userName;
        // Start is called before the first frame update
        void Start()
        {
            if (GlobalDatas.authdata != null)
                userName.text = GlobalDatas.authdata.username;
            else userName.text = "demo";
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
