using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Logic {
	public class LightControl : MonoBehaviour {
		public bool lightOn = false;
		public Material onMaterial;
		public Material offMaterial;
		public Light lightComponent;

		void Start () {
			if(!lightOn) {
				gameObject.GetComponent<Renderer>().material = offMaterial;
				lightComponent.enabled = false;
			}
		}
		
		public void Switch(bool on) {
			lightOn = on;
			Material material;
			if(lightOn) {
				material = onMaterial;
			}
			else {
				material = offMaterial;
			}

			gameObject.GetComponent<Renderer>().material = material;
			lightComponent.enabled = on;
		}
	}
}