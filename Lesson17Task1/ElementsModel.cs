using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson17Task1
{
    /// <summary>
    /// Класс, поддерживающий привязку свойств к текстовым полям формы
    /// </summary>
    public class ElementsModel : INotifyPropertyChanged
    {
        private string sqlConnectionState;
        private string sqlPublicString;
        private string oleConnectionState;
        private string olePublicString;
        
        public ElementsModel()
        {
            sqlConnectionState = "Not available";
            oleConnectionState = "Not available";
        }

        /// <summary>
        /// Состояние подключения к базе SQL
        /// </summary>
        public string SqlConnectionState
        {
            get { return sqlConnectionState; }
            set 
            {
                if (sqlConnectionState != value)
                {
                    sqlConnectionState = value;
                    OnPropertyChanged(nameof(SqlConnectionState));
                }
            }
        }

        /// <summary>
        /// Состояние подключения к базе OLE
        /// </summary>
        public string OleConnectionState
        {
            get { return oleConnectionState; }
            set
            {
                if (oleConnectionState != value)
                {
                    oleConnectionState = value;
                    OnPropertyChanged(nameof(OleConnectionState));
                }
            }
        }

        /// <summary>
        /// Отображаемая строка подключения к базе SQL
        /// </summary>
        public string SqlPublicString
        {
            get { return sqlPublicString; }
            set
            {
                if (sqlPublicString != value)
                {
                    sqlPublicString = value;
                    OnPropertyChanged(nameof(SqlPublicString));
                }
            }
        }

        /// <summary>
        /// Отображаемая строка подключения к базе OLE
        /// </summary>
        public string OlePublicString
        {
            get { return olePublicString; }
            set
            {
                if (olePublicString != value)
                {
                    olePublicString = value;
                    OnPropertyChanged(nameof(OlePublicString));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
