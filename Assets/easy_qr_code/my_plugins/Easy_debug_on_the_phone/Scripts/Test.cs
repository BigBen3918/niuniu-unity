using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace epoching.easy_debug_on_the_phone
{
    public class Test : MonoBehaviour
    {

        public void on_log_normal_btn_click()
        {
            E_debug.log("This is a normal log");
        }

        public void on_log_error_btn_click()
        {
            E_debug.log_error("This is a error log");
        }

        public void on_log_warn_btn_click()
        {
            E_debug.log_warn("This is a warn log");
        }

        public void on_log_bigger_size_btn_click()
        {
            E_debug.log("This is a bigger fontsize log",56);
        }

        public void on_log_smaller_size_btn_click()
        {
            E_debug.log("This is a smaller fontsize log", 32);
        }

    }
}
