// by Leo Pawel <https://github.com/galaxy126>
// at 10/07/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class UserInfo{
	public string name;
	public string id;
	public string balance;
	public string exp;
	public string avata;
	Texture2D photo;
	public UserInfo(string _name, string _id, string _balance, string _exp, string _avata){
		UpdateInfo(_name, _id, _balance, _exp, _avata);
	}

	public void UpdateInfo(string _name, string _id, string _balance, string _exp, string _avata){
		name = _name;
		id = _id;
		balance = _balance;
		exp = _exp;
		avata = _avata;
	}

	public void UpdateBalance(float _balance, float _exp){
		balance = (float.Parse(balance) + _balance).ToString("0.00");
		exp = (float.Parse(exp) + _exp).ToString("0.00");
	}
}

// public class PlayerSaveData{
// 	public string username;
// 	public string password;
// 	public string bank;
// 	public string bankAccount;
// 	public string bankOwner;
// 	public string alipayAccount;
// 	public string alipayOwner;

// 	public void write() {
// 		PlayerPrefs.SetString("username", "");
// 	}
// }

public class GameManager : MonoBehaviour {
	// signed user
	// Start is called before the first frame update
	public UserInfo userInfo;
	public static GameManager instance;
	public CanvasScaler mainCanvas;
	public CanvasScaler coinCanvas;
	public CURRENT_PAGE currentPage;
	public float globalUIScale;
	void Start() {
		Helper.SetupLocale("en-US");
		UI.instance.ShowPage("login");
		instance = this;
		globalUIScale = Screen.height* 1.3f/1440f ;
        mainCanvas.scaleFactor = globalUIScale;
		coinCanvas.scaleFactor = globalUIScale;


		PlayerPrefs.SetString("username", "");
		// AES aes = new AES();
		// print(aes.Decrypt("xQjcZ9SSpAu5XxM5NfNJVw=="));
		// UI.instance.showUI("login");
	}

	// Update is called once per frame
	void Update() {

	}

	void OnEnable(){
		instance = this;
	}
}
