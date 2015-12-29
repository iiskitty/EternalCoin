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
                AddAnimation(1, 0, 0, GVar.AnimStates.Button.EnterLocation, spriteID.Width, spriteID.Height, Vector2.Zero);
                AddAnimation(1, 0, 0, GVar.AnimStates.Button.LookEye, spriteID.Width, spriteID.Height, Vector2.Zero);
                AddAnimation(1, 0, 0, GVar.AnimStates.Button.NPCButton, spriteID.Width, spriteID.Height, Vector2.Zero);
                AddAnimation(1, 0, 0, GVar.AnimStates.Button.FaceFrontAnim, 75, 75, Vector2.Zero);
                AddAnimation(1, 0, 1350, GVar.AnimStates.Button.FaceBackAnim, 75, 75, Vector2.Zero);
                AddAnimation(19, 0, 0, GVar.AnimStates.Button.SpinAnim, 75, 75, Vector2.Zero);
                AddAnimation(1, 0, 0, GVar.AnimStates.Button.def, spriteID.Width, spriteID.Height, Vector2.Zero);
                PlayAnimation(GVar.AnimStates.Button.FaceFrontAnim);
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
            if (currentAnimation != null && currentAnimation.Contains(GVar.AnimStates.Button.SpinAnim) && name.Contains("ExitButton"))
            {
                PlayAnimation(GVar.AnimStates.Button.FaceBackAnim);
                GVar.exitGame = true;
            }
            if (currentAnimation != null && currentAnimation.Contains(GVar.AnimStates.Button.SpinAnim) && state.Contains("Coin"))
            {
                PlayAnimation(GVar.AnimStates.Button.FaceBackAnim);
            }
            
        }

        public static void CreateLocationButtons(LocationNode node)
        {
            if (node.State.Contains("Sub"))
            {
                Button exitLocationButton = new Button(Textures.exitLocationButtonTex, node.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "ExitLocation", "Alive", 0f);
                exitLocationButton.PlayAnimation(GVar.AnimStates.Button.EnterLocation);
                Lists.locationButtons.Add(exitLocationButton);

                if (GVar.location != null && !GVar.location.Searched)
                {
                    Button lookEyeButton = new Button(Textures.lookEyeTex, node.Position, new Vector2(Vector.lookEyeSize.X, Vector.lookEyeSize.Y), Color.White, "LookEyeButton", "Alive", 0f);
                    lookEyeButton.PlayAnimation(GVar.AnimStates.Button.LookEye);
                    Lists.locationButtons.Add(lookEyeButton);
                }
                if (GVar.location != null && GVar.location.Searched && GVar.location.HasNPC)
                {
                    Button npcButton = new Button(Textures.npcButtonTex, node.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "NPCButton", "Alive", 0f);
                    npcButton.PlayAnimation(GVar.AnimStates.Button.NPCButton);
                    Lists.locationButtons.Add(npcButton);
                }
                if (GVar.location != null && GVar.location.Searched && GVar.location.HasShop)
                {
                    Button shopButton = new Button(Textures.pixel, node.Position, Vector.locationButtonSize, Color.Blue, "ShopButton", "Alive", 0f);
                    shopButton.PlayAnimation(GVar.AnimStates.Button.def);
                    Lists.locationButtons.Add(shopButton);
                }
            }
            else if (node.State.Contains("Main"))
            {
                Button enterLocationButton = new Button(Textures.enterLocationButtonTex, node.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "EnterLocation", "Alive", 0f);
                enterLocationButton.PlayAnimation(GVar.AnimStates.Button.EnterLocation);
                Lists.locationButtons.Add(enterLocationButton);
            }
            else
            {
                if (GVar.location != null && !GVar.location.Searched)
                {
                    Button lookEyeButton = new Button(Textures.lookEyeTex, node.Position, new Vector2(Vector.lookEyeSize.X, Vector.lookEyeSize.Y), Color.White, "LookEyeButton", "Alive", 0f);
                    lookEyeButton.PlayAnimation(GVar.AnimStates.Button.LookEye);
                    Lists.locationButtons.Add(lookEyeButton);
                }
                if (GVar.location != null && GVar.location.Searched && GVar.location.HasNPC)
                {
                    Button npcButton = new Button(Textures.npcButtonTex, node.Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "NPCButton", "Alive", 0f);
                    npcButton.PlayAnimation(GVar.AnimStates.Button.NPCButton);
                    Lists.locationButtons.Add(npcButton);
                }
                if (GVar.location != null && GVar.location.Searched && GVar.location.HasShop)
                {
                    Button shopButton = new Button(Textures.pixel, node.Position, Vector.locationButtonSize, Color.Blue, "ShopButton", "Alive", 0f);
                    shopButton.PlayAnimation(GVar.AnimStates.Button.def);
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
