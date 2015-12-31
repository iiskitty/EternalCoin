using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;
using System.Xml;
using System.Text;

namespace Eternal_Coin
{
    public class Quest
    {
        string description;
        string shortDescription;
        string completingAction;
        string completingLocation;
        string locationFilePath;

        bool completed;

        public Quest(string description, string shortDescription, string completingAction, string completingLocation, bool completed, string locationFilePath)
        {
            this.description = description;
            this.shortDescription = shortDescription;
            this.completingAction = completingAction;
            this.completingLocation = completingLocation;
            this.completed = completed;
            this.locationFilePath = locationFilePath;
        }

        public static void AcceptQuest(Entity P)
        {
            XmlNode locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc");
            locNPC[GVar.XmlTags.QuestTags.hasquest].InnerText = "False";
            locNPC[GVar.XmlTags.QuestTags.questaccepted].InnerText = "True";
            locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/quest");
            LocationNode temp = P.CurrentLocation[0];
            Lists.quests.Add(new Quest(locNPC[GVar.XmlTags.QuestTags.description].InnerText, locNPC[GVar.XmlTags.QuestTags.shortdescription].InnerText, locNPC[GVar.XmlTags.QuestTags.completingaction].InnerText, locNPC[GVar.XmlTags.QuestTags.completinglocation].InnerText, false, "Content/GameFiles/" + P.Name + "/" + temp.LocatoinFilePath));
            foreach (UIElement ui in Lists.uiElements)
            {
                if (ui.SpriteID == Textures.questListUI && ui.Draw)
                {
                    Lists.viewQuestInfoButtons.Add(new Button(Textures.clearPixel, new Vector2(), new Vector2(268, 15), Color.White, "ViewQuestInfo", "Alive", 0f));
                }
            }
            locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/greeting");
            GVar.npc.Greeting = locNPC[GVar.XmlTags.NPCTags.Greetings.acceptquest].InnerText;
            GVar.npc.Greeting = Text.WrapText(Fonts.lucidaConsole14Regular, GVar.npc.Greeting, 150);
            SaveXml.SaveLocationXmlFile(P, temp);
            Save.SaveGame(GVar.savedGameLocation, P, Lists.quests);
        }

        public static void HandInQuest(Entity P)
        {
            XmlNode locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc");
            LocationNode temp = P.CurrentLocation[0];
            locNPC[GVar.XmlTags.QuestTags.questfinished].InnerText = "False";
            locNPC[GVar.XmlTags.QuestTags.questcompleted].InnerText = "True";
            locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/greeting");
            GVar.npc.Greeting = locNPC[GVar.XmlTags.NPCTags.Greetings.handinquest].InnerText;
            GVar.npc.Greeting = Text.WrapText(Fonts.lucidaConsole14Regular, GVar.npc.Greeting, 150);
            locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/quest");
            for (int k = 0; k < Lists.quests.Count; k++)
            {
                if (Lists.quests[k].CompletingAction == locNPC[GVar.XmlTags.QuestTags.completingaction].InnerText)
                {
                    Lists.quests.RemoveAt(k);
                    if (Lists.viewQuestInfoButtons.Count > 0)
                        Lists.viewQuestInfoButtons.RemoveAt(k);
                }
            }
            XmlNodeList questRewardItems = GVar.curLocNode.SelectNodes("/location/npc/quest/reward/item");
            foreach (XmlNode QRI in questRewardItems)
            {
                
                for (int i = 0; i < 40; i++)
                {
                    if (InventoryManager.playerInventory.itemSlots[i].item == null)
                    {
                        InventoryManager.playerInventory.itemSlots[i].item = ItemBuilder.BuildItem(Dictionaries.items[QRI[GVar.XmlTags.ItemTags.itemname].InnerText]);
                        Lists.items.Add(InventoryManager.playerInventory.itemSlots[i].item);
                        break;
                    }
                }
            }
            SaveXml.SaveLocationXmlFile(P, temp);
            Save.SaveGame(GVar.savedGameLocation, P, Lists.quests);
        }

        public static void CheckAction(string action, Node locationNode)
        {
            for (int i = 0; i < Lists.quests.Count; i++)
            {
                if (action.Contains("Entering") && Lists.quests[i].CompletingAction.Contains("Exploring") && Lists.quests[i].CompletingLocation == locationNode.SubName && locationNode.Searched)
                {
                    Lists.quests[i].Completed = true;
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(Lists.quests[i].LocationFilePath);
                    XmlNode locNode = xmlDoc.DocumentElement.SelectSingleNode("/location/npc");
                    locNode[GVar.XmlTags.QuestTags.questfinished].InnerText = "True";
                    locNode[GVar.XmlTags.QuestTags.questaccepted].InnerText = "False";
                    xmlDoc.Save(Lists.quests[i].LocationFilePath);
                    Save.SaveGame(GVar.savedGameLocation, Lists.entity[0], Lists.quests);
                }

                if (Lists.quests[i].CompletingAction == action && Lists.quests[i].CompletingLocation == locationNode.SubName)
                {
                    Lists.quests[i].Completed = true;
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(Lists.quests[i].LocationFilePath);
                    XmlNode locNode = xmlDoc.DocumentElement.SelectSingleNode("/location/npc");
                    locNode[GVar.XmlTags.QuestTags.questfinished].InnerText = "True";
                    locNode[GVar.XmlTags.QuestTags.questaccepted].InnerText = "False";
                    xmlDoc.Save(Lists.quests[i].LocationFilePath);
                    Save.SaveGame(GVar.savedGameLocation, Lists.entity[0], Lists.quests);
                }
            }
            action = "";
        }

        public string Description { get { return description; } set { description = value; } }
        public string ShortDescription { get { return shortDescription; } set { shortDescription = value; } }
        public string CompletingAction { get { return completingAction; } set { completingAction = value; } }
        public string CompletingLocation { get { return completingLocation; } set { completingLocation = value; } }
        public bool Completed { get { return completed; } set { completed = value; } }
        public string LocationFilePath { get { return locationFilePath; } set { locationFilePath = value; } }
    }
}
