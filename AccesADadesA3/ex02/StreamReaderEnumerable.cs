using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex02
{
    internal class StreamReaderEnumerable : IEnumerable<string>
    {
        // Atributs
        private string fileName;

        // Constructors
        public StreamReaderEnumerable(string fileName)
        {
            this.fileName = fileName;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return new MyStreamReaderEnumerator(fileName);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        // Clases
        internal class MyStreamReaderEnumerator : IEnumerator<string>
        {
            // Atributs
            private StreamReader sR;
            private string current;

            // Propietats
            public string Current { get { return current; } }
            object IEnumerator.Current => Current;

            // Constructors
            public MyStreamReaderEnumerator(string filename)
            {
                sR = new StreamReader(filename);
            }

            // Métodes
            public bool MoveNext()
            {
                bool result = true;

                if (sR.Peek() != -1)
                    current = sR.ReadLine();
                else
                    result = false;

                return result;
            }
            public void Dispose()
            {
                sR.Close();
            }

            public void Reset()
            {
                sR.DiscardBufferedData();
                sR.BaseStream.Seek(0, SeekOrigin.Begin);
                current = null;
            }
        }
    }
}
