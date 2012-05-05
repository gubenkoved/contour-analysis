using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ContourAnalysis.Core
{
    [Serializable]
    public class ContourInformation : INotifyPropertyChanged, ICloneable
    {
        private Contour m_contour;
        public Contour Contour
        {
            get { return m_contour; }
            set
            {
                m_contour = value;
                OnPropertyChanged("Contour");
            }
        }

        private string m_description;
        public string Description
        {
            get
            {
                return m_description;
            }
            set
            {
                m_description = value;
                OnPropertyChanged("Description");
            }
        }

        #region Comparative characteristics (fills by matcher)
        [NonSerialized]
        private Complex m_maxNsp;
        public Complex MaxNSP
        {
            get { return m_maxNsp; }
            set { m_maxNsp = value; }
        }

        [NonSerialized]
        private Complex m_acf_nsp;
        public Complex ACF_NSP
        {
            get { return m_acf_nsp; }
            set { m_acf_nsp = value; }
        }
        
        [NonSerialized]
        private bool m_reverseUsed;
        public bool ReverseUsed
        {
            get { return m_reverseUsed; }
            set { m_reverseUsed = value; }
        }

        public void SendDynamicInfosChangedNotification()
        {
            OnPropertyChanged("MaxNSP");
            OnPropertyChanged("ReverseUsed");
            OnPropertyChanged("ACF_NSP");
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (m_propertyChanged != null)
            {
                m_propertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }        

        [NonSerialized]
        private PropertyChangedEventHandler m_propertyChanged;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                m_propertyChanged += value;
            }
            remove
            {
                m_propertyChanged -= value;
            }
        }
        #endregion

        public ContourInformation(Contour contour)
        {
            m_contour = contour;            
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
