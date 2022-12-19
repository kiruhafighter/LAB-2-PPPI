using System;

namespace LAB_3
{
    public class Person
    {
        private string name;
        private string surname;
        private DateTime birthday;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        public int ChangeBirthdayYear
        {
            get
            {
                return birthday.Year;
            }

            set
            {
                birthday = new DateTime(value, birthday.Month, birthday.Day);
            }
        }

        public Person(string name, string surname, DateTime birthday)
        {
            Name = name;
            Surname = surname;
            Birthday = birthday;
        }

        public Person()
        {
            Name = "Joe";
            Surname = "Swanson";
            Birthday = new DateTime(1985, 06, 21);
        }

        public override string ToString()
        {
            return $"Name: {Name}, Surname: {Surname}, Birthday: {Birthday.ToShortDateString()}\n";
        }

        public virtual string ToShortString()
        {
            return $"Name: {Name}, Surname: {Surname}\n";
        }

        public override bool Equals(object obj)
        {
            if (obj is not Person person || obj is null)
                return false;

            return person.Name == Name && person.Surname == Surname && person.Birthday == Birthday;
        }

        public static bool operator ==(Person person1, Person person2)
        {
            if ((person1.Name == person2.Name) && (person1.Surname == person2.Surname) && (person1.Birthday == person2.Birthday))
                return true;

            return false;
        }

        public static bool operator !=(Person person1, Person person2)
        {
            if ((person1.Name != person2.Name) || (person1.Surname != person2.Surname) || (person1.Birthday != person2.Birthday))
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            int hashcode = Name.GetHashCode();
            hashcode = 31 * hashcode + Surname.GetHashCode();
            hashcode = 31 * hashcode + Birthday.GetHashCode();

            return hashcode;
        }

        public virtual object DeepCopy()
        {
            DateTime newBirthday = new(Birthday.Year, Birthday.Month, Birthday.Day);

            return new Person(Name, Surname, newBirthday);
        }
    }
}
