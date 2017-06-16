using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using Microsoft.Xna.Framework.Audio;
using System.Xml;
using System.Text;

namespace Eternal_Coin
{
    /// <summary>
    /// holds information for animations of attacks
    /// </summary>
    public class AttackAnim
    {
        string id;
        int xPos;
        int yPos;
        int width;
        int height;
        int frames;

        /// <summary>
        /// holds information for animations of attacks
        /// </summary>
        /// <param name="frames">how many frames the animation has</param>
        /// <param name="yPos">y position on the spritesheet</param>
        /// <param name="xPos">x position on the spritesheet</param>
        /// <param name="id">ID of the animation</param>
        /// <param name="width">width of the animation</param>
        /// <param name="height">height of the animtion</param>
        public AttackAnim(int frames, int yPos, int xPos, string id, int width, int height)
        {
            this.frames = frames;
            this.yPos = yPos;
            this.xPos = xPos;
            this.id = id;
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// ID of the animation
        /// </summary>
        public string ID { get { return id; } set { id = value; } }
        /// <summary>
        /// X position on the spritesheet
        /// </summary>
        public int XPos { get { return xPos; } set { xPos = value; } }
        /// <summary>
        /// Y position on the spritesheet
        /// </summary>
        public int YPos { get { return yPos; } set { yPos = value; } }
        /// <summary>
        /// width of the animation
        /// </summary>
        public int Width { get { return width; } set { width = value; } }
        /// <summary>
        /// height of the animation
        /// </summary>
        public int Height { get { return height; } set { height = value; } }
        /// <summary>
        /// how many frames the animation has
        /// </summary>
        public int Frames { get { return frames; } set { frames = value; } }
    }

    /// <summary>
    /// an attack of any weapon or magic item
    /// </summary>
    public class Attack : AnimationManager
    {
        string id;
        string type;
        string magicProjectile;
        Texture2D anim;
        List<AttackAnim> attackAnims;

        /// <summary>
        /// an attack of any weapon or magic item
        /// </summary>
        /// <param name="id">ID of an attack</param>
        /// <param name="type">type of an attack</param>
        /// <param name="anim">texture of an animation</param>
        public Attack(string id, string type, Texture2D anim)
        {
            this.id = id;
            this.type = type;
            this.anim = anim;
            attackAnims = new List<AttackAnim>();
        }

        /// <summary>
        /// an attack of any weapon or magic item
        /// </summary>
        /// <param name="id">ID of an attack</param>
        /// <param name="type">type of an attack</param>
        /// <param name="magicProjectile">key of a projectile</param>
        /// <param name="anim">texture of an animation</param>
        public Attack(string id, string type, string magicProjectile, Texture2D anim)
        {
            this.id = id;
            this.type = type;
            this.magicProjectile = magicProjectile;
            this.anim = anim;
            attackAnims = new List<AttackAnim>();
        }

        public override void AnimationDone(string animation)
        {
            
        }

        /// <summary>
        /// Adds an available attack from equipped weapon or magic item
        /// </summary>
        /// <param name="ids">List of attack id's from weapon or magic item</param>
        public static void AddAvailableAttacks(List<string> ids)
        {
            try
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    string id = GVar.displayPicID + ids[i];
                    if (Dictionaries.availableAttacks.Count != Dictionaries.availableAttacks.Count + ids.Count)
                        Dictionaries.availableAttacks.Add(id, Dictionaries.attacks[id]);
                    if (Lists.availableAttacksIDs.Count != Lists.availableAttacksIDs.Count + ids.Count)
                        Lists.availableAttacksIDs.Add(id);
                }
            }
            catch(Exception e)
            {
                GVar.LogDebugInfo("!error![" + e + "]", 1);
            }
        }

        /// <summary>
        /// Takes available attacks from weapon or magic item being unequiped
        /// </summary>
        /// <param name="ids">List of attacks id's from weapon or magic item</param>
        public static void TakeAvailableAttacks(List<string> ids)
        {
            for (int i = 0; i < ids.Count; i++)
            {
                Lists.availableAttacksIDs.Remove(GVar.displayPicID + ids[i]);
                Dictionaries.availableAttacks.Remove(GVar.displayPicID + ids[i]);
            }
        }

        /// <summary>
        /// Adds an available attack from equipped weapon or magic item
        /// </summary>
        /// <param name="ids">List of attack id's from weapon or magic item</param>
        public static void AddEnemyAttack(List<string> ids)
        {
            for (int i = 0; i < ids.Count; i++)
            {
                string id = GVar.eDisplayPicID + ids[i];
                if (Dictionaries.enemyAttacks.Count != Dictionaries.enemyAttacks.Count + ids.Count)
                    Dictionaries.enemyAttacks.Add(id, Dictionaries.attacks[id]);
                if (Lists.enemyAttackIDs.Count != Lists.enemyAttackIDs.Count + ids.Count)
                    Lists.enemyAttackIDs.Add(id);
            }
        }

        /// <summary>
        /// Takes available attack from equipped weapon or magic item
        /// </summary>
        /// <param name="ids">List of attack id's from weapon or magic item</param>
        public static void TakeEnemyAttack(List<string> ids)
        {
            for (int i = 0; i < ids.Count; i ++)
            {
                Lists.enemyAttackIDs.Remove(GVar.eDisplayPicID + ids[i]);
                Dictionaries.enemyAttacks.Remove(GVar.eDisplayPicID + ids[i]);
            }
        }

