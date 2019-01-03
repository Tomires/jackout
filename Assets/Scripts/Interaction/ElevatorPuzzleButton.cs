using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Interaction {
	public class ElevatorPuzzleButton : BaseButton {
		public SpriteRenderer spriteRenderer;
		public Sprite[] puzzleIcons;
		public int currentIcon = 0;
		public Logic.ElevatorControl elevatorControl;
		
		void Start () {
			UpdateSprite();
		}

		public override void Action() {
			currentIcon++;

			if(currentIcon == puzzleIcons.Length) {
				currentIcon = 0;
			}

			UpdateSprite();
			elevatorControl.CheckButtonStates();
		}

		private void UpdateSprite() {
			spriteRenderer.sprite = puzzleIcons[currentIcon];
		}
	}
}