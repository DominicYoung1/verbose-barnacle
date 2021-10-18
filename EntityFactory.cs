using System;
using System.Collections.Generic;
using System.Text;

namespace MUD
{
    public enum EntityType
    {
        Skeleton,
        Goblin,
        Jim
    }

    public class EntityFactory
    {
        public static Entity CreateEntity(EntityType type, string name, Room room)
        {
            switch (type)
            {
                case EntityType.Skeleton:
                    {
                        Entity ret = new Entity(room, name);
                        ret.equipedWeapon = new Weapon("Bone", "I've got a bone to pick with you", 3.0);
                        ret.equipedArmor = new Armor("Bones", "My bones rattle", 1.0);
                        ret.health = 7.0;
                        ret.combatStats = new CombatStats(5.0, 2.0);
                        return ret;
                    }
                case EntityType.Goblin:
                    {
                        Entity ret = new Entity(room, name);
                        ret.equipedWeapon = new Weapon("Shank", "Shawdemption", 4.0);
                        ret.equipedArmor = new Armor("Dirt", "Is in fact the dirtiest", 1.0);
                        ret.health = 6.0;
                        ret.combatStats = new CombatStats(3.0, 2.0);
                        return ret;
                    }
                case EntityType.Jim:
                    {
                        Entity ret = new Entity(room, name);
                        ret.equipedWeapon = new Weapon("JimDozer", "The Universe cannot comprehend this", 9.0);
                        ret.equipedArmor = new Armor("Tactical Jorts", "Flexible and stylish", 10.0);
                        ret.health = 25.0;
                        ret.combatStats = new CombatStats(10.0, 15.0);
                        return ret;
                    }
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
