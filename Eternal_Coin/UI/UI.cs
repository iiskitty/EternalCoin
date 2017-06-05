using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal_Coin
{
    public class UI
    {
        /// <summary>
        /// Activates Quests UI.
        /// </summary>
        public static void DisplayQuests()
        {
            //cycle through each UIElement.
            for (int i = 0; i < Lists.uiElements.Count; i++)
            {
                if (Lists.uiElements[i].SpriteID == Textures.UI.questListUI && !Lists.uiElements[i].Draw)//if Sprite is the Quests UI and is not active.
                {
                    //cycle through active quests.
                    for (int j = 0; j < Lists.quests.Count; j++)
                    {
                        Button viewQuestInfoButton = new Button(Textures.Misc.clearPixel, Vector2.Zero, new Vector2(268, 15), Color.White, "ViewQuestInfo", "Alive", 0f);//create a button for each active quest.
                        Lists.viewQuestInfoButtons.Add(viewQuestInfoButton);//add button to QuestInfoButtons.
                    }
                    Button closeQuestListUI = new Button(Textures.Misc.pixel, new Vector2(), new Vector2(20, 20), Color.Red, "CloseQuestListUI", "Alive", 0f);//create close Quests UI Button.
                    closeQuestListUI.PlayAnimation(GVar.AnimStates.Button.def);//set animation state of button to default.
                    Lists.mainWorldButtons.Add(closeQuestListUI);//add button to MainWorldButtons.
                    Lists.uiElements[i].Draw = true;//Activate Quests UI.
                }
            }
        }

        /// <summary>
        /// Deactivate Quests UI.
        /// </summary>
        public static void CloseQuestListUI()
        {
            //cycle through UIElements.
            for (int i = 0; i < Lists.uiElements.Count; i++)
            {
                if (Lists.uiElements[i].SpriteID == Textures.UI.questListUI)//if Sprite is the Quests UI.
                {
                    Lists.uiElements[i].Draw = false;//Deactivate Quests UI.
                }
                if (Lists.uiElements[i].SpriteID == Textures.UI.questInfoUI && Lists.uiElements[i].Draw)//if Sprite is Quests Info UI and is active.
                {
                    Lists.uiElements[i].Draw = false;//Deactivate Quests Info UI.
                    GVar.questInfo = "";//Reset Quest Info.
                    //cycle through MainWorldButtons.
                    for (int j = 0; j < Lists.mainWorldButtons.Count; j++)
                    {
                        if (Lists.mainWorldButtons[j].Name == "CloseQuestInfoUI")//if Button is Close Quest Info UI Button.
                        {
                            Lists.mainWorldButtons.RemoveAt(j);//Delete the button.
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Deactivate Quests Info UI.
        /// </summary>
        public static void CloseQuestInfoUI()
        {
            //cycle through UIElements.
            for (int i = 0; i < Lists.uiElements.Count; i++)
            {
                if (Lists.uiElements[i].SpriteID == Textures.UI.questInfoUI)//if Sprite is Quest Info UI.
                {
                    Lists.uiElements[i].Draw = false;//deactivate Quest Info UI.
                    GVar.questInfo = "";//Reset Quest Info.
                }
            }
        }

        /// <summary>
        /// Deactivate NPC UI.
        /// </summary>
        public static void CloseNPCUI()
        {
            GVar.npc = new NPC();//Reset Global NPC variable to nothing.

            //cycle through UIElements.
            for (int i = 0; i < Lists.uiElements.Count; i++)
            {
                if (Lists.uiElements[i].SpriteID == Textures.UI.NPCInfoUI && Lists.uiElements[i].Draw)//if Sprite is NPC Info UI and is active.
                {
                    //cycle through MainWorldButtons.
                    for (int j = 0; j < Lists.mainWorldButtons.Count; j++)
                    {
                        if (Lists.mainWorldButtons[j].Name == "HandInQuestButton")//if button is Hand In Quest Button.
                        {
                            Lists.mainWorldButtons[j].State = "delete";//delete the button.
                        }
                        else if (Lists.mainWorldButtons[j].Name == "CloseNPCUIButton")//if button is Close NPC Info UI.
                        {
                            Lists.mainWorldButtons[j].State = "delete";//delete the button.
                        }
                        else if (Lists.mainWorldButtons[j].Name == "QuestAcceptButton")//if button is Quest Accept Button.
                        {
                            Lists.mainWorldButtons[j].State = "delete";//delete the button.
                        }
                        else if (Lists.mainWorldButtons[j].Name == "OpenShop")//if button is Open Shop button.
                        {
                            Lists.mainWorldButtons[j].State = "delete";//delete the button.
                        }
                    }
                    Lists.uiElements[i].Draw = false;//Deactivate NPC Info UI.
                }
            }
        }

        /// <summary>
        /// Draw UIElement
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to Draw Sprites.</param>
        /// <param name="spriteID">Sprite of the UIElement.</param>
        /// <param name="position">Position of the UIElement.</param>
        /// <param name="size">Size of the UIElement.</param>
        /// <param name="layer">Layer to draw the UIElement.</param>
        public static void DrawUIElement(SpriteBatch spriteBatch, Texture2D spriteID, Vector2 position, Vector2 size, float layer)
        {
            spriteBatch.Draw(spriteID, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, layer);
        }
    }
}
