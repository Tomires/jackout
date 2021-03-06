﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Interaction {
	public class ObjectGrabbable : MonoBehaviour {
		public bool grabbed = false;
		public Material dopplegangerMaterial;
		public BaseGrab grabBehaviour;
		private Vector3 initialPosition;
		private Quaternion initialRotation;
		private Transform parent;
		private GameObject doppleganger;
		private float animationStep = 1.0f;
		private Vector3 targetPosition;
		private Quaternion targetRotation;
		

		void Start () {
			initialPosition = transform.position;
			initialRotation = transform.rotation;
			parent = gameObject.transform.parent.gameObject.transform;
		}
		
		void Update () {
			if(animationStep < 1.0f) {
				transform.position = Vector3.Lerp(transform.position, targetPosition, animationStep);				
				transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, animationStep);
				animationStep += Time.deltaTime;
			}
			/* else if(grabbed){
				transform.localPosition = Vector3.zero;
			} */
		}

		void OnTriggerEnter(Collider col) {
			/* visual cue */
		}

		public void attachToController(GameObject controller) {
			doppleganger = Instantiate(gameObject, initialPosition, initialRotation);
			foreach(Renderer r in doppleganger.GetComponentsInChildren<Renderer>()) {
				r.material = dopplegangerMaterial;
			}

			Destroy(doppleganger.GetComponent<ObjectGrabbable>());
			Destroy(doppleganger.GetComponentInChildren<PhoneGrab>());
			Destroy(doppleganger.GetComponentInChildren<AudioSource>());

			transform.SetParent(controller.transform);
			/* targetPosition = controller.transform.position;
			targetRotation = transform.rotation;
			animationStep = 0.0f; */
			transform.localPosition = Vector3.zero;
			grabbed = true;

			if(grabBehaviour != null) {
				grabBehaviour.PickedUp();
			}
		}

		public void returnToInitialLocation() {
			transform.SetParent(parent);
			targetPosition = initialPosition;
			targetRotation = initialRotation;
			animationStep = 0.0f;

			Destroy(doppleganger);
			grabbed = false;

			if(grabBehaviour != null) {
				grabBehaviour.Dropped();
			}
		}
	}
}