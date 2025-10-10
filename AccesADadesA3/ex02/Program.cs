namespace ex02
{
    internal class Program
    {
        /// <summary>
        /// Muestra formateado en texto una lista de strings dada debajo de un encabezado también dado
        /// </summary>
        /// <param name="what2Show">El encabezado bajo el que se mostrará</param>
        /// <param name="showable">La lista que se mostrará</param>
        static void Mostrar(string what2Show, IEnumerable<string> showable)
        {
            Console.WriteLine($"{what2Show}:");
            foreach (string line in showable)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            StreamReaderEnumerable sre = new StreamReaderEnumerable("DADES.TXT");
            int caseA = sre.Count(n => true); // Valdría sre.Count
            IEnumerable<string> caseB = sre.Where(n => n.StartsWith("SE"));
            int caseC = sre.Count(n => n.Contains("RA"));
            IEnumerable<string> caseD = sre.OrderBy(n => n);
            IEnumerable<string> caseE = sre.Skip(2).Take(3).Select(n => n.ToLower());
            IEnumerable<string> caseF = sre.Select(n => n.Substring(0, n.IndexOf(" ")));
            // Consulta LINQ que recoge todas las líneas y las muestra en leetspeak
            IEnumerable<string> caseG = sre.Select(n => n.Replace("E", "3").Replace("A", "4").Replace("I", "1"));

            // Consulta LINQ que muestra solo la primera y última letra de cada linea sin repetidos
            IEnumerable<string> caseH = sre.Select(n => n.Substring(0, 1) + n.Substring(n.IndexOf(" ") - 1)).Distinct();

            // Consulta LINQ que comprueba si alguna línea tiene una letra A en la posición que resulta del ratio
            // entero entre longitud y apariciones de la letra A
            bool caseI = sre.Any(n => n[(n.Length / n.Count(n => n == 'A'))] == 'A');

            // Consulta LINQ que de entre todas las líneas con más de dos A's, elimina en conjunto entre el inicio y el final
            // de la linea y de manera equitativa el ratio entero entre longitud de linea y apariciones de la letra A
            IEnumerable<string> caseJ = sre.Where(n => n.Count(n => n == 'A') > 2).Select(n => n.Substring(n.Length / n.Count(n => n == 'A') / 2, n.Length - n.Length / n.Count(n => n == 'A')));

            Console.WriteLine($"a) Quantes linies té el fitxer\n{caseA}\n");
            Mostrar("b) Quines linies comencen per \"SE\".", caseB);
            Console.WriteLine($"c) Quantes linies contenen el substring \"RA\".\n{caseC}\n");
            Mostrar("d) Mostrar alfabèticament totes les linies.", caseD);
            Mostrar("e) Mostrar des de la 3a fins la 5a linia, en minuscules.", caseE);
            Mostrar("f) Mostrar la primera paraula de cada element de l'enumeració.", caseF);
            Mostrar("g) Mostrar todas las lineas en formato l33tsp3ak.", caseG);
            Mostrar("h) Mostrar todas las líneas pero solo la primera y última letra de la primera palabra. Sin repetidos.", caseH);
            Console.WriteLine("i) Mostrar si existe alguna linea que contenga una A en la posición que resulta de la longitud de la linea dividida entre el número de A's de la linea");
            if (caseI)
                Console.WriteLine("True\n");
            else
                Console.WriteLine("False\n");
            Mostrar("j) Mostrar de todas las lineas que contengan más de dos A's, un substring del cual se eliminen entre el principio y el final, el total resultante de la longitud de la linea entre su número total de A's.", caseJ);
        }
    }
}
