using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Interaction {
	public class ObjectGrabbable : MonoBehaviour {
	private Vector3 initialPosition;
	private Quaternion initialRotation;

		void Start () {
			initialPosition = transform.position;
			initialRotation = transform.rotation;
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}