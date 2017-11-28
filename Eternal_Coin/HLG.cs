using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Eternal_Coin
{
  /// <summary>
  /// Inherited by Entity and Enemy for sprite drawing and animation.
  /// </summary>
  public abstract class AnimationManager
  {
    protected int frameIndex;
    protected double timeElapsed;
    protected double timeToUpdate;
    protected Color colour;

    /// <summary>
    /// Dictionary that contains all animations
    /// </summary>
    protected Dictionary<string, Rectangle[]> sAnimations = new Dictionary<string, Rectangle[]>();
    /// <summary>
    /// Dictionary that contains all animation offsets
    /// </summary>
    protected Dictionary<string, Vector2> sOffsets = new Dictionary<string, Vector2>();
    protected string currentAnimation;

    public string CurrentAnimation { get { return currentAnimation; } set { currentAnimation = value; } }

    /// <summary>
    /// Our time per frame is equal to 1 divided by frames per second(we are deciding FPS)
    /// </summary>
    public int FPS { set { timeToUpdate = (1f / value); } }

    public AnimationManager() { }

    /// <summary>
    /// Constructor for this class
    /// </summary>
    /// <param name="position"></param>
    public AnimationManager(Vector2 position, Color colour)
    {
      this.colour = colour;
    }

    /// <summary>
    /// Determines when we have to change frames
    /// </summary>
    /// <param name="GameTime">GameTime</param>
    public void Update(GameTime gameTime)
    {
      timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;
      if (timeElapsed > timeToUpdate)
      {
        timeElapsed -= timeToUpdate;
        if (currentAnimation != null && frameIndex < sAnimations[currentAnimation].Length - 1)                  //incriments frameIndex(number of which frame to draw)
        {
          frameIndex++;
        }
        else
        {
          AnimationDone(currentAnimation);
          frameIndex = 0;                                                         //resets frameIndex if at the end of set frames
        }
      }
    }

    /// <summary>
    /// Adds an animation to the AnimatedSprite
    /// </summary>
    /// <param name="frames">number of frames for animation</param>
    /// <param name="yPos">Y position on sprite sheet</param>
    /// <param name="xStartFrame">starting X position on sprite sheet</param>
    /// <param name="name">name of animation</param>
    /// <param name="frameWidth">width of a single frame</param>
    /// <param name="frameHeight">height of a single frame</param>
    /// <param name="offset"></param>
    public void AddAnimation(int frames, int yPos, int xStartFrame, string name, int frameWidth, int frameHeight, Vector2 offset)
    {
      Rectangle[] rectangles = new Rectangle[frames];

      for (int i = 0; i < frames; i++)
      {
        if (frames == 1)
          rectangles[i] = new Rectangle(xStartFrame, yPos, frameWidth, frameHeight);
        else
          rectangles[i] = new Rectangle((i + xStartFrame) * frameWidth, yPos, frameWidth, frameHeight);
      }
      sAnimations.Add(name, rectangles);
      sOffsets.Add(name, offset);
    }

    /// <summary>
    /// Draws the sprite on the screen
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch</param>
    public void Draw(SpriteBatch spriteBatch, Texture2D spriteID, Rectangle bounds, float layer, float rotation, Vector2 origin)
    {
      spriteBatch.Draw(spriteID, bounds, sAnimations[currentAnimation][frameIndex], colour, rotation, origin, SpriteEffects.None, layer);
    }

    /// <summary>
    /// Plays an animation
    /// </summary>
    /// <param name="name">Animation to play</param>
    public void PlayAnimation(string name)
    {
      if (currentAnimation != name)
      {
        currentAnimation = name;
        frameIndex = 0;
      }
    }

    /// <summary>
    /// Method that is called every time an animation finishes
    /// </summary>
    /// <param name="animation">Ended animation</param>
    public abstract void AnimationDone(string animation);
  }

  /// <summary>
  /// An Entity can be anything from a player controlled character to a random rat that roams around for no reason.
  /// Personal Behaviors and/or controls for different Entities are defined in their own class
  /// </summary>
  public abstract class Entity : AnimationManager
  {
    //protected List<LocationNode> currentLocation;
    protected LocationNode currentLocation;

    protected Vector2 position;
    protected Vector2 size;
    protected Texture2D spriteID;
    protected Color[] spriteIDData;
    protected Rectangle bounds;
    protected Vector2 velocity;
    protected float health;
    protected float armor;
    protected float damage;
    protected string name;
    protected string state;
    protected float maxHealth;

    /// <summary>
    /// Override this class for PlayerControlled or friendly characters
    /// </summary>
    /// <param name="spriteID">Texture this entity will use</param>
    /// <param name="position">Position this entity will be drawn to</param>
    /// <param name="size">Size of this entity</param>
    /// <param name="velocity">Velocity or speed of this entity</param>
    /// <param name="colour">Colour of this entity</param>
    /// <param name="health">Health of this entity</param>
    /// <param name="armor">Armor of this entity</param>
    public Entity(Texture2D spriteID, Vector2 position, Vector2 size, string name, string state, Vector2 velocity, Color colour, float health, float armor, float damage)
        : base(position, colour)
    {
      this.spriteID = spriteID;
      this.position = position;
      this.size = size;
      this.name = name;
      this.state = state;
      this.velocity = velocity;
      this.health = health;
      this.armor = armor;
      this.damage = damage;
      bounds = new Rectangle();
      currentLocation = null;
    }

    /// <summary>
    /// Override Update method to update each Entity
    /// </summary>
    /// <param name="gameTime">gameTime.ElapsedGameTime.TotalSeconds</param>
    public abstract void Update(float gameTime);

    /// <summary>
    /// Override this function to handle movement of Entity
    /// </summary>
    /// <param name="position">characters position</param>
    /// <param name="gameTime">(float)gameTime.ElapsedGameTime.TotalSeconds</param>
    public abstract void HandleMovement(Vector2 pos, float gameTime);

    /// <summary>
    /// Current Location of the Player.
    /// </summary>
    public LocationNode CurrentLocation { get { return currentLocation; } set { currentLocation = value; } }
    /// <summary>
    /// size of entity
    /// </summary>
    public Vector2 Size { get { return size; } set { size = value; } }
    /// <summary>
    /// position of entity
    /// </summary>
    public Vector2 Position { get { return position; } set { position = value; } }
    /// <summary>
    /// sprite of entity
    /// </summary>
    public Texture2D SpriteID { get { return spriteID; } set { spriteID = value; } }
    /// <summary>
    /// spriteData used for pixel perfect collision(never used)
    /// </summary>
    public Color[] SpriteIDData { get { return spriteIDData; } set { spriteIDData = value; } }
    /// <summary>
    /// colour of entity
    /// </summary>
    public Color Colour { get { return colour; } set { colour = value; } }
    /// <summary>
    /// Rectangle Bounds of entity used for simple collisions
    /// </summary>
    public Rectangle Bounds { get { return bounds; } set { bounds = value; } }
    /// <summary>
    /// health of entity
    /// </summary>
    public float Health { get { return health; } set { health = value; } }
    /// <summary>
    /// max health of entity(health can not exceed this value)
    /// </summary>
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    /// <summary>
    /// velocity/speed of entity
    /// </summary>
    public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
    /// <summary>
    /// armor of entity
    /// </summary>
    public float Armour { get { return armor; } set { armor = value; } }
    /// <summary>
    /// damage this entity does
    /// </summary>
    public float Damage { get { return damage; } set { damage = value; } }
    /// <summary>
    /// name of entity
    /// </summary>
    public string Name { get { return name; } set { name = value; } }
    /// <summary>
    /// state of entity(dead/alive)
    /// </summary>
    public string State { get { return state; } set { state = value; } }
  }

  /// <summary>
  /// hostile characters
  /// </summary>
  public abstract class Enemy : AnimationManager
  {
    protected Vector2 position;
    protected Vector2 size;
    protected Texture2D spriteID;
    protected Color[] spriteIDData;
    protected Rectangle bounds;
    protected Vector2 velocity;
    protected float health;
    protected float armor;
    protected float damage;
    protected string name;
    protected string state;
    protected int score;

    /// <summary>
    /// overide this class for enemy characters
    /// </summary>
    /// <param name="spriteID">Sprite/Texture for the Enemy</param>
    /// <param name="position">Vector2 position of the Enemy</param>
    /// <param name="size">Vector2 size of the Enemy</param>
    /// <param name="name">string name of the Enemy</param>
    /// <param name="state">sting state of the Enemy eg. "Alive" "Dead"</param>
    /// <param name="velocity">Vector2 velocity how fast this Enemy can move</param>
    /// <param name="colour">Color colour of the Enemy</param>
    /// <param name="health">float health of the Enemy</param>
    /// <param name="armor">float armor of the Enemy</param>
    /// <param name="score">int score for killing/defeating the Enemy</param>
    public Enemy(Texture2D spriteID, Vector2 position, Vector2 size, string name, string state, Vector2 velocity, Color colour, float health, float armor, float damage, int score)
        : base(position, colour)
    {
      this.spriteID = spriteID;
      this.position = position;
      this.size = size;
      this.name = name;
      this.state = state;
      this.velocity = velocity;
      this.damage = damage;
      this.health = health;
      this.armor = armor;
      this.score = score;
      bounds = new Rectangle();
    }

    /// <summary>
    /// Override Update method to update each Enemy
    /// </summary>
    /// <param name="gameTime">Pass in GameTime for smoother movement(doesnt need to be used but has to be passed in)</param>
    public abstract void Update(float gameTime);

    /// <summary>
    /// handle movement of Enemy characters
    /// </summary>
    /// <param name="position">characters position</param>
    /// <param name="gameTime">(float)gameTime.ElapsedGameTime.TotalSeconds</param>
    public abstract void HandleMovement(Vector2 pos, float gameTime);

    /// <summary>
    /// position of enemy
    /// </summary>
    public Vector2 Position { get { return position; } set { position = value; } }
    /// <summary>
    /// velocity/speed of enemy
    /// </summary>
    public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
    /// <summary>
    /// size of enemy
    /// </summary>
    public Vector2 Size { get { return size; } set { size = value; } }
    /// <summary>
    /// score for killing enemy
    /// </summary>
    public int Score { get { return score; } set { score = value; } }
    /// <summary>
    /// sprite of enemy
    /// </summary>
    public Texture2D SpriteID { get { return spriteID; } set { spriteID = value; } }
    /// <summary>
    /// spriteData of enemy(for pixel perfect collision(never used))
    /// </summary>
    public Color[] SpriteIDData { get { return spriteIDData; } set { spriteIDData = value; } }
    /// <summary>
    /// colour of enemy
    /// </summary>
    public Color Colour { get { return colour; } set { colour = value; } }
    /// <summary>
    /// Renctangle Bounds of enemy(for simple collisions)
    /// </summary>
    public Rectangle Bounds { get { return bounds; } set { bounds = value; } }
    /// <summary>
    /// health of enemy
    /// </summary>
    public float Health { get { return health; } set { health = value; } }
    /// <summary>
    /// armor of enemy
    /// </summary>
    public float Armor { get { return armor; } set { armor = value; } }
    /// <summary>
    /// name of enemy
    /// </summary>
    public string Name { get { return name; } set { name = value; } }
    /// <summary>
    /// damage this enemy does
    /// </summary>
    public float Damage { get { return damage; } set { damage = value; } }
    /// <summary>
    /// state of enemy(dead/alive)
    /// </summary>
    public string State { get { return state; } set { state = value; } }
  }

  /// <summary>
  /// An Object can be anything to do with the environment that is interactable or not
  /// </summary>
  public abstract class Object : AnimationManager
  {
    protected Vector2 position;
    protected Vector2 size;
    protected Vector2 direction;
    protected Texture2D spriteID;
    protected Color[] spriteIDData;
    protected Rectangle bounds;
    protected string name;
    protected string state;
    protected float worth;

    /// <summary>
    /// override this class for objects such as collectables, clickables and/or inanimate objects
    /// </summary>
    /// <param name="spriteID">Sprite of Object</param>
    /// <param name="position">Position of Object</param>
    /// <param name="size">Size of Object</param>
    /// <param name="colour">Color of Object</param>
    /// <param name="name">Name of Object</param>
    /// <param name="state">State of Object</param>
    /// <param name="worth">Worth of Object</param>
    public Object(Texture2D spriteID, Vector2 position, Vector2 size, Color colour, string name, string state, float worth)
        : base(position, colour)
    {
      this.spriteID = spriteID;
      this.position = position;
      this.size = size;
      this.name = name;
      this.state = state;
      this.worth = worth;
      bounds = new Rectangle();
    }

    /// <summary>
    /// Override this function to update Objects
    /// </summary>
    /// <param name="gameTime"></param>
    public abstract void Update(float gameTime);

    /// <summary>
    /// Override this function to handle and movement for Objects
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="gameTime"></param>
    public abstract void HandleMovement(Vector2 pos, float gameTime);

    /// <summary>
    /// alpha colour of object(opacity, used for fading)
    /// </summary>
    public byte ColourA { get { return colour.A; } set { colour.A = value; } }
    /// <summary>
    /// direction object can/is moving
    /// </summary>
    public Vector2 Direction { get { return direction; } set { direction = value; } }
    /// <summary>
    /// sprite of object
    /// </summary>
    public Texture2D SpriteID { get { return spriteID; } set { spriteID = value; } }
    /// <summary>
    /// spriteData of object(used for pixel perfect collision(never used))
    /// </summary>
    public Color[] SpriteIDData { get { return spriteIDData; } set { spriteIDData = value; } }
    /// <summary>
    /// size of object
    /// </summary>
    public Vector2 Size { get { return size; } set { size = value; } }
    /// <summary>
    /// position of object
    /// </summary>
    public Vector2 Position { get { return position; } set { position = value; } }
    /// <summary>
    /// colour of object
    /// </summary>
    public Color Colour { get { return colour; } set { colour = value; } }
    /// <summary>
    /// Rectangle Bounds of object(used for simple collisions
    /// </summary>
    public Rectangle Bounds { get { return bounds; } set { bounds = value; } }
    /// <summary>
    /// name of object
    /// </summary>
    public string Name { get { return name; } set { name = value; } }
    /// <summary>
    /// state of object(dead/alive)
    /// </summary>
    public string State { get { return state; } set { state = value; } }
    /// <summary>
    /// object worth(for collecting or destroying object)
    /// </summary>
    public float Worth { get { return worth; } set { worth = value; } }
  }

  /// <summary>
  /// Play a Audio File
  /// </summary>
  public class SoundManager
  {
    /// <summary>
    /// List of SoundEffectInstance
    /// </summary>
    public static List<SoundEffectInstance> sounds = new List<SoundEffectInstance>();


    public static Song music;

    public static void PlaySong(Song song)
    {
      music = song;
      MediaPlayer.Volume = GVar.Volume.Music.volume;
      MediaPlayer.IsRepeating = true;
      //MediaPlayer.Play(music);
    }

    /// <summary>
    /// if SoundEffect is not null, create a new SoundEffectInstance with SoundEffect, set volume, pitch and pan, add instance to List.
    /// if List contains more than 12 SoundEffectInstance, sound will not play.
    /// </summary>
    /// <param name="sound">SoundEffect to be played</param>
    public static void PlaySound(SoundEffect sound)
    {
      if (sound != null)
      {
        SoundEffectInstance soundInstance = sound.CreateInstance(); //new SoundEffectInstance with SoundEffect

        soundInstance.Volume = GVar.Volume.Audio.volume;
        soundInstance.Pitch = GVar.Volume.Audio.pitch;
        soundInstance.Pan = GVar.Volume.Audio.pan;
        soundInstance.IsLooped = false;
        sounds.Add(soundInstance);
        if (sounds.Count < 12)
        {
          soundInstance.Play();
        }
      }
    }

    /// <summary>
    /// checks the list of sounds, if a sound is inactive dispose and delete from the list
    /// this caused me problems in the past with sounds not being disposed of properly when finished playing
    /// </summary>
    public static void CheckSounds()
    {
      for (int i = 0; i < sounds.Count; i++)
      {
        if (sounds[i].State == SoundState.Stopped)
        {
          sounds[i].Dispose();
          sounds.RemoveAt(i);
        }
      }
    }
  }

  /// <summary>
  /// for all your user input needs
  /// Call the Update() function to enable it
  /// </summary>
  public class InputManager
  {
    static float currentScrollWheelValue;
    static float previousScrollWheelValue;
    static MouseState currentMouseState;
    static MouseState previousMouseState;
    static KeyboardState currentKeyboardState;
    static KeyboardState previousKeyboardState;
    static GamePadState[] currentGamePadState = new GamePadState[4];
    static GamePadState[] previousGamePadState = new GamePadState[4];
    static bool[] Connected = new bool[4];
    static bool updateGamePad = true;

    /// <summary>
    /// an array of Keys used for typing player name
    /// NEED TO ADD SYMBOLS 
    /// </summary>
    public static Keys[] keys = new Keys[]
    {
            Keys.Q,
            Keys.W,
            Keys.E,
            Keys.R,
            Keys.T,
            Keys.Y,
            Keys.U,
            Keys.I,
            Keys.O,
            Keys.P,
            Keys.A,
            Keys.S,
            Keys.D,
            Keys.F,
            Keys.G,
            Keys.H,
            Keys.J,
            Keys.K,
            Keys.L,
            Keys.Z,
            Keys.X,
            Keys.C,
            Keys.V,
            Keys.B,
            Keys.N,
            Keys.M,
            Keys.Space,
            Keys.Back,
            Keys.D1,
            Keys.D2,
            Keys.D3,
            Keys.D4,
            Keys.D5,
            Keys.D6,
            Keys.D7,
            Keys.D8,
            Keys.D9,
            Keys.D0
    };

    public enum Stick
    {
      Left, Right
    }

    /// <summary>
    ///Call this in every update method of your game to keep things up to date.
    /// </summary>
    public static void Update()
    {
      //if game window is active(not tabbed out) update keyboard, mouse and scroll wheel states
      if (GVar.windowIsActive)
      {
        previousKeyboardState = currentKeyboardState;
        currentKeyboardState = Keyboard.GetState();
        previousMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();
        previousScrollWheelValue = currentScrollWheelValue;
        currentScrollWheelValue = currentMouseState.ScrollWheelValue;

        try
        {
          //if GamePad is supported GamePad states will be updated
          if (updateGamePad)
          {
            for (int i = 0; i < 4; i++)
            {
              previousGamePadState[i] = currentGamePadState[i];
              currentGamePadState[i] = GamePad.GetState((PlayerIndex)i);
              Connected[i] = GamePad.GetState((PlayerIndex)i).IsConnected;
            }
          }
        }
        catch
        {
          GVar.LogDebugInfo("Xbox GamePad not supported on this machine", 1);
          updateGamePad = false;
        }
      }
    }
    #region Mouse
    #region LeftMouseButton
    /// <summary>
    /// Is the left mouse button being held down
    /// </summary>
    /// <returns></returns>
    public static bool IsLMDown()
    {
      if (currentMouseState.LeftButton == ButtonState.Pressed)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Is the left mouse button not being held down
    /// </summary>
    /// <returns></returns>
    public static bool IsLMReleased()
    {
      if (currentMouseState.LeftButton == ButtonState.Released)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Has the left mouse button been clicked
    /// </summary>
    /// <returns></returns>
    public static bool IsLMPressed()
    {
      if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
    #endregion
    #region RightMouseButton
    /// <summary>
    /// Is the right mouse button being held down
    /// </summary>
    /// <returns></returns>
    public static bool IsRMDown()
    {
      if (currentMouseState.RightButton == ButtonState.Pressed)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Is the right mouse button not being held down
    /// </summary>
    /// <returns></returns>
    public static bool IsRMReleased()
    {
      if (currentMouseState.RightButton == ButtonState.Released)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Has the right mouse buttong been clicked
    /// </summary>
    /// <returns></returns>
    public static bool IsRMPressed()
    {
      if (currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
    #endregion
    #region MiddleMouseButton
    /// <summary>
    /// Is the middle mouse button being held down
    /// </summary>
    /// <returns></returns>
    public static bool IsMMDown()
    {
      if (currentMouseState.MiddleButton == ButtonState.Pressed)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Is the middle mouse button not being held down
    /// </summary>
    /// <returns></returns>
    public static bool IsMMReleased()
    {
      if (currentMouseState.MiddleButton == ButtonState.Released)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Has the middle mouse button been clicked
    /// </summary>
    /// <returns></returns>
    public static bool IsMMPressed()
    {
      if (currentMouseState.MiddleButton == ButtonState.Pressed && previousMouseState.MiddleButton == ButtonState.Released)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
    #endregion
    #region Scroll Wheel
    /// <summary>
    /// Has the scroll wheel been scrolled down
    /// </summary>
    /// <returns></returns>
    public static bool IsScrollWheelDown()
    {
      if (previousScrollWheelValue > currentScrollWheelValue)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Has the scroll wheel been scrolled up
    /// </summary>
    /// <returns></returns>
    public static bool IsScrollWheelUp()
    {
      if (previousScrollWheelValue < currentScrollWheelValue)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
    #endregion
    #endregion
    #region GamePad
    /// <summary>
    /// Checks if the button is being held down.
    /// </summary>
    /// <param name="button">The gamepad button you would like to check.</param>
    /// <returns>Returns a boolean value of rather the button is down or not.</returns>
    public static bool IsButtonDown(PlayerIndex controller, Buttons button)
    {
      return (currentGamePadState[(int)controller].IsButtonDown(button));
    }
    /// <summary>
    /// Checks if the button is up. (Not being held down)
    /// </summary>
    /// <param name="button">The gamepad button you would like to check.</param>
    /// <returns>Returns a boolean value of rather the button is up or not.</returns>
    public static bool IsButtonUp(PlayerIndex controller, Buttons button)
    {
      return (currentGamePadState[(int)controller].IsButtonUp(button));
    }
    /// <summary>
    /// Checks if the button has been pressed and released.
    /// </summary>
    /// <param name="button">The gamepad button you would like to check.</param>
    /// <returns>Returns a boolean value of rather the button has been pressed or not.</returns>
    public static bool IsButtonPressed(PlayerIndex controller, Buttons button)
    {
      return (currentGamePadState[(int)controller].IsButtonDown(button) &&
          previousGamePadState[(int)controller].IsButtonUp(button));
    }
    #endregion
    #region Keyboard
    /// <summary>
    /// Checks if a key is down.
    /// </summary>
    /// <param name="key">The key you would like to check.</param>
    /// <returns>Returns a boolean value of rather the key is down or not.</returns>
    public static bool IsKeyDown(Keys key)
    {
      return (currentKeyboardState.IsKeyDown(key));
    }

    /// <summary>
    /// Checks if a key is up.
    /// </summary>
    /// <param name="key">The key you would like to check.</param>
    /// <returns>Returns a boolean value of rather the key is up or not.</returns>
    public static bool IsKeyUp(Keys key)
    {
      return (currentKeyboardState.IsKeyUp(key));
    }
    /// <summary>
    /// Checks if a key has been pressed and released.
    /// </summary>
    /// <param name="key">The key you would like to check.</param>
    /// <returns>Returns a boolean value of rather the key has been pressed and released.</returns>
    public static bool IsKeyPressed(Keys key)
    {
      return (currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key));
    }
    #endregion

  }

  /// <summary>
  /// keeps mouse position and creates a Rectangle at the mouses position
  /// Call the Update() function to enable it.
  /// </summary>
  public class MouseManager
  {
    public static MouseManager mouse = new MouseManager();

    MouseState mouseState;
    public Rectangle mouseBounds;

    public delegate void InventoryItemClick(ItemSlot item);
    public event InventoryItemClick InventoryItemClicked;

    protected virtual void OnInventoryItemClicked()
    {
      InventoryItemClicked?.Invoke(GVar.mouseHoveredItem);
    }

    public delegate void ButtonClick(Button button);
    public event ButtonClick ButtonClicked;

    protected virtual void OnButtonClicked()
    {
      ButtonClicked?.Invoke(GVar.mouseHoveredButton);
    }

    public void Update(bool isFullScreen)
    {
      mouseState = Mouse.GetState(); //keep mouseState up to date
      mouseBounds = new Rectangle(mouseState.Position.X, mouseState.Position.Y, 2, 2); //keep mouseBounds up to date
                                                                                       //if fullscreen, keep mouse inside the game window
      if (isFullScreen)
      {
        if (mouseState.Position.X > GVar.gameScreenX)
          Mouse.SetPosition((int)GVar.gameScreenX, mouseState.Position.Y);
        if (mouseState.Position.Y > GVar.gameScreenY)
          Mouse.SetPosition(mouseState.Position.X, (int)GVar.gameScreenY);
      }

      if (InputManager.IsLMPressed())
      {
        OnInventoryItemClicked();
        OnButtonClicked();
      }

    }

    /// <summary>
    /// Gets and returns the position of the mouse
    /// </summary>
    /// <returns>Vector2 mousePos(mouse position)</returns>
    public Vector2 GetMousePosition()
    {
      MouseState mouse = Mouse.GetState();
      Vector2 mousePos = new Vector2(mouse.Position.X, mouse.Position.Y);
      return mousePos;
    }
  }

  /// <summary>
  /// automatically used by ParticleGenerator
  /// </summary>
  public class Particle
  {
    Texture2D texture;
    Vector2 position;
    Vector2 size;
    Vector2 velocity;
    Color colour;
    float timeAlive;
    bool hasGravity;
    float maxGravity;
    float gravitySpeed;
    string id;

    /// <summary>
    /// ParticleGenerator creates x amount of particles based on the value that is passed in
    /// </summary>
    /// <param name="texture">texture of the particle</param>
    /// <param name="colour">colour of the particle</param>
    /// <param name="position">position of the particle</param>
    /// <param name="size">sixe of the particle</param>
    /// <param name="velocity">velocity of the particle</param>
    /// <param name="hasGravity">if the particle should fall react to 'gravity' or not</param>
    /// <param name="maxGravity">max speed the particle will fall</param>
    /// <param name="gravitySpeed">how fast the particle will fall to maxGravity</param>
    /// <param name="timeAlive">timer for how long the particle will stay alive(lifespan)</param>
    /// <param name="id">I don't know</param>
    public Particle(Texture2D texture, Color colour, Vector2 position, Vector2 size, Vector2 velocity, bool hasGravity, float maxGravity, float gravitySpeed, float timeAlive, string id)
    {
      this.texture = texture;
      this.colour = colour;
      this.position = position;
      this.size = size;
      this.velocity = velocity;
      this.hasGravity = hasGravity;
      this.maxGravity = maxGravity;
      this.gravitySpeed = gravitySpeed;
      this.timeAlive = timeAlive;
      this.id = id;
    }

    /// <summary>
    /// position of the particle
    /// </summary>
    public Vector2 Position { get { return position; } set { position = value; } }
    /// <summary>
    /// lifespan of the particle
    /// </summary>
    public float TimeAlive { get { return timeAlive; } set { timeAlive = value; } }
    /// <summary>
    /// size of the particle
    /// </summary>
    public Vector2 Size { get { return size; } set { size = value; } }
    /// <summary>
    /// ID of the particle
    /// </summary>
    public string ID { get { return id; } set { id = value; } }

    public void Update(GameTime gameTime)
    {
      float gTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
      position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

      if (hasGravity)
      {
        if (velocity.X < -0.1)
          velocity.X += gravitySpeed * gTime;
        else if (velocity.X > 0.1)
          velocity.X -= gravitySpeed * gTime;

        if (velocity.Y < maxGravity)
          velocity.Y += gravitySpeed * gTime;
      }
    }

    /// <summary>
    /// Draw the particles!
    /// </summary>
    /// <param name="spriteBatch">pass in spriteBatch from Draw function in Game1.cs</param>
    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), null, colour, 0f, Vector2.Zero, SpriteEffects.None, 0.01f);
    }
  }

  /// <summary>
  /// Create an array or particles that will display on the screen for a set amount of time.
  /// </summary>
  public class ParticleGenerator
  {
    Texture2D texture;
    string id;

    private int numoParticles;

    List<Particle> particles = new List<Particle>();

    /// <summary>
    /// Create an "engine" for the particles
    /// Make sure to call Update and Draw to "activate" the particles
    /// </summary>
    /// <param name="texture">The texture the particles will use</param>
    public ParticleGenerator(Texture2D texture)
    {
      this.texture = texture;

    }

    /// <summary>
    /// number of particles
    /// </summary>
    public int NumoParticles { get { return numoParticles; } set { numoParticles = value; } }

    /// <summary>
    /// call to create pretty particles!
    /// </summary>
    /// <param name="numoParticles">Amount of particles</param>
    /// <param name="minTimeAlive">minimum lifespan</param>
    /// <param name="maxTimeAlive">maximum lifespan</param>
    /// <param name="hasGravity">has gravity or not</param>
    /// <param name="maxGravity">max speed the particles will fall</param>
    /// <param name="gravitySpeed">how fast the particles gravity increases to maxGravity</param>
    /// <param name="position">position to spawn particles</param>
    /// <param name="size">size of particles</param>
    /// <param name="minMaxX">new Vector2(minX, maxX); how fast particles will move along x axis</param>
    /// <param name="minMaxY">new Vector2(minY, maxY); how fast particles will move along y axis</param>
    /// <param name="colour">colour of particles</param>
    /// <param name="id">id of particles</param>
    public void CreateParticle(int numoParticles, int minTimeAlive, int maxTimeAlive, bool hasGravity, float maxGravity, float gravitySpeed, Vector2 position, Vector2 size, Vector2 minMaxX, Vector2 minMaxY, Color colour, string id)
    {
      this.id = id;
      int tempMinX = (int)minMaxX.X, tempMaxX = (int)minMaxX.Y, tempMinY = (int)minMaxY.X, tempMaxY = (int)minMaxY.Y;
      float tempTimeAlive;
      Random tempRand = new Random();

      for (int i = 0; i < numoParticles; i++)
      {
        //getting a random time to be alive(and what a time it is!)
        tempTimeAlive = tempRand.Next(minTimeAlive, maxTimeAlive);
        //creating a particle with the passed in values
        particles.Add(new Particle(texture, colour, position, size, new Vector2(tempRand.Next(tempMinX, tempMaxX), tempRand.Next(tempMinY, tempMaxY)), hasGravity, maxGravity, gravitySpeed, tempTimeAlive, id));
      }
    }

    /// <summary>
    /// Update all the particles!
    /// </summary>
    /// <param name="gameTime">gameTime for smoother movement</param>
    public void Update(GameTime gameTime)
    {
      //updating and removing particles based on their timeAlive
      for (int i = 0; i < particles.Count; i++)
      {
        particles[i].Update(gameTime);
        particles[i].TimeAlive -= 30f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (particles[i].TimeAlive <= 0)
        {
          particles.RemoveAt(i);
          i--;
        }
      }
      numoParticles = particles.Count();
    }

    /// <summary>
    /// Draw all the particles!
    /// </summary>
    /// <param name="spriteBatch">spriteBatch for drawing things</param>
    public void Draw(SpriteBatch spriteBatch)
    {
      foreach (Particle p in particles)
      {
        p.Draw(spriteBatch);
      }
    }
  }

  /// <summary>
  /// Logs debug information for analysis in Documents/JSKGames/'game name'/DebugFile...
  /// </summary>
  public static class DebugLog
  {
    private static string debugFile;
    private static string debugFolder;
    private static string debugText;
    private static FileStream stream;
    private static bool runDebugLog;

    /// <summary>
    /// Create the folder for the debugFile to be created in
    /// </summary>
    public static void CreateFileStream()
    {
      debugFile = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "JSKGames/EternalCoin/DebugFile " + Environment.MachineName + " " + Environment.OSVersion + ".jskg");
      debugFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "JSKGames/EternalCoin");
      if (!Directory.Exists(debugFolder))
      {
        Directory.CreateDirectory(debugFolder);
      }
      if (File.Exists(debugFile))
      {
        File.Delete(debugFile);
      }
      stream = new FileStream(debugFile, FileMode.Append);
      stream.Close();
    }

    /// <summary>
    /// Write the string of text to the debugFile folder location and reset the string
    /// </summary>
    /// <param name="text"></param>
    public static void LogDebugText(string text)
    {
      if (runDebugLog)
      {
        File.AppendAllText(debugFile, text);
        debugText = "";
      }
      else
        debugText = "";
    }

    /// <summary>
    /// Add text onto the debugText string
    /// </summary>
    public static string DebugText
    {
      get { return debugText; }
      set
      {
        debugText += value;

        if (debugText.Count() >= 4000)
        {
          LogDebugText(debugText);
        }
      }
    }

    /// <summary>
    /// Set to true to activate the debugText logger
    /// </summary>
    public static bool RunDebugLog
    {
      get { return runDebugLog; }
      set { runDebugLog = value; }
    }
  }

  /// <summary>
  /// this is new and i have forgoten how to use it. come back later.
  /// </summary>
  public class Camera
  {
    protected float zoom;
    protected Matrix transform;
    protected Vector2 position;
    protected float rotation;

    public Camera()
    {
      zoom = 1.0f;
      rotation = 0.0f;
      position = Vector2.Zero;
    }

    public float Zoom
    {
      get { return zoom; }
      set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; }
    }

    public float Rotation
    {
      get { return rotation; }
      set { rotation = value; }
    }

    public Vector2 Position
    {
      get { return position; }
      set { position = value; }
    }

    public Matrix Transform
    {
      get { return transform; }
      set { transform = value; }
    }

    public Matrix GetTransformation(Viewport viewport)
    {
      transform = Matrix.CreateTranslation(new Vector3(position.X, position.Y, 0)) * Matrix.CreateRotationZ(rotation) * Matrix.CreateScale(zoom, zoom, 1) * Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));
      return transform;
    }
  }
}