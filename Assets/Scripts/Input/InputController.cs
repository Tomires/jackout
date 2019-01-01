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

		void Start () {
			if(controllerHand == ControllerHand.LeftController) {
				controller = TBInput.Controller.LHandController;
			}
			else {
				controller = TBInput.Controller.RHandController;
			}
		}

		void Update () {
			bool actionTeleportation = false;
			bool shiftLeft = false;
			bool shiftRight = false;
			
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
		}
	}
}