        /// <summary>
        /// Loads enemy attacks from LoadAttacks.xml
        /// </summary>
        /// <param name="Content">ContentManager</param>
        /// <param name="edpid">ID of display picture</param>
        public static void LoadEnemyAttacks(ContentManager Content, string edpid)
        {
            XmlDocument attacksDoc = new XmlDocument();
            attacksDoc.Load("./Content/LoadData/Data.xml");
            XmlNodeList attacks = attacksDoc.SelectNodes("/data/loadattacks/attacks/enemyattacks/attack");
            foreach (XmlNode attack in attacks)
            {
                string id = "";
                string type = "";
                Texture2D anim = null;
                    try
                    {
                        id = edpid + attack["id"].InnerText; //gets ID of attack
                    }
                    catch (Exception e)
                    {
                        GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                    }
                    
                    try
                    {
                        string fileLoc = attack["animfileloc"].InnerText.Replace("DPID", edpid); //gets location of texture for animation

                        anim = Content.Load<Texture2D>(fileLoc);
                    }
                    catch (Exception e)
                    {
                        GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                    }

                    try
                    {
                        type = attack["type"].InnerText; //gets the type of attack
                    }
                    catch (Exception e)
                    {
                        GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                    }

                    try
                    {
                        Attack atk = new Attack(id, type, anim); //creates the attack
                        
                        XmlNodeList animList = attack.SelectNodes("animation");

                        foreach (XmlNode anima in animList)
                        {
                            try
                            {
                                //adds aniations to the created attack
                                atk.attackAnims.Add(new AttackAnim(Convert.ToInt32(anima["frames"].InnerText), Convert.ToInt32(anima["y"].InnerText), Convert.ToInt32(anima["x"].InnerText), anima["name"].InnerText, Convert.ToInt32(anima["width"].InnerText), Convert.ToInt32(anima["height"].InnerText)));
                            }
                            catch (Exception e)
                            {
                                GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                            }
                        }

                        Dictionaries.attacks.Add(id, atk); //attack is added to a dictionary
                        Lists.attackIDs.Add(id); //ID of attack is added to a list
                    }
                    catch (Exception e)
                    {
                        GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                    }
            }
        }

        /// <summary>
        /// Loaded player attacks
        /// </summary>
        /// <param name="Content">ContentManager</param>
        /// <param name="dpid">ID of display picture</param>
        public static void LoadAttacks(ContentManager Content, string dpid)
        {
            XmlDocument attacksDoc = new XmlDocument();
            attacksDoc.Load("./Content/LoadData/Data.xml");
            XmlNodeList attacks = attacksDoc.SelectNodes("/data/loadattacks/attacks/attack");
            
            foreach (XmlNode attack in attacks)
            {
                string id = "";
                try
                {
                    id = dpid + attack["id"].InnerText; //gets ID of attack
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                }
                string type = "";
                try
                {
                    type = attack["type"].InnerText; //gets type of attack
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                }
                string magicProj = "";
                try
                {
                    magicProj = attack["magicprojectile"].InnerText; //gets projectile of attack
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                }
                Texture2D anim = null;
                try
                {
                    string fileLoc = attack["animfileloc"].InnerText.Replace("DPID", dpid); //gets location of texture for animation

                    anim = Content.Load<Texture2D>(fileLoc);
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                }

                Dictionaries.textures.Add(id, Content.Load<Texture2D>(attack["atkbutspritefileloc"].InnerText)); //loads sprite for button to use attack

                try
                {
                    Attack atk = null;
                    if (magicProj == "")
                    {
                        atk = new Attack(id, type, anim); //if attack has no projectile, this one is used to create attack
                    }
                    else
                    {
                        atk = new Attack(id, type, magicProj, anim); //if attack has a projectile, this one is used to create attack
                    }
                    XmlNodeList animList = attack.SelectNodes("animation");

                    foreach (XmlNode anima in animList)
                    {
                        try
                        {
                            //adds animations to created attack
                            atk.attackAnims.Add(new AttackAnim(Convert.ToInt32(anima["frames"].InnerText), Convert.ToInt32(anima["y"].InnerText), Convert.ToInt32(anima["x"].InnerText), anima["name"].InnerText, Convert.ToInt32(anima["width"].InnerText), Convert.ToInt32(anima["height"].InnerText)));
                        }
                        catch (Exception e)
                        {
                            GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                        }
                    }
                    Dictionaries.attacks.Add(id, atk); //attack is added to a dictionary
                    Lists.attackIDs.Add(id); //ID of attack is added to a list
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                }
            }
        }

        /// <summary>
        /// loads default attack, if there is no attack in availableAttacks, shit breaks
        /// </summary>
        public static void LoadDefaultAttack()
        {
            for (int i = 0; i < Lists.attackIDs.Count; i++)
            {
                if (Lists.attackIDs[i] == "DefaultPunch")
                {
                    Lists.availableAttacksIDs.Add(Lists.attackIDs[i]);
                    Dictionaries.availableAttacks.Add(Lists.attackIDs[i], Dictionaries.attacks[Lists.attackIDs[i]]);
                }
            }
        }

        /// <summary>
        /// ID of attack
        /// </summary>
        public string ID { get { return id; } set { id = value; } }
        /// <summary>
        /// Type of attack
        /// </summary>
        public string Type { get { return type; } set { type = value; } }
        /// <summary>
        /// name/key of projectile
        /// </summary>
        public string MagicProjectile { get { return magicProjectile; } set { magicProjectile = value; } }
        /// <summary>
        /// texture of animation
        /// </summary>
        public Texture2D Anim { get { return anim; } set { anim = value; } }
        /// <summary>
        /// List of attack animations
        /// </summary>
        public List<AttackAnim> AttackAnims { get { return attackAnims; } set { attackAnims = value; } }
    }
}
