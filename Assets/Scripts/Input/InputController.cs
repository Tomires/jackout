using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TButt;

namespace Jackout.Input {
	public class InputController : MonoBehaviour {
		public enum ControllerHand {
			LeftController,
			RightController
		}

		public Jackout.Input.TeleportationController teleportationController;
		public ControllerHand controllerHand;
		public float joystickThreshold = 0.6f;
		private TBInput.Controller controller;
		private bool teleportInitiated = false;
		private bool shiftInitiated = false;
		private bool grabPossible = false;
		private bool objectGrabbed = false;
		private GameObject grabbedObject;

		void Start () {
			if(controllerHand == ControllerHand.LeftController) {
				controller = TBInput.Controller.LHandController;
			}
			else {
				controller = TBInput.Controller.RHandController;
			}
		}

		void Update () {
			bool actionTeleportation, shiftLeft, shiftRight, actionInteract, actionRelease;
			DetectInputs(out actionTeleportation, out shiftLeft, out shiftRight, out actionInteract, out actionRelease);

			if(actionTeleportation) {
				teleportationController.InitiateTeleport();
				teleportInitiated = true;
			}
			else if(teleportInitiated) {
				teleportationController.ConcludeTeleport();
				teleportInitiated = false;
			}

			if(shiftLeft) {
				teleportationController.ShiftLeft();
				shiftLeft = false;
			}
			else if(shiftRight) {
				teleportationController.ShiftRight();
				shiftRight = false;
			}

			if(actionInteract && grabPossible && !objectGrabbed) {
				grabbedObject.GetComponent<Interaction.ObjectGrabbable>().attachToController(gameObject);
				GetComponent<MeshRenderer>().enabled = false;
				objectGrabbed = true;
			}
			else if(actionRelease && objectGrabbed) {
				grabbedObject.GetComponent<Interaction.ObjectGrabbable>().returnToInitialLocation();
				GetComponent<MeshRenderer>().enabled = true;
				objectGrabbed = false;
			}

		}

		private void OnTriggerEnter(Collider col) {
			if(col.gameObject.GetComponent<Interaction.ObjectGrabbable>() && !col.gameObject.GetComponent<Interaction.ObjectGrabbable>().grabbed) {
				/* visual cue */
				grabbedObject = col.gameObject;
				grabPossible = true;
			}
		}

		private void OnTriggerExit(Collider col) {
			grabPossible = false;
		}

		private void DetectInputs(out bool actionTeleportation, out bool shiftLeft, out bool shiftRight, out bool actionInteract, out bool actionRelease) {
			actionTeleportation = false;
			shiftLeft = false;
			shiftRight = false;
			actionInteract = false;
			actionRelease = false;

			/* joystick is pulled toward or away from player -> teleport */
			Vector2 joystickPosition = TBInput.GetAxis2D(TBInput.Button.Joystick, controller);
			if(Mathf.Abs(joystickPosition.y) > joystickThreshold) {
				actionTeleportation = true;
			}
			/* joystick is pulled to the right -> blink shift to right */
			else if(joystickPosition.x > joystickThreshold) {
				if(!shiftInitiated) {
					shiftRight = true;
					shiftInitiated = true;
				}
			}
			/* joystick is pulled to the left -> blink shift to left */
			else if(joystickPosition.x < -joystickThreshold) {
				if(!shiftInitiated) {
					shiftLeft = true;
					shiftInitiated = true;
				}
			}
			else {
				shiftInitiated = false;
			}

			/* trigger is pressed -> interact with object */
			if(TBInput.GetButtonDown(TBInput.Button.PrimaryTrigger, controller)) {
				actionInteract = true;
			}
			
			/* trigger is released -> release object */
			if(TBInput.GetButtonUp(TBInput.Button.PrimaryTrigger, controller)) {
				actionRelease = true;
			}
		}
	}
}
