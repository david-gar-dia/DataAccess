using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01
{
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
        public IEnumerator<string> GetEnumerator()
        {
            StreamReader sr = new StreamReader(fileName);
            string line;

            while (sr.Peek() != -1)
                yield return sr.ReadLine();
            sr.Close();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
