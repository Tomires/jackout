using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Logic {
	public class GateControl : MonoBehaviour {
		public Vector3 openedOffset;
		public float openingTime = 5.0f;
		private bool gateOpened = false;
		private float animationStep;
		public AudioClip audioOpen;
		public bool audioLoop;
		public AudioSource audioSource;
		private Vector3 initialPosition;
		private Vector3 target;
		private Vector3 origin;

		void Start () {
			initialPosition = transform.position;
			animationStep = openingTime;
			audioSource.loop = audioLoop;
		}

		void Update () {
			if(animationStep < openingTime) {
				transform.position = Vector3.Lerp(origin, target, animationStep / openingTime);
				animationStep += Time.deltaTime;
			}
			else {
				audioSource.Stop();
			}
		}

		public void Switch(bool opened) {
			if(gateOpened == opened) {
				return;
			}

			gateOpened = opened;
			animationStep = 0.0f;

			audioSource.clip = audioOpen;
			audioSource.Play();

			if(gateOpened) {
				target = initialPosition;
				origin = initialPosition + openedOffset;
			}
			else {
				target = initialPosition + openedOffset;
				origin = initialPosition;
			}
		}
	}
}