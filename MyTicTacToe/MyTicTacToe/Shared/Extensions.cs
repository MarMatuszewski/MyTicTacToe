using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTicTacToe.Shared
{
    public static class Extensions
    {
        public static string FindKeyByValue( this Dictionary<string, List<string>> dictionary, List<string> value )
        {
            string key = null;
            foreach( var pair in dictionary )
            {
                if( value.Equals( pair.Value ) )
                {
                    key = pair.Key;
                }
            }
            return key;
        }


        public static List<string> FindLineWithTwoSameSigns( 
            this Dictionary<string, List<string>> dictionary,
            List<string> listWithPlayersFields )
        {
            var counter = 0;
            var returnList = new List<string>();
            foreach( var pair in dictionary )
            {
                foreach( var field in listWithPlayersFields )
                {
                    if( pair.Value.Contains( field ) )
                    {
                        counter++;
                    }
                }

                if( counter == 2 )
                {
                    returnList.Add( pair.Key );
                }

                counter = 0;
            }

            return returnList;
        }
    }
}
