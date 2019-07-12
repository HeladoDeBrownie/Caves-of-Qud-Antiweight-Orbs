using System;

namespace XRL.World.Parts {
    [Serializable] public class helado_SkywardlyInclined : IPart {

    public override bool FireEvent (Event e) {
        if (e.ID == "EndTurn") {
            IPart.AddPlayerMessage ("TODO: check if we should float upward");
            return true; }
        return false; }

    public override void Register (GameObject go) {
        go.RegisterPartEvent (this, "EndTurn"); } } }
