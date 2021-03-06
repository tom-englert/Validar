﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TemplatesGeneric.Sandra
{
    public class Model :
        IDataErrorInfo,
        INotifyPropertyChanged,
        INotifyDataErrorInfo
    {
        ValidationTemplate<Model> validationTemplate;
        string property1;
        string property2;

        public string Property1
        {
            get { return property1; }
            set
            {
                property1 = value;
                OnPropertyChanged();
            }
        }

        public string Property2
        {
            get { return property2; }
            set
            {
                property2 = value;
                OnPropertyChanged();
            }
        }

        public Model()
        {
            validationTemplate = new ValidationTemplate<Model>(this);
        }

        public string this[string columnName] => validationTemplate[columnName];

        public string Error => validationTemplate.Error;

        public IEnumerable GetErrors(string propertyName)
        {
            return validationTemplate.GetErrors(propertyName);
        }

        bool INotifyDataErrorInfo.HasErrors => validationTemplate.HasErrors;

        event EventHandler<DataErrorsChangedEventArgs> INotifyDataErrorInfo.ErrorsChanged
        {
            add { validationTemplate.ErrorsChanged += value; }
            remove { validationTemplate.ErrorsChanged -= value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}