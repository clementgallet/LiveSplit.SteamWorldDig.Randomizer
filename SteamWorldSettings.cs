using System;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Text;
using System.Collections.Generic;
namespace LiveSplit.SteamWorldDig {
	public partial class SteamWorldSettings : UserControl {
        private SteamWorldMemory mem;
        private bool hasLog = true;
        private static string LOGFILE = "_SteamWorld.log";

        public SteamWorldSettings() {
			InitializeComponent();

            mem = new SteamWorldMemory();
            listDifficulty.SelectedItem = "Speedrunner";
        }

        private void BtnRandomize_Click(object sender, EventArgs e)
        {
            btnRandomize.Text = "Randomizing...";
            btnRandomize.Enabled = false;
            this.UseWaitCursor = true;

            // Store all information about items and areas from the game
            if (mem.HookProcess())
            {
                textBoxInfo.Text = "";

                /* Generate a new seed if blank */
                if (string.IsNullOrWhiteSpace(textSeed.Text))
                {
                    SetSeedBasedOnDifficulty();
                }

                StringBuilder sb = new StringBuilder();

                string difficulty = GetRandomizerDifficulty();

                sb.Append("Selecting difficulty is ").AppendLine(difficulty);
                textBoxInfo.Text = sb.ToString();

                string seed = GetSeed();
                if (string.IsNullOrWhiteSpace(seed))
                {
                    btnRandomize.Text = "Randomize";
                    btnRandomize.Enabled = false;
                    this.UseWaitCursor = false;
                    return;
                }

                int parsedSeed;
                if (!int.TryParse(seed, out parsedSeed))
                {
                    MessageBox.Show("Seed must be numeric or blank.", "Seed Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sb.AppendLine("Seed must be numeric or blank.");
                    textBoxInfo.Text = sb.ToString();
                    btnRandomize.Text = "Randomize";
                    btnRandomize.Enabled = false;
                    this.UseWaitCursor = false;
                    return;
                }

                sb.Append("Seed is ").AppendLine(seed);
                textBoxInfo.Text = sb.ToString();

                sb.AppendLine("Collecting upgrades...");
                textBoxInfo.Text = sb.ToString();
                textBoxInfo.Update();

                mem.SaveAllUpgrades();

                sb.Append("Found: ").AppendLine(mem.PrintAllUpgrades());
                sb.AppendLine("Collecting cave entries...");
                textBoxInfo.Text = sb.ToString();
                textBoxInfo.Update();

                mem.SaveAllAreaEntries();

                sb.Append("Found: ").AppendLine(mem.PrintAllAreaEntries());
                sb.AppendLine("Collecting cave exits...");
                textBoxInfo.Text = sb.ToString();
                textBoxInfo.Update();

                mem.SaveAllAreaExits();

                sb.Append("Found: ").AppendLine(mem.PrintAllAreaExits());
                textBoxInfo.Text = sb.ToString();
                textBoxInfo.Update();

                List<Location> locations;
                if (difficulty == "Speedrunner")
                {
                    locations = new LocationsSpeedrunner().Locations;
                }
                else
                {
                    locations = new LocationsCasual().Locations;
                }

                SteamWorldRandomizer randomizer = new SteamWorldRandomizer(mem, parsedSeed, locations);

                sb.Append("Starting building a randomizer");
                textBoxInfo.Text = sb.ToString();
                textBoxInfo.Update();

                int ret = 0;
                for (int i=0; i<1000; i++)
                {
                    ret = randomizer.Randomize();
                    if (ret == -1)
                    {
                        sb.AppendLine("").AppendLine("Error occured: mismatch number of upgrades");
                        textBoxInfo.Text = sb.ToString();
                        break;
                    }
                    if (ret == -2)
                    {
                        sb.AppendLine("").AppendLine("Error occured: mismatch number of areas");
                        textBoxInfo.Text = sb.ToString();
                        break;
                    }
                    if (ret == 1)
                    {
                        sb.AppendLine("").AppendLine("Successfully found a correct case!");
                        textBoxInfo.Text = sb.ToString();
                        break;
                    }
                    sb.Append(".");
                    textBoxInfo.Text = sb.ToString();
                    textBoxInfo.Update();
                }

                if (ret == 0)
                {
                    sb.AppendLine("").AppendLine("Could not find any correct case...");
                    textBoxInfo.Text = sb.ToString();
                }
            }

            btnRandomize.Text = "Randomize";
            btnRandomize.Enabled = false;
            this.UseWaitCursor = false;
        }

        private void SetSeedBasedOnDifficulty()
        {
            switch (listDifficulty.SelectedItem.ToString())
            {
                case "Casual":
                    textSeed.Text = string.Format("C{0:0000000}", (new SeedRandom()).Next(10000000));
                    break;
                default:
                    textSeed.Text = string.Format("S{0:0000000}", (new SeedRandom()).Next(10000000));
                    break;
            }
        }

        private string GetRandomizerDifficulty()
        {
            string difficulty = "Speedrunner";

            if (textSeed.Text.ToUpper().Contains("C"))
            {
                difficulty = "Casual";
            }
            else if (textSeed.Text.ToUpper().Contains("S"))
            {
                difficulty = "Speedrunner";
            }

            listDifficulty.SelectedItem = difficulty;

            return difficulty;
        }

        private string GetSeed()
        {
            if (textSeed.Text.ToUpper().Contains("C"))
            {
                return textSeed.Text.ToUpper().Replace("C", "");
            }
            else if (textSeed.Text.ToUpper().Contains("S"))
            {
                return textSeed.Text.ToUpper().Replace("S", "");
            }
            MessageBox.Show("The seed string is unrecognized.", "Seed Difficulty", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return "";
        }

        private void WriteLog(string data)
        {
            if (hasLog || !Console.IsOutputRedirected)
            {
                if (Console.IsOutputRedirected)
                {
                    using (StreamWriter wr = new StreamWriter(LOGFILE, true))
                    {
                        wr.WriteLine(data);
                    }
                }
                else
                {
                    Console.WriteLine(data);
                }
            }
        }


        public XmlNode UpdateSettings(XmlDocument document)
        {
            XmlElement xmlSettings = document.CreateElement("Settings");
            return xmlSettings;
        }
        //private void SetSetting(XmlDocument document, XmlElement settings, bool val, string name)
        //{
        //    XmlElement xmlOption = document.CreateElement(name);
        //    xmlOption.InnerText = val.ToString();
        //    settings.AppendChild(xmlOption);
        //}
        public void SetSettings(XmlNode settings)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        //private bool GetSetting(XmlNode settings, string name, bool defaultVal = false)
        //{
        //    XmlNode option = settings.SelectSingleNode(name);
        //    if (option != null && option.InnerText != "")
        //    {
        //        return bool.Parse(option.InnerText);
        //    }
        //    return defaultVal;
        //}
    }
}
 