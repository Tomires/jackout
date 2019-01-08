using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Interaction {
    public class HeadsetGrab : BaseGrab
    {
        public GameObject cameraRig;
        public Logic.StateControl stateControl;
        private bool headsetOnHead = false;
        public override void PickedUp() {
            
        }

        public override void Dropped() {
            if(headsetOnHead) {
                stateControl.ChangeState(Shared.State.Ending);
            }
        }

        private void OnTriggerEnter(Collider col) {
            if(col.gameObject == cameraRig) {
                headsetOnHead = true;
            }
        }

        private void OnTriggerExit(Collider col) {
            if(col.gameObject == cameraRig) {
                headsetOnHead = false;
            }
        }
    }
}