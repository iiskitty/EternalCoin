using Microsoft.Xna.Framework;

namespace Eternal_Coin
{
    public class ItemBuilder
    {
        public static Item BuildItem(string itemClass, int playerInventorySlot, string inventorySlot, string itemName, ItemType type, Material material)
        {
            if (itemClass == GVar.ItemClassName.weapon)
            {
                Weapon weapon = new Weapon(Dictionaries.itemTextures[type.name], Vector2.Zero, new Vector2(71, 71), Color.White, itemClass, material.cost + type.cost, type.damage + material.damage, itemName, playerInventorySlot, inventorySlot, material, type);
                GVar.LogDebugInfo("ItemCreated: " + GetItemInfo(weapon), 2);
                return weapon;
            }
            else if (itemClass == GVar.ItemClassName.armor)
            {
                Armor armor = new Armor(Dictionaries.itemTextures[type.name], Vector2.Zero, new Vector2(71, 71), Color.White, itemClass, material.cost + type.cost, material.armor + type.armor, itemName, playerInventorySlot, inventorySlot, material, type);
                GVar.LogDebugInfo("ItemCreated: " + GetItemInfo(armor), 2);
                return armor;
            }
            else if (itemClass == GVar.ItemClassName.jewellry)
            {
                Jewellry jewellry = new Jewellry(Dictionaries.itemTextures[type.name], Vector2.Zero, new Vector2(71, 71), Color.White, itemClass, itemName, material.cost + type.cost, inventorySlot, material, type);
                GVar.LogDebugInfo("ItemCreated: " + GetItemInfo(jewellry), 2);
                return jewellry;
            }
            else if (itemClass == GVar.ItemClassName.eternalcoin)
            {
                EternalCoin eternalCoin = new EternalCoin(Dictionaries.itemTextures[type.name], Vector2.Zero, new Vector2(71, 71), Color.White, itemClass, itemName, material.cost + type.cost, inventorySlot, material, type);
                GVar.LogDebugInfo("ItemCreated: " + GetItemInfo(eternalCoin), 2);
                return eternalCoin;
            }
            return null;
        }
        public static Item BuildItem(Item item)
        {
            if (item.ItemClass == GVar.ItemClassName.weapon)
            {
                Weapon weapon = new Weapon(Dictionaries.itemTextures[item.Type.name], Vector2.Zero, new Vector2(71, 71), Color.White, item.ItemClass, item.Material.cost + item.Type.cost, item.Type.damage + item.Material.damage, item.ItemName, item.PlayerInventorySlot, item.InventorySlot, item.Material, item.Type);
                weapon.Attacks = item.Attacks;
                GVar.LogDebugInfo("ItemCreated: " + GetItemInfo(weapon), 2);
                return weapon;
            }
            else if (item.ItemClass == GVar.ItemClassName.armor)
            {
                Armor armor = new Armor(Dictionaries.itemTextures[item.Type.name], Vector2.Zero, new Vector2(71, 71), Color.White, item.ItemClass, item.Material.cost + item.Type.cost, item.Material.armor + item.Type.armor, item.ItemName, item.PlayerInventorySlot, item.InventorySlot, item.Material, item.Type);
                GVar.LogDebugInfo("ItemCreated: " + GetItemInfo(armor), 2);
                return armor;
            }
            else if (item.ItemClass == GVar.ItemClassName.jewellry)
            {
                Jewellry jewellry = new Jewellry(Dictionaries.itemTextures[item.Type.name], Vector2.Zero, new Vector2(71, 71), Color.White, item.ItemClass, item.ItemName, item.Material.cost + item.Type.cost, item.InventorySlot, item.Material, item.Type);
                GVar.LogDebugInfo("ItemCreated: " + GetItemInfo(jewellry), 2);
                return jewellry;
            }
            else if (item.ItemClass == GVar.ItemClassName.eternalcoin)
            {
                EternalCoin eternalCoin = new EternalCoin(Dictionaries.itemTextures[item.Type.name], Vector2.Zero, new Vector2(71, 71), Color.White, item.ItemClass, item.ItemName, item.Material.cost + item.Type.cost, item.InventorySlot, item.Material, item.Type);
                eternalCoin.Attacks = item.Attacks;
                GVar.LogDebugInfo("ItemCreated: " + GetItemInfo(eternalCoin), 2);
                return eternalCoin;
            }
            return null;
        }

        public static string GetItemInfo(Item item)
        {
            string itemInfo = "";

            int itemDamage = item.Type.damage + item.Material.damage;
            int itemArmor = item.Type.armor + item.Material.armor;

            itemInfo = "[Class: " + item.ItemClass + ", Name: " + item.ItemName + ", Inventory Slot: " + item.InventorySlot + ", Cost: " + item.Cost.ToString() + ", Damage: " + itemDamage.ToString() + ", Armor: " + itemArmor.ToString() + ", Type: " + item.Type.name + ", Material: " + item.Material.name + "]";

            return itemInfo;
        }
    }
}