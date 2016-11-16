using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion
{
    class Program
    {
        static void Main(string[] args)
        {
            int Menu=0;
            do
            {
                Console.Clear();
                Console.WriteLine("1- Alta Usuario");
                Console.WriteLine("2- Autenticarse");
                Console.WriteLine("0- Salir");
                try
                {
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
                        else if (key.Key == ConsoleKey.Backspace)
                        {
                            if ((Password.Length > 0))
                            {
                                Console.Write("\b\b");
                            }
                        }
                    } while (key.Key != ConsoleKey.Enter);
                    Console.WriteLine();
                    Console.WriteLine("Usuario Creado Correctamente");
                    Console.ReadKey();

                    byte[] salt;
                    new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                    String resultadoSalt = BitConverter.ToString(salt, 0);

                    var prueba = new Rfc2898DeriveBytes(Password, salt, 1);
                    byte[] hash = prueba.GetBytes(32);

                    String resultadoHash = BitConverter.ToString(hash, 0);

                    Console.WriteLine("Introduce el nombre del archivo:");
                    String nombre = null;
                    while (nombre == null)
                        nombre = Console.ReadLine();

                    File.AppendAllText(nombre,Nombre+" "+resultadoSalt+" "+resultadoHash+string.Format(Environment.NewLine));
                }
            } while (Menu != 0);
        }
    }
}
