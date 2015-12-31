using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Xml;
using System.Text;

namespace Eternal_Coin
{
    public class Load
    {
        public static void LoadDisplayPictures(ContentManager Content)
        {
            XmlDocument displayPicDoc = new XmlDocument();
            displayPicDoc.Load("./Content/LoadData/LoadDisplayPictures.xml");
            XmlNodeList displayPics = displayPicDoc.SelectNodes("/pictures/picture");
            XmlNodeList eDisplayPics = displayPicDoc.SelectNodes("/pictures/epicture");

            foreach (XmlNode displayPic in displayPics)
            {
                Dictionaries.displayPictures.Add(displayPic["id"].InnerText, new DisplayPicture(displayPic["id"].InnerText, Content.Load<Texture2D>(displayPic["filepath"].InnerText)));
                Lists.displayPictureIDs.Add(displayPic["id"].InnerText);
            }

            foreach (XmlNode eDisplayPic in eDisplayPics)
            {
                Dictionaries.eDisplayPictures.Add(eDisplayPic["id"].InnerText, new DisplayPicture(eDisplayPic["id"].InnerText, Content.Load<Texture2D>(eDisplayPic["filepath"].InnerText)));
            }
        }

        public static void LoadGame(string name)
        {
            GVar.LogDebugInfo("Loading Game.", 1);
            XmlDocument saveDoc = new XmlDocument();
            XmlNode saveNode;
            for (int i = 0; i < Lists.savedGamesXmlDoc.Count; i++)
            {
                try
                {
                    saveNode = Lists.savedGamesXmlDoc[i].DocumentElement.SelectSingleNode("/savedgame");
                    if (name == saveNode[GVar.XmlTags.Player.name].InnerText)
                    {
                        saveDoc = Lists.savedGamesXmlDoc[i];
                        GVar.LogDebugInfo("Save File Found.", 2);
                    }
                }
                catch
                {
                    GVar.LogDebugInfo("!!!Save File Not Found!!!", 1);
                    break;
                }
            }
            Player player = new Player(Textures.pixel, new Vector2(GVar.gameScreenX / 2, GVar.gameScreenY / 2), new Vector2(20, 20), name, "Alive", Vector2.Zero, Color.Green, 100, 0, 0);
            saveNode = saveDoc.DocumentElement.SelectSingleNode("/savedgame");
            GVar.storyName = saveNode["storyname"].InnerText;
            GVar.displayPicID = saveNode["dpid"].InnerText;
            player.CurrentLocation.Add(SetStartingLocation(saveNode[GVar.XmlTags.Player.currentlocation].InnerText));

            

            Lists.entity.Add(player);
            XmlNodeList quests = saveDoc.SelectNodes("/savedgame/quest");
            foreach (XmlNode q in quests)
            {
                Lists.quests.Add(new Quest(q[GVar.XmlTags.QuestTags.description].InnerText, q[GVar.XmlTags.QuestTags.shortdescription].InnerText, q[GVar.XmlTags.QuestTags.completingaction].InnerText, q[GVar.XmlTags.QuestTags.completinglocation].InnerText, Convert.ToBoolean(q[GVar.XmlTags.QuestTags.completed].InnerText), q[GVar.XmlTags.QuestTags.locationfilepath].InnerText));
                GVar.LogDebugInfo("Guest Loaded: " + q[GVar.XmlTags.QuestTags.shortdescription].InnerText, 2);
            }
            saveNode = saveDoc.DocumentElement.SelectSingleNode("/savedgame/playerinventory");
            XmlNodeList piItemList = saveDoc.SelectNodes("/savedgame/playerinventory/item");
            int itemCount = 0;
            for (int i = 0; i < piItemList.Count; i++)
            {
                if (InventoryManager.playerInventory.itemSlots[itemCount].item == null)
                {
                    try
                    {
                        InventoryManager.playerInventory.itemSlots[itemCount].item = ItemBuilder.BuildItem(Dictionaries.items[piItemList[i][GVar.XmlTags.ItemTags.itemname].InnerText]);
                        Lists.playerItems.Add(InventoryManager.playerInventory.itemSlots[itemCount].item);

                        GVar.LogDebugInfo("Item Loaded To Player Inventory: " + ItemBuilder.GetItemInfo(InventoryManager.playerInventory.itemSlots[itemCount].item), 2);
                    }
                    catch
                    {
                        i--;
                    }
                }
                itemCount++;
            }

            XmlNodeList itemList = saveDoc.SelectNodes("/savedgame/characterinventory/item");
            GameTime gameTime = new GameTime();
            InventoryManager.characterInventory.UpdateInventoryBounds(gameTime);
            foreach (XmlNode I in itemList)
            {
                foreach (Player P in Lists.entity)
                {
                    Item item = ItemBuilder.BuildItem(Dictionaries.items[I[GVar.XmlTags.ItemTags.itemname].InnerText]);
                    Lists.playerItems.Add(item);
                    if (item.ItemClass == GVar.ItemClassName.weapon)
                    {
                        try
                        {
                            if (InventoryManager.characterInventory.itemSlots[GVar.InventorySlot.leftHandWeapon].item == null)
                            {
                                InventoryManager.characterInventory.itemSlots[GVar.InventorySlot.leftHandWeapon].item = item;
                                P.AddItemStats(InventoryManager.characterInventory.itemSlots[GVar.InventorySlot.leftHandWeapon].item);
                                Attack.AddAvailableAttacks(item.Attacks);
                                GVar.LogDebugInfo("Weapon Loaded To Character Inventory: " + ItemBuilder.GetItemInfo(InventoryManager.characterInventory.itemSlots[GVar.InventorySlot.leftHandWeapon].item), 2);
                            }
                            else if (InventoryManager.characterInventory.itemSlots[GVar.InventorySlot.leftHandWeapon].item != null && InventoryManager.characterInventory.itemSlots[GVar.InventorySlot.rightHandWeapon].item == null)
                            {
                                InventoryManager.characterInventory.itemSlots[GVar.InventorySlot.rightHandWeapon].item = item;
                                P.AddItemStats(InventoryManager.characterInventory.itemSlots[GVar.InventorySlot.rightHandWeapon].item);
                                Attack.AddAvailableAttacks(item.Attacks);
                                GVar.LogDebugInfo("Weapon Loaded To Character Inventory: " + ItemBuilder.GetItemInfo(InventoryManager.characterInventory.itemSlots[GVar.InventorySlot.rightHandWeapon].item), 2);
                            }
                        }
                        catch (Exception e)
                        {
                            GVar.LogDebugInfo("!!!Failed To Load Weapon To Character Inventory: " + e, 1);
                        }
                    }
                    else if (item.ItemClass == GVar.ItemClassName.armor)
                    {
                        for (int i = 0; i < Lists.inventorySlots.Count; i++)
                        {
                            try
                            {
                                if (item.InventorySlot == InventoryManager.characterInventory.itemSlots[Lists.inventorySlots[i]].inventorySlot)
                                {
                                    InventoryManager.characterInventory.itemSlots[Lists.inventorySlots[i]].item = item;
                                    P.AddItemStats(InventoryManager.characterInventory.itemSlots[Lists.inventorySlots[i]].item);
                                    GVar.LogDebugInfo("Armor Loaded To Character Inventory: " + ItemBuilder.GetItemInfo(InventoryManager.characterInventory.itemSlots[Lists.inventorySlots[i]].item), 2);
                                }
                            }
                            catch (Exception e)
                            {
                                GVar.LogDebugInfo("!!!Failed To Load Armor To Character Inventory: " + e, 1);
                            }
                        }
                    }
                    else if (item.ItemClass == GVar.ItemClassName.jewellry)
                    {
                        for (int i = 0; i < Lists.inventorySlots.Count; i ++)
                        {
                            try
                            {
                                if (InventoryManager.characterInventory.itemSlots[Lists.inventorySlots[i]].inventorySlot.Contains(item.InventorySlot) && InventoryManager.characterInventory.itemSlots[Lists.inventorySlots[i]].item == null)
                                {
                                    InventoryManager.characterInventory.itemSlots[Lists.inventorySlots[i]].item = item;
                                    P.AddItemStats(InventoryManager.characterInventory.itemSlots[Lists.inventorySlots[i]].item);
                                    GVar.LogDebugInfo("Jewelry Loaded To Character Inventory: " + ItemBuilder.GetItemInfo(InventoryManager.characterInventory.itemSlots[Lists.inventorySlots[i]].item), 2);
                                    break;
                                }
                            }
                            catch (Exception e)
                            {
                                GVar.LogDebugInfo("!!!Failed to load jewelry to character inventory: " + e, 1);
                            }
                        }
                    }
                }
            }
        }

