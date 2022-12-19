using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_3
{
    public class ResearchTeamCollection <TKey>
    {
        private List<ResearchTeam> LR;
        private static Dictionary<TKey, ResearchTeam> TKeyResearchTeam;
        public string NameCol { get; set; }
        public ResearchTeamCollection(string name)
        {
            NameCol = name;
            LR = new List<ResearchTeam>();
        }
        /*• метод bool Remove(ResearchTeam rt) видалення елемента зі значенням
         * rt зі словника Dictionary<TKey, ResearchTeam>;
         * якщо у словнику немає елемента rt, метод повертає значення false; */
        public event ResearchTeamChangedHandler<TKey> ResearchTeamsChanged;
        public bool Remove(ResearchTeam rt)
        {
            TKeyResearchTeam = new Dictionary<TKey, ResearchTeam>();
            ResearchTeam researcht = new ResearchTeam();
            Dictionary<TKey, ResearchTeam>.KeyCollection keyColl = TKeyResearchTeam.Keys;
            if (TKeyResearchTeam.ContainsValue(rt))
            {
                foreach (TKey t in keyColl)
                {
                    TKeyResearchTeam.TryGetValue(t, out researcht);
                    if (rt == researcht)
                    {
                        ResearchTeamsChanged(this, new ResearchTeamChangedEventArgs<TKey>(NameCol, Revision.Remove, " ", 1));
                        return TKeyResearchTeam.Remove(t);
                    }
                    else
                    {
                        continue;
                    }

                }
            }
            return false;


        }
        /*• метод bool Replace(ResearchTeam rtold, ResearchTeam rtnew) для заміни у словнику
         * Dictionary<TKey, ResearchTeam > елемента зі значенням rtold на елемент зі значенням rtnew;
         * якщо у словнику немає елемента зі значенням rtold, метод повертає значення false; */
        public bool Replace(ResearchTeam rtold, ResearchTeam rtnew)
        {
            TKeyResearchTeam = new Dictionary<TKey, ResearchTeam>();

            if (TKeyResearchTeam.ContainsValue(rtold))
            {
                rtold = rtnew;
                ResearchTeamsChanged(this, new ResearchTeamChangedEventArgs<TKey>(NameCol, Revision.Replace, " ", 2));

                return true;
            }
            else
            {
                return false;
            }
        }
        //метод, який повертає кількість журналів,
        // * Реєстраційний номер яких входить у заданий діапазон.Межі діапазону передати як параметри
        // * для визначення потрібної кількості використовувати методи розширення
        public int Method(List<TeamsJournalEntry> tje, int a, int b)
        {

            int kolvo = tje.Count(t1 => t1.NumberOfRegistration > a && t1.NumberOfRegistration < b);

            return kolvo;

        }

        /*•   событие ResearchTeamsChanged типа ResearchTeamsChangedHandler<TKey>, 
         * которое происходит, когда изменяется набор элементов в
         *  коллекции-словаре Dictionary<TKey,ResearchTeam> или изменяются данные одного из ее элементов. */


        public void AddDefaults()
        {
            LR.Add(new ResearchTeam());
        }
        public void AddResearchTeams(params ResearchTeam[] RT)
        {
            LR.AddRange(RT);
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < LR.Count; i++)
                s += LR[i].ToString() + "\n";
            return s;
        }


    }
}

