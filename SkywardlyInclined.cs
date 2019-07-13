using System;
using XRL.UI;

namespace XRL.World.Parts {
    [Serializable] public class helado_SkywardlyInclined : IPart {

    public void FloatAway (GameObject go) {
        if (go.IsPlayer ()) {
            go.FireEvent (Event.New ("Die",
                "Reason",
                    "You floated away and asphixiated in the void of space.",
                "Accidental",
                    1)); }
        else {
            IPart.AddPlayerMessage (go.The + go.DisplayNameOnly +
                " floats away.");
            go.Destroy (); } }

    public override bool FireEvent (Event e) {
        if (e.ID == "EndTurn") {
            Physics p = this.ParentObject.pPhysics;
            GameObject haver = p.InInventory ?? p.Equipped;
            if (haver == null) {
                if (this.ParentObject.IsUnderSky ()) {
                    FloatAway (this.ParentObject); } }
            else if (haver.IsUnderSky ()) {
                if (haver.GetPart<Inventory> ().GetWeight () <= -200) {
                    FloatAway (haver); } }
            return true; }
        return false; }

    public override void Register (GameObject go) {
        go.RegisterPartEvent (this, "EndTurn"); } } }
