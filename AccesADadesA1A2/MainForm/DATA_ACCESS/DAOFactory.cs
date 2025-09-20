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
        static public IDAO CreateDAOImpl(EnumDAOType daoType, string connection = "")
        {
            IDAO result;

            switch(daoType)
            {
                case EnumDAOType.CSV:
                    if(connection != "")
                        result = new DAOImplCSV(connection);
                    else
                        result = new DAOImplCSV();
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
