using Microsoft.Xna.Framework;
using System;
using System.Xml;

namespace Eternal_Coin
{
  public class LocationButtons
  {
    public static void SearchLocation()
    {
      if (GVar.player.CurrentLocation.State.Contains("Sub"))//if location is a sub location.
      {
        ReadXml.ReadLocationXmlFile(GVar.player.CurrentLocation.MainLocNode);//read current locations connecting main locations xml file.
        GVar.player.CurrentLocation.Searched = true;
        GVar.player.CurrentLocation.MainLocNode.Searched = true;

        XmlNode mainLocNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location");//grab location tag from current locations xml file.
        mainLocNode[GVar.XmlTags.LocationTags.searched].InnerText = GVar.player.CurrentLocation.Searched.ToString();//set the searched tag to true.
        SaveXml.SaveLocationXmlFile(GVar.player, GVar.player.CurrentLocation.MainLocNode);//save the xml file.
      }
      ReadXml.ReadCurrentLocation();//read current locations xml file.
      GVar.player.CurrentLocation.Searched = true;

      XmlNode locNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location");//grab location tag from current locations xml file.
      locNode[GVar.XmlTags.LocationTags.searched].InnerText = GVar.player.CurrentLocation.Searched.ToString();//set searched tag to true.

      locNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/actions");//grab action tag from current locations xml file.
      Quest.CheckAction(locNode[GVar.XmlTags.Actions.explore].InnerText, GVar.player.CurrentLocation);//check action against any active quests.

      SaveXml.SaveLocationXmlFile(GVar.player, GVar.player.CurrentLocation);//save current locations xml file.

      Lists.locationButtons.Clear();
      Button.CreateLocationButtons(GVar.player.CurrentLocation);
    }

    public static void OpenNPCUI()
    {
      for (int k = 0; k < Lists.mainWorldButtons.Count; k++)//cycle through MainWorldButtons.
        if (Lists.mainWorldButtons[k].Name == "OpenShop")//if button is Open Shop Button.
          Lists.mainWorldButtons[k].State = "delete";//delete Open Shop Button.

      XmlNode locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc");//grab npc tag from the current locations xml file.
      XmlNode locNPCGreeting = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/greeting");
      GVar.npc = new NPC(locNPC[GVar.XmlTags.NPCTags.name].InnerText, string.Empty, Convert.ToBoolean(locNPC[GVar.XmlTags.NPCTags.hasquest].InnerText), locNPC[GVar.XmlTags.NPCTags.currentquest].InnerText);//create new NPC with data from the current locations xml file.
      GVar.npc.Greeting = Text.WrapText(Fonts.lucidaConsole14Regular, locNPCGreeting[GVar.XmlTags.NPCTags.Greetings.normalgreeting].InnerText, 500);

      if (!UIElement.IsUIElementActive(Textures.UI.NPCInfoUI))
      {
        Button closeNPCUI = new Button(Textures.Button.closeButton, new Vector2(UIElement.GetUIElement(Textures.UI.NPCInfoUI).Position.X + UIElement.GetUIElement(Textures.UI.NPCInfoUI).Size.X - Textures.Button.closeButton.Width, UIElement.GetUIElement(Textures.UI.NPCInfoUI).Position.Y), new Vector2(35, 35), Color.White, "CloseNPCUIButton", "Alive", 0f);//create close NPC UI Button.
        closeNPCUI.PlayAnimation(GVar.AnimStates.Button.def);//set animation state of button to default.
        Button viewQuests = new Button(Textures.Button.questsButton, new Vector2(UIElement.GetUIElement(Textures.UI.NPCInfoUI).Position.X + UIElement.GetUIElement(Textures.UI.NPCInfoUI).Size.X - 50, UIElement.GetUIElement(Textures.UI.NPCInfoUI).Position.Y + UIElement.GetUIElement(Textures.UI.NPCInfoUI).Size.Y - 20), new Vector2(80, 30), Color.LightBlue, "ViewQuests", "Alive", 0f);
        viewQuests.PlayAnimation(GVar.AnimStates.Button.def);
        Lists.mainWorldButtons.Add(closeNPCUI);//add close NPC UI button to MainWorldButton.
        Lists.mainWorldButtons.Add(viewQuests);
        UIElement.GetUIElement(Textures.UI.NPCInfoUI).Draw = true;//activate NPC Info UI.
      }
    }

