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
  public class Delete
  {
    public static void ClearFolder(string folderName)
    {
      DirectoryInfo dirInfo = new DirectoryInfo(folderName);

      foreach (FileInfo fi in dirInfo.GetFiles())
      {
        try
        {
          fi.Delete();
        }
        catch
        {
          ClearFolder(folderName);
        }
      }

      foreach (DirectoryInfo di in dirInfo.GetDirectories())
      {
        ClearFolder(di.FullName);
        try
        {
          di.Delete();
        }
        catch
        {
          ClearFolder(di.FullName);
        }
      }
    }

    public static void DeleteGame(string saveFileLocation, string locationFilesLocation)
    {
      if (File.Exists(saveFileLocation))
      {
        File.Delete(saveFileLocation);
      }
      ClearFolder(locationFilesLocation);
      try
      {
        Directory.Delete(locationFilesLocation);
      }
      catch
      {
        try
        {
          Directory.Delete(locationFilesLocation);
        }
        catch
        {

        }
      }
    }
  }
}
