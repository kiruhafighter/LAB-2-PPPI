using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_3
{
    public class TeamsJournalEntry
    {
        public string NameCollection { get; set; }
        public Revision TypeOfEvent { get; set; }
        public string PropertyRT { get; set; }
        public int NumberOfRegistration { get; set; }

        public TeamsJournalEntry(string n, Revision r, string s, int n1)
        {
            NameCollection = n;
            TypeOfEvent = r;
            PropertyRT = s;
            NumberOfRegistration = n1;
        }
        public override string ToString()
        {
            return String.Format($"Name of the Research Team - {NameCollection},\n Event type: {TypeOfEvent} \n Information: {PropertyRT} \n Registration number {NumberOfRegistration} \n" );
        }
    }
}
