using System.Collections.Generic;
namespace LiveSplit.SteamWorldDig {
	public class SteamWorldRandomizer
    {
        private SteamWorldMemory mem;
        private static SeedRandom random;
        private List<string> haveItems;
        private readonly int seed;
        private List<Location> locations;
        private List<string> upgrades;
        private List<string> areas;

        public SteamWorldRandomizer(SteamWorldMemory mem, int seed, List<Location> locations)
        {
            random = new SeedRandom(seed);
            this.locations = locations;
            this.seed = seed;
            this.mem = mem;
            haveItems = new List<string>();

            upgrades = mem.GetUpgradeNames();
            areas = mem.GetAreaNames();
        }

        public int Randomize()
        {
            bool ret;

            ret = PermuteItems(upgrades, Location.RandomizeType.Upgrade);
            if (!ret) return -1;

            ret = PermuteItems(areas, Location.RandomizeType.Area);
            if (!ret) return -2;

            ret = CheckValid();
            if (!ret) return 0;

            /* Write into the game */
            foreach (var location in locations)
            {
                if (location.Type == Location.RandomizeType.Upgrade)
                {
                    mem.RandomizeUpgrade(location.Name, location.Grant);
                }
                if (location.Type == Location.RandomizeType.Area)
                {
                    mem.RandomizeArea(location.Name, location.Grant);
                }

            }
            return 1;
        }

        private bool CheckValid()
        {
            haveItems.Clear();

            bool didAdd = false;

            do
            {
                didAdd = false;
                foreach (var location in locations)
                {
                    /* We don't care if we already have the item */
                    if (haveItems.Contains(location.Grant))
                    {
                        continue;
                    }

                    /* Check that we have access to the location */
                    if (location.CanAccess(haveItems))
                    {
                        /* Make a copy of our items and add the granted item, then check if we can escape */
                        List<string> itemsAdded = new List<string>(haveItems);
                        itemsAdded.Add(location.Grant);
                        if (location.CanEscape(itemsAdded))
                        {
                            /* We can successfully grab the item and escape! Add the item to our list */
                            haveItems.Add(location.Grant);
                            didAdd = true;
                        }
                    }
                }
            }
            while (didAdd && !haveItems.Contains("vectron_boss"));

            return haveItems.Contains("vectron_boss");
        }

        private static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private bool PermuteItems(List<string> items, Location.RandomizeType type)
        {
            List<string> randomized_items = new List<string>(items);
            Shuffle(randomized_items);
            int i = 0;
            foreach (var location in locations)
            {
                if (location.Type == type)
                {
                    if (i == randomized_items.Count)
                    {
                        return false;
                    }
                    location.Grant = randomized_items[i];
                    i++;
                }
            }

            if (i != randomized_items.Count)
            {
                return false;
            }

            return true;
        }


    }
}
 