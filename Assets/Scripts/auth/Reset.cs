// by Leo Pawel <https://github.com/galaxy126>
// at 10/07/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    // Start is called before the first frame update
    public static Reset instance;
    // game ui 
    private string email = "";
    // ui collection

    void Start() {
        instance = this;
    }

    public void setEmail(string email) {
        this.email = email;
    }

    public void onSubmit() {
        if (!Helper.validateEmail(email)) {
            print(Helper.getError(20001));
            UI.instance.ErrorMessage(NotificationWords.instance.errorList[6]);
            return;
        }
        print(email);
        NetworkManager.instance.Send("reset", new string[]{email});
    }
}
