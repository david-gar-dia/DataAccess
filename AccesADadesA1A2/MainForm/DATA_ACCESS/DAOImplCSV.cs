using MainForm.MODEL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MainForm.DATA_ACCESS
{
    internal class DAOImplCSV : IDAO
    {
        //Atributs
        private string filename;

        //Constructor
        public DAOImplCSV() : this("raw_titles.csv"){}
        public DAOImplCSV(string filename)
        {
            this.filename = filename;
        }

        //Métodes
        public int SelectByGenre(string genre, string outputfile)
        {
            MatchCollection fields, curGenres;
            StringBuilder sb;
            string[] genresArray;
            string line;
            int result = 0, indexGenre;

            using(StreamReader sr = new StreamReader(filename))
            {
                using(StreamWriter sw = new StreamWriter("ResultGenresSearch.txt"))
                {
                    line = sr.ReadLine();
                    sw.WriteLine(line);
                    line = sr.ReadLine();
                    while(line != null)
                    {
                        indexGenre = 0;
                        fields = Regex.Matches(line, @"""[^""]*""|[^,]+");

                        curGenres = Regex.Matches(fields[7].Value, @"'[^']*'");
                        genresArray = new string[curGenres.Count];

                        foreach(Match genreMatch in curGenres)
                        {
                            genresArray[indexGenre] = genreMatch.Value;
                            indexGenre++;
                        }

                        if(genresArray.Contains("'"+genre+"'"))
                        {
                            sb = new StringBuilder();

                            sb.Append(fields[0].Value);
                            sb.Append(";");
                            sb.Append(fields[1].Value);
                            sb.Append(";");
                            sb.Append(fields[2].Value);
                            sb.Append(";");
                            sb.Append(fields[7].Value);

                            sw.WriteLine(sb.ToString());
                            result++;
                        }

                        line = sr.ReadLine();
                    }
                }
            }

            return result;
        }
        public RawTitle SelectByIndex(int index)
        {
            MatchCollection fields;
            RawTitle result;
            string line;
            int curIndex;
            bool found = false, passed = false;

            using (StreamReader sr = new StreamReader(filename))
            {
                line = sr.ReadLine();
                line = sr.ReadLine();
                while (line != null && !found && !passed)
                {
                    fields = Regex.Matches(line, @"""[^""]*""|[^,]+");
                    curIndex = Convert.ToInt32(fields[0].Value);

                    if (curIndex > index)
                        passed = true;
                    else if (curIndex == index)
                        found = true;
                    else
                        line = sr.ReadLine();
                }

                if (found)
                    result = new RawTitle(line);
                else
                    result = null;
            }

            return result;
        }
        public RawTitle SelectById(int id)
        {
            MatchCollection fields;
            RawTitle result;
            string line;
            int curId;
            bool found = false;

            using (StreamReader sr = new StreamReader(filename))
            {
                line = sr.ReadLine();
                line = sr.ReadLine();
                while (line != null && !found)
                {
                    fields = Regex.Matches(line, @"""[^""]*""|[^,]+");
                    curId = Convert.ToInt32(fields[1].Value.Substring(2));

                    if (curId == id)
                        found = true;
                    else
                        line = sr.ReadLine();
                }

                if (found)
                    result = new RawTitle(line);
                else
                    result = null;
            }

            return result;
        }
        public RawTitle[] ReadTitles(int index, int length)
        {
            RawTitle[] result = new RawTitle[length];
            MatchCollection fields;
            string line;
            int count = 0, curIndex;
            bool found = false, passed = false;

            using (StreamReader sr = new StreamReader(filename))
            {
                line = sr.ReadLine();
                line = sr.ReadLine();
                while (line != null && !found && !passed)
                {
                    fields = Regex.Matches(line, @"""[^""]*""|[^,]+");
                    curIndex = Convert.ToInt32(fields[0].Value);

                    if (curIndex > index)
                        passed = true;
                    else if (curIndex == index)
                        found = true;
                    else
                        line = sr.ReadLine();
                }

                if (found)
                {
                    while (count < length && line != null)
                    {
                        result[count] = new RawTitle(line);
                        count++;
                        line = sr.ReadLine();
                    }

                    if (count < length)
                        result = result.ToArray();
                }
                else
                    result = null;
            }

            return result;
        }
        public void PreMerge(RawTitle[] titles, string outputFileName)
        {
            if (outputFileName == "")
                outputFileName = $"preMerge_{titles.Count()}.txt";

            Array.Sort(titles);
            using(StreamWriter sw = new StreamWriter(outputFileName))
            {
                foreach (RawTitle title in titles)
                    sw.WriteLine(title);
            }
        }

        public int Merge(string inputFileName1, string inputFileName2, string outputFileName)
        {
            StreamReader sr1 = new StreamReader(inputFileName1);
            StreamReader sr2 = new StreamReader(inputFileName2);
            RawTitle curTitle1, curTitle2;
            string line1, line2;
            int curCompareResult, result = 0;

            if (outputFileName == "")
                outputFileName = "MergeResult.txt";

            using (StreamWriter sw = new StreamWriter(outputFileName))
            {
                line1 = sr1.ReadLine();
                line2 = sr2.ReadLine();
                
                while(line1 != null && line2 != null)
                {
                    curTitle1 = new RawTitle(line1);
                    curTitle2 = new RawTitle(line2);

                    curCompareResult = curTitle1.CompareTo(curTitle2);

                    if(curCompareResult == 0)
                    {
                        sw.WriteLine(line1);
                        sw.WriteLine(line2);

                        line1 = sr1.ReadLine();
                        line2 = sr2.ReadLine();

                        result += 2;
                    }
                    else if(curCompareResult < 0)
                    {
                        sw.WriteLine(line1);
                        line1 = sr1.ReadLine();

                        result++;
                    }
                    else
                    {
                        sw.WriteLine(line2);
                        line2 = sr2.ReadLine();

                        result++;
                    }
                }

                while(line1 != null)
                {
                    sw.WriteLine(line1);
                    line1 = sr1.ReadLine();

                    result++;
                }

                while(line2 != null)
                {
                    sw.WriteLine(line2);
                    line2 = sr2.ReadLine();

                    result++;
                }

                return result;
            }
        }
    }
}
