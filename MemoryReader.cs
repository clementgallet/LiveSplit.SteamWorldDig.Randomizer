using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
namespace LiveSplit.Memory {
	public static class MemoryReader {
		private static Dictionary<int, Module64[]> ModuleCache = new Dictionary<int, Module64[]>();
		public static T Read<T>(this Process targetProcess, IntPtr address, params int[] offsets) where T : struct {
			if (targetProcess == null || targetProcess.HasExited || address == IntPtr.Zero) { return default(T); }

			int last = OffsetAddress(targetProcess, ref address, offsets);

			Type type = typeof(T);
			type = (type.IsEnum ? Enum.GetUnderlyingType(type) : type);

			int count = (type == typeof(bool)) ? 1 : Marshal.SizeOf(type);
			byte[] buffer = Read(targetProcess, address + last, count);

			object obj = ResolveToType(buffer, type);
			return (T)((object)obj);
		}
		private static object ResolveToType(byte[] bytes, Type type) {
			if (type == typeof(int)) {
				return BitConverter.ToInt32(bytes, 0);
			} else if (type == typeof(uint)) {
				return BitConverter.ToUInt32(bytes, 0);
			} else if (type == typeof(float)) {
				return BitConverter.ToSingle(bytes, 0);
			} else if (type == typeof(double)) {
				return BitConverter.ToDouble(bytes, 0);
			} else if (type == typeof(byte)) {
				return bytes[0];
			} else if (type == typeof(bool)) {
				return bytes != null && bytes[0] > 0;
			} else if (type == typeof(short)) {
				return BitConverter.ToInt16(bytes, 0);
			} else if (type == typeof(ushort)) {
				return BitConverter.ToUInt16(bytes, 0);
			} else if (type == typeof(long)) {
				return BitConverter.ToInt64(bytes, 0);
			} else if (type == typeof(ulong)) {
				return BitConverter.ToUInt64(bytes, 0);
			} else {
				GCHandle gCHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
				try {
					return Marshal.PtrToStructure(gCHandle.AddrOfPinnedObject(), type);
				} finally {
					gCHandle.Free();
				}
			}
		}
		public static byte[] Read(this Process targetProcess, IntPtr address, int numBytes) {
			byte[] buffer = new byte[numBytes];
			if (targetProcess == null || targetProcess.HasExited || address == IntPtr.Zero) { return buffer; }

			int bytesRead;
			WinAPI.ReadProcessMemory(targetProcess.Handle, address, buffer, numBytes, out bytesRead);
			return buffer;
		}
		public static string Read(this Process targetProcess, IntPtr address) {
			if (targetProcess == null || targetProcess.HasExited || address == IntPtr.Zero) { return string.Empty; }

			int length = Read<int>(targetProcess, address, 0x8);
			return Encoding.Unicode.GetString(Read(targetProcess, address + 0xc, 2 * length));
		}
		public static string ReadAscii(this Process targetProcess, IntPtr address) {
			if (targetProcess == null || targetProcess.HasExited || address == IntPtr.Zero) { return string.Empty; }

			StringBuilder sb = new StringBuilder();
			byte[] data = new byte[128];
			int bytesRead;
			int offset = 0;
			bool invalid = false;
			do {
				WinAPI.ReadProcessMemory(targetProcess.Handle, address + offset, data, 128, out bytesRead);
				int i = 0;
				while (i < bytesRead) {
					byte d = data[i++];
					if (d == 0) {
						i--;
						break;
					} else if (d > 127) {
						invalid = true;
						break;
					}
				}
				if (i > 0) {
					sb.Append(Encoding.ASCII.GetString(data, 0, i));
				}
				if (i < bytesRead || invalid) {
					break;
				}
				offset += 128;
			} while (bytesRead > 0);

			return invalid ? string.Empty : sb.ToString();
		}
		public static void Write(this Process targetProcess, IntPtr address, int value, params int[] offsets) {
			if (targetProcess == null || targetProcess.HasExited) { return; }

			int last = OffsetAddress(targetProcess, ref address, offsets);
			byte[] buffer = BitConverter.GetBytes(value);
			int bytesWritten;
			WinAPI.WriteProcessMemory(targetProcess.Handle, address + last, buffer, 4, out bytesWritten);
		}
		public static void Write(this Process targetProcess, IntPtr address, long value, params int[] offsets) {
			if (targetProcess == null || targetProcess.HasExited) { return; }

			int last = OffsetAddress(targetProcess, ref address, offsets);
			byte[] buffer = BitConverter.GetBytes(value);
			int bytesWritten;
			WinAPI.WriteProcessMemory(targetProcess.Handle, address + last, buffer, 8, out bytesWritten);
		}
		public static void Write(this Process targetProcess, IntPtr address, byte value, params int[] offsets) {
			if (targetProcess == null || targetProcess.HasExited) { return; }

			int last = OffsetAddress(targetProcess, ref address, offsets);
			byte[] buffer = new byte[] { value };
			int bytesWritten;
			WinAPI.WriteProcessMemory(targetProcess.Handle, address + last, buffer, 1, out bytesWritten);
		}
		public static void Write(this Process targetProcess, IntPtr address, short value, params int[] offsets) {
			if (targetProcess == null || targetProcess.HasExited) { return; }

			int last = OffsetAddress(targetProcess, ref address, offsets);
			byte[] buffer = BitConverter.GetBytes(value);
			int bytesWritten;
			WinAPI.WriteProcessMemory(targetProcess.Handle, address + last, buffer, 2, out bytesWritten);
		}
		public static void Write(this Process targetProcess, IntPtr address, float value, params int[] offsets) {
			if (targetProcess == null || targetProcess.HasExited) { return; }

			int last = OffsetAddress(targetProcess, ref address, offsets);
			byte[] buffer = BitConverter.GetBytes(value);
			int bytesWritten;
			WinAPI.WriteProcessMemory(targetProcess.Handle, address + last, buffer, 4, out bytesWritten);
		}
		public static void Write(this Process targetProcess, IntPtr address, double value, params int[] offsets) {
			if (targetProcess == null || targetProcess.HasExited) { return; }

			int last = OffsetAddress(targetProcess, ref address, offsets);
			byte[] buffer = BitConverter.GetBytes(value);
			int bytesWritten;
			WinAPI.WriteProcessMemory(targetProcess.Handle, address + last, buffer, 8, out bytesWritten);
		}
		public static void Write(this Process targetProcess, IntPtr address, bool value, params int[] offsets) {
			if (targetProcess == null || targetProcess.HasExited) { return; }

			int last = OffsetAddress(targetProcess, ref address, offsets);
			byte[] buffer = new byte[] { value ? (byte)1 : (byte)0 };
			int bytesWritten;
			WinAPI.WriteProcessMemory(targetProcess.Handle, address + last, buffer, 1, out bytesWritten);
		}
		public static void Write(this Process targetProcess, IntPtr address, byte[] data, params int[] offsets) {
			if (targetProcess == null || targetProcess.HasExited) { return; }

			int last = OffsetAddress(targetProcess, ref address, offsets);
			int bytesWritten;
			WinAPI.WriteProcessMemory(targetProcess.Handle, address + last, data, data.Length, out bytesWritten);
		}
		private static int OffsetAddress(this Process targetProcess, ref IntPtr address, params int[] offsets) {
			bool is64bit = Is64Bit(targetProcess);
			byte[] buffer = new byte[is64bit ? 8 : 4];
			int bytesWritten;
			for (int i = 0; i < offsets.Length - 1; i++) {
				WinAPI.ReadProcessMemory(targetProcess.Handle, address + offsets[i], buffer, buffer.Length, out bytesWritten);
				if (is64bit) {
					address = (IntPtr)BitConverter.ToUInt64(buffer, 0);
				} else {
					address = (IntPtr)BitConverter.ToUInt32(buffer, 0);
				}
			}
			return offsets.Length > 0 ? offsets[offsets.Length - 1] : 0;
		}

