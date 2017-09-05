using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using LiveSplit.Memory;
namespace LiveSplit.SteamWorldDig {
	public partial class SteamWorldMemory {
		public Process Program { get; set; }
		public bool IsHooked { get; set; } = false;
		private DateTime lastHooked;
        private static Dictionary<string, byte[]> upgradeSegments = new Dictionary<string, byte[]>();
        private static Dictionary<string, IntPtr> upgradePointers = new Dictionary<string, IntPtr>();
        private static Dictionary<string, byte[]> areaSegments = new Dictionary<string, byte[]>();
        private static Dictionary<string, IntPtr> areaPointers = new Dictionary<string, IntPtr>();
        private static Dictionary<string, byte[]> exitSegments = new Dictionary<string, byte[]>();
        private static Dictionary<string, IntPtr> exitPointers = new Dictionary<string, IntPtr>();

        public SteamWorldMemory() {
			lastHooked = DateTime.MinValue;
		}

        public List<IntPtr> FindAllUpgrades()
        {
            // First locate all occurences of "upgrade_podium"
            List<IntPtr> upgrades = Program.FindAllSignatures("upgrade_podium");

            // Remove the ones that are not related to a level upgrade
            for (int i = upgrades.Count - 1; i >= 0; i--)
            {
                int type = Program.Read<int>(upgrades[i] + 0x30);
                int size = Program.Read<int>(upgrades[i] + 0x2c);

                if ((size == 0) || (type != 0x0F && type != 0x1F))
                {
                    upgrades.RemoveAt(i);
                }
            }
            return upgrades;
        }

        public void SaveAllUpgrades()
        {
            List<IntPtr> upgrade_ptrs = FindAllUpgrades();
            foreach (IntPtr ptr in upgrade_ptrs)
            {
                byte[] segment = Program.Read(ptr + 0x1C, 0x18);
                string key;
                if (Program.Read<int>(ptr + 0x2C) < 0x10)
                {
                    key = Program.ReadAscii(ptr + 0x1C);
                }
                else
                {
                    key = Program.ReadAscii((IntPtr)Program.Read<uint>(ptr + 0x1C));
                }
                upgradeSegments[key] = segment;
                upgradePointers[key] = ptr + 0x1C;
            }
        }

        public string PrintAllUpgrades()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var ptr in upgradePointers)
            {
                sb.Append(ptr.Key).Append(',');
            }
            if (sb.Length > 0)
            {
                sb.Length--;
            }
            return sb.ToString();
        }

        public List<string> GetUpgradeNames()
        {
            List<string> upgrades = new List<string>(upgradePointers.Keys);
            upgrades.Sort();
            return upgrades;
        }

        public void RandomizeUpgrade(string oldUpgrade, string newUpgrade)
        {
            Program.Write(upgradePointers[oldUpgrade], upgradeSegments[newUpgrade]);
        }

        public List<IntPtr> FindAllAreaEntries()
        {
            // First locate all occurences of "[start]"
            List<IntPtr> areas = Program.FindAllSignatures("[start]");

            // Remove the ones that are not a real area
            for (int i = areas.Count - 1; i >= 0; i--)
            {
                int type = Program.Read<int>(areas[i] - 0x08);
                uint size = Program.Read<uint>(areas[i] - 0x0c);

                if ((size == 0 || size > 0x80) || (type != 0x0F && type != 0x1F))
                {
                    areas.RemoveAt(i);
                }
            }
            return areas;
        }

        public void SaveAllAreaEntries()
        {
            List<IntPtr> areas_ptrs = FindAllAreaEntries();
            foreach (IntPtr ptr in areas_ptrs)
            {
                byte[] segment = Program.Read(ptr - 0x1C, 0x18);
                string key;
                if (Program.Read<int>(ptr - 0x0c) < 0x10)
                {
                    key = Program.ReadAscii(ptr - 0x1C);
                }
                else
                {
                    key = Program.ReadAscii((IntPtr)Program.Read<uint>(ptr - 0x1C));
                }

                // Don't randomize the boss room and the generators
                if (!key.Contains("boss") && key != "archaea_cave_generator_1" &&
                    key != "oldworld_cave_generator_1" && key != "oldworld")
                {
                    areaSegments[key] = segment;
                    areaPointers[key] = ptr - 0x1C;
                }
            }
        }

        public string PrintAllAreaEntries()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var ptr in areaPointers)
            {
                sb.Append(ptr.Key).Append(',');
            }
            if (sb.Length > 0)
            {
                sb.Length--;
            }
            return sb.ToString();
        }

        public List<string> GetAreaNames()
        {
            List<string> areas = new List<string>(areaPointers.Keys);
            areas.Sort();
            return areas;
        }

        public List<IntPtr> FindAllAreaExits()
        {
            // First locate all occurences of "[start]"
            List<IntPtr> areas = Program.FindAllSignatures("PlayerExit1");
            areas.AddRange(Program.FindAllSignatures("PlayerExit2"));

            // Remove the ones that are not a real exits
            for (int i = areas.Count - 1; i >= 0; i--)
            {
                int type = Program.Read<int>(areas[i] + 0x54);
                uint size = Program.Read<uint>(areas[i] + 0x50);

                if ((size == 0 || size > 0x80) || (type != 0x0F && type != 0x1F && type != 0x2F))
                {
                    areas.RemoveAt(i);
                }
            }
            return areas;
        }

        public void SaveAllAreaExits()
        {
            List<IntPtr> areas_ptrs = FindAllAreaExits();
            foreach (IntPtr ptr in areas_ptrs)
            {
                byte[] segment = Program.Read(ptr + 0x24, 0x34);
                string key;
                if (Program.Read<int>(ptr + 0x50) < 0x10)
                {
                    key = Program.ReadAscii(ptr + 0x40);
                }
                else
                {
                    key = Program.ReadAscii((IntPtr)Program.Read<uint>(ptr + 0x40));
                }

                // Filter keys that are not "from_xxx" where xxx is an area entry
                foreach (var entryptr in areaPointers)
                {
                    if (key == ("from_" + entryptr.Key))
                    {
                        exitSegments[entryptr.Key] = segment;
                        exitPointers[entryptr.Key] = ptr + 0x24;
                        break;
                    }
                }
            }
        }

        public string PrintAllAreaExits()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var ptr in exitPointers)
            {
                sb.Append(ptr.Key).Append(',');
            }
            if (sb.Length > 0)
            {
                sb.Length--;
            }
            return sb.ToString();
        }

        public void RandomizeArea(string oldArea, string newArea)
        {
            Program.Write(areaPointers[oldArea], areaSegments[newArea]);
            Program.Write(exitPointers[newArea], exitSegments[oldArea]);
            //Program.Write(exitPointers[oldArea], exitSegments[newArea]);
        }

        public bool HookProcess() {
			if ((Program == null || Program.HasExited) && DateTime.Now > lastHooked.AddSeconds(1)) {
				lastHooked = DateTime.Now;
				Process[] processes = Process.GetProcessesByName("SteamWorldDig");
				Program = processes.Length == 0 ? null : processes[0];
				IsHooked = true;
			}

			if (Program == null || Program.HasExited) {
				IsHooked = false;
			}

			return IsHooked;
		}
		public void Dispose() {
			if (Program != null) {
				Program.Dispose();
			}
		}
	}
}