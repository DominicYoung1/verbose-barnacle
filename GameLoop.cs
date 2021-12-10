using System;
using System.Collections.Generic;
using System.Text;
using MUD.Multithreading;

namespace MUD
{
    class GameLoop: IActor
    {
        ArrayQueue inbox;
        PlayerController controller;
        EntityController npcs;

        public GameLoop(PlayerController c, EntityController n)
        {
            inbox = new ArrayQueue();
            Initialize();
            controller = c;
            npcs = n;
        }

        override protected  IThreadedQueue Inbox()
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
                SendMessage("Player", new PrintEvent(ret));
                //Console.WriteLine("COMMUUUNICATIONS {0}", cEvt.GetString());
                //SendMessage("Self", new CommandEvent("Another one!"));
            }
        }

        protected override void Update()
        {
            //This function will be responsible for "anything that does not depend on the user"'s actions in time.
            string message = npcs.UpdateEntities(); // can be used to make a string that can be shown to the player if something moves in their view.
        }
    }
}
