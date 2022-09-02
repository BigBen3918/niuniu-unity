// by Leo Pawel <https://github.com/galaxy126>
// at 10/07/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationWords : MonoBehaviour
{

    public Dictionary<int, string> errorList = new Dictionary<int, string>();
    public static NotificationWords instance;
    void Start() {
        instance = this;
        ErrorListInit();
    }

    void ErrorListInit(){
        errorList.Add(10000, "没找到!");
        errorList.Add(10001, "无效数据格式!");

        errorList.Add(20000, "无效邮箱格式!");
        errorList.Add(20001, "无效邮箱地址!");
        errorList.Add(20002, "无效邮箱验证码!");

        errorList.Add(20004, "无效密码格式·!");
        errorList.Add(20005, "密码不匹配!");

        errorList.Add(20006, "您没有登录权限!");

        errorList.Add(20030, "无效参数(s)!");
        errorList.Add(20032, "密码不匹配!");
        errorList.Add(20034, "您没有权限!");
        errorList.Add(20037, "已存在.!");
        errorList.Add(20100, "请您登录游戏!");

        errorList.Add(20200, "没找到游戏房间!");
        errorList.Add(20201, "游戏已经开始了!");
        errorList.Add(20300, "没找到游戏用户!");
        errorList.Add(20400, "输入的底注价无效!");
        errorList.Add(20401, "您的账户余额不够!");
        errorList.Add(20402, "请输入转账金额!");
        errorList.Add(20403, "请输入账户ID!");
        
        errorList.Add(20500, "请输入银行名称!");
        errorList.Add(20501, "请输入卡号!");
        errorList.Add(20502, "请输入卡住姓名!");
        
        errorList.Add(20511, "请输入支付宝账户!");
        errorList.Add(20512, "请输入本人姓名!");
        
        errorList.Add(20520, "首先绑定银行账户!");
        errorList.Add(20521, "首先绑定支付宝账户!");
        errorList.Add(20522, "请设置提现额度!");
        errorList.Add(20523, "提现额度不正确!");

        errorList.Add(32000, "系统报错.");
        errorList.Add(0, "未知错误");
        errorList.Add(-1, "网络断开!");

        errorList.Add(32700, "Invalid JSON was received by the server. An error occurred on the server while parsing the JSON text.");
        errorList.Add(32600, "The JSON sent is not a valid Request object.");
        errorList.Add(32601, "The method does not exist / is not available.");
        errorList.Add(32602, "Invalid method parameter(s).");
        errorList.Add(32603, "Internal JSON-RPC error.");
        errorList.Add(32604, "Internal Authentication Method.");

        // //login
        // errorList.Add(20004, "密码长度必须6个字以上。");
        // errorList.Add(20001, "无效邮箱格式");
        // errorList.Add(20005, "登录密码不匹配");
        // errorList.Add((int)ERROR_TYPE.LOGING_NO_ACTIVE, "您的账户没激活。");
        // errorList.Add((int)ERROR_TYPE.REGISTER_PASSWORD_INVALID, "密码格式错误");
        // errorList.Add((int)ERROR_TYPE.REGISTER_USER_INVALID, "您已注册");
        // //create room
        // errorList.Add((int)ERROR_TYPE.DATA_FORMAT_INVALID, "时间格式无效!!");
        // errorList.Add((int)ERROR_TYPE.USER_INVALID, "无效用户!!");
        // errorList.Add((int)ERROR_TYPE.REQUEST_INVALID_COST, "底注范围是 0.5 - 20 !!");
        // errorList.Add((int)ERROR_TYPE.LACK_BALANCE, "您的余额不够!!");
        // errorList.Add((int)ERROR_TYPE.VERIFY_EMAIL, "请再输入正确验证码!!");
        // errorList.Add((int)ERROR_TYPE.USER_INROOM, "请等待当前回合完成。");

    }

}
