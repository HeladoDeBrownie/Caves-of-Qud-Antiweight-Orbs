using System;
using XRL.UI;

namespace XRL.World.Parts {
    [Serializable] public class helado_SkywardlyInclined : IPart {

    public override bool FireEvent (Event e) {
        if (e.ID == "EndTurn") {
            Physics p = this.ParentObject.pPhysics;
            GameObject haver = p.InInventory ?? p.Equipped;
            if (haver == null) {
                if (this.ParentObject.IsUnderSky ()) {
                    IPart.AddPlayerMessage ("The sphere floats away.");
                    this.ParentObject.Destroy (); } }
            else if (haver.IsUnderSky ()) {
                if (haver.GetPart<Inventory> ().GetWeight () <= -200) {
                    if (haver.IsPlayer ()) {
                        Popup.Show ("You float away and asphixiate in the void of space."); }
                    else {
                        IPart.AddPlayerMessage ("[haver] floats away."); }
                    haver.Destroy ("Asphixiated in space."); } }
            return true; }
        return false; }

    public override void Register (GameObject go) {
        go.RegisterPartEvent (this, "EndTurn"); } } }
