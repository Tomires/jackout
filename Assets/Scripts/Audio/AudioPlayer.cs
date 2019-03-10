using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Audio {
    public class AudioPlayer : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip[] audioClips;
        public bool loop = false;
        public float[] timeouts;
        public float[] initialDelays;
        private List<float> timeToPlay;

        void Start()
        {
            timeToPlay = new List<float>();
            foreach(float delay in initialDelays) {
                timeToPlay.Add(delay);
            }
        }

        void Update()
        {
            for(int i = 0; i < audioClips.Length; i++) {
                timeToPlay[i] -= Time.deltaTime;

                if(timeToPlay[i] < 0) {
                    if(loop) {
                        timeToPlay[i] = timeouts[i];
                    }
                    else {
                        timeToPlay[i] = int.MaxValue;
                    }
                    audioSource.clip = audioClips[i];
                    audioSource.Play();
                }
            }
        }
    }
}