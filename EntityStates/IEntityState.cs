using MUD.Multithreading;

namespace MUD.EntityStates
{
    public interface IEntityState
    {

        EventWithReceiver ProcessEvent(IEvent evt, Entity npc);

        IEntityState PerformTransition(IEvent evt);
    }
}
