namespace XRL.World.Parts
{
    [System.Serializable]
    public class helado_AntiweightOrbs_SkywardlyInclined : IPart
    {
        const string ASPHIXIATE_DEATH_MESSAGE = "You floated away and asphixiated in the void of space.";
        const string GENERIC_DEATH_MESSAGE = "You floated away into the void of space.";

        public static void FloatAway(GameObject floater)
        {
            var deathMessage = GetDeathMessageFor(floater);

            if (floater.IsPlayer())
            {
                floater.Die(
                    Reason: deathMessage,
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
                    Reason: deathMessage,
                    Obliterate: true
                );
            }
        }

        public static string GetDeathMessageFor(GameObject dier)
        {
            return dier.Respires ?
                ASPHIXIATE_DEATH_MESSAGE :
                GENERIC_DEATH_MESSAGE;
        }

        public void CheckFloatingAway()
        {
            var physics = ParentObject.pPhysics;

            var floater =
                physics.InInventory ??
                physics.Equipped ??
                ParentObject;

            if (
                floater != null &&
                !floater.IsInGraveyard() &&
                floater.IsUnderSky() &&
                floater.Weight < 0
            )
            {
                FloatAway(floater);
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
            CheckFloatingAway();
            return true;
        }
    }
}
