using System;

namespace LAB_3
{
    class Team : INameAndCopy
    {
        protected private string organisationName;
        protected private int registrationNumber;

        public string OrganisationName
        {
            get { return organisationName; }
            set { organisationName = value; }
        }

        public int RegistrationNumber
        {
            get { return registrationNumber; }
            set 
            {
                    if (value <= 0)
                        throw new ArgumentOutOfRangeException("Value <= 0");

                    registrationNumber = value;               
            }
        }

        public Team (string organisationName, int registrationNumber)
        {
            OrganisationName = organisationName;
            RegistrationNumber = registrationNumber;
        }

        public Team ()
        {
            OrganisationName = "Standart";
            RegistrationNumber = 2;
        }

        public override string ToString()
        {
            return $"Organisation Name: {OrganisationName}\n\nRegistration Number: {RegistrationNumber}\n";
        }

        public virtual object DeepCopy()
        {
            return new Team(OrganisationName, RegistrationNumber);
        }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not Team team || obj is null)
                return false;

            return team.OrganisationName == OrganisationName && team.RegistrationNumber == RegistrationNumber;
        }

        public static bool operator ==(Team team1, Team team2)
        {
            if ((team1.OrganisationName == team2.OrganisationName) && (team1.RegistrationNumber == team2.RegistrationNumber))
                return true;

            return false;
        }

        public static bool operator !=(Team team1, Team team2)
        {
            if ((team1.OrganisationName != team2.OrganisationName) || (team1.RegistrationNumber != team2.RegistrationNumber))
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            int hashcode = OrganisationName.GetHashCode();
            hashcode = 31 * hashcode + RegistrationNumber.GetHashCode();

            return hashcode;
        }
    }
}
