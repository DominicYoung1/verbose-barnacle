using System;
using System.Collections.Generic;
using System.Text;
using MUD.Multithreading;

namespace MUD
{
    class GameLoop: IActor
    {
        ListPriorityQueue inbox;
        PlayerController controller;
        EntityController npcs;

        public GameLoop(PlayerController c, EntityController n)
        {
            inbox = new ListPriorityQueue();
            Initialize();
            controller = c;
            npcs = n;
        }

        override protected  IThreadedPrioQueue Inbox()
        {
            return inbox;
        }

        override protected void ProcessEvent(IEvent evt)
        {
            if (evt is CommandEvent cEvt)
            {
                // We need the ability for the GameLoop to keep going even if there are no user events.

                //need to use the process command method on the "PlayerController"
                string message = cEvt.GetString();
                string ret = controller.processCommand(message);
                SendMessage("Player", new PrintEvent(ret), 0);
                //Console.WriteLine("COMMUUUNICATIONS {0}", cEvt.GetString());
                //SendMessage("Self", new CommandEvent("Another one!"));
            }
        }

        protected override void Update()
        {
            //This function will be responsible for "anything that does not depend on the user"'s actions in time.
            ArrayList<string> occupants = new ArrayList<string>(10);
            ArrayList<Entity> entities = controller.PlayerInfo().currentRoom.GetOccupants();
            for (int i = 0; i < entities.Length(); i++)
            {
                occupants.Push(entities[i].name);
            }
            npcs.UpdateEntities(); // can be used to make a string that can be shown to the player if something moves in their view.
            ArrayList<string> occupants2 = new ArrayList<string>(10);
            for (int i = 0; i < entities.Length(); i++)
            {
                occupants2.Push(entities[i].name);
            }
            for (int i = 0; i < occupants.Length(); i++)
            {
                bool isIn = false; 
                for (int j = 0; j < occupants2.Length(); j++)
                {
                    if (occupants[i] == occupants2[j])
                    {
                        isIn = true;
                    }
                }
                if (isIn == false)
                {
                    string message = String.Format("{0} left the room!", occupants[i]);
                    SendMessage("Player", new PrintEvent(message),0);
                }
            }
            for (int i = 0; i < occupants2.Length(); i++)
            {
                bool isIn = false;
                for (int j = 0; j < occupants.Length(); j++)
                {
                    if (occupants2[i] == occupants[j])
                    {
                        isIn = true;
                    }
                }
                if (isIn == false)
                {
                    string message = String.Format("{0} entered the room!", occupants2[i]);
                    SendMessage("Player", new PrintEvent(message),0);
                }
            }
        }
    }
}