        public static LocationNode SetStartingLocation(string location)
        {
            foreach (LocationNode lc in Lists.locNodes)
            {
                if (lc.SubName == location)
                {
                    

                    return lc;
                }
            }
            return null;
        }

        public static void LoadItemData(ContentManager Content)
        {
            XmlDocument itemData = new XmlDocument();
            itemData.Load("./Content/LoadData/ItemData.xml");
            XmlNodeList itemTypes = itemData.SelectNodes("/itemdata/itemtype");
            XmlNodeList materials = itemData.SelectNodes("/itemdata/material");

            foreach (XmlNode it in itemTypes)
            {
                try
                {
                    Texture2D tempTex = Content.Load<Texture2D>(it["imagefilepath"].InnerText);
                    Dictionaries.itemTextures.Add(it["name"].InnerText, tempTex);
                    Dictionaries.itemTypes.Add(it["name"].InnerText, new ItemType(it["name"].InnerText, Convert.ToInt32(it["cost"].InnerText), Convert.ToInt32(it["armor"].InnerText), Convert.ToInt32(it["damage"].InnerText)));
                }
                catch (Exception first)
                {
                    Dictionaries.itemTextures.Remove(it["name"].InnerText);
                    GVar.LogDebugInfo("!!!ERROR!!! first pass of creating item type failed, trying second. [" + first + "]", 1);
                    try
                    {
                        Texture2D tempTex = Content.Load<Texture2D>(it["imagefilepath"].InnerText);
                        Dictionaries.itemTextures.Add(it["name"].InnerText, tempTex);
                        Dictionaries.itemTypes.Add(it["name"].InnerText, new ItemType(it["name"].InnerText, Convert.ToInt32(it["cost"].InnerText)));
                    }
                    catch (Exception second)
                    {
                        GVar.LogDebugInfo("!!!ERROR!!! second pass of creating item type failed. [" + it["name"].InnerText + it["imagefilepath"].InnerText + "]-[" + second + "]", 1);
                    }
                }
            }

            foreach (XmlNode m in materials)
            {
                try
                {
                    Dictionaries.materials.Add(m["name"].InnerText, new Material(m["name"].InnerText, Convert.ToInt32(m["cost"].InnerText), Convert.ToInt32(m["armor"].InnerText), Convert.ToInt32(m["damage"].InnerText)));
                }
                catch
                {
                    GVar.LogDebugInfo("!!!Failed To Load ItemMaterial[name: " + m["name"].InnerText + "]!!!", 1);
                }
            }
        }

