// by Leo Pawel <https://github.com/galaxy126>
// at 10/07/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    public static UI instance;
    // game ui 
    public GameObject splashUI;
    public GameObject lobbyUI;
    public GameObject loginUI;

    public GameObject gameSelectUI;

    public GameObject gameRoomUI;

    public ErrorDialog errorDialog;

    public GameObject coinCanvas;
    public GameObject gameOtherPanels;
    
    public GameObject loadingPanel;
    public bool _showLoading = false;
    // ui collection
    private Dictionary<string, GameObject> UIs = new Dictionary<string, GameObject>();

    

    void Start() {
        instance = this;

        loadingPanel.SetActive(true);
        UIs.Add("splash", splashUI);
        UIs.Add("login", loginUI);
        UIs.Add("lobby", lobbyUI);
        UIs.Add("gameSelect", gameSelectUI);
        UIs.Add("gameRoom", gameRoomUI);

    }

    // Update is called once per frame
    void Update() {
        
    }

    public void ShowPage(string name) {
        if (UIs.ContainsKey(name)) {
            foreach (var item in UIs) {
                item.Value.SetActive(name.Equals(item.Key));
            }
        }
        if(name != "gameRoom"){
            coinCanvas.SetActive(false);
            gameOtherPanels.SetActive(false);
        }else{
            coinCanvas.SetActive(true);
            gameOtherPanels.SetActive(true);
        }
	}
    public void ShowLoading(bool bShow) {
        loadingPanel.SetActive(bShow);
        _showLoading = bShow;
    }
    public void onLogin() {
        
    }
    
    public void ErrorMessage(string error){
        errorDialog.Show(error);
    }
    public void ErrorMessage(int error){
        errorDialog.Show(NotificationWords.instance.errorList[error]);
    }
}
