using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Logic {
	public class GateControl : MonoBehaviour {
		public Vector3 openedOffset;
		private bool gateOpened = false;
		private float animationStep = 1.0f;
		private Vector3 initialPosition;
		private Vector3 target;

		void Start () {
			initialPosition = transform.position;
		}

		void Update () {
			if(animationStep < 1.0f) {
				transform.position = Vector3.Lerp(transform.position, target, animationStep);
				animationStep += Time.deltaTime;
			}
			else {
				Switch();
			}
		}

		public void Switch() {
			gateOpened = !gateOpened;
			animationStep = 0.0f;

			target = initialPosition;
			if(gateOpened) {
				target += openedOffset;
			}
		}
	}
}