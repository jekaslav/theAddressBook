using static System.Console;
using System.Collections.Generic;

namespace AddressBoook;

public class Contact
{
        public string? Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        public Guid Id { get; set; }
}