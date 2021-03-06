﻿using Microsoft.Xna.Framework;
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

    bool unlocked;
    string unlockRequirement;
    bool accepted;
    bool completed;
    bool handedIn;

    public Quest(string questid, string description, string shortDescription, string completingAction, string completingLocation, bool unlocked, string unlockRequirement, bool accepted, bool completed, bool handedIn, string locationFilePath)
    {
      this.questid = questid;
      this.description = description;
      this.shortDescription = shortDescription;
      this.completingAction = completingAction;
      this.completingLocation = completingLocation;
      this.unlocked = unlocked;
      this.unlockRequirement = unlockRequirement;
      this.locationFilePath = locationFilePath;
      this.accepted = accepted;
      this.completed = completed;
      this.handedIn = handedIn;
    }

    public string QuestID { get { return questid; } set { questid = value; } }
    public string Description { get { return description; } set { description = value; } }
    public string ShortDescription { get { return shortDescription; } set { shortDescription = value; } }
    public string CompletingAction { get { return completingAction; } set { completingAction = value; } }
    public string CompletingLocation { get { return completingLocation; } set { completingLocation = value; } }
    public bool Unlocked { get { return unlocked; } set { unlocked = value; } }
    public string UnlockRequirement { get { return unlockRequirement; } set { unlockRequirement = value; } }
    public bool Accepted { get { return accepted; } set { accepted = value; } }
    public bool Completed { get { return completed; } set { completed = value; } }
    public bool HandedIn { get { return handedIn; } set { handedIn = value; } }
    public string LocationFilePath { get { return locationFilePath; } set { locationFilePath = value; } }

    public static string GetLocationFilePath(Entity P)
    {
      string path = GVar.gameFilesLocation + P.Name + "/" + P.CurrentLocation.LocatoinFilePath;
      return path;
    }

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
          Lists.quests.Add(new Quest(quests[i][GVar.XmlTags.QuestTags.questid].InnerText, quests[i][GVar.XmlTags.QuestTags.description].InnerText, quests[i][GVar.XmlTags.QuestTags.shortdescription].InnerText, quests[i][GVar.XmlTags.QuestTags.completingaction].InnerText, quests[i][GVar.XmlTags.QuestTags.completinglocation].InnerText, Convert.ToBoolean(quests[i][GVar.XmlTags.QuestTags.unlocked].InnerText), quests[i][GVar.XmlTags.QuestTags.unlockrequirement].InnerText, Convert.ToBoolean(quests[i][GVar.XmlTags.QuestTags.accepted].InnerText), Convert.ToBoolean(quests[i][GVar.XmlTags.QuestTags.completed].InnerText), Convert.ToBoolean(quests[i][GVar.XmlTags.QuestTags.handedin].InnerText), GetLocationFilePath(P)));
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

    public static void HandInQuest(Entity P, string questid)
    {
      XmlNode locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc");
      XmlNode locNPCGreeting = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/greeting/questid/" + questid);
      XmlNodeList quests = GVar.curLocNode.SelectNodes("/location/npc/quest");
      XmlNodeList rewards = GVar.curLocNode.DocumentElement.SelectNodes("/location/npc/questrewards/" + questid + "/item");

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

    public static void CreateNPCQuestsButtons()
    {
      Lists.NPCQuestButtons.Clear();
      Lists.NPCQuests.Clear();


      XmlNodeList quests = GVar.curLocNode.DocumentElement.SelectNodes("/location/npc/quest");

      for (int j = 0; j < quests.Count; j++)
      {
        if (!Convert.ToBoolean(quests[j][GVar.XmlTags.QuestTags.unlocked].InnerText))
        {
          for (int k = 0; k < Lists.completedQuests.Count; k++)
          {
            if (quests[j][GVar.XmlTags.QuestTags.unlockrequirement].InnerText == Lists.completedQuests[k])
            {
              quests[j][GVar.XmlTags.QuestTags.unlocked].InnerText = "true";
            }
          }
        }
        if (quests[j][GVar.XmlTags.QuestTags.completingaction].InnerText.Contains("Exploring"))
        {
          Lists.NPCQuestButtons.Add(new GeneratedButton(Vector2.Zero, Color.White, 20, "NPCQuestButton", "Explore", quests[j][GVar.XmlTags.QuestTags.questid].InnerText));
        }
        else if (quests[j][GVar.XmlTags.QuestTags.completingaction].InnerText.Contains("Talking"))
        {
          Lists.NPCQuestButtons.Add(new GeneratedButton(Vector2.Zero, Color.White, 20, "NPCQuestButton", "Talk", quests[j][GVar.XmlTags.QuestTags.questid].InnerText));
        }
        Lists.NPCQuests.Add(new Quest(quests[j][GVar.XmlTags.QuestTags.questid].InnerText, quests[j][GVar.XmlTags.QuestTags.description].InnerText, quests[j][GVar.XmlTags.QuestTags.shortdescription].InnerText, quests[j][GVar.XmlTags.QuestTags.completingaction].InnerText, quests[j][GVar.XmlTags.QuestTags.completinglocation].InnerText, Convert.ToBoolean(quests[j][GVar.XmlTags.QuestTags.unlocked].InnerText), quests[j][GVar.XmlTags.QuestTags.unlockrequirement].InnerText, Convert.ToBoolean(quests[j][GVar.XmlTags.QuestTags.accepted].InnerText), Convert.ToBoolean(quests[j][GVar.XmlTags.QuestTags.completed].InnerText), Convert.ToBoolean(quests[j][GVar.XmlTags.QuestTags.handedin].InnerText), Quest.GetLocationFilePath(GVar.player)));
      }
    }

    public static void AddAcceptOrHandInQuestButtons(GeneratedButton button)
    {
      for (int i = 0; i < Lists.NPCQuests.Count; i++)
      {
        if (Lists.NPCQuests[i].QuestID == button.State)
        {
          Quest Q = Lists.NPCQuests[i];
          if (!Q.Unlocked && !Q.Accepted)
          {
            NPC.SetGreeting(button.State, GVar.XmlTags.NPCTags.Greetings.questunaccepted);
            SoundManager.PlayNPCDialogue(GVar.player.CurrentLocation.SubName, GVar.npc.Name, button.State, GVar.XmlTags.NPCTags.Greetings.questunaccepted);
            break;
          }
          else if (Q.Unlocked && !Q.Accepted)
          {
            GeneratedButton acceptButton = new GeneratedButton(new Vector2(9999, 9999), Color.White, 20, "QuestAcceptButton", "Accept", button.State);
            Lists.NPCQuestButtons.Add(acceptButton);
            NPC.SetGreeting(button.State, GVar.XmlTags.NPCTags.Greetings.questunaccepted);
            SoundManager.PlayNPCDialogue(GVar.player.CurrentLocation.SubName, GVar.npc.Name, button.State, GVar.XmlTags.NPCTags.Greetings.questunaccepted);
          }
          else if (Q.Unlocked && Q.Accepted && !Q.Completed)
          {
            NPC.SetGreeting(button.State, GVar.XmlTags.NPCTags.Greetings.questaccepted);
            SoundManager.PlayNPCDialogue(GVar.player.CurrentLocation.SubName, GVar.npc.Name, button.State, GVar.XmlTags.NPCTags.Greetings.questaccepted);
            break;
          }
          else if (Q.Unlocked && Q.Accepted && Q.Completed && !Q.HandedIn)
          {
            GeneratedButton handInButton = new GeneratedButton(new Vector2(9999, 9999), Color.White, 20, "HandInQuestButton", "Hand In", button.State);
            Lists.NPCQuestButtons.Add(handInButton);
            NPC.SetGreeting(button.State, GVar.XmlTags.NPCTags.Greetings.questfinished);
            SoundManager.PlayNPCDialogue(GVar.player.CurrentLocation.SubName, GVar.npc.Name, button.State, GVar.XmlTags.NPCTags.Greetings.questfinished);
          }
          else if (Q.Unlocked && Q.Accepted && Q.Completed && Q.HandedIn)
          {
            NPC.SetGreeting(button.State, GVar.XmlTags.NPCTags.Greetings.questfinished);
            SoundManager.PlayNPCDialogue(GVar.player.CurrentLocation.SubName, GVar.npc.Name, button.State, GVar.XmlTags.NPCTags.Greetings.questfinished);
            break;
          }
        }
      }
    }
  }
}
