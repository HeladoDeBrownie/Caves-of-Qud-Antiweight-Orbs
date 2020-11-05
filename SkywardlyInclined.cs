using static XRL.UI.ConversationUI; // VariableReplace

namespace XRL.World.Parts
{
    [System.Serializable]
    public class helado_SkywardlyInclined : IPart
    {

        public void FloatAway(GameObject go)
        {
            if (go.IsPlayer())
            {
                go.Die(
                    Killer: (GameObject)null,
                    Reason: "You floated away and asphixiated in the void of space.",
                    Accidental: true
                );
            }
            else
            {
                IPart.AddPlayerMessage(VariableReplace(
                    "=capitalize==subject.the==subject.name= =verb:float= away!",
                go));

                go.Destroy(Obliterate: true);
            }
        }

        public override bool FireEvent(Event @event)
        {
            switch (@event.ID)
            {
                case "EndTurn":
                    if (ParentObject.Weight < 0)
                    {
                        var physics = ParentObject.pPhysics;
                        var holder = physics.InInventory ?? physics.Equipped;

                        if (holder == null && ParentObject.IsUnderSky())
                        {
                            FloatAway(ParentObject);
                        }
                        else if (holder.IsUnderSky() && holder.Weight < 0)
                        {
                                FloatAway(holder);
                        }
                    }

                    return true;

                default:
                    return base.FireEvent(@event);
            }
        }

        public override void Register(GameObject go)
        {
            go.RegisterPartEvent(this, "EndTurn");
        }
    }
}
