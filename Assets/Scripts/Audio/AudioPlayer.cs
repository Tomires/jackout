using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Audio {
    public class AudioPlayer : MonoBehaviour
    {
        AudioSource audioSource;
        AudioClip audioClip;
        bool loop = false;

        void Start()
        {
            audioSource.clip = audioClip;
        }

        void Update()
        {
            
        }
    }
}