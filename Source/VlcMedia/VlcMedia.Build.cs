// Copyright 2015 Headcrash Industries LLC. All Rights Reserved.

using System.IO;

namespace UnrealBuildTool.Rules
{
	using System.IO;

	public class VlcMedia : ModuleRules
	{
		public VlcMedia(TargetInfo Target)
		{
			DynamicallyLoadedModuleNames.AddRange(
				new string[] {
					"Media",
					"Settings",
				}
			);

			PrivateDependencyModuleNames.AddRange(
				new string[] {
					"Core",
					"CoreUObject",
					"Projects",
					"RenderCore",
				}
			);

			PrivateIncludePathModuleNames.AddRange(
				new string[] {
					"Media",
					"Settings",
				}
			);

			PrivateIncludePaths.AddRange(
				new string[] {
					"VlcMedia/Private",
					"VlcMedia/Private/Player",
					"VlcMedia/Private/Shared",
					"VlcMedia/Private/Vlc",
				}
			);

			// add VLC libraries
			string BaseDirectory = Path.GetFullPath(Path.Combine(ModuleDirectory, "..", ".."));
			string VlcDirectory = Path.Combine(BaseDirectory, "ThirdParty", "vlc", Target.Platform.ToString());

			if (Target.Platform == UnrealTargetPlatform.Linux)
			{
				VlcDirectory = Path.Combine(VlcDirectory, Target.Architecture);

				RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(VlcDirectory, "libvlc.so.5.5.0")));
				RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(VlcDirectory, "libvlccore.so.8.0")));
				RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(VlcDirectory, "libvlc.so.5")));
				RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(VlcDirectory, "libvlccore.so.8")));
				RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(VlcDirectory, "libvlc.so")));
				RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(VlcDirectory, "libvlccore.so")));
			}
			else if (Target.Platform == UnrealTargetPlatform.Mac)
			{
				RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(VlcDirectory, "libvlc.dylib")));
				RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(VlcDirectory, "libvlc.5.dylib")));
				RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(VlcDirectory, "libvlccore.dylib")));
				RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(VlcDirectory, "libvlccore.8.dylib")));
			}
			else if (Target.Platform == UnrealTargetPlatform.Win32)
			{
				RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(VlcDirectory, "libgcc_s_sjlj-1.dll")));
				RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(VlcDirectory, "libvlc.dll")));
				RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(VlcDirectory, "libvlccore.dll")));
			}
			else if (Target.Platform == UnrealTargetPlatform.Win64)
			{
				RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(VlcDirectory, "libgcc_s_seh-1.dll")));
				RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(VlcDirectory, "libvlc.dll")));
				RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(VlcDirectory, "libvlccore.dll")));
			}

			// add VLC plug-ins
			string PluginDirectory = Path.Combine(VlcDirectory, "plugins");

			if (Directory.Exists(PluginDirectory))
			{
				foreach (string Plugin in Directory.EnumerateFiles(PluginDirectory))
				{
					RuntimeDependencies.Add(new RuntimeDependency(Plugin));
				}
			}
		}
	}
}
