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

        static public IDAODirectAccess CreateDAO(string fileName)
        {
            return new DAODirectAccessImpl(fileName);
        }
    }
}
