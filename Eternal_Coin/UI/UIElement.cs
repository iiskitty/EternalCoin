using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal_Coin
{
    public class UIElement
    {
        Texture2D spriteID;
        Vector2 position;
        Vector2 size;
        GVar.GameState gameState;
        bool draw;
        float layer;

        public UIElement(Texture2D spriteID, Vector2 position, Vector2 size, float layer, bool draw, GVar.GameState gameState)
        {
            this.spriteID = spriteID;
            this.position = position;
            this.size = size;
            this.layer = layer;
            this.draw = draw;
            this.gameState = gameState;
        }

        public static List<UIElement> AddUIElements(List<UIElement> uiElements)
        {
            uiElements.Add(new UIElement(Textures.UI.locationInfoUI, new Vector2(GVar.gameScreenX / 2 - Textures.UI.locationInfoUI.Width / 2, 0 + (GVar.gameScreenY - Textures.UI.locationInfoUI.Height)), new Vector2(Textures.UI.locationInfoUI.Width, Textures.UI.locationInfoUI.Height), 0.18f, true, GVar.GameState.game));
            uiElements.Add(new UIElement(Textures.UI.NPCInfoUI, new Vector2(0 + (GVar.gameScreenX / 2 - Textures.UI.NPCInfoUI.Width / 2), GVar.gameScreenY / 2 - Textures.UI.NPCInfoUI.Height / 2), new Vector2(Textures.UI.NPCInfoUI.Width, Textures.UI.NPCInfoUI.Height), 0.18f, false, GVar.GameState.game));
            uiElements.Add(new UIElement(Textures.UI.questListUI, new Vector2(0, 0), new Vector2(Textures.UI.questListUI.Width, Textures.UI.questListUI.Height), 0.18f, false, GVar.GameState.game));
            uiElements.Add(new UIElement(Textures.UI.questInfoUI, new Vector2(Textures.UI.questListUI.Width, 0), new Vector2(Textures.UI.questInfoUI.Width, Textures.UI.questInfoUI.Height), 0.18f, false, GVar.GameState.game));
            uiElements.Add(new UIElement(Textures.UI.inventoryUI, new Vector2(0, 0), new Vector2(Textures.UI.inventoryUI.Width, Textures.UI.inventoryUI.Height), 0.18f, true, GVar.GameState.inventory));
            uiElements.Add(new UIElement(Textures.UI.pauseUI, new Vector2(GVar.gameScreenX / 2 - Textures.UI.pauseUI.Width / 2, GVar.gameScreenY / 2 - Textures.UI.pauseUI.Height / 2), new Vector2(Textures.UI.pauseUI.Width, Textures.UI.pauseUI.Height), 0.18f, false, GVar.GameState.game));
            uiElements.Add(new UIElement(Textures.UI.shopInventoryUI, new Vector2(0, 0), new Vector2(Textures.UI.shopInventoryUI.Width, Textures.UI.shopInventoryUI.Height), 0.18f, true, GVar.GameState.shop));
            uiElements.Add(new UIElement(Textures.UI.battleUI, new Vector2(0, GVar.gameScreenY - Textures.UI.battleUI.Height), new Vector2(Textures.UI.battleUI.Width, Textures.UI.battleUI.Height), 0.18f, true, GVar.GameState.battle));
            uiElements.Add(new UIElement(Textures.UI.newGameUIBorder, new Vector2(GVar.gameScreenX / 2 - Textures.UI.newGameUIBorder.Width / 2, GVar.gameScreenY / 2 - Textures.UI.newGameUIBorder.Height / 2), new Vector2(Textures.UI.newGameUIBorder.Width, Textures.UI.newGameUIBorder.Height), 0.19f, false, GVar.GameState.chooseCharacter));
            uiElements.Add(new UIElement(Textures.UI.endBattleUI, new Vector2(GVar.gameScreenX / 2 - Textures.UI.endBattleUI.Width / 2, GVar.gameScreenY / 2 - Textures.UI.endBattleUI.Height / 2), new Vector2(Textures.UI.endBattleUI.Width, Textures.UI.endBattleUI.Height), 0.181f, false, GVar.GameState.battle));
            uiElements.Add(new UIElement(Textures.Misc.pixel, new Vector2(GVar.gameScreenX / 2 - 250, GVar.gameScreenY / 2 + 150), new Vector2(500, 300), 0.18f, false, GVar.GameState.game));
            return uiElements;
        }

        public Texture2D SpriteID
        {
            get { return spriteID; }
            set { spriteID = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        public GVar.GameState GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }

        public float Layer
        {
            get { return layer; }
            set { layer = value; }
        }

        public bool Draw
        {
            get { return draw; }
            set { draw = value; }
        }
    }
}
