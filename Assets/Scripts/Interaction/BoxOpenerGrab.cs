using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Interaction {
    public class BoxOpenerGrab : BaseGrab
    {
        public GameObject electricBoxDoor;
        public Logic.ElectricBoxControl electricBoxControl;
        public Vector3 holePosition, holeRotation, holeScale;
        private bool openerInHole = false;
        private Transform controller;

        private void OnTriggerEnter(Collider col) {
            if(col.gameObject == electricBoxDoor) {
                transform.parent = electricBoxDoor.transform;
                transform.localPosition = holePosition;
                transform.localRotation = Quaternion.Euler(holeRotation);
                transform.localScale = holeScale;
                openerInHole = true;
            }
        }

        private void OnTriggerExit(Collider col) {
            if(col.gameObject == electricBoxDoor) {
                transform.parent = controller;
                transform.localPosition = Vector3.zero;
                openerInHole = false;
            }
        }

        public override void PickedUp() {
            controller = transform.parent;
        }

        public override void Dropped() {
            if(openerInHole) {
                transform.parent = electricBoxDoor.transform;
                transform.localPosition = holePosition;
                transform.localRotation = Quaternion.Euler(holeRotation);
                transform.localScale = holeScale;
                Destroy(GetComponent<ObjectGrabbable>()); /* key should no longer be grabbable once in the hole */
                Destroy(GetComponent<BoxCollider>());
                electricBoxControl.Unlock();
            }
        }
    }
}