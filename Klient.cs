using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace KR_V5
{
    class Klient                              //Объявление класса "Klient"
    {
        struct klient                           
        {

            public string fio;                  //имя клиента
            public string numPassport;          //номер паспорта
            public string nameVklad;            //наименование Вкладa
            public double summaVklada;          //сумма вклада
            public DateTime dateVklada;         //дата вклада
            public int collMonths;              //количество месяцев вклада
            public DateTime dateViplati;        //дата выплаты
            public double summaViplatiBonus;    //сумма выплаты (с бонусом, есть такой имеется)
            // procent и DiscountBonus нужны для изменения суммы вклада
            public double procent;              //процентная ставка (год)
            public double DiscountBonus;        //размер бонуса

            public override string ToString()   //Метод,приводящий информацию в строку для вывода в консоль
            {
                return "\tФ.И.О. клиента:               " + fio +
                       "\n\tПаспортные данные:         " + numPassport +
                       "\n\tНаименование вкладa:       " + nameVklad +
                       "\n\tCумма вклада               " + Math.Round(summaVklada, 2) + " руб." +       // округление до сотых
                       "\n\tДата вклада               " + dateVklada.Day + "." + dateVklada.Month + "." + dateVklada.Year +
                       "\n\tКоличество месяцев вклада  " + collMonths +
                       "\n\tДата выплаты               " + dateViplati.Day + "." + dateViplati.Month + "." + dateViplati.Year +
                       "\n\tСумма выплаты              " + Math.Round(summaViplatiBonus, 2) + " руб.";  // округление до сотых
            }

            public string ToFile()              //Метод,приводящий информацию в строку для записи в файл
            {
                return fio + "|" + numPassport + "|" + nameVklad + "|" + Math.Round(summaVklada, 2) + "|" + dateVklada.Day + "." + dateVklada.Month + "." + dateVklada.Year + "|" +
                    collMonths + "|" + dateViplati.Day + "." + dateViplati.Month + "." + dateViplati.Year + "|" + Math.Round(summaViplatiBonus, 2) + "|" + procent + "|" + DiscountBonus;
            }
        }

        public static List<Object> GetKlients() //Метод,получающий информацию из файла и возвращающий её в виде нумерованного списка
        {
            List<Object> klients = new List<Object>();  
            klient new_klient = new klient();           
            try
            {                                           
                string[] lines = File.ReadAllText("clients.txt", Encoding.GetEncoding(1251)).Split('\n');   //Считывание текста из файла в массив строк
                foreach (string line in lines)
                {                                                               //Перечисление массива строк
                    if (line == string.Empty) continue;                         //Если строка пустая, то пропустить эту строку
                    string[] parts = line.Split('|');                           //Разбить текущую строку на подстроки по пробелам
                    new_klient.fio = parts[0];                                  //Первую подстроку присвоить полю fio
                    new_klient.numPassport = parts[1];                          //2ю подстроку присвоить полю numPassport
                    new_klient.nameVklad = parts[2];                            //3ю подстроку присвоить полю nameVklad
                    new_klient.summaVklada = Convert.ToDouble(parts[3]);        //4ю подстроку присвоить полю summaVklada приведя её к типу Double
                    new_klient.dateVklada = DateTime.Parse(parts[4]);           //5ю подстроку присвоить полю dateVklada приведя её к типу DateTime
                    new_klient.collMonths = Int32.Parse(parts[5]);              //6ю подстроку присвоить полю collMonths пиведя её к типу int
                    new_klient.dateViplati = DateTime.Parse(parts[6]);          //7ю подстроку присвоить полю dateViplati приведя её к типу DateTime
                    new_klient.summaViplatiBonus = Convert.ToDouble(parts[7]);  //8ю подстроку присвоить полю summaViplatiBonus приведя её к типу Double
                    new_klient.procent = Convert.ToDouble(parts[8]);            //9ю подстроку присвоить полю procents приведя её к типу Double
                    new_klient.DiscountBonus = Convert.ToDouble(parts[9]);      //10ю подстроку присвоить полю DiscountBonus приведя её к типу Double
                    klients.Add(new_klient);                                    //Добавить в нумерованный список 
                }
            }
            catch (Exception ex)                                                
            {
                Console.ForegroundColor = ConsoleColor.Blue;                   
                Console.WriteLine("Exception! " + ex.Message);                  
                Console.ResetColor();                                           
            }
            return klients;                                                     
        }

        public static void ShowAll()            //Метод вывода на консоль всех клиентов
        {
            Console.Clear();                                                    
            Console.WriteLine("\tИнформация о всех клиентах банка:");          
            int collkl = 0;                                                     //Объявление счётчика количества клиентов (нумератор)
            foreach (klient t in GetKlients())
            {                                                                   //Получение и перечисление всех билетов
                Console.WriteLine();                                            //Подсчёт количества клиентов банка
                collkl++;
                Console.WriteLine("\n" + collkl + " ---------------------------------"); //Вывод номера текущего клиента
                Console.Write("\n" + t.ToString());                             
            }
            if (collkl == 0) Console.WriteLine("\t\nНа данные момент времени в банке нет клиентов.");  
        }

        public static void ShowMy()             //Метод для поиска открытого вклада клиента	
        {
            Console.Clear();                                                    
                                                                                
            Console.Write("\n\tВведите серию и номер паспорта (без пробелов): ");
            string find = Console.ReadLine();                                   //Запись в переменную find строки, введённой пользователем
            Console.WriteLine();                                                
            bool flag = true;                                                   
            foreach (klient t in GetKlients())                                  //Получение и перечисление всех вкладов
            {                                                                   
                if (t.numPassport == find)                                      //Проверка: совпадает ли номер паспорта с искомым и находится ли дата в будущем
                {                                                               
                    Console.WriteLine(t.ToString() + "\n");                     
                    flag = false;                                              
                }
            }
            if (flag) Console.Write("\n\tДанные по клиенту не обнаружены");    
        }

        public static void OpenDepozit(string nameVklad, double procent)        //Метод открытия вклада(депозита)
        {
            Console.Clear();                                                    
            klient new_klient = new klient();                                   
            new_klient.dateVklada = DateTime.Today.AddDays(-1);                 
            Console.Write("\n\tДля открытия вклада " + nameVklad + " необходимо ввести паспортные данные:\n\n");     
            try
            {                                                                   
                bool flag = true;                                               
                while (flag)
                {                                                               
                    Console.Write("\tВведите Ф.И.О.: ");                           
                    new_klient.fio = Console.ReadLine();                        
                    flag = false;                                               
                    if (new_klient.fio.Length < 2 && new_klient.fio == string.Empty)    //меньше 2х букв (по условию задачи)
                    {                                                                   
                        flag = true;                                                    
                        ErrorMessage("Ф.И.О. не может отсутствовать,содержать цифры (символы), быть меньше 2х букв");   
                    }
                    else
                        foreach (char c in new_klient.fio)
                        {                                                        
                            if (Char.IsNumber(c) || Char.IsPunctuation(c) || Char.IsSymbol(c))
                            {                                                    
                                flag = true;                                                                                
                                ErrorMessage("ФИО не может отсутствовать,содержать цифры(символы), быть меньше 2х букв");   
                            }
                        }
                }

                flag = true;                                                     	
                while (flag)
                {                                                               
                    Console.Write("\tВведите серию и номер паспорта (без пробелов): "); 
                    new_klient.numPassport = Console.ReadLine();                        
                    flag = false;                                                       
                    foreach (char c in new_klient.numPassport)
                    {                                                                   
                        if (Char.IsPunctuation(c) || Char.IsWhiteSpace(c) || Char.IsSymbol(c))
                        {                                                               
                            flag = true;                                                
                            ErrorMessage("Серия и номер паспорта не должны содержать пробелов и символов");  
                        }
                    }
                }

                new_klient.nameVklad = nameVklad;                               

                flag = true;                                                    
                while (flag)
                {                                                               
                    Console.Write("\tВведите сумму вклада: ");                  
                    try                                                         
                    {
                        new_klient.summaVklada = double.Parse(Console.ReadLine());
                        flag = false;                                           
                        if (new_klient.summaVklada < 0)                         //Проверка величины вклада на отрицательное значение
                        {
                            flag = true;                                        
                            ErrorMessage("Сумма вклада не может отсутствовать,а также быть отрицательной");   
                        }
                    }
                    catch (System.FormatException)
                    {
                        ErrorMessage("Вы ввели не число! Проверьте правильность ввода. Разделитель(,)");       
                        flag = true;                                                                           
                    }
                }

                while (new_klient.dateVklada < DateTime.Today)
                {                                                               
                    Console.Write("\tВведите дату вклада (ДД.ММ.ГГГГ): ");      
                    try
                    {                                                           
                        new_klient.dateVklada = DateTime.Parse(Console.ReadLine());                   
                        if (new_klient.dateVklada < DateTime.Today) throw new ArgumentException();   
                    }
                    catch (Exception)
                    {                                                           
                        flag = true;                                            
                        ErrorMessage("Дата не может быть в прошлом и должна соответствовать формату (ДД.ММ.ГГГГ)"); 
                    }
                }

                flag = true;                                                         
                while (flag)
                {                                                                    
                    Console.Write("\tВведите количество месяцев хранения вклада: "); 
                    try                                                              
                    {
                        new_klient.collMonths = int.Parse(Console.ReadLine());       
                        flag = false;                                                
                        if (new_klient.collMonths < 0)                               
                        {
                            flag = true;                                             
                            ErrorMessage("Количество месяцев не может отсутствовать,а также быть отрицательным"); 
                        }
                    }
                    catch (System.FormatException)
                    {
                        ErrorMessage("Вы ввели не число! Проверьте правильность ввода."); 
                        flag = true;                                                      
                    }
                }

                new_klient.dateViplati = new_klient.dateVklada.AddMonths(new_klient.collMonths); 
                new_klient.procent = procent;                                                    

                flag = true;                                                                     
                while (flag)
                {                                                                                
                    Console.Write("\tЕсли вы обладаете бонусной картой,то введите её код\n\t(чтобы пропустить нажмите Enter): ");    
                    string bonus = Console.ReadLine();                                           
                    if (bonus == string.Empty)
                    {                                                                            
                        new_klient.summaViplatiBonus = new_klient.summaVklada * (1 + (procent / 100 / 12 * new_klient.collMonths)); //Подсчёт суммы выплаты
                        Console.Write("\tСумма выплаты по вкладу (без бонуса): " + Math.Round(new_klient.summaViplatiBonus, 2) + " руб.\n"); 
                    }
                    if (bonus == string.Empty) break;                                         

                    double DiscountBonus = Bonus.GetDiscount(bonus, procent);                  //Высчитать скидку по бонусу (проверяет наличие купонов)

                    if (procent == DiscountBonus)                                              //Если процентная ставка вклада не отличается от вклада с бонусом 
                    {
                        flag = true;                                                           
                        new_klient.summaViplatiBonus = new_klient.summaVklada * (1 + (procent / 100 / 12 * new_klient.collMonths)); 
                        Console.Write("\tТакого бонуса не существует!\n\tСумма выплаты по вкладу (без бонуса):  " + Math.Round(new_klient.summaViplatiBonus, 2) + " руб.\n"); 
                        ErrorMessage("Попробуйте ввести код бонуса ещё раз.");                 
                        new_klient.DiscountBonus = 0;                                          
                    }
                    else
                    {
                        if (DiscountBonus <= 10)                                               //Если вклад с бонусом меньше либо равен 10, то это процентный бонус
                        {
                            new_klient.summaViplatiBonus = new_klient.summaVklada * (1 + ((procent + DiscountBonus) / 100 / 12 * new_klient.collMonths)); 
                            Console.Write("\tБонус найден! Бонус даёт +{0}% годовых к ставке депозита", DiscountBonus);
                            Console.Write("\n\tCумма выплаты по вкладу (С БОНУСОМ): " + Math.Round(new_klient.summaViplatiBonus, 2) + " руб.\n"); 
                            flag = false;                                                                                                        
                        }
                        else                                                                   
                        {
                            new_klient.summaViplatiBonus = new_klient.summaVklada * (1 + (procent / 100 / 12 * new_klient.collMonths)) + DiscountBonus; 
                            Console.Write("\tБонус найден! Бонус даёт +{0} руб. к сумме выплаты", DiscountBonus);
                            Console.Write("\n\tCумма выплаты по вкладу (С БОНУСОМ): " + Math.Round(new_klient.summaViplatiBonus, 2) + " руб.\n"); 
                            flag = false;                                                                                                        

                        }
                        new_klient.DiscountBonus = DiscountBonus;                              

                    }
                }

                StreamWriter file = new StreamWriter("clients.txt", true, Encoding.GetEncoding(1251)); 
                file.WriteLine(new_klient.ToFile());                                            
                file.Close();                                                                   
                Console.Write("\n\tСпасибо за открытие депозита.");                             
            }
            catch (Exception ex)                               
            {
                Console.ForegroundColor = ConsoleColor.Blue;  
                Console.WriteLine("Exception! " + ex.Message); 
                Console.ResetColor();                          
            }
        }
        static void ErrorMessage(string message)             
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Exception! " + message);     
            Console.ResetColor();                       
            Console.ReadLine();                         
            Console.CursorTop -= 2;                     
                                                        
            Console.WriteLine("                                                                                                                                                                       ");
            Console.WriteLine("                                                                                                                                                                       ");
            Console.CursorTop -= 2;                     
        }

        public static void EditClient()                 
        {
            double change = new double();               
            List<Object> klients = GetKlients();        
            Console.Clear();                            
            Console.Write("\n\tВвведите серию и номер паспорта Клиента (без пробелов): ");     
            string poiskPassport = Console.ReadLine(); 
            int flag = 0;                              
            for (int i = 0; i < klients.Count; i++)
            {                                          
                klient t = (klient)klients[i];
                if (poiskPassport == t.numPassport)
                {
                    flag++;                            
                    Console.WriteLine("\tФ.И.О. (Клиента): " + t.fio);                     
                    Console.Write("\n\tВведите на сколько рублей пополнить вклад: ");  
                    try
                    {                                   
                        change = Convert.ToDouble(Console.ReadLine());
                    }
                    catch (Exception e)
                    {                                   
                        Console.WriteLine("\n\t" + e.Message);  
                    }
                    t.summaVklada = t.summaVklada + change;     
                    if (t.DiscountBonus <= 10)                  
                        t.summaViplatiBonus = t.summaVklada * (1 + ((t.procent + t.DiscountBonus) * 0.01 / 12 * t.collMonths)); 
                    else
                        t.summaViplatiBonus = t.summaVklada * (1 + (t.procent * 0.01 / 12 * t.collMonths)) + t.DiscountBonus;   
                    Console.WriteLine("\tНовая сумма вклада: " + t.summaVklada);                     
                    Console.WriteLine("\tНовая сумма выплаты по вкладу: " + t.summaViplatiBonus);    

                    klients.RemoveAt(i);                                                             
                    klients.Insert(i, t);                                                            
                    Console.ReadKey();                                                               
                    Console.WriteLine("\n\tВклад успешно изменён");                                  
                }
            }

            StreamWriter file = new StreamWriter("clients.txt", false, Encoding.GetEncoding(1251));  
            foreach (klient t in klients)
            {                                                   
                file.WriteLine(t.ToFile());                     
            }
            file.Close();                                       	
            if (flag == 0) Console.WriteLine("\tКлиент не найден. Проверьте правильность ввода.");
        }

        public static void Calac()                       // Метод расчёта общей суммы выплат
        {
            try
            { 

                StreamReader read = new StreamReader("clients.txt", Encoding.GetEncoding(1251)); 
                string line;                            
                double amount = 0;                      

                while ((line = read.ReadLine()) != null)
                {
                    string[] var = line.Split('|');     
                    amount += Convert.ToDouble(var[7]); 
                }
                read.Close();                           

                Console.WriteLine("\n\tОбщая сумма выплат:"); 
                Console.WriteLine("\t{0:### ### ###.##} бел.руб.", amount); 

            }
            catch (Exception ex)                               
            {
                Console.ForegroundColor = ConsoleColor.Blue;  
                Console.WriteLine("Exception! " + ex.Message); 
                Console.ResetColor();                          
            }
        }
        public static void Delete()                                 //Метод удаления клиента
        {
            ShowAll();                                              
            Console.Write("\n\n\tВведите номер клиента, которого необходимо удалить: "); 
            try
            {                                                       
                ushort del = ushort.Parse(Console.ReadLine());      
                List<Object> klients = GetKlients();                
                Console.Write("\n\tВы точно хотите удалить этого клиента? (да/нет)\n\t");
                if (Console.ReadLine() == "да")
                {
                    klients.RemoveAt(del - 1);                      
                    Console.WriteLine("\n\tКлиент удалён."); 
                }

                StreamWriter file = new StreamWriter("clients.txt", false, Encoding.GetEncoding(1251)); 
                foreach (klient t in klients)                       
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
