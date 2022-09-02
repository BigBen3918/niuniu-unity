// by Leo Pawel <https://github.com/galaxy126>
// at 10/07/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class GameRoom : MonoBehaviour {
	public static GameRoom instance;

	#region RoomInfo
		public GAME_STEP gameStep;
		public Text currentTimeTxt;
		public Text roomIdRxt;
		public int spectators; public GameObject spectatorCount;
		public float antes = 0; public Text antesTxt;
		public int bankerId;
		List<int> localPlayerPositions = new List<int>();
		public Text dataTimeTxt;
	#endregion
	
	#region PlayerInfo
		int playerPosition;
		public PlayerManager[] players;
		bool isPlayer;
	#endregion

	#region ChildClass
		public DistributeManager distriManager;
	#endregion
	
	#region Objects
		public Animator startRoundEffect;
		public Animator GameResultEffect;
		public GameObject rankSeletBtGroup;
		public GameObject zeroButton;
		public GameObject showModeBtGroup;
		public GameObject SpectatorMark;
		public GameObject quitButton;
		public FilpOneCard filpOneCard;
		public TimeLine timeLine;
	#endregion
	
	#region Pool
		public float poolAmount;
		public float poolNewAmount;
		public Text poolAmountTxt;
		public Text poolLeftTimeTxt;
		public float poolV;
		
	#endregion
	void Start() {
		instance = this;
		poolAmount = 0;
		poolNewAmount = 0;
		Reset();
	}

	// Update is called once per frame
	void Update() {
		dataTimeTxt.text = System.DateTime.Now.ToString("yyyy/mm/dd HH:mm");
		if(!isPlayer) {
			Image image = quitButton.GetComponent<Image>();
         	image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
			SpectatorMark.SetActive(true);
			
		}else{
			if(gameStep == GAME_STEP.Result || gameStep == GAME_STEP.None || gameStep == GAME_STEP.Ready){
				Image image = quitButton.GetComponent<Image>();
				image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
			}else{
				Image image = quitButton.GetComponent<Image>();
				image.color = new Color(image.color.r, image.color.g, image.color.b, 0.1f);
			}
			SpectatorMark.SetActive(false);
		}

		if(poolAmount < poolNewAmount){
			poolV = poolNewAmount-poolAmount;
			poolAmount += Time.deltaTime * (poolV)/3f;
			//poolAmountTxt.text = (Mathf.Round(poolAmount * 100f) * 0.01f).ToString();
			poolAmountTxt.text = poolAmount.ToString("0.00");
		}else{
			poolV= 0f;
			poolAmount = poolNewAmount;
			poolAmountTxt.text = poolAmount.ToString("0.00");
		}
		//poolAmountTxt.text = (Mathf.Round(poolAmount * 100f) * 0.01f).ToString();
		

		
	}

	public void Reset(){
		gameStep = GAME_STEP.None;
		Component[] animators;
		animators = gameObject.GetComponentsInChildren<Animator>();	
		playerPosition = 0;
	}

	void ResetRound(){
		localPlayerPositions.Clear();
		if(distriManager != null){
			distriManager.Reset();	
		}
		
		foreach(var player in players){
			player.RoundReset();
		}
		bankerId = -1;
		spectatorCount.SetActive(false);
		rankSeletBtGroup.SetActive(false);
		showModeBtGroup.SetActive(false);
		zeroButton.SetActive(true);
		isPlayer = true;
	}

	void ResetDistribute(){
		distriManager.Reset();
	}

	private void OnEnable() {
		Reset();
		instance = this;
        GameManager.instance.currentPage = CURRENT_PAGE.Game;
	}

	int GetLocalPositon(int i){
		if(!isPlayer) return i;
		int localPosition = i - playerPosition;/////
		if(localPosition < 0)//
			localPosition = 6 + localPosition;
		return localPosition;
	}
	public void SetEnterRoomData(List<string> data){
		if(gameStep == GAME_STEP.None || gameStep == GAME_STEP.Ready){
			ResetRound();
		}
		roomIdRxt.text = popString(data);
		antes = float.Parse(popString(data));
		antes = Mathf.Round(antes *100.0f) *0.01f;
		antesTxt.text = antes.ToString();
		playerPosition = int.Parse(popString(data));
		isPlayer = playerPosition < 6;
		print("playerPosition(first) ---" + playerPosition);
		if(playerPosition > 5){
			playerPosition = 0;
			isPlayer = false;
		}else{
			isPlayer = true;
		}
		spectators = int.Parse(popString(data));
		if(spectators == 0){
			spectatorCount.SetActive(false);
		}else if(spectators > 0){
			spectatorCount.SetActive(true);
			spectatorCount.GetComponentsInChildren<Text>()[0].text = spectators.ToString();
		}
		for(int i = 0; i < 6; i++){
			List<string> sendData = new List<string>();	
			for(int k = 0; k < 3; k++){
				sendData.Add(popString(data));
			}
			players[GetLocalPositon(i)].SetEnterRoomData(sendData);
		}
		if(gameStep == GAME_STEP.None){
			gameStep = GAME_STEP.Ready;
		}
	}
	public void ReadyRound(){
		//NetworkManager.instance.Send("ready-round", new String[]{roomIdRxt.text});
	}
	public void SetCurrentRoundData(List<string> data){
		//ResetRound();
		localPlayerPositions.Clear();
		isPlayer = false;
		int playerCount = int.Parse(popString(data));
		gameStep = (GAME_STEP)int.Parse(popString(data));
		int bankerIndex = int.Parse(popString(data));
		List<int> cardList = new List<int>();
		if(bankerIndex != -1 && bankerIndex < 6)
			players[bankerIndex].SetBankerMark();
		for(int i = 0; i < playerCount; i++){
			int playerIndex = int.Parse(popString(data));
			localPlayerPositions.Add(playerIndex);
			cardList.Clear();
			for(int k = 0; k <5; k++){
				int buf = int.Parse(popString(data));
				cardList.Add(buf);
			}
			players[playerIndex].SetCardsInfo(cardList);
			players[playerIndex].FilpCards();
			for(int m = 0; m < 5; m++){
				players[playerIndex].ShowCard(m);
			}
			
			int multiplier = int.Parse(popString(data));
			if(multiplier != -1){
				players[playerIndex].ShowbankerNum(multiplier);
			}
			int judge, judgeNumber;
			judge = int.Parse(popString(data));
			judgeNumber = int.Parse(popString(data));
			players[playerIndex].ShowPaeResult(judge, judgeNumber);
		}
	}
	public void StartRound(List<string> data){
		print("playerPosition" + playerPosition);
		AudioManager.instance.ShotOneAudio("startRound");
		gameStep = GAME_STEP.BankerSelect;
		List<int> playerCards = new List<int>();
		for(int i = 0; i < 6; i++){

			if(popString(data) != "-1") {
				int pos = GetLocalPositon(i);
				localPlayerPositions.Add(pos);
				PlayerManager player = players[pos];
				try
				{
					player.balanceTxt.text = (float.Parse(player.balanceTxt.text) - antes).ToString();
				}
				catch (System.Exception)
				{
					
				}
				

				 if (playerPosition==i) {
				 	TopInfoPanel.instance.UpdateBalance(-antes, 0);
				}
			}

		}
		for(int i = 0; i <5; i++){
			int buf = int.Parse(popString(data));
			playerCards.Add(buf);
		}
		SetCardsInfo(0, playerCards);
		//DistributeCard(playerPostionList);
		StartCoroutine(DistributeProcess(localPlayerPositions));
	}

	public void SetRobBanker(List<string> data){
		ResetDistribute();
		for(int i = 0; i < 6; i++){
			int num = int.Parse(popString(data));
			OnSetRankNum(GetLocalPositon(i), num);
			if( i == playerPosition && num > -1){
				rankSeletBtGroup.SetActive(false);
				if(num == 0){
					AudioManager.instance.ShotOneAudio("nobanker");
				}
			}	
		}
	}

	public void SetMultiplier(List<string> data){
		ResetDistribute();
		for(int i = 0; i < 6; i++){
			int num = int.Parse(popString(data));
			OnSetRankNum(GetLocalPositon(i), num);
			if( i == playerPosition && num > -1){
				rankSeletBtGroup.SetActive(false);
			}	
		}
	}

	public void ResultRobBanker(List<string> data){
		gameStep = GAME_STEP.MultiplierSelect;
		AudioManager.instance.ShotOneAudio("banker");
		ResetDistribute();
		for(int i = 0; i < 6; i++){
			int num = int.Parse(popString(data));
			OnSetRankNum(GetLocalPositon(i), num);
			if( num > -1){
				bankerId = i;
				players[GetLocalPositon(i)].SetBankerMark();
				players[GetLocalPositon(i)].SetFocus();
				rankSeletBtGroup.SetActive(false);
				if(playerPosition != bankerId && isPlayer) {
					rankSeletBtGroup.SetActive(true);
				}	
				zeroButton.SetActive(false);
			}

		}
	}
	public void CardFilpStart(){
		gameStep = GAME_STEP.ShowCard;
		ResetDistribute();
		if(isPlayer){
			showModeBtGroup.SetActive(true);
		}
		foreach(var playerIndex in localPlayerPositions){
			if(isPlayer){
				if(playerIndex == 0) continue;
				StartCoroutine(players[playerIndex].profileUI.Viewing());
			}else{
				StartCoroutine(players[playerIndex].profileUI.Viewing());
			}	
		}
			
	}

	public void SetOneFilpCard(List<string> data){
		ResetDistribute();
		int cardNum = int.Parse(popString(data));
		filpOneCard.StartShow(cardNum);

	}
	IEnumerator DelayCardResult(List<string> data, float delay){
		yield return new WaitForSeconds(delay);
		ResetDistribute();
		for(int pos = 0; pos < 6; pos++){
			int position = GetLocalPositon(pos);
			List<int> cardResult = new List<int>();
			List<int> cards = new List<int>();
			List<int> restCards = new List<int>();
			cardResult.Add(int.Parse(popString(data)));
			cardResult.Add(int.Parse(popString(data)));
			restCards.Add(int.Parse(popString(data)));
			restCards.Add(int.Parse(popString(data)));

			for(int i = 0; i < 5; i++){
				cards.Add(int.Parse(popString(data)));
			}
			if(pos == playerPosition && cardResult[0] != -1){
				showModeBtGroup.SetActive(false);
				filpOneCard.Hide();
			}
			players[position].profileUI.cardFilped = true;
			ShowCardResult(position, cardResult[0], cardResult[1]);
			SetCardsInfo(position, cards, restCards);
			FilpCard(position);
		}
	}
	float lastResultTime = 0;
	float resultAudioDealy = 0.7f;
	float delayCount = 0;

	public void SetCardResult(List<string> data){
		if((Time.time - lastResultTime) > resultAudioDealy){
			StartCoroutine(DelayCardResult(data, 0f));
			delayCount = 0;
		}else{
			delayCount ++;
			StartCoroutine(DelayCardResult(data, delayCount * resultAudioDealy));
		}
		lastResultTime = Time.time;
		
	}

	public void SetGameResult(List<string> data){
		ResetDistribute();
		StartCoroutine(GameResultPorcess(data));
		gameStep = GAME_STEP.Result;
	}

	public void EndRound(){
		ResetRound();
	}
	IEnumerator GameResultPorcess(List<string> data){
		List<int[]> earns = new List<int[]>();
		List<int[]> loss = new List<int[]>();
		int earnsNum = int.Parse(popString(data));
		int lossNum = int.Parse(popString(data));
		bool myWin = false;
		float totalEarns = 0, totalLoss = 0;
		for(int i = 0; i < earnsNum; i++) {
			var pos = GetLocalPositon(int.Parse(popString(data)));
			float v = float.Parse(popString(data));
			totalEarns += v;
		 	earns.Add(new int[]{pos, (int)v});
		}
		for(int i = 0; i < lossNum; i++){
			int pos = GetLocalPositon(int.Parse(popString(data)));
			float v = float.Parse(popString(data));
			totalLoss += v;
			loss.Add(new int[]{pos, (int)v});
			if (pos==playerPosition) {
				myWin = true;
			}
		}
		yield return new WaitForSeconds(0.5f);

		var isBanker = playerPosition==bankerId;
		print("isRanker---" + isBanker);
		print("lossNum---" + lossNum);
		print("earnNum---" + earnsNum);
		if(lossNum == 0/*  && earnsNum > 0 */) {
			if(earnsNum == 1){
				GameResultEffect.SetTrigger(isBanker ? "smallWin" : "smallLoss");
				AudioManager.instance.ShotOneAudio(isBanker ? "youwin" : "youlost");
			} else {
				GameResultEffect.SetTrigger("win");
				AudioManager.instance.ShotOneAudio("bankerwin");
			}
		} else if(earnsNum == 0) {
			if(lossNum == 1){
				GameResultEffect.SetTrigger(isBanker ? "smallLoss" : "smallWin");
				AudioManager.instance.ShotOneAudio(isBanker ? "youlost" : "youwin");
			} else {
				GameResultEffect.SetTrigger("loss");
				AudioManager.instance.ShotOneAudio("bankerlost");
			}
		} else {
			if (isPlayer) {
				if (isBanker) {
					GameResultEffect.SetTrigger(totalEarns > totalLoss ? "smallWin" : "smallLoss");
				} else {
					GameResultEffect.SetTrigger(myWin ? "smallWin" : "smallLoss");
				}
			}
		}

		//distribute coin
		if(earns.Count > 0)
			yield return new WaitForSeconds(2f);
		foreach(var tx in earns){
			SendCoin(tx[0], GetLocalPositon(bankerId), tx[1]);
		}

		yield return new WaitForSeconds(2f);
		foreach(var tx in loss){
			SendCoin(GetLocalPositon(bankerId), tx[0], tx[1]);
		}
	}
	IEnumerator DistributeProcess(List<int> playerPositions){
		yield return new WaitForSeconds(0.1f);
		startRoundEffect.SetTrigger("start");
		yield return new WaitForSeconds(0.8f);
		DistributeCard(playerPositions);
		yield return new WaitForSeconds(2.5f);
		players[0].FilpCards();
		yield return new WaitForSeconds(0.5f);
		if(isPlayer)
			rankSeletBtGroup.SetActive(true);
	}

	public void DistributeCard(List<int> positions){
		positions.Sort();
		distriManager.StartDistribute(positions);
	}

	public void DistributeEvent(int playerPosition, int cardPosition){
		players[playerPosition].ShowCard(cardPosition);
	}

	public void SetCardsInfo(int playerPosition, List<int> cardIds, List<int> restCards = null){
		//print("pos--" + playerPosition + "---cardIds" + cardIds[3]);
		players[playerPosition].SetCardsInfo(cardIds, restCards);
	}

	public void FilpCard(int playerPosition){
		players[playerPosition].FilpCards();
	}

	public void OnSetRankNum(int playerPosition, int id){
		players[playerPosition].ShowbankerNum(id);
	}

	public void ShowCardResult(int playerPosition, int result, int num){
		
		players[playerPosition].ShowPaeResult(result, num);
	}

	public void GotCoin(int playerPosition, float balance){
		players[playerPosition].profileUI.GotCoin(balance);
	}

	public void LostCoin(int playerPosition, float balance){
		players[playerPosition].profileUI.LostCoin(balance);
	}

	public RectTransform GetEarnPosition(int playerPosition){
		return players[playerPosition].profileUI.GetPosition();
	}

	public void SendCoin(int from, int to, int balance){
		GetComponent<EarnUI>().SendCoin(GetEarnPosition(from), GetEarnPosition(to), balance, from, to);
		//
		// ;
		// localPlayerPositions.Add(pos);
		PlayerManager playerFrom = players[from];
		playerFrom.balanceTxt.text = (float.Parse(playerFrom.balanceTxt.text) - balance * antes).ToString("0.00");
		
		PlayerManager playerTo = players[to];
		playerTo.balanceTxt.text = (float.Parse(playerTo.balanceTxt.text) + balance * antes).ToString("0.00");

		int pos = GetLocalPositon(playerPosition);
		if (pos==from) {
			TopInfoPanel.instance.UpdateBalance(-balance * antes, 0);
		} else if (pos==to) {
			TopInfoPanel.instance.UpdateBalance(balance * antes, balance * antes);
		}
	}

	string popString(List<string> list){
        string result = list[0];
        list.RemoveAt(0);
        return result;
    }

	public void SetTimeLineText(List<string> txt){
		timeLine.UpdateInfo(txt);
	}

	public void OnSetRankNum(int num){
		if(gameStep == GAME_STEP.BankerSelect)
			NetworkManager.instance.Send("set-robBanker", new string[]{roomIdRxt.text, num.ToString()});
		if(gameStep == GAME_STEP.MultiplierSelect)
			NetworkManager.instance.Send("set-multiplier", new string[]{roomIdRxt.text, num.ToString()});
	}

	public void OnFilpCard(){
		NetworkManager.instance.Send("filp-card", new string[]{roomIdRxt.text});
	}

	public void OnFilpOneCard(){
		NetworkManager.instance.Send("filp-one-card", new string[]{roomIdRxt.text});
	}

	public void OnLeaveRoom(){
		if(gameStep == GAME_STEP.None || gameStep == GAME_STEP.Result || !isPlayer ||  gameStep == GAME_STEP.Ready){
			NetworkManager.instance.Send("leave-room", new string[]{roomIdRxt.text});
			UI.instance.ShowPage("lobby");
			Reset();
		}
	}
	public void UpdatePool(List<string> data){
		poolNewAmount = float.Parse(popString(data));
		int poolLeftTime = int.Parse(popString(data));
		var hh = poolLeftTime / 60 / 60;
		var mm = poolLeftTime / 60 % 60;
		var ss = poolLeftTime % 60;
		poolLeftTimeTxt.text = hh.ToString("00") + ":" + mm.ToString("00") + ":" + ss.ToString("00");
		PoolViwer.instance.SetTime(poolLeftTimeTxt.text);
	}
}
