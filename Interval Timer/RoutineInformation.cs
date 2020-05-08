using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Controls;

namespace Interval_Timer
{
    
    public class RoutineInformation
    {
        public string routineFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"Routines\";
        private static RoutineInformation routineInformation;
        private Dictionary<string, Routine> routineDictionary;
        private BinaryFormatter formatter;
        private const string data_filename = "Routines.dat";

        public static RoutineInformation Instance()
        {
            if (routineInformation == null)
            {
                routineInformation = new RoutineInformation();
            }
            return routineInformation;
        }

        private RoutineInformation()
        {
            this.routineDictionary = new Dictionary<string, Routine>();
            this.formatter = new BinaryFormatter();
        }
        public void AddRoutine(string name, int warmup, int lowSet, int highSet, int sets, bool firstIntHigh= true, int cooldown = 0, int repeat = 0, int rest = 0)
        {
            if (this.routineDictionary.ContainsKey(name))
            {
                Console.WriteLine("You already have that routine");
                //NameError.Text = "Routine name already exists, please choose another name";
                //NameError.Visibility = Visibility.Visible;
            }
            else
            {
                this.routineDictionary.Add(name, new Routine(name, warmup, lowSet, highSet, sets, firstIntHigh, cooldown, repeat, rest));
                Console.WriteLine("Routine added successfully");
            }
        }

        public void RemoveRoutine(string name)
        {
            if (!this.routineDictionary.ContainsKey(name))
            {
                Console.WriteLine(name + " routine doesn't exist");
            }
            else
            {
                if (this.routineDictionary.Remove(name))
                {
                    Console.WriteLine("Routine removed successfully");
                }
                else
                {
                    Console.WriteLine("Error removing " + name);
                }
            }
        }

        public void Save()
        {
            try
            {
                FileStream writerFileStream = new FileStream(data_filename, FileMode.Create, FileAccess.Write);
                this.formatter.Serialize(writerFileStream, this.routineDictionary);
                writerFileStream.Close();
                Console.WriteLine("Saved successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured while trying to save: " + e);
            }
        }

        public void Load()
        {
            if (File.Exists(data_filename))
            {
                try
                {
                    FileStream readerFileSream = new FileStream(data_filename, FileMode.Open, FileAccess.Read);
                    this.routineDictionary = (Dictionary<string, Routine>)
                        this.formatter.Deserialize(readerFileSream);
                    readerFileSream.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unable to read the file: " + e);
                }
            }
        }
        public void Print()
        {
            if (this.routineDictionary.Count > 0)
            {
                Console.WriteLine("Name");
                foreach (Routine routine in this.routineDictionary.Values)
                {
                    int timeLeft = routine.LowSet + routine.HighSet;
                    int h = timeLeft / 3600;
                    int m = (timeLeft % 3600) / 60 ;
                    int s = timeLeft % 60;
                    string hh = h.ToString();
                    string mm = m.ToString();
                    string ss = s.ToString();
                    if (h < 10) { hh = hh.Insert(0, "0"); }

                    if (m < 10) { mm = mm.Insert(0, "0"); }

                    if (s < 10) { ss = ss.Insert(0, "0"); }
                    Console.WriteLine($"{routine.Name} Time left: { hh }:{ mm }:{ ss } Sets: {routine.Sets} Warmup: {routine.Warmup} Cooldown: {routine.Cooldown} Repeat: {routine.Repeat} Rest: {routine.Rest}");
                }
            }
            else
            {
                Console.WriteLine("There are no saved routines");
            }
        }

