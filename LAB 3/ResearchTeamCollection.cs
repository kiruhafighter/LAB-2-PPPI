using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;

namespace LAB_3
{
    class ResearchTeamCollection<TKey>
    {
        public delegate TKey KeySelector<TKey>(ResearchTeam rt);

        public event ResearchTeamChangedHandler<TKey> ResearchTeamsChanged;

        public string CollectionName { get; set; }

        private Dictionary<TKey, ResearchTeam> collection = new(0);

        public KeySelector<TKey> keySelector;

        public ResearchTeamCollection(KeySelector<TKey> keyselector) => keySelector += keyselector ?? throw new NullReferenceException("Subscribe method for keys");

        public DateTime LastPublication
        {
            get
            {
                if (collection.Count == 0 || collection == null)
                    return new DateTime();

                DateTime maxDate = new(), rtMaxDate;

                foreach (ResearchTeam rt in collection.Values)
                {
                    rtMaxDate = rt.PublicationList.Max(date => date.PublicationDate);

                    if (maxDate < rtMaxDate)
                        maxDate = rtMaxDate;
                }

                return maxDate;
            }
        }

        public IEnumerable<IGrouping<TimeFrame, KeyValuePair<TKey, ResearchTeam>>> Group
        {
            get => collection.GroupBy(pair => pair.Value.ResearchDuration);
        }



        public void AddDeafaults()
        {
            ResearchTeam rt1 = new();

            ResearchTeam rt2 = new()
            {
                ResearchTopic = "Wood",
                OrganisationName = "Astralis",
                RegistrationNumber = 99,
                ResearchDuration = TimeFrame.Year,

                ProjectMembers = new List<Person>
                    {
                        new Person(),
                        new Person()
                    },

                PublicationList = new List<Paper>
                    {
                        new Paper("Woods", new(), new DateTime()),
                        new Paper("North", new(), new DateTime()),
                    }
            };

            collection = new Dictionary<TKey, ResearchTeam>
            {
                { keySelector(rt1), rt1 },
                { keySelector(rt2), rt2}
            };
            ResearchTeamsChanged?.Invoke(this, new(rt1.ResearchTopic, Revision.Add, "Research Team added", rt1.RegistrationNumber));
            ResearchTeamsChanged?.Invoke(this, new(rt2.ResearchTopic, Revision.Add, "Research Team added", rt2.RegistrationNumber));
            rt1.PropertyChanged += PropertyChanged;
            rt2.PropertyChanged += PropertyChanged;
        }

        public void AddResearchTeams(params ResearchTeam[] researchTeams)
        {
            foreach (ResearchTeam rt in researchTeams)
            {
                collection.Add(keySelector(rt), rt);
                ResearchTeamsChanged?.Invoke(this, new(rt.ResearchTopic, Revision.Add, "Research Team added", rt.RegistrationNumber));
                rt.PropertyChanged += PropertyChanged;
            }
        }

        public override string ToString()
        {
            string list = null;
            if (collection == null || collection.Count == 0)
                list = "null, add new\n\n";
            else
                foreach (ResearchTeam researchTeam in collection.Values) list += researchTeam.ToString();

            return list;
        }

        public string ToShortString()
        {
            string list = null;
            if (collection == null || collection.Count == 0)
                list = "null, add new\n\n";
            else
                foreach (ResearchTeam researchTeam in collection.Values) list += researchTeam.ToShortString();

            return list;
        }

        public IEnumerable<KeyValuePair<TKey, ResearchTeam>> TimeFrameGroup(TimeFrame value)
        {
            return collection.Where(pair => pair.Value.ResearchDuration == value);
        }

        public bool Remove(ResearchTeam rt)
        {
            ResearchTeamsChanged?.Invoke(this, new(rt.ResearchTopic, Revision.Remove, "Research Team is removed", rt.RegistrationNumber));
            rt.PropertyChanged -= PropertyChanged;
            return true;
        }

        public void ChangeTopic (ResearchTeam rs , string topicName)
        {
            rs.researchTopic = topicName;
            rs.PropertyChanged += PropertyChanged;
            ResearchTeamsChanged?.Invoke(this, new(rs.ResearchTopic, Revision.Property, "Topic name changed", rs.RegistrationNumber));
        }
        
        public void ChangeDuration (ResearchTeam rt, TimeFrame timeFrame)
        {
            rt.researchDuration = timeFrame;
            rt.PropertyChanged += PropertyChanged;
            ResearchTeamsChanged?.Invoke(this, new(rt.ResearchTopic, Revision.Property, "Duration changed", rt.RegistrationNumber));
        }

        public bool Replace(ResearchTeam rtOld, ResearchTeam rtNew)
        {
            //if (!collection.ContainsValue(rtOld))
            //    return false;

            collection[keySelector(rtOld)] = rtNew;

            ResearchTeamsChanged?.Invoke(this, new(rtNew.ResearchTopic, Revision.Replace, "Research Team replaced", rtOld.RegistrationNumber));

            rtOld.PropertyChanged -= PropertyChanged;

            rtNew.PropertyChanged += PropertyChanged;

            return true;
        }

        public void PropertyChanged(object source, PropertyChangedEventArgs args)
        {
            if (source is ResearchTeam rt)
                ResearchTeamsChanged?.Invoke(this, new(rt.ResearchTopic, Revision.Property, args.PropertyName, rt.RegistrationNumber));
        }



    }
}


