using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace epoching.easy_gui
{
    public class Canvas_confirm_box : MonoBehaviour
    {
        [Header("title，content，cancel button text，confirm button text")]
        public Text text_title;
        public Text text_content;
        public Text text_cancel_str;
        public Text text_confirm_str;
        public Text text_confirm_str_must;

        [Header("background")]
        public Image image_panel;

        [Header("confirm button")]
        public Button confirm_btn;

        [Header("cancel button")]
        public Button cancel_btn;

        [Header("confirm button must")]
        public Button confirm_btn_must;

        //点击取消按钮的事件
        public static Handler hander_cancel;

        //点击确定按钮的事件
        public static Handler hander_confirm;

   

        //重构一个默认的双语弹出面板
        //public static void confirm_box(string content, Handler handler_cancel, Handler handler_confirm)
        //{
        //    GameObject go = Resources.Load<GameObject>("Canvas_confirm_box");
        //    go.GetComponent<Canvas_confirm_box>().init(I2.Loc.LocalizationManager.GetTranslation("Prompt"), 
        //       content,
        //       I2.Loc.LocalizationManager.GetTranslation("Cancel"),
        //       I2.Loc.LocalizationManager.GetTranslation("Confirm"),
        //       handler_cancel, handler_confirm); 
        //    Instantiate(go);
        //}

        //public static void confirm_box(string content, bool is_must, Handler handler_cancel, Handler handler_confirm)
        //{
        //    GameObject go = Resources.Load<GameObject>("Canvas_confirm_box");
        //    go.GetComponent<Canvas_confirm_box>().init(I2.Loc.LocalizationManager.GetTranslation("Prompt"),
        //       content,
        //       I2.Loc.LocalizationManager.GetTranslation("Cancel"),
        //       I2.Loc.LocalizationManager.GetTranslation("Confirm"),
        //       is_must,
        //       handler_cancel, handler_confirm);
        //    //go.GetComponent<Canvas_confirm_box>().confirm_btn.onClick.AddListener(test);
        //    Instantiate(go);
        //}


        /// <summary>
        /// "title"  means box's title.
        /// "content"  means box's content.
        /// "cancel_str" means the cancel button text.
        /// "confirm_str"  means  the confirm button text.
        /// </summary>
        public static void confirm_box(string title, string content, string cancel_str,
            string confirm_str, Handler handler_cancel, Handler handler_confirm)
        {
            GameObject go = Resources.Load<GameObject>("Canvas_confirm_box");
            go.GetComponent<Canvas_confirm_box>().init(title, content, cancel_str, confirm_str, handler_cancel, handler_confirm);
            //go.GetComponent<Canvas_confirm_box>().confirm_btn.onClick.AddListener(test);
            Instantiate(go);

           
        }

        /// <summary>
        /// "title"  means box's title.
        /// "content"  means box's content.
        /// "cancel_str" means the cancel button text.
        /// "confirm_str"  means  the confirm button text.
        /// "is_must"  means  must click confirm.
        /// </summary>
        public static void confirm_box(string title, string content, string cancel_str,
            string confirm_str, bool is_must, Handler handler_cancel, Handler handler_confirm)
        {
            GameObject go = Resources.Load<GameObject>("Canvas_confirm_box");
            go.GetComponent<Canvas_confirm_box>().init(title, content, cancel_str, confirm_str, is_must, handler_cancel, handler_confirm);
            //go.GetComponent<Canvas_confirm_box>().confirm_btn.onClick.AddListener(test);
            Instantiate(go);
        }


        // Use this for initialization
        void OnEnable()
        {
            this.GetComponent<Canvas>().worldCamera = Camera.main;

            //show the confirm box
            StartCoroutine(Canvas_grounp_fade.show(this.gameObject));
        }

        /// <summary>
        /// "title"  means box's title.
        /// "content"  means box's content.
        /// "cancel_str" means the cancel button text.
        /// "confirm_str"  means  the confirm button text.
        /// </summary>
        public void init(string title, string content, string cancel_str,
            string confirm_str, Handler hander_cancel, Handler hander_confirm)
        {
            ////set the canvas width and height
            this.GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);

            //1.set the title
            this.text_title.text = title;

            //2.set the content
            this.text_content.text = content;

            //4.set the cancel button text
            this.text_cancel_str.text = cancel_str;

            //4.set the confirm button text
            this.text_confirm_str.text = confirm_str;
            this.text_confirm_str_must.text = confirm_str;

            //4.set the size
            float scale = Screen.width / 1440f;
            this.image_panel.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);

            //set the hander
            Canvas_confirm_box.hander_cancel = hander_cancel;
            Canvas_confirm_box.hander_confirm = hander_confirm;

            this.cancel_btn.gameObject.SetActive(true);
            this.confirm_btn.gameObject.SetActive(true);
            this.confirm_btn_must.gameObject.SetActive(false);
        }

        public void init(string title, string content, string cancel_str,
          string confirm_str, bool is_must, Handler hander_cancel, Handler hander_confirm)
        {
            ////set the canvas width and height
            this.GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);

            //1.set the title
            this.text_title.text = title;

            //2.set the content
            this.text_content.text = content;

            //4.set the cancel button text
            this.text_cancel_str.text = cancel_str;

            //4.set the confirm button text
            this.text_confirm_str.text = confirm_str;
            this.text_confirm_str_must.text = confirm_str;

            //4.set the size
            float scale = Screen.width / 1334f;
            this.image_panel.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);

            //set the hander
            Canvas_confirm_box.hander_cancel = hander_cancel;
            Canvas_confirm_box.hander_confirm = hander_confirm;

            if (is_must == true)
            {
                this.cancel_btn.gameObject.SetActive(false);
                this.confirm_btn.gameObject.SetActive(false);
                this.confirm_btn_must.gameObject.SetActive(true);
            }
            else
            {
                this.cancel_btn.gameObject.SetActive(true);
                this.confirm_btn.gameObject.SetActive(true);
                this.confirm_btn_must.gameObject.SetActive(false);
            }

            this.confirm_btn.onClick.AddListener(on_yes_btn_event);
        }

        //listen the cancel button 
        public void on_cancel_btn_event()
        {
            //播放音效
           // Audio_control.instance.play_btn_sound();

            //隐藏窗口
            StartCoroutine(Canvas_grounp_fade.hide(this.gameObject, true));

            //执行传入的事件
            Canvas_confirm_box.hander_cancel();
        }

        //listen the confirm button
        public void on_yes_btn_event()
        {
            //播放音效
            //Audio_control.instance.play_btn_sound();

            //隐藏窗口
            StartCoroutine(Canvas_grounp_fade.hide(this.gameObject, true));

            //执行传入的事件
            Canvas_confirm_box.hander_confirm();
        }
    }
    //delegate
    public delegate void Handler();
}
