using static System.Console;

namespace AddressBoook
{
    public static class Program
    {
        static int width = Console.LargestWindowWidth / 2 + 1;
        static int height = Console.LargestWindowHeight;
        public static void Main(string[] args)
        {
            var addressbook = new AddressBook();
            while (true)
            {
                WriteLine("Choose an action:");
                WriteLine("1. Add a contact");
                WriteLine("2. View contacts");
                WriteLine("3. Search contacts");
                WriteLine("4. Remove a contact");
                WriteLine("5. Update the contact");
                WriteLine("6. Goodbye!");

                var parsed = int.TryParse(ReadLine(), out var action);
                if (!parsed)
                {
                    throw new InvalidOperationException("Wrong action");
                }

                switch (action)
                {
                    case 1:
                        addressbook.AddContact();
                        break;
                    case 2:
                        addressbook.ViewContacts();
                        break;
                    case 3:
                        addressbook.SearchContacts();
                        break;
                    case 4:
                        addressbook.RemoveContact();
                          break;
                    case 5:
                        addressbook.UpdateContact();
                        break;
                    case 6:
                        return; 
                    default:
                            WriteLine("Wrong action");
                        break;
                }
            }
        }
    }
}