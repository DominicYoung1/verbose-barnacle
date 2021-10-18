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

        public void UpdateEntities()
        {
            //This moves entities between rooms.
            for (int i = 0; i < npcs.Length(); i++)
            {
                Random rng = new Random();
                int randInt = rng.Next(npcs[i].currentRoom.doors.Count);
                //take doors on the current room, make an array out of it, random number is the index choosen
                ArrayList<string> doors = new ArrayList<string>(10);
                foreach(string key in npcs[i].currentRoom.doors.Keys)
                {
                    doors.Push(key);
                }
                // take randInt -> doors -> entity.Move(of all of that)
                npcs[i].Move(doors[randInt]);
            }
            // RE-EXAMINE THIS MECHANIC/PROCESS WITH FRIENDS
            // combat can be started with an attack
            // the function that runs to combat instance can take in two inputs (combatants)
            // these can be determined by who makes the first initiating attack and the target
            // the combat instance would take priority over the UpdateEntities job (shit doesnt move while you are in combat)
            // the ending of the combat instance counts as a passing of time (UpdateEntities then fires after so shit can move)
            // combat ends when one of the entities health drops to below 0
        }
    }
}
