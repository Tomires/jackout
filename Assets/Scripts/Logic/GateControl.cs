using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Logic {
	public class GateControl : MonoBehaviour {
		public Vector3 openedOffset;
		public float openingTime = 5.0f;
		private bool gateOpened = false;
		private float animationStep;
		public AudioClip audioOpen, audioClose;
		public bool audioLoop;
		public AudioSource audioSource;
		public bool autoClose = false;
		public float closeInterval = 10.0f;
		private float timeToClose;
		private Vector3 initialPosition;
		private Vector3 target;
		private Vector3 origin;

		void Start () {
			initialPosition = transform.position;
			animationStep = openingTime;
			audioSource.loop = audioLoop;
			timeToClose = closeInterval;

			if(audioClose == null) {
				audioClose = audioOpen;
			}
		}

		void Update () {
			if(animationStep < openingTime) {
				transform.position = Vector3.Lerp(origin, target, animationStep / openingTime);
				animationStep += Time.deltaTime;
			}
			else {
				audioSource.Stop();
			}

			if(autoClose) {
				if(timeToClose > 0.0f && gateOpened) {
					timeToClose -= Time.deltaTime;
				}
				else if(gateOpened) {
					Switch(false);
				}
			}
		}

		public void Switch(bool opened) {
			if(gateOpened == opened) {
				return;
			}

			gateOpened = opened;
			animationStep = 0.0f;

			audioSource.clip = gateOpened ? audioOpen : audioClose;
			audioSource.Play();

			if(!gateOpened) {
				target = initialPosition;
				origin = initialPosition + openedOffset;
			}
			else {
				target = initialPosition + openedOffset;
				origin = initialPosition;
				timeToClose = closeInterval;
			}
		}
	}
}