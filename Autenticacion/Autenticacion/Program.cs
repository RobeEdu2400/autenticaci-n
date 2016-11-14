using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion
{
    class Program
    {
        static void Main(string[] args)
        {
            int Menu = 5;
            while (Menu != 0) {
                Console.Clear();
                Console.WriteLine("1- Alta Usuario");
                Console.WriteLine("2- Autenticarse");
                Console.WriteLine("0- Salir");
                try { 
                Menu = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Valor incorrecto,presiona una tecla para continuar");
                    Console.ReadKey();
                }
                
                if (Menu == 1)
                {
                    String Password = null;
                    String Nombre = null;
                    Console.WriteLine("Introduce el nombre de usuario");
                    Nombre = Console.ReadLine();
                    Console.WriteLine("Introduce el password");
                    ConsoleKeyInfo key;
                    
                    do
                    {
                        key = Console.ReadKey(true);
                        if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                        {
                            Password += key.KeyChar;
                            Console.Write("*");
                        }
                        else if(key.Key==ConsoleKey.Backspace)
                            {
                            if ((Password.Length>0))
                            {
                                Console.Write("\b\b");
                            }
                        }
                    } while (key.Key != ConsoleKey.Enter);
                    Console.WriteLine();
                    Console.WriteLine("Usuario Creado Correctamente");
                    Console.ReadKey();
                }


            }
        }
    }
}
