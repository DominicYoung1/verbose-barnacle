using MUD.Multithreading;
using System;

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

        public EventWithReceiver processCommand(string s)
        {
            // some sort of parser to take input strings and match them to the commands accepted trigger words/phrases
            // this needs to be able to do ALOT of shit.



            if (s == "help")
            {
                string m = ProcessHelp();
                return new EventWithReceiver(new PrintEvent(m), "Player");

            }

            if (s == "inv")
            {
                string m = ProcessInv();
                return new EventWithReceiver(new PrintEvent(m), "Player");

            }

            if (s == "look")
            {
                string m = ProcessLook();
                return new EventWithReceiver(new PrintEvent(m), "Player");

            }

            if (s.StartsWith("move"))
            {
                string m = ProcessMove(s);
                return new EventWithReceiver(new PrintEvent(m), "Player");

            }

            if (s.StartsWith("take"))
            {
                string m = ProcessTake(s);
                return new EventWithReceiver(new PrintEvent(m), "Player");

            }

            if (s.StartsWith("drop"))
            {
                string m = ProcessDrop(s);
                return new EventWithReceiver(new PrintEvent(m), "Player");

            }

            if (s.StartsWith("attack"))
            {
                //return ProcessAttack(s);
                string defender = s.Substring(7);
                return new EventWithReceiver(new CombatEvent("Player", defender), "Self");
            }
            if (s.StartsWith("inspect"))
            {
                string m = ProcessInspect(s);
                return new EventWithReceiver(new PrintEvent(m), "Player");

            }
            if (s.StartsWith("equip"))
            {
                string m = ProcessEquip(s);
                return new EventWithReceiver(new PrintEvent(m), "Player");

            }
            if (s.StartsWith("quit"))
            {
                return new EventWithReceiver(new QuitEvent(), "Self");
            }
            return new EventWithReceiver(new PrintEvent("Please enter a valid command."), "Player");
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
equip <item>: Equips an item from your inventory
";
        }

        string ProcessInv()
        {
            string s = "You look in bag and you see:";
            ArrayList<Item> inv = player.ListInventory();
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


        string ProcessInspect(string s)
        {
            // look to add ability to look at things in the players inventory/OR write a new function that does this.
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
