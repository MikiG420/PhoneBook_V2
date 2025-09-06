using PhoneBookMVVM.MVVM.Models;
using System.Collections.ObjectModel;

namespace PhoneBookMVVM.MVVM.Pages;

public partial class AddPage : ContentPage
{
        ObservableCollection<Person> ContactsRef;
        private readonly Action SortAction;

        public AddPage(ObservableCollection<Person> contacts, Action sortAction)
        {
            InitializeComponent(); 
            ContactsRef = contacts;
            SortAction = sortAction;
        }

    private async void AddPerson_Clicked(object sender, EventArgs e)
    {
        try
        {
            string firstName = FirstNameEntry?.Text?.Trim() ?? "";
            string lastName = LastNameEntry?.Text?.Trim() ?? "";
            string phone = PhoneNumberEntry?.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(phone))
            {
                await DisplayAlert("B³¹d", "Wszystkie pola s¹ wymagane.", "OK");
                return;
            }

            ContactsRef.Add(new Person
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phone
            });

            SortAction?.Invoke();

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Wyst¹pi³ b³¹d", $"Nie uda³o siê dodaæ osoby:\n{ex.Message}", "OK");
        }
    }
}