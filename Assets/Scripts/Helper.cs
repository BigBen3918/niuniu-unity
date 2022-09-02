// by Leo Pawel <https://github.com/galaxy126>
// at 10/07/2022

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
// using System.Text.Encoding;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class ItemType {
    public string k;
    public string v;
}

public class Helper {

	private static Dictionary<string, string> errors;
	public static void SetupLocale(string locale) {
		var json = Resources.Load<TextAsset>(@"locals/" + locale).ToString();
		errors = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
	}

	public static bool validateEmail(string email) {
		Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
		Match match = regex.Match(email);
		return match.Success;
	}

	public static string getError(int code) {
		return errors["error." + code];
	}

	public static DateTime UnixTimeStampToDateTime(int unixTimeStamp) {
		// Unix timestamp is seconds past epoch
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		dateTime = dateTime.AddSeconds( (double)unixTimeStamp ).ToLocalTime();
		return dateTime;
	}

	public static Texture2D ParseTexture(string txtureString){
        /*string b64_string = loadFromSomewhere;*/
        byte[] b64_bytes = System.Convert.FromBase64String(txtureString);
        Texture2D tex = new Texture2D(100,100);
        tex.LoadImage( b64_bytes);
        return tex;
        
    }

	public static string popString(List<string> list){
        string result = list[0];
        list.RemoveAt(0);
        return result;
    }
	public static string FormatNumber(string value){
		try {
			var num = float.Parse(value);
			if (num > 1000) {
				return "+" + (Math.Floor(num / 100) / 10) + "K";
			} else {
				return (Math.Floor(num * 10) / 10).ToString();
			}
		} catch (System.Exception) {
		}
        return "0";
    }

	public static Texture2D LoadPNG(string filePath) {
 
     Texture2D tex = null;
     byte[] fileData;
 
     if (File.Exists(filePath))     {
         fileData = File.ReadAllBytes(filePath);
         tex = new Texture2D(100, 100);
         tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
     }
     return tex;
 }
}
