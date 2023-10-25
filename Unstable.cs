using System; // Random

namespace XRL.World.Parts
{
    [System.Serializable]
    public class helado_AntiweightOrbs_Unstable : IPart
    {
        public const int EXPLODE_ON_DAMAGE_IMPROBABILITY = 10;
        public const int EXPLODE_EACH_TURN_IMPROBABILITY = 10000;

        public Random RandomSource;

        public void Destabilize()
        {
            // If we're traveling on the world map, take an unfortunate aside.
            ThePlayer.PullDown();

            XDidY(
                Actor: ParentObject,
                Verb: "explode",
                EndMark: "!",
                ColorAsBadFor: ParentObject
            );

            // Much weaker than a flux explosion, but still pretty devastating.
            ParentObject.Explode(Force: 3000, BonusDamage: "3d10+80");
        }

        public override void Attach()
        {
            RandomSource = XRL.Rules.Stat.GetSeededRandomGenerator(
                $"helado_Antiweight Orbs_{ParentObject.id}"
            );
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
            if (RandomSource.Next(1, EXPLODE_ON_DAMAGE_IMPROBABILITY) == 1)
            {
                Destabilize();
            }

            return true;
        }

        public override bool HandleEvent(EndTurnEvent @event)
        {
            if (RandomSource.Next(1, EXPLODE_EACH_TURN_IMPROBABILITY) == 1)
            {
                Destabilize();
            }

            return true;
        }

        // Don't stack; each item rolls separately to destabilize.
        public override bool SameAs(IPart part)
        {
            return false;
        }
    }
}
