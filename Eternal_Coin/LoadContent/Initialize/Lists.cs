﻿using Microsoft.Xna.Framework;
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
    public class Lists
    {
        public static List<Object> mainMenuButtons;
        public static List<Object> inventoryButtons;
        public static List<Object> chooseCharacterButtons;
        public static List<Object> optionsButtons;
        public static List<Object> locationButtons;
        public static List<Object> attackButtons;
        public static List<Object> displayPictureButtons;
        public static List<Quest> quests;
        public static List<Object> viewQuestInfoButtons;
        public static List<Object> availableStoriesButtons;
        public static List<Object> mainWorldButtons;
        public static List<Entity> entity;
        public static List<Enemy> enemy;
        public static List<UIElement> uiElements;
        public static List<Node> subNodes;
        public static List<LocationNode> locNodes;
        public static List<XmlDocument> savedGamesXmlDoc;
        public static List<SavedGame> savedGames;
        public static List<string> inventorySlots;
        public static List<string> availableAttacksIDs;
        public static List<string> enemyAttackIDs;
        public static List<string> displayPictureIDs;
        public static List<string> eDisplayPictureIDs;
        public static List<string> soundIDs;

        public static void InitializeLists()
        {
            mainMenuButtons = new List<Object>();
            inventoryButtons = new List<Object>();
            availableStoriesButtons = new List<Object>();
            mainWorldButtons = new List<Object>();
            locationButtons = new List<Object>();
            entity = new List<Entity>();
            enemy = new List<Enemy>();
            chooseCharacterButtons = new List<Object>();
            uiElements = new List<UIElement>();
            quests = new List<Quest>();
            viewQuestInfoButtons = new List<Object>();
            subNodes = new List<Node>();
            locNodes = new List<LocationNode>();
            optionsButtons = new List<Object>();
            savedGames = new List<SavedGame>();
            savedGamesXmlDoc = new List<XmlDocument>();
            displayPictureButtons = new List<Object>();
            attackButtons = new List<Object>();
            availableAttacksIDs = new List<string>();
            enemyAttackIDs = new List<string>();
            soundIDs = new List<string>();
            displayPictureIDs = new List<string>();
            eDisplayPictureIDs = new List<string>();

            inventorySlots = new List<string>();
            inventorySlots.Add(GVar.InventorySlot.helmet);
            inventorySlots.Add(GVar.InventorySlot.chestplate);
            inventorySlots.Add(GVar.InventorySlot.leggings);
            inventorySlots.Add(GVar.InventorySlot.leftBoot);
            inventorySlots.Add(GVar.InventorySlot.rightBoot);
            inventorySlots.Add(GVar.InventorySlot.leftGauntlet);
            inventorySlots.Add(GVar.InventorySlot.rightGauntlet);
            inventorySlots.Add(GVar.InventorySlot.leftHandWeapon);
            inventorySlots.Add(GVar.InventorySlot.rightHandWeapon);
        }

        public static void ClearGameLists()
        {
            locationButtons.Clear();
            quests.Clear();
            viewQuestInfoButtons.Clear();
            mainWorldButtons.Clear();
            entity.Clear();
        }
    }
}