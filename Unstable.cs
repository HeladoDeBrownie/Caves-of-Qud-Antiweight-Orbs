using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts {
    [Serializable] public class helado_Unstable : IPart {
        public override bool FireEvent (Event e) {
            if (e.ID == "EndTurn") {
                if (Stat.Random (1, 10000) <= 1) {
                    Popup.Show ("[destabilize]"); }
                return true; }
            else {
                return false; } }

        public override void Register (GameObject go) {
            go.RegisterPartEvent (this, "EndTurn"); } } }
