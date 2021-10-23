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
        private string sqlPartialString;
        private string oleConnectionState;
        private string olePartialString;
        
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
        public string SqlPartialString
        {
            get { return sqlPartialString; }
            set
            {
                if (sqlPartialString != value)
                {
                    sqlPartialString = value;
                    OnPropertyChanged(nameof(SqlPartialString));
                }
            }
        }

        /// <summary>
        /// Отображаемая строка подключения к базе OLE
        /// </summary>
        public string OlePartialString
        {
            get { return olePartialString; }
            set
            {
                if (olePartialString != value)
                {
                    olePartialString = value;
                    OnPropertyChanged(nameof(OlePartialString));
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
