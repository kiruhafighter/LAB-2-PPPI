using System;

namespace LAB_3
{
    public class Paper
    {
        public string PublicationName { get; set; }
        public Person InformationAboutAutor { get; set; }
        public DateTime PublicationDate { get; set; }

        public Paper(string publicationName, Person informationAboutAutor, DateTime publicationDate)
        {
            PublicationName = publicationName;
            InformationAboutAutor = informationAboutAutor;
            PublicationDate = publicationDate;
        }

        public Paper()
        {
            PublicationName = "Hovs";
            InformationAboutAutor = new Person("Peter", "Griffin", new DateTime(1983, 08, 12));
            PublicationDate = new DateTime(2022, 12, 03);
        }

        public override string ToString()
        {
            return $"Publication name: {PublicationName}\nInformation about autor:\n{InformationAboutAutor}Publication date: {PublicationDate.ToShortDateString()}\n";
        }

        public virtual object DeepCopy()
        {
            Person deepcopyInformationAboutAutor = (Person)InformationAboutAutor.DeepCopy();

            DateTime newPublicationDate = new (PublicationDate.Year, PublicationDate.Month, PublicationDate.Day);

            return new Paper(PublicationName, deepcopyInformationAboutAutor, newPublicationDate);
        }
    }
}
