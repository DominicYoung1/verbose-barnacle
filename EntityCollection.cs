using MUD.EntityStates;

namespace MUD
{
    /**
     *  Tell npcs to move. TODO
     *  spawn npcs
     * */
    public class EntityCollection
    {
        ArrayList<Entity> npcs;
        ArrayList<EntityBehaviorStateMachine> controllers;

        public EntityCollection()
        {
            npcs = new ArrayList<Entity>(2);
            controllers = new ArrayList<EntityBehaviorStateMachine>(2);
        }
        public void RegisterEntity(Entity entity)
        {
            npcs.Push(entity);
            controllers.Push(new EntityBehaviorStateMachine(new WanderingState()));
        }

        public Entity GetNPC(string name)
        {
            for (int i = 0; i < npcs.Length(); i++)
            {
                if (name == npcs[i].name)
                {
                    return npcs[i];
                }
            }
            return null;
        }

        public ArrayList<Entity> GetAllNPCS()
        {
            return npcs;
        }

        public EntityBehaviorStateMachine GetStateMachine(string name)
        {
            for (int i = 0; i < controllers.Length(); i++)
            {
                if (name == npcs[i].name)
                {
                    return controllers[i];
                }
            }
            return null;
        }
    }
}
