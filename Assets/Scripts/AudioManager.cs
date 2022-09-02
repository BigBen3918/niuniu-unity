using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Serializable]
    public struct NamedAudio {
        public string name;
        public AudioClip audio;
    }
    public AudioSource audioSource;
    public NamedAudio[] audios;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShotOneAudio(string key){
        foreach(var item in audios){
            if(item.name == key)
                audioSource.PlayOneShot(item.audio, 1.0f);
        }
        
    }

}
