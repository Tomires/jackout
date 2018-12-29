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
		private TBInput.Controller controller;
		private bool teleportInitiated = false;

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
			/* trigger is pulled toward or away from player -> teleport */
			Vector2 joystickPosition = TBInput.GetAxis2D(TBInput.Button.Joystick, controller);
			if(Mathf.Abs(joystickPosition.y) > 0.6) {
				actionTeleportation = true;
			}

			if(controllerHand == ControllerHand.RightController) actionTeleportation = true;

			if(actionTeleportation) {
				teleportationController.InitiateTeleport();
				teleportInitiated = true;
			}
			else if(teleportInitiated) {
				teleportationController.ConcludeTeleport();
				teleportInitiated = false;
			}
		}
	}
}
