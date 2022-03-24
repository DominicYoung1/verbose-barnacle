namespace MUD
{
    public class Entity
    {
        //ArrayList<Armor> armor;
        //ArrayList<Weapon> weapons;
        ArrayList<Item> inventory;
        public Room currentRoom;
        public string name;
        public double health;
        public CombatStats combatStats;
        public Weapon equipedWeapon;
        public Armor equipedArmor;
        //string kind static/moblie/player ASK JORDAN ABOUT IT LATER


        public Entity(Room startingRoom, string id)
        {
            inventory = new ArrayList<Item>(10);
            currentRoom = startingRoom;
            name = id;
            currentRoom.AddOccupant(this);
            equipedArmor = null;
            equipedWeapon = null;
            health = 15.0;
            combatStats = new CombatStats();
            //facingDirection = startingDirection;
        }

        //public ArrayList<Weapon> ListWeapons()
        //{
        //return weapons;
        //}

        //public ArrayList<Armor> ListArmor()
        //{
        //return armor;
        //}

        public ArrayList<Item> ListInventory()
        {
            //ListWeapons();
            //ListArmor();
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
                if (name == inventory[i].Name())
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

        public bool EquipWeapon(Weapon w)
        {
            equipedWeapon = w;
            return true;
        }

        public bool EquipArmor(Armor a)
        {
            equipedArmor = a;
            return true;
        }

        // prompt the user for input on what they sould do after each round of combat.
        public void CombatAction()
        {

        }

        //public void Dead()
        //{
        //    for (int i = 0; i < inventory.Length(); i++)
        //    {
        //        inventory.RemoveItem(i.name);
        //    }
        //}
    }
}

