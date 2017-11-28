using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;

namespace Eternal_Coin
{
    public class NPC
    {
        string name;
        string greeting;

        string questID;
        string currentQuest;

        bool hasQuest;

        public NPC(string name, string greeting, bool hasQuest, string questID)
        {
            this.name = name;
            this.greeting = greeting;
            this.hasQuest = hasQuest;
            this.questID = questID;
        }

        public NPC()
        {
            greeting = string.Empty;
        }

        /// <summary>
        /// Draws the name and current greeting of the NPC being talked to.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw strings.</param>
        /// <param name="name">Name of NPC</param>
        /// <param name="greeting">Greeting of NPC</param>
        public static void DrawNPCInfo(SpriteBatch spriteBatch, string name, string greeting)
        {
            foreach (UIElement ui in Lists.uiElements)
            {
                if (ui.SpriteID == Textures.UI.NPCInfoUI && ui.Draw)
                {
                    if (GVar.player.CurrentLocation != null && GVar.player.CurrentLocation.Searched)
                    {
                        if (name != null)
                            spriteBatch.DrawString(Fonts.lucidaConsole18Bold, name, new Vector2(ui.Position.X + 10, ui.Position.Y + 8), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                        if (greeting != null)
                            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, greeting, new Vector2(ui.Position.X + 10, ui.Position.Y + 45), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                    }
                }
            }
        }

        public static void SetGreeting(string questid, string tag)
        {
            XmlNode greeting = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/greeting/questid/" + questid);

            GVar.npc.Greeting = greeting[tag].InnerText;
            GVar.npc.Greeting = Text.WrapText(Fonts.lucidaConsole14Regular, GVar.npc.Greeting, 500);
        }

        public string QuestID { get { return questID; } set { questID = value; } }
        public string CurrentQuest { get { return currentQuest; } set { currentQuest = value; } }

        public string Name { get { return name; } set { name = value; } }
        public string Greeting { get { return greeting; } set { greeting = value; } }
        public bool HasQuest { get { return hasQuest; } set { hasQuest = value; } }
    }
}
