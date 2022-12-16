using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;

namespace LAB_3
{

    class ResearchTeam : Team, INameAndCopy, IEnumerable , IComparer<ResearchTeam> , INotifyPropertyChanged
    {
        private string researchTopic;
        private TimeFrame researchDuration;
        private List<Person> projectMembers;
        private List<Paper> publicationList;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged (string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs (name));
        }

        public string ResearchTopic
        {
            get { return researchTopic; }
            set {
                    researchTopic = value;
                    OnPropertyChanged("Theme"); 
                }
        }

        public TimeFrame ResearchDuration
        {
            get { return researchDuration; }
            set { 
                    researchDuration = value;
                    OnPropertyChanged("Duration");
                }
        }

        public List<Person> ProjectMembers
        {
            get { return projectMembers; }
            set { projectMembers = value; }
        }

        public List<Paper> PublicationList
        {
            get { return publicationList; }
            set { publicationList = value; }
        }

        public ResearchTeam(string researchTopic, string organisationName, int registrationNumber, TimeFrame researchDuration, List<Person> projectMembers, List<Paper> publicationList)
        {
            ResearchTopic = researchTopic;
            OrganisationName = organisationName;
            RegistrationNumber = registrationNumber;
            ResearchDuration = researchDuration;
            ProjectMembers = projectMembers;
            PublicationList = publicationList;
        }

        public ResearchTeam(string researchTopic, string organisationName, int registrationNumber, TimeFrame researchDuration) 
            : base(organisationName, registrationNumber)
        {
            ResearchTopic = researchTopic;
            ResearchDuration = researchDuration;
            ProjectMembers = new List<Person>();
            PublicationList = new List<Paper>();
        }

        public ResearchTeam()
        {
            ResearchTopic = "History";
            OrganisationName = "Tork Inc";
            RegistrationNumber = 3;
            ResearchDuration = TimeFrame.TwoYears;

            ProjectMembers = new List<Person>
            {
                new Person(),
                new Person()
            };

            PublicationList = new List<Paper>
            {
                new Paper("Hovar", new(), new DateTime()),
                new Paper("Norn", new(), new DateTime()),
                new Paper("Dew", new(), new DateTime())
            };
        }

        public Paper LatestPublication
        {
            get
            {
                if (PublicationList == null || PublicationList.Count == 0)
                    return null;

                int PublicationNumber = 0;

                DateTime LatestDate = PublicationList[0].PublicationDate;

                for (int i = 1; i < PublicationList.Count; i++)
                {
                    if (LatestDate <= PublicationList[i].PublicationDate)
                    {
                        LatestDate = PublicationList[i].PublicationDate;
                        PublicationNumber = i;
                    }
                }

                return PublicationList[PublicationNumber];
            }
        }

        public Team BaseTeamValue
        {
            get
            {
                return new Team(OrganisationName, RegistrationNumber);
            }

            set
            {
                
                organisationName = value.OrganisationName;
                registrationNumber = value.RegistrationNumber;
            }
        }

        public void AddPapers(params Paper[] publications)
        {
            PublicationList.AddRange(publications);
        }

        public void AddMembers(params Person[] members)
        {
            ProjectMembers.AddRange(members);
        }

        public override string ToString()
        {
            string list1 = null;

            if (ProjectMembers == null || ProjectMembers.Count == 0)
                list1 = "null, add new\n\n";
            else
                foreach (Person person in ProjectMembers) list1 += person.ToString();

            string list2 = null;

            if (PublicationList == null || PublicationList.Count == 0)
                list2 = "null, add new\n\n";
            else
                foreach (Paper publication in PublicationList) list2 += publication.ToString()+"\n";

            return $"Research topic:\n{ResearchTopic}\n\nOrganisation name:\n{OrganisationName}\n\nRegistration number:\n{RegistrationNumber}\n\nResearch duration:\n{ResearchDuration}\n\nProject Members:\n{list1}\nPublicationList:\n\n{list2}";
        }

        public virtual string ToShortString()
        {
            return $"Research topic:\n{ResearchTopic}\n\nOrganisation name:\n{OrganisationName}\n\nRegistration number:\n{RegistrationNumber}\n\nResearch duration:\n{ResearchDuration}\n\n";
        }

        public override object DeepCopy()
        {
            List<Person> deepcopyProjectMembers = new();

            for (int i = 0; i < ProjectMembers.Count; i++)
                deepcopyProjectMembers.Add((Person)ProjectMembers[i].DeepCopy());

            List<Paper> deepcopyPublicationList = new ();

            for (int i = 0; i < PublicationList.Count; i++)
                deepcopyPublicationList.Add((Paper)PublicationList[i].DeepCopy());

            return new ResearchTeam(ResearchTopic, OrganisationName, RegistrationNumber, ResearchDuration, deepcopyProjectMembers, deepcopyPublicationList);
        }

        public IEnumerable<Person> GetMembersWithoutPublications()
        {
            if (PublicationList == null || PublicationList.Count == 0)
                yield break;

            bool havePublication = false;

            for (int i = 0; i < ProjectMembers.Count; i++)
            {
                for (int j = 0; j < PublicationList.Count; j++)
                    if (ProjectMembers[i] == PublicationList[j].InformationAboutAutor)
                    {
                        havePublication = true;
                        break;
                    }

                if (!havePublication)
                    yield return ProjectMembers[i];

                    havePublication = false;
            }
        }

        public IEnumerable<Paper> GetLastYearsPublications(int yearBorder)
        {
            if (PublicationList == null || PublicationList.Count == 0)
                yield break;

            int border = DateTime.Today.Year - yearBorder;

            for (int i = 0; i < PublicationList.Count; i++)
                if (PublicationList[i].PublicationDate.Year >= border)
                    yield return PublicationList[i];
        }

        public IEnumerator GetEnumerator() => new ResearchTeamEnumenator(ProjectMembers, PublicationList);

        public IEnumerable<Person> GetMembersWithSomePublications()
        {
            if (PublicationList == null || PublicationList.Count == 0)
                yield break;

            int countPublications = 0; 
            bool haveTwoOrMorePublication = false;

            for (int i = 0; i < ProjectMembers.Count; i++)
            {
                for (int j = 0; j < PublicationList.Count; j++)
                {
                    if (ProjectMembers[i] == PublicationList[j].InformationAboutAutor)
                        ++countPublications;

                    if (countPublications == 2)
                    {
                        countPublications = 0;
                        haveTwoOrMorePublication = true;
                        break;
                    }
                }

                if (haveTwoOrMorePublication)
                {
                    yield return ProjectMembers[i];
                    haveTwoOrMorePublication = false;
                }
            }
        }

        public IEnumerable<Paper> GetLastYearPublications()
        {
            if (PublicationList == null || PublicationList.Count == 0)
                yield break;

            DateTime border = new (DateTime.Today.Year - 1, DateTime.Today.Month, DateTime.Today.Day);

            for (int i = 0; i < PublicationList.Count; i++)
                if (PublicationList[i].PublicationDate >= border)
                    yield return PublicationList[i];
        }

        public int Compare(ResearchTeam x, ResearchTeam y)
        {
            throw new NotImplementedException();
        }
    }
}
