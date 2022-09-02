using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace epoching.easy_debug_on_the_phone
{
    public class E_debug : MonoBehaviour
    {
        //log normal
        public static void log(string str,int fontsize=48)
        {
            DebugControl.Instance.AddOneDebug(str, ColorType.normal, fontsize);
        }
        //log error
        public static void log_error(string str, int fontsize = 48)
        {
            DebugControl.Instance.AddOneDebug(str, ColorType.error, fontsize);
        }
        //log error
        public static void log_warn(string str, int fontsize = 48)
        {
            DebugControl.Instance.AddOneDebug(str, ColorType.warn, fontsize);
        }
    }
}