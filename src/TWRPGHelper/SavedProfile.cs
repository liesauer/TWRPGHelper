using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWRPGHelper
{
    class SavedProfile
    {
        private string id;
        private string gameVersion;
        private string compatiableVersion;
        private string profession;
        private int level;
        private string exp;
        private List<string> heroItems = new List<string>();
        private List<string> backpackItems = new List<string>();
        private string code;

        public string Id { get => id; set => id = value; }
        public string GameVersion { get => gameVersion; set => gameVersion = value; }
        public string CompatiableVersion { get => compatiableVersion; set => compatiableVersion = value; }
        public string Profession { get => profession; set => profession = value; }
        public int Level { get => level; set => level = value; }
        public string Exp { get => exp; set => exp = value; }
        public int ExpGot {
            get {
                return int.Parse(Exp.Split('/')[0].Trim());
            }
        }
        public int ExpNextLevel {
            get {
                return int.Parse(Exp.Split('/')[1].Trim());
            }
        }
        public string Code { get => code; set => code = value; }
        public List<string> HeroItems { get => heroItems; set => heroItems = value; }
        public List<string> BackpackItems { get => backpackItems; set => backpackItems = value; }

    }
}
