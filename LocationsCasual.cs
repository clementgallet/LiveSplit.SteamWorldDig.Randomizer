using System.Collections.Generic;
namespace LiveSplit.SteamWorldDig {
	public class LocationsCasual
    {
        public List<Location> Locations { get; set; }
        public LocationsCasual() {
            Locations = new List<Location>
            {
                new Location
                {
                    Name = "archaea_cave_run",
                    Grant = "archaea_cave_run", // randomized
                    Type = Location.RandomizeType.Area,
                },
                new Location
                {
                    Name = "archaea_cave_1",
                    Grant = "archaea_cave_1", // randomized
                    Type = Location.RandomizeType.Area,
                },
                new Location
                {
                    Name = "archaea_cave_chargejump",
                    Grant = "archaea_cave_chargejump", // randomized
                    Type = Location.RandomizeType.Area,
                },
                new Location
                {
                    Name = "archaea_cave_2",
                    Grant = "archaea_cave_2", // randomized
                    Type = Location.RandomizeType.Area,
                },
                new Location
                {
                    Name = "archaea_cave_4",
                    Grant = "archaea_cave_4", // randomized
                    Type = Location.RandomizeType.Area,
                },
                new Location
                {
                    Name = "archaea_cave_drill",
                    Grant = "archaea_cave_drill", // randomized
                    Type = Location.RandomizeType.Area,
                },
                new Location
                {
                    Name = "oldworld_cave_2",
                    Grant = "oldworld_cave_2", // randomized
                    Type = Location.RandomizeType.Area,
                    CanAccess = have => CanBreak(have)
                },
                new Location
                {
                    Name = "oldworld_cave_dynamite",
                    Grant = "oldworld_cave_dynamite", // randomized
                    Type = Location.RandomizeType.Area,
                    CanAccess = have => CanBreak(have)
                },
                new Location
                {
                    Name = "oldworld_cave_3",
                    Grant = "oldworld_cave_3", // randomized
                    Type = Location.RandomizeType.Area,
                    CanAccess = have => CanBreak(have)
                },
                new Location
                {
                    Name = "oldworld_cave_4",
                    Grant = "oldworld_cave_4", // randomized
                    Type = Location.RandomizeType.Area,
                    CanAccess = have => CanDrill(have)
                },
                new Location
                {
                    Name = "oldworld_cave_steampunch",
                    Grant = "oldworld_cave_steampunch", // randomized
                    Type = Location.RandomizeType.Area,
                    CanAccess = have => CanDrill(have)
                },
                new Location
                {
                    Name = "oldworld_cave_falldampeners",
                    Grant = "oldworld_cave_falldampeners", // randomized
                    Type = Location.RandomizeType.Area,
                    CanAccess = have => CanDrill(have)
                },
                new Location
                {
                    Name = "vectron_cave_1",
                    Grant = "vectron_cave_1", // randomized
                    Type = Location.RandomizeType.Area,
                    CanAccess = have => CanDrill(have)
                },
                new Location
                {
                    Name = "vectron_cave_minimap",
                    Grant = "vectron_cave_minimap", // randomized
                    Type = Location.RandomizeType.Area,
                    CanAccess = have => CanDrill(have)
                },
                new Location
                {
                    Name = "vectron_cave_2",
                    Grant = "vectron_cave_2", // randomized
                    Type = Location.RandomizeType.Area,
                    CanAccess = have => CanDrill(have) && have.Contains("vectron_barrier_1")
                },
                new Location
                {
                    Name = "vectron_cave_staticdash",
                    Grant = "vectron_cave_staticdash", // randomized
                    Type = Location.RandomizeType.Area,
                    CanAccess = have => CanDrill(have) && have.Contains("vectron_barrier_1")
                },
                new Location
                {
                    Name = "vectron_cave_4",
                    Grant = "vectron_cave_4", // randomized
                    Type = Location.RandomizeType.Area,
                    CanAccess = have => CanDrill(have) && have.Contains("vectron_barrier_1") && have.Contains("vectron_barrier_2")
                },
                new Location
                {
                    Name = "vectron_cave_generator_1",
                    Grant = "vectron_cave_generator_1", // randomized
                    Type = Location.RandomizeType.Area,
                    CanAccess = have => CanDrill(have) && have.Contains("vectron_barrier_1") && have.Contains("vectron_barrier_2")
                },
                new Location
                {
                    Name = "vectron_boss",
                    Grant = "vectron_boss",
                    CanAccess = have => CanDrill(have) && have.Contains("vectron_barrier_1") && have.Contains("vectron_barrier_2") &&
                        have.Contains("archaea_generator") && have.Contains("oldworld_generator") && have.Contains("vectron_generator") &&
                        have.Contains("enable_charge_jump")
                },
                new Location
                {
                    Name = "enable_run",
                    Grant = "enable_run", // randomized
                    Type = Location.RandomizeType.Upgrade,
                    CanAccess = have => have.Contains("archaea_cave_run"),
                    CanEscape = have => have.Contains("enable_run") || have.Contains("enable_charge_jump") ||
                        have.Contains("enable_jump_double")
                },
                new Location
                {
                    Name = "archaea_cave_generator_1",
                    Grant = "archaea_generator",
                    CanAccess = have => have.Contains("archaea_cave_run") && have.Contains("enable_charge_jump"),
                    CanEscape = have => true
                },
                new Location
                {
                    Name = "enable_charge_jump",
                    Grant = "enable_charge_jump", // randomized
                    Type = Location.RandomizeType.Upgrade,
                    CanAccess = have => have.Contains("archaea_cave_chargejump") && (have.Contains("enable_run") ||
                        have.Contains("enable_charge_jump") || have.Contains("enable_jump_double")),
                    CanEscape = have => (have.Contains("enable_run") && have.Contains("enable_jump_double")) ||
                        have.Contains("enable_charge_jump")
                         
                },
                new Location
                {
                    Name = "drill",
                    Grant = "drill", // randomized
                    Type = Location.RandomizeType.Upgrade,
                    CanAccess = have => have.Contains("archaea_cave_drill") &&
                        (have.Contains("enable_charge_jump") || have.Contains("enable_dynamite")),
                    CanEscape = have => (have.Contains("enable_charge_jump") && have.Contains("drill")) ||
                        have.Contains("enable_dynamite")
                },
                new Location
                {
                    Name = "enable_dynamite",
                    Grant = "enable_dynamite", // randomized
                    Type = Location.RandomizeType.Upgrade,
                    CanAccess = have => have.Contains("oldworld_cave_dynamite"),
                    CanEscape = have => true
                },
                new Location
                {
                    Name = "punch",
                    Grant = "punch", // randomized
                    Type = Location.RandomizeType.Upgrade,
                    CanAccess = have => have.Contains("oldworld_cave_steampunch") && (have.Contains("enable_run") ||
                        have.Contains("enable_charge_jump") || have.Contains("enable_jump_double")),
                    CanEscape = have => have.Contains("enable_charge_jump") || have.Contains("enable_dynamite")
                },
                new Location
                {
                    Name = "fall_dampeners",
                    Grant = "fall_dampeners", // randomized
                    Type = Location.RandomizeType.Upgrade,
                    CanAccess = have => have.Contains("oldworld_cave_falldampeners"),
                    CanEscape = have => true
                },
                new Location
                {
                    Name = "oldworld_cave_generator_1",
                    Grant = "oldworld_generator",
                    CanAccess = have => have.Contains("oldworld_cave_falldampeners") && have.Contains("enable_jump_double"),
                    CanEscape = have => true
                },
                new Location
                {
                    Name = "minimap_resources",
                    Grant = "minimap_resources", // randomized
                    Type = Location.RandomizeType.Upgrade,
                    CanAccess = have => have.Contains("vectron_cave_minimap"),
                    CanEscape = have => true
                },
                new Location
                {
                    Name = "vectron_barrier_1",
                    Grant = "vectron_barrier_1",
                    CanAccess = have => have.Contains("vectron_cave_minimap"),
                    CanEscape = have => have.Contains("enable_charge_jump") || have.Contains("enable_jump_double")
                },
                new Location
                {
                    Name = "enable_jump_double",
                    Grant = "enable_jump_double", // randomized
                    Type = Location.RandomizeType.Upgrade,
                    CanAccess = have => have.Contains("vectron_cave_staticdash"),
                    CanEscape = have => have.Contains("enable_jump_double")
                },
                new Location
                {
                    Name = "vectron_barrier_2",
                    Grant = "vectron_barrier_2",
                    CanAccess = have => have.Contains("vectron_cave_staticdash") &&
                        have.Contains("enable_charge_jump") && have.Contains("enable_jump_double"),
                    CanEscape = have => true
                },
                new Location
                {
                    Name = "vectron_generator",
                    Grant = "vectron_generator",
                    CanAccess = have => have.Contains("vectron_cave_generator_1") &&
                        ((have.Contains("enable_charge_jump") && have.Contains("drill")) ||
                        have.Contains("enable_dynamite")),
                    CanEscape = have => true
                },


            };
        }

        /* Can break some rocks, but not much */
        private bool CanBreak(List<string> have)
        {
            return (have.Contains("drill") && have.Contains("enable_charge_jump")) ||
                        have.Contains("enable_dynamite");
        }

        /* Can break a lot of rocks */
        private bool CanDrill(List<string> have)
        {
            return have.Contains("drill") && have.Contains("enable_charge_jump");
        }
    }
}
 