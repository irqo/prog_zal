using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.ExceptionServices;

namespace prog_zal
{
    internal class Program
    {
        static string[,] parking = new string[10, 10];
        static void Main(string[] args)
        {
            string plik = "Parking.csv";

            //sprawdzenie, czy plik istnieje
            if (!File.Exists(plik))                                                     
            {
                //tworzenie pustego parkingu z naglowkami
                for (int i = 0; i < parking.GetLength(0); i++)                          
                {
                    for (int j = 0; j < parking.GetLength(1); j++)
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

                //tworzenie pliku (jesli nie istnieje) i uzupelnianie pustym parkingiem
                string[] linia = new string[parking.GetLength(1)];
                using (StreamWriter sw = new StreamWriter(plik))                        
                {
                    for (int i = 0; i < parking.GetLength(0); i++)
                    {

                        for (int j = 0; j < parking.GetLength(1); j++)
                        {
                            linia[j] = parking[i, j] ?? "";
                        }

                        sw.WriteLine(string.Join(";", linia));
                    }
                }
                Console.WriteLine(); 
                Console.WriteLine("Utworzylem plik i uzupelnilem pierwotnymi danymi.");
                Console.WriteLine();
            }

            //wyswietlanie parkingu z pliku (jeżeli plik istnieje)
            else
            {
                Console.WriteLine();
                Console.WriteLine("================================ PARKING ================================");
                string[] linie = File.ReadAllLines(plik);

                for (int i = 0; i < linie.Length; i++)
                {
                    string[] kolumny = linie[i].Split(';');

                    for (int j = 0; j < kolumny.Length; j++)
                    {
                        Console.Write($"{kolumny[j]}\t");
                    }
                    Console.WriteLine("\n");
                }
                Console.WriteLine("================================ PARKING ================================");
            }
            Console.WriteLine();
            Console.WriteLine();



            Menu();
        }




        //==================================== MENU ====================================
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
                    Console.WriteLine("| 0. Zakoncz dzialanie aplikacji                 |");
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
                        case '1': SprawdzParking(Program.parking, "Parking.csv"); break;
                        case '2': ParkujPojazd(Program.parking, "Parking.csv"); break;
                        case '3': ZabierzPojazd(Program.parking, "Parking.csv"); break;
                        case '4': ZmienMiejsce(Program.parking, "Parking.csv"); break;
                        case '5': SprawdzZajetosc(Program.parking, "Parking.csv"); break;
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
        //==================================== MENU ====================================





        //==================================== 1. SPRAWDZANIE PARKINGU ====================================
        static void SprawdzParking(string[,] parking, string plik)
        {
            Console.WriteLine();
            Console.WriteLine("================================ PARKING ================================");
            string[] linie = File.ReadAllLines(plik);

            for (int i = 0; i < linie.Length; i++)
            {
                string[] kolumny = linie[i].Split(';');

                for (int j = 0; j < kolumny.Length; j++)
                {
                    Console.Write($"{kolumny[j]}\t");
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("================================ PARKING ================================");
        }
        //==================================== 1. SPRAWDZANIE PARKINGU ====================================




        //==================================== 2. PARKOWANIE POJAZDU ====================================
        static void ParkujPojazd(string[,] parking, string plik)
        {
            //wczytanie pliku
            if (File.Exists(plik))
            {
                string[] linie = File.ReadAllLines(plik);

                for (int i = 0; i < linie.Length; i++)
                {
                    string[] kolumny = linie[i].Split(';');

                    for (int j = 0; j < kolumny.Length; j++)
                    {
                        parking[i, j] = kolumny[j];
                    }
                }
            }
            
            //zmiana w konsoli
            Console.WriteLine();
            Console.WriteLine("Wybierz miejsce parkingowe");
            int r;
            int c;

            Console.WriteLine("Skad chcesz zabrac samochod?");

            while (true)
            {
                Console.Write("Podaj rzad (od 1 do 9): ");
                r = Convert.ToInt32(Console.ReadLine());
                
                if (r >= 1 && r <= 9)
                {
                    break;
                }
                Console.WriteLine();
                Console.WriteLine("BLAD! Musisz podac wartosc z zakresu 1-9!");
            }

            while (true)
            {
                Console.Write("Podaj miejsce od lewej (od 1 do 9): ");
                c = Convert.ToInt32(Console.ReadLine());
                
                if (c >= 1 && c <= 9)
                {
                    break;
                }
                Console.WriteLine();
                Console.WriteLine("BLAD! Musisz podac wartosc z zakresu 1-9!");

            }

            if (parking[r, c] == "X")
            {
                Console.WriteLine();
                Console.WriteLine("Miejsce zajete, nie mozesz tam zaparkowac");
            }
            else
            {
                parking[r, c] = "X";
                Console.WriteLine();
                Console.WriteLine($"Twoje miejsce jest w rzedzie {r} i jest to miejsce {c}.");
            }

            //zapis zamian do pliku
            using (StreamWriter sw = new StreamWriter(plik))      
            {
                for (int i = 0; i < parking.GetLength(0); i++)
                {
                    for (int j = 0; j < parking.GetLength(1); j++)
                    {
                        sw.Write(parking[i, j] ?? ".");
                        
                        if (j < parking.GetLength(1) - 1)
                        {
                            sw.Write(";");
                        }
                    }

                    sw.WriteLine();
                }
            }
            Console.WriteLine();
        }
        //==================================== 2. PARKOWANIE POJAZDU ====================================






        //==================================== 3. ZABIERANIE POJAZDU ====================================
        static void ZabierzPojazd(string[,] parking, string plik)
        {
            //wczytanie pliku
            if (File.Exists(plik))
            {
                string[] linie = File.ReadAllLines(plik);

                for (int i = 0; i < linie.Length; i++)
                {
                    string[] kolumny = linie[i].Split(';');

                    for (int j = 0; j < kolumny.Length; j++)
                    {
                        parking[i, j] = kolumny[j];
                    }
                }
            }

            //zmiana w konsoli
            Console.WriteLine();
            int r;
            int c;

            Console.WriteLine("Skad chcesz zabrac samochod?");

            while (true)
            {
                Console.Write("Podaj rzad (od 1 do 9): ");                 
                r = Convert.ToInt32(Console.ReadLine());
                
                if (r >=1 && r <= 9)
                {
                    break;
                }
                Console.WriteLine();
                Console.WriteLine("BLAD! Musisz podac wartosc z zakresu 1-9!");
            }
            
            while (true)
            {
                Console.Write("Podaj miejsce od lewej (od 1 do 9): ");    
                c = Convert.ToInt32(Console.ReadLine());
                
                if (c >=1 && c <= 9)
                {
                    break;
                }
                Console.WriteLine();
                Console.WriteLine("BLAD! Musisz podac wartosc z zakresu 1-9!");
            }
            

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


            //zapis zmian do pliku
            using (StreamWriter sw = new StreamWriter(plik))                        
            {
                for (int i = 0; i < parking.GetLength(0); i++)
                {
                    for (int j = 0; j < parking.GetLength(1); j++)
                    {
                        sw.Write(parking[i, j] ?? ".");
                        
                        if (j < parking.GetLength(1) - 1)
                        {
                            sw.Write(";");
                        }
                    }

                    sw.WriteLine();
                }
            }
            Console.WriteLine();
        }
        //==================================== 3. ZABIERANIE POJAZDU ====================================





        //==================================== 4. PRZEPARKOWANIE POJAZDU ====================================
        static void ZmienMiejsce(string[,] parking, string plik)
        {
            //wczytanie pliku
            if (File.Exists(plik))
            {
                string[] linie = File.ReadAllLines(plik);

                for (int i = 0; i < linie.Length; i++)
                {
                    string[] kolumny = linie[i].Split(';');

                    for (int j = 0; j < kolumny.Length; j++)
                    {
                        parking[i, j] = kolumny[j];
                    }
                }
            }

            //zmiana w konsoli
            Console.WriteLine();
            int r1;
            int c1;
            int r2;
            int c2;

            Console.WriteLine("Skad chcesz zabrac samochod?");
            
            while (true)
            {
                Console.Write("Podaj, w ktorym rzedzie stoi Twoj samochod (od 1 do 9): ");
                r1 = Convert.ToInt32(Console.ReadLine());
                
                if (r1 >= 1 && r1 <= 9)
                {
                    break;
                }
                Console.WriteLine();
                Console.WriteLine("BLAD! Musisz podac wartosc z zakresu 1-9!");
            }

            while (true)
            {
                Console.Write("Podaj, na ktorym miejscu stoi Twoj samochod (od 1 do 9): ");
                c1 = Convert.ToInt32(Console.ReadLine());
                
                if (c1 >= 1 && c1 <= 9)
                {
                    break;
                }
                Console.WriteLine();
                Console.WriteLine("BLAD! Musisz podac wartosc z zakresu 1-9!");
            }
            Console.WriteLine();
            
            if (parking[r1, c1] == ".")
            {
                Console.WriteLine("Na tym miejscu nie ma zaparkowanego samochodu");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Podaj, gdzie chcesz zaparkowac:");

                while (true)
                {
                    Console.Write("Podaj, w którym rzedzie chcesz zaparkowac: ");
                    r2 = Convert.ToInt32(Console.ReadLine());

                    if ( r2 >= 1 && r2 <= 9)
                    {
                        break;
                    }
                    Console.WriteLine();
                    Console.WriteLine("BLAD! Musisz podac wartosc z zakresu 1-9!");
                }    
                
                while (true)
                {
                    Console.Write("Podaj, na ktorym miejscu chcesz zaparkowac: ");
                    c2 = Convert.ToInt32(Console.ReadLine());

                    if (c2 >= 1 && c2 <= 9)
                    {
                        break;
                    }
                    Console.WriteLine();
                    Console.WriteLine("BLAD! Musisz podac wartosc z zakresu 1-9!");
                }
                Console.WriteLine();
                
                if (parking[r2, c2] == "X")
                {
                    Console.WriteLine();
                    Console.WriteLine("Miejsce zajete, nie mozesz tam przeparkowac samochodu.");
                }
                else
                {
                    parking[r2, c2] = "X";
                    parking[r1, c1] = ".";

                    Console.WriteLine();
                    Console.WriteLine($"Twoje nowe miejsce parkingowe miejsce jest w rzedzie {r2} i jest to miejsce {c2}.");
                }
            }

            //zapis zmian do pliku
            using (StreamWriter sw = new StreamWriter(plik))
            {
                for (int i = 0; i < parking.GetLength(0); i++)
                {
                    for (int j = 0; j < parking.GetLength(1); j++)
                    {
                        sw.Write(parking[i, j] ?? ".");
                        
                        if (j < parking.GetLength(1) - 1)
                        {
                            sw.Write(";");
                        }
                    }

                    sw.WriteLine();
                }
            }
            Console.WriteLine();

        }
        //==================================== 4. PRZEPARKOWANIE POJAZDU ====================================





        //==================================== 5. SPRAWDZENIE ZAJĘTOŚCI MIEJSCA ====================================
        static void SprawdzZajetosc(string[,] parking, string plik)
        {
            //wczytanie pliku
            if (File.Exists(plik))
            {
                string[] linie = File.ReadAllLines(plik);

                for (int i = 0; i < linie.Length; i++)
                {
                    string[] kolumny = linie[i].Split(';');

                    for (int j = 0; j < kolumny.Length; j++)
                    {
                        parking[i, j] = kolumny[j];
                    }
                }
            }

            //sprawdzenie zajętości
            Console.WriteLine();
            int r;
            int c;
            Console.WriteLine("Sprawdz, czy miejsce jest wolne, czy zajete");

            while (true)
            {
                Console.WriteLine("W ktorym rzedzie sprawdzamy miesjsce (od 0 do 9): ");
                r = Convert.ToInt32(Console.ReadLine());

                if (r >= 1 && r <= 9)
                {
                    break;
                }
                Console.WriteLine();
                Console.WriteLine("BLAD! Musisz podac wartosc z zakresu 1-9!");
            }
            
            while (true)
            {
                Console.WriteLine($"Ktore miejsce w rzedzie {r} sprawdzamy (od 0 do 9): ");
                c = Convert.ToInt32(Console.ReadLine());

                if (c >= 1 && c <= 9)
                {
                    break;
                }
                Console.WriteLine();
                Console.WriteLine("BLAD! Musisz podac wartosc z zakresu 1-9!");
            }

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
        //==================================== 5. SPRAWDZENIE ZAJĘTOŚCI MIEJSCA ====================================
    }
}