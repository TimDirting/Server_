using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Management.Instrumentation;
using System.ComponentModel;

namespace Data_Server_A01
{
    class Packing
    {
        public Packing()
        { 
            Win32_Class.Add("SELECT * FROM Win32_Processor");            // 0
            Win32_Class.Add("SELECT * FROM Win32_PhysicalMemory");       // 1
            Win32_Class.Add("SELECT * FROM Win32_DiskDrive");            // 2
            Win32_Class.Add("SELECT * FROM Win32_VideoSettings");        // 3
            Win32_Class.Add("SELECT * FROM Win32_NetworkAdapter");       // 4

            ValofStar.Add(CreateCa("LoadPercentage", Win32_Class[0], "%"));
            ValofStarneed1.Add(CreateCa("Level", Win32_Class[0]));                      //1
            ValofStarneed1.Add(CreateCa("MaxClockSpeed", Win32_Class[0], "HZ"));                //1
            ValofStarneed1.Add(CreateCa("NumberOfCores", Win32_Class[0]));                      //1
            ValofStarneed1.Add(CreateCa("NumberOfLogicalProcessors", Win32_Class[0]));                      //1
            ValofStarneed1.Add(CreateCa("ThreadCount", Win32_Class[0]));                      //1
            ValofStar.Add(CreateCa("CurrentClockSpeed", Win32_Class[0], "HZ"));
            ValofStarneed1.Add(CreateCa("DataWidth", Win32_Class[0]));                      //1
            ValofStarneed1.Add(CreateCa("Family", Win32_Class[0]));                      //1
        }
        public List<qanten> ValofStar = new List<qanten> { };
        public List<qanten> ValofStarneed1 = new List<qanten> { };
        public List<string> Win32_Class = new List<string> { };
        public string[] data = new string[200];
        public string[] tempdata = new string[200];

        public string[] getData(bool Want_Data_once)
        {
            if (Want_Data_once)
            {
                for (int i = 0; i < ValofStarneed1.Count; i++)
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(ValofStarneed1[i].Class);
                    ManagementObjectCollection colle = searcher.Get();
                    foreach (ManagementObject obj in colle)
                    {
                        data[i] = Convert.ToString(obj[ValofStarneed1[i].TSelect]);
                    }
                }
                return data;
            }
            else
            {
                for (int i = 0; i < ValofStar.Count; i++)
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(ValofStar[i].Class);
                    ManagementObjectCollection colle = searcher.Get();
                    foreach (ManagementObject obj in colle)
                    {
                        tempdata[i] = Convert.ToString(obj[ValofStar[i].TSelect]);
                    }
                }
                return tempdata;
            }
            
        }

        public qanten CreateCa(string Select, string Classindex, string val)
        {
            qanten V;
            V.Class = Classindex;
            V.TSelect = Select;
            V.ValType = val;

            return V;
        }
        public qanten CreateCa(string Select, string Classindex)
        {
            qanten V;
            V.Class = Classindex;
            V.TSelect = Select;
            V.ValType = "-";
            return V;
        }


    }
}
