using Microsoft.Xna.Framework;
using System;

namespace Eternal_Coin
{
  public class MainWorldButtons
  {
    public static void OpenShop(Button button)
    {
      Shop.LoadShopInventory(GVar.curLocNode);//load shop inventory of current location.
      Lists.inventoryButtons.Add(Button.CreateButton(Textures.Button.closeButton, UIElement.GetUIElement(Textures.UI.inventoryUI), new Vector2(35, 35), new Vector2(-5, 5), "CloseInventory", "Alive", Button.ButtonPosition.topright));
      Lists.mainWorldButtons.Remove(button);//remove Open Shop Button.
      GVar.currentGameState = GVar.GameState.shop;//set current GameState to shop.
      GVar.previousGameState = GVar.GameState.game;//set previous GameState to game.
      UI.CloseNPCUI();//close NPC UI.
    }

    public static void ViewQuests()
    {
      if (!UIElement.IsUIElementActive(Textures.UI.NPCQuestListUI))
        UI.DisplayNPCQuestList();
      else if (UIElement.IsUIElementActive(Textures.UI.NPCQuestListUI))
        UI.CloseNPCQuestListUI();
    }

    public static void CreateMainWorldButtons()
    {
      Lists.mainWorldButtons.Add(Button.CreateButton(Textures.Misc.pixel, UIElement.GetUIElement(Textures.UI.locationInfoUI), new Vector2(50, 50), new Vector2(-5, 40), "DisplayQuests", "Alive", Button.ButtonPosition.topright));
      Lists.mainWorldButtons.Add(Button.CreateButton(Textures.Button.inventoryButton, UIElement.GetUIElement(Textures.UI.locationInfoUI), new Vector2(50, 50), new Vector2(-60, 40), "DisplayInventory", "Alive", Button.ButtonPosition.topright));
    }

    public static void SetMainWorldButtonPositions()
    {
      for (int i = 0; i < Lists.mainWorldButtons.Count; i++)
      {
        if (Lists.mainWorldButtons[i].Name == "DisplayQuests")
          Lists.mainWorldButtons[i].Position = Button.SetButtonPosition(UIElement.GetUIElement(Textures.UI.locationInfoUI), Lists.mainWorldButtons[i].Size, new Vector2(-5, 40), Button.ButtonPosition.topright);
        else if (Lists.mainWorldButtons[i].Name == "DisplayInventory")
          Lists.mainWorldButtons[i].Position = Button.SetButtonPosition(UIElement.GetUIElement(Textures.UI.locationInfoUI), Lists.mainWorldButtons[i].Size, new Vector2(-60, 40), Button.ButtonPosition.topright);
        else if (Lists.mainWorldButtons[i].Name == "MainMenu")
          Lists.mainWorldButtons[i].Position = Button.SetButtonPosition(UIElement.GetUIElement(Textures.UI.pauseUI), Lists.mainWorldButtons[i].Size, new Vector2(0, 30), Button.ButtonPosition.topcenter);
        else if (Lists.mainWorldButtons[i].Name == "Options")
          Lists.mainWorldButtons[i].Position = Button.SetButtonPosition(UIElement.GetUIElement(Textures.UI.pauseUI), Lists.mainWorldButtons[i].Size, new Vector2(0, -30), Button.ButtonPosition.bottomcenter);
      }
    }

    public static void DisplayInventory()
    {
      Lists.inventoryButtons.Add(Button.CreateButton(Textures.Button.backArrow, UIElement.GetUIElement(Textures.UI.inventoryUI), new Vector2(32, 28), new Vector2(-10, 6), "CloseInventory", "Alive", Button.ButtonPosition.topright));
      GVar.currentGameState = GVar.GameState.inventory;//set current GameState to inventory.
      GVar.previousGameState = GVar.GameState.game;//set previous GameState to game.
    }

    public static void CloseQuestListUI(Button button)
    {
      UI.CloseQuestListUI();//deactiate Quests UI.
      Lists.mainWorldButtons.Remove(button);//remove Close Quests UI Button.
      Lists.viewQuestInfoButtons.Clear();//delete Quest Info Buttons.
    }

    public static void CloseQuestInfoUI(Button button)
    {
      UI.CloseQuestInfoUI();//deactivate Quest Info UI.
      Lists.mainWorldButtons.Remove(button);//remove Close Quest Info UI Button.
    }

    public static void CloseNPCUI(Button button)
    {
      Lists.mainWorldButtons.Remove(button);//delete Close NPC UI Button.
      for (int k = 0; k < Lists.mainWorldButtons.Count; k++)//cycle through MainWorldButtons.
        if (Lists.mainWorldButtons[k].Name == "OpenShop" || Lists.mainWorldButtons[k].Name == "ViewQuests")//if Button is Open Shop Button.
          Lists.mainWorldButtons.RemoveAt(k);//delete Open Shop Button.

      Lists.NPCQuests.Clear();
      UI.CloseNPCUI();//deactivate NPC UI.
      UI.CloseNPCQuestListUI();
    }

    public static void QuitGame()
    {
      Save.SaveGame(GVar.savedGameLocation, GVar.player, Lists.quests);//Save the game.
      GVar.changeToMainMenu = true;//change to menu bool to true.
      Colours.drawBlackFade = true;//draw black fade in bool to true.
      Colours.fadeIn = true;//fade in bool to true.
      GVar.playerName = string.Empty;//reset players name.
    }

    public static void DeleteButton(string name)
    {
      for (int i = 0; i < Lists.mainWorldButtons.Count; i++)
        if (Lists.mainWorldButtons[i].Name == name)
          Lists.mainWorldButtons.RemoveAt(i);
    }
  }
}
