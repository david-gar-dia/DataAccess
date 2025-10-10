using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex02
{
    /// <summary>
    /// Clase que hace enumerable un fichero a través de su filename
    /// </summary>
    internal class StreamReaderEnumerable : IEnumerable<string>
    {
        // Atributs
        private string fileName;

        // Constructors
        public StreamReaderEnumerable(string fileName)
        {
            this.fileName = fileName;
        }

        /// <summary>
        /// Enumera cada linea de un fichero utilizando la clase MyStreamReaderEnumerator
        /// </summary>
        /// <returns>El IEnumerator del fichero</returns>
        public IEnumerator<string> GetEnumerator()
        {
            return new MyStreamReaderEnumerator(fileName);
        }

        /// <summary>
        /// Enumera cada linea de un fichero utilizando la clase MyStreamReaderEnumerator
        /// </summary>
        /// <returns>El IEnumerator del fichero</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        // Clases
        /// <summary>
        /// Enumera todos los elementos de un fichero de texto
        /// </summary>
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
            /// <summary>
            /// Avanza al siguiente elemento en la enumeración de lineas del fichero mediante el uso
            /// del atribute de tipo StreamReader
            /// </summary>
            /// <returns>Devuelve true si se ha podido avanzar a un siguiente elemento o false si no hay
            /// más elementos a los que avanzar</returns>
            public bool MoveNext()
            {
                bool result = true;

                if (sR.Peek() != -1)
                    current = sR.ReadLine();
                else
                    result = false;

                return result;
            }

            /// <summary>
            /// Libera los recursos utilizados para enumerar el fichero
            /// </summary>
            public void Dispose()
            {
                sR.Close();
            }

            /// <summary>
            /// Devuelve los valores de la clase a su estado inicial para enumerar los elementos de nuevo
            /// </summary>
            public void Reset()
            {
                sR.DiscardBufferedData();
                sR.BaseStream.Seek(0, SeekOrigin.Begin);
                current = null;
            }
        }
    }
}
