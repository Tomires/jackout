using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Interaction {
	public class LightSwitchButton : BaseButton {
		public Logic.LightControl[] lights;
		public bool lightsOn = false;
		public SpriteRenderer spriteRenderer;
		public Sprite spriteOn, spriteOff;

		public override void Action() {
			lightsOn = !lightsOn;
			spriteRenderer.sprite = lightsOn ? spriteOn : spriteOff;

			foreach(Logic.LightControl light in lights) {
				light.Switch(lightsOn);
			}
		}
	}
}