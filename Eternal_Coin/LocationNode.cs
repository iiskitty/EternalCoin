using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal_Coin
{
    public abstract class Node : AnimationManager
    {
        protected List<LocationNode> locNodeConnections;
        protected LocationNode mainLocNode;
        protected LocationNode subLocNode;
        protected string locationFilePath;
        protected Rectangle bounds;
        protected Rectangle playerPort;
        protected Texture2D spriteID;
        protected Vector2 position;
        protected Vector2 truePosition;
        protected Vector2 worldMapPosition;
        protected Vector2 size;
        protected string locName;
        protected string name;
        protected string mainName;
        protected string subName;
        protected string state;
        protected bool searched;

        protected string mainLocNodeName;
        protected string subLocNodeName;
        protected List<string> locNodeConName;

        public Node(Texture2D spriteID, Vector2 position, Vector2 truePosition, Vector2 size, Color colour, bool searched, string name, string mainName, string subName, string state)
            : base(position, colour)
        {
            this.spriteID = spriteID;
            this.position = position;
            this.truePosition = truePosition;
            this.size = size;
            this.searched = searched;
            this.name = name;
            this.mainName = mainName;
            this.subName = subName;
            this.state = state;
            locationFilePath = mainName + "/" + subName + "/" + subName + "Info.xml";
        }

        public Node(string name, string mainName, string subName, string state, Vector2 position, Color colour)
            : base(position, colour)
        {
            this.name = name;
            this.mainName = mainName;
            this.subName = subName;
            this.state = state;
        }

        public abstract void Update(float gameTime);
        public abstract void AddConnection(LocationNode locationNode);
        public abstract void HandleMovement(Vector2 wPos);

        public Rectangle Bounds { get { return bounds; } set { bounds = value; } }
        public Rectangle PlayerPort { get { return playerPort; } set { playerPort = value; } }
        public Texture2D SpriteID { get { return spriteID; } set { spriteID = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }
        public Vector2 Size { get { return size; } set { size = value; } }
        public Vector2 TruePosition { get { return truePosition; } set { truePosition = value; } }
        public Color Colour { get { return colour; } set { colour = value; } }
        public byte ColourA { get { return colour.A; } set { colour.A = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string SubName { get { return subName; } set { subName = value; } }
        public string State { get { return state; } set { state = value; } }
        public string LocName { get { return locName; } set { locName = value; } }
        public string LocatoinFilePath { get { return locationFilePath; } set { locationFilePath = value; } }
        public List<LocationNode> LocNodeConnections { get { return locNodeConnections; } }
        public LocationNode SubLocNode { get { return subLocNode; } set { subLocNode = value; } }
        public LocationNode MainLocNode { get { return mainLocNode; } set { mainLocNode = value; } }
        public bool Searched { get { return searched; } set { searched = value; } }
        public string MainName { get { return mainName; } set { mainName = value; } }
        public string MainLocNodeName { get { return mainLocNodeName; } set { mainLocNodeName = value; } }
        public string SubLocNodeName { get { return subLocNodeName; } set { subLocNodeName = value; } }
        public List<string> LocNodeConName { get { return locNodeConName; } set { locNodeConName = value; } }
    }

    public class LocationNode : Node
    {
        public LocationNode(Texture2D spriteID, Vector2 position, Vector2 truePosition, Vector2 size, Color colour, bool searched, string name, string mainName, string subName, string state)
            : base(spriteID, position, truePosition, size, colour, searched, name, mainName, subName, state)
        {
            AddAnimation(1, 0, 0, "LocationNode", spriteID.Width, spriteID.Height, Vector2.Zero);
            PlayAnimation("LocationNode");
            locNodeConnections = new List<LocationNode>();
            subLocNode = null;
            mainLocNode = null;
            locNodeConName = new List<string>();
        }

        public LocationNode(string name, string mainName, string subName, string state, Vector2 position, Color colour)
            : base(name, mainName, subName, state, position, colour)
        {

        }

        public override void Update(float gameTime)
        {
            Vector2 playerPortPosition = new Vector2(position.X + size.X / 4, position.Y - size.Y / 2);

            bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            playerPort = new Rectangle((int)playerPortPosition.X, (int)playerPortPosition.Y, (int)size.X / 2, (int)size.Y / 2);
        }

        public override void AnimationDone(string animation)
        {
            
        }

        public override void HandleMovement(Vector2 wPos)
        {
            position = new Vector2((wPos.X + truePosition.X), (wPos.Y + truePosition.Y));
        }

        public override void AddConnection(LocationNode locationNode)
        {
            locNodeConnections.Add(locationNode);
        }
    }

    public class Location
    {
        string name;
        string description;
        bool searched;
        bool hasNPC;
        bool hasShop;
        bool hasEnemy;

        public Location(string name, string description, bool searched, bool hasNPC, bool hasShop, bool hasEnemy)
        {
            this.name = name;
            this.description = description;
            this.searched = searched;
            this.hasNPC = hasNPC;
            this.hasShop = hasShop;
            this.hasEnemy = hasEnemy;
        }

        /// <summary>
        /// Cycle through current location and connecting locations, if location have been explored, draw their names.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw strings</param>
        public static void DrawLocationNames(SpriteBatch spriteBatch)
        {
            if (GVar.currentGameState == GVar.GameState.game)
            {
                if (GVar.player.CurrentLocation.Searched)
                {
                    string curLocName = GVar.player.CurrentLocation.Name;
                    Vector2 curLocPos = GVar.player.CurrentLocation.Position;
                    spriteBatch.DrawString(Fonts.lucidaConsole20Bold, curLocName, new Vector2(curLocPos.X - curLocName.Length / 2, curLocPos.Y + 20), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.16f);
                }
                for (int k = 0; k < GVar.player.CurrentLocation.LocNodeConnections.Count; k++)
                {
                    if (GVar.player.CurrentLocation.LocNodeConnections[k].Searched)
                    {
                        string curLocConName = GVar.player.CurrentLocation.LocNodeConnections[k].Name;
                        Vector2 curLocConPos = GVar.player.CurrentLocation.LocNodeConnections[k].Position;
                        spriteBatch.DrawString(Fonts.lucidaConsole20Bold, curLocConName, new Vector2(curLocConPos.X - curLocConName.Length / 2, curLocConPos.Y + 20), Color.FromNonPremultiplied(0, 0, 0, GVar.player.CurrentLocation.LocNodeConnections[k].ColourA), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.16f);
                    }
                }
            }
        }

        /// <summary>
        /// Draws location name and description if the location has been explored.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw strings.</param>
        /// <param name="location">Current location</param>
        public static void DrawLocationInfo(SpriteBatch spriteBatch, Location location)
        {
            foreach (UIElement ui in Lists.uiElements)
            {
                if (ui.SpriteID == Textures.UI.locationInfoUI)
                {
                    if (GVar.location.Searched)
                    {
                        spriteBatch.DrawString(Fonts.lucidaConsole20Bold, location.Name, new Vector2((ui.Position.X + ui.Size.X / 2 - location.Name.Length / 2), (ui.Position.Y)), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                        spriteBatch.DrawString(Fonts.lucidaConsole16Regular, location.Description, new Vector2((ui.Position.X + ui.Size.X / 6), (ui.Position.Y + 30)), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.19f);
                    }
                }
            }
        }

        public string Name { get { return name; } set { name = value; } }
        public string Description { get { return description; } set { description = value; } }
        public bool Searched { get { return searched; } set { searched = value; } }
        public bool HasNPC { get { return hasNPC; } set { hasNPC = value; } }
        public bool HasShop { get { return hasShop; } set { hasShop = value; } }
        public bool HasEnemy { get { return hasEnemy; } set { hasEnemy = value; } }
    }

    
}
