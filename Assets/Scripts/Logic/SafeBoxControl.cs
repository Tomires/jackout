using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Logic {
    public class SafeBoxControl : PuzzleControl
    {
        public GameObject headset;
        public AudioSource audioSource;
        public AudioClip safeConfirm, safeDeny;
        public Interaction.ObjectRotateable safeDoor;
        public Interaction.PuzzleButton[] puzzleButtons;
        public int[] acceptedSequence;
        private List<int> currentSequence;

        void Start()
        {
            headset.SetActive(false);
            currentSequence = new List<int>();
        }

        public override void CheckButtonStates(Interaction.PuzzleButton button) {
            for(int i = 0; i < puzzleButtons.Length; i++) {
                if(button == puzzleButtons[i]) {
                    currentSequence.Add(i);
                    CheckSequence();
                    break;
                }
            }            
        }

        private void CheckSequence() {
            if(currentSequence.Count == acceptedSequence.Length) {
                bool correct = true;
                for(int i = 0; i < acceptedSequence.Length; i++) {
                    if(acceptedSequence[i] != currentSequence[i]) {
                        correct = false;
                    }
                }

                if(correct) {
                    safeDoor.rotationEnabled = true;
                    headset.SetActive(true);
                    audioSource.clip = safeConfirm;
                    audioSource.Play();
                }
                else {
                    audioSource.clip = safeDeny;
                    audioSource.Play();
                }

                currentSequence.Clear();
            }
        }
    }
}