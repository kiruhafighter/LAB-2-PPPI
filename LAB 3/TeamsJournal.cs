using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_3
{
    public class TeamsJournal
    {
        public List<TeamsJournalEntry> ListOfEditions = new List<TeamsJournalEntry>();
        public void TeamsJournalHandler<TKey>(object source, ResearchTeamChangedEventArgs<TKey> RTCHEA)
        {
            ListOfEditions.Add(new TeamsJournalEntry(RTCHEA.NameCollection, RTCHEA.InfoAboutEvent, RTCHEA.NamePropertyResearchTeam, RTCHEA.NumberRegisterResearchTeam));
        }
        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < ListOfEditions.Count; i++)
            {
                str += ListOfEditions[i].ToString();
                str += "  ";
            }
            return str;
        }
    }
}
