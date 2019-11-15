using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace KR_V5
{
    class Vklad                             //Oбъявление класса Вклад
    {
        struct vklad                        
        {
            public string nameVklad;        
            public double procent;          

            public override string ToString() //Метод,приводящий информацию структуры в строку для вывода в консоль
            {
                return string.Format("\tНаименование Вклада: {0}\n" +
                                     "\tПроцентная ставка:   {1:0.##}%", nameVklad, procent); 
            }

            public string ToFile()            //Метод,приводящий информацию структуры в строку для записи в файл
            {
                return nameVklad + "|" + procent + "|%";  
            }
        }

        public static List<Object> GetVklad() //Метод,получающий информацию из файла,возвращающий её в виде нумерованного списка структур
        {
            List<Object> vklads = new List<Object>();   
            vklad new_vklad = new vklad();              
            try                                         
            {
                string[] lines = File.ReadAllText("vklad.txt", Encoding.GetEncoding(1251)).Split('\n');        
                foreach (string line in lines)
                {                                                                   
                    if (line == string.Empty) continue;                             
                    string[] info = line.Split('|');                                
                    new_vklad.nameVklad = info[0];                                  
                    new_vklad.procent = Convert.ToDouble(info[1]);
                    vklads.Add(new_vklad);                                          
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Blue;                       
                Console.WriteLine("Exception! " + ex.Message);                      
                Console.ResetColor();                                               
            }
            return vklads;                                                          
        }

        public static void Find()             //Метод поиска вклада
        {
            Console.Write("\tВведите наименование интересующего Вас вклада: ");     
            string find = Console.ReadLine();                                       
            bool flag = true;                                                       
            List<Object> vklads = GetVklad();                                       
            foreach (vklad t in vklads)
            {                                                                       
                if (t.nameVklad == find)
                {                                                                   
                    Console.Write("\n" + t.ToString());                             
                    flag = false;                                                   
                }
            }
            if (flag) Console.WriteLine("\tНе обнаружено действующих вкладов.");    
        }
        public static void Find1()            //Метод поиска вклада + метод OpenDepozit класса Klient
        {
            Console.Write("\n\tВведите наименование интересующего Вас вклада: ");   
            string find = Console.ReadLine();                                       
            bool flag = true;                                                       
            List<Object> vklads = GetVklad();                                       
            foreach (vklad t in vklads)
            {                                                                       
                if (t.nameVklad == find)
                {                                                                   
                    Console.Write("\n" + t.ToString() +                             
                                  "\n\n\tХотите открыть депозит? (да/нет)\n\t");
                    if (Console.ReadLine() == "да") Klient.OpenDepozit(t.nameVklad, t.procent);    //Если пользователь введёт "да",то вызвать метод OpenDepozit класса Klient
                    flag = false;                                                   
                }
            }
            if (flag) Console.WriteLine("\tНе обнаружено действующих вкладов.");    
        }

        public static void ShowAll()          //Метод вывода всех доступных вкладов на консоль
        {
            Console.Clear();                                                          
            Console.WriteLine("\n\tСписок действующих вкладов: ");
            int i = 1;                                                                
            List<Object> vklads = GetVklad();                                         
            foreach (vklad t in vklads)
            {                                                                         
                Console.WriteLine("\n" + i++ + " ---------------------------------"); 
                Console.WriteLine(t.ToString());                                      
            }
        }
        public static void Add()              //Метод добавления нового вклада           
        {
            try
            {                                       
                while (true)
                {                                       
                    vklad new_vklad;                    
                    Console.Clear();                    
                    Console.Write("\n\tДобавление нового вклада (Клавиша 'Enter' - Отмена)" +          
                                  "\n\n\tВведите наименование вклада: ");
                    new_vklad.nameVklad = Console.ReadLine();                    
                    if (new_vklad.nameVklad == string.Empty) break;              
                    FlagProcent1:                                                
                    Console.Write("\tВведите процентную ставку (,):  ");         
                    try                                                          
                    {
                        new_vklad.procent = Convert.ToDouble(Console.ReadLine()); 
                    }
                    catch (System.FormatException)                               
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;                        
                        Console.Write("\n\tВы ввели не число! Проверьте правильность ввода.\n");
                        Console.ResetColor();                                                
                        goto FlagProcent1;                                                   
                    }
                    if (new_vklad.procent <= 0 || new_vklad.procent > 100)       
                    {
                        Console.WriteLine("\n\tПроцентная ставка не может быть меньше 0 и больше 100! ");   
                        goto FlagProcent1;                                       
                    }
                    StreamWriter file = new StreamWriter("vklad.txt", true, Encoding.Default); //Инициализировать поток записи в файл текстовой информации (true - дописать)
                    file.WriteLine(new_vklad.ToFile());                     
                    file.Close();                                           
                    Console.WriteLine("\n\tНовый вклад добавлен в базу!");  
                    break;                                                  
                }
            }
            catch (Exception ex)                               
            {
                Console.ForegroundColor = ConsoleColor.Blue;  
                Console.WriteLine("Exception! " + ex.Message); 
                Console.ResetColor();                          
            }
        }

        public static void Edit()             //Метод редактирования вклада
        {
            ShowAll();                                         
            List<Object> vklads = GetVklad();                  
            Console.Write("\n\tВведите номер вклада,который необходимо редактировать: ");
            try
            {                                                   
                int edit = int.Parse(Console.ReadLine());       
                vklad buffer = (vklad)vklads[edit - 1];         
                Console.Clear();                                
                Console.Write("\n" + buffer.ToString() + "\n\n\tКакое поле вы хотить изменить?\n\t");   
                int top = Console.CursorTop;                    
                int left = Console.CursorLeft;                  
                for (int q = 1; q <= 2; q++)
                {                                               
                    Console.CursorTop = q;                      
                    Console.CursorLeft = 4;                     
                    Console.Write(q + " - ");                         
                }
                Console.CursorTop = top;                        
                Console.CursorLeft = left;                      

                switch (Console.ReadLine())
                {                                               
                    case "1":
                        Console.Write("\n\tВведите изменённое название вклада: ");             
                        buffer.nameVklad = Console.ReadLine();                                 //Присвоение нового значения полю nameVklad
                        Console.WriteLine("\n\tВклад успешно изменён");                        
                        break;
                    case "2":
                        FlagProcent2:                                        
                        Console.Write("\n\tВведите изменённое значение процентной ставки: ");  	
                        buffer.procent = double.Parse(Console.ReadLine());                     //Присвоение нового значения полю procent
                        if (buffer.procent <= 0 || buffer.procent > 100)                       
                        {
                            Console.WriteLine("\n\tПроцентная ставка не может быть меньше 0 и больше 100! ");
                            goto FlagProcent2;                              
                        }
                        Console.WriteLine("\n\tВклад успешно изменён");                        
                        break;
                    default:
                        Console.WriteLine("\n\tТакого поля не существует");                    
                        break;
                }
                vklads.RemoveAt(edit - 1);                                                     								
                vklads.Insert(edit - 1, buffer);                                               

                StreamWriter file = new StreamWriter("vklad.txt", false, Encoding.GetEncoding(1251)); 
                foreach (vklad t in vklads)
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

        public static void Delete()           //Метод удаления вклада
        {
            ShowAll();                                              
            Console.Write("\n\tВведите номер вклада,который необходимо удалить: ");        
            try
            {                                                       
                ushort del = ushort.Parse(Console.ReadLine());      
                List<Object> vklads = GetVklad();                   
                Console.Write("\n\tВы точно хотите удалить этот вклад? (да/нет)\n\t");
                if (Console.ReadLine() == "да")
                {
                    vklads.RemoveAt(del - 1);                       
                    Console.WriteLine("\n\tBклад успешно удалён."); 
                }

                StreamWriter file = new StreamWriter("vklad.txt", false, Encoding.GetEncoding(1251)); 
                foreach (vklad t in vklads)
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
    }
}
