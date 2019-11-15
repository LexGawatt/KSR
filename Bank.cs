using System;                       
using System.Collections.Generic;   
using System.IO;                    
using System.Linq;                  
using System.Text;                  

namespace KR_V5
{
    class Bank
    {
        static void ManagerBanka()                                  //Статический метод вывода на экран меню менеджера
        {							
            Console.Write("\n\tВНИМАНИЕ! Вы переходите в режим 'Менеджер банка'. "); 
            Console.Write("\n\tВведите пароль: ");                                   
            string password = "";                                                         
            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);                           
                if (i.Key == ConsoleKey.Enter)                                      
                {
                    break;                                                            
                }
                else if (i.Key == ConsoleKey.Backspace)                             
                {
                    password = password.Remove(password.Length - 1);                               
                    Console.Write("\b \b");
                }
                else                                                                 
                {
                    password += i.KeyChar;                                                
                    Console.Write("*");                                              
                }
            }

            if (password == "15021993")                                                       
            {
                try                                                                  
                {
                    while (true)                                                    
                    {
                        Console.Clear();                                             
                        Console.WriteLine("\tМеню Менеджера банка:\n 0 - Вернуться в меню пользователя" + 
                                          "\n   -------------------------------" +
                                          "\n 1 - Информация по вкладам" +
                                          "\n 2 - Поиск вклада" +
                                          "\n 3 - Добавить новый вклад" +
                                          "\n 4 - Редактировать вклад" +
                                          "\n 5 - Удалить вклад" +
                                          "\n   -------------------------------" +
                                          "\n 6 - Информация о всех клиентах" +
                                          "\n 7 - Информация о клиенте" +
                                          "\n 8 - Открыть депозит (Добавить клиента)" +
                                          "\n 9 - Пополнить сумму вклада клиента" +
                                          "\n10 - Удалить клиента" +
                                          "\n   -------------------------------" +
                                          "\n11 - Информация по всем доступным вкладчикам бонусам" +
                                          "\n12 - Добавить бонус" +
                                          "\n13 - Удалить бонус" +
                                          "\n   -------------------------------" +
                                          "\n14 - Расчёт общей суммы выплат");
                        switch (Console.ReadLine()) 
                        {
                            case "1":
                                Vklad.ShowAll();   // вызов метода вывода информации по всем вкладам
                                Console.ReadKey(); 
                                break;             
                            case "2":
                                Vklad.Find();      // вызов метода поиска вклада по названию 
                                Console.ReadKey(); 
                                break;             
                            case "3":
                                Vklad.Add();       // вызов метода добовление вклада 
                                Console.ReadKey(); 
                                break;             
                            case "4":
                                Vklad.Edit();      // вызов метода редактирования вклада
                                Console.ReadKey(); 
                                break;             
                            case "5":
                                Vklad.Delete();    // вызов метода удаления вклада
                                Console.ReadKey(); 
                                break;             
                            case "6":
                                Klient.ShowAll();  // вызов метода вывода информации о всех клиентах
                                Console.ReadKey(); 
                                break;             
                            case "7":
                                Klient.ShowMy();   // вызов метода вывода информации о клиенте (по номеру паспорта)
                                Console.ReadKey(); 
                                break;             
                            case "8":
                                Vklad.Find1();     // вызов метода открытия депозита 
                                Console.ReadKey(); 
                                break;             
                            case "9":
                                Klient.EditClient(); // вызов метода пополнения суммы вклада 
                                Console.ReadKey(); 
                                break;             
                            case "10":
                                Klient.Delete();   // вызов метода удаления клиента из базы
                                Console.ReadKey(); 
                                break;             
                            case "11":
                                Bonus.ShowAll();   // вызов метода вывода информации о всех бонусах 
                                Console.ReadKey(); 
                                break;             
                            case "12":
                                Bonus.Add();       // вызов метода добовления бонуса
                                Console.ReadKey(); 
                                break;             
                            case "13":
                                Bonus.Delete();    // вызов метода удаления бонуса
                                Console.ReadKey(); 
                                break;             
                            case "14":
                                Klient.Calac();    // вызов метода расчёта общей суммы выплат
                                Console.ReadKey(); 
                                break;             
                            case "0":
                                return;
                            default:
                                Console.ForegroundColor = ConsoleColor.Blue;             
                                Console.WriteLine("\nТакого пункта меню не существует!"); 
                                Console.ResetColor();                                     
                                Console.ReadKey();                                        
                                break;                                                    
                        }
                    }
                }
                catch (Exception ex)                              
                {
                    Console.ForegroundColor = ConsoleColor.Blue;  
                    Console.WriteLine("Exception! " + ex.Message); 
                    Console.ResetColor();                          
                }
            }
            else
            {
                Console.WriteLine("\n\tТакого пароля не существует");
            }  

        }

        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\tМеню пользователя:\n 0 - Выход из программы" +
                                      "\n   -------------------" +
                                      "\n 1 - Информация по вкладам" +
                                      "\n 2 - Поиск вклада" +
                                      "\n 3 - Открыть депозит" +
                                      "\n 4 - Посмотреть открытый депозит" +
                                      "\n   -------------------" +
                                      "\n ManagerBanka - Меню менеджера банка");
                    switch (Console.ReadLine()) 
                    {
                        case "1":
                            Vklad.ShowAll();   // вызов метода вывода информации по всем вкладам
                            Console.ReadKey(); 
                            break;             
                        case "2":
                            Vklad.Find();      // вызов метода поиска вклада по названию вклада
                            Console.ReadKey(); 
                            break;             
                        case "3":
                            Vklad.ShowAll();   // вызов метода вывода информации по всем вкладам
                            Vklad.Find1();     // вызов метода открытия счёта (депозит)
                            Console.ReadKey(); 
                            break;             
                        case "4":
                            Klient.ShowMy();   // вызов метода вывода информации о состоянии открытого счёта (по номеру паспорта)
                            Console.ReadKey(); 
                            break;             
                        case "ManagerBanka":
                            ManagerBanka();    //вызов метода меню менеджера банка
                            Console.ReadKey(); 
                            break;             
                        case "0":
                            return;
                        default:
                            Console.ForegroundColor = ConsoleColor.Blue; 
                            Console.WriteLine("\nТакого пункта меню не существует!"); 
                            Console.ResetColor();    
                            Console.ReadKey();       
                            break;                   
                    }
                }
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
