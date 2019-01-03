using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Logic {
	public class ElevatorControl : MonoBehaviour {
		public StateControl stateControl;
		public Interaction.ElevatorPuzzleButton[] puzzleButtons;
		public GateControl elevatorDoor1, elevatorDoor2;
		public Interaction.ElevatorFloorButton secondFloorButton;
		public AudioSource audioSource;
		public AudioClip audioElevatorEngine;
		public float timeToReachDestination = 10.0f;
		private float animationStep;
		private bool inMotion = false;
		private bool currentlyOnUpperFloor = false;

		void Setup () {
			animationStep = timeToReachDestination;
			audioSource.loop = true;
		}

		void Update () {
			if(inMotion) {
				if(animationStep < (timeToReachDestination - 0.5f) && animationStep > (elevatorDoor1.openingTime + 0.5f)) {
					audioSource.clip = audioElevatorEngine;
					audioSource.Play();
				}
				else if(animationStep > (timeToReachDestination - 0.5f)) {
					audioSource.Stop();
					elevatorDoor1.Switch(true);
					elevatorDoor2.Switch(true);
				}
				animationStep += Time.deltaTime;
			}
		}

		public void GoToFloor(bool upperFloor) {
			if(currentlyOnUpperFloor == upperFloor) {
				return;
			}

			currentlyOnUpperFloor = upperFloor;
			inMotion = true;
			animationStep = 0.0f;

			if(upperFloor) {
				stateControl.ChangeState(Shared.State.AptArrivedUpperFloor);
			}
			else {
				stateControl.ChangeState(Shared.State.AptArrivedLowerFloor);
			}

			elevatorDoor1.Switch(false);
			elevatorDoor2.Switch(false);
		}

		public void CheckButtonStates() {
			/* if we reach a specific combination, unlock button for second floor */
			if(puzzleButtons[0].currentIcon == 0 &&
				puzzleButtons[1].currentIcon == 1 &&
				puzzleButtons[2].currentIcon == 2 &&
				puzzleButtons[3].currentIcon == 3) {
				
				secondFloorButton.SetEnabled(true);
			}
			else {
				secondFloorButton.SetEnabled(false);
			}
		}
	}
}