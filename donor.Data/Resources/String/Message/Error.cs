using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace donor.Data.Resources.String.Message
{
    public class Error
    {
        public const string USER_NOT_VERIFIED = "The user who requesting could not be verified";
        //Adding donor
        public const string CITY_NOT_ADDED = "The given city not added to list";
        public const string TOWN_NOT_ADDED = "The given town not added to list";
        public const string DONOR_NOT_ADDED = "The donor not added to list";
        public const string BRANCH_NOT_FOUND = "The branch not found";
        //Find donor
        public const string DONOR_NOT_FOUND = "The donor not found";
        public const string USER_NOT_FOUND = "The user not found";
        //Donate
        public const string DONATION_HISTORY_NOT_CREATED = "Donation history is not created";
        public const string BLOOD_COUNT_NOT_UPDATED= "Blood count is not updated";
        public const string UNDO_BLOOD_COUNT_NOT_UPDATED = "Undo blood count is not updated. It seems more blood count";

    }
}
