using System;
using System.Collections.Generic;
using System.Text;

namespace MUD
{
    public class Entity
    {
        ArrayList<Item> inventory;
        public Room currentRoom;
        public string name;
        double health;
        CombatStats combatStats;
        //string kind static/moblie/player ASK JORDAN ABOUT IT LATER
        

        public Entity(Room startingRoom, string id)
        {
            inventory = new ArrayList<Item>(10);
            currentRoom = startingRoom;
            name = id;
            currentRoom.AddOccupant(this);
            //facingDirection = startingDirection;
        }

        public ArrayList<Item> ListInventory()
        {
            return inventory;
        }

        public void Move(string destination)
        {
            // Use the input string and find the matching key in the room's dictionary
            // change the current room to that room

            if (currentRoom.doors.ContainsKey(destination))
            {
                currentRoom.RemoveOccupant(this);
                currentRoom = currentRoom.doors[destination];
                currentRoom.AddOccupant(this);
            }
        }

        public Item RemoveItem(string name)
        {
            for (int i = 0; i < inventory.Length(); i++)
            {
                if (name == inventory[i].name)
                {
                    return inventory.Remove(i);
                }
            }

            return null;
        }

        public void PickupItem(Item item)
        {
            inventory.Push(item);
        }

        public void InitiateCombat(Entity d)
        {
           CombatController combat = new CombatController(this, d);
           combat.ProcessCombat();
        }

        public void TakeDamage(double dmg)
        {
            health -= combatStats.ComputeDamageTaken(dmg);
        }

        public double ImpartDamage()
        {
            return combatStats.ComputeDamageImparted();
        }

        public bool Alive()
        {
            return health > 0.0;
        }
    }
}
    
