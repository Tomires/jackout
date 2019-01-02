using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Logic {
	public class StateControl : MonoBehaviour {
		public GameObject movementOutside, movementStationTicket, movementStationInside, movementApartmentHallway, movementElevator, movementApartmentInside, movementPhoneBooth;
		public GameObject colliderOutside, colliderStationTicket, colliderStationInside, colliderApartmentHallway, colliderApartmentElevator, colliderApartmentInside;
		public GameObject aptUpperFloorWall, aptBlockEntryDoor, aptRoomEntryDoor;
		public GameObject stationGate1, stationGate2, stationLight1, stationLight2, stationIntercom;
		public GameObject computerCamera, computerStatic;
		private enum State {
			Initial, ElectricalBox1, ElectricalBox2, StationBarrierOpen, ElevatorCalled, AptArrivedLowerFloor, AptArrivedUpperFloor
		}
		private State previousState = State.Initial;
		private State currentState = State.Initial;

		private void ChangeState(State nextState) {
			previousState = currentState;
			currentState = nextState;

			UpdateScene();
		}

		private void UpdateScene() {
			switch(currentState) {
				case State.ElectricalBox1:
					/* open up station */
					stationGate1.GetComponent<GateControl>().Switch(true);
					stationGate2.GetComponent<GateControl>().Switch(true);
					stationLight1.GetComponent<LightControl>().Switch(true);
					stationLight2.GetComponent<LightControl>().Switch(true);
					stationIntercom.SetActive(true);

					/* front area of station is accessible */
					movementStationTicket.SetActive(true);
					break;

				case State.StationBarrierOpen:
					/* area behind ticket barriers is accessible */
					movementStationInside.SetActive(true);
					break;

				case State.ElectricalBox2:
					/* computer in apartment shows camera feed */
					computerCamera.SetActive(true);
					computerStatic.SetActive(false);
					break;

				case State.ElevatorCalled:
					/* inside of elevator is accessible */
					movementElevator.SetActive(true);
					break;

				case State.AptArrivedUpperFloor:
					/* apartment door is unlocked */
					aptRoomEntryDoor.GetComponent<Interaction.ObjectRotateable>().Enable();

					/* show wall with painting instead of door */
					aptBlockEntryDoor.SetActive(false);
					aptUpperFloorWall.SetActive(true);

					/* apartment is accessible, outside is not */
					movementOutside.SetActive(false);
					movementPhoneBooth.SetActive(false);
					movementApartmentInside.SetActive(true);
					break;
				case State.AptArrivedLowerFloor:
					/* apartment door is locked */
					aptRoomEntryDoor.GetComponent<Interaction.ObjectRotateable>().Disable();

					/* hide wall, show entry door */
					aptBlockEntryDoor.SetActive(true);
					aptUpperFloorWall.SetActive(false);

					/* outside is accessible, apartment is not */
					movementOutside.SetActive(true);
					movementPhoneBooth.SetActive(true);
					movementApartmentInside.SetActive(false);
					break;

				case State.Initial:
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

					/* outside is accessible */
					movementOutside.SetActive(true);
					movementPhoneBooth.SetActive(true);

					/* apartment door is locked */
					aptRoomEntryDoor.GetComponent<Interaction.ObjectRotateable>().Disable();
					break;
			}
		}
	}
}
