using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Logic {
    public class SafeBoxControl : MonoBehaviour
    {
        public GameObject headset;
        public AudioSource audioSource;
        public AudioClip safeConfirm, safeDeny;
        public Interaction.ObjectRotateable safeDoor;

        void Start()
        {
            headset.SetActive(false);
        }

        void Update()
        {
            
        }
    }
}