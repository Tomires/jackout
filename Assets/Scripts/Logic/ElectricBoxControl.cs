using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Logic {
    public class ElectricBoxControl : PuzzleControl
    {
        public AudioSource audioSource;
        public AudioClip electricSwitch;
        public Interaction.PuzzleButton[] puzzleButtons;
        public StateControl stateControl;

        void Start()
        {
            audioSource.clip = electricSwitch;
        }

        public override void CheckButtonStates() {
            /* 42 -> turn on camera */
            if(puzzleButtons[0].currentIcon == 5 &&
			    puzzleButtons[1].currentIcon == 0) {
				stateControl.ChangeState(Shared.State.ElectricalBox1);
                audioSource.Play();
			}
            /* 06 -> activate station */
            else if(puzzleButtons[0].currentIcon == 2 &&
			    puzzleButtons[1].currentIcon == 1) {
                stateControl.ChangeState(Shared.State.ElectricalBox2);
                audioSource.Play();
            }
			else {
				stateControl.ChangeState(Shared.State.Initial);
			}
        }
    }
}