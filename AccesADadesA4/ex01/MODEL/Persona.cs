using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex01.MODEL
{
    internal class Persona
    {
        private string dni;
        private double sous;

        public Persona(string dni, double sous)
        {
            this.dni = dni;
            this.sous = sous;
        }

        public string Dni{ get{ return dni; } }
        public double Sous{ get { return sous; } }

        /// <summary>
        /// Muestra el objeto persona en un formato legible
        /// </summary>
        /// <returns>El string legible</returns>
        public override string ToString()
        {
            return $"{dni} - {sous}";
        }
    }
}
