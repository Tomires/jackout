using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Logic {
    public abstract class PuzzleControl : MonoBehaviour
    {
        abstract public void CheckButtonStates(Interaction.PuzzleButton button);
    }
}