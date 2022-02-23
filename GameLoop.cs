using System;
using System.Collections.Generic;
using System.Text;
using MUD.Multithreading;
using MUD.EntityStates;

namespace MUD
{
   class GameLoop: IActor
    {
        ListPriorityQueue inbox;
        PlayerController controller;
        EntityCollection npcs;

        public GameLoop(PlayerController c, EntityCollection n)
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

        protected override void ProcessEvent(IEvent evt)
        {
            switch (evt)
            {
                case CommandEvent cEvt:
                    {
                        string message = cEvt.GetString();
                        EventWithReceiver ret = controller.processCommand(message);
                        SendMessage(ret.actorName, ret.message, 0);
                        break;
                    }
                case KickOffEvent:
                    {
                        for (int i = 0; i < npcs.GetAllNPCS().Length(); i++)
                        {
                            Entity movingEntity = npcs.GetAllNPCS()[i];
                            string door = movingEntity.currentRoom.GetRandomDoorName();
                            SendMessage("Self", new NpcActionEvent(npcs.GetAllNPCS()[i].name), 4000);
                        }
                        break;
                    }
                case MoveEvent mEvt:
                    {
                        // check curretn stautgyers
                        string entityName = mEvt.GetNameOfMovingThing();
                        string doorName = mEvt.GetNameOfSelectedDoor();
                        MoveEntity(entityName, doorName);
                        // call method GetNextEvent
                        break;
                    }
                case AttackEvent aEvt:
                    {
                        string attacker = aEvt.GetAttacker();
                        string defender = aEvt.GetDefender();
                        Entity attackerEntity = attacker == "Player" ? controller.PlayerInfo() : npcs.GetNPC(attacker);
                        Entity defenderEntity = defender == "Player" ? controller.PlayerInfo() : npcs.GetNPC(defender);
                        double swing = attackerEntity.ImpartDamage();
                        defenderEntity.TakeDamage(swing);
                        if (!defenderEntity.Alive())
                        {
                            SendMessage("Self", new DeathEvent(defender), 0);
                            break;
                        }
                        if (attacker == "Player")
                        {
                            SendMessage("Player", new PrintEvent(String.Format("You did {0} damage!", swing)), 0);
                        }
                        if (defender == "Player")
                        {
                            SendMessage("Player", new PrintEvent(String.Format("You recieved {0} damage, Remaining HP {1}", swing, defenderEntity.health)), 0);
                        }
                        SendMessage("Self", new CombatEvent(defender, attacker), 3000);
                        break;
                    }
                case CombatEvent cEvt:
                    {
                        if (cEvt.GetActiveMember() == "Player")
                        {
                            SendMessage("Self",
                                new AttackEvent("Player", cEvt.GetPassiveMemeber()),
                                0);
                            break;
                        }
                        EntityBehaviorStateMachine activeMembersMachine = npcs.GetStateMachine(cEvt.GetActiveMember());
                        Entity need = npcs.GetNPC(cEvt.GetActiveMember());
                        EventWithReceiver nextAction = activeMembersMachine.ProcessEvent(evt, need);
                        if (nextAction != null)
                        {
                            SendMessage(nextAction.actorName, nextAction.message, 0);
                        }
                        break;
                    }
                case NpcActionEvent nEvt:
                    {
                        EntityBehaviorStateMachine stateMachine = npcs.GetStateMachine(nEvt.GetNpcName());
                        Entity need = npcs.GetNPC(nEvt.GetNpcName());
                        EventWithReceiver nextAction = stateMachine.ProcessEvent(evt, need);
                        if (nextAction != null)
                        {
                            SendMessage(nextAction.actorName, nextAction.message, 0);
                        }
                        SendMessage("Self", new NpcActionEvent(nEvt.GetNpcName()), 4000);
                        break;
                    }
                case DeathEvent dEvt:
                    {
                        if (dEvt.GetNameOfDead() == "Player")
                        {
                            SendMessage("Player", new PrintEvent(String.Format("You died horribly!")), 0);
                            SendMessage("Self", new QuitEvent(), 0);
                        }
                        break;
                    }
                case QuitEvent qEvt:
                    {
                        Stop();
                        break;
                    }
                case FleeEvent fEvt:
                    {
                        string nameOfPersonMoving = fEvt.GetPersonRunning();
                        Entity personMoving = npcs.GetNPC(nameOfPersonMoving);
                        string selectedDoorName = personMoving.currentRoom.GetRandomDoorName();
                        Console.WriteLine("{0} has fled!", nameOfPersonMoving);
                        SendMessage("Self", new MoveEvent(nameOfPersonMoving, selectedDoorName), 0);
                        break;
                    }
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
