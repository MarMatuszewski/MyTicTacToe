using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyTicTacToe.Shared
{
    public class ObservableObject : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent( string propertyName )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }


        protected void SetProperty( ref string fieldName, string value, [CallerMemberName] string propertyName = null )
        {
            if( object.Equals( fieldName, value ) )
            {
                return;
            }

            fieldName = value;
            RaisePropertyChangedEvent( propertyName );
        }
    }
}
