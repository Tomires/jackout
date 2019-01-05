using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Logic {
    public class ImageChangeControl : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Sprite[] sprites;
        public float timeBetweenChanges = 0.05f;
        private float timeToChange;
        private int currentSprite = 0;

        void Start() 
        {
            timeToChange = timeBetweenChanges;
        }

        void Update()
        {
            timeToChange -= Time.deltaTime;

            if(timeToChange < 0.0f) {
                timeToChange = timeBetweenChanges;
                currentSprite++;

                if(currentSprite == sprites.Length) {
                    currentSprite = 0;
                }

                spriteRenderer.sprite = sprites[currentSprite];
            }
        }
    }
}