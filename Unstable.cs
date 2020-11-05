using System;
using XRL.Rules;
using static XRL.UI.ConversationUI; // VariableReplace

namespace XRL.World.Parts
{
    [Serializable]
    public class helado_AntiweightOrbs_Unstable : IPart
    {
        public const int EXPLODE_ON_HIT_IMPROBABILITY = 10;
        public const int EXPLODE_EACH_TURN_IMPROBABILITY = 10000;

        public void Destabilize()
        {
            GameObject player = this.ParentObject.ThePlayer;

            if (player.OnWorldMap())
            {
                player.PullDown();
            }

            IPart.AddPlayerMessage(VariableReplace(
                "=capitalize==subject.the==subject.name= =verb:explode=!",
            ParentObject));

            ParentObject.Explode(Force: 3000, BonusDamage: "1d200");
            this.ParentObject.Destroy();
        }

        public override bool FireEvent(Event e)
        {
            if (e.ID == "BeforeApplyDamage")
            {
                if (Stat.Random(1, EXPLODE_ON_HIT_IMPROBABILITY) <= 1)
                {
                    Destabilize();
                }
                return true;
            }
            else if (e.ID == "EndTurn")
            {
                if (Stat.Random(1, EXPLODE_EACH_TURN_IMPROBABILITY) <= 1)
                {
                    Destabilize();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Register(GameObject go)
        {
            go.RegisterPartEvent(this, "BeforeApplyDamage");
            go.RegisterPartEvent(this, "EndTurn");
        }
    }
}
