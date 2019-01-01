using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Input {
	public class TeleportRingController : MonoBehaviour {

		public Material allowedMaterial;
		public Material deniedMaterial;

		void Start () {
		}
		
		void Update () {
			
		}

        public void MoveRing(Vector3 position, Quaternion rotation)
        {
			transform.position = position;
			transform.rotation = Quaternion.Euler(90,0,0) * rotation;
        }

		public void SetAllowed(bool allowed) {
			Material material;
			if(allowed) {
				material = allowedMaterial;
			}
			else {
				material = deniedMaterial;
			}

			foreach(Renderer r in GetComponentsInChildren<Renderer>()) {
				r.material = material;
			}
		}
    }
}
