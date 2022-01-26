using FloatingAway = XRL.World.Effects.helado_AntiweightOrbs_FloatingAway;

namespace XRL.World.Parts
{
    [System.Serializable]
    public class helado_AntiweightOrbs_SkywardlyInclined : IPart
    {
        public override bool WantEvent(int id, int cascade)
        {
            return
                id == EndTurnEvent.ID ||
            base.WantEvent(id, cascade);
        }

        public override bool HandleEvent(EndTurnEvent @event)
        {
            var physics = ParentObject.pPhysics;

            var floater =
                physics.InInventory ??
                physics.Equipped ??
                ParentObject;

            if (
                FloatingAway.CanFloatAway(floater) &&
                !floater.HasEffect(typeof(FloatingAway))
            )
            {
                floater.ApplyEffect(new FloatingAway());
            }

            return true;
        }
    }
}
