using static System.Console;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
    
namespace AddressBoook;

public class AddressBook
{
    private IList<Contact> Contacts { get; } 

    private const string PathWritingContact = @"C:\Users\Delor\Documents\Contacts\";

    public AddressBook()
    {
        Contacts = new List<Contact>(); 
    }

    public void AddContact()
    {
        Write("Enter a name: ");
        var name = ReadLine() ?? string.Empty;

        if (!string.IsNullOrEmpty(name))
        {
            throw new InvalidOperationException("Wrong name");
        }

        Write("Enter a phone number: ");
        var phoneNumber = ReadLine() ?? string.Empty;
        var regex = new Regex(@"^[0-9]+$");

        if (phoneNumber != null && !regex.IsMatch(phoneNumber))
        {
            throw new InvalidOperationException("Wrong phone number");
        }

        Write("Enter an e-mail: ");
        var mail = ReadLine() ?? string.Empty;

        if (!mail.Contains('@'))
        {
            throw new InvalidOperationException("Wrong e-mail address");
        }

        var contact = new Contact
        {
            Id = Guid.NewGuid(),
            Name = name,
            Mail = mail,
            PhoneNumber = phoneNumber
        };
        
        var writeToJson = JsonConvert.SerializeObject(contact);
        File.WriteAllText(PathWritingContact + contact.Id + ".json", writeToJson);
        IEnumerable<Contact> toAdd = new[] { contact }; 
        Contacts?.Add(contact);
        WriteLine("Success");
    }

    public void ViewContacts()
    {
        if (Contacts.Any())
        {
            foreach (var contact in Contacts!)
            {
                WriteLine("Contacts List:");
                WriteLine($"{contact.Id} : {contact.Name} : {contact.PhoneNumber} : {contact.Mail}");
                WriteLine("**");
            }

            return;
        }
        
        var files = Directory.GetFiles(PathWritingContact);

        foreach (var file in files)
        {
            var readToJson = File.ReadAllText(file);
            var obj = JsonConvert.DeserializeObject<Contact>(readToJson); // что такое сериализация и десереализация
            if (obj != null) Contacts?.Add(obj);
        }

        
    }

    public void SearchContacts()
    {
        var foundContacts = new List<Contact>();
        WriteLine("Enter a parameter");
        var searchParameter = ReadLine();

        foreach (var file in Contacts!)
        {
            if (Contacts != null)
                foundContacts = Contacts
                    .Where(c => c.Name == searchParameter // Where? Select?
                                || c.Mail == searchParameter
                                || c.PhoneNumber == searchParameter)
                    .ToList(); // ToList?
        }

        foreach (var foundContact in foundContacts)
        {
            WriteLine($"ID:{foundContact.Id}");
            WriteLine($"Name:{foundContact.Name}");
            WriteLine($"Phone:{foundContact.PhoneNumber}");
            WriteLine($"E-Mail:{foundContact.Mail}");
        }

    }

    public void RemoveContact()
    {
         WriteLine("Enter an ID");
         if (Guid.TryParse(ReadLine(), out var id))
         {
             WriteLine($"Contact with Id {id} was not found");
         }

         var foundContact = Contacts
             .FirstOrDefault(foundContact => foundContact.Id == id);
         
         if (foundContact is not null)
         {
             Contacts.Remove(foundContact);
                 
             if (File.Exists(PathWritingContact + $@"{id}.json"))
             {
                 File.Delete(PathWritingContact + $@"{id}.json");
             }
             
             WriteLine("Success");
             return;
         }
         WriteLine($"Contact with Id {id} was not found");
    }

    public void UpdateContact()
    {
        WriteLine("Enter an ID");
        if (Guid.TryParse(ReadLine(), out var id)) { }

        foreach (var foundContact in Contacts!)
        {
            if (foundContact.Id == id)
            {
                WriteLine($"Contact with Id {id} was not found");
            }

            WriteLine("Enter a new name:");
            var newName = ReadLine();
            
            WriteLine("Enter a new phone number:");
            var newPhoneNumber = ReadLine();

            WriteLine("Enter a new e-mail:");
            var newEmail = ReadLine();

            foundContact.Name = string.IsNullOrWhiteSpace(newName)
                ? foundContact.Name
                : newName;
            
            foundContact.PhoneNumber = string.IsNullOrWhiteSpace(newPhoneNumber)
                ? foundContact.PhoneNumber
                : newPhoneNumber;
            
            foundContact.Mail = string.IsNullOrWhiteSpace(newEmail)
                ? foundContact.Mail
                : newEmail;
            
            var json = JsonConvert.SerializeObject(foundContact, Formatting.Indented);
            File.WriteAllText(PathWritingContact + $@"{id}.json", json);
            WriteLine("Success");
        }
    } }



     