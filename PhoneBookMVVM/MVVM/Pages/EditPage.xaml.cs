using PhoneBookMVVM.MVVM.Models;
using System.Collections.ObjectModel;

namespace PhoneBookMVVM.MVVM.Pages;

public partial class EditPage : ContentPage
{
    ObservableCollection<Person> ContactsRef;
    Person PersonToEdit { get; set; }

    public EditPage(Person person, ObservableCollection<Person> contacts) 
    {
        InitializeComponent();
        ContactsRef = contacts;
        PersonToEdit = person;
        BindingContext = PersonToEdit;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            PersonToEdit.Validate();

            var sorted = ContactsRef.OrderBy(p => p.LastName)
                                    .ThenBy(p => p.FirstName)
                                    .ToList();

            ContactsRef.Clear();
            foreach (var p in sorted)
                ContactsRef.Add(p);

            await Navigation.PopAsync();
        }
        catch (ArgumentException ex)
        {
            await DisplayAlert("B³¹d walidacji", ex.Message, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("B³¹d", $"Nieoczekiwany b³¹d: {ex.Message}", "OK");
        }
    }
}