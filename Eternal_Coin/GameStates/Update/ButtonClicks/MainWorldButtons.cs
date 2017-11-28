using Microsoft.Xna.Framework;
using System;

namespace Eternal_Coin
{
  public class MainWorldButtons
  {
    public static void OpenShop(Button button)
    {
      Shop.LoadShopInventory(GVar.curLocNode);//load shop inventory of current location.
      Button closeInv = new Button(Textures.Misc.pixel, new Vector2(GVar.gameScreenX - button.Size.X, 0), new Vector2(25, 25), Color.Red, "CloseInventory", "Alive", 0f);//create button to close inventory.
      Lists.inventoryButtons.Add(closeInv);//add close button to InventoryButtons.
      Lists.mainWorldButtons.Remove(button);//remove Open Shop Button.
      GVar.currentGameState = GVar.GameState.shop;//set current GameState to shop.
      GVar.previousGameState = GVar.GameState.game;//set previous GameState to game.
      UI.CloseNPCUI();//close NPC UI.
    }

    public static void ViewQuests()
    {
      for (int i = 0; i < Lists.uiElements.Count; i++)
      {
        if (Lists.uiElements[i].SpriteID == Textures.UI.NPCQuestListUI && !Lists.uiElements[i].Draw)
          UI.DisplayNPCQuestList();
        else if (Lists.uiElements[i].SpriteID == Textures.UI.NPCQuestListUI && Lists.uiElements[i].Draw)
          UI.CloseNPCQuestListUI();
      }
    }

    public static void DisplayInventory()
    {
      Button closeInv = new Button(Textures.Misc.pixel, new Vector2(GVar.gameScreenX - 25, 0), new Vector2(25, 25), Color.Red, "CloseInventory", "Alive", 0f);//create button to close inventory.
      Lists.inventoryButtons.Add(closeInv);//add close button to InventoryButtons.
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
  }
}
