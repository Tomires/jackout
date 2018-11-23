using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OculusTeleport : MonoBehaviour {
	public GameObject cameraRig;
	public GameObject pointer;
	public float headHeight = 1.75f;

	void Start() {
		XRSettings.eyeTextureResolutionScale = 1.5f;
	}

	void Update () {
		RaycastHit hit;
		if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity)) {
			if(!pointer.activeInHierarchy)
				pointer.SetActive(true);

			pointer.transform.position = hit.point;
			pointer.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
			pointer.transform.localPosition -= 0.025f * Vector3.Normalize(hit.point - transform.position);
			pointer.transform.localScale = Vector3.Distance(hit.point, transform.position) * 0.03f * Vector3.one;

			if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) {
				cameraRig.transform.position = pointer.transform.position + new Vector3(0,headHeight,0);
			}
		}
		else {
			pointer.SetActive(false);
		}	
	}
}
