using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using Microsoft.Xna.Framework.Audio;
using System.Xml;

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
            uiElements.Add(new UIElement(Textures.locationInfoUITex, new Vector2(GVar.gameScreenX / 2 - Textures.locationInfoUITex.Width / 2, 0 + (GVar.gameScreenY - Textures.locationInfoUITex.Height)), new Vector2(Textures.locationInfoUITex.Width, Textures.locationInfoUITex.Height), 0.18f, true, GVar.GameState.game));
            uiElements.Add(new UIElement(Textures.NPCInfoUITex, new Vector2(0 + (GVar.gameScreenX - Textures.NPCInfoUITex.Width), 0), new Vector2(Textures.NPCInfoUITex.Width, Textures.NPCInfoUITex.Height), 0.18f, false, GVar.GameState.game));
            uiElements.Add(new UIElement(Textures.questListUI, new Vector2(0, 0), new Vector2(Textures.questListUI.Width, Textures.questListUI.Height), 0.18f, false, GVar.GameState.game));
            uiElements.Add(new UIElement(Textures.questInfoUI, new Vector2(Textures.questListUI.Width, 0), new Vector2(Textures.questInfoUI.Width, Textures.questInfoUI.Height), 0.18f, false, GVar.GameState.game));
            uiElements.Add(new UIElement(Textures.inventoryUI, new Vector2(0, 0), new Vector2(Textures.inventoryUI.Width, Textures.inventoryUI.Height), 0.18f, true, GVar.GameState.inventory));
            uiElements.Add(new UIElement(Textures.pauseUI, new Vector2(GVar.gameScreenX / 2 - Textures.pauseUI.Width / 2, GVar.gameScreenY / 2 - Textures.pauseUI.Height / 2), new Vector2(Textures.pauseUI.Width, Textures.pauseUI.Height), 0.18f, false, GVar.GameState.game));
            uiElements.Add(new UIElement(Textures.shopInventoryUI, new Vector2(0, 0), new Vector2(Textures.shopInventoryUI.Width, Textures.shopInventoryUI.Height), 0.18f, true, GVar.GameState.shop));
            uiElements.Add(new UIElement(Textures.battleUI, new Vector2(0, GVar.gameScreenY - Textures.battleUI.Height), new Vector2(Textures.battleUI.Width, Textures.battleUI.Height), 0.18f, true, GVar.GameState.battle));
            uiElements.Add(new UIElement(Textures.newGameUIBorder, new Vector2(GVar.gameScreenX / 2 - Textures.newGameUIBorder.Width / 2, GVar.gameScreenY / 2 - Textures.newGameUIBorder.Height / 2), new Vector2(Textures.newGameUIBorder.Width, Textures.newGameUIBorder.Height), 0.19f, false, GVar.GameState.chooseCharacter));
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
