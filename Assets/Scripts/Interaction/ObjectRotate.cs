using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Interaction {
	public class ObjectRotate : MonoBehaviour {
		public bool rotationEnabled = false;
		public Shared.Axis rotationAxis = Shared.Axis.X;
		public float allowedOffset = 90.0f; /* in degrees */
		public GameObject hinge;
		private Quaternion initialRotation;
		private GameObject pivot;
		private bool inRotation = false;

		void Start () {
			if(hinge == null) {
				hinge = gameObject;
			}

			initialRotation = transform.rotation;
		}
		
		void Update () {
			if(inRotation) {
				/* TODO */
			}	
		}

		public void RotateAroundPivot(GameObject _pivot) {
			pivot = _pivot;
			inRotation = true;
		}

		public void StopRotating() {
			inRotation = false;
		}
	}
}