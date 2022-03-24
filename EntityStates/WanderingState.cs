using MUD.Multithreading;

namespace MUD.EntityStates
{
    public class WanderingState : IEntityState
    {
        // EVENTS WE CARE ABOUT FOR THIS STATE. MOVE EVENTS, ATTACK EVENTS

        public WanderingState()
        {

        }

        public EventWithReceiver ProcessEvent(IEvent evt, Entity npc)
        {
            // get list of possible actions and apply the math table logic stuff for determination.
            if (evt is NpcActionEvent)
            {
                string selectedDoorName = npc.currentRoom.GetRandomDoorName();
                return new EventWithReceiver(new MoveEvent(npc.name, selectedDoorName), "Self");
            }
            return null;
        }

        public IEntityState PerformTransition(IEvent evt)
        {
            // if the event proccessed is and attack event then preform the transition.
            if (evt is CombatEvent)
            {
                return new InCombatState();
            }
            return null;
        }
    }
}
