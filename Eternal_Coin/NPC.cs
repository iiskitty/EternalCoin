using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;
using System.Xml;

namespace Eternal_Coin
{
    public class NPC
    {
        string name;
        string greeting;
        bool hasQuest;
        bool questAccepted;
        bool questFinished;
        bool questCompleted;

        public NPC(string name, string greeting, bool hasQuest, bool questAccepted, bool questFinished, bool questCompleted)
        {
            this.name = name;
            this.greeting = greeting;
            this.hasQuest = hasQuest;
            this.questAccepted = questAccepted;
            this.questFinished = questFinished;
            this.questCompleted = questCompleted;
        }

        public NPC() { }

        public static void DrawNPCInfo(SpriteBatch spriteBatch, string name, string greeting)
        {
            foreach (UIElement ui in Lists.uiElements)
            {
                if (ui.SpriteID == Textures.NPCInfoUITex && ui.Draw)
                {
                    if (GVar.location != null && GVar.location.Searched)
                    {
                        if (name != null)
                            spriteBatch.DrawString(Fonts.lucidaConsole18Bold, name, new Vector2(ui.Position.X + 10, ui.Position.Y + 8), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                        if (greeting != null)
                            spriteBatch.DrawString(Fonts.lucidaConsole14Regular, greeting, new Vector2(ui.Position.X + 10, ui.Position.Y + 45), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                    }
                }
            }
        }

        public string Name { get { return name; } set { name = value; } }
        public string Greeting { get { return greeting; } set { greeting = value; } }
        public bool HasQuest { get { return hasQuest; } set { hasQuest = value; } }
        public bool QuestAccepted { get { return questAccepted; } set { questAccepted = value; } }
        public bool QuestFinished { get { return questFinished; } set { questFinished = value; } }
        public bool QuestCompleted { get { return questCompleted; } set { questCompleted = value; } }
    }
}
