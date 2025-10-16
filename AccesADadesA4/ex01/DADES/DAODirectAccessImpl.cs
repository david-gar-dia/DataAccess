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

        public Persona GetByNumeroDeRegistre(int nRegistre)
        {
            Persona result;
            string dni;
            double sous;

            if (nRegistre > fs.Length / 18)
                throw new ArgumentException("There doesn't seem to exist a register that high");

            fs.Seek(18 * (nRegistre - 1), SeekOrigin.Begin);

            dni = br.ReadString();
            sous = br.ReadDouble();

            result = new Persona(dni, sous);

            fs.Position = 0;

            return result;
        }
        public double GetByDNI(string dni)
        {
            double result;
            string currentDni;


            currentDni = br.ReadString();
            fs.Seek(8, SeekOrigin.Current);

            while (fs.Position < fs.Length && currentDni != dni)
            {
                currentDni = br.ReadString();
                fs.Seek(8, SeekOrigin.Current);
            }

            fs.Seek(-8, SeekOrigin.Current);
            result = br.ReadDouble();

            fs.Seek(0, SeekOrigin.Begin);

            return result;
        }
        public void Add(Persona p)
        {
            fs.Seek(fs.Length, SeekOrigin.Begin);

            bw.Write(p.Dni);
            bw.Write(p.Sous);

            fs.Seek(0, SeekOrigin.Begin);
        }
        public void Close()
        {
            br.Close();
            bw.Close();
            fs.Close();
        }
    }
}
