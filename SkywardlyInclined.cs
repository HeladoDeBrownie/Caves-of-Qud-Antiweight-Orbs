namespace XRL.World.Parts
{
    [System.Serializable]
    public class helado_SkywardlyInclined : IPart
    {

        public void FloatAway(GameObject Go)
        {
            if (Go.IsPlayer())
            {
                Go.Die(
                    Killer: (GameObject)null,
                    Reason: "You floated away and asphixiated in the void of space.",
                    Accidental: true
                );
            }
            else
            {
                IPart.AddPlayerMessage(Go.The + Go.DisplayNameOnly + " floats away!");
                Go.Destroy();
            }
        }

        public override bool FireEvent(Event E)
        {
            switch (E.ID)
            {
                case "EndTurn":
                    var p = ParentObject.pPhysics;

                    if (p.Weight < 0)
                    {
                        var haver = p.InInventory ?? p.Equipped;

                        if (haver == null)
                        {
                            if (ParentObject.IsUnderSky())
                            {
                                FloatAway(ParentObject);
                            }
                        }
                        else if (haver.IsUnderSky())
                        {
                            if (haver.pPhysics.Weight < 0)
                            {
                                FloatAway(haver);
                            }
                        }
                    }

                    return true;

                default:
                    return base.FireEvent(E);
            }
        }

        public override void Register(GameObject Go)
        {
            Go.RegisterPartEvent(this, "EndTurn");
        }
    }
}
