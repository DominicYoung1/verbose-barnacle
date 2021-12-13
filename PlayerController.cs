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

        public Entity PlayerInfo()
        {
            return player;
        }

        public string processCommand(string s)
        {
            // some sort of parser to take input strings and match them to the commands accepted trigger words/phrases
            // this needs to be able to do ALOT of shit.

            

            if (s == "help")
            {
                return ProcessHelp();
               
            }

            if (s == "inv")
            {
                return ProcessInv();
               
            }

            if (s == "look")
            {
                return ProcessLook();
                
            }

            if (s.StartsWith("move"))
            {
                return ProcessMove(s);
                
            }

            if (s.StartsWith("take"))
            {
                return ProcessTake(s);
                
            }

            if (s.StartsWith("drop"))
            {
                return ProcessDrop(s);
                
            }

            if (s.StartsWith("attack"))
            {
                return ProcessAttack(s);
                
            }
            if (s.StartsWith("inspect"))
            {
                return ProcessInspect(s);
              
            }
            if (s.StartsWith("equip"))
            {
                return ProcessEquip(s);
                
            } 
                return String.Format("Error! Please enter a valid command.");
        }

        string ProcessHelp()
        {
            return @"
Commands:
help: Shows a list of all commands
inv: Shows the current contents of the players inventory
look: Shows the information of the room the player is in
quit: Exits the game
move <direction>: Moves the player in the desired direction
take <item>: Takes an item in the room and puts in the player inventory
drop <item>: Removes an item from the player inventory and places it in the room
inspect <item>: Takes a closer look at an item
";    
        }

        string ProcessInv()
        {
            string s = "You look in bag and you see:";
            ArrayList<Item>  inv = player.ListInventory();
            for (int i = 0; i < inv.Length(); i++)
            {
                //Console.WriteLine("{0}", inv[i].Name());
                s = s + "\n" + inv[i].Name();
            }
            return s;
        }

        string ProcessLook()
        {
            Console.WriteLine("You take a look around.");
            string s = player.currentRoom.Describe();
            return s;
        }
        
        string ProcessMove(string s)
        {
            string s2 = s.Substring(5);
            player.Move(s2);
            return ProcessLook();
        }

        string ProcessTake(string s)
        {
            // needs to look through the list of items in a room for the input string
            // then needs to add the item to the players inventory
           Item queryItem = player.currentRoom.RemoveItem(s.Substring(5));
            if (queryItem != null)
            {
                player.PickupItem(queryItem);
            }
            return String.Format("You picked up {0}.", s);
        }

        string ProcessDrop(string s)
        {
            Item queryItem = player.RemoveItem(s.Substring(5));
            if (queryItem != null)
            {
                player.currentRoom.AddItem(queryItem);
            }
            return String.Format("You dropped {0}.", s);
        }

        string ProcessAttack(string s)
        {
            // grab the name of the entity being attacked
            // 
            // call the InitiateCombat function with player as a and the entity as d
            string dName = s.Substring(7);
           Entity defender = player.currentRoom.GetEntity(dName);
            player.InitiateCombat(defender);
            return "boi";
        }
        string ProcessInspect(string s)
        {
            string inspectName = s.Substring(8);
            string ret = "";
            for (int i = 0; i < player.currentRoom.ListItems().Length(); i++)
            {
                if (inspectName == player.currentRoom.ListItems()[i].Name())
                {
                    ret = player.currentRoom.ListItems()[i].Inspect();
                }
            }
            return ret;
        }

        string ProcessEquip(string s)
        {
            string equipName = s.Substring(6);
            for (int i = 0; i < player.ListInventory().Length(); i++)
            {
                if (equipName == player.ListInventory()[i].Name())
                {
                    if (player.ListInventory()[i] is Weapon w) // and example of PATTERN MATCHING (see notes)
                    {
                        player.EquipWeapon(w);
                        Console.WriteLine("Equipped {0}", w.Name());
                    }
                    if (player.ListInventory()[i] is Armor a)
                    {
                        player.EquipArmor(a);
                        Console.WriteLine("Equipped {0}", a.Name());
                    }
                }
            }
            return "boi";
        }
    }
}
