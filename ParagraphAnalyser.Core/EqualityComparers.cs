using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParagraphAnalyser.Core
{
    // These aren't super efficent, but they work
    // If we wanted super fast, unreadable and unmaintainable we'd be using regular expressions anyway
    // these are a good happy medium

    public class CharComparerCurrentCultureIgnoreCase : IEqualityComparer<char>
    {
        //assume we're comparing user generated data, so they will care about their current culture
        //if the data is more machine generated, or the source is more well known, use StringComparer.OrdinalIgnoreCase instead
        private readonly StringComparer CurrentCultureIgnoreCaseStringComparer = StringComparer.CurrentCultureIgnoreCase;
        public bool Equals(char x, char y)
        {
            //char is a struct, so no need to null check
            
            //this is pretty self explanitory 
            //convert each char to a string and use the string comparer to do the comparison
            return CurrentCultureIgnoreCaseStringComparer.Equals(x.ToString(), y.ToString());
        }

        public int GetHashCode(char obj)
        {
            //char is a struct, so no need to null check

            //this is pretty self explanitory 
            //convert the char to a string and use the string comparer to do the hashing
            return CurrentCultureIgnoreCaseStringComparer.GetHashCode(obj.ToString());
        }
    }
    public class CharComparerCurrentCulture : IEqualityComparer<char>
    {
        //assume we're comparing user generated data, so they will care about their current culture
        //if the data is more machine generated, or the source is more well known, use StringComparer.OrdinalIgnoreCase instead
        private readonly StringComparer CurrentCultureStringComparer = StringComparer.CurrentCulture;
        public bool Equals(char x, char y)
        {
            //char is a struct, so no need to null check

            //this is pretty self explanitory 
            //convert each char to a string and use the string comparer to do the comparison
            return CurrentCultureStringComparer.Equals(x.ToString(), y.ToString());
        }

        public int GetHashCode(char obj)
        {
            //char is a struct, so no need to null check

            //this is pretty self explanitory 
            //convert the char to a string and use the string comparer to do the hashing
            return CurrentCultureStringComparer.GetHashCode(obj.ToString());
        }
    }
}
