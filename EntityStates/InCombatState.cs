using MUD.Multithreading;

namespace MUD.EntityStates
{
    class InCombatState : IEntityState, IRandomSelection
    {
        public InCombatState()
        {

        }

        public EventWithReceiver ProcessEvent(IEvent evt, Entity npc)
        {
            if (evt is CombatEvent cEvt)
            {
                IEvent eventToReturn = (this as IRandomSelection).PickAOrB(
                    new AttackEvent(cEvt.GetActiveMember(), cEvt.GetPassiveMemeber()),
                    new FleeEvent(cEvt.GetActiveMember(), cEvt.GetPassiveMemeber()),
                    npc.health < 25 ? 0.7 : 1.0);
                return new EventWithReceiver(
                   eventToReturn,
                    "Self");
            }
            return null;
        }

        public IEntityState PerformTransition(IEvent evt)
        {
            if (evt is EndCombatEvent)
            {
                return new WanderingState();
            }
            return null;
        }
    }
}
