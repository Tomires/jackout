using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Logic {
    public class ElectricBoxControl : PuzzleControl
    {
        public AudioSource audioSource;
        public AudioClip electricSwitch, doorUnlock;
        public Interaction.PuzzleButton[] puzzleButtons;
        public Interaction.ObjectRotateable door;
        public StateControl stateControl;

        void Start()
        {
            /* buttons should be disabled until box is unlocked */
            foreach(Interaction.PuzzleButton button in puzzleButtons) {
                button.gameObject.SetActive(false);
            }
        }

        public override void CheckButtonStates() {
            /* 42 -> turn on camera */
            if(puzzleButtons[0].currentIcon == 5 &&
			    puzzleButtons[1].currentIcon == 0) {
				stateControl.ChangeState(Shared.State.ElectricalBox1);
                audioSource.clip = electricSwitch;
                audioSource.Play();
			}
            /* 06 -> activate station */
            else if(puzzleButtons[0].currentIcon == 2 &&
			    puzzleButtons[1].currentIcon == 1) {
                stateControl.ChangeState(Shared.State.ElectricalBox2);
                audioSource.clip = electricSwitch;
                audioSource.Play();
            }
			else {
				stateControl.ChangeState(Shared.State.Initial);
			}
        }

        public void Unlock() {
            door.rotationEnabled = true;
            foreach(Interaction.PuzzleButton button in puzzleButtons) {
                button.gameObject.SetActive(true);
            }

            audioSource.clip = doorUnlock;
            audioSource.Play();
        }
    }
}