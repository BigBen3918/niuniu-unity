using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace epoching.easy_gui
{
    public class Canvas_input_dialog : MonoBehaviour
    {

        [Header("弹出框的标题，确定按钮上的字")]
        public Text text_title;
        public Text text_confirm_str;

        [Header("输入框")]
        public InputField input_field;


        [Header("提示框面板图片")]
        public Image image_panel;

        //点击确定按钮的事件
        public static hander_one_argument hander_confirm;

        //[Header("提示框面板图片")]
        //public Image image_panel;

        /// <summary>
        /// "text_type"  password  number  text
        /// </summary>
        public static void input_dialog(string title, string confirm_str, string placeholder, string text_type, hander_one_argument hander_confirm)
        {
            GameObject go = Resources.Load<GameObject>("Canvas_input_dialog");
            go.GetComponent<Canvas_input_dialog>().init(title, confirm_str, placeholder, text_type, hander_confirm);
            Instantiate(go);
        }

        // Use this for initialization
        void OnEnable()
        {
            //show the toast
            StartCoroutine(Canvas_grounp_fade.show(this.gameObject));
        }

        /// <summary>
        /// "title"  means box's title
        /// "content"  means box's content
        /// "confirm_str"  means  the confirm button text
        /// </summary>
        public void init(string title, string confirm_str, string placeholder, string text_type, hander_one_argument hander_confirm)
        {
            ////set the width and height
            this.GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);

            //1.set the title
            this.text_title.text = title;

            //2.set the button text
            this.text_confirm_str.text = confirm_str;

            //3.set inputfield placeholder
            this.input_field.placeholder.GetComponent<Text>().text = placeholder;

            //4.set inputfield type
            switch (text_type)
            {
                case "password":
                    this.input_field.contentType = InputField.ContentType.Password;
                    break;

                case "number":
                    this.input_field.contentType = InputField.ContentType.IntegerNumber;
                    break;

                case "text":
                    this.input_field.contentType = InputField.ContentType.Standard;
                    break;

                default:
                    break;
            }

            //5.set the size
            float scale = Screen.width / 1334f;
            this.image_panel.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);

            //6.set the hander
            Canvas_input_dialog.hander_confirm = hander_confirm;
        }

        //listen close btn
        public void on_close_btn_event()
        {
            StartCoroutine(Canvas_grounp_fade.hide(this.gameObject, true));
        }

        //listen the confirm button
        //public void on_confirm_btn_event()
        //{
        //    if (this.input_field.text == "")
        //    {
        //        Canvas_toast.toast(I2.Loc.LocalizationManager.GetTranslation("Input cannot be empty"));
        //        return;
        //    }
        //    //播放按钮声音
        //    if (Audio_control.instance != null)
        //        Audio_control.instance.play_btn_sound();

        //    //隐藏当前面板
        //    StartCoroutine(Canvas_grounp_fade.hide(this.gameObject, true));

        //    //执行传入的事件
        //    Canvas_input_dialog.hander_confirm(this.input_field.text);
        //}
    }

    //定义一个可以传递一个字符串的参数
    public delegate void hander_one_argument(string str);
}
