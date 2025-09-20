using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainForm.DATA_ACCESS
{
    internal class DAOFactory
    {
        //Atributs

        //Constructors

        //Métodes
        static IDAO createDAOImpl(EnumDAOType daoType, string connection)
        {
            IDAO result;

            switch(daoType)
            {
                case EnumDAOType.CSV:
                    result = new DAOImplCSV(connection);
                    break;
                default:
                    throw new ArgumentException("The given DAO Type was not withing the valid parameters");
            }

            return result;
        }
    }

    enum EnumDAOType
    {
        CSV
    }
}
