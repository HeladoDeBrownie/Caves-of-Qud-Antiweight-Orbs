using System;

namespace XRL.World.Parts {
    [Serializable] public class helado_SkywardlyInclined : IPart {

    public override bool FireEvent (Event e) {
        if (e.ID == "EndTurn") {
            Physics p = this.ParentObject.pPhysics;
            GameObject haver = p.InInventory ?? p.Equipped;
            if (haver == null) {
                if (this.ParentObject.IsUnderSky ()) {
                    IPart.AddPlayerMessage ("The sphere floats away.");
                    this.ParentObject.Destroy (false); } }
            else if (haver.IsUnderSky ()) {
                if (haver.GetPart<Inventory> ().GetWeight () <= -200) {
                    IPart.AddPlayerMessage ("[haver] floats away.");
                    haver.Destroy (false); } }
            return true; }
        return false; }

    public override void Register (GameObject go) {
        go.RegisterPartEvent (this, "EndTurn"); } } }
