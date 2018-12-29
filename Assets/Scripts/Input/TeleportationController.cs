using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Input {
	public class TeleportationController : MonoBehaviour {
		public GameObject teleportationRing;
		public GameObject cameraRig;
		public string teleportLayerName = "TeleportBoundary";
		private Vector3 teleportTarget;
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
			Debug.Log("init");
			RaycastHit hit;
			if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity)) {
				if(!teleportationRing.activeInHierarchy)
					teleportationRing.SetActive(true);
				Debug.Log(hit.point);
				teleportationRing.GetComponent<TeleportRingController>().MoveRing(hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
				/*teleportationRing.transform.position = hit.point;
				#teleportationRing.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
				#teleportationRing.transform.localPosition -= 0.025f * Vector3.Normalize(hit.point - transform.position);
				teleportationRing.transform.localScale = Vector3.Distance(hit.point, transform.position) * 0.03f * Vector3.one;

				/*if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) {
					cameraRig.transform.position = teleportationRing.transform.position + new Vector3(0,headHeight,0);
				}*/
				
				bool teleportationAllowed = (hit.transform.gameObject.layer == LayerMask.NameToLayer(teleportLayerName));
				teleportationRing.GetComponent<TeleportRingController>().SetAllowed(teleportationAllowed);
				teleportTarget = hit.point;
			}
			else {
				teleportationRing.SetActive(false);
			}
		}

		public void ConcludeTeleport() {
			teleportationRing.SetActive(false);
			warpingNow = true;
		}
	}
}