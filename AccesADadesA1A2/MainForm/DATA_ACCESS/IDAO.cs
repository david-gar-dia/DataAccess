using MainForm.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainForm.DATA_ACCESS
{
    internal interface IDAO
    {
        public int SelectByGenre(string genre, string outputfile);
        public RawTitle SelectByIndex(int index);
        public RawTitle SelectById(int id);
        public RawTitle[] ReadTitles(int index, int length);
    }
}
