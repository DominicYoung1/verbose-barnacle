using System;
using System.Collections.Generic;
using System.Text;

namespace MUD
{
    public class PlayerController
    {
        Entity player;
        ArrayList<Room> rooms;


        public PlayerController(Entity p, ArrayList<Room> r)
        {
            player = p;
            rooms = r;
        }

        public void processCommand(string s)
        {
            // some sort of parser to take input strings and match them to the commands accepted trigger words/phrases
            // this needs to be able to do ALOT of shit.

            if (s == "help")
            {
                ProcessHelp();
            }

            if (s == "inv")
            {
                ProcessInv();
            }

            if (s == "look")
            {
                ProcessLook();
            }

            if (s.StartsWith("move"))
            {
                ProcessMove(s);
            }

            if (s.StartsWith("take"))
            {
                ProcessTake(s);
            }

            if (s.StartsWith("drop"))
            {
                ProcessDrop(s);
            }

            if (s.StartsWith("attack"))
            {
                ProcessAttack(s);
            }
        }

        void ProcessHelp()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("help: Shows a list of all commands");
            Console.WriteLine("inv: Shows the current contents of the players inventory");
            Console.WriteLine("look: Shows the information of the room the player is in");
            Console.WriteLine("quit: Exits the game");
            Console.WriteLine("move <direction>: Moves the player in the desired direction");
            Console.WriteLine("take <item>: Takes an item in the room and puts in the player inventory");
            Console.WriteLine("drop <item>: Removes an item from the player inventory and places it in the room");
        }

        void ProcessInv()
        {
            Console.WriteLine("You look in bag and you see:");
          ArrayList<Item>  inv = player.ListInventory();
            for (int i = 0; i < inv.Length(); i++)
            {
                Console.WriteLine("{0}", inv[i].name);
            }
        }

        void ProcessLook()
        {
            Console.WriteLine("You take a look around.");
            player.currentRoom.Describe();
        }
        
        void ProcessMove(string s)
        {
            string s2 = s.Substring(5);
            player.Move(s2);
            ProcessLook();
        }

        void ProcessTake(string s)
        {
            // needs to look through the list of items in a room for the input string
            // then needs to add the item to the players inventory
           Item queryItem = player.currentRoom.RemoveItem(s.Substring(5));
            Console.WriteLine("picking up {0}", queryItem);
            if (queryItem != null)
            {
                player.PickupItem(queryItem);
            }
            Console.WriteLine("You picked up {0}.", s);
        }

        void ProcessDrop(string s)
        {
            Item queryItem = player.RemoveItem(s.Substring(5));
            if (queryItem != null)
            {
                player.currentRoom.AddItem(queryItem);
            }
            Console.WriteLine("You dropped {0}.", s);
        }

        void ProcessAttack(string s)
        {
            // grab the name of the entity being attacked
            // 
            // call the InitiateCombat function with player as a and the entity as d
            string dName = s.Substring(7);
           Entity defender = player.currentRoom.GetEntity(dName);
            player.InitiateCombat(defender);
        }
    }
}
