﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal_Coin
{
  public class WorldMap : AnimationManager
  {
    Texture2D spriteID;
    Vector2 position;
    Vector2 playerPosition;
    Vector2 nodePosition; 
    Vector2 size;
    Rectangle bounds;

    Vector2 mapSpeed;
    Vector2 slowSpeed;

    public WorldMap(Texture2D spriteID, Vector2 position, Vector2 size, Color colour)
        : base(position, colour)
    {
      this.spriteID = spriteID;
      this.position = position;
      this.size = size;
      AddAnimation(1, 0, 0, "worldmap", spriteID.Width, spriteID.Height, Vector2.Zero);
      PlayAnimation("worldmap");
    }

    public void SetMapPosition(Node locationNode)
    {
      if (GVar.isFullScreen)
        position = new Vector2(0 - locationNode.TruePosition.X + GVar.trueScreenX / 2, 0 - locationNode.TruePosition.Y + GVar.trueScreenY / 2);
      else if (!GVar.isFullScreen)
        position = new Vector2(0 - locationNode.TruePosition.X + GVar.gameScreenX / 2, 0 - locationNode.TruePosition.Y + GVar.gameScreenY / 2);
    }

    public void SetMapSpeed(Entity player, Node locationNode)
    {
      playerPosition = new Vector2(player.Position.X, player.Position.Y);
      nodePosition = new Vector2(locationNode.PlayerPort.X, locationNode.PlayerPort.Y);

      //distanceX = 60 * (x / (x + y)) and distanceY = 60 * (y / (x + y))

      mapSpeed.X = ((playerPosition.X + player.Size.X / 2) - (nodePosition.X + locationNode.PlayerPort.Width / 2));

      mapSpeed.Y = ((playerPosition.Y + player.Size.Y) - (nodePosition.Y + locationNode.PlayerPort.Height / 2));

      if (mapSpeed.X < 0 && mapSpeed.Y < 0)
      {
        mapSpeed = new Vector2(-100 * (mapSpeed.X / (mapSpeed.X + mapSpeed.Y)), -100 * (mapSpeed.Y / (mapSpeed.X + mapSpeed.Y)));
        slowSpeed = new Vector2(-10, -10);
      }
      else if (mapSpeed.X > 0 && mapSpeed.Y > 0)
      {
        mapSpeed = new Vector2(100 * (mapSpeed.X / (mapSpeed.X + mapSpeed.Y)), 100 * (mapSpeed.Y / (mapSpeed.X + mapSpeed.Y)));
        slowSpeed = new Vector2(10, 10);
      }
      else if (mapSpeed.X < 0 && mapSpeed.Y > 0)
      {
        mapSpeed = new Vector2(-100 * (mapSpeed.X / (mapSpeed.X + mapSpeed.Y * -1)), 100 * (mapSpeed.Y * -1 / (mapSpeed.X + mapSpeed.Y * -1)));
        slowSpeed = new Vector2(-10, 10);
      }
      else if (mapSpeed.X > 0 && mapSpeed.Y < 0)
      {
        mapSpeed = new Vector2(100 * (mapSpeed.X * -1 / (mapSpeed.X * -1 + mapSpeed.Y)), -100 * (mapSpeed.Y / (mapSpeed.X * -1 + mapSpeed.Y)));
        slowSpeed = new Vector2(10, -10);
      }
    }

    public void Update(float gameTime) => bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

    public static void SelectNewMap(Node location)
    {
      if (location.SubName.Contains("Main"))
        GVar.worldMap.SpriteID = Textures.Misc.worldMap;
      else
        GVar.worldMap.SpriteID = Dictionaries.maps[location.MainName + "Map"];
    }

    public static void ResetMap()
    {
      try
      {
        GVar.worldMap.SpriteID = null;
      }
      catch
      {

      }
    }

    public void MapMovement(float gameTime, Rectangle playerPort)
    {
      if (GVar.player.Bounds.Intersects(playerPort))
      {
        position.X += ((GVar.player.Position.X + GVar.player.Size.X / 2) - (playerPort.X + playerPort.Size.X / 2)) * gameTime;
        position.Y += ((GVar.player.Position.Y + GVar.player.Size.Y / 2) - playerPort.Y) * gameTime;
      }
      else if (!GVar.player.Bounds.Intersects(playerPort))
      {
        position.X += mapSpeed.X * gameTime;
        position.Y += mapSpeed.Y * gameTime;
      }
    }

    public Vector2 MapSpeed { get { return mapSpeed; } set { mapSpeed = value; } }
    public Rectangle Bounds { get { return bounds; } set { bounds = value; } }
    public Texture2D SpriteID { get { return spriteID; } set { spriteID = value; } }
    public Vector2 Position { get { return position; } set { position = value; } }
    public Vector2 Size { get { return size; } set { size = value; } }
    public Vector2 PlayerPosition { get { return playerPosition; } set { playerPosition = value; } }
    public Vector2 NodePosition { get { return nodePosition; } set { nodePosition = value; } }
    public Color Colour { get { return colour; } set { colour = value; } }

    public override void AnimationDone(string animation) { }
  }
}
