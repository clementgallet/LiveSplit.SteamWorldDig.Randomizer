using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.Reflection;
namespace LiveSplit.SteamWorldDig {
	public class SteamWorldFactory : IComponentFactory {
		public string ComponentName { get { return "SteamWorld Dig Randomizer"; } }
		public string Description { get { return "Randomizer for SteamWorld Dig"; } }
		public ComponentCategory Category { get { return ComponentCategory.Control; } }
		public IComponent Create(LiveSplitState state) { return new SteamWorldComponent(); }
		public string UpdateName { get { return this.ComponentName; } }
		public string UpdateURL { get { return "https://raw.githubusercontent.com/clementgallet/LiveSplit.SteamWorldDig.Randomizer/master/"; } }
		public string XMLURL { get { return this.UpdateURL + "Components/LiveSplit.SteamWorldDig.Randomizer.Updates.xml"; } }
		public Version Version { get { return Version.Parse("1.2"); } }
	}
}