    public static void OpenShopUI()
    {
      XmlNode shopKeep = GVar.curLocNode.SelectSingleNode("/location/shop");//grab shop tag from current locations xml file.

      string greeting = Text.WrapText(Fonts.lucidaConsole14Regular, shopKeep["greeting"].InnerText, GVar.npcTextWrapLength);//wrap ShopKeeps greeting to fit in UI.

      GVar.npc = new NPC(shopKeep["name"].InnerText, greeting, false, "QUESTID");//create NPC(ShopKeep) with name and greeting.

      Button openShop = new Button(Textures.Misc.pixel, Vector2.Zero, new Vector2(25, 15), Color.Yellow, "OpenShop", "Alive", 0f);//create OpenShop Button.

      Lists.mainWorldButtons.Add(openShop);//add Button to MainWorldButtons.

      if (!UIElement.IsUIElementActive(Textures.UI.NPCInfoUI))
      {
        Button closeNPCUI = new Button(Textures.Button.closeButton, new Vector2(UIElement.GetUIElement(Textures.UI.NPCInfoUI).Position.X + UIElement.GetUIElement(Textures.UI.NPCInfoUI).Size.X - Textures.Button.closeButton.Width, UIElement.GetUIElement(Textures.UI.NPCInfoUI).Position.Y), new Vector2(35, 35), Color.White, "CloseNPCUIButton", "Alive", 0f);//create close NPC Info UI Button.
        closeNPCUI.PlayAnimation(GVar.AnimStates.Button.def);//set animation state for button to default.
        Lists.mainWorldButtons.Add(closeNPCUI);//add button to MainWorldButton.
        UIElement.ActivateUIElement(Textures.UI.NPCInfoUI);//activate NPC Info UI.
      }
    }

    public static void EnterLocation()
    {
      UI.CloseNPCUI();//deactivate NPC UI.

      Colours.EnableFadeOutMap();//activate map fade out.

      //cycle through connected location nodes of the sub location node of the current location.
      for (int slnc = 0; slnc < GVar.player.CurrentLocation.SubLocNode.LocNodeConnections.Count; slnc++)
      {
        ReadXml.ReadLocationXmlFile(GVar.player.CurrentLocation.SubLocNode.LocNodeConnections[slnc]);//read the xml file of the connected location node of the sub location node of the current location node.
        GVar.player.CurrentLocation.SubLocNode.LocNodeConnections[slnc].ColourA = 5;//set the alpha colour value of the connected location node of the sub location node of the current location ready for fade in.
      }
      ReadXml.ReadLocationXmlFile(GVar.player.CurrentLocation.SubLocNode);//read the xml file of the sub location node of the current location.
      XmlNode tempNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/actions");//grab the actions tag from the current locations xml file.
      Quest.CheckAction(tempNode[GVar.XmlTags.Actions.enter].InnerText, GVar.player.CurrentLocation);//check actions for current active quests.

      WorldMap.SelectNewMap(GVar.player.CurrentLocation.SubLocNode);//set new map to the sub location of the current location.
      GVar.player.CurrentLocation.SubLocNode.ColourA = 5;//set the alpha colour value of the sub location of the current location ready for fade in.

      GVar.LogDebugInfo("LocationChange: " + GVar.player.CurrentLocation.SubLocNode.Name, 2);

      GVar.npc = new NPC();//set NPC to nothing.
      Lists.locationButtons.Clear();//clear location buttons ready for next location.
      GVar.player.CurrentLocation = GVar.player.CurrentLocation.SubLocNode;//set the current location to the sub location of the current location.
      Button.CreateLocationButtons(GVar.player.CurrentLocation);//create location buttons for the new current location.
    }

    public static void ExitLocation()
    {
      UI.CloseNPCUI();//deactivate NPC UI.

      Colours.EnableFadeOutMap();//activate map fade out.

      //cycle through current locations main locations connected main location nodes.
      for (int slnc = 0; slnc < GVar.player.CurrentLocation.MainLocNode.LocNodeConnections.Count; slnc++)
      {
        ReadXml.ReadLocationXmlFile(GVar.player.CurrentLocation.MainLocNode.LocNodeConnections[slnc]);//read current locations connected main locations main location node xml file.
        GVar.player.CurrentLocation.MainLocNode.LocNodeConnections[slnc].ColourA = 5;//set the alpha colour value of the current locations main locations main location node ready for fade in.
      }

      GVar.player.CurrentLocation = GVar.player.CurrentLocation.MainLocNode;//set current location to current locations connected main location.

      ReadXml.ReadCurrentLocation();

      GVar.worldMap.SpriteID = Textures.Misc.worldMap;//set current map to world map.
      GVar.player.CurrentLocation.ColourA = 5;//set the alpha colour value of the current locations connected main location ready for fade in.

      GVar.LogDebugInfo("LocationChange: " + GVar.player.CurrentLocation.Name, 2);

      GVar.npc = new NPC();//set NPC to nothing.
      Lists.locationButtons.Clear();//clear location buttons ready for next location.
      Button.CreateLocationButtons(GVar.player.CurrentLocation);
      
    }
  }
}
