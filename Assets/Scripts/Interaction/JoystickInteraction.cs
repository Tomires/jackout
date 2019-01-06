using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Interaction {
    public class JoystickInteraction : MonoBehaviour
    {
        public float bound = 0.5f;
        public float maxStickRotation = 15.0f;
        public GameObject stickBase;
        public Logic.CameraPanControl securityCamera;
        private bool grabbed = false;
        private GameObject controller;
        private Vector3 initialControllerPosition;
        private Quaternion initialStickRotation;
        void Start()
        {
            initialStickRotation = stickBase.transform.rotation;
        }

        void Update()
        {
            if(grabbed) {
                Vector3 positionOffset = initialControllerPosition - controller.transform.position;

                /* clamp and normalize offsets */
                float posx = Shared.Map(positionOffset.x, -bound, bound) / bound;
                float posy = Shared.Map(positionOffset.z, -bound, bound) / bound;

                stickBase.transform.rotation = initialStickRotation * Quaternion.Euler(-posy * maxStickRotation, 0, posx * maxStickRotation);
                
                /* find out which axis has a larger offset */
                if(Mathf.Abs(posx) > Mathf.Abs(posy)) {
                    if(Mathf.Abs(posx) > 0.2f) {
                        securityCamera.Pan(posx);
                    }
                    
                }
                else {
                    if(Mathf.Abs(posy) > 0.2f) {
                        securityCamera.Zoom(posy);
                    }
                }
            }
        }

        public void Grabbed(GameObject _controller) {
            grabbed = true;
            controller = _controller;
            initialControllerPosition = _controller.transform.position;
        }

        public void Dropped() {
            grabbed = false;
            stickBase.transform.rotation = initialStickRotation;
        }
    }
}