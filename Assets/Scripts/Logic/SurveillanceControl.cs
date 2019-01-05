using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Logic {
    public class SurveillanceControl : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip audioPrompt, audioShutter;
        public GameObject cameraRotatingPart;
        public GameObject cameraRig;
        private TicketBarrierControl awaitingBarrierControl;
        private Quaternion initialRotation;
        bool active = false;

        void Start () {
            initialRotation = cameraRotatingPart.transform.rotation;
        }

        void Update () {
            Vector3 direction = cameraRig.transform.position - cameraRotatingPart.transform.position;
            Vector3 newDirection = Vector3.RotateTowards(cameraRotatingPart.transform.forward, direction, Time.deltaTime, 0.0f);
            cameraRotatingPart.transform.rotation = Quaternion.LookRotation(newDirection);
        }

        public void LookForPlayer(TicketBarrierControl barrier) {
            active = true;
            awaitingBarrierControl = barrier;
            audioSource.clip = audioPrompt;
            audioSource.loop = true;
            audioSource.Play();
        }

        public void StopLooking() {
            active = false;
            audioSource.Stop();
            cameraRotatingPart.transform.rotation = initialRotation;
        }

        private void OnTriggerEnter(Collider col) {
            if(col.tag == "CameraCollider") {
                if(active) {
                    audioSource.clip = audioShutter;
                    audioSource.loop = false;
                    audioSource.Play();
                    awaitingBarrierControl.CameraLookedAt();
                }
            }
        }
    }
}