using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion{
    class Program{
        static void Main(string[] args){
            int Menu=0;
            do{
                Console.Clear();
                Console.WriteLine("1- Alta Usuario");
                Console.WriteLine("2- Autenticarse");
                Console.WriteLine("0- Salir");
                try{
                    Menu = Convert.ToInt32(Console.ReadLine());
                }
                catch{
                    Console.WriteLine("Valor incorrecto,presiona una tecla para continuar");
                    Console.ReadKey();
                    
                }

                if (Menu == 1){
                    String Password = null;
                    String Nombre = null;
                    Boolean comprobar = false;
                    Console.WriteLine("Introduce el nombre de usuario");
                    Nombre = Console.ReadLine();
                    comprobar=comprobarNombre(Nombre);
                    if (comprobar==true)
                    {
                        Console.WriteLine("El usuario ya existe,presiona una tecla para continuar");
                        Console.ReadKey();
                    }
                    else { 
                        Console.WriteLine("Introduce el password");
                        Password = generarPassword();                    
                    
                        Console.WriteLine();
                        Console.WriteLine("Usuario Creado Correctamente,presiona una tecla para continuar");
                        Console.ReadKey();
                        byte[] salt;
                        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                        byte[] hash=creacionHash(salt,Password); 
                        string resultadoSalt;
                        string resultadoHash;
                        resultadoSalt = Convert.ToBase64String(salt);
                        resultadoHash = Convert.ToBase64String(hash);

                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\C#\autenticaci-n\Autenticacion\Autenticacion\bin\Debug\WriteLines.txt", true))
                        {
                            file.WriteLine(Nombre + (',') + resultadoSalt + (',') + resultadoHash);
                        }
                    }                        
                }
                else
                {
                    if (Menu == 2)
                    {
                        String Password;
                        String Nombre;
                        String Usuario;
                        String[] Archivo;
                        
                        Console.WriteLine("Introduce el nombre de usuario");
                        Nombre = Console.ReadLine();
                        Console.WriteLine("Introduce el password");
                        Password = generarPassword();

                        Usuario = encontrarSalt(Nombre);
                        try
                        {
                            Archivo = Usuario.Split(',');
                            byte[] salt = Convert.FromBase64String(Archivo[1]);
                            byte[] hash = creacionHash(salt, Password);

                            String resultadoHash = Convert.ToBase64String(hash);
                            if (resultadoHash == Archivo[2])
                            {
                                Console.WriteLine("Usuario autenticado correctamente");
                            }
                            else
                            {
                                Console.WriteLine("Usuario auntenticado incorrectamente");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("El usuario no existe");
                        }
                       
                    }
                    Console.ReadKey();
                }
            } while (Menu != 0);
        }

        private static Boolean comprobarNombre(String Nombre)
        {
            Boolean encontrado=false;
            try
            {
                using (StreamReader sr = new StreamReader("WriteLines.txt"))
                {
                    while (sr.Peek() > -1)
                    {
                        String line = sr.ReadLine();
                        string[] usuario;

                        usuario = line.Split(',');

                        if (usuario[0].Equals(Nombre))
                        {
                             encontrado = true;
                        }
                    }


                }

            }
            catch
            {
                Console.WriteLine("El archivo no existe");
            }
            return encontrado;
        }

        private static String encontrarSalt(String Nombre)
        {
            try
            {
                using (StreamReader sr = new StreamReader("WriteLines.txt"))
                {
                    while (sr.Peek() >-1)
                    {
                        String line = sr.ReadLine();
                        string[] usuario;

                        usuario = line.Split(',');

                        if (usuario[0].Equals(Nombre))
                        {
                            return usuario[0] + ',' + usuario[1] + ',' + usuario[2];
                        }
                    }


                }
                
            }
            catch
            {
                Console.WriteLine("El archivo no existe");
            }
            return null;
        }

        private static byte[] creacionHash(byte[] salt, string Password)
        {
            var prueba = new Rfc2898DeriveBytes(Password, salt, 1);
            byte[] hash = prueba.GetBytes(32);
            return hash;
        }

        private static string generarPassword()
        {
            ConsoleKeyInfo key;
            String Password=null;
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
                    if (Password.Length > 0)
                    {
                        Password.Remove(Password.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return Password;
        }
    }
}
