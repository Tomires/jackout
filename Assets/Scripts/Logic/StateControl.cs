using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Logic {
	public class StateControl : MonoBehaviour {
		public GameObject movementOutside, movementStationTicket, movementStationInside, movementApartmentHallway, movementElevator, movementApartmentInside, movementPhoneBooth;
		public GameObject colliderOutside, colliderStationTicket, colliderStationInside, colliderApartmentHallway, colliderElevator, colliderApartmentInside;
		public GameObject aptUpperFloorWall, aptBlockEntryDoor, aptRoomEntryDoor, elevatorCallButton;
		public GameObject stationGate1, stationGate2, stationLight1, stationLight2, stationIntercom;
		public GameObject computerCamera, computerStatic;
		public Interaction.PhoneGrab telephone;
		public GameObject cameraRig, endCube;
		public Shared.State currentState = Shared.State.Initial;

		void Start () {
			UpdateScene();
			telephone.startRinging();
		}

		public void ChangeState(Shared.State nextState) {
			currentState = nextState;
			UpdateScene();
		}

		private void UpdateScene() {
			switch(currentState) {
				case Shared.State.ElectricalBox1:
					/* open up station */
					stationGate1.GetComponent<GateControl>().Switch(true);
					stationGate2.GetComponent<GateControl>().Switch(true);
					stationLight1.GetComponent<LightControl>().Switch(true);
					stationLight2.GetComponent<LightControl>().Switch(true);
					stationIntercom.SetActive(true);

					/* front area of station is accessible */
					movementStationTicket.SetActive(true);
					colliderStationTicket.SetActive(true);
					break;

				case Shared.State.StationBarrierOpen:
					/* area behind ticket barriers is accessible */
					movementStationInside.SetActive(true);
					colliderStationInside.SetActive(true);
					break;

				case Shared.State.ElectricalBox2:
					/* computer in apartment shows camera feed */
					computerCamera.SetActive(true);
					computerStatic.SetActive(false);
					break;

				case Shared.State.ElevatorCalled:
					/* inside of elevator is accessible */
					movementElevator.SetActive(true);
					colliderElevator.SetActive(true);
					break;

				case Shared.State.AptArrivedUpperFloor:
					/* apartment door is unlocked */
					aptRoomEntryDoor.GetComponent<Interaction.ObjectRotateable>().Enable();

					/* change elevator call button sprite to down arrow*/
					elevatorCallButton.GetComponent<Interaction.ElevatorCallButton>().ChangeButtonSprite(false);

					/* show wall with painting instead of door */
					aptBlockEntryDoor.SetActive(false);
					aptUpperFloorWall.SetActive(true);

					/* apartment is accessible, outside is not */
					movementOutside.SetActive(false);
					movementPhoneBooth.SetActive(false);
					movementApartmentInside.SetActive(true);
					colliderOutside.SetActive(false);
					colliderApartmentInside.SetActive(true);
					break;
				case Shared.State.AptArrivedLowerFloor:
					/* apartment door is locked */
					aptRoomEntryDoor.GetComponent<Interaction.ObjectRotateable>().Disable();

					/* change elevator call button sprite to up arrow */
					elevatorCallButton.GetComponent<Interaction.ElevatorCallButton>().ChangeButtonSprite(true);

					/* hide wall, show entry door */
					aptBlockEntryDoor.SetActive(true);
					aptUpperFloorWall.SetActive(false);

					/* outside is accessible, apartment is not */
					movementOutside.SetActive(true);
					movementPhoneBooth.SetActive(true);
					movementApartmentInside.SetActive(false);
					colliderOutside.SetActive(true);
					colliderApartmentInside.SetActive(false);
					break;

				case Shared.State.Initial:
					/* close down station */
					stationGate1.GetComponent<GateControl>().Switch(false);
					stationGate2.GetComponent<GateControl>().Switch(false);
					stationLight1.GetComponent<LightControl>().Switch(false);
					stationLight2.GetComponent<LightControl>().Switch(false);
					stationIntercom.SetActive(false);

					/* computer in apartment shows static */
					computerCamera.SetActive(false);
					computerStatic.SetActive(true);

					/* no station area is accessible */
					movementStationTicket.SetActive(false);
					movementStationInside.SetActive(false);
					colliderStationTicket.SetActive(false);
					colliderStationInside.SetActive(false);

					/* outside is accessible */
					movementOutside.SetActive(true);
					movementPhoneBooth.SetActive(true);
					colliderOutside.SetActive(true);

					/* apartment door is locked */
					aptRoomEntryDoor.GetComponent<Interaction.ObjectRotateable>().Disable();
					break;
				case Shared.State.Ending:
					cameraRig.transform.position = endCube.transform.position;
					cameraRig.transform.rotation = Quaternion.Euler(0, 0, 0);
					break;
			}
		}
	}
}
