using System;

namespace LAB_3
{
    public enum Revision { Remove, Replace, Property }
    public delegate void ResearchTeamChangedHandler<TKey>(object source, ResearchTeamChangedEventArgs<TKey> args);
    public class ResearchTeamChangedEventArgs<TKey> : System.EventArgs
    {
        public string NameCollection { get; set; }

        public Revision InfoAboutEvent { get; set; }
        public string NamePropertyResearchTeam { get; set; }

        public int NumberRegisterResearchTeam { get; set; }
        public ResearchTeamChangedEventArgs(string str, Revision rev, string str1, int i)
        {
            NameCollection = str;
            InfoAboutEvent = rev;
            NamePropertyResearchTeam = str1;
            NumberRegisterResearchTeam = i;
        }

        public override string ToString()
        {
            return String.Format("Name of collection - {0},\n Information about change : {1}\n Name of class archTeam, which is source of changing: {2}\n Number of registration ResearchTeam {3}", NameCollection, InfoAboutEvent, NamePropertyResearchTeam, NumberRegisterResearchTeam);
        }
    }
}
