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
    public class UI
    {
        public static void DisplayQuests()
        {
            foreach (UIElement ui in Lists.uiElements)
            {
                if (ui.SpriteID == Textures.UI.questListUI && !ui.Draw)
                {
                    foreach (Quest q in Lists.quests)
                    {
                        Button viewQuestInfoButton = new Button(Textures.Misc.clearPixel, Vector2.Zero, new Vector2(268, 15), Color.White, "ViewQuestInfo", "Alive", 0f);
                        Lists.viewQuestInfoButtons.Add(viewQuestInfoButton);
                    }
                    Button closeQuestListUI = new Button(Textures.Misc.pixel, new Vector2(), new Vector2(20, 20), Color.Red, "CloseQuestListUI", "Alive", 0f);
                    closeQuestListUI.PlayAnimation(GVar.AnimStates.Button.def);
                    Lists.mainWorldButtons.Add(closeQuestListUI);
                    ui.Draw = true;
                }
            }
        }

        public static void CloseQuestListUI()
        {
            foreach (UIElement ui in Lists.uiElements)
            {

                if (ui.SpriteID == Textures.UI.questListUI)
                {
                    ui.Draw = false;
                }
                if (ui.SpriteID == Textures.UI.questInfoUI && ui.Draw)
                {
                    ui.Draw = false;
                    GVar.questInfo = "";
                    for (int j = 0; j < Lists.mainWorldButtons.Count; j++)
                    {
                        if (Lists.mainWorldButtons[j].Name == "CloseQuestInfoUI")
                        {
                            Lists.mainWorldButtons.RemoveAt(j);
                        }
                    }
                }
            }
        }

        public static void CloseQuestInfoUI()
        {
            foreach (UIElement ui in Lists.uiElements)
            {
                if (ui.SpriteID == Textures.UI.questInfoUI)
                {
                    ui.Draw = false;
                    GVar.questInfo = "";
                }
            }
        }

        public static void CloseNPCUI()
        {
            GVar.npc = new NPC();

            foreach (UIElement ui in Lists.uiElements)
            {
                if (ui.SpriteID == Textures.UI.NPCInfoUITex && ui.Draw)
                {
                    for (int b = 0; b < Lists.mainWorldButtons.Count; b++)
                    {
                        if (Lists.mainWorldButtons[b].Name == "HandInQuestButton")
                        {
                            Lists.mainWorldButtons[b].State = "delete";
                        }
                        else if (Lists.mainWorldButtons[b].Name == "CloseNPCUIButton")
                        {
                            Lists.mainWorldButtons[b].State = "delete";
                        }
                        else if (Lists.mainWorldButtons[b].Name == "QuestAcceptButton")
                        {
                            Lists.mainWorldButtons[b].State = "delete";
                        }
                        else if (Lists.mainWorldButtons[b].Name == "OpenShop")
                        {
                            Lists.mainWorldButtons[b].State = "delete";
                        }
                    }
                    ui.Draw = false;
                }
            }
        }

        public static void DrawUIElement(SpriteBatch spriteBatch, Texture2D spriteID, Vector2 position, Vector2 size, float layer)
        {
            spriteBatch.Draw(spriteID, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, layer);
        }
    }
}
