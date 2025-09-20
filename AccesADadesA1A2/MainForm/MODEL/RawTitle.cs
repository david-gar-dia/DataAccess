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
    internal class RawTitle : IComparable
    {
        //Atributs
        private int index;
        private int id;
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

            fields = Regex.Matches(csv, @"(?: ""[^""] * "" | [^,] *)(?:,|$)";

            index = Convert.ToInt32(fields[0].Value);
            id = Convert.ToInt32(fields[1].Value);
            title = fields[2].Value;
            type = (EnumTitleType) Enum.Parse(typeof(EnumTitleType), fields[3].Value);
            release_year = Convert.ToInt32(fields[4].Value);
            seasons = Convert.ToDouble(fields[9].Value);
            imdb_score = Convert.ToDouble(fields[11].Value);
            imdb_votes = Convert.ToDouble(fields[12].Value);
        }
        public RawTitle(int index, int id, string title, EnumTitleType type, int release_year, double seasons, double imdb_scores, double imdb_votes)
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
    }

    enum EnumTitleType
    {
        SHOW,
        MOVIE
    }
}
