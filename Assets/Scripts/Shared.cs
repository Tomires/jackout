using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout {
	public static class Shared {
		public static float LerpSin(float step) {
			return Mathf.Sin(step * Mathf.PI * 0.5f) * 0.5f;
		}

		public enum Axis {
			X, Y, Z
		}

		public static bool isInRange(float value, float min, float max) {
			if(value < min && value > max) {
				return false;
			}
			else {
				return true;
			}
		}
	}
}