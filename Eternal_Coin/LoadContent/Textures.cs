using Microsoft.Xna.Framework.Graphics;
using System.Xml;
using Microsoft.Xna.Framework.Content;

namespace Eternal_Coin
{
  public class Textures
  {
    public struct Button
    {
      public static Texture2D locationNode;
      public static Texture2D startButton;
      public static Texture2D questsButton;
      public static Texture2D backButton;
      public static Texture2D backArrow;
      public static Texture2D closeButton;
      public static Texture2D continueButton;
      public static Texture2D newButton;
      public static Texture2D loadButton;
      public static Texture2D deleteButton;
      public static Texture2D inventoryButton;
      public static Texture2D playButton;
      public static Texture2D exitButton;
      public static Texture2D optionsButton;
      public static Texture2D npcButton;
      public static Texture2D enterLocationButton;
      public static Texture2D exitLocationButton;
      public static Texture2D leftLightSide;
      public static Texture2D leftDarkSide;
      public static Texture2D rightLightSide;
      public static Texture2D rightDarkSide;
      public static Texture2D middleLight;
      public static Texture2D middleDark;
      public static Texture2D lookEye;
    };

    public struct UI
    {
      public static Texture2D locationInfoUI;
      public static Texture2D NPCInfoUI;
      public static Texture2D NPCQuestListUI;
      public static Texture2D questListUI;
      public static Texture2D questInfoUI;
      public static Texture2D inventoryUI;
      public static Texture2D inventoryBackDropUI;
      public static Texture2D shopInventoryUI;
      public static Texture2D battleUI;
      public static Texture2D endBattleUI;
      public static Texture2D pauseUI;
      public static Texture2D itemInfoUI;
      public static Texture2D newGameUIBorder;
      public static Texture2D newGameUIInner;
      public static Texture2D saveGameUI;
      public static Texture2D tick;
      public static Texture2D cross;
      public static Texture2D padLock;
    };

    public struct Misc
    {
      public static Texture2D clearPixel;
      public static Texture2D pixel;
      public static Texture2D worldMap;
      public static Texture2D cursor;
      public static Texture2D title;
      public static Texture2D background;
    };

    public static void LoadTextures(ContentManager Content)
    {
      XmlDocument worldMaps = new XmlDocument();
      worldMaps.Load("./Content/AvailableStories.xml");
      XmlNodeList storyList = worldMaps.SelectNodes("/stories/story");
      
      foreach (XmlNode story in storyList)
        Dictionaries.worldMaps.Add(story["name"].InnerText + "Map", Content.Load<Texture2D>("LocationTemplates/" + story["name"].InnerText + "/WorldMap"));

      XmlDocument texDoc = new XmlDocument();
      texDoc.Load("./Content/LoadData/Textures.xml");
      XmlNodeList texList = texDoc.SelectNodes("/textures/load/texture");

      foreach (XmlNode tex in texList)
        Dictionaries.textures.Add(tex["id"].InnerText, Content.Load<Texture2D>(tex["filepath"].InnerText));

      XmlDocument setTexDoc = new XmlDocument();
      setTexDoc.Load("./Content/LoadData/Textures.xml");
      XmlNode texID = setTexDoc.SelectSingleNode("/textures/set");

      UI.itemInfoUI = Dictionaries.textures[texID["UIiteminfo"].InnerText];
      UI.newGameUIBorder = Dictionaries.textures[texID["UInewgameborder"].InnerText];
      UI.newGameUIInner = Dictionaries.textures[texID["UInewgameinner"].InnerText];
      UI.saveGameUI = Dictionaries.textures[texID["UIsavegame"].InnerText];
      UI.locationInfoUI = Dictionaries.textures[texID["UIlocinfo"].InnerText];
      UI.NPCInfoUI = Dictionaries.textures[texID["UInpcinfo"].InnerText];
      UI.NPCQuestListUI = Dictionaries.textures[texID["UInpcquestlist"].InnerText];
      UI.questListUI = Dictionaries.textures[texID["UIquestlist"].InnerText];
      UI.questInfoUI = Dictionaries.textures[texID["UIquestinfo"].InnerText];
      UI.inventoryUI = Dictionaries.textures[texID["UIinv"].InnerText];
      UI.inventoryBackDropUI = Dictionaries.textures[texID["UIinvback"].InnerText];
      UI.shopInventoryUI = Dictionaries.textures[texID["UIshopinv"].InnerText];
      UI.pauseUI = Dictionaries.textures[texID["UIpause"].InnerText];
      UI.battleUI = Dictionaries.textures[texID["UIBattle"].InnerText];
      UI.endBattleUI = Dictionaries.textures[texID["UIendbattle"].InnerText];
      UI.tick = Dictionaries.textures[texID["tick"].InnerText];
      UI.cross = Dictionaries.textures[texID["cross"].InnerText];
      UI.padLock = Dictionaries.textures[texID["padlock"].InnerText];

      Misc.background = Dictionaries.textures[texID["background"].InnerText];
      Misc.title = Dictionaries.textures[texID["title"].InnerText];
      Misc.cursor = Dictionaries.textures[texID["cursor"].InnerText];
      Misc.pixel = Dictionaries.textures[texID["pixel"].InnerText];
      Misc.clearPixel = Dictionaries.textures[texID["cpixel"].InnerText];

      Button.locationNode = Dictionaries.textures[texID["locnode"].InnerText];
      Button.continueButton = Dictionaries.textures[texID["continuebut"].InnerText];
      Button.startButton = Dictionaries.textures[texID["startbut"].InnerText];
      Button.backButton = Dictionaries.textures[texID["backbut"].InnerText];
      Button.backArrow = Dictionaries.textures[texID["backarw"].InnerText];
      Button.newButton = Dictionaries.textures[texID["newbut"].InnerText];
      Button.loadButton = Dictionaries.textures[texID["loadbut"].InnerText];
      Button.deleteButton = Dictionaries.textures[texID["delbut"].InnerText];
      Button.inventoryButton = Dictionaries.textures[texID["invbut"].InnerText];
      Button.closeButton = Dictionaries.textures[texID["closebut"].InnerText];
      Button.playButton = Dictionaries.textures[texID["playbut"].InnerText];
      Button.exitButton = Dictionaries.textures[texID["exitbut"].InnerText];
      Button.optionsButton = Dictionaries.textures[texID["optionsbut"].InnerText];
      Button.lookEye = Dictionaries.textures[texID["lookeye"].InnerText];
      Button.npcButton = Dictionaries.textures[texID["npcbut"].InnerText];
      Button.enterLocationButton = Dictionaries.textures[texID["enterloc"].InnerText];
      Button.exitLocationButton = Dictionaries.textures[texID["exitloc"].InnerText];
      Button.leftLightSide = Dictionaries.textures[texID["lightleftgenbut"].InnerText];
      Button.middleLight = Dictionaries.textures[texID["midlightgenbut"].InnerText];
      Button.rightLightSide = Dictionaries.textures[texID["rightlightgenbut"].InnerText];
      Button.leftDarkSide = Dictionaries.textures[texID["leftdarkgenbut"].InnerText];
      Button.middleDark = Dictionaries.textures[texID["middarkgenbut"].InnerText];
      Button.rightDarkSide = Dictionaries.textures[texID["rightdarkgenbut"].InnerText];
      Button.questsButton = Dictionaries.textures[texID["questsbut"].InnerText];

    }
  }
}
