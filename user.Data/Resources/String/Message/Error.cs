using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user.Data.Resources.String.Message
{
    public static class Error
    {
        //Register
        public const string USER_NOT_DELETED_SUCCESSFULLY_WHILE_REGISTERING = "There is a problem in registering because user is taken any update. User is not successfully deleted";
        public const string USER_ALREADY_EXIST = "The user is already exist";
        //Login
        public const string USER_NOT_FOUND = "Password and email not matched!";
        public const string BRANCH_NOT_FOUND = "The branch not found";
        public const string HOSPITAL_NOT_FOUND = "The hospital not found";
        public const string TOWN_NOT_FOUND = "There is no branch within given town";
        public const string CITY_NOT_FOUND = "There is no branch within given city";
       
    }
}
