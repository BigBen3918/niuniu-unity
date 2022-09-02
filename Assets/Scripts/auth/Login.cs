// by Leo Pawel <https://github.com/galaxy126>
// at 10/07/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    public static Login instance;


    public InputField passwordInput;
    
    public InputField emailInput;

    void Start() {
        instance = this;
        emailInput.text = PlayerPrefs.GetString("email", "");
        passwordInput.text = PlayerPrefs.GetString("password", "");
    }

    public void onSubmit() {
        string password = passwordInput.text;
        string email = emailInput.text;
        if (!Helper.validateEmail(email)) {
            print(Helper.getError(20001));
            UI.instance.ErrorMessage(NotificationWords.instance.errorList[(int)ERROR_TYPE.LOGIN_EMAIL_INVALID]);
            return;
        }
        if (password.Length<6 || password.Length>32) {
            UI.instance.ErrorMessage(NotificationWords.instance.errorList[2]);
            print(Helper.getError(20005));
            return;
        }
        
        PlayerPrefs.SetString("email", email);
        PlayerPrefs.SetString("password", password);

        NetworkManager.instance.Send("login", new string[]{email, password});
        UI.instance.ShowLoading(true);
    }

    void OnEnable() {
        GameManager.instance.currentPage = CURRENT_PAGE.Loggin;
    }
    
}
