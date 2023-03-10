using System.Collections;
using Newtonsoft.Json;
using static System.Console;

namespace AddressBoook
{
    class Program
    {
        public static void Main(string[] args)
        {
            AddressBook addressbook = new AddressBook();
            string[] files = Directory.GetFiles(AddressBook.PathWritingContact);
            foreach (var file in files)
            {
                var readToJson = File.ReadAllText(file);
                Contact? obj = JsonConvert.DeserializeObject<Contact>(readToJson);
                
                    if (obj != null)
                        addressbook.Contacts.Add(obj);
            }

            while (true)
            {
                WriteLine("Choose an action:");
                WriteLine("1. Add a contact");
                WriteLine("2. View contacts");
                WriteLine("3. Search contacts");
                WriteLine("4. Remove a contact");
                WriteLine("5. Goodbye!");

                var parsed = int.TryParse(ReadLine(), out var action);
                if (!parsed)
                {
                    throw new InvalidOperationException("Wrong action");
                }

                switch (action)
                {
                    case 1:
                        AddressBook.AddContact(addressbook);
                        break;
                    case 2:
                        AddressBook.ViewContacts(addressbook);
                        break;
                    case 3:
                        AddressBook.SearchContacts(addressbook);
                        break;
                     case 4:
                         AddressBook.RemoveContact(addressbook);
                         break;
                    case 5:
                        return;
                    default:
                        WriteLine("Wrong action");
                        break;
                }
            }
        }
    }
}