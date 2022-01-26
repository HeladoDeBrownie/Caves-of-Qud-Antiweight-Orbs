namespace XRL.World.Effects
{
    public class helado_AntiweightOrbs_FloatingAway : Effect
    {
        const string ASPHYXIATE_DEATH_MESSAGE = "You floated away and asphyxiated in the void of space.";
        const string GENERIC_DEATH_MESSAGE = "You floated away into the void of space.";

        public static bool CanFloatAway(GameObject floater)
        {
            return
                floater != null &&
                !floater.IsInGraveyard() &&
                floater.IsUnderSky() &&
                floater.Weight < 0;
        }

        public helado_AntiweightOrbs_FloatingAway()
        {
            DisplayName = "{{B|floating away}}";
            Duration = DURATION_INDEFINITE;
        }

        public override bool Apply(GameObject floater)
        {
            // Only one instance of floating away is allowed per object.
            if (floater.HasEffect(GetType()))
            {
                return false;
            }

            floater.ModIntProperty("IgnoresGravity", 1);
            return true;
        }

        public override void Remove(GameObject floater)
        {
            floater.ModIntProperty("IgnoresGravity", -1, RemoveIfZero: true);
            floater.Gravitate();
        }

        public override bool WantEvent(int id, int cascade)
        {
            return
                id == EndTurnEvent.ID ||
            base.WantEvent(id, cascade);
        }

        public override bool HandleEvent(EndTurnEvent @event)
        {
            if (!CanFloatAway(Object))
            {
                Object.RemoveEffect(this);
                return true;
            }

            XDidY(
                what: Object,
                verb: "float",
                extra: "away",
                terminalPunctuation: "!",
                ColorAsBadFor: Object
            );

            if (Object.CurrentZone.Z > 0)
            {
                Object.Move(
                    Direction: "U",
                    Forced: true,
                    IgnoreGravity: true
                );
            }
            else
            {
                var deathMessage =
                    Object.Respires ?   ASPHYXIATE_DEATH_MESSAGE :
                    /* otherwise */     GENERIC_DEATH_MESSAGE;

                if (Object.IsPlayer())
                {
                    Object.Die(
                        Reason: deathMessage,
                        Accidental: true
                    );
                }
                else
                {
                    Object.Destroy(
                        Reason: deathMessage,
                        Obliterate: true
                    );
                }
            }

            return true;
        }
    }
}
