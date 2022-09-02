using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace epoching.easy_debug_on_the_phone
{

    public enum ColorType
    {
        normal,
        warn,
        error
    }
    public class DebugControl : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
    {
        private bool isDebug = false;
        private Text txt;
        private ScrollRect scrollRect;

        private static DebugControl _instance;

        public Button clearBtn;

        public static DebugControl Instance
        {
            get
            {
                return _instance;
            }
        }

        void Awake()
        {
            _instance = this;

            //DebugConfig config = Resources.Load<DebugConfig>("DebugConfig") as DebugConfig;
            //if (config == null)
            //{
            //    Debug.LogError("检查是否存在该.asset,没有请点击CreatAsset." + "DebugConfig");
            //    return;
            //}
            //isDebug = config.isDebug;
            int layer = LayerMask.NameToLayer("IgonerUI");
            if (this.gameObject.layer != layer)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(0).gameObject.layer = layer;
                }
            }

            scrollRect = GameObject.Find("Debug_Scroll View").GetComponent<ScrollRect>();
            txt = GameObject.Find("Debug_Text").GetComponent<Text>();
        }

        void Start()
        {
            if (clearBtn == null)
            {
                clearBtn = GameObject.Find("ClearBtn").GetComponent<Button>();
            }
            if (clearBtn != null)
                clearBtn.onClick.AddListener(OnClearTextBtnClick);
        }


        public void AddOneDebug(string content, ColorType color = ColorType.normal, int fontSize = 24)
        {
            if (string.IsNullOrEmpty(content) /*|| !isDebug*/)
                return;
            //当未传颜色过来默认使用灰色
            //scrollRect.enabled = !scrollRect.enabled;
            string str;
            switch (color)
            {
                case ColorType.normal:
                    str = "<color=#c0c0c0>" + "<size=" + fontSize.ToString() + ">" + content + "</size></color>\n";
                    txt.text += str;
                    break;
                case ColorType.warn:
                    str = "<color=#dbdb70>" + "<size=" + fontSize.ToString() + ">" + content + "</size></color>\n";
                    txt.text += str;
                    break;
                case ColorType.error:
                    str = "<color=#ff0000>" + "<size=" + fontSize.ToString() + ">" + content + "</size></color>\n";
                    txt.text += str;
                    break;
            }
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0;
            Canvas.ForceUpdateCanvases();
            //scrollRect.enabled = false;
            //txt.text = "<color=#0000ff><size=60>小明</size></color>送了<color=#0000ff><size=60>小红</size></color>一辆游艇";
        }

        public void OnClearTextBtnClick()
        {
            txt.text = "";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //PassEvent(eventData, ExecuteEvents.submitHandler);
            PassEvent(eventData, ExecuteEvents.pointerClickHandler);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            PassEvent(eventData, ExecuteEvents.pointerDownHandler);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PassEvent(eventData, ExecuteEvents.pointerUpHandler);
        }

        public void PassEvent<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function) where T : IEventSystemHandler
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(data, results);
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.layer == LayerMask.NameToLayer("IgonerUI"))
                {
                    continue;
                }
                //RaycastAll后ugui会自己排序，如果你只想响应透下去的最近的一个响应，这里ExecuteEvents.Execute后直接break就行
                if (results[i].gameObject.GetComponent<Button>() != null)
                {
                    ExecuteEvents.Execute(results[i].gameObject, data, function);
                    break;
                }
            }

        }
    }
}