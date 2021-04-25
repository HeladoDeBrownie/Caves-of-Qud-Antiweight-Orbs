namespace XRL.World.Parts
{
    [System.Serializable]
    public class helado_AntiweightOrbs_SkywardlyInclined : IPart
    {
        const string ASPHIXIATE_DEATH_MESSAGE = "You floated away and asphixiated in the void of space.";
        const string GENERIC_DEATH_MESSAGE = "You floated away into the void of space.";

        public static string DeathMessageFor(GameObject dier)
        {
            return CanBreathe(dier) ?
                ASPHIXIATE_DEATH_MESSAGE :
                GENERIC_DEATH_MESSAGE;
        }

        public static bool CanBreathe(GameObject what)
        {
            return what.IsAlive;
        }

        public void FloatAway(GameObject floater)
        {
            if (floater.IsPlayer())
            {
                floater.Die(
                    Reason: DeathMessageFor(floater),
                    Accidental: true
                );
            }
            else
            {
                XDidY(
                    what: floater,
                    verb: "float",
                    extra: "away",
                    terminalPunctuation: "!",
                    ColorAsBadFor: floater
                );

                floater.Destroy(
                    Reason: DeathMessageFor(floater),
                    Obliterate: true
                );
            }
        }

        public override bool WantEvent(int id, int cascade)
        {
            return
                id == EndTurnEvent.ID ||
            base.WantEvent(id, cascade);
        }

        public override bool HandleEvent(EndTurnEvent @event)
        {
            if (ParentObject.Weight < 0)
            {
                var physics = ParentObject.pPhysics;
                var holder = physics.InInventory ?? physics.Equipped;

                if (holder == null)
                {
                    if (ParentObject.IsUnderSky())
                    {
                        FloatAway(ParentObject);
                    }
                }
                else if (holder.IsUnderSky() && holder.Weight < 0)
                {
                    FloatAway(holder);
                }
            }

            return true;
        }
    }
}
