using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Interaction {
	public abstract class BaseGrab : MonoBehaviour {
		abstract public void PickedUp();
        abstract public void Dropped();
	}
}