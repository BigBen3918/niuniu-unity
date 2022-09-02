using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
    物体setactive时 的渐变效果
*/
namespace epoching.easy_gui
{
    public class Canvas_grounp_fade : MonoBehaviour
    {
        public static float fade_time = 0.3141592653f;

        //fade
        public static IEnumerator fade(GameObject obj, float from, float to)
        {
            if (to == 1)
                obj.SetActive(true);

            CanvasGroup canvas_group = obj.GetComponent<CanvasGroup>();

            float duration = Canvas_grounp_fade.fade_time;


            float elaspedTime = 0f;
            while (elaspedTime <= duration)
            {
                elaspedTime += Time.deltaTime;
                canvas_group.alpha = Mathf.Lerp(from, to, elaspedTime / duration);
                yield return null;
            }
            canvas_group.alpha = to;

            if (to == 0)
            {
                obj.SetActive(false);
            }
        }

        //show //0 不管 1 暂停 2 恢复
        public static IEnumerator show(GameObject obj)
        {
            float from = 0;
            float to = 1;

            obj.SetActive(true);

            CanvasGroup canvas_group = obj.GetComponent<CanvasGroup>();

            float duration = Canvas_grounp_fade.fade_time;

            float elaspedTime = 0f;
            while (elaspedTime <= duration)
            {
                elaspedTime += Time.deltaTime;
                canvas_group.alpha = Mathf.Lerp(from, to, elaspedTime / duration);
                yield return null;
            }
            canvas_group.alpha = to;
        }

        /// <summary>
        /// "is_destory"  means is destory when hided
        /// </summary>
        public static IEnumerator hide(GameObject obj, bool is_destory = false)
        {
            float from = 1;
            float to = 0;

            CanvasGroup canvas_group = obj.GetComponent<CanvasGroup>();

            float duration = Canvas_grounp_fade.fade_time;

            float elaspedTime = 0f;
            while (elaspedTime <= duration)
            {
                elaspedTime += Time.deltaTime;
                canvas_group.alpha = Mathf.Lerp(from, to, elaspedTime / duration);
                yield return null;
            }
            canvas_group.alpha = to;

            obj.SetActive(false);

            if (is_destory == true)
            {
                Destroy(obj);
            }
        }
    }
}