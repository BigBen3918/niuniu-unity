using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using epoching.easy_gui;

namespace epoching.easy_qr_code
{
    public class Demo_control : MonoBehaviour
    {
        [Header("audio source")]
        public AudioSource audio_source;

        //singleton
        public static Demo_control instance;

        //public WebCamTexture cam_texture;

        private Demo_statu demo_statu;  //the statu of the demo

        [Header("main gameobject, read_qr_code gameobject,generate_qr_code gameobject")]
        public GameObject game_obj_main;
        public GameObject game_obj_read_qr_code;
        public GameObject game_obj_generate_qr_code;

        void Awake()
        {
            Demo_control.instance = this;

            this.change_to_main();
        }

        void Start()
        {
            new WebCamTexture(WebCamTexture.devices[0].name);

            //WebCamTexture cam_texture = new WebCamTexture();
            //cam_texture.Play();
        }

        public void change_to_main()
        {
            StartCoroutine(Canvas_grounp_fade.hide(this.game_obj_read_qr_code));
            StartCoroutine(Canvas_grounp_fade.hide(this.game_obj_generate_qr_code));

            StartCoroutine(Canvas_grounp_fade.show(this.game_obj_main));


            this.demo_statu = Demo_statu.main;
        }

        public void change_to_read_qr_code()
        {
            StartCoroutine(Canvas_grounp_fade.hide(this.game_obj_main));
            StartCoroutine(Canvas_grounp_fade.hide(this.game_obj_generate_qr_code));

            StartCoroutine(Canvas_grounp_fade.show(this.game_obj_read_qr_code));


            this.demo_statu = Demo_statu.read_qr_code;
        }

        public void change_to_generate_qr_code()
        {
            StartCoroutine(Canvas_grounp_fade.hide(this.game_obj_read_qr_code));
            StartCoroutine(Canvas_grounp_fade.hide(this.game_obj_main));

            StartCoroutine(Canvas_grounp_fade.show(this.game_obj_generate_qr_code));


            this.demo_statu = Demo_statu.generate_qr_code;
        }

        //event
        #region
        public void on_read_qr_code_btn()
        {
            this.change_to_read_qr_code();
            this.audio_source.Play();
        }

        public void on_generate_qr_code_btn()
        {
            this.change_to_generate_qr_code();
            this.audio_source.Play();
        }

        public void on_back_btn()
        {
            this.change_to_main();
            this.audio_source.Play();
        }
        #endregion
    }

    public enum Demo_statu
    {
        main,
        read_qr_code,
        generate_qr_code
    }
}
