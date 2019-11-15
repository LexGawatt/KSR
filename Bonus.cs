using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace KR_V5
{
    class Bonus                                          //Класс Bonus для работы с бонусами
    {
        struct bonus
        {                                                	
            public string name;                          
            public double value;                         
            public string type;                          
            public string description;                   

            public override string ToString()           
            {
                return "\tИмя бонуса:    " + name +
                       "\n\tРазмер бонуса: " + value + type +
                       "\n\tОписание:      " + description;
            }

            public string ToFile()                      //Метот,приводящий информацию в строку для записи в файл
            {
                return name + "|" + value + "|" + type + "|" + description;
            }
        }

        public static List<Object> GetBonus()           //Метод считывания информации из файла в нумерованный список 
        {
            List<Object> bonus = new List<Object>();   
            bonus new_bonus = new bonus();              
            try
            {                                           
                string[] lines = File.ReadAllText("bonus.txt", Encoding.GetEncoding(1251)).Split('\n');   
                foreach (string line in lines)
                {                                       
                    if (line == string.Empty) continue; 
                    string[] info = line.Split('|');    
                    new_bonus.name = info[0];           
                    new_bonus.value = Convert.ToDouble(info[1]);  
                    new_bonus.type = info[2];           
                    new_bonus.description = info[3];    
                    bonus.Add(new_bonus);              
                }
            }
            catch (Exception ex)                               
            {
                Console.ForegroundColor = ConsoleColor.Blue;  
                Console.WriteLine("Exception! " + ex.Message); 
                Console.ResetColor();                          
            }
            return bonus;                                     
        }
        public static void ShowAll()                     //Метод вывода на консоль всех доступных бонусов
        {
            Console.Clear();                             
            Console.WriteLine("\n\tДействующие бонусы\n\n");
            ushort i = 1;                                
            foreach (bonus c in GetBonus())
            {                                            
                Console.WriteLine("\n\t" + i++ + "-----------------------------------");    
                Console.WriteLine(c.ToString());                                            
            }
            Console.ReadKey();                           
        }
        public static void Add()                           //Метод добавления новых бонусов
        {                                                
            try
            {                                            
                while (true)
                {                                                               
                    bonus new_bonus;                                            
                    Console.Clear();                                            
                    Console.Write("\n\tДобавление нового бонуса" +               
                                  "\n\n\tВведите код бонуса: ");                
                    new_bonus.name = Console.ReadLine();                        
                    if (new_bonus.name == string.Empty) break;                  
                    Console.Write("\tВведите величину скидки бонуса: ");        
                    new_bonus.value = Convert.ToDouble(Console.ReadLine());     
                    Console.Write("\tВведите тип бонуса (% или р): ");          
                    new_bonus.type = Console.ReadLine();                         
                    Console.Write("\tВведите описание бонуса: ");               
                    new_bonus.description = Console.ReadLine();                 

                    StreamWriter file = new StreamWriter("bonus.txt", true, Encoding.GetEncoding(1251));  
                    file.WriteLine(new_bonus.ToFile());                         
                    file.Close();                                               		
                    Console.WriteLine("\n\tНовый бонус добавлен в базу");       
                }
            }
            catch (Exception ex)                               
            {
                Console.ForegroundColor = ConsoleColor.Blue;  
                Console.WriteLine("Exception! " + ex.Message); 
                Console.ResetColor();                          
            }
            Console.ReadKey();                                 
        }
        public static void Delete()                                 //Метод удаления бонусов
        {
            ShowAll();                                              
            Console.Write("\n\tВведите номер бонуса который необходимо удалить: "); 
            try
            {                                                       
                ushort del = ushort.Parse(Console.ReadLine());      
                List<Object> bonuss = GetBonus();                   
                Console.Write("\n\tВы точно хотите удалить этот бонус? (да/нет)\n\t");
                if (Console.ReadLine() == "да")
                {
                    bonuss.RemoveAt(del - 1);                       
                    Console.WriteLine("\n\tБонус успешно удалён."); 
                }

                StreamWriter file = new StreamWriter("bonus.txt", false, Encoding.GetEncoding(1251)); 
                foreach (bonus t in bonuss)                         
                {
                    file.WriteLine(t.ToFile());                     
                }
                file.Close();                                       
            }
            catch (Exception ex)                               
            {
                Console.ForegroundColor = ConsoleColor.Blue;  
                Console.WriteLine("Exception! " + ex.Message); 
                Console.ResetColor();                          
            }
        }


        public static double GetDiscount(string bonus, double procent) //Метод возращения величины бонуса
        {
            List<Object> bonuss = GetBonus();                         
            foreach (bonus c in bonuss)
            {                                                          
                if (c.name == bonus)
                {                                                      
                    if (c.type == "%") return procent = c.value;       //Подсчитывание величины бонуса,если тип бонуса процентный:procent = procent + c.value;  
                    else if (c.type == "p" || c.type == "р") return procent = c.value; //Подсчитывание величины бонуса,если тип бонуса рублёвый
                    else return procent;                                                
                }
            }
            return procent;                                            
        }
    }
}
