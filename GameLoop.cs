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
            if (evt is KickOffEvent uEvt)
            {
                // Need this to facilitate the actions of the world
                // and then at the end create another update event to be processed

                //SendMessage("Player", new PrintEvent("Update Occured!"), 0);
                for (int i = 0; i < npcs.GetAllNPCS().Length(); i++)
                {
                    Entity movingEntity = npcs.GetAllNPCS()[i];
                    string door = movingEntity.currentRoom.GetRandomDoorName();
                    SendMessage("Self", new MoveEvent(npcs.GetAllNPCS()[i].name, door), 4000);
                }
            }
            if (evt is MoveEvent mEvt)
            {
                // Need this to take care of the movment of all npcs
                // If the movment happens in view of the player via their room, it needs send a printevent for the player to read.

                string entityName = mEvt.GetNameOfMovingThing();
                string doorName = mEvt.GetNameOfSelectedDoor();
                Room occupiedRoom = MoveEntity(entityName, doorName);
                string selectedDoorName = occupiedRoom.GetRandomDoorName();
                Random rnd = new Random();
                int num = rnd.Next(4000, 8000);
                SendMessage("Self", new MoveEvent(entityName, selectedDoorName), num);
            }
        }

        private Room MoveEntity(string entityName, string doorName)
        {
            bool isPlayerInBefore = false;
            bool isPlayerInAfter = false;
            Entity selected = npcs.GetNPC(entityName);
            Room playerRoom = controller.PlayerInfo().currentRoom;
            Room destination = selected.currentRoom.doors[doorName];
            if (selected.currentRoom.name == playerRoom.name){
                isPlayerInBefore = true;
            }
            if (destination.name == playerRoom.name)
            {
                isPlayerInAfter = true;
            }
            selected.Move(doorName);
            if (isPlayerInAfter)
            {
                string preppedMessage = String.Format("{0} has entered the room!", selected.name);
                SendMessage("Player", new PrintEvent(preppedMessage), 0);
            }
            if (isPlayerInBefore)
            {
                string preppedMessage = String.Format("{0} has left the room!", selected.name);
                SendMessage("Player", new PrintEvent(preppedMessage), 0);
            }
            return selected.currentRoom;
        }
        //protected  void Update()
        //{
        //    //This function will be responsible for "anything that does not depend on the user"'s actions in time.
        //    ArrayList<string> occupants = new ArrayList<string>(10);
        //    ArrayList<Entity> entities = controller.PlayerInfo().currentRoom.GetOccupants();
        //    for (int i = 0; i < entities.Length(); i++)
        //    {
        //        occupants.Push(entities[i].name);
        //    }
        //    npcs.UpdateEntities(); // can be used to make a string that can be shown to the player if something moves in their view.
        //    ArrayList<string> occupants2 = new ArrayList<string>(10);
        //    for (int i = 0; i < entities.Length(); i++)
        //    {
        //        occupants2.Push(entities[i].name);
        //    }
        //    for (int i = 0; i < occupants.Length(); i++)
        //    {
        //        bool isIn = false; 
        //        for (int j = 0; j < occupants2.Length(); j++)
        //        {
        //            if (occupants[i] == occupants2[j])
        //            {
        //                isIn = true;
        //            }
        //        }
        //        if (isIn == false)
        //        {
        //            string message = String.Format("{0} left the room!", occupants[i]);
        //            SendMessage("Player", new PrintEvent(message),0);
        //        }
        //    }
        //    for (int i = 0; i < occupants2.Length(); i++)
        //    {
        //        bool isIn = false;
        //        for (int j = 0; j < occupants.Length(); j++)
        //        {
        //            if (occupants2[i] == occupants[j])
        //            {
        //                isIn = true;
        //            }
        //        }
        //        if (isIn == false)
        //        {
        //            string message = String.Format("{0} entered the room!", occupants2[i]);
        //            SendMessage("Player", new PrintEvent(message),0);
        //        }
        //    }
        //}
    }
}
