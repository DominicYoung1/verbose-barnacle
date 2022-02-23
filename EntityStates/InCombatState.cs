using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    new FleeEvent(cEvt.GetActiveMember()),
                    npc.health < 25 ? 0.7 : 1.0);
                return new EventWithReceiver(
                   eventToReturn,
                    "Self");
            }
            return null;
        }

        public IEntityState PerformTransition(IEvent evt)
        {
            if (evt is MoveEvent)
            {
                return null;
            }
            return null;
        }
    }
}
