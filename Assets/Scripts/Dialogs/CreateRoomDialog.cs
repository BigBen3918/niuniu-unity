using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomDialog : Dialog
{
	public InputField antes;
	public static CreateRoomDialog instance;
	// Start is called before the first frame update
	void Start()
	{
		instance = this;
	}

	// Update is called once per frame
	void Update() {
		
	}

	void AddItem() {

	}

	// public void Show() {
	// 	GetComponent<Animator>().SetBool("show", true);
	// }

	// public void Hide(){
	// 	GetComponent<Animator>().SetBool("show", false);
	// }

	public void OnCreateRoom(){
		try {
			if ("".Equals(antes.text)) {
				UI.instance.ErrorMessage("请输入底注!");
			} else {
				var iAntes = float.Parse(antes.text);
				if (iAntes < 0.5 && iAntes > 20) {
					UI.instance.ErrorMessage("底注是 0.5 ~ 20.");
				} else {
					NetworkManager.instance.Send("create-room", new string[]{"0", antes.text});
					UI.instance.ShowLoading(true);
				}
			}
			
		} catch (System.Exception) {
			UI.instance.ErrorMessage("底注是数字");
		}
	}

    public override void Show()
    {
        base.Show();
    }
}
