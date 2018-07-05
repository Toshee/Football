using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WPFApp.Model
{
    public class Team
    {
        public string TeamName { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsTaken { get; set; }
        public int ScoreDifference { get; set; }

        public Team(string teamName, int goalsScored, int goalsTaken, int scoreDifference)
        {
            TeamName = teamName;
            GoalsScored = goalsScored;
            GoalsTaken = goalsTaken;
            ScoreDifference = scoreDifference;
        }

        public static IEnumerable<Team> ReadCSV()
        {
            bool isheader = true;
            List<string> headers = new List<string>();
            // We change file extension here to make sure it's a .csv file.
            // TODO: Error checking.
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "football.csv");


            string[] lines = File.ReadAllLines(path);

            // lines.Select allows me to project each line as a Team. 
            // This will give me an IEnumerable<Team> back.
            return lines.Select(line =>            {
                string[] data = line.Split(',');
                // We return a person with the data in order.
                if (isheader)
                {
                    isheader = false;
                    headers = data.ToList();
                }
                else if(data!=null && (data.Length % headers.Count == 0))
                {
                    return new Team(data[0], Convert.ToInt32(data[5]), Convert.ToInt32(data[7]),
                        Math.Abs(Convert.ToInt32(data[5]) - Convert.ToInt32(data[7])));
                }
                return null;
            });
        }
        
    }
}
