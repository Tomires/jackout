using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TButt;

namespace Jackout.Access {
	public class BoundaryControl : MonoBehaviour {
		private float defaultNearClipPlane;
		private float defaultFarClipPlane;

		void Start () {
			defaultNearClipPlane = TBCameraRig.instance.nearClipPlane;
			defaultFarClipPlane = TBCameraRig.instance.farClipPlane;
			transform.parent = TBCameraRig.instance.GetCenter();
		}

		private void OnTriggerEnter(Collider col) {
			if(col.gameObject.CompareTag("Access")) {
				TBCameraRig.instance.nearClipPlane = 0.1f;
				TBCameraRig.instance.farClipPlane = 0.2f;
			}
		}

		private void OnTriggerExit(Collider col) {
			if(col.gameObject.CompareTag("Access")) {
				TBCameraRig.instance.farClipPlane = defaultFarClipPlane;
				TBCameraRig.instance.nearClipPlane = defaultNearClipPlane;
			}
		}
	}
}