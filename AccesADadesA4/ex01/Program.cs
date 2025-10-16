using ex01.DADES;
using ex01.MODEL;

namespace ex01
{
    internal class Program
    {
        static public void MostrarTaula()
        {
            Console.Clear();
            Console.WriteLine("1- FIND BY PERSON REGISTER NUMBER");
            Console.WriteLine("2- FIND SALARY BY DNI");
            Console.WriteLine("3- ADD A PERSON");
            Console.WriteLine("4- EXIT");
        }
        static public void ManejarOpcions(string fileName)
        {
            IDAODirectAccess daoDirectAccess = DAODirectAccessFactory.CreateDAO(fileName);
            ConsoleKeyInfo option;
            do
            {
                MostrarTaula();
                option = Console.ReadKey();

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
            } while (option.Key != ConsoleKey.D4);
        }
        static void DoFindByRegister(IDAODirectAccess dao)
        {
            Persona personToShow;
            int register;

            Console.Write("Insert the register to show: ");
            register = Convert.ToInt32(Console.ReadLine());

            personToShow = dao.GetByNumeroDeRegistre(register);

            Console.Write(personToShow);
        }
        static void DoFindByDni(IDAODirectAccess dao)
        {
            string dni;
            double salary;

            Console.Write("Insert the DNI to look for: ");
            dni = Console.ReadLine();

            int.Parse(dni.Substring(0, 8));

            dao.GetByDNI(dni);
        }
        static void DoAddPerson(IDAODirectAccess dao)
        {
            Persona p;
            string dni;
            double salary;

            Console.Write("Introduce the DNI for the new person: ");
            dni = Console.ReadLine();
            Console.WriteLine("Introduce the salary for the new person: ");
            salary = Convert.ToDouble(Console.ReadLine());

            p = new Persona(dni, salary);

            dao.Add(p);
        }
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

            ManejarOpcions(fileName);
        }
    }
}