		public static List<IntPtr> FindAllSignatures(this Process targetProcess, string searchString) {
			List<IntPtr> returnAddresses = new List<IntPtr>();
            byte[] byteCode = Encoding.ASCII.GetBytes(searchString);

			long minAddress = 65536;
			long maxAddress = Is64Bit(targetProcess) ? 140737488289791L : 2147418111L;
			uint memInfoSize = (uint)Marshal.SizeOf(typeof(MemInfo));
			MemInfo memInfo;

			while (minAddress < maxAddress) {
				WinAPI.VirtualQueryEx(targetProcess.Handle, (IntPtr)minAddress, out memInfo, memInfoSize);
				long regionSize = (long)memInfo.RegionSize;
				if (regionSize <= 0) { break; }

				if ((memInfo.Protect & 0x04) != 0 && (memInfo.Type & 0x20000) != 0 && memInfo.State == 0x1000) {
					byte[] buffer = new byte[regionSize];

					int bytesRead = 0;
					if (WinAPI.ReadProcessMemory(targetProcess.Handle, memInfo.BaseAddress, buffer, (int)regionSize, out bytesRead)) {
						SearchAllMemory(buffer, byteCode, (IntPtr)minAddress, returnAddresses);
					}
				}

				minAddress += regionSize;
			}

			return returnAddresses;
		}
		private static void SearchAllMemory(byte[] buffer, byte[] byteCode, IntPtr currentAddress, List<IntPtr> foundAddresses) {
			for (int i = 0, j = 0; i <= buffer.Length - byteCode.Length; i++) {
				int k = i;
				while (j < byteCode.Length && buffer[k] == byteCode[j]) {
					k++; j++;
				}
				if (j == byteCode.Length) {
					foundAddresses.Add(currentAddress + i);
				}
				j = 0;
			}
		}
		public static bool Is64Bit(this Process process) {
			if (process == null || process.HasExited) { return false; }
			bool flag;
			WinAPI.IsWow64Process(process.Handle, out flag);
			return Environment.Is64BitOperatingSystem && !flag;
		}
		public static Module64 MainModule64(this Process p) {
			Module64[] modules = p.Modules64();
			return modules == null || modules.Length == 0 ? null : modules[0];
		}

