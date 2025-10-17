using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01.DADES
{
    internal class DAODirectAccessFactory
    {
        public DAODirectAccessFactory() { }

        /// <summary>
        /// Crea una nueva instancia IDAODirectAccess de lectura binaria en el fichero dado como
        /// parámetro
        /// </summary>
        /// <param name="fileName">Es el fichero que leerá el objeto IDAODirectAccess</param>
        /// <returns>El objeto IDAODirectAccess</returns>
        static public IDAODirectAccess CreateDAO(string fileName)
        {
            return new DAODirectAccessImpl(fileName);
        }
    }
}
