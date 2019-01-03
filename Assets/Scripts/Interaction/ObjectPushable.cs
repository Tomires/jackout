using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Interaction {
	public class ObjectPushable : MonoBehaviour {
		public Vector3 pushOffset;
		public AudioClip audioPush;
		public AudioSource audioSource;
		public BaseButton buttonBehaviour;
		private Vector3 initialPosition;
		private float animationStep = 1.0f;
		private bool pushed = false;

		void Start () {
			initialPosition = transform.position;	
		}

		void Update () {
			if(animationStep < 1.0f) {
				if(pushed) {
					transform.position = Vector3.Lerp(initialPosition, initialPosition + pushOffset, animationStep);
				}
				else {
					transform.position = Vector3.Lerp(initialPosition + pushOffset, initialPosition, animationStep);
				}
				animationStep += Time.deltaTime * 7.0f;
			}
			else if(pushed){
				/* return button to initial position after push */
				pushed = false;
				animationStep = 0.0f;
			}
		}

		public void Use() {
			buttonBehaviour.Action();

			if(audioPush != null) {
				audioSource.clip = audioPush;
				audioSource.Play();
			}

			pushed = true;
			animationStep = 0.0f;
		}
	}
}