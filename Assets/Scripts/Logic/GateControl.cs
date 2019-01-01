using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Logic {
	public class GateControl : MonoBehaviour {
		public Vector3 openedOffset;
		public float openingTime = 5.0f;
		private bool gateOpened = false;
		private float animationStep;
		private Vector3 initialPosition;
		private Vector3 target;
		private Vector3 origin;

		void Start () {
			initialPosition = transform.position;
			animationStep = openingTime;
		}

		void Update () {
			if(animationStep < openingTime) {
				transform.position = Vector3.Lerp(origin, target, animationStep / openingTime);
				animationStep += Time.deltaTime;
			}
		}

		public void Switch() {
			gateOpened = !gateOpened;
			animationStep = 0.0f;

			if(gateOpened) {
				target = initialPosition;
				origin = initialPosition + openedOffset;
			}
			else {
				target = initialPosition + openedOffset;
				origin = initialPosition;
			}
		}
	}
}