        public static void LoadWorldMaps(ContentManager Content)
        {
            foreach (LocationNode ln in Lists.locNodes)
            {
                if (ln.State == "Main")
                {
                    ln.SubLocNode.Add(Dictionaries.locNodes[ln.SubLocNodeName]);
                    foreach (string locName in ln.LocNodeConName)
                    {
                        ln.AddConnection(Dictionaries.locNodes[locName]);
                    }
                }
                else if (ln.State == "Sub")
                {
                    ln.MainLocNode.Add(Dictionaries.locNodes[ln.MainLocNodeName]);
                    foreach (string locName in ln.LocNodeConName)
                    {
                        ln.AddConnection(Dictionaries.locNodes[locName]);
                    }

                    string mapDir = "LocationTemplates/" + GVar.storyName + "/" + ln.MainName + "/" + ln.MainName + "Map";
                    Texture2D tempTex = Content.Load<Texture2D>(mapDir);
                    Dictionaries.maps.Add(ln.MainName + "Map", tempTex);
                    GVar.LogDebugInfo("Map Created: " + ln.MainName + "Map", 2);
                }
                else
                {
                    foreach (string locName in ln.LocNodeConName)
                    {
                        ln.AddConnection(Dictionaries.locNodes[locName]);
                    }
                }

                foreach (LocationNode locNode in ln.LocNodeConnections)
                {
                    XmlDocument locNodeDoc = new XmlDocument();
                    string filePath = "./Content/GameFiles/" + GVar.playerName + "/" + locNode.LocatoinFilePath;
                    locNodeDoc.Load(filePath);

                    XmlNode node = locNodeDoc.SelectSingleNode("/location/searched");

                    locNode.Searched = Convert.ToBoolean(node.InnerText);
                }
            }
        }

