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
    public class MainWorld
    {
        public static void DrawMainWorld(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (GVar.location != null)
                Location.DrawLocationInfo(spriteBatch, GVar.location);

            NPC.DrawNPCInfo(spriteBatch, GVar.npc.Name, GVar.npc.Greeting);

            foreach (UIElement ui in Lists.uiElements)
            {
                if (ui.SpriteID == Textures.questInfoUI && ui.Draw)
                {
                    Vector2 questInfoPosition = new Vector2(ui.Position.X + ui.SpriteID.Width - 195, ui.Position.Y + ui.SpriteID.Height - 120);
                    spriteBatch.DrawString(Fonts.lucidaConsole10Regular, GVar.questInfo, questInfoPosition, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                }

                if (ui.SpriteID == Textures.questListUI && ui.Draw)
                {
                    Vector2 questInfoPosition = new Vector2(ui.SpriteID.Width - 275, ui.SpriteID.Height - 364);
                    Vector2 questCompletedPosition = new Vector2(ui.SpriteID.Width - 24, ui.SpriteID.Height - 366);
                    foreach (Quest q in Lists.quests)
                    {
                        if (q.Completed)
                            spriteBatch.Draw(Textures.tickTex, new Rectangle((int)questCompletedPosition.X, (int)questCompletedPosition.Y, Textures.tickTex.Width, Textures.tickTex.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
                        else if (!q.Completed)
                            spriteBatch.Draw(Textures.crossTex, new Rectangle((int)questCompletedPosition.X, (int)questCompletedPosition.Y, Textures.crossTex.Width, Textures.crossTex.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
                        spriteBatch.DrawString(Fonts.lucidaConsole10Regular, q.ShortDescription, questInfoPosition, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                        questInfoPosition.Y += 18;
                        questCompletedPosition.Y += 18;
                    }
                }
            }

            foreach (Object b in Lists.viewQuestInfoButtons)
            {
                b.Update(gameTime);
                b.Draw(spriteBatch, b.SpriteID, b.Bounds, 0.2f, 0f, Vector2.Zero);
                if (MouseManager.mouseBounds.Intersects(b.Bounds))
                {
                    GVar.DrawBoundingBox(b.Bounds, spriteBatch, Textures.pixel, 1, 0.2f, Color.Green);
                }
            }

            foreach (Object b in Lists.mainWorldButtons)
            {
                b.Update(gameTime);
                b.Draw(spriteBatch, b.SpriteID, b.Bounds, 0.2f, 0f, Vector2.Zero);
                if (MouseManager.mouseBounds.Intersects(b.Bounds))
                {
                    b.PlayAnimation(GVar.AnimStates.Button.mouseover);
                }
                if (b.CurrentAnimation == GVar.AnimStates.Button.mouseover && !MouseManager.mouseBounds.Intersects(b.Bounds))
                {
                    b.PlayAnimation(GVar.AnimStates.Button.def);
                }
                if (b.Name == "MainMenu")
                {
                    spriteBatch.DrawString(Fonts.lucidaConsole18Bold, "Main Menu", new Vector2(b.Position.X + b.Size.X / 4, b.Position.Y + b.Size.Y / 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.21f);
                }
                if (b.Name == "Options")
                {
                    spriteBatch.DrawString(Fonts.lucidaConsole18Bold, "Options", new Vector2(b.Position.X + b.Size.X / 4, b.Position.Y + b.Size.Y / 4), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.21f);
                }
            }

            GVar.worldMap.Update(gameTime);
            GVar.worldMap.Draw(spriteBatch, GVar.worldMap.SpriteID, GVar.worldMap.Bounds, 0.1f, 0f, Vector2.Zero);
            foreach (Entity P in Lists.entity)
            {
                P.Update(gameTime);
                P.Draw(spriteBatch, P.SpriteID, P.Bounds, 0.172f, 0f, Vector2.Zero);
                foreach (Node cln in P.CurrentLocation)
                {
                    if (P.Bounds.Intersects(cln.PlayerPort))
                    {
                        foreach (Object LB in Lists.locationButtons)
                        {
                            LB.Update(gameTime);
                            LB.Draw(spriteBatch, LB.SpriteID, LB.Bounds, 0.17f, 0f, Vector2.Zero);
                            if (MouseManager.mouseBounds.Intersects(LB.Bounds) && !GVar.gamePaused)
                            {
                                LB.PlayAnimation(GVar.AnimStates.Button.mouseover);
                            }
                            if (LB.CurrentAnimation == GVar.AnimStates.Button.mouseover && !MouseManager.mouseBounds.Intersects(LB.Bounds))
                            {
                                LB.PlayAnimation(GVar.AnimStates.Button.def);
                            }
                        }
                    }
                    cln.Update(gameTime);
                    cln.Draw(spriteBatch, cln.SpriteID, cln.Bounds, 0.17f, 0f, Vector2.Zero);
                    foreach (Node conLocNode in cln.LocNodeConnections)
                    {
                        conLocNode.Update(gameTime);
                        conLocNode.Draw(spriteBatch, conLocNode.SpriteID, conLocNode.Bounds, 0.17f, 0f, Vector2.Zero);
                    }
                }
            }
        }

        public static void UpdateMainWorld(GameTime gameTime)
        {
            Button.CheckButtonsForDelete();

            if (InputManager.IsKeyPressed(Keys.Q))
            {
                SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                foreach (UIElement ui in Lists.uiElements)
                {
                    if (ui.SpriteID == Textures.questListUI)
                    {
                        if (!ui.Draw)
                        {
                            UI.DisplayQuests();
                        }
                        else if (ui.Draw)
                        {
                            UI.CloseQuestListUI();
                            for (int i = 0; i < Lists.mainWorldButtons.Count; i++)
                            {
                                if (Lists.mainWorldButtons[i].Name == "CloseQuestListUI")
                                {
                                    Lists.mainWorldButtons.RemoveAt(i);
                                    Lists.viewQuestInfoButtons.Clear();
                                }
                            }
                        }
                    }
                }
            }

            if (InputManager.IsKeyPressed(Keys.I))
            {
                SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                Button closeInv = new Button(Textures.pixel, new Vector2(), new Vector2(25, 25), Color.Red, "CloseInventory", "Alive", 0f);
                Lists.inventoryButtons.Add(closeInv);
                GVar.currentGameState = GVar.GameState.inventory;
                GVar.previousGameState = GVar.GameState.game;
            }

            foreach (UIElement ui in Lists.uiElements)
            {
                if (InputManager.IsKeyPressed(Keys.Escape) && ui.SpriteID == Textures.pauseUI && !ui.Draw)
                {
                    GVar.gamePaused = true;
                    ui.Draw = true;
                    Button mainMenu = new Button(Textures.pixel, new Vector2(ui.Position.X + 93, ui.Position.Y + 93), new Vector2(322, 40), Color.Yellow, "MainMenu", "Alive", 0f);
                    Button options = new Button(Textures.pixel, new Vector2(ui.Position.X + 93, ui.Position.Y + 158), new Vector2(322, 40), Color.Yellow, "Options", "Alive", 0f);
                    Lists.mainWorldButtons.Add(options);
                    Lists.mainWorldButtons.Add(mainMenu);
                }
                else if (InputManager.IsKeyPressed(Keys.Escape) && ui.SpriteID == Textures.pauseUI && ui.Draw)
                {
                    GVar.gamePaused = false;
                    ui.Draw = false;
                    for (int i = 0; i < Lists.mainWorldButtons.Count; i++)
                    {
                        if (Lists.mainWorldButtons[i].Name == "MainMenu" || Lists.mainWorldButtons[i].Name == "Options")
                        {
                            Lists.mainWorldButtons[i].State = "delete";
                        }
                    }
                }
            }

            GVar.worldMap.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            foreach (Entity P in Lists.entity)
            {
                if (InputManager.IsKeyDown(Keys.LeftShift) && InputManager.IsKeyPressed(Keys.S))
                {
                    Save.SaveGame(GVar.savedGameLocation, P, Lists.quests);
                }
                Vector2 viewQuestInfoButtonPosition = new Vector2();
                foreach (UIElement ui in Lists.uiElements)
                {
                    if (ui.SpriteID == Textures.questListUI)
                    {
                        viewQuestInfoButtonPosition = new Vector2(ui.SpriteID.Width - 277, ui.SpriteID.Height - 366);
                    }
                }
                for (int i = 0; i < Lists.viewQuestInfoButtons.Count; i++)
                {
                    Lists.viewQuestInfoButtons[i].Position = viewQuestInfoButtonPosition;
                    Lists.viewQuestInfoButtons[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                    viewQuestInfoButtonPosition.Y += 18;
                    if (MouseManager.mouseBounds.Intersects(Lists.viewQuestInfoButtons[i].Bounds) && InputManager.IsLMPressed())
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                        GVar.LogDebugInfo("ButtonClicked: " + Lists.viewQuestInfoButtons[i].Name, 2);
                        for (int ui = 0; ui < Lists.uiElements.Count; ui++)
                        {
                            if (Lists.uiElements[ui].SpriteID == Textures.questInfoUI && !Lists.uiElements[ui].Draw)
                            {
                                Lists.mainWorldButtons.Add(new Button(Textures.pixel, new Vector2(), new Vector2(20, 20), Color.Red, "CloseQuestInfoUI", "Alive", 0f));
                                Lists.uiElements[ui].Draw = true;
                                GVar.questInfo = Lists.quests[i].Description;
                                GVar.questInfo = Text.WrapText(Fonts.lucidaConsole10Regular, GVar.questInfo, 200);
                            }
                            else if (Lists.uiElements[ui].SpriteID == Textures.questInfoUI && Lists.uiElements[ui].Draw)
                            {
                                GVar.questInfo = Lists.quests[i].Description;
                                GVar.questInfo = Text.WrapText(Fonts.lucidaConsole10Regular, GVar.questInfo, 200);
                            }
                        }
                    }
                }
                for (int i = 0; i < Lists.mainWorldButtons.Count; i++)
                {
                    Updates.UpdateGameButtons(Lists.mainWorldButtons[i], P, gameTime);
                    if (MouseManager.mouseBounds.Intersects(Lists.mainWorldButtons[i].Bounds) && InputManager.IsLMPressed() && !GVar.gamePaused)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                        GVar.LogDebugInfo("ButtonClicked: " + Lists.mainWorldButtons[i].Name, 2);
                        if (Lists.mainWorldButtons.Count > 0 && Lists.mainWorldButtons[i].Name == "QuestAcceptButton")
                        {
                            Quest.AcceptQuest(P);
                            Lists.mainWorldButtons.RemoveAt(i);
                            break;
                        }
                        else if (Lists.mainWorldButtons.Count > 0 && Lists.mainWorldButtons[i].Name == "HandInQuestButton")
                        {
                            Quest.HandInQuest(P);
                            Lists.mainWorldButtons.RemoveAt(i);
                        }
                        else if (Lists.mainWorldButtons[i].Name == "OpenShop")
                        {
                            Shop.LoadShopInventory(GVar.curLocNode);

                            Button closeInv = new Button(Textures.pixel, new Vector2(), new Vector2(25, 25), Color.Red, "CloseInventory", "Alive", 0f);
                            Lists.inventoryButtons.Add(closeInv);

                            Lists.mainWorldButtons.RemoveAt(i);

                            GVar.currentGameState = GVar.GameState.shop;
                            GVar.previousGameState = GVar.GameState.game;

                            UI.CloseNPCUI();
                        }
                        else if (Lists.mainWorldButtons[i].Name == "DisplayQuests")
                        {
                            UI.DisplayQuests();
                        }
                        else if (Lists.mainWorldButtons[i].Name == "DisplayInventory")
                        {
                            Button closeInv = new Button(Textures.pixel, new Vector2(), new Vector2(25, 25), Color.Red, "CloseInventory", "Alive", 0f);
                            Lists.inventoryButtons.Add(closeInv);
                            GVar.currentGameState = GVar.GameState.inventory;
                            GVar.previousGameState = GVar.GameState.game;
                        }
                        else if (Lists.mainWorldButtons[i].Name == "CloseQuestListUI")
                        {
                            UI.CloseQuestListUI();
                            Lists.mainWorldButtons.RemoveAt(i);
                            Lists.viewQuestInfoButtons.Clear();
                        }
                        else if (Lists.mainWorldButtons[i].Name == "CloseQuestInfoUI")
                        {
                            UI.CloseQuestInfoUI();
                            Lists.mainWorldButtons.RemoveAt(i);
                        }
                        else if (Lists.mainWorldButtons[i].Name == "CloseNPCUIButton")
                        {
                            Lists.mainWorldButtons.RemoveAt(i);
                            for (int j = 0; j < Lists.mainWorldButtons.Count; j++)
                            {
                                if (Lists.mainWorldButtons[j].Name == "OpenShop")
                                    Lists.mainWorldButtons.RemoveAt(j);
                            }
                            UI.CloseNPCUI();
                            break;
                        }
                    }
                    else if (MouseManager.mouseBounds.Intersects(Lists.mainWorldButtons[i].Bounds) && InputManager.IsLMPressed() && GVar.gamePaused)
                    {
                        SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickButton", false);
                        if (Lists.mainWorldButtons[i].Name == "MainMenu")
                        {
                            Save.SaveGame(GVar.savedGameLocation, P, Lists.quests);
                            GVar.changeToMainMenu = true;
                            Colours.drawBlackFade = true;
                            Colours.fadeIn = true;
                            GVar.playerName = string.Empty;
                        }
                        else if (Lists.mainWorldButtons[i].Name == "Options")
                        {
                            Button backToGame = new Button(Textures.pixel, new Vector2(1130, 50), new Vector2(100, 50), Color.Yellow, "BackToGame", "Alive", 0f);
                            Lists.optionsButtons.Add(backToGame);
                            GVar.changeToOptions = true;
                            Colours.drawBlackFade = true;
                            Colours.fadeIn = true;
                        }
                    }
                }

                if (InputManager.IsKeyDown(Keys.LeftShift) && InputManager.IsKeyPressed(Keys.S))
                    Save.SaveGame(GVar.savedGameLocation, P, Lists.quests);

                P.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

                for (int i = 0; i < P.CurrentLocation.Count; i++)
                {
                    if (P.Bounds.Intersects(P.CurrentLocation[i].PlayerPort))
                    {
                        if (GVar.location.HasEnemy && !Colours.fadeIn && !GVar.changeToBattle && GVar.location.Searched)
                        {
                            Colours.fadeIn = true;
                            Colours.drawBlackFade = true;
                            GVar.changeToBattle = true;
                        }

                        Colours.UpdateMainAlphas(P.CurrentLocation[i]);

                        for (int j = 0; j < Lists.locationButtons.Count; j++)
                        {
                            Updates.UpdateGameButtons(Lists.locationButtons[j], P, gameTime);

                            if (MouseManager.mouseBounds.Intersects(Lists.locationButtons[j].Bounds) && InputManager.IsLMPressed() && !GVar.gamePaused)
                            {
                                SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickLocButton", false);
                                GVar.LogDebugInfo("ButtonClicked: " + Lists.locationButtons[j].Name, 2);
                                if (GVar.location != null && GVar.location.Searched)
                                {
                                    if (Lists.locationButtons[j].Name == "NPCButton")
                                    {
                                        for (int k = 0; k < Lists.mainWorldButtons.Count; k++)
                                        {
                                            if (Lists.mainWorldButtons[k].Name == "OpenShop")
                                            {
                                                Lists.mainWorldButtons[k].State = "delete";
                                            }
                                        }
                                        try
                                        {
                                            XmlNode locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc");
                                            GVar.npc = new NPC(locNPC[GVar.XmlTags.NPCTags.name].InnerText, "", Convert.ToBoolean(locNPC[GVar.XmlTags.QuestTags.hasquest].InnerText), Convert.ToBoolean(locNPC[GVar.XmlTags.QuestTags.questaccepted].InnerText), Convert.ToBoolean(locNPC[GVar.XmlTags.QuestTags.questfinished].InnerText), Convert.ToBoolean(locNPC[GVar.XmlTags.QuestTags.questcompleted].InnerText));
                                            locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/npc/greeting");

                                            if (GVar.npc.HasQuest && !GVar.npc.QuestAccepted)
                                            {
                                                GVar.npc.Greeting = locNPC[GVar.XmlTags.NPCTags.Greetings.questunaccepted].InnerText;
                                                foreach (UIElement ui in Lists.uiElements)
                                                {
                                                    if (ui.SpriteID == Textures.NPCInfoUITex && !ui.Draw)
                                                    {
                                                        Button acceptQuest = new Button(Textures.pixel, new Vector2(ui.Position.X, ui.Position.Y + ui.Size.Y - Textures.pixel.Width), new Vector2(25, 15), Color.Green, "QuestAcceptButton", "Alive", 0f);
                                                        acceptQuest.PlayAnimation(GVar.AnimStates.Button.def);
                                                        Lists.mainWorldButtons.Add(acceptQuest);
                                                    }
                                                }
                                            }
                                            else if (!GVar.npc.HasQuest && GVar.npc.QuestAccepted)
                                            {
                                                GVar.npc.Greeting = locNPC[GVar.XmlTags.NPCTags.Greetings.questaccepted].InnerText;
                                            }
                                            else if (!GVar.npc.HasQuest && GVar.npc.QuestFinished)
                                            {
                                                GVar.npc.Greeting = locNPC[GVar.XmlTags.NPCTags.Greetings.questfinished].InnerText;
                                                foreach (UIElement ui in Lists.uiElements)
                                                {
                                                    if (ui.SpriteID == Textures.NPCInfoUITex && !ui.Draw)
                                                    {
                                                        Button handInQuest = new Button(Textures.pixel, new Vector2(ui.Position.X, ui.Position.Y + ui.Size.Y - Textures.pixel.Width), new Vector2(25, 15), Color.Blue, "HandInQuestButton", "Alive", 0f);
                                                        handInQuest.PlayAnimation(GVar.AnimStates.Button.def);
                                                        Lists.mainWorldButtons.Add(handInQuest);
                                                    }
                                                }
                                            }
                                            else if (!GVar.npc.HasQuest && GVar.npc.QuestCompleted)
                                            {
                                                GVar.npc.Greeting = locNPC[GVar.XmlTags.NPCTags.Greetings.questcompleted].InnerText;
                                            }
                                            GVar.npc.Greeting = Text.WrapText(Fonts.lucidaConsole14Regular, GVar.npc.Greeting, 150);

                                            locNPC = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/actions");
                                            Quest.CheckAction(locNPC[GVar.XmlTags.Actions.talknpc].InnerText, P.CurrentLocation[i]);

                                            foreach (UIElement ui in Lists.uiElements)
                                            {
                                                if (ui.SpriteID == Textures.NPCInfoUITex && !ui.Draw)
                                                {
                                                    Button closeNPCUI = new Button(Textures.pixel, new Vector2(ui.Position.X + ui.Size.X - Textures.pixel.Width, ui.Position.Y), new Vector2(15, 15), Color.Red, "CloseNPCUIButton", "Alive", 0f);
                                                    closeNPCUI.PlayAnimation(GVar.AnimStates.Button.def);
                                                    Lists.mainWorldButtons.Add(closeNPCUI);
                                                    ui.Draw = true;
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            GVar.npc = new NPC();
                                        }
                                    }
                                    else if (Lists.locationButtons[j].Name == "ShopButton")
                                    {
                                        XmlNode shopKeep = GVar.curLocNode.SelectSingleNode("/location/shop");

                                        string greeting = Text.WrapText(Fonts.lucidaConsole14Regular, shopKeep["greeting"].InnerText, 150);

                                        GVar.npc = new NPC(shopKeep["name"].InnerText, greeting, false, false, false, false);

                                        Button openShop = new Button(Textures.pixel, Vector2.Zero, new Vector2(25, 15), Color.Yellow, "OpenShop", "Alive", 0f);
                                        Lists.mainWorldButtons.Add(openShop);

                                        foreach (UIElement ui in Lists.uiElements)
                                        {
                                            if (ui.SpriteID == Textures.NPCInfoUITex && !ui.Draw)
                                            {
                                                Button closeNPCUI = new Button(Textures.pixel, new Vector2(ui.Position.X + ui.Size.X - Textures.pixel.Width, ui.Position.Y), new Vector2(15, 15), Color.Red, "CloseNPCUIButton", "Alive", 0f);
                                                closeNPCUI.PlayAnimation(GVar.AnimStates.Button.def);
                                                Lists.mainWorldButtons.Add(closeNPCUI);
                                                ui.Draw = true;
                                            }
                                        }
                                    }
                                }
                                if (GVar.location != null && !GVar.location.Searched)
                                {
                                    if (Lists.locationButtons[j].Name == "LookEyeButton")
                                    {
                                        if (P.CurrentLocation[i].State.Contains("Sub"))
                                        {
                                            for (int k = 0; k < P.CurrentLocation[i].MainLocNode.Count; k++)
                                            {
                                                Button exitLocationButton = new Button(Textures.exitLocationButtonTex, P.CurrentLocation[i].Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "ExitLocation", "Alive", 0f);
                                                exitLocationButton.PlayAnimation(GVar.AnimStates.Button.def);
                                                Lists.locationButtons.Add(exitLocationButton);
                                                ReadXml.ReadLocationXmlFile(P, P.CurrentLocation[i].MainLocNode[k]);
                                                GVar.location.Searched = true;
                                                P.CurrentLocation[i].MainLocNode[k].Searched = true;
                                                XmlNode mainLocNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location");
                                                mainLocNode[GVar.XmlTags.LocationTags.searched].InnerText = GVar.location.Searched.ToString();
                                                SaveXml.SaveLocationXmlFile(P, P.CurrentLocation[i].MainLocNode[k]);
                                                GVar.location = null;
                                            }
                                        }
                                        ReadXml.ReadLocationXmlFile(P, P.CurrentLocation[i]);
                                        GVar.location.Searched = true;
                                        P.CurrentLocation[i].Searched = true;
                                        XmlNode locNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location");
                                        locNode[GVar.XmlTags.LocationTags.searched].InnerText = GVar.location.Searched.ToString();

                                        locNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/actions");
                                        Quest.CheckAction(locNode[GVar.XmlTags.Actions.explore].InnerText, P.CurrentLocation[i]);

                                        SaveXml.SaveLocationXmlFile(P, P.CurrentLocation[i]);

                                        if (GVar.location.HasNPC)
                                        {
                                            Button npcButton = new Button(Textures.npcButtonTex, P.CurrentLocation[i].Position, Vector.locationButtonSize, Color.White, "NPCButton", "Alive", 0f);
                                            npcButton.PlayAnimation(GVar.AnimStates.Button.def);
                                            Lists.locationButtons.Add(npcButton);
                                        }

                                        if (GVar.location.HasShop)
                                        {
                                            Button shopButton = new Button(Textures.pixel, P.CurrentLocation[i].Position, Vector.locationButtonSize, Color.Blue, "ShopButton", "Alive", 0f);
                                            shopButton.PlayAnimation(GVar.AnimStates.Button.def);
                                            Lists.locationButtons.Add(shopButton);
                                        }

                                        Lists.locationButtons.RemoveAt(j);
                                    }
                                }
                            }
                        }
                    }
                    if (!P.Bounds.Intersects(P.CurrentLocation[i].PlayerPort))
                    {
                        GVar.worldMap.SetMapSpeed(P, P.CurrentLocation[i]);
                        GVar.worldMap.MapMovement((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    else
                    {
                        GVar.worldMap.SetMapSpeed(P, P.CurrentLocation[i]);
                        GVar.worldMap.MapMovement((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }

                    P.CurrentLocation[i].HandleMovement(GVar.worldMap.Position);
                    P.CurrentLocation[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                    for (int j = 0; j < P.CurrentLocation[i].LocNodeConnections.Count; j++)
                    {
                        P.CurrentLocation[i].LocNodeConnections[j].HandleMovement(GVar.worldMap.Position);

                        for (int k = 0; k < Lists.locationButtons.Count; k++)
                        {
                            if (MouseManager.mouseBounds.Intersects(Lists.locationButtons[k].Bounds) && InputManager.IsLMPressed() && !GVar.gamePaused)
                            {
                                //SoundManager.PlaySound(Dictionaries.sounds[GVar.PlaySound.clickbutton], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickLocButton", false);
                                GVar.LogDebugInfo("ButtonClicked: " + Lists.locationButtons[k].Name, 2);
                                if (Lists.locationButtons[k].Name == "EnterLocation")
                                {
                                    UI.CloseNPCUI();

                                    Colours.EnableFadeOutMap();

                                    for (int l = 0; l < P.CurrentLocation[i].SubLocNode.Count; l++)
                                    {
                                        for (int slnc = 0; slnc < P.CurrentLocation[i].SubLocNode[l].LocNodeConnections.Count; slnc++)
                                        {
                                            ReadXml.ReadLocationXmlFile(P, P.CurrentLocation[i].SubLocNode[l].LocNodeConnections[slnc]);
                                            P.CurrentLocation[i].SubLocNode[l].LocNodeConnections[slnc].ColourA = 5;
                                        }
                                        ReadXml.ReadLocationXmlFile(P, P.CurrentLocation[i].SubLocNode[l]);
                                        XmlNode tempNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/actions");
                                        Quest.CheckAction(tempNode[GVar.XmlTags.Actions.enter].InnerText, P.CurrentLocation[i]);

                                        WorldMap.SelectNewMap(P.CurrentLocation[i].SubLocNode[l]);
                                        P.CurrentLocation[i].SubLocNode[l].ColourA = 5;
                                        P.CurrentLocation.Add(P.CurrentLocation[i].SubLocNode[l]);
                                        GVar.LogDebugInfo("LocationChange: " + P.CurrentLocation[i].SubLocNode[l].Name, 2);
                                        GVar.npc = new NPC();
                                        Lists.locationButtons.Clear();
                                        Button.CreateLocationButtons(P.CurrentLocation[i].SubLocNode[l]);

                                        P.CurrentLocation.RemoveAt(i);
                                    }
                                }
                                else if (Lists.locationButtons[k].Name == "ExitLocation")
                                {
                                    UI.CloseNPCUI();

                                    Colours.EnableFadeOutMap();

                                    for (int l = 0; l < P.CurrentLocation[i].MainLocNode.Count; l++)
                                    {
                                        for (int slnc = 0; slnc < P.CurrentLocation[i].MainLocNode[l].LocNodeConnections.Count; slnc++)
                                        {
                                            ReadXml.ReadLocationXmlFile(P, P.CurrentLocation[i].MainLocNode[l].LocNodeConnections[slnc]);
                                            P.CurrentLocation[i].MainLocNode[l].LocNodeConnections[slnc].ColourA = 5;
                                        }
                                        ReadXml.ReadLocationXmlFile(P, P.CurrentLocation[i].MainLocNode[l]);

                                        GVar.worldMap.SpriteID = Textures.worldMapTex;
                                        P.CurrentLocation[i].MainLocNode[l].ColourA = 5;
                                        P.CurrentLocation.Add(P.CurrentLocation[i].MainLocNode[l]);
                                        GVar.LogDebugInfo("LocationChange: " + P.CurrentLocation[i].MainLocNode[l].Name, 2);
                                        GVar.npc = new NPC();
                                        Lists.locationButtons.Clear();
                                        if (P.CurrentLocation[i].MainLocNode[l].State.Contains("Main"))
                                        {
                                            Button enterLocationButton = new Button(Textures.enterLocationButtonTex, Lists.entity[i].CurrentLocation[j].Position, new Vector2(Vector.locationButtonSize.X, Vector.locationButtonSize.Y), Color.White, "EnterLocation", "Alive", 0f);
                                            enterLocationButton.PlayAnimation(GVar.AnimStates.Button.def);
                                            Lists.locationButtons.Add(enterLocationButton);
                                        }

                                        P.CurrentLocation.RemoveAt(i);
                                    }
                                }
                            }
                        }

                        if (MouseManager.mouseBounds.Intersects(P.CurrentLocation[i].LocNodeConnections[j].Bounds) && InputManager.IsLMPressed() && !GVar.gamePaused)
                        {
                            UI.CloseNPCUI();
                            SoundManager.PlaySound(Dictionaries.sounds[GVar.SoundIDs.clicklocnode], GVar.Volume.Audio.volume, GVar.Volume.Audio.pitch, GVar.Volume.Audio.pan, "ClickLocNode", false);
                            ReadXml.ReadLocationXmlFile(P, P.CurrentLocation[i].LocNodeConnections[j]);
                            GVar.worldMap.SetMapSpeed(P, P.CurrentLocation[i].LocNodeConnections[j]);
                            foreach (LocationNode LN in P.CurrentLocation[i].LocNodeConnections[j].LocNodeConnections)
                            {
                                if (LN.Name != P.CurrentLocation[i].Name)
                                {
                                    LN.ColourA = 5;
                                }
                            }
                            P.CurrentLocation.Add(P.CurrentLocation[i].LocNodeConnections[j]);
                            GVar.LogDebugInfo("LocationChange: " + P.CurrentLocation[i].LocNodeConnections[j].Name, 2);
                            GVar.npc = new NPC();
                            Lists.locationButtons.Clear();
                            Button.CreateLocationButtons(P.CurrentLocation[i].LocNodeConnections[j]);

                            XmlNode tempNode = GVar.curLocNode.DocumentElement.SelectSingleNode("/location/actions");
                            Quest.CheckAction(tempNode[GVar.XmlTags.Actions.enter].InnerText, P.CurrentLocation[i].LocNodeConnections[j]);

                            P.CurrentLocation.RemoveAt(i);

                            break;
                        }
                        P.CurrentLocation[i].LocNodeConnections[j].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                }
            }
        }

        public static void LoadMainWorld()
        {
            string folderDir = "Content/GameFiles/" + GVar.playerName;
            string locationTemplate = "Content/LocationTemplates/" + GVar.storyName;

            if (!Directory.Exists(folderDir))
            {
                Directory.CreateDirectory(folderDir);

                DirectoryCopy(locationTemplate, folderDir, true);

                GVar.loadData = true;
            }

            Button quests = new Button(Textures.pixel, new Vector2(), new Vector2(25, 25), Color.Violet, "DisplayQuests", "Alive", 0f);
            Button inventory = new Button(Textures.inventoryButton, new Vector2(), new Vector2(25, 25), Color.White, "DisplayInventory", "Alive", 0f);
            Lists.mainWorldButtons.Add(inventory);
            Lists.mainWorldButtons.Add(quests);

            for (int i = 0; i < Lists.entity.Count; i++)
            {
                for (int j = 0; j < Lists.entity[i].CurrentLocation.Count; j++)
                {
                    for (int k = 0; k < Lists.entity[i].CurrentLocation[j].LocNodeConnections.Count; k++)
                    {
                        ReadXml.ReadLocationXmlFile(Lists.entity[i], Lists.entity[i].CurrentLocation[j].LocNodeConnections[k]);
                    }
                    ReadXml.ReadLocationXmlFile(Lists.entity[i], Lists.entity[i].CurrentLocation[j]);
                    GameTime gameTime = new GameTime();
                    Lists.entity[i].CurrentLocation[j].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                    GVar.worldMap.SetMapPosition(Lists.entity[i].CurrentLocation[j]);

                    Button.CreateLocationButtons(Lists.entity[i].CurrentLocation[j]);
                }

            }
            GVar.npc = new NPC();
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
