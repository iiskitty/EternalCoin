using System.Collections.Generic;
using System.IO;
using System;
using System.Xml;

namespace Eternal_Coin
{
    public class Save
    {
        public static void SaveGame(string saveGameLocation, Entity P, List<Quest> quests)
        {
            string saveLocation = "";
            GVar.LogDebugInfo("Saving Game.", 1);
            try
            {
                for (int i = 0; i < Lists.savedGamesXmlDoc.Count; i++)
                {
                    XmlNode saveNode = Lists.savedGamesXmlDoc[i].DocumentElement.SelectSingleNode("/savedgame");
                    if (P.Name == saveNode[GVar.XmlTags.Player.name].InnerText)
                    {
                        saveLocation = GVar.savedGameLocation + saveNode["filenumber"].InnerText + ".xml";
                        File.Delete(saveLocation);
                        GVar.LogDebugInfo("Save File Found.", 2);
                        break;
                    }
                }
            }
            catch 
            {
                GVar.LogDebugInfo("!!!Failed To Find SaveFile!!!", 1);
            }
            int num = 0;
            if (saveLocation == "")
            {
                for (int i = 0; i < 10; i++)
                {
                    saveLocation = GVar.savedGameLocation + i.ToString() + ".xml";
                    num = i;
                    if (!File.Exists(saveLocation))
                    {
                        break;
                    }
                }
            }
            if (!File.Exists(saveLocation))
            {
                XmlDocument tempDoc = new XmlDocument();

                XmlDeclaration declaration = tempDoc.CreateXmlDeclaration("1.0", string.Empty, string.Empty);
                XmlElement element = tempDoc.DocumentElement;
                tempDoc.InsertBefore(declaration, element);

                XmlElement body = tempDoc.CreateElement(string.Empty, "savedgame", string.Empty);
                tempDoc.AppendChild(body);

                XmlElement fileName = tempDoc.CreateElement(string.Empty, "filename", string.Empty);
                XmlText fileNameInner = tempDoc.CreateTextNode(saveLocation);
                fileName.AppendChild(fileNameInner);
                body.AppendChild(fileName);

                XmlElement fileNumber = tempDoc.CreateElement(string.Empty, "filenumber", string.Empty);
                XmlText fileNumberInner = tempDoc.CreateTextNode(num.ToString());
                fileNumber.AppendChild(fileNumberInner);
                body.AppendChild(fileNumber);

                XmlElement dpid = tempDoc.CreateElement(string.Empty, "dpid", string.Empty);
                XmlText dpidInner = tempDoc.CreateTextNode(GVar.displayPicID);
                dpid.AppendChild(dpidInner);
                body.AppendChild(dpid);

                XmlElement name = tempDoc.CreateElement(string.Empty, "name", string.Empty);
                XmlText nameInner = tempDoc.CreateTextNode(P.Name);
                name.AppendChild(nameInner);
                body.AppendChild(name);

                XmlElement storyName = tempDoc.CreateElement(string.Empty, "storyname", string.Empty);
                XmlText storyNameInner = tempDoc.CreateTextNode(GVar.storyName);
                storyName.AppendChild(storyNameInner);
                body.AppendChild(storyName);

                XmlElement currentLocation = tempDoc.CreateElement(string.Empty, "currentlocation", string.Empty);
                XmlText currentLocationInner = tempDoc.CreateTextNode(P.CurrentLocation.SubName);
                currentLocation.AppendChild(currentLocationInner);
                body.AppendChild(currentLocation);

                for (int i = 0; i < quests.Count; i++)
                {
                    try
                    {
                        XmlElement quest = tempDoc.CreateElement(string.Empty, "quest", string.Empty);
                        body.AppendChild(quest);

                        XmlElement questid = tempDoc.CreateElement(string.Empty, "questid", string.Empty);
                        XmlText questidInner = tempDoc.CreateTextNode(quests[i].QuestID);
                        questid.AppendChild(questidInner);
                        quest.AppendChild(questid);

                        XmlElement description = tempDoc.CreateElement(string.Empty, GVar.XmlTags.QuestTags.description, string.Empty);
                        XmlText descriptionInner = tempDoc.CreateTextNode(quests[i].Description);
                        description.AppendChild(descriptionInner);
                        quest.AppendChild(description);

                        XmlElement shortDescription = tempDoc.CreateElement(string.Empty, GVar.XmlTags.QuestTags.shortdescription, string.Empty);
                        XmlText shortDescriptionInner = tempDoc.CreateTextNode(quests[i].ShortDescription);
                        shortDescription.AppendChild(shortDescriptionInner);
                        quest.AppendChild(shortDescription);

                        XmlElement completingAction = tempDoc.CreateElement(string.Empty, GVar.XmlTags.QuestTags.completingaction, string.Empty);
                        XmlText completingActionInner = tempDoc.CreateTextNode(quests[i].CompletingAction);
                        completingAction.AppendChild(completingActionInner);
                        quest.AppendChild(completingAction);

                        XmlElement completingLocation = tempDoc.CreateElement(string.Empty, GVar.XmlTags.QuestTags.completinglocation, string.Empty);
                        XmlText completingLocationInner = tempDoc.CreateTextNode(quests[i].CompletingLocation);
                        completingLocation.AppendChild(completingLocationInner);
                        quest.AppendChild(completingLocation);

                        XmlElement completed = tempDoc.CreateElement(string.Empty, GVar.XmlTags.QuestTags.completed, string.Empty);
                        XmlText completedInner = tempDoc.CreateTextNode(Convert.ToString(quests[i].Completed));
                        completed.AppendChild(completedInner);
                        quest.AppendChild(completed);

                        XmlElement locationFilePath = tempDoc.CreateElement(string.Empty, GVar.XmlTags.QuestTags.locationfilepath, string.Empty);
                        XmlText locationFilePathInner = tempDoc.CreateTextNode(quests[i].LocationFilePath);
                        locationFilePath.AppendChild(locationFilePathInner);
                        quest.AppendChild(locationFilePath);

                        GVar.LogDebugInfo("Quest Saved." + quests[i].ShortDescription, 2);
                    }
                    catch 
                    {
                        GVar.LogDebugInfo("!!!Failed To Save Quest", 1);
                    }
                    
                }
                XmlElement pInventory = tempDoc.CreateElement(string.Empty, "playerinventory", string.Empty);
                body.AppendChild(pInventory);
                for (int i = 0; i < 40; i++)
                {
                    if (InventoryManager.playerInventory.ItemSlots[i].item != null)
                    {
                        try
                        {
                            pInventory.AppendChild(SaveXml.CreateItemXmlElement(InventoryManager.playerInventory.ItemSlots[i].item, tempDoc));

                            GVar.LogDebugInfo("Item Saved To Player Inventory: " + ItemBuilder.GetItemInfo(InventoryManager.playerInventory.ItemSlots[i].item), 2);
                        }
                        catch
                        {
                            GVar.LogDebugInfo("!!!Failed To Save Weapon To Player Inventory!!!", 1);
                        }
                    }
                }
                XmlElement cInventory = tempDoc.CreateElement(string.Empty, "characterinventory", string.Empty);
                body.AppendChild(cInventory);
                for (int i = 0; i < Lists.inventorySlots.Count; i++)
                {
                    if (InventoryManager.characterInventory.ItemSlots[Lists.inventorySlots[i]].item != null)
                    {
                        try
                        {
                            cInventory.AppendChild(SaveXml.CreateItemXmlElement(InventoryManager.characterInventory.ItemSlots[Lists.inventorySlots[i]].item, tempDoc));

                            GVar.LogDebugInfo("Item Saved To Character Inventory: " + ItemBuilder.GetItemInfo(InventoryManager.characterInventory.ItemSlots[Lists.inventorySlots[i]].item), 2);
                        }
                        catch
                        {
                            GVar.LogDebugInfo("!!!Failed To Save Item To Character Inventory!!!", 1);
                        }
                    }
                }
                Lists.savedGamesXmlDoc.Add(tempDoc);
                tempDoc.Save(saveLocation);
                GVar.LogDebugInfo("Game Saved.", 1);
            }
        }
    }
}