        public string GetTime(string name)
        {
            if (this.routineDictionary.Count > 0)
            { 
                foreach(Routine routine in this.routineDictionary.Values)
                {
                    if (name == routine.Name)
                    {                        
                        if (routine.Repeat > 0)
                        {
                            int timeLeft = routine.Warmup + ((routine.HighSet + routine.LowSet) * routine.Sets) * routine.Repeat + (routine.Rest) * (routine.Repeat - 1) + routine.Cooldown;
                            int h = timeLeft / 3600;
                            int m = (timeLeft % 3600) / 60;
                            int s = timeLeft % 60;

                            string hh = h.ToString();
                            string mm = m.ToString();
                            string ss = s.ToString();
                            if (h < 10) { hh = hh.Insert(0, "0"); }

                            if (m < 10) { mm = mm.Insert(0, "0"); }

                            if (s < 10) { ss = ss.Insert(0, "0"); }
                            return $"{ hh }:{ mm }:{ ss }";
                        }
                        else
                        {
                            int timeLeft = routine.Warmup + (routine.HighSet + routine.LowSet) * routine.Sets + routine.Cooldown;
                            int h = timeLeft / 3600;
                            int m = (timeLeft % 3600) / 60;
                            int s = timeLeft % 60;
                            string hh = h.ToString();
                            string mm = m.ToString();
                            string ss = s.ToString();
                            if (h < 10) { hh = hh.Insert(0, "0"); }

                            if (m < 10) { mm = mm.Insert(0, "0"); }

                            if (s < 10) { ss = ss.Insert(0, "0"); }
                            return $"{ hh }:{ mm }:{ ss }";
                        }
                    } 
                }
            }
            return "";
        }

        public string GetTimeFromSeconds(int time)
        {
            int h = time / 3600;
            int m = (time % 3600) / 60;
            int s = time % 60;
            string hh = h.ToString();
            string mm = m.ToString();
            string ss = s.ToString();
            if (h < 10) { hh = hh.Insert(0, "0"); }

            if (m < 10) { mm = mm.Insert(0, "0"); }

            if (s < 10) { ss = ss.Insert(0, "0"); }
            if (Convert.ToInt32(hh) == 0) { return $"{mm}:{ss}"; }
            else { return $"{hh}:{mm}:{ss}"; }
        }

        public string GetAll(string name)
        {
            if (this.routineDictionary.Count > 0)
            {
                foreach (Routine routine in this.routineDictionary.Values)
                {
                    if (name == routine.Name)
                    {
                        return $"{routine.Warmup} {routine.HighSet} {routine.LowSet} {routine.FirstIntHigh} {routine.Sets} {routine.Repeat} {routine.Rest} {routine.Cooldown}";
                    }
                }
            }
            return "";
        }


        public int GetTimeSeconds(string name)
        {
            if (this.routineDictionary.Count > 0)
            {
                foreach (Routine routine in this.routineDictionary.Values)
                {
                    if (name == routine.Name)
                    {
                        if (routine.Repeat > 0)
                        {
                            int timeLeft = routine.Warmup + ((routine.HighSet + routine.LowSet) * routine.Sets) * routine.Repeat + (routine.Rest) * (routine.Repeat - 1) + routine.Cooldown;
                            return timeLeft;
                        }
                        else
                        {
                            int timeLeft = routine.Warmup + (routine.HighSet + routine.LowSet) * routine.Sets + routine.Cooldown;
                            return timeLeft;
                        }
                    }
                }
            }
            return 0;
        }

        //public string GetName(string name)
        //{
        //    if (this.routineDictionary.Count > 0)
        //    {
        //        foreach (Routine routine in this.routineDictionary.Values)
        //        {
        //            if (name == routine.Name)
        //            {
        //                Console.WriteLine($"{ routine.Name }");
        //                Console.WriteLine($"{ routine.HighSet }");
        //                Console.WriteLine($"{ routine.LowSet }");
        //                Console.WriteLine($"{ routine.Warmup }");
        //                Console.WriteLine($"{ routine.Sets }");
        //                break;
        //            }
        //            return routine.Name;
        //        }
        //    }
        //    return "Name can't be loaded";
        //}

        public Dictionary<string, Routine> GetDict()
        {
            return this.routineDictionary;
        }

        //public string GetAllNames()
        //{
        //    if (this.routineDictionary.Count > 0)
        //    {
        //        foreach (Routine routine in this.routineDictionary.Values)
        //        {
        //            Console.WriteLine($"{ routine.Name }");
        //            return routine.Name;
        //        }
        //    }
        //    return "Name can't be loaded" ;
        //}

        public int Length()
        {
            if (this.routineDictionary.Count > 0)
            {
                return this.routineDictionary.Count;
            }
            return 0;
        }

    }
}
