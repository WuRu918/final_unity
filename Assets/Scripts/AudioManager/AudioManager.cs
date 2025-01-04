using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    List<AudioSource> audios = new List<AudioSource>();

    void Start(){
        for(int i=0;i<5;i++){
            var audio = this.gameObject.AddComponent<AudioSource>();
            audios.Add(audio);
        }
    }
    void Update(){

    }
    
}
