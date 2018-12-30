using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Logic {
	public class TicketBarrierControl : MonoBehaviour {
		private bool gateOpened = false;
		private float animationStep = 1.0f;
		private float closeTimeout = 5.0f;
		public GameObject[] doors;
		public float[] openedOffsets;
		public Shared.Axis rotationAxis;
		private Vector3 rotationAxisInEuler; 
		private List<Quaternion> initialRotation;
		private float timeout;

		void Start () {
			initialRotation = new List<Quaternion>();

			foreach(GameObject door in doors) {
				initialRotation.Add(door.transform.rotation);
			}

			switch(rotationAxis) {
				case Shared.Axis.X:
					rotationAxisInEuler = new Vector3(1, 0, 0);
					break;
				case Shared.Axis.Y:
					rotationAxisInEuler = new Vector3(0, 1, 0);
					break;
				default:
					rotationAxisInEuler = new Vector3(0, 0, 1);
					break;
			}
		}

		void Update () {
			if(animationStep < 1.0f) {
				for(int d = 0; d < doors.Length; d++) {
					Quaternion target = initialRotation[d];
					if(gateOpened) {
						target *= Quaternion.Euler(rotationAxisInEuler * openedOffsets[d]);
					}
					doors[d].transform.rotation = Quaternion.Lerp(doors[d].transform.rotation, target, animationStep);
				}
				animationStep += Time.deltaTime;
			}
			else if(gateOpened) {
				timeout -= Time.deltaTime;
				if(timeout < 0) {
					Switch();
				}
			}
		}

		public void Switch() {
			gateOpened = !gateOpened;
			animationStep = 0.0f;
			timeout = closeTimeout;
		}
	}
}