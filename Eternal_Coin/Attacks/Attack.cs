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
    public class AttackAnim
    {
        string id;
        int xPos;
        int yPos;
        int width;
        int height;
        int frames;

        public AttackAnim(int frames, int yPos, int xPos, string id, int width, int height)
        {
            this.frames = frames;
            this.yPos = yPos;
            this.xPos = xPos;
            this.id = id;
            this.width = width;
            this.height = height;
        }

        public string ID { get { return id; } set { id = value; } }
        public int XPos { get { return xPos; } set { xPos = value; } }
        public int YPos { get { return yPos; } set { yPos = value; } }
        public int Width { get { return width; } set { width = value; } }
        public int Height { get { return height; } set { height = value; } }
        public int Frames { get { return frames; } set { frames = value; } }
    }

    public class Attack : AnimationManager
    {
        string id;
        string type;
        string magicProjectile;
        Texture2D anim;
        List<AttackAnim> attackAnims;

        public Attack(string id, string type, Texture2D anim)
        {
            this.id = id;
            this.type = type;
            this.anim = anim;
            attackAnims = new List<AttackAnim>();
        }

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

        public static void TakeAvailableAttacks(List<string> ids)
        {
            for (int i = 0; i < ids.Count; i++)
            {
                Lists.availableAttacksIDs.Remove(GVar.displayPicID + ids[i]);
                Dictionaries.availableAttacks.Remove(GVar.displayPicID + ids[i]);
            }
        }

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

        public static void TakeEnemyAttack(List<string> ids)
        {
            for (int i = 0; i < ids.Count; i ++)
            {
                Lists.enemyAttackIDs.Remove(GVar.eDisplayPicID + ids[i]);
                Dictionaries.enemyAttacks.Remove(GVar.eDisplayPicID + ids[i]);
            }
        }

        public static void LoadEnemyAttacks(ContentManager Content, string edpid)
        {
            XmlDocument attacksDoc = new XmlDocument();
            attacksDoc.Load("./Content/LoadData/LoadAttacks.xml");
            XmlNodeList attacks = attacksDoc.SelectNodes("/attacks/enemyattacks/attack");
            foreach (XmlNode attack in attacks)
            {
                string id = "";
                string type = "";
                Texture2D anim = null;
                    try
                    {
                        id = edpid + attack["id"].InnerText;
                    }
                    catch (Exception e)
                    {
                        GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                    }
                    
                    try
                    {
                        string fileLoc = attack["animfileloc"].InnerText.Replace("DPID", edpid);

                        anim = Content.Load<Texture2D>(fileLoc);
                    }
                    catch (Exception e)
                    {
                        GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                    }

                    try
                    {
                        type = attack["type"].InnerText;
                    }
                    catch (Exception e)
                    {
                        GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                    }

                    try
                    {
                        Attack atk = new Attack(id, type, anim);
                        
                        XmlNodeList animList = attack.SelectNodes("animation");

                        foreach (XmlNode anima in animList)
                        {
                            try
                            {
                                atk.attackAnims.Add(new AttackAnim(Convert.ToInt32(anima["frames"].InnerText), Convert.ToInt32(anima["y"].InnerText), Convert.ToInt32(anima["x"].InnerText), anima["name"].InnerText, Convert.ToInt32(anima["width"].InnerText), Convert.ToInt32(anima["height"].InnerText)));
                            }
                            catch (Exception e)
                            {
                                GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                            }
                        }

                        Dictionaries.attacks.Add(id, atk);
                        Lists.attackIDs.Add(id);
                    }
                    catch (Exception e)
                    {
                        GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                    }
            }
        }

        public static void LoadAttacks(ContentManager Content, string dpid)
        {
            XmlDocument attacksDoc = new XmlDocument();
            attacksDoc.Load("./Content/LoadData/LoadAttacks.xml");
            XmlNodeList attacks = attacksDoc.SelectNodes("/attacks/attack");
            foreach (XmlNode attack in attacks)
            {
                string id = "";
                try
                {
                    id = dpid + attack["id"].InnerText;
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                }
                string type = "";
                try
                {
                    type = attack["type"].InnerText;
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                }
                string magicProj = "";
                try
                {
                    magicProj = attack["magicprojectile"].InnerText;
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                }
                Texture2D anim = null;
                try
                {
                    string fileLoc = attack["animfileloc"].InnerText.Replace("DPID", dpid);

                    anim = Content.Load<Texture2D>(fileLoc);
                }
                catch (Exception e)
                {
                    GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                }

                try
                {
                    Attack atk = null;
                    if (magicProj == "")
                    {
                        atk = new Attack(id, type, anim);
                    }
                    else
                    {
                        atk = new Attack(id, type, magicProj, anim);
                    }
                    XmlNodeList animList = attack.SelectNodes("animation");

                    foreach (XmlNode anima in animList)
                    {
                        try
                        {
                            atk.attackAnims.Add(new AttackAnim(Convert.ToInt32(anima["frames"].InnerText), Convert.ToInt32(anima["y"].InnerText), Convert.ToInt32(anima["x"].InnerText), anima["name"].InnerText, Convert.ToInt32(anima["width"].InnerText), Convert.ToInt32(anima["height"].InnerText)));
                        }
                        catch (Exception e)
                        {
                            GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                        }
                    }
                    Dictionaries.attacks.Add(id, atk);
                    Lists.attackIDs.Add(id);
                }
                catch (Exception e)
                {
                    //GVar.LogDebugInfo("!!!ERROR!!![" + e + "]", 1);
                }
            }
        }

        public static void LoadDefaultAttack()
        {
            foreach (string atkID in Lists.attackIDs)
            {
                if (atkID == "DefaultPunch")
                {
                    Lists.availableAttacksIDs.Add(atkID);
                    Dictionaries.availableAttacks.Add(atkID, Dictionaries.attacks[atkID]);
                }
            }
        }

        public string ID { get { return id; } set { id = value; } }
        public string Type { get { return type; } set { type = value; } }
        public string MagicProjectile { get { return magicProjectile; } set { magicProjectile = value; } }
        public Texture2D Anim { get { return anim; } set { anim = value; } }
        public List<AttackAnim> AttackAnims { get { return attackAnims; } set { attackAnims = value; } }
    }
}
