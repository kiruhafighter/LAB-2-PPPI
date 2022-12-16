using System;
using System.Collections;
using System.Collections.Generic;

namespace LAB_3
{
    class ResearchTeamEnumenator : IEnumerator
    {
        private List<Person> ProjectMembers { get; set; }
        private List<Paper> PublicationList { get; set; }

        int position = -1;
        
        public ResearchTeamEnumenator(List<Person> projectMembers, List<Paper> publicationList)
        {
            ProjectMembers = projectMembers;
            PublicationList = publicationList;
        }

        public object Current
        {
            get
            {
                if (position == -1 || position >= ProjectMembers.Count)
                    throw new ArgumentException();

                for (int i = 0; i < PublicationList.Count; i++)
                    if (ProjectMembers[position] == PublicationList[i].InformationAboutAutor)
                        return ProjectMembers[position];

                return null;
            }
        }

        public bool MoveNext()
        {
            if (position < ProjectMembers.Count - 1)
            {
                position++;
                return true;
            }
            else
                return false;
        }

        public void Reset() => position = -1;
    }
}
