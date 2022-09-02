using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shapes2D;
using SimpleFileBrowser;

enum SelectStat{
    avata,
    name,
    password
}
public class SettingDialog : Dialog
{
    public static SettingDialog instance;
    public InputField userNameUI;
    public InputField currentPasswordUI;
    public InputField passwordUI;
    public InputField password1UI;
    public Shape avataUI;
    private Texture2D avata;
    SelectStat stat;

    // Start is called before the first frame update
    void Start() {
        instance = this;
        stat = SelectStat.avata;
    }

    void Update(){
        if(FileBrowser.Success){
            FileBrowser.Success = false;
            SetAavata(FileBrowser.Result[0]);
        }
    }



    // Update is called once per frame
    public void OnSubmit(){
        if(stat == SelectStat.name){
            if (userNameUI.text.Equals("")) {
                UI.instance.ErrorMessage("没找到");
                return;
            }
            NetworkManager.instance.Send("update-alias", new string[]{userNameUI.text});
        }else if(stat == SelectStat.password){
            
            if (password1UI.text.Equals("") || passwordUI.text.Equals("") || currentPasswordUI.text.Equals("")) {
                UI.instance.ErrorMessage("没找到");
                return;
            }
            if(password1UI.text != passwordUI.text){
                UI.instance.ErrorMessage("确认密码不匹配");
                return;
            }
            NetworkManager.instance.Send("update-password", new string[]{currentPasswordUI.text, password1UI.text});
            
        }else if(stat == SelectStat.avata){
            byte[] textureBytes = avataUI.settings.fillTexture.EncodeToPNG();
            string textureBytesEncodedAsBase64  = System.Convert.ToBase64String(textureBytes);
            NetworkManager.instance.Send("update-avata", new string[]{textureBytesEncodedAsBase64});
        }
        
    }

    public void OnAvata(){
        stat = SelectStat.avata;
    }
    public void OnName(){
        stat = SelectStat.name;
    }

    public void OnPassword(){
        stat = SelectStat.password;
    }

    public void OnShow(){
        avataUI.settings.fillTexture = TopInfoPanel.instance.avataTexture;
        base.Show();
    }

    public void SetAavata(string path){
        print("deleget");
        Texture2D newAvata = Helper.LoadPNG(path);
        newAvata = Resize(newAvata, 100, 100);
        if(newAvata == null){
            UI.instance.ErrorMessage("No exist image");
        }else{
            FileBrowser.HideDialog();
            avataUI.settings.fillTexture = newAvata;
        }
    }

    Texture2D Resize(Texture2D texture2D,int targetX,int targetY)
    {
        RenderTexture rt=new RenderTexture(targetX, targetY,24);
        RenderTexture.active = rt;
        Graphics.Blit(texture2D,rt);
        Texture2D result=new Texture2D(targetX,targetY);
        result.ReadPixels(new Rect(0,0,targetX,targetY),0,0);
        result.Apply();
        return result;
    }
}
