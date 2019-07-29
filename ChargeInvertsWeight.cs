using System;

namespace XRL.World.Parts {
    [Serializable] public class helado_ChargeInvertsWeight : IPart {

        public bool Active = false;

        public override bool FireEvent (Event E) {
            switch (E.ID) {
            case "CellChanged":
            case "EndTurn":
                Active = ParentObject.UseCharge (60);
                break;
            case "GetWeight":
                if (Active) {
                    E.SetParameter ("Weight", -E.GetIntParameter ("Weight"));
                    return false; }
                else {
                    goto default; }
            default:
                return base.FireEvent (E); }
            return true; }

        public override void Register (GameObject Go) {
            Go.RegisterPartEvent (this, "CellChanged");
            Go.RegisterPartEvent (this, "EndTurn");
            Go.RegisterPartEvent (this, "GetWeight");
            base.Register (Go); } } }