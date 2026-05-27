using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace prog_zal
{
    internal class Program
    {
        static string[,] parking = new string[10, 10];
        static void Main(string[] args)
        {

            Console.WriteLine("===== PARKING =====");

            for (int i = 0; i < 10; i++) //uzupełnienie pustegu parkingu (z nagłówkami)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0)
                    {
                        Program.parking[i, j] = Convert.ToString(j);
                    }
                    else if (j == 0)
                    {
                        Program.parking[i, j] = Convert.ToString(i);
                    }
                    else
                    {
                        Program.parking[i, j] = ".";
                    }
                    Console.Write($"{Program.parking[i, j]} ");

                }
                Console.WriteLine();
            }
            Menu();
        }



        //================== MENU ==================
        static void Menu()
        {
            char c = 'c';
            try
            {
                while (c != '0')
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine(" ===================== MENU =====================");
                    Console.WriteLine("| 1. Wyswietl aktualny stan parkingu             |");
                    Console.WriteLine("| 2. Zaparkuj pojazd                             |");
                    Console.WriteLine("| 3. Zabierz pojazd                              |");
                    Console.WriteLine("| 4. Przeparkuj pojazd na inne miejsce           |");
                    Console.WriteLine("| 5. Sprawdz, czy miejsce jest puste, czy zajete |");
                    Console.WriteLine("| 0. Zakoncz działanie aplikacji                 |");
                    Console.WriteLine(" ===================== MENU =====================");

                    Console.WriteLine();
                    Console.Write("Co chcesz zrobić: ");
                    c = Convert.ToChar(Console.ReadLine());
                    if (String.IsNullOrEmpty(c.ToString()) || String.IsNullOrWhiteSpace(c.ToString()))
                    {
                        Console.WriteLine("Bledny wybor");
                        return;
                    }
                    switch (c)
                    {
                        case '1': SprawdzParking(Program.parking); break;
                        case '2': ParkujPojazd(Program.parking); break;
                        case '3': ZabierzPojazd(Program.parking); break;
                        case '4': ZmienMiejsce(Program.parking); break;
                        case '5': SprawdzZajetosc(Program.parking); break;
                        case '0': Console.WriteLine("KONIEC"); break;
                        default: Console.WriteLine("Bledny wybor"); break;
                    }
                }
            }
            catch (global::System.Exception ex)

            {
                Console.WriteLine("Bledny wybor");
                Menu();
            }
        }



        //================== SPRAWDZANIE PARKINGU ==================
        static void SprawdzParking(string[,] parking)
        {
            Console.WriteLine();
            Console.WriteLine("===== PARKING =====");
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write($"{parking[i, j]} ");
                }
                Console.WriteLine();
            }

        }



        //================== PARKOWANIE POJAZDU ==================
        static void ParkujPojazd(string[,] parking)
        {
            Console.WriteLine();
            Console.WriteLine("Wybierz miejsce parkingowe");
            Console.Write("Podaj rząd (od 1 do 9): ");                 
            int r = Convert.ToInt32(Console.ReadLine());
            Console.Write("Podaj miejsce od lewej (od 1 do 9): ");  
            int c = Convert.ToInt32(Console.ReadLine());

            if (parking[r, c] == "X")
            {
                Console.WriteLine();
                Console.WriteLine("Miejsce zajęte, nie możesz tam zaparkować");
            }
            else
            {
                parking[r, c] = "X";
                Console.WriteLine();
                Console.WriteLine($"Twoje miejsce jest w rzędzie {r} i jest to miejsce {c}.");
            }
        }



        //================== ZABIERANIE POJAZDU ==================
        static void ZabierzPojazd(string[,] parking)
        {
            Console.WriteLine();
            Console.WriteLine("Skąd chcesz zabrać samochód?");
            Console.Write("Podaj rząd (od 1 do 9): ");                 
            int r = Convert.ToInt32(Console.ReadLine());
            Console.Write("Podaj miejsce od lewej (od 1 do 9): ");    
            int c = Convert.ToInt32(Console.ReadLine());

            if (parking[r, c] == ".")
            {
                Console.WriteLine();
                Console.WriteLine("To miejsce jest puste i nie ma tu Twojego samochodu!!!");
            }
            else
            {
                parking[r, c] = ".";
                Console.WriteLine();
                Console.WriteLine("Szerokiej drogi!");
            }
        }



        //================== PRZEPARKOWANIE POJAZDU ==================
        static void ZmienMiejsce(string[,] parking)
        {
            Console.WriteLine();
            Console.WriteLine("Skąd chcesz zabrać samochód?");
            Console.Write("Podaj, w którym rzędzie stoi Twój samochód (od 1 do 9): ");    
            int r1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Podaj, na którym miejscy stoi Twój samochód (od 1 do 9): ");     
            int c1 = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine();
            if (parking[r1, c1] == ".")
            {
                Console.WriteLine("Na tym miejscu nie ma zaparkowanego samochodu");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Podaj, gdzie chcesz zaparkować");
                Console.WriteLine("Podaj, w którym rzędzie chcesz zaparkować: ");
                int r2 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Podaj, na którym miejscu chcesz zaparkować: ");
                int c2 = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();
                while (parking[r2, c2] != "X")
                {
                    if (parking[r2, c2] == "X")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Miejsce zajęte, nie możesz tam zaparkować");
                    }
                    else
                    {
                        parking[r2, c2] = "X";
                        parking[r1, c1] = ".";

                        Console.WriteLine();
                        Console.WriteLine($"Twoje nowe miejsce parkingowe miejsce jest w rzędzie {r2} i jest to miejsce {c2}.");
                    }
                }
            }
        }





        //================== SPRAWDZENIE ZAJĘTOŚCI ==================
        static void SprawdzZajetosc(string[,] parking)
        {
            Console.WriteLine();
            Console.WriteLine("Sprawdz, czy miejsce jest wolne, czy zajete");
            Console.WriteLine("W ktorym rzedzie sprawdzamy miesjsce (od 0 do 9): ");
            int r = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Ktore miejsce w rzedzie {r} sprawdzamy (od 0 do 9): ");
            int c = Convert.ToInt32(Console.ReadLine());

            if (parking[r, c] == ".")
            {
                Console.WriteLine();
                Console.WriteLine("To miejsce jest wolne");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("To miejsce jest zajete");
            }

        }
    }
}