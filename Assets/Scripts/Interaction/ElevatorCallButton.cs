using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Interaction {
	public class ElevatorCallButton : BaseButton {
		public Logic.GateControl elevatorDoor1, elevatorDoor2;
		public Logic.StateControl stateControl;
		public SpriteRenderer buttonSpriteRenderer;
		public Sprite buttonUp, buttonDown;

		public override void Action() {
			stateControl.ChangeState(Shared.State.ElevatorCalled);
			elevatorDoor1.Switch(true);
			elevatorDoor2.Switch(true);
		}

		public void ChangeButtonSprite(bool up) {
			if(up) {
				buttonSpriteRenderer.sprite = buttonUp;
			}
			else {
				buttonSpriteRenderer.sprite = buttonDown;
			}
		}
	}
}