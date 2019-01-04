using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Interaction {
	public class PuzzleButton : BaseButton {
		public SpriteRenderer spriteRenderer;
		public Sprite[] puzzleIcons;
		public int currentIcon = 0;
		public Logic.PuzzleControl puzzleControl;
		
		void Start () {
			UpdateSprite();
		}

		public override void Action() {
			currentIcon++;

			if(currentIcon == puzzleIcons.Length) {
				currentIcon = 0;
			}

			UpdateSprite();
			puzzleControl.CheckButtonStates();
		}

		private void UpdateSprite() {
			spriteRenderer.sprite = puzzleIcons[currentIcon];
		}
	}
}