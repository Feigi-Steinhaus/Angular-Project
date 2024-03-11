using System.Text.RegularExpressions;

namespace Bll
{
    public static class ValidationTests
    {
        //פונקציה לבדיקת תקינות שם-אותיות עבריות בלבד
        public static bool IsHebrewString(string name)
        {
            string pattern = @"^[\u0590-\u05FF\s]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(name);
        }

        //פונקציה לבדיקת תקינות מספר טלפון
        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            string pattern = @"^[0-9]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }

        //פונקציה לבדיקת תקינות למייל
        public static bool IsEmailValid(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        //בדיקת תקינות לסיסמה
        public static bool IsPasswordValid(string password)
        {
            string pattern = @"^(?=.*[A-Za-z])(?=.*\d).{6,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(password);
        }

        //בדיקה שהתאריך גדול מהיום
        public static bool IsValidDate(DateTime? date)
        {
            DateTime current = DateTime.Now;
            if (date > current.Date)
                return true;
            return false;
        }
        //בדיקה שמשך הטיול גדול משלוש וקטן משתים עשרה
        static public bool IsValidHours(int? hours)
        {
            if(hours>3 && hours<12) return true;
            return false;
        }
        //מספר מקומות פנויים גדול מאפס
        static public bool IsValidplaces(int? count)
        {
            if(count>0) return true;    
            return false;
        }

        //מחיר גבוה אי שלילי גבוה מאפס
        public static bool IsValidPrice(Decimal? price)
        {
            if(price>0) return true;
            return false;
        }
    }
}
