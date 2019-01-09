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
		}

		private void OnCollisionEnter(Collision col) {
			Debug.Log(col.gameObject.tag);
			if(col.gameObject.CompareTag("Access")) {
				TBCameraRig.instance.nearClipPlane = 0.1f;
				TBCameraRig.instance.farClipPlane = 0.2f;
				Debug.Log("ON");
			}
		}

		private void OnCollisionExit(Collision col) {
			if(col.gameObject.CompareTag("Access")) {
				TBCameraRig.instance.farClipPlane = defaultFarClipPlane;
				TBCameraRig.instance.nearClipPlane = defaultNearClipPlane;
				Debug.Log("OFF");
			}
		}
	}
}