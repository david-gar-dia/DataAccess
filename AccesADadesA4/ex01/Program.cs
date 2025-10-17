using ex01.DADES;
using ex01.MODEL;

namespace ex01
{
    internal class Program
    {
        /// <summary>
        /// Muestra la tabla de opciones disponibles
        /// </summary>
        static public void MostrarTaula()
        {
            Console.Clear();
            Console.WriteLine("1- FIND BY PERSON REGISTER NUMBER");
            Console.WriteLine("2- FIND SALARY BY DNI");
            Console.WriteLine("3- ADD A PERSON");
            Console.WriteLine("4- EXIT");
        }

        /// <summary>
        /// Pide una opción del menu y ejecuta la función adecuada dependiendo de la respuesta
        /// o de la ausencia de una respuesta válida
        /// </summary>
        /// <param name="fileName">Es el fichero binario que se usará en cada una de las
        /// funciones seleccionables</param>
        static public void ManejarOpcions(string fileName)
        {
            IDAODirectAccess daoDirectAccess = DAODirectAccessFactory.CreateDAO(fileName);
            ConsoleKeyInfo option;
            do
            {
                MostrarTaula();
                option = Console.ReadKey();
                try
                {
                    switch (option.Key)
                    {
                        case ConsoleKey.D1:
                            Console.Clear();
                            DoFindByRegister(daoDirectAccess);
                            Console.Write("\nPress any key to continue: ");
                            Console.ReadKey();
                            break;
                        case ConsoleKey.D2:
                            Console.Clear();
                            DoFindByDni(daoDirectAccess);
                            Console.Write("\nPress any key to continue: ");
                            Console.ReadKey();
                            break;
                        case ConsoleKey.D3:
                            Console.Clear();
                            DoAddPerson(daoDirectAccess);
                            Console.Write("\nPress any key to continue: ");
                            Console.ReadKey();
                            break;
                        case ConsoleKey.D4:
                            daoDirectAccess.Close();
                            ExitProtocol();
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("This option is not valid");
                            Console.Write("\nPress any key to continue: ");
                            Console.ReadKey();
                            break;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"\nThere was a problem: \n{ex.Message}");
                    Console.Write("\nPress any key to continue: ");
                    Console.ReadKey();
                }
            } while (option.Key != ConsoleKey.D4);
        }

        /// <summary>
        /// Pide los parámetros necesarios para buscar un objeto Persona en el fichero
        /// binario a través del objeto DAODirectAccessImpl
        /// </summary>
        /// <param name="dao">El objeto DAODirectAccessImpl que usará para buscar a la
        /// persona en el fichero binario</param>
        static void DoFindByRegister(IDAODirectAccess dao)
        {
            Persona personToShow;
            int register;

            Console.Write("Insert the register to show: ");
            register = Convert.ToInt32(Console.ReadLine());

            personToShow = dao.GetByNumeroDeRegistre(register);

            Console.WriteLine(personToShow);
        }

        /// <summary>
        /// Pide los parámetros para buscar a una persona en un fichero binario a través del
        /// objeto DAODirectAccessImpl
        /// </summary>
        /// <param name="dao">El objeto DAODirectAccessImpl que usará para buscar a la
        /// persona en el fichero binario</param>
        /// <exception cref="ArgumentException">Se lanzará si el DNI dado no está en un formato
        /// apropiado</exception>
        static void DoFindByDni(IDAODirectAccess dao)
        {
            string dni;
            double salary;

            Console.Write("Insert the DNI to look for: ");
            dni = Console.ReadLine();

            // Aunque esta parte puede parecer copiada.
            // 1. Conozco el uso de _ para variables que quieres que sean descartadas
            // por haberlo visto usado en un fragmento de código el curso pasado.
            // 2. La función AsSpan la recomienda Visual Studio cuando intentas usar
            // Substring dentro de TryParse (Podéis comprobarlo reemplazando AsSpan por Substring)
            if (!int.TryParse(dni.AsSpan(0, 8), out _)) 
            {
                throw new ArgumentException("The DNI must be formed by 8 numbers and one letter");
            }

            salary = dao.GetByDNI(dni);

            if (salary != -1)
                Console.WriteLine($"{dni} - {salary}");
            else
                Console.WriteLine($"There was no person with the DNI {dni}");
        }

        /// <summary>
        /// Pide los parámetros para añadir una persona a un fichero binario y la añade
        /// mediante el objeto DAODirectAccessImpl
        /// </summary>
        /// <param name="dao">El objeto DAODirectAccessImpl que usará para
        /// agregar el objeto Persona al fichero</param>
        /// <exception cref="ArgumentException">Se lanza si el DNI no tiene un formato
        /// propio o el salario es menor a 0</exception>
        static void DoAddPerson(IDAODirectAccess dao)
        {
            Persona p;
            string dni;
            double salary;

            Console.Write("Introduce the DNI for the new person: ");
            dni = Console.ReadLine();

            if (!int.TryParse(dni.AsSpan(0, 8), out _))
            {
                throw new ArgumentException("The DNI must be formed by 8 numbers and one letter");
            }

            Console.Write("Introduce the salary for the new person: ");
            salary = Convert.ToDouble(Console.ReadLine());

            if(salary <= 0)
            {
                throw new ArgumentException("The salary must be greater than 0");
            }

            p = new Persona(dni, salary);

            dao.Add(p);

            Console.WriteLine("The new person was added successfully!");
        }

        /// <summary>
        /// Muestra una frase y puntos suspensivos que se acumulan cada medio segundo hasta 3 veces
        /// </summary>
        static void ExitProtocol()
        {
            Console.Clear();
            Console.Write("Exiting the program");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
        }
        static void Main(string[] args)
        {
            string fileName;

            Console.WriteLine("What file do you want to use for the reading?");
            fileName = Console.ReadLine();

            // File.Exists la he tenido que buscar por Google para hacer esta comprobación
            if(!File.Exists(fileName))
            {
                Console.Write("The given file does not exist, proceeding to create it");
                Thread.Sleep(500);
                Console.Write(".");
                Thread.Sleep(500);
                Console.Write(".");
                Thread.Sleep(500);
                Console.WriteLine(".");
                File.Create(fileName).Close();
                Console.WriteLine("File created successfully!");
                Console.Write("\nPress any key to continue: ");
                Console.ReadKey();
            }
            
            ManejarOpcions(fileName);

        }
    }
}
