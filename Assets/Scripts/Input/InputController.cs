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
		public float triggerThreshold = 0.7f;
		public Material interactiveMaterial;
		public GameObject analogRepresentation, triggerRepresentation;
		public Renderer renderer;
		public AudioSource globalAudio;
		public AudioClip grabSound;
		public AudioClip dropSound;
		private Material defaultMaterial;
		private TBInput.Controller controller;
		private bool teleportInitiated = false;
		private bool shiftInitiated = false;
		private bool grabPossible = false;
		private bool objectGrabbed = false;
		private bool rotatePossible = false;
		private bool objectInRotation = false;
		private bool pushPossible = false;
		private GameObject grabbedObject;
		private GameObject rotateableObject;
		private GameObject pushableObject;
		private bool triggerPushed = false;
		public float rumbleIntensity = 0.2f;
		private float rumbleBuffer = 0.0f;

		void Start () {
			if(controllerHand == ControllerHand.LeftController) {
				controller = TBInput.Controller.LHandController;
			}
			else {
				controller = TBInput.Controller.RHandController;
			}

			defaultMaterial = renderer.material;
		}

		void Update () {
			if(rumbleBuffer > 0.0f) {
				TBInput.SetRumble(controller, rumbleIntensity);
				rumbleBuffer -= Time.deltaTime;
			}
			else {
				TBInput.SetRumble(controller, 0.0f);
			}

			AnimateControllerModel();

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
				foreach(Renderer r in GetComponentsInChildren<Renderer>()) {
					r.enabled = false;
				}
				grabbedObject.GetComponent<Interaction.ObjectGrabbable>().attachToController(gameObject);
				globalAudio.clip = grabSound;
				globalAudio.Play();
				objectGrabbed = true;
			}
			else if(actionRelease && objectGrabbed) {
				grabbedObject.GetComponent<Interaction.ObjectGrabbable>().returnToInitialLocation();
				foreach(Renderer r in GetComponentsInChildren<Renderer>()) {
					r.enabled = true;
				}
				globalAudio.clip = dropSound;
				globalAudio.Play();
				objectGrabbed = false;
			}
			else if(actionInteract && rotatePossible) {
				rotateableObject.GetComponent<Interaction.ObjectRotateable>().RotateTowardPivot(gameObject);
				objectInRotation = true;
			}
			else if(actionRelease && objectInRotation) {
				rotateableObject.GetComponent<Interaction.ObjectRotateable>().StopRotating();
				objectInRotation = false;
			}
			else if(actionInteract && pushPossible) {
				pushableObject.GetComponent<Interaction.ObjectPushable>().Use();
			}

		}

		private void OnTriggerEnter(Collider col) {
			bool interactable = false;

			if(col.gameObject.GetComponent<Interaction.ObjectGrabbable>() && !col.gameObject.GetComponent<Interaction.ObjectGrabbable>().grabbed) {
				interactable = true;
				grabbedObject = col.gameObject;
				grabPossible = true;
			}
			else if(col.gameObject.GetComponent<Interaction.ObjectRotateable>()) {
				interactable = true;
				rotateableObject = col.gameObject;
				rotatePossible = true;
			}
			else if(col.gameObject.GetComponent<Interaction.ObjectPushable>()) {
				interactable = true;
				pushableObject = col.gameObject;
				pushPossible = true;
			}

			if(interactable) {
				renderer.material = interactiveMaterial;
				rumbleBuffer = 0.05f;
			}
		}

		private void OnTriggerExit(Collider col) {
			renderer.material = defaultMaterial;
			grabPossible = false;
			rotatePossible = false;
			pushPossible = false;
		}

		private void AnimateControllerModel() {
			if(TBInput.GetAxis1D(TBInput.Button.PrimaryTrigger, controller) > triggerThreshold) {
				triggerRepresentation.transform.localRotation = Quaternion.Euler(-10.0f, -180.0f, 0.0f);
			}
			else {
				triggerRepresentation.transform.localRotation = Quaternion.Euler(10.0f, -180.0f, 0.0f);
			}

			Vector2 joyPos = TBInput.GetAxis2D(TBInput.Button.Joystick, controller);
			analogRepresentation.transform.localRotation = Quaternion.Euler(-joyPos.y * 30.0f, 180.0f, joyPos.x * 30.0f);
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
			//if(TBInput.GetButtonDown(TBInput.Button.PrimaryTrigger, controller)) {
			if(TBInput.GetAxis1D(TBInput.Button.PrimaryTrigger, controller) > triggerThreshold && !triggerPushed) {
				triggerPushed = true;
				actionInteract = true;
			}
			
			/* trigger is released -> release object */
			//if(TBInput.GetButtonUp(TBInput.Button.PrimaryTrigger, controller)) {
			if(TBInput.GetAxis1D(TBInput.Button.PrimaryTrigger, controller) < triggerThreshold && triggerPushed) {
				triggerPushed = false;
				actionRelease = true;
			}
		}
	}
}
