using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace Eternal_Coin
{
    public class Text
    {
        public static string WrapText(SpriteFont spriteFont, string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = spriteFont.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = spriteFont.MeasureString(word);

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }

            return sb.ToString();
        }

        public static void RecordText()
        {
            for (int i = 0; i < InputManager.keys.Length; i++)
            {
                if (InputManager.IsKeyDown(Keys.LeftShift) && InputManager.IsKeyPressed(InputManager.keys[i]))
                {
                    int number = (int)InputManager.keys[i];
                    if (number == 8)
                    {
                        if (string.IsNullOrEmpty(GVar.playerName))
                        {
                            continue;
                        }
                        else
                        {
                            GVar.playerName = GVar.playerName.Substring(0, GVar.playerName.Length - 1);
                        }
                    }
                    else if (GVar.playerName.Length < 10)
                    {
                        char letter = (char)number;

                        GVar.playerName += letter;
                    }
                }
                else if (InputManager.IsKeyPressed(InputManager.keys[i]))
                {
                    int number = (int)InputManager.keys[i];
                    if (number == 8)
                    {
                        if (string.IsNullOrEmpty(GVar.playerName))
                        {
                            continue;
                        }
                        else
                        {
                            GVar.playerName = GVar.playerName.Substring(0, GVar.playerName.Length - 1);
                        }
                    }
                    else if (GVar.playerName.Length < 10 && number <= 90 && number >= 65)
                    {
                        if (number != 32)
                        {
                            number += 32;
                        }
                        char letter = (char)number;

                        GVar.playerName += letter;
                    }
                    else if (GVar.playerName.Length < 10 && number <= 57 && number >= 48)
                    {
                        char letter = (char)number;

                        GVar.playerName += letter;
                    }
                }
            }
        }
    }
}
