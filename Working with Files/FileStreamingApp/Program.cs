using System;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace FileStreamingApp
{
    class Program
    {
        static string path = @"C:\Users\aygun\Documents\My Web Sites\FileStreamFolder\myTextDoc.txt";
        static string pathNew = @"C:\Users\aygun\Documents\My Web Sites\FileStreamFolder\myTextDocNew.txt";
        static async Task Main(string[] args)
        {
            try
            {
                Char[] buffer;
                // Read the document text
                using (var sr = new StreamReader(path))
                {
                    buffer = new char[(int)sr.BaseStream.Length];
                    await sr.ReadAsync(buffer, 0, (int)sr.BaseStream.Length);
                }
                // Log text to the console
                Console.WriteLine(new String(buffer));

                string[] students = new string[8];
                int index = 0;
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < buffer.Length; i++)
                {
                    // first get each line ending with ';' and build a string.
                    if (buffer[i].ToString() != ";")
                    {
                        stringBuilder.Append(buffer[i]);
                    }
                    // now add each string to the students array.
                    else
                    {
                        students[index] = stringBuilder.ToString().Trim();
                        stringBuilder.Clear();
                        index++;
                    }
                }
                SplitEachEntry(students);
                Console.Read();
            }
            catch (DirectoryNotFoundException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch (System.IO.IOException ex)
            {
                Console.Write(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e);
            }
        }

        // spits each string into 4 pieces.
        static void SplitEachEntry(string[] students)
        {
            string[] eachStudent = new string[3];
            char[] separator = { '-' };
            int scoreTotal = 0;
            string name = "";

            Console.WriteLine("*****************************************************\nRecords will be Saved to the following path:\n"
                + pathNew + "\n");

            for (int i = 0; i < students.Length; i++)
            {
                eachStudent = students[i].Split(separator, 4,StringSplitOptions.RemoveEmptyEntries);
                for (int j = 1; j < eachStudent.Length; j++)
                {
                    name = eachStudent[0];
                    scoreTotal += Convert.ToInt32(eachStudent[j]);
                }
                WriteToFile(name, CalculateAverage(scoreTotal).ToString());
                Console.WriteLine("\n"+ (1+i)+"." +"Student Name : " + name + "\n  Average Score: " + CalculateAverage(scoreTotal)+"\n");
                scoreTotal = 0;
            }
        }

        // calculate the ave score
        static int CalculateAverage(int score)
        {
            return score / 3;
        }

        // write each student and the ave score to a file
        static void WriteToFile(string name, string score)
        {
            try
            {
                // Write each directory name to a file.
                using (StreamWriter sw = new StreamWriter(pathNew,true))
                {
                    sw.WriteLineAsync("Name: " + name + " - Average Score: " + score);
                }
            }
            catch(DirectoryNotFoundException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch (System.IO.IOException ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}
