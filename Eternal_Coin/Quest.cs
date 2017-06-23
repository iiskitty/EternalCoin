using Microsoft.Xna.Framework;
using System;
using System.Xml;

namespace Eternal_Coin
{
    public class Quest
    {
        string description;
        string shortDescription;
        string completingAction;
        string completingLocation;
        string locationFilePath;
        string questid;

        bool completed;

        public Quest(string questid, string description, string shortDescription, string completingAction, string completingLocation, bool completed, string locationFilePath)
        {
            this.questid = questid;
            this.description = description;
            this.shortDescription = shortDescription;
            this.completingAction = completingAction;
            this.completingLocation = completingLocation;
            this.completed = completed;
            this.locationFilePath = locationFilePath;
        }

        //public static void AcceptQuest(Entity P)
        //{
        //    XmlNode locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc");
        //    locNPC[GVar.XmlTags.NPCTags.hasquest].InnerText = "false";
        //    locNPC[GVar.XmlTags.QuestTags.questaccepted].InnerText = "True";
        //    locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/quest");
        //    LocationNode temp = P.CurrentLocation;
        //    Lists.quests.Add(new Quest(locNPC[GVar.XmlTags.QuestTags.description].InnerText, locNPC[GVar.XmlTags.QuestTags.shortdescription].InnerText, locNPC[GVar.XmlTags.QuestTags.completingaction].InnerText, locNPC[GVar.XmlTags.QuestTags.completinglocation].InnerText, false, GVar.gameFilesLocation + P.Name + "/" + temp.LocatoinFilePath));
        //    foreach (UIElement ui in Lists.uiElements)
        //    {
        //        if (ui.SpriteID == Textures.UI.questListUI && ui.Draw)
        //        {
        //            Lists.viewQuestInfoButtons.Add(new Button(Textures.Misc.clearPixel, new Vector2(), new Vector2(268, 15), Color.White, "ViewQuestInfo", "Alive", 0f));
        //        }
        //    }
        //    locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/greeting");
        //    GVar.npc.Greeting = locNPC[GVar.XmlTags.NPCTags.Greetings.acceptquest].InnerText;
        //    GVar.npc.Greeting = Text.WrapText(Fonts.lucidaConsole14Regular, GVar.npc.Greeting, 500);
        //    SaveXml.SaveLocationXmlFile(P, temp);
        //    Save.SaveGame(GVar.savedGameLocation, P, Lists.quests);
        //}

        public static void AcceptQuest(Entity P, string questid)
        {
            XmlNode locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc");
            XmlNode locNPCGreeting = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/greeting/questid/" + questid);
            XmlNodeList quests = GVar.curLocNode.DocumentElement.SelectNodes("/location/npc/quest");

            locNPC[GVar.XmlTags.NPCTags.currentquest].InnerText = questid;

            for (int i = 0; i < quests.Count; i++)
            {
                if (quests[i][GVar.XmlTags.QuestTags.questid].InnerText == questid)
                {
                    quests[i][GVar.XmlTags.QuestTags.accepted].InnerText = "true";
                    Lists.quests.Add(new Quest(quests[i][GVar.XmlTags.QuestTags.questid].InnerText, quests[i][GVar.XmlTags.QuestTags.description].InnerText, quests[i][GVar.XmlTags.QuestTags.shortdescription].InnerText, quests[i][GVar.XmlTags.QuestTags.completingaction].InnerText, quests[i][GVar.XmlTags.QuestTags.completinglocation].InnerText, Convert.ToBoolean(quests[i][GVar.XmlTags.QuestTags.completed].InnerText), GVar.gameFilesLocation + P.Name + "/" + P.CurrentLocation.LocatoinFilePath));
                }
            }

            for (int i = 0; i < Lists.uiElements.Count; i++)
            {
                if (Lists.uiElements[i].SpriteID == Textures.UI.questListUI && Lists.uiElements[i].Draw)
                {
                    Lists.viewQuestInfoButtons.Add(new Button(Textures.Misc.clearPixel, new Vector2(), new Vector2(268, 15), Color.White, "ViewQuestInfoButton", "Alive", 0f));
                }
            }

            GVar.npc.Greeting = locNPCGreeting[GVar.XmlTags.NPCTags.Greetings.acceptquest].InnerText;
            GVar.npc.Greeting = Text.WrapText(Fonts.lucidaConsole14Regular, GVar.npc.Greeting, 500);
            SaveXml.SaveLocationXmlFile(P, P.CurrentLocation);
            Save.SaveGame(GVar.savedGameLocation, P, Lists.quests);
        }

