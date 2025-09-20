using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace MainForm.MODEL
{
    class RawTitle : IComparable
    {
        //Atributs
        private int index;
        private string id;
        private string title;
        private EnumTitleType type;
        private int release_year;
        private double seasons;
        private double imdb_score;
        private double imdb_votes;

        //Constructors
        public RawTitle(string csv)
        {
            MatchCollection fields;
            string auxString;

            fields = Regex.Matches(csv, @"(""[^""]*""|[^,]*)(,|$)");

            auxString = fields[0].Value;
            index = Convert.ToInt32(auxString.Substring(0, auxString.Length - 1));
            auxString = fields[1].Value;
            id = auxString.Substring(0, auxString.Length - 1);
            auxString = fields[2].Value;
            title = auxString.Substring(0, auxString.Length - 1);
            auxString = fields[3].Value;
            type = (EnumTitleType)Enum.Parse(typeof(EnumTitleType), fields[3].Value.Substring(0, auxString.Length - 1));
            auxString = fields[4].Value;
            release_year = Convert.ToInt32(auxString.Substring(0, auxString.Length - 1));
            
            auxString = fields[9].Value;
            if(auxString != ",")
                seasons = Convert.ToDouble(auxString.Substring(0, auxString.Length - 1));

            auxString = fields[11].Value;
            if (auxString != ",")
                imdb_score = Convert.ToDouble(auxString.Substring(0, auxString.Length - 1));

            auxString = fields[12].Value;
            if (auxString != ",")
                imdb_votes = Convert.ToDouble(auxString.Substring(0, auxString.Length - 1));
        }
        public RawTitle(int index, string id, string title, EnumTitleType type, int release_year, double seasons, double imdb_scores, double imdb_votes)
        {
            this.index = index;
            this.id = id;
            this.title = title;
            this.type = type;
            this.release_year = release_year;
            this.seasons = seasons;
            this.imdb_score = imdb_scores;
            this.imdb_votes = imdb_votes;
        }

        //Methods
        public int CompareTo(object? obj)
        {
            int result;
            RawTitle objAsTitle;

            if (obj == null || obj is not RawTitle)
                result = -1;
            else
            {
                objAsTitle = obj as RawTitle;
                result = id.CompareTo(objAsTitle.id);
            }

            return result;
        }
        public override string ToString()
        {
            return $"{index},{id},{title},{type},{release_year},{seasons},{imdb_score},{imdb_votes}";
        }
    }

    enum EnumTitleType
    {
        SHOW,
        MOVIE
    }
}
