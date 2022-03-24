using MUD.Multithreading;

namespace MUD.EntityStates
{
    public class EntityBehaviorStateMachine
    {
        IEntityState currentState;

        public EntityBehaviorStateMachine(IEntityState s)
        {
            currentState = s;
        }


        public EventWithReceiver ProcessEvent(IEvent evt, Entity npc)
        {
            IEntityState newState = currentState.PerformTransition(evt);
            if (newState != null)
            {
                currentState = newState;
            }
            return currentState.ProcessEvent(evt, npc);
        }
    }
}
