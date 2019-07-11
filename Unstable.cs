using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts {
    [Serializable] public class helado_Unstable : IPart {
        public const int EXPLODE_ON_HIT_IMPROBABILITY = 10;
        public const int EXPLODE_EACH_TURN_IMPROBABILITY = 10000;

        public void Destabilize () {
        Popup.Show ("[destabilize]"); }

        public override bool FireEvent (Event e) {
            if (e.ID == "BeforeApplyDamage") {
                if (Stat.Random (1, EXPLODE_ON_HIT_IMPROBABILITY) <= 1) {
                    Destabilize(); }
                return true; }
            else if (e.ID == "EndTurn") {
                if (Stat.Random (1, EXPLODE_EACH_TURN_IMPROBABILITY) <= 1) {
                     Destabilize(); }
                return true; }
            else {
                return false; } }

        public override void Register (GameObject go) {
            go.RegisterPartEvent (this, "BeforeApplyDamage");
            go.RegisterPartEvent (this, "EndTurn"); } } }
