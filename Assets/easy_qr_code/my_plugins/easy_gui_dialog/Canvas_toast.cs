using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace epoching.easy_gui
{
    public class Canvas_toast : MonoBehaviour
    {
        //toast show time
        public float show_time = 1.5f;

         static GameObject data ;

        /// <summary>
        /// "str"  means content that toast show time,
        /// "font_size"  means  font size.
        /// "show_time"  means  how long it will show.
        /// </summary>
        public static void toast(string str, int font_size, float show_time = 1.5f)
        {
            if (data != null)
            {
                Canvas_grounp_fade.hide(data, true);
            }
            GameObject go = Resources.Load<GameObject>("Canvas_toast");
            go.GetComponent<Canvas_toast>().init(str, font_size, show_time);
           
            
            data=go;
            Instantiate(go);
        }

        /// <summary>
        /// "str"  means content that toast show time,
        /// "show_time"  means  how long it will show.
        /// </summary>
        public static void toast(string str, float show_time = 2f)
        {
            //if (data != null)
            //{
            //    Canvas_grounp_fade.hide(data, true);
            //}


            GameObject go = Resources.Load<GameObject>("Canvas_toast");
            go.GetComponent<Canvas_toast>().init(str, show_time);

          
         //   data = go;
            Instantiate(go);
        }

        // Use this for initialization
        void Start()
        {
            //show the toast
            StartCoroutine(Canvas_grounp_fade.show(this.gameObject));

            //delay disappear
            StartCoroutine(this.hide(this.gameObject, this.show_time));
        }

        // Use this for initialization
        public void init(string str, float show_time = 1.5f)
        {
            ////set the canvas width and height
            this.GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);

            //set the show time
            this.show_time = show_time;

            //get the text and background
            GameObject game_object_text = this.transform.Find("Text").gameObject;
            GameObject game_object_bg = this.transform.Find("bg").gameObject;

            //set the fontsize and text content
            Text text = game_object_text.GetComponent<Text>();
            text.GetComponent<Text>().fontSize = (int)(Screen.width / 1080f * 48);
            text.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, 34);
            text.GetComponent<Text>().text = str;

            //set the background and text position
            game_object_text.transform.localPosition = new Vector3(0, -Screen.height / 4, 0);
            game_object_bg.transform.localPosition = new Vector3(0, -Screen.height / 4, 0);

            //set the text size and background size
            float width = text.preferredWidth + 24;
            float height = text.preferredHeight + 24;
            text.GetComponent<RectTransform>().sizeDelta = new Vector2(text.preferredWidth, text.preferredHeight);
            game_object_bg.GetComponent<RectTransform>().sizeDelta = new Vector3(width, height);
        }

        // Use this for initialization
        public void init(string str, int font_size = 20, float show_time = 1.5f)
        {
            //set the canvas width and height
            this.GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);

            //set the show time
            this.show_time = show_time;

            //get the text and background
            GameObject game_object_text = this.transform.Find("Text").gameObject;
            GameObject game_object_bg = this.transform.Find("bg").gameObject;

            //set the fontsize and text content
            Text text = game_object_text.GetComponent<Text>();
            text.GetComponent<Text>().fontSize = font_size;
            text.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, 24);
            text.GetComponent<Text>().text = str;


            //set the background and text position
            game_object_text.transform.localPosition = new Vector3(0, -Screen.height / 4, 0);
            game_object_bg.transform.localPosition = new Vector3(0, -Screen.height / 4, 0);

            //set the text size and background size 
            float width = text.preferredWidth + 16;
            float height = text.preferredHeight + 16;
            text.GetComponent<RectTransform>().sizeDelta = new Vector2(text.preferredWidth, text.preferredHeight);
            game_object_bg.GetComponent<RectTransform>().sizeDelta = new Vector3(width, height);
        }

        //hide the object
        IEnumerator hide(GameObject go, float show_time)
        {
            yield return new WaitForSeconds(show_time);

            //hide the object
            
            StartCoroutine(Canvas_grounp_fade.hide(go, true));
        }
    }
}