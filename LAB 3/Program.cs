using System;

namespace LAB_3
{
    enum TimeFrame { Year, TwoYears, Long };

    class Program
    {
        static void Main()
        {
            //1

            Team t1 = new();
            Team t2 = new();

            Console.WriteLine("Equals: {0}", t1.Equals(t2));
            Console.WriteLine("==: {0}", t1 == t2);
            Console.WriteLine("ReferenceEquals: {0}", ReferenceEquals(t1, t2));
            Console.WriteLine("GetHashCode: {0}", t1.GetHashCode());
            Console.WriteLine("GetHashCode: {0}", t2.GetHashCode());

            Console.WriteLine("\n-----------------------\n");

            //2

            Console.WriteLine("If Argument out of exeption:");

            try { t1.RegistrationNumber = -5;  }

            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            Console.WriteLine("\n-----------------------\n");

            //3

            ResearchTeam rt1 = new();

            Console.Write("Before:\n\n{0}", rt1.ToString());

            rt1.AddMembers(new Person("New", "Surname", new DateTime(1999, 12, 18)), new Person("New", "Surname", new DateTime(1999, 12, 10)));
            rt1.AddPapers(new Paper("New", new Person(), new DateTime(2010, 10, 10)), new Paper("New", new Person(), new DateTime(2010, 10, 12)));

            Console.Write("After:\n\n{0}", rt1.ToString());

            Console.WriteLine("-----------------------\n");

            //4

            Console.WriteLine("Team Property (OrganisationName): {0}", rt1.OrganisationName);

            Console.WriteLine("\n-----------------------\n");

            //5

            var rt2 = (ResearchTeam)rt1.DeepCopy();

            Console.Write("Before:\n\nOriginal:\n\n{0}Copy:\n\n{1}", rt1.ToString(), rt2.ToString());

            rt1.PublicationList.RemoveAt(4);

            rt1.OrganisationName = "New Name";

            rt1.PublicationList[0].PublicationDate = new DateTime(2000, 10, 10);

            Console.Write("After:\n\nOriginal:\n\n{0}Copy:\n\n{1}", rt1.ToString(), rt2.ToString());

            Console.WriteLine("-----------------------\n");

            //6

            rt1.AddMembers(new Person("Name", "Surname", new DateTime(1000, 01, 01)));
            rt1.AddPapers(new Paper("New", new Person("Name", "Surname", new DateTime(1000, 01, 01)), new DateTime(2010, 10, 10)));

            rt1.ProjectMembers[0].Surname = "Gor";

            //Console.WriteLine("For cheking:\n\n{0}", r1.ToString());

            Console.WriteLine("Authors without publications:\n");

            foreach (Person member in rt1.GetMembersWithoutPublications())
                Console.WriteLine(member.ToString());

            Console.WriteLine("-----------------------\n");

            //7

            rt1.PublicationList[0].PublicationDate = new DateTime(2020, 12, 11);
            rt1.PublicationList[4].PublicationDate = new DateTime(2021, 02, 11);

            //Console.WriteLine("For cheking:\n\n{0}", r1.ToString());

            Console.WriteLine("Publications for last 3 years:\n");

            foreach (Paper publication in rt1.GetLastYearsPublications(3))
                Console.WriteLine(publication.ToString());

            Console.WriteLine("-----------------------\n");

            //8

            Console.WriteLine("Authors with publications:\n");

            foreach (var member in rt1)
                if (member != null)
                Console.WriteLine(member.ToString());

            Console.WriteLine("-----------------------\n");

            //9

            Console.WriteLine("Authors with two or more publications:\n");

            foreach (Person member in rt1.GetMembersWithSomePublications())
                Console.WriteLine(member.ToString());

            Console.WriteLine("-----------------------\n");

            //10

            rt1.PublicationList[0].PublicationDate = new DateTime(2021, 04, 10);

            Console.WriteLine("Publications in last year:\n");

            foreach (Paper publication in rt1.GetLastYearPublications())
                Console.WriteLine(publication.ToString());

            Console.WriteLine("-----------------------");
        }


    }
}
