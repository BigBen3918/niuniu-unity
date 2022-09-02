// by Leo Pawel <https://github.com/galaxy126>
// at 10/07/2022
using System;
using System.Collections;
using System.Collections.Generic;
// using System.Net.WebSockets;
using UnityEngine;
using NativeWebSocket;
using Newtonsoft.Json;
using epoching.easy_qr_code;
/*
PlayerInfo info = new PlayerInfo();
string json = JsonUtility.ToJson(info)


string json = '{}'
PlayerInfo info = new PlayerInfo();
JsonUtility.FromJsonOverwrite(json, info);
*/ 
public class NetworkManager : MonoBehaviour {
	public bool debug = true;
	public bool isLocal;
	// Start is called before the first frame update
	public static NetworkManager instance;
	private string serverUri;
 	
	private string cookie = "";
	private string apiKey = "3f760ae3aa4abe86c51cfe3814a9b760";

	private WebSocket websocket;


	public enum NETWORKSTATUS {
		Disconnected,
		Pendding,
		Connected
	}

	private NETWORKSTATUS networkstatus;

	public class TestStruct {
		public string name;
	}

	void Start() {
		if(isLocal){
			serverUri = "ws://192.168.111.29:15001/3f760ae3aa4abe86c51cfe3814a9b760:";
		}else{
			serverUri = "wss://niuniu-api.deamchain.com/3f760ae3aa4abe86c51cfe3814a9b760:";
		}
	
		instance = this;

		Guid guid = Guid.NewGuid();
		cookie = guid.ToString();
		networkstatus = NETWORKSTATUS.Disconnected;
		

		// Keep sending messages at every 0.3s
		


		// TestStruct info = new TestStruct();
		// info.name = "1234";

		// string json = JsonUtility.ToJson(info);
		// print(json);

		// // string json = '{}'
		// TestStruct info1 = new TestStruct();
		// JsonUtility.FromJsonOverwrite(json, info1);
		// print(info1.name);
		connect();
	}

	// Update is called once per frame
	void Update() {
		#if !UNITY_WEBGL || UNITY_EDITOR
      		websocket.DispatchMessageQueue();
    	#endif	
	}

	
	async void connect() {
			
			websocket = new WebSocket(serverUri + cookie);

			websocket.OnOpen += () => {
				networkstatus = NETWORKSTATUS.Connected;
				if (debug) print("Connection open!");
				UI.instance.ShowLoading(false);
			};

			websocket.OnError += (e) => {
				if (debug) print("Error! " + e);
			};

			websocket.OnClose += (e) => {
				
				if (debug) print("Connection closed!");
				
				if (networkstatus != NETWORKSTATUS.Disconnected) {
					networkstatus = NETWORKSTATUS.Disconnected;
					UI.instance.ShowLoading(true);
					UI.instance.ErrorMessage("网络断开!");
				}
				
				UI.instance.ShowPage("login");
				
				connect();
			};

			websocket.OnMessage += (bytes) => {
				// print(bytes);

				// getting the message as a string
				var message = System.Text.Encoding.UTF8.GetString(bytes);
				onRecv(message);
			};

			//InvokeRepeating("SendWebSocketMessage", 0.0f, 5f);
			// waiting for messages
			await websocket.Connect();
		
	}
	public async void Send(string method, string[] args) {
		try {
			if (websocket!=null && websocket.State == WebSocketState.Open) {
				// convert to 
				RequestType json = new RequestType(method, args);
				string plain = JsonUtility.ToJson(json);

				AES aes = new AES();
				string cipered = aes.Encrypt(plain);
				// Sending bytes
				await websocket.SendText(cipered);
			}
		} catch (System.Exception e) {
			print(e);
		}
  	}

	private void onRecv(string message) {
		AES aes = new AES();
		string json = aes.Decrypt(message);
		ResponseType response = new ResponseType();
		JsonUtility.FromJsonOverwrite(json, response);
		Parse(response.method, response.result, response.error);
	}



	// private async void SendWebSocketMessage() {
	// 	return;
	// 	// try {
	// 	// 	if (websocket!=null && websocket.State == WebSocketState.Open) {
	// 	// 		// Sending bytes
	// 	// 		await websocket.Send(new byte[] { 10, 20, 30 });
	// 	// 		await websocket.SendText("plain text message");
	// 	// 	}
	// 	// } catch (System.Exception e) {
	// 	// 	print(e);
	// 	// }
  	// }
	private void OnDestroy() {
		if(websocket.State == WebSocketState.Open)
			websocket.Close();
	}
	private async void OnApplicationQuit() {
		try {
			if (websocket!=null) await websocket.Close();
		} catch (System.Exception e) {
			print(e);
		}
	}