		public static Module64[] Modules64(this Process p) {
			if (ModuleCache.Count > 100) { ModuleCache.Clear(); }

			IntPtr[] buffer = new IntPtr[1024];
			uint cb = (uint)(IntPtr.Size * buffer.Length);
			uint totalModules;
			if (!WinAPI.EnumProcessModulesEx(p.Handle, buffer, cb, out totalModules, 3u)) {
				return null;
			}
			uint moduleSize = totalModules / (uint)IntPtr.Size;
			int key = p.StartTime.GetHashCode() + p.Id + (int)moduleSize;
			if (ModuleCache.ContainsKey(key)) { return ModuleCache[key]; }

			List<Module64> list = new List<Module64>();
			StringBuilder stringBuilder = new StringBuilder(260);
			int count = 0;
			while ((long)count < (long)((ulong)moduleSize)) {
				stringBuilder.Clear();
				if (WinAPI.GetModuleFileNameEx(p.Handle, buffer[count], stringBuilder, (uint)stringBuilder.Capacity) == 0u) {
					return list.ToArray();
				}
				string fileName = stringBuilder.ToString();
				stringBuilder.Clear();
				if (WinAPI.GetModuleBaseName(p.Handle, buffer[count], stringBuilder, (uint)stringBuilder.Capacity) == 0u) {
					return list.ToArray();
				}
				string moduleName = stringBuilder.ToString();
				ModuleInfo modInfo = default(ModuleInfo);
				if (!WinAPI.GetModuleInformation(p.Handle, buffer[count], out modInfo, (uint)Marshal.SizeOf(modInfo))) {
					return list.ToArray();
				}
				list.Add(new Module64 {
					FileName = fileName,
					BaseAddress = modInfo.BaseAddress,
					MemorySize = (int)modInfo.ModuleSize,
					EntryPointAddress = modInfo.EntryPoint,
					Name = moduleName
				});
				count++;
			}
			ModuleCache.Add(key, list.ToArray());
			return list.ToArray();
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct MemInfo {
			public IntPtr BaseAddress;
			public IntPtr AllocationBase;
			public uint AllocationProtect;
			public IntPtr RegionSize;
			public uint State;
			public uint Protect;
			public uint Type;
		}
		private static class WinAPI {
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MemInfo lpBuffer, uint dwLength);
			[DllImport("kernel32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool IsWow64Process(IntPtr hProcess, [MarshalAs(UnmanagedType.Bool)] out bool wow64Process);
			[DllImport("psapi.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool EnumProcessModulesEx(IntPtr hProcess, [Out] IntPtr[] lphModule, uint cb, out uint lpcbNeeded, uint dwFilterFlag);
			[DllImport("psapi.dll", SetLastError = true)]
			public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, uint nSize);
			[DllImport("psapi.dll")]
			public static extern uint GetModuleBaseName(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, uint nSize);
			[DllImport("psapi.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool GetModuleInformation(IntPtr hProcess, IntPtr hModule, out ModuleInfo lpmodinfo, uint cb);
		}
	}
	public class Module64 {
		public IntPtr BaseAddress { get; set; }
		public IntPtr EntryPointAddress { get; set; }
		public string FileName { get; set; }
		public int MemorySize { get; set; }
		public string Name { get; set; }
		public FileVersionInfo FileVersionInfo { get { return FileVersionInfo.GetVersionInfo(FileName); } }
		public override string ToString() {
			return Name ?? base.ToString();
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct ModuleInfo {
		public IntPtr BaseAddress;
		public uint ModuleSize;
		public IntPtr EntryPoint;
	}
}