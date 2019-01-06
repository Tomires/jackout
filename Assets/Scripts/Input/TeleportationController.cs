using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Input {
	public class TeleportationController : MonoBehaviour {
		public GameObject teleportationRing;
		public GameObject cameraRig;
		public string teleportLayerName = "TeleportBoundary";
		public float shiftIncrement = 45.0f;
		public Vector3 teleportationOffset;
		private Vector3 teleportTarget;
		private bool teleportationAllowed = true;
		private bool warpingNow = false;
		private float animationStep = 0f;
		void Start () {
			
		}

		void Update () {
			if(warpingNow && animationStep < 1.0f) {
				Vector3 target = teleportTarget;
				target.y = cameraRig.transform.position.y; /* do not change Y position */
				cameraRig.transform.position = Vector3.Lerp(cameraRig.transform.position, target, Jackout.Shared.LerpSin(animationStep));
				animationStep += Time.deltaTime;
			}
			else {
				animationStep = 0.0f;
				warpingNow = false;
			}
		}

		public void InitiateTeleport() {
			if(warpingNow) {
				return;
			}

			RaycastHit hit;
			if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity) && isNormalGround(hit.normal, 5.0f)) {
				if(!teleportationRing.activeInHierarchy) {
					teleportationRing.SetActive(true);
				}

				teleportationRing.GetComponent<TeleportRingController>().MoveRing(hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
				
				teleportationAllowed = (hit.transform.gameObject.layer == LayerMask.NameToLayer(teleportLayerName));
				teleportationRing.GetComponent<TeleportRingController>().SetAllowed(teleportationAllowed);
				teleportTarget = hit.point + teleportationOffset;
			}
			else {
				teleportationRing.SetActive(false);
			}
		}

		public void ConcludeTeleport() {
			teleportationRing.SetActive(false);

			if(warpingNow || !teleportationAllowed) {
				return;
			}
			warpingNow = true;
		}

		public void ShiftLeft() {
			cameraRig.transform.rotation *= Quaternion.Euler(0, -shiftIncrement, 0);
		}

		public void ShiftRight() {
			cameraRig.transform.rotation *= Quaternion.Euler(0, shiftIncrement, 0);
		}

		private bool isNormalGround(Vector3 normal, float margin) {
			bool x = Shared.isInRange(Mathf.Abs(normal.x), 0.0f, margin);
			bool y = Shared.isInRange(Mathf.Abs(normal.y), 1.0f - margin, 1.0f);
			bool z = Shared.isInRange(Mathf.Abs(normal.z), 0.0f, margin);
			return x && y && z;
		}
	}
}