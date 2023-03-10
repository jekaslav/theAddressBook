using static System.Console;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace AddressBoook;

public class Methods
{
    
    public static AddressBook AddContact(AddressBook addressbook)
    {
        Contact contact = new Contact();
        
        Write("Enter a name: ");
        contact.Name = ReadLine();

        if (!string.IsNullOrEmpty(contact.Name))
        {
            
        }

        Write("Enter a phone number: ");
        contact.PhoneNumber = ReadLine() ?? string.Empty;
        
        Regex regex = new Regex(@"^[0-9]+$");
        
        if (contact.PhoneNumber != null && !regex.IsMatch(contact.PhoneNumber))
        {
            throw new InvalidOperationException("Wrong phone number");
        }

        Write("Enter an e-mail: ");
        contact.Mail = ReadLine();

        if (contact.Mail != null && !contact.Mail.Contains('@'))
        {
            throw new InvalidOperationException("Wrong e-mail address");
        }

        contact.Id = Guid.NewGuid();
        
        var writeToJson = JsonConvert.SerializeObject(contact);
        File.WriteAllText(AddressBook.PathWritingContact + contact.Id + ".json", writeToJson);
        IEnumerable<Contact> toAdd = new Contact[] { contact };
        addressbook.Contacts?.Add(contact);
        Write("Success");
        return addressbook;
    }
    
    public static void ViewContacts(AddressBook addressbook)
    {
        foreach (Contact contact in addressbook.Contacts)
        {
            WriteLine($"{contact.Id}:{contact.Name}:{contact.PhoneNumber}:{contact.Mail}");
        }
    }

    public static void SearchContacts(AddressBook addressbook)
    {
        Write("Enter a parameter");
                        var searchParameter = ReadLine();
            foreach (var file in addressbook.Contacts)
            {
                var foundContacts = addressbook.Contacts.Where(c => c.Name == searchParameter
                                                                     || c.Mail == searchParameter
                                                                     || c.PhoneNumber == searchParameter).ToList();
                foreach (var foundContact in foundContacts)
                {
                    WriteLine($"ID:{foundContact.Id}, Name:{foundContact.Name}, Phone:{foundContact.PhoneNumber}, E-Mail:{foundContact.Mail}");
                }
            }
    }

    public static void RemoveContact(AddressBook addressBook)
    {
        
    }
    
}