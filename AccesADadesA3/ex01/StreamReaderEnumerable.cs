using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01
{
    /// <summary>
    /// Clase que hace enumerable un fichero a través de su filename
    /// </summary>
    public class StreamReaderEnumerable : IEnumerable<string>
    {
        // Atributs
        private string fileName;

        // Constructors
        public StreamReaderEnumerable(string fileName)
        {
            this.fileName = fileName;
        }

        // Métodes

        /// <summary>
        /// Enumera cada linea de un fichero utilizando un objeto StreamReader
        /// </summary>
        /// <returns>El IEnumerator del fichero</returns>
        public IEnumerator<string> GetEnumerator()
        {
            StreamReader sr = new StreamReader(fileName);
            string line;

            while (sr.Peek() != -1)
                yield return sr.ReadLine();
            sr.Close();
        }

        /// <summary>
        /// Enumera cada linea de un fichero utilizando un objeto StreamReader
        /// </summary>
        /// <returns>El IEnumerator del fichero</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
