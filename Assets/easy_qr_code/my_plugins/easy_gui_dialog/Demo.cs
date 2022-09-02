using UnityEngine;
using System.Collections;

namespace epoching.easy_gui
{
    public class Demo : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
        }
        //事件-----------------------------------------------------
        #region
        //监听toast 按钮 
        public void on_toast_btn_event()
        {
            Canvas_toast.toast("Hello, This is a toast", 48, 1f);
        }

        //监听alert 按钮 
        public void on_alert_btn_event()
        {
            Canvas_alert.alert("Hint", "Hello，This is an alert", "confirm");

            //Z_confirm_box.confirm_box("confirm box", "Hello，This is a confirm box", "cancel", "yes", delegate () { }, delegate ()
            //{
            //    print("sssssssssssssssss");
            //});
        }

        //监听confirm_box 按钮 
        public void on_confirm_box_btn_event()
        {
            Canvas_confirm_box.confirm_box("confirm box", "Hello，This is a confirm box", "cancel", "yes", delegate () { }, delegate ()
             {
                 print("yes button has been clicked!");
             });
        }

        //监听confirm_box 按钮 
        public void on_input_dialog_event()
        {
            // Canvas_input

            Canvas_input_dialog.input_dialog("提示", "确定", "请输入密码", "number", delegate (string str)
            {

                print(">>>>" + str);

            });

            //Canvas_confirm_box.confirm_box("confirm box", "Hello，This is a confirm box", "cancel", "yes", delegate () { }, delegate ()
            //{
            //    print("yes button has been clicked!");
            //});
        }
        #endregion
    }
}