        public static void LoadLocationNodes(ContentManager Content)
        {
            GVar.worldMap = new WorldMap(Textures.worldMapTex, Vector.worldMapPosition, Vector.worldMapSize, Color.White);

            Vector2 mainLocationSize = new Vector2(20, 20);
            Vector2 subLocationSize = new Vector2(15, 15);

            XmlDocument createLocationNodes = new XmlDocument();
            createLocationNodes.Load("./Content/LoadData/CreateLocationNodes.xml");
            XmlNodeList nodes = createLocationNodes.SelectNodes("locationnode/node");

            foreach (XmlNode n in nodes)
            {
                if (n["state"].InnerText == "Main")
                {
                    LocationNode locNode = new LocationNode(Textures.locationNodeTex, new Vector2(Convert.ToInt32(n["positionx"].InnerText), Convert.ToInt32(n["positiony"].InnerText)), new Vector2(Convert.ToInt32(n["positionx"].InnerText), Convert.ToInt32(n["positiony"].InnerText)), mainLocationSize, Color.White, false, n["name"].InnerText, n["mainname"].InnerText, n["subname"].InnerText, n["state"].InnerText);
                    GVar.LogDebugInfo("LocationCreated: " + locNode.Name, 2);
                    locNode.MainLocNodeName = n["mainlocnode"].InnerText;
                    locNode.SubLocNodeName = n["sublocnode"].InnerText;
                    XmlNodeList nodeCon = createLocationNodes.SelectNodes("locationnode/node/" + n["subname"].InnerText + "connection");
                    foreach (XmlNode nc in nodeCon)
                    {
                        locNode.LocNodeConName.Add(nc.InnerText);
                    }
                    Dictionaries.locNodes.Add(n["subname"].InnerText, locNode);
                    Lists.locNodes.Add(locNode);
                }
                else if (n["state"].InnerText == "Sub")
                {
                    LocationNode locNode = new LocationNode(Textures.locationNodeTex, new Vector2(Convert.ToInt32(n["positionx"].InnerText), Convert.ToInt32(n["positiony"].InnerText)), new Vector2(Convert.ToInt32(n["positionx"].InnerText), Convert.ToInt32(n["positiony"].InnerText)), mainLocationSize, Color.White, false, n["name"].InnerText, n["mainname"].InnerText, n["subname"].InnerText, n["state"].InnerText);
                    GVar.LogDebugInfo("LocationCreated: " + locNode.Name, 2);
                    locNode.MainLocNodeName = n["mainlocnode"].InnerText;
                    locNode.SubLocNodeName = n["sublocnode"].InnerText;
                    XmlNodeList nodeCon = createLocationNodes.SelectNodes("locationnode/node/" + n["subname"].InnerText + "connection");
                    foreach (XmlNode nc in nodeCon)
                    {
                        locNode.LocNodeConName.Add(nc.InnerText);
                    }
                    Dictionaries.locNodes.Add(n["subname"].InnerText, locNode);
                    Lists.locNodes.Add(locNode);
                }
                else
                {
                    LocationNode locNode = new LocationNode(Textures.locationNodeTex, new Vector2(Convert.ToInt32(n["positionx"].InnerText), Convert.ToInt32(n["positiony"].InnerText)), new Vector2(Convert.ToInt32(n["positionx"].InnerText), Convert.ToInt32(n["positiony"].InnerText)), subLocationSize, Color.White, false, n["name"].InnerText, n["mainname"].InnerText, n["subname"].InnerText, n["state"].InnerText);
                    GVar.LogDebugInfo("LocationCreated: " + locNode.Name, 2);
                    locNode.MainLocNodeName = n["mainlocnode"].InnerText;
                    locNode.SubLocNodeName = n["sublocnode"].InnerText;
                    XmlNodeList nodeCon = createLocationNodes.SelectNodes("locationnode/node/" + n["subname"].InnerText + "connection");
                    foreach (XmlNode nc in nodeCon)
                    {
                        locNode.LocNodeConName.Add(nc.InnerText);
                    }
                    Dictionaries.locNodes.Add(n["subname"].InnerText, locNode);
                    Lists.locNodes.Add(locNode);
                }
            }

            GVar.npc = new NPC();
        }
    }
}
