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

        public GameLoop(PlayerController c)
        {
            inbox = new ArrayQueue();
            Initialize();
            controller = c;
        }

        override protected  IThreadedQueue Inbox()
        {
            return inbox;
        }

        override protected void ProcessEvent(IEvent evt)
        {
            if (evt is CommandEvent cEvt)
            {
                //need to use the process command method on the "PlayerController"
                string message = cEvt.GetString();
                controller.processCommand(message);

                //Console.WriteLine("COMMUUUNICATIONS {0}", cEvt.GetString());
                //SendMessage("Self", new CommandEvent("Another one!"));
            }
        }
    }
}
