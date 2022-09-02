using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace epoching.easy_gui
{
    public class Canvas_alert : MonoBehaviour
    {
        [Header("弹出框的标题，内容，确定按钮上的字")]
        public Text text_title;
        public Text text_content;
        public Text text_confirm_str;

        [Header("提示框面板图片")]
        public Image image_panel;

        //重构
        //public static void alert(string content)
        //{
        //    GameObject go = Resources.Load<GameObject>("Canvas_alert");

        //    go.GetComponent<Canvas_alert>().init
        //        (
        //        I2.Loc.LocalizationManager.GetTranslation("Prompt"),
        //        content,
        //        I2.Loc.LocalizationManager.GetTranslation("Confirm")
        //        );
        //    Instantiate(go);
        //}

        //到处都可以调用的显示对话框的静态函数
        public static void alert(string title, string content, string confirm_str)
        {
            GameObject go = Resources.Load<GameObject>("Canvas_alert");
            go.GetComponent<Canvas_alert>().init(title, content, confirm_str);
            Instantiate(go);
        }

        //public static void alert(string title)
        //{
        //    GameObject go = Resources.Load<GameObject>("Canvas_alert");
        //    go.GetComponent<Canvas_alert>().init(title);
        //    Instantiate(go);
        //}

        // Use this for initialization
        void OnEnable()
        {
            //show the toast
            StartCoroutine(Canvas_grounp_fade.show(this.gameObject));
        }

        /// <summary>
        /// "content"  means box's content
        /// </summary>
        public void init(string content)
        {
            ////set the width and height
            this.GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);

            //1.set the title
            this.text_title.text = "提示";

            //2.set the content
            this.text_content.text = content;

            //3.set the button text
            this.text_confirm_str.text = "确定";

            //4.set the size
            float scale = Screen.width / 1334f;
            this.image_panel.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
        }

        /// <summary>
        /// "title"  means box's title
        /// "content"  means box's content
        /// "confirm_str"  means  the confirm button text
        /// </summary>
        public void init(string title, string content, string confirm_str)
        {
            ////set the width and height
            this.GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);

            //1.set the title
            this.text_title.text = title;

            //2.set the content
            this.text_content.text = content;

            //3.set the button text
            this.text_confirm_str.text = confirm_str;

            //4.set the size
            float scale = Screen.width / 1334f;
            this.image_panel.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
        }

        //listen the confirm button
        public void on_confirm_btn_event()
        {
            //播放按钮声音
            //Audio_control.instance.play_btn_sound();

            StartCoroutine(Canvas_grounp_fade.hide(this.gameObject, true));
        }
    }

}