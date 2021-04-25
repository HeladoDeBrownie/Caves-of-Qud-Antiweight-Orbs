// TODO: Replace Stat.Random calls with per-object RNG.
using XRL.Rules;

namespace XRL.World.Parts
{
    [System.Serializable]
    public class helado_AntiweightOrbs_Unstable : IPart
    {
        public const int EXPLODE_ON_HIT_IMPROBABILITY = 10;
        public const int EXPLODE_EACH_TURN_IMPROBABILITY = 10000;

        public void Destabilize()
        {
            ThePlayer.PullDown();

            XDidY(
                what: ParentObject,
                verb: "explode",
                terminalPunctuation: "!",
                ColorAsBadFor: ParentObject
            );

            ParentObject.Explode(Force: 3000, BonusDamage: "1d200");
            ParentObject.Destroy();
        }

        public override bool WantEvent(int id, int cascade)
        {
            return
                id == BeforeApplyDamageEvent.ID ||
                id == EndTurnEvent.ID ||
            base.WantEvent(id, cascade);
        }

        public override bool HandleEvent(BeforeApplyDamageEvent @event)
        {
            if (Stat.Random(1, EXPLODE_ON_HIT_IMPROBABILITY) <= 1)
            {
                Destabilize();
            }

            return true;
        }

        public override bool HandleEvent(EndTurnEvent @event)
        {
            if (Stat.Random(1, EXPLODE_EACH_TURN_IMPROBABILITY) <= 1)
            {
                Destabilize();
            }

            return true;
        }
    }
}
