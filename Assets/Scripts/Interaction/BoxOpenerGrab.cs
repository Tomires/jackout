using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Interaction {
    public class BoxOpenerGrab : BaseGrab
    {
        public GameObject electricBoxDoor, electricBoxKeyhole;
        public Logic.ElectricBoxControl electricBoxControl;
        public Vector3 holePositionOffset, holeRotationOffset;
        private bool openerInHole = false;

        private void OnTriggerEnter(Collider col) {
            if(col.gameObject == electricBoxDoor) {
                transform.position = electricBoxDoor.transform.position;
                openerInHole = true;
            }
        }

        private void OnTriggerExit(Collider col) {
            if(col.gameObject == electricBoxDoor) {
                transform.localPosition = Vector3.zero;
                openerInHole = false;
            }
        }

        public override void PickedUp() {
            
        }

        public override void Dropped() {
            if(openerInHole) {
                transform.parent = electricBoxKeyhole.transform;
                transform.position += holePositionOffset;
                transform.rotation *= Quaternion.Euler(holeRotationOffset);
                Destroy(GetComponent<ObjectGrabbable>()); /* key should no longer be grabbable once in the hole */
            }
        }
    }
}