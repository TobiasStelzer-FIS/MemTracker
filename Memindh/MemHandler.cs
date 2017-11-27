﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Memindh
{
    public class MemHandler
    {
        private SYSTEM_INFO systemInfo;
        private Process process;
        private string processname;
        private int processHandle;

        public string Processname { get; set; }
        
        public MemHandler(string processname)
        {
            Processname = processname;

            Initialize();
        }

        private void Initialize()
        {
            // getting minimum and maximum address
            GetSystemInfo(out systemInfo);

            process = Process.GetProcessesByName(Processname)[0];

            // opening process with desired access level
            processHandle = (int)OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_WM_READ, false, process.Id);
        }

        public IntPtr CalculateFinalAddress(string moduleName, int startAddress, int[] preOffsets, int offset = 0)
        {
            IntPtr currentAddress = IntPtr.Zero;

            try
            {
                ProcessModuleCollection modules = process.Modules;

                foreach (ProcessModule module in modules)
                {
                    if (module.ModuleName == moduleName)
                    {
                        currentAddress = IntPtr.Add(module.BaseAddress, startAddress);
                    }
                }

                if (preOffsets != null)
                {
                    for (int i = 0; i < preOffsets.Length; i++)
                    {
                        if (currentAddress == IntPtr.Zero)
                        {
                            throw new Exception("Error in CalculateFinalAddress. Couldn't apply offset.");
                        }
                        else
                        {
                            currentAddress = ReadPointer(IntPtr.Add(currentAddress, preOffsets[i]));
                        }
                    }
                }

                currentAddress = IntPtr.Add(currentAddress, offset);
            }
            catch (Exception e)
            {
                throw e;
            }

            return currentAddress;
        }

        public int Read(string moduleName, int startAddress, int[] preOffsets, int offset = 0)
        {
            try
            {
                IntPtr finalAddress = CalculateFinalAddress(moduleName, startAddress, preOffsets, offset);
                return ReadInt32(finalAddress);
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        public IntPtr ReadPointer(IntPtr address)
        {
            byte[] buffer = new byte[IntPtr.Size];
            int bytesRead = 0;
            
            ReadProcessMemory(processHandle, (int)address, buffer, buffer.Length, ref bytesRead);

            if (bytesRead > 0)
            {
                return (IntPtr)BitConverter.ToInt32(buffer, 0);
            }
            throw new Exception("Error in ReadPointer. Couldn't read from Memory-Address " + address.ToString());
        }

        public Int32 ReadInt32(IntPtr address)
        {
            byte[] buffer = new byte[sizeof(Int32)];
            int bytesRead = 0;

            ReadProcessMemory(processHandle, (int)address, buffer, buffer.Length, ref bytesRead);

            if (bytesRead > 0)
            {
                return BitConverter.ToInt32(buffer, 0);
            }
            throw new Exception("Error in ReadInt32. Couldn't read from Memory-Address " + address.ToString());
        }

        public int Read(Type type)
        {
            int pointer = 501416720;

            Process process = Process.GetProcessesByName("Terraria")[0];

            IntPtr processHandle = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_WM_READ, false, process.Id);

            byte[] buffer = new byte[4];
            int bytesRead = 0;
            ReadProcessMemory((int)processHandle, pointer, buffer, buffer.Length, ref bytesRead);

            return BitConverter.ToInt32(buffer, 0);
        }
/*
        public void Do()
        {
            

            IntPtr proc_min_address = sys_info.minimumApplicationAddress;
            IntPtr proc_max_address = sys_info.maximumApplicationAddress;

            long proc_min_address_l = (long)proc_min_address;
            long proc_max_address_l = (long)proc_max_address;

            Process process = Process.GetProcessesByName("notepad")[0];

            // opening process with desired access level
            IntPtr processHandle = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_WM_READ, false, process.Id);

            StreamWriter sw = new StreamWriter("C:\\Users\\Tobias\\Desktop\\dump.txt");

            // this will store any information we get from VirtualQueryEx()
            MEMORY_BASIC_INFORMATION mem_basic_info = new MEMORY_BASIC_INFORMATION();

            int bytesRead = 0; // number of bytes read with ReadProcessMemory

            while(proc_min_address_l < proc_max_address_l)
            {
                // 28 = sizeof(MEMORY_BASIC_INFORMATION)
                VirtualQueryEx(processHandle, proc_min_address, out mem_basic_info, 28);

                // if this memory chunk is accessible
                if(mem_basic_info.Protect == PAGE_READWRITE && mem_basic_info.State == MEM_COMMIT)
                {
                    byte[] buffer = new byte[mem_basic_info.RegionSize];
                    
                    // read everything in the buffer above
                    ReadProcessMemory((int)processHandle, mem_basic_info.BaseAddress, buffer, mem_basic_info.RegionSize, ref bytesRead);

                    // then output this in the file
                    for (int i = 0; i < mem_basic_info.RegionSize; i++)
                    {
                        if ((char)buffer[i] >= 33 && (char)buffer[i] <= 126)
                        {
                            sw.WriteLine("0x{0} : {1}", (mem_basic_info.BaseAddress + i).ToString("X"), (char)buffer[i]);
                        }
                    }
                }

                // move to the next memory chunk
                proc_min_address_l += mem_basic_info.RegionSize;
                proc_min_address = new IntPtr(proc_min_address_l);
            }

            sw.Close();

            Console.ReadLine();
        }
*/

        // REQUIRED CONSTS
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int MEM_COMMIT = 0x00001000;
        const int PAGE_READWRITE = 0x04;
        const int PROCESS_WM_READ = 0x0010;

        // REQUIRED METHODS
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        // REQUIRED STRUCTS

        public struct MEMORY_BASIC_INFORMATION
        {
            public int BaseAddress;
            public int AllocationBase;
            public int AllocationProtect;
            public int RegionSize;
            public int State;
            public int Protect;
            public int lType;
        }

        public struct SYSTEM_INFO
        {
            public ushort processorArchitecture;
            ushort reserved;
            public uint pageSize;
            public IntPtr minimumApplicationAddress;
            public IntPtr maximumApplicationAddress;
            public IntPtr activeProcessorMask;
            public uint processorType;
            public uint allocationGranularity;
            public ushort processorLevel;
            public ushort processorRevision;
        }


        
    }
}