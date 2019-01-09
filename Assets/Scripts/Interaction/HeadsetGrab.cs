using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TButt;

namespace Jackout.Interaction {
    public class HeadsetGrab : BaseGrab
    {
        public GameObject colorOverlay;
        public Material[] brightColors;
        public Logic.StateControl stateControl;
        public GameObject[] disableWhenFlashing;
        public AudioSource globalAudio;
        public AudioClip shortBeep, longBeep;
        private bool headsetPicked = false;
        private float timeout = 3.0f;
        public override void PickedUp() {
            headsetPicked = true;
        }

        public override void Dropped() {
            
        }

        void Update() {
            if(headsetPicked) {
                timeout -= Time.deltaTime;

                if(timeout < 0.0f) {
                    stateControl.ChangeState(Shared.State.Ending);
                    colorOverlay.SetActive(false);
                    globalAudio.clip = longBeep;
                    globalAudio.Play();
                    for(int i = 0; i < disableWhenFlashing.Length; i++) {
                        disableWhenFlashing[i].SetActive(true);
                    }
                    headsetPicked = false;
                }
                else if(timeout < 2.0f) {
                    int color = Random.Range(0,brightColors.Length * 2);
                    int dice = Random.Range(0,5);
                    if(dice <= 1) {
                        if(color >= brightColors.Length) {
                            colorOverlay.SetActive(false);
                            for(int i = 0; i < disableWhenFlashing.Length; i++) {
                                disableWhenFlashing[i].SetActive(true);
                            }
                        }
                        else {
                            for(int i = 0; i < disableWhenFlashing.Length; i++) {
                                disableWhenFlashing[i].SetActive(false);
                            }
                            colorOverlay.SetActive(true);
                            globalAudio.clip = shortBeep;
                            globalAudio.Play();
                            colorOverlay.GetComponent<Renderer>().material = brightColors[color];
                        }
                    }
                }
            }
        }
    }
}