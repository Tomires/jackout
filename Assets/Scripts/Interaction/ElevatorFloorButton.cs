using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Interaction {
	public class ElevatorFloorButton : BaseButton {
		new public bool enabled = false;
		public SpriteRenderer spriteRenderer;
		public Sprite spriteEnabled, spriteDisabled;
		public Logic.ElevatorControl elevatorControl;
		public bool upperFloor = false;

		public override void Action() {
			if(enabled) {
				elevatorControl.GoToFloor(upperFloor);
			}
		}

		public void SetEnabled(bool on) {
			if(on) {
				enabled = true;
				spriteRenderer.sprite = spriteEnabled;
			}
			else {
				enabled = false;
				spriteRenderer.sprite = spriteDisabled;
			}
		}
	}
}