        //public static void HandInQuest(Entity P)
        //{
        //    XmlNode locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc");
        //    LocationNode temp = P.CurrentLocation;
        //    locNPC[GVar.XmlTags.QuestTags.questfinished].InnerText = "False";
        //    locNPC[GVar.XmlTags.QuestTags.questcompleted].InnerText = "True";
        //    locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/greeting");
        //    GVar.npc.Greeting = locNPC[GVar.XmlTags.NPCTags.Greetings.handinquest].InnerText;
        //    GVar.npc.Greeting = Text.WrapText(Fonts.lucidaConsole14Regular, GVar.npc.Greeting, 500);
        //    locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/quest");
        //    for (int k = 0; k < Lists.quests.Count; k++)
        //    {
        //        if (Lists.quests[k].CompletingAction == locNPC[GVar.XmlTags.QuestTags.completingaction].InnerText)
        //        {
        //            Lists.quests.RemoveAt(k);
        //            if (Lists.viewQuestInfoButtons.Count > 0)
        //                Lists.viewQuestInfoButtons.RemoveAt(k);
        //        }
        //    }
        //    XmlNodeList questRewardItems = GVar.curLocNode.SelectNodes("/location/npc/quest/reward/item");
        //    foreach (XmlNode QRI in questRewardItems)
        //    {

        //        for (int i = 0; i < 40; i++)
        //        {
        //            if (InventoryManager.playerInventory.ItemSlots[i].item == null)
        //            {
        //                InventoryManager.playerInventory.ItemSlots[i].item = ItemBuilder.BuildItem(Dictionaries.items[QRI[GVar.XmlTags.ItemTags.itemname].InnerText]);
        //                Lists.playerItems.Add(InventoryManager.playerInventory.ItemSlots[i].item);
        //                break;
        //            }
        //        }
        //    }
        //    SaveXml.SaveLocationXmlFile(P, temp);
        //    Save.SaveGame(GVar.savedGameLocation, P, Lists.quests);
        //}

        public static void HandInQuest(Entity P, string questid)
        {
            XmlNode locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc");
            XmlNode locNPCGreeting = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/greeting/questid/" + questid);
            XmlNodeList quests = GVar.curLocNode.SelectNodes("/location/npc/quest");
            XmlNodeList rewards = null;

            GVar.npc.Greeting = locNPCGreeting[GVar.XmlTags.NPCTags.Greetings.handinquest].InnerText;
            GVar.npc.Greeting = Text.WrapText(Fonts.lucidaConsole14Regular, GVar.npc.Greeting, 500);
            
            for (int i = 0; i < Lists.quests.Count; i++)
            {
                if (Lists.quests[i].QuestID == questid)
                {
                    Lists.completedQuests.Add(questid);
                    Lists.quests.RemoveAt(i);
                    if (Lists.viewQuestInfoButtons.Count > 0)
                        Lists.viewQuestInfoButtons.RemoveAt(i);
                }
            }
            for (int i = 0; i < quests.Count; i++)
            {
                if (quests[i][GVar.XmlTags.QuestTags.questid].InnerText == questid)
                {
                    rewards = quests[i].SelectNodes("reward/item"); ///////////////////////////////////this is not working... fix it fix it fix it fix it fix it fix it fix it fix it fix it
                    quests[i][GVar.XmlTags.QuestTags.handedin].InnerText = "true";
                    locNPC[GVar.XmlTags.NPCTags.currentquest].InnerText = "QUESTID";
                }
            }
            for (int i = 0; i < rewards.Count; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    if (InventoryManager.playerInventory.ItemSlots[j].item == null)
                    {
                        InventoryManager.playerInventory.ItemSlots[j].item = ItemBuilder.BuildItem(Dictionaries.items[rewards[i][GVar.XmlTags.ItemTags.itemname].InnerText]);
                        Lists.playerItems.Add(InventoryManager.playerInventory.ItemSlots[j].item);
                        break;
                    }
                }
            }

