using System;
using System.Collections.Generic;
using System.Text;

namespace MUD
{
    /**
     *  Tell npcs to move. TODO
     *  spawn npcs
     *  tell npcs to pick up and drop items
     *  tell npcs to interact..(attacking, using, etc)
     *  tell npcs to die
     * */
    public class EntityController
    {
        ArrayList<Entity> npcs;

        public EntityController()
        {
            npcs = new ArrayList<Entity>(2);
        }
        public void RegisterEntity(Entity entity)
        {
            npcs.Push(entity);
        }

        void NpcMove()
        {
            //This moves entities between rooms.
            for (int i = 0; i < npcs.Length(); i++)
            {
                Random rng = new Random();
                int randInt = rng.Next(npcs[i].currentRoom.doors.Count);
                //take doors on the current room, make an array out of it, random number is the index choosen
                ArrayList<string> doors = new ArrayList<string>(10);
                foreach (string key in npcs[i].currentRoom.doors.Keys)
                {
                    doors.Push(key);
                }
                // take randInt -> doors -> entity.Move(of all of that)
                npcs[i].Move(doors[randInt]);
            }
        }
        void NpcPickup()
        {

        }

        public void UpdateEntities()
        {
            // EVENTUALLY logic will need to be applied to actions, for now focus on it working.
            NpcMove();
            
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
    }
}
