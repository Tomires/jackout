using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jackout.Interaction {
    public class BarrierButton : BaseButton
    {
        public Logic.TicketBarrierControl barrierControl;
        public bool entrySide = true;
        public override void Action() {
            barrierControl.ButtonPressed(entrySide);
        }
    }
}