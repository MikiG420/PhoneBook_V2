using PhoneBookMVVM.MVVM.Models;
using System.Collections.ObjectModel;

namespace PhoneBookMVVM.MVVM.Pages;

public partial class Home : ContentPage
{
    public ObservableCollection<Person> Contacts { get; set; }

    public ObservableCollection<Person> SortedContacts { get; set; }
    public Home()
    {
        InitializeComponent();
        Contacts = new ObservableCollection<Person>
            {
                new Person("Jan", "Kowalski", "123 456 789"),
                new Person("Anna", "Nowak", "987 654 321"),
                new Person("Halina", "Zelek", "123 456 789"),
                new Person("Ludwik", "Ptak", "987 654 321"),
                new Person("And¿elika", "Gocal", "123 456 789"),
                new Person("Józef", "Augustyn", "987 654 321")
            };

        SortedContacts = new ObservableCollection<Person> { };

        SortContacts();
        BindingContext = this;
    }

    private async void OnAddContactButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddPage(Contacts, SortContacts));
    }

    private void ClearContact_Clicked(object sender, EventArgs e)
    {
        Person? personToRemove = null;

        if (sender is MenuFlyoutItem menuItem && menuItem.CommandParameter is Person personFromMenu)
        {
            personToRemove = personFromMenu;
        }
        else if (sender is SwipeItem swipeItem && swipeItem.BindingContext is Person personFromSwap)
        {
            personToRemove = personFromSwap;
        }

        if (personToRemove != null && Contacts.Contains(personToRemove) || SortedContacts.Contains(personToRemove))
        {
            Contacts.Remove(personToRemove);
            SortedContacts.Remove(personToRemove);
        }
    }

    private void ClearMany_Clicked(object sender, EventArgs e)
    {
        var selectedContacts = Contacts.Where(p => p.IsSelected).ToList();
        var selectedSortedContacts = SortedContacts.Where(p => p.IsSelected).ToList();

        foreach (var person in selectedContacts)
        {
            Contacts.Remove(person);
        }

        foreach (var person in selectedSortedContacts)
        {
            SortedContacts.Remove(person);
        }
    }

    protected async void OnEditButtonClicked(object sender, EventArgs e)
    {
        Person? selectedPerson = null;

        if (sender is MenuFlyoutItem menuItem && menuItem.CommandParameter is Person personFromMenu)
        {
            selectedPerson = personFromMenu;
        }
        else if (sender is SwipeItem swapItem && swapItem.BindingContext is Person personFromSwap)
        {
            selectedPerson = personFromSwap;
        }

        if (selectedPerson != null)
        {
            await Navigation.PushAsync(new EditPage(selectedPerson, Contacts));
        }
        else
        {
            await DisplayAlert("B³¹d", "Nie uda³o siê pobraæ kontaktu do edycji.", "OK");
        }
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchString = e.NewTextValue?.ToLower() ?? string.Empty;

        var filteredContacts = Contacts
            .Where(c => c.FirstName.ToLower().Contains(searchString) || c.LastName.ToLower().Contains(searchString) || new string(c.PhoneNumber.Where(char.IsDigit).ToArray()).Contains(searchString))
            .ToList();

        SortedContacts.Clear();
        foreach ( var contact in filteredContacts) 
            SortedContacts.Add(contact);



    }

    private void ResetFilter_Clicked(object sender, EventArgs e)
    {
        search_bar.Text = string.Empty;

        var sorted = Contacts.OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ToList();
        SortedContacts.Clear();
        foreach (var p in sorted)
            SortedContacts.Add(p);
    }

    private void SortContacts()
    {
        var sorted = Contacts.OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ToList();
        SortedContacts.Clear();
        foreach (var p in sorted)
            SortedContacts.Add(p);
    }
}