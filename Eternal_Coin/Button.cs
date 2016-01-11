using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Eternal_Coin
{
    public class Button : Object
    {
        public Button(Texture2D spriteID, Vector2 position, Vector2 size, Color colour, string name, string state, float worth)
            : base(spriteID, position, size, colour, name, state, worth)
        {
            FPS = 40;
            if (spriteID != null)
            {
                AddAnimation(1, 0, 0, GVar.AnimStates.Button.def, spriteID.Width / 2, spriteID.Height, Vector2.Zero);
                AddAnimation(1, 0, spriteID.Width / 2, GVar.AnimStates.Button.mouseover, spriteID.Width / 2, spriteID.Height, Vector2.Zero);
                PlayAnimation(GVar.AnimStates.Button.def);
            }
        }

        public override void Update(float gameTime)
        {
            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public override void HandleMovement(Vector2 pos, float gameTime)
        {
            
        }

        public override void AnimationDone(string animation)
        {

        }

        public static void CreateLocationButtons(LocationNode node)
        {
            if (node.State.Contains("Sub"))
            {
                Button exitLocationButton = new Button(Textures.exitLocationButtonTex, node.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "ExitLocation", GVar.States.Button.locationbutton, 0f);
                Lists.locationButtons.Add(exitLocationButton);

                if (GVar.location != null && !GVar.location.Searched)
                {
                    Button lookEyeButton = new Button(Textures.lookEyeTex, node.Position, Vector.lookEyeSize, Color.White, "LookEyeButton", GVar.States.Button.locationbutton, 0f);
                    Lists.locationButtons.Add(lookEyeButton);
                }
                if (GVar.location != null && GVar.location.Searched && GVar.location.HasNPC)
                {
                    Button npcButton = new Button(Textures.npcButtonTex, node.Position, Vector.locationButtonSize, Color.White, "NPCButton", GVar.States.Button.locationbutton, 0f);
                    Lists.locationButtons.Add(npcButton);
                }
                if (GVar.location != null && GVar.location.Searched && GVar.location.HasShop)
                {
                    Button shopButton = new Button(Textures.pixel, node.Position, Vector.locationButtonSize, Color.Blue, "ShopButton", GVar.States.Button.locationbutton, 0f);
                    Lists.locationButtons.Add(shopButton);
                }
            }
            else if (node.State.Contains("Main"))
            {
                Button enterLocationButton = new Button(Textures.enterLocationButtonTex, node.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "EnterLocation", GVar.States.Button.locationbutton, 0f);
                Lists.locationButtons.Add(enterLocationButton);
            }
            else
            {
                if (GVar.location != null && !GVar.location.Searched)
                {
                    Button lookEyeButton = new Button(Textures.lookEyeTex, node.Position, new Vector2(Vector.lookEyeSize.X, Vector.lookEyeSize.Y), Color.White, "LookEyeButton", GVar.States.Button.locationbutton, 0f);
                    Lists.locationButtons.Add(lookEyeButton);
                }
                if (GVar.location != null && GVar.location.Searched && GVar.location.HasNPC)
                {
                    Button npcButton = new Button(Textures.npcButtonTex, node.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "NPCButton", GVar.States.Button.locationbutton, 0f);
                    Lists.locationButtons.Add(npcButton);
                }
                if (GVar.location != null && GVar.location.Searched && GVar.location.HasShop)
                {
                    Button shopButton = new Button(Textures.pixel, node.Position, Vector.locationButtonSize, Color.Blue, "ShopButton", GVar.States.Button.locationbutton, 0f);
                    Lists.locationButtons.Add(shopButton);
                }
            }

            foreach (Object b in Lists.locationButtons)
            {
                b.ColourA = 5;
            }
        }

        public static void CheckButtonsForDelete()
        {
            for (int i = 0; i < Lists.mainWorldButtons.Count; i++)
            {
                if (Lists.mainWorldButtons[i].State == "delete")
                {
                    Lists.mainWorldButtons.RemoveAt(i);
                }
            }
        }
    }
}
