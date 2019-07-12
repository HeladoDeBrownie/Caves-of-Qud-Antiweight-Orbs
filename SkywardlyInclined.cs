using System;

namespace XRL.World.Parts {
    [Serializable] public class helado_SkywardlyInclined : IPart {

    public override bool FireEvent (Event e) {
        if (e.ID == "EndTurn") {
            GameObject haver = null;
            if (haver == null) {
                if (this.ParentObject.IsUnderSky ()) {
                    IPart.AddPlayerMessage ("The sphere floats away.");
                    this.ParentObject.Destroy (false); } }
            else if (haver.IsUnderSky ()) {
                IPart.AddPlayerMessage ("TODO: Make the haver float away."); }
            return true; }
        return false; }

    public override void Register (GameObject go) {
        go.RegisterPartEvent (this, "EndTurn"); } } }
