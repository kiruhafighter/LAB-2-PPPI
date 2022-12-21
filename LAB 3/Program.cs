
using System;
using System.Linq;

namespace LAB_3
{
    public enum TimeFrame { Year, TwoYears, Long };

    public class Program
    {
        //private static string GenerateKey(ResearchTeam rs)
        //{
        //    return rs.ResearchTopic;
        //}

        private static int GenerateKey(ResearchTeam rs)
        {
            return rs.RegistrationNumber;
        }

        static void Main()
        {
            //1

            Console.WriteLine("\n1)\n*************************************************\n\n");
            
            ResearchTeamCollection<int> researchTeamString = new ResearchTeamCollection<int>(GenerateKey);
            researchTeamString.CollectionName = "111";
            TeamsJournal teamJournal = new TeamsJournal();
            researchTeamString.ResearchTeamsChanged += teamJournal.TeamsJournalHandler;

            ResearchTeam[] RT = new ResearchTeam[3];
            RT[0] = new ResearchTeam("North America Climate", "Nielsen", 1234, TimeFrame.TwoYears);
            RT[1] = new ResearchTeam("Atlantic Ocean Biosphere", "Ipsos", 9875, TimeFrame.Long);
            RT[2] = new ResearchTeam("Air pollution", "Kantar ", 500, TimeFrame.Year);
            researchTeamString.AddResearchTeams(RT);


            researchTeamString.AddDeafaults();
            researchTeamString.ChangeTopic(RT[0], "Tigers' population");
            researchTeamString.ChangeDuration(RT[1], TimeFrame.Year);
            researchTeamString.Remove(RT[1]);
            researchTeamString.Replace(RT[0], RT[2]);
            RT[0].researchDuration = TimeFrame.Year;



            //Console.WriteLine("Publications for last 3 years:\n");
            //RT[0].AddMembers(new Person("New", "Surname", new DateTime(1999, 12, 18)), new Person("New", "Surname", new DateTime(1999, 12, 10)));
            //RT[0].AddPapers(new Paper("New", new Person(), new DateTime(2022, 10, 10)), new Paper("New", new Person(), new DateTime(2021, 10, 12)));
            ////RT[0].PublicationList[0].PublicationDate = new DateTime(2021, 04, 10);
            //foreach (Paper publication in RT[0].GetLastYearsPublications(3))
            //    Console.WriteLine(publication.ToString());


            Console.WriteLine(teamJournal.ToString());

            Console.WriteLine("-----------------------\n");

            Console.WriteLine("****************************************");
            Console.ReadLine();
        }


    }
}
