using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Interaction {
    public class PhoneGrab : BaseGrab
    {
        public AudioSource audioSource;
        public AudioClip audioRinging, audioCall;
        private bool callAnswered = false;

        public void startRinging() {
            audioSource.loop = true;
            audioSource.clip = audioRinging;
            audioSource.Play();
        }

        public override void PickedUp() {
            if(!callAnswered) {
                callAnswered = true;
                audioSource.loop = false;
                audioSource.clip = audioCall;
                audioSource.maxDistance = 1.0f;
                audioSource.Play();
            }
        }

        public override void Dropped() {
            audioSource.Stop();
        }
    }
}