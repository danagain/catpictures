using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace Wallpaper1
{
    class Program
    {
        //Setting the system parameters
        const int SET_DESKTOP_BACKGROUND = 20;
        const int UPDATE_INI_FILE = 1;
        const int SEND_WINDOWS_INI_CHANGE = 2;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]//importing dynamic linked library
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public enum PicturePosition
        {
            Tile, Center, Stretch, Fit, Fill
        }

        /// <summary>
        /// Sets a given image file into desired layout and then updates the system
        /// with the systemParametersInfo call
        /// </summary>
        /// <param name="directroy">Given directory of file on hard disk</param>
        /// <param name="style">Layout of the image</param>
        public static void setImage(string directroy, PicturePosition style)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            switch (style)
            {
                case PicturePosition.Tile:
                    key.SetValue(@"PicturePosition", "0");
                    key.SetValue(@"TileWallpaper", "1");
                    break;
                case PicturePosition.Center:
                    key.SetValue(@"PicturePosition", "0");
                    key.SetValue(@"TileWallpaper", "0");
                    break;
                case PicturePosition.Stretch:
                    key.SetValue(@"PicturePosition", "2");
                    key.SetValue(@"TileWallpaper", "0");
                    break;
                case PicturePosition.Fit:
                    key.SetValue(@"PicturePosition", "6");
                    key.SetValue(@"TileWallpaper", "0");
                    break;
                case PicturePosition.Fill:
                    key.SetValue(@"PicturePosition", "10");
                    key.SetValue(@"TileWallpaper", "0");
                    break;
            }
            key.Close();
            SystemParametersInfo(SET_DESKTOP_BACKGROUND, 0, directroy, UPDATE_INI_FILE | SEND_WINDOWS_INI_CHANGE);
        }

        /// <summary>
        /// Creates a directory in MyPictures called Wallpapers
        /// </summary>
        static void createDirectory()
        {
            //create a directory if necessary to store the image in my pictures
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "/Wallpapers/";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        /// <summary>
        /// Uses RegEx and RNG to grab a random image from
        /// http://octodex.github.com
        /// </summary>
        /// <returns>Random URL address</returns>
        static string randomUrl()
        {
            int randomNumber = 0;
            List<string> urls = new List<string>();
            using (WebClient client = new WebClient())
            {
                // client.DownloadFile("https://octodex.github.com/", @"C:\localfile.html");
                string htmlCode = client.DownloadString("https://octodex.github.com/");
                foreach (Match match in Regex.Matches(htmlCode, @"/images/(.*).jpg"))
                {
                    urls.Add(match.Groups[1].Value);
                    randomNumber++;
                }
            }
            Random rnd = new Random();
            randomNumber = randomNumber - rnd.Next(0, randomNumber);
            return urls[randomNumber];
        }

        /// <summary>
        /// Downloads the image from the random URL, stores it and then calls the setBack method
        /// to impliment wallpaper change
        /// </summary>
        /// <param name="randomURL">randomURL passed from randomURL() method</param>
        static void setWallpaper(string randomURL)
        {
            randomURL = string.Concat(randomURL, ".jpg");
            int fileCount = 0;
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "/Wallpapers/";
            //check how many files are in the folder 
            string directory = Path.Combine(dir, string.Format("Background{0}.jpg", fileCount));
            while (System.IO.File.Exists(directory))
            {
                fileCount++;
                directory = Path.Combine(dir, string.Format("Background{0}.jpg", fileCount));
            }
            
            //Open a webClient, download file to the created directory, assign file name
            Uri baseUri = new Uri("http://octodex.github.com/images/");
            Uri myUri = new Uri(baseUri, randomURL);
            string myUriStr = myUri.ToString();
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(myUri, directory);
            }
            setImage(directory, PicturePosition.Fill);
        }

        static void Main(string[] args)
        {
            createDirectory();
            string rURL = randomUrl();
            setWallpaper(rURL);
        }
    }


}