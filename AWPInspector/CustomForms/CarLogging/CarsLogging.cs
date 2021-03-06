﻿using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using BasicDataStructures.Interfaces;
using Dacha.DataModel;
using Dacha.Inspector.CustomForms.CarLogging;
using WPF.Dictionaries.Annotations;
using WPF.Dictionaries.CustomForms;
using WPF.Dictionaries.Factories;

namespace Dacha.Inspector.CustomForms
{
    internal class CarsLogging : IDomainSpecificCustomForm
    {
        private string _enteredCarNumber;
        private string _foundNumber;
        public IPresenterFactory Presenter { get; set; }

        public IDomainDataAcces DataServices { get; set; }

        public string EnteredCarNumber
        {
            get { return _enteredCarNumber; }
            set
            {
                if (value == _enteredCarNumber) return;
                _enteredCarNumber = value;
                StartLookingForTheCar();
                NotifyPropertyChanged();
            }
        }

        public string FoundNumber
        {
            get { return _foundNumber; }
            set
            {
                if (value == _foundNumber) return;
                _foundNumber = value;
                NotifyPropertyChanged();
            }
        }

        private void StartLookingForTheCar()
        {
            var splitCarNumber = CarNumberHelper.SplitToMajorParts(_enteredCarNumber);
            var foundNumber = DataServices.SearchForCarNumber(splitCarNumber);
            FoundNumber = foundNumber.FirstOrDefault()?.Number;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
