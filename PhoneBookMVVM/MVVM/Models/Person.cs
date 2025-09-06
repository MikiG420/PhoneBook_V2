using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneBookMVVM.MVVM.Models
{
    public class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        private string _firstName;
        private string _lastName;
        private string _phoneNumber;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
                }
            }
        }
        public Person () { }

        public Person(string firstName, string lastName, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            IsSelected = false;
        }

        public void Validate()
        {
            ValidateFirstName(FirstName);
            ValidateLastName(LastName);
            ValidatePhoneNumber(PhoneNumber);
        }

        private void ValidatePhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex(@"^(\+48)?\s?(\d{3})[\s\-]?\d{3}[\s\-]?\d{3}$");

            if (phoneNumber.Length >= 12)
            {
                throw new ArgumentException("Phone number must be 10 digits long.");
            }

            if (!regex.IsMatch(phoneNumber))
            {
                throw new ArgumentException("Phone number must be in format: 123 456 789 or 123-456-789 or 123-456-789 or +48 123 456 789.");
            }
        }

        private void ValidateFirstName(string firstName)
        {
            Regex regex = new Regex(@"^[A-ZĄĆĘŁŃÓŚŻŹ][a-ząćęłńóśżź]+(?:\s[A-ZĄĆĘŁŃÓŚŻŹ][a-ząćęłńóśżź]+)*$");

            if (!regex.IsMatch(firstName))
            {
                throw new ArgumentException("First name must start with a capital letter and consist only of letters. Compound first names are also allowed.");
            }
        }


        private void ValidateLastName(string lastName)
        {
            Regex regex = new Regex(@"^[A-ZĄĆĘŁŃÓŚŻŹ][a-ząćęłńóśżź]+(?:[-'][A-ZĄĆĘŁŃÓŚŻŹ][a-ząćęłńóśżź]+)*$");

            if (!regex.IsMatch(lastName))
            {
                throw new ArgumentException("Last name must start with a capital letter and consist only of letters. Compound last names are also allowed.");
            }
        }
    }
}
