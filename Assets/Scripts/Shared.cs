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

		public enum State {
			Initial, ElectricalBox1, ElectricalBox2, StationBarrierOpen, ElevatorCalled, AptArrivedLowerFloor, AptArrivedUpperFloor
		}

		public static bool isInRange(float value, float min, float max) {
			if(value < min && value > max) {
				return false;
			}
			else {
				return true;
			}
		}

		public static float Map(this float value, float min, float max) {
			if(value < min) {
				return min;
			}
			else if(value > max) {
				return max;
			}
			else {
				return value;
			}
		}
	}
}