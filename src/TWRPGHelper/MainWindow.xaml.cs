using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TWRPGHelper
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> contents = new List<string>();
        private List<FileInfo> fileInfos = new List<FileInfo>();
        private List<SavedProfile> profiles = new List<SavedProfile>();


        public MainWindow()
        {
            InitializeComponent();
            string path = Helper.GetDZClientPath();
            string war3Path = Helper.GetWar3Path();

            foreach (FileInfo fileInfo in new DirectoryInfo(war3Path + @"\\TWRPG").GetFiles("*.txt"))
            {
                fileInfos.Add(fileInfo);
            }

            fileInfos.Sort(new FileComparer());

            foreach (FileInfo fileInfo in fileInfos)
            {
                contents.Add(System.IO.Path.GetFileNameWithoutExtension(fileInfo.Name));
                SavedProfile profile = new SavedProfile() { Exp = "0/0" };
                string text = File.ReadAllText(fileInfo.FullName);
                Match res = Regex.Match(text, @"游戏ID: (?<id>.*?)"".*?游戏版本: (?<gameVersion>.*?)"".*?兼容版本: (?<compatiableVersion>.*?)"".*?职业: (?<profession>.*?)"".*?等级: (?<level>.*?)"".*?经验值: (?<exp>.*?)"".*?-load (?<code>.*?)""", RegexOptions.Singleline);
                if (res.Success)
                {
                    profile.Id = res.Groups["id"].Value;
                    profile.GameVersion = res.Groups["gameVersion"].Value;
                    profile.CompatiableVersion = res.Groups["compatiableVersion"].Value;
                    profile.Profession = res.Groups["profession"].Value;
                    profile.Level = int.Parse(res.Groups["level"].Value);
                    profile.Exp = res.Groups["exp"].Value;
                    profile.Code = res.Groups["code"].Value;
                }
                MatchCollection matches = Regex.Matches(text, @"物品 (.*?)""", RegexOptions.Multiline);
                int index = -1;
                bool changed = false;
                foreach (Match match in matches)
                {
                    string[] parts= match.Groups[1].Value.Split('.');
                    int i = int.Parse(parts[0].Trim());
                    string name = parts[1].Trim();

                    if (!changed && i < index)
                    {
                        changed = true;
                    }
                    index = i;
                    if (changed)
                    {
                        profile.BackpackItems.Add(name);
                    }
                    else
                    {
                        profile.HeroItems.Add(name);
                    }
                }

                profiles.Add(profile);
            }

            list.ItemsSource = contents;
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            SavedProfile profile = profiles[listBox.SelectedIndex];
            content.Text = String.Format("ID：{0}\n游戏版本：{1}\n兼容版本：{2}\n职业：{3}\n等级：{4}\n经验：{5}\n",profile.Id,profile.GameVersion,profile.CompatiableVersion,profile.Profession,profile.Level,profile.Exp);
            foreach (string itemName in profile.HeroItems.Concat(profile.BackpackItems).ToList<string>())
            {
                content.Text += String.Format("物品：{0}\n",itemName);
            }
        }

        private void list_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            Clipboard.SetText("-load " + profiles[listBox.SelectedIndex].Code);
        }
    }
}