            SaveXml.SaveLocationXmlFile(P, P.CurrentLocation);
            Save.SaveGame(GVar.savedGameLocation, P, Lists.quests);
        }

        //public static void CheckAction(string action, Node locationNode)
        //{
        //    for (int i = 0; i < Lists.quests.Count; i++)
        //    {
        //        if (action.Contains("Entering") && Lists.quests[i].CompletingAction.Contains("Exploring") && Lists.quests[i].CompletingLocation == locationNode.SubName && locationNode.Searched)
        //        {
        //            Lists.quests[i].Completed = true;
        //            XmlDocument xmlDoc = new XmlDocument();
        //            xmlDoc.Load(Lists.quests[i].LocationFilePath);
        //            XmlNode locNode = xmlDoc.DocumentElement.SelectSingleNode("/location/npc");
        //            locNode[GVar.XmlTags.QuestTags.questfinished].InnerText = "True";
        //            locNode[GVar.XmlTags.QuestTags.questaccepted].InnerText = "False";
        //            xmlDoc.Save(Lists.quests[i].LocationFilePath);
        //            Save.SaveGame(GVar.savedGameLocation, GVar.player, Lists.quests);
        //        }

        //        if (Lists.quests[i].CompletingAction == action && Lists.quests[i].CompletingLocation == locationNode.SubName)
        //        {
        //            Lists.quests[i].Completed = true;
        //            XmlDocument xmlDoc = new XmlDocument();
        //            xmlDoc.Load(Lists.quests[i].LocationFilePath);
        //            XmlNode locNode = xmlDoc.DocumentElement.SelectSingleNode("/location/npc");
        //            locNode[GVar.XmlTags.QuestTags.questfinished].InnerText = "True";
        //            locNode[GVar.XmlTags.QuestTags.questaccepted].InnerText = "False";
        //            xmlDoc.Save(Lists.quests[i].LocationFilePath);
        //            Save.SaveGame(GVar.savedGameLocation, GVar.player, Lists.quests);
        //        }
        //    }
        //    action = "";
        //}

        public static void CheckAction(string action, Node locationNode)
        {
            for (int i = 0; i < Lists.quests.Count; i++)
            {
                if (action.Contains("Entering") && Lists.quests[i].CompletingAction.Contains("Exploring") && Lists.quests[i].CompletingLocation == locationNode.SubName && locationNode.Searched)
                {
                    CompleteQuest(i);
                }

                if (Lists.quests[i].CompletingAction == action && Lists.quests[i].CompletingLocation == locationNode.SubName)
                {
                    CompleteQuest(i);
                }
            }
            action = "";
        }

        private static void CompleteQuest(int i)
        {
            Lists.quests[i].Completed = true;
            XmlDocument locDoc = new XmlDocument();
            locDoc.Load(Lists.quests[i].locationFilePath);
            XmlNode locNPC = locDoc.DocumentElement.SelectSingleNode("/location/npc");
            XmlNodeList quests = locDoc.DocumentElement.SelectNodes("/location/npc/quest");

            for (int j = 0; j < quests.Count; j++)
            {
                if (quests[j][GVar.XmlTags.QuestTags.questid].InnerText == Lists.quests[i].QuestID)
                {
                    quests[j][GVar.XmlTags.QuestTags.completed].InnerText = "true";
                    locDoc.Save(Lists.quests[i].LocationFilePath);
                    Save.SaveGame(GVar.savedGameLocation, GVar.player, Lists.quests);
                }
            }
        }

        public string QuestID { get { return questid; } set { questid = value; } }
        public string Description { get { return description; } set { description = value; } }
        public string ShortDescription { get { return shortDescription; } set { shortDescription = value; } }
        public string CompletingAction { get { return completingAction; } set { completingAction = value; } }
        public string CompletingLocation { get { return completingLocation; } set { completingLocation = value; } }
        public bool Completed { get { return completed; } set { completed = value; } }
        public string LocationFilePath { get { return locationFilePath; } set { locationFilePath = value; } }
    }
}
