// by Leo Pawel <https://github.com/galaxy126>
// at 10/07/2022
// at 11/07/2022

using System;
using System.Collections.Generic;
using UnityEngine;

// basic data type

public class RequestType {
    // public string       jsonrpc;
	public string       method;
	public string[]     args;
	// public int          id;

    public RequestType(string method, string[] args) {
        this.method = method;
        this.args = args;
    }
}

public class ResponseType {
    // public string       jsonrpc;
	public string       method;
	public List<string>     result;
	public int          error;
	// public int          id;
}

// app specific data type
public enum JUDGETYPE {
	None,		// 无牛			牌型：五张牌中，任意3张牌的点数之和都不为10的整数倍
	Cattle_1,	// 牛1 			有牛牌型：5张牌中有3张牌的点数之和为10的整数倍，另外2张牌的点数之和mod（10）等于几，即为牛几
	Gold_1,		// 金刚牛1 			有牛牌型：5张牌中有3张牌的点数之和为10的整数倍，另外2张牌的点数之和mod（10）等于几，即为牛几
	Cattle_2,	// 牛2
	Gold_2,		// 金刚牛2
	Cattle_3,	// 牛3
	Gold_3,		// 金刚牛3
	Cattle_4,	// 牛4
	Gold_4,		// 金刚牛4
	Cattle_5,	// 牛5
	Gold_5,		// 金刚牛5
	Cattle_6,	// 牛6
	Gold_6,		// 金刚牛6
	Cattle_7,	// 牛7
	Gold_7,		// 金刚牛7
	Cattle_8,	// 牛8
	Gold_8,		// 金刚牛8
	Cattle_9,
	Gold_9,
	Double,		// 牛牛  		牌型：5张牌中有3张牌的点数之和为10的整数倍，并且，另外2张牌的点数之和为10的整数倍
	GoldDouble,	// 金牌牛牛		5张牌中有3张一样，且两张之和为10
	
	Sequence,	// 顺子			5张牌中的数字 为顺子（2，3，4，5，6）
	Gourd,		// 葫芦牛		5张牌的牌型为 AABBB （3张一样的牌加2张一样的牌）
	Ten,		// 十小			无花中5张牌的点数相加之和小于等于10
	Forty,		// 四十			无花中5张牌的点数相加之和大于或者等于40
	Bomb,		// 炸弹牛		5张牌中4张点数一样（ABBBB）
}

public enum CURRENT_PAGE{
	Loggin,
	GameSelect,
	Lobby,
	Game,

}
public enum GAME_STEP {
	None,
	Ready,				// 分派
	Distribute,			// 分派牌张
	BankerSelect,		// 选择抢庄倍数
	MultiplierSelect,	// 选择倍数
	ShowCard,			// 亮牌
	Result,				// 结果
}

public enum ERROR_TYPE{
	REGISTER_EMAIL_INVALID = 1,

	REGISTER_PASSWORD_INVALID = 2,

	REGISTER_USER_INVALID = 3,

	LOGING_USER_INVALID = 4,

	LOGING_PASSWORD_FORMAT_INVALID = 5,

	LOGING_PASSWORD_INVALID = 6,

	LOGING_NO_ACTIVE = 7,

	LOGIN_EMAIL_INVALID = 8,

	DATA_FORMAT_INVALID = 10,

	USER_INVALID = 11,

	REQUEST_INVALID_COST = 12,

	LACK_BALANCE = 13,

	
	VERIFY_EMAIL = 15,
	
	USER_INROOM = 16,

	UNKNOWN = 14,
}
public class PlayerType {
	/* user inner information */
	public string cookie;
	// user avatar raw iamge
	public UnityEngine.UI.RawImage avatar;
	// user name
	public string username;
	// unique account id
	public int id;
	// balance
	public float balance;
	// card holding status
	public List<int> cardList;
	// position in room
	public int position;
	/* user game variables */

	// 任何人可以抢庄，按照1，2，3，4倍数抢。选择倍数最高的玩家成为庄家, if zero, never 抢庄
	public int robBanker;

	// affected by win bonus or loss, value in 1 ~ 4
	public int multiplier;

	// 亮牌模式， 若真, 搓牌模式，若否，亮牌模式
	public bool showManual;

	// 牌型
	public JUDGETYPE judgeType;
}