	string array;
	private void Parse(string method, List<string> result, int error) {
		
		array = "";
		if(error != 0){
			array = "NULL";
		}else{
			foreach(string str in result){
				array += "," + str;
			}
		}
		if(method != "pool" && method != "banker-select-time" && method != "pool-data")
			Debug.Log("ReciveMethod(" + method + ")error(" + error + ")time(" + Time.time + ")Data(" + array + ")");
		if (UI.instance._showLoading) {
			UI.instance.ShowLoading(false);
		}
		if(error != 0){
			UI.instance.ErrorMessage(NotificationWords.instance.errorList[error]);
			return;
		}

		if (result==null || result.Count == 0)
			return;
		
		switch (method) {
		case "login":
			if (result==null) {
				print(method + " response" + String.Join(',', result));
			} else {
				UI.instance.ShowPage("gameSelect");
			}
			break;
		case "update-user-info":
			TopInfoPanel.instance.UpdateInfo(result[0], result[1], result[2], result[3], result[4], result[5], result[6], result[7], result[8]);
			break;
		case "send-code":
			if(error == 0) {
				Register.instance.UpdateSendCode();
			} else {
				UI.instance.ErrorMessage("邮箱发送失败了");	
			}
			break;
		case "register":
			if(error == 0) {
				UI.instance.ErrorMessage("注册成功！你可以登录使用!");	
				Register.instance.UpdateSuccess();
			}
			break;

		case "create-room":
			
			break;

		case "lobby_updateRoom":
			//Lobby.instance.UpdateRoom(result);
			break;
		
		case "enter-lobby":
			GameManager.instance.currentPage = CURRENT_PAGE.Lobby;
			Lobby.instance.UpdateLobby(result);
			break;

		case "enter-room-data":
			UI.instance.ShowPage("gameRoom");
			GameManager.instance.currentPage = CURRENT_PAGE.Game;
			StartCoroutine(EnterGame(result));
			try{
				CreateRoomDialog.instance.Hide();
			}catch(System.Exception e){
				print(e);
			}
			break;
		case "ready-round":
			GameRoom.instance.ReadyRound();
			break;
		case "start-round":
			StartCoroutine(StartRound(result));
			break;

		case "banker-select-time":
			GameRoom.instance.SetTimeLineText(result);
			break;

		case "set-robBanker":
			GameRoom.instance.SetRobBanker(result);
			break;

		case "result-robBanker":
			GameRoom.instance.ResultRobBanker(result);
			break;

		case "set-multiplier":
			GameRoom.instance.SetMultiplier(result);
			break;

		case "card-filp-start":
			GameRoom.instance.CardFilpStart();
			break;

		case "filp-one-card":
			GameRoom.instance.SetOneFilpCard(result);
			break;
		case "card-result":
			GameRoom.instance.SetCardResult(result);
			break;

		case "game-result":
			GameRoom.instance.SetGameResult(result);
			break;
		
		case "end-round":
			GameRoom.instance.EndRound();
			break;

		case "current-round-data":
			GameManager.instance.currentPage = CURRENT_PAGE.Game;
			UI.instance.ShowPage("gameRoom");
			StartCoroutine(EnterCurrentRound(result));
			break;
		
		case "pool-data":
			if(GameManager.instance.currentPage != CURRENT_PAGE.Game)
				return;
			GameRoom.instance.UpdatePool(result);
			break;
		case "send-coin":
			UI.instance.ErrorMessage("金币转账成功·");
			break;
		case "get-moneylog":
			SendMoneyDialog.instance.SetHistoryData(result);
			break;
		case "get-sysmsg":
			EMailDialog.instance.SetData(result);
			break;
		case "get-exps":
			print(GameManager.instance.currentPage);
			if(GameManager.instance.currentPage == CURRENT_PAGE.Game){
				PoolViwer.instance.SetData(result);
			}else{
				RangDialog.instance.SetData(result);
			}
				
			break;
		case "get-spectators":
			SpectatorDialog.instance.SetData(result);
			break;
		case "get-downloadlink":
			Generate_qr_code.instance.on_generate(result[0]);
			break;
		case "get-backendlink":
			BackendDialog.instance.SetData(result[0]);
			break;
		case "get-rewards":
			RewardDialog.instance.SetData(result[0]);
			break;
		case "set-withdraw":
			WithdrawDialog.instance.Reset();
			UI.instance.ErrorMessage("您的兑换申请已提交成功!");	
			break;
		case "get-withdraw":
			WithdrawDialog.instance.SetLogData(result);
			break;
		case "update-avata":
			UI.instance.ErrorMessage("头像更换成功");
			break;
		// case "update-alias":
		// 	UI.instance.ErrorMessage("昵称更换成功");
		// 	break;
		case "update-password":
			UI.instance.ErrorMessage("密码更换成功");
			break;
		}
	}
	IEnumerator EnterGame(List<string> enterRoomData){
		yield return new WaitForSeconds(0);
		GameRoom.instance.SetEnterRoomData(enterRoomData);
		yield return 0;
	}

	IEnumerator StartRound(List<string> data){
		yield return new WaitForSeconds(0.2f);
		GameRoom.instance.StartRound(data);
		yield return 0;
	}

	IEnumerator EnterCurrentRound(List<string> data){
		while (true){
			if(GameManager.instance.currentPage != CURRENT_PAGE.Game) break;
			yield return new WaitForSeconds(0.2f);
			if(GameRoom.instance != null){
				GameRoom.instance.SetCurrentRoundData(data);
				break;
				// if(GameRoom.instance.gameStep != GAME_STEP.None){
				// 	GameRoom.instance.SetCurrentRoundData(data);
				// 	break;
				// }
				
			}
		}
	}

}
