using ex01.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01.DADES
{
    internal class DAODirectAccessImpl : IDAODirectAccess
    {
        private FileStream fs;
        private BinaryWriter bw;
        private BinaryReader br;

        public DAODirectAccessImpl(string file)
        {
            fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            bw = new BinaryWriter(fs);
            br = new BinaryReader(fs);
        }

        /// <summary>
        /// Mueve el puntero del fichero binario hasta la posición del registro dado
        /// y crea y muestra un nuevo objeto Persona usando los siguientes 18 bytes
        /// </summary>
        /// <param name="nRegistre">El número de registro que usaremos para mostrar</param>
        /// <returns>El objeto Persona que se ha creado para mostrar</returns>
        /// <exception cref="ArgumentException">Se lanza si el número de registro es menor a 0
        /// o mayor al número de registros en el fichero</exception>
        public Persona GetByNumeroDeRegistre(int nRegistre)
        {
            Persona result;
            string dni;
            double sous;

            if (nRegistre <= 0 || nRegistre > fs.Length / 18)
                throw new ArgumentException("The register number must be an unsigned integer within the limits of the collection");

            fs.Seek(18 * (nRegistre - 1), SeekOrigin.Begin);

            dni = br.ReadString();
            sous = br.ReadDouble();

            result = new Persona(dni, sous);

            fs.Position = 0;

            return result;
        }

        /// <summary>
        /// Busca en grupos de 18 bytes si los 10 primeros corresponden al DNI proporcionado
        /// y muestra por consola los siguientes 8 en formate double en caso positivo
        /// </summary>
        /// <param name="dni">El DNI por el cuál se buscará en el fichero</param>
        /// <returns>El salario en formate double atribuido al DNI dado o -1 en caso que no exista</returns>
        public double GetByDNI(string dni)
        {
            double result = -1;
            string currentDni;
            bool found = false;


            currentDni = br.ReadString();
            found = currentDni.Equals(dni);
            fs.Seek(8, SeekOrigin.Current);

            while (fs.Position < fs.Length && !found)
            {
                currentDni = br.ReadString();
                found = currentDni.Equals(dni);
                fs.Seek(8, SeekOrigin.Current);
            }

            if(found)
            {
                fs.Seek(-8, SeekOrigin.Current);
                result = br.ReadDouble();
            }

            fs.Seek(0, SeekOrigin.Begin);

            return result;
        }

        /// <summary>
        /// Añade un nuevo conjunto de 18 bytes al final del fichero que conforman DNI y salario a partir
        /// de un objeto Persona proporcionado
        /// </summary>
        /// <param name="p">El objeto persona proporcionado para escribir</param>
        /// <exception cref="ArgumentException">Se lanza si el DNI del objeto persona ya existe
        /// en los registros del fichero</exception>
        public void Add(Persona p)
        {
            if (GetByDNI(p.Dni) != -1)
                throw new ArgumentException("The person to add does already exist.");

            fs.Seek(fs.Length, SeekOrigin.Begin);

            bw.Write(p.Dni);
            bw.Write(p.Sous);

            fs.Seek(0, SeekOrigin.Begin);
        }

        /// <summary>
        /// Libera los recursos usados para la lectura binaria de manera segura
        /// </summary>
        public void Close()
        {
            br.Close();
            bw.Close();
            fs.Close();
        }
    }
}
