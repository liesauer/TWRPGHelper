using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace TWRPGHelper
{
    internal class Helper
    {
        /// <summary>
        /// 获取魔兽官方对战平台安装目录
        /// </summary>
        /// <returns>返回魔兽的安装目录</returns>
        internal static string GetDZClientPath()
        {
            string[] locations = new string[] { @"SOFTWARE\WOW6432Node\Netease\dzclient", @"SOFTWARE\Netease\dzclient" };
            string path = "";
            foreach (string location in locations)
            {
                try
                {
                    RegistryKey dzclientKey = Registry.LocalMachine.OpenSubKey(location);
                    path = dzclientKey.GetValue("path", "").ToString();
                }
                catch (Exception)
                {
                }
                if (!string.IsNullOrEmpty(path))
                {
                    path = path.Replace("\\Platform.exe", "");
                    break;
                }
            }
            return path;
        }
        /// <summary>
        /// 获取魔兽争霸3目录
        /// </summary>
        /// <returns>war3目录</returns>
        internal static string GetWar3Path()
        {
            string[] locations = new string[] { @"SOFTWARE\WOW6432Node\Blizzard Entertainment\Warcraft III", @"SOFTWARE\Blizzard Entertainment\Warcraft III" };
            string path = "";
            foreach (string location in locations)
            {
                try
                {
                    RegistryKey dzclientKey = Registry.LocalMachine.OpenSubKey(location);
                    path = dzclientKey.GetValue("InstallPath", "").ToString();
                }
                catch (Exception)
                {
                }
                if (!string.IsNullOrEmpty(path))
                {
                    path = path.TrimEnd('\\');
                    break;
                }
            }
            return path;
        }
        /// <summary>
        /// 取中间文本
        /// </summary>
        /// <param name="str">全文本</param>
        /// <param name="leftstr">左边文本</param>
        /// <param name="rightstr">右边文本</param>
        /// <returns></returns>
        internal static string GetMiddleText(string str, string leftstr, string rightstr)
        {
            int i = str.IndexOf(leftstr) + leftstr.Length;
            string temp = str.Substring(i, str.IndexOf(rightstr, i) - i);
            return temp;
        }
    }
}
