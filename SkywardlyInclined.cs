using System;

namespace XRL.World.Parts {
    [Serializable] public class helado_SkywardlyInclined : IPart {

    public void FloatAway (GameObject Go) {
        if (Go.IsPlayer ()) {
            Go.FireEvent (Event.New ("Die",
                "Reason", "You floated away and asphixiated in the void of space.",
                "Accidental", 1)); }
        else {
            IPart.AddPlayerMessage (Go.The + Go.DisplayNameOnly + " floats away.");
            Go.Destroy (); } }

    public override bool FireEvent (Event E) {
        switch (E.ID) {
        case "EndTurn":
            var p = ParentObject.pPhysics;

            if (p.Weight < 0) {
                var haver = p.InInventory ?? p.Equipped;

                if (haver == null) {
                    if (ParentObject.IsUnderSky ()) {
                        FloatAway (ParentObject); } }
                else if (haver.IsUnderSky ()) {
                    if (haver.GetPart<Inventory> ().GetWeight () <= -haver.pPhysics.Weight) {
                        FloatAway (haver); } } }
            break;
        default:
            return base.FireEvent (E); }
        return true; }

    public override void Register (GameObject Go) {
        Go.RegisterPartEvent (this, "EndTurn"); } } }
