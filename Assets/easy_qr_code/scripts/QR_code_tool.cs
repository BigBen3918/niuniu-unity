using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using epoching.easy_debug_on_the_phone;
using UnityEngine.UI;
using epoching.easy_gui;


public class QR_code_tool : MonoBehaviour
{
    [Header("raw_image_video")]
    public RawImage raw_image_video;

    //camera texture
    private WebCamTexture cam_texture;

    void Start()
    {

        Canvas_confirm_box.confirm_box("confirm box", "No camera detected", "cancel", "OK", true, delegate () { }, delegate ()
        {
            print("yes button has been clicked!");
        });

        return;

        try
        {
            //init camera texture
            this.cam_texture = new WebCamTexture();

            //if (cam_texture != null)
            //{
                this.cam_texture.Play();


                if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    this.raw_image_video.rectTransform.sizeDelta = new Vector2(Screen.width * cam_texture.width / (float)this.cam_texture.height, Screen.width);
                    this.raw_image_video.rectTransform.rotation = Quaternion.Euler(0, 0, -90);
                }
                else
                {
                    this.raw_image_video.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.width * this.cam_texture.height / (float)this.cam_texture.width);
                }

                this.raw_image_video.texture = cam_texture;
            //}
        }
        catch (Exception ex)
        {

            print(ex);

            Canvas_confirm_box.confirm_box("confirm box", "No camera detected", "cancel", "OK", true, delegate () { }, delegate ()
            {
                print("yes button has been clicked!");
            });

            throw;
        }

       







    }

    private float interval_time = 1f;
    private float time_stamp = 0;
    void Update()
    {
        this.time_stamp += Time.deltaTime;

        if (this.time_stamp > this.interval_time)
        {
            this.time_stamp = 0;

            try
            {
                IBarcodeReader barcodeReader = new BarcodeReader();
                // decode the current frame
                var result = barcodeReader.Decode(this.cam_texture.GetPixels32(), this.cam_texture.width, this.cam_texture.height);
                if (result != null)
                {
                    //Debug.Log("DECODED TEXT FROM QR: " + result.Text);

                    E_debug.log("DECODED TEXT FROM QR: " + result.Text);
                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
        }
    }


    void OnGUI()
    {
        // drawing the camera on screen
        //GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);

        // do the reading — you might want to attempt to read less often than you draw on the screen for performance sake
      
    }
}

