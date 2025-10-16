using ex01.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01.DADES
{
    internal interface IDAODirectAccess
    {
        Persona GetByNumeroDeRegistre(int nRegistre);
        double GetByDNI(string dni);
        void Add(Persona p);
        void Close();
    }
}
