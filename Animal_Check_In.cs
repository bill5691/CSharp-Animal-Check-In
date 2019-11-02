// Program: PET-ER Animal Check-In
// Author: Bill Skiles
// Description: Basic concept for client check-in system for a veterinary emergency room where clients can check-in their pet
// prior to visiting the facility, similar to online patient check-in systems used by hospitals, clinics, and doctor offices. 
// Implementing this type of system can speed up the check-in process for both the client and emergency facility and can be
// used for quick client record lookup.
//
// Version 2: Added menu functionality, giving users the ability to choose between adding records, displaying all available records, 
// or accessing a submenu system for looking up client records (if any)

using System;
using System.Collections.Generic;
using System.Linq;

namespace Animal_Check_In
{
    
    class Client
    {
        public string f_name;
        public string m_initial;
        public string l_name;
        public string p_name;
        public string p_gender;
        public int p_age;
        public string p_type;
        public string p_breed;
        public string p_fixed;
        public int client_id;
        public string visit_desc;

        // Constructor for new clients
        public Client(
            string f_name, 
            string m_initial, 
            string l_name, 
            string p_name, 
            string p_gender, 
            int p_age, 
            string p_type, 
            string p_breed, 
            string p_fixed, 
            int client_id, 
            string visit_desc)
        {
            this.f_name = f_name;
            this.m_initial = m_initial;
            this.l_name = l_name;
            this.p_name = p_name;
            this.p_gender = p_gender;
            this.p_age = p_age;
            this.p_type = p_type;
            this.p_breed = p_breed;
            this.p_fixed = p_fixed;
            this.client_id = client_id;
            this.visit_desc = visit_desc;
        }

        public override string ToString()
        {
            return "\n  First Name: " + f_name + 
                "\n  Middle Initial: " + m_initial + 
                "\n  Last Name: " + l_name + 
                "\n  Pet Name: " + p_name + 
                "\n  Pet Gender: " + p_gender + 
                "\n  Pet Age: " + p_age + 
                "\n  Pet Type: " + p_type + 
                "\n  Pet Breed: " + p_breed + 
                "\n  Fixed: " + p_fixed + 
                "\n  Client ID: " + client_id + 
                "\n  Reason for visit:\n  " + visit_desc;
        }
    }
    

    class Check_In_Application
    {
        static void Main(string[] args)
        {
            string f_name;
            string m_initial;
            string l_name;
            string p_name;
            string p_gender;
            string age;
            int p_age;
            string p_type;
            string p_breed;
            string p_fixed = "";
            string visit_desc;
            string option;
            int client_id = 1;
            bool exit_code = false;

            // Creates a List collection to store client records
            List<Client> clients = new List<Client>();

            // Display menu options to the user
            Console.WriteLine("\n\tPET-ER Client Check-In\n");
            Console.WriteLine(" Options:\n 1 - Add Client Record\n 2 - Display all Client Records\n 3 - Lookup Client Record\n 0 - Exit");
            Console.Write("\n Please enter an option: ");
            option = Console.ReadLine();

            // Gives error message for invalid option choices
            while (!option.Equals("1") && !option.Equals("2") && 
                !option.Equals("3") && !option.Equals("0"))
            {
                Console.Write(" Error: Please select a valid option: ");
                option = Console.ReadLine();
            }

            // Loop that allows user to select different options until option 0 is entered to exit
            while (exit_code == false)
            {
                switch(option)
                {
                    case "1":
                        Console.Write("\n Owner First Name: ");
                        f_name = Console.ReadLine();

                        Console.Write(" Owner Middle Initial: ");
                        m_initial = Console.ReadLine();
                        while (m_initial.Length > 1)
                        {
                            Console.Write(" Error - Enter one letter for M. Initial: ");
                            m_initial = Console.ReadLine();
                        }

                        Console.Write(" Owner Last Name: ");
                        l_name = Console.ReadLine();

                        Console.Write(" Pet Name: ");
                        p_name = Console.ReadLine();

                        Console.Write(" Pet Gender(m/f): ");
                        p_gender = Console.ReadLine();
                        while (!p_gender.Equals("m") && !p_gender.Equals("f"))
                        {
                            Console.Write(" Error - Enter \"m\" for male or \"f\" for female: ");
                            p_gender = Console.ReadLine();
                        }

                        Console.Write(" Pet Age: ");
                        age = Console.ReadLine();
                        while (!int.TryParse(age, out p_age))
                        {
                            Console.Write(" Error - Enter a number for age: ");
                            age = Console.ReadLine();
                        }

                        Console.Write(" Pet Type: ");
                        p_type = Console.ReadLine();

                        Console.Write(" Pet Breed: ");
                        p_breed = Console.ReadLine();

                        p_fixed = pFixed(p_gender);
                        
                        Console.Write(" Reason for visit: ");
                        visit_desc = Console.ReadLine();

                        Console.WriteLine("\n Adding record...\n");

                        // Adds a new client to the list using entered information
                        clients.Add(new Client(
                            f_name,
                            m_initial,
                            l_name,
                            p_name,
                            p_gender,
                            p_age,
                            p_type,
                            p_breed,
                            p_fixed,
                            client_id,
                            visit_desc
                        ));

                        // Increments Client ID for next record addition
                        client_id++;

                        Console.Write("\n Please enter an option: ");
                        option = Console.ReadLine();

                        break;
                    case "2":
                        if (clients.Any())
                        {
                            Console.WriteLine(" Displaying Client Records:");

                            // Displays each recorded client in the list collection (if any)
                            foreach (Client client in clients)
                            {
                                Console.WriteLine(client);
                            }
                        }
                        else
                        {
                            Console.WriteLine(" No records to display\n");
                        }

                        Console.Write("\n Please enter an option: ");
                        option = Console.ReadLine();

                        break;
                    case "3":
                        ClientLookup(clients);

                        Console.Write("\n Please enter an option: ");
                        option = Console.ReadLine();

                        break;
                    case "0":
                        exit_code = true;

                        break;
                    default:
                        while (!option.Equals("1") && !option.Equals("2") && 
                            !option.Equals("3") && !option.Equals("0"))
                        {
                            Console.Write(" Error: Please select a valid option: ");
                            option = Console.ReadLine();
                        }

                        break;
                }
            } 

            // End application
            Console.WriteLine("\n Press any key to close...");
            Console.ReadKey();

        }

        // Method that determines if the pet is fixed; returns string
        private static string pFixed(string gdr)
        {
            string p_gender = gdr;
            string p_fixed = "";

            // Switch statement that displays "neutered" or "spayed" based on the pet's gender
            switch (p_gender)
            {
                case "m":
                    Console.Write(" Neutered?(y/n): ");
                    p_fixed = Console.ReadLine();

                    while (!p_fixed.Equals("y") && !p_fixed.Equals("n"))
                    {
                        Console.Write(" Error - Enter \"y\" for yes or \"n\" for no: ");
                        p_fixed = Console.ReadLine();
                    }
                    break;
                case "f":
                    Console.Write(" Spayed?(y/n): ");
                    p_fixed = Console.ReadLine();

                    while (!p_fixed.Equals("y") && !p_fixed.Equals("n"))
                    {
                        Console.Write(" Error - Enter \"y\" for yes or \"n\" for no: ");
                        p_fixed = Console.ReadLine();
                    }
                    break;
            }

            return p_fixed;
        }

        // Method for client lookup submenu using LINQ to search through records for the given lookup option
        private static void ClientLookup(List<Client> c)
        {
            string lookup_option;
            bool exit_lookup = false;
            string fName;
            string lName;
            string pName;
            string pType;
            string c_id;
            int clientId;
            List<Client> clients = c;

            if (clients.Any())
            {
                Console.WriteLine("\n  Lookup client records by:\n   1 - Owner First Name\n   2 - Owner Last Name\n   3 - Pet Name\n   4 - Pet Type\n   5 - Client ID\n   0 - Exit Lookup");
                Console.Write("\n  Enter a lookup option: ");
                lookup_option = Console.ReadLine();
                
                while (exit_lookup == false)
                {
                    switch (lookup_option)
                    {
                        case "1":
                            Console.Write("  Enter Owner First Name: ");
                            fName = Console.ReadLine();

                            var fn = from client in clients
                                     where String.Equals(client.f_name, fName, StringComparison.OrdinalIgnoreCase)
                                     select client;

                            if (!fn.Any())
                            {
                                Console.WriteLine("  No Client Records Found");
                            }
                            else
                            {
                                foreach (var qfirst in fn)
                                {
                                    Console.WriteLine(qfirst);
                                }
                            }

                            Console.Write("\n  Enter a lookup option: ");
                            lookup_option = Console.ReadLine();

                            break;
                        case "2":
                            Console.Write("  Enter Owner Last Name: ");
                            lName = Console.ReadLine();

                            var ln = from client in clients
                                     where String.Equals(client.l_name, lName, StringComparison.OrdinalIgnoreCase)
                                     select client;

                            if (!ln.Any())
                            {
                                Console.WriteLine("  No Client Records Found");
                            }
                            else
                            {
                                foreach (var qlast in ln)
                                {
                                    Console.WriteLine(qlast);
                                }
                            }

                            Console.Write("\n  Enter a lookup option: ");
                            lookup_option = Console.ReadLine();

                            break;
                        case "3":
                            Console.Write("  Enter Pet Name: ");
                            pName = Console.ReadLine();

                            var pn = from client in clients
                                     where String.Equals(client.p_name, pName, StringComparison.OrdinalIgnoreCase)
                                     select client;

                            if (!pn.Any())
                            {
                                Console.WriteLine("  No Client Records Found");
                            }
                            else
                            {
                                foreach (var qpet in pn)
                                {
                                    Console.WriteLine(qpet);
                                }
                            }

                            Console.Write("\n  Enter a lookup option: ");
                            lookup_option = Console.ReadLine();

                            break;
                        case "4":
                            Console.Write("  Enter Pet Type: ");
                            pType = Console.ReadLine();

                            var pt = from client in clients
                                     where String.Equals(client.p_type, pType, StringComparison.OrdinalIgnoreCase)
                                     select client;

                            if (!pt.Any())
                            {
                                Console.WriteLine("  No Client Records Found");
                            }
                            else
                            {
                                foreach (var qtype in pt)
                                {
                                    Console.WriteLine(qtype);
                                }
                            }

                            Console.Write("\n  Enter a lookup option: ");
                            lookup_option = Console.ReadLine();

                            break;
                        case "5":
                            Console.Write("  Enter Client ID: ");
                            c_id = Console.ReadLine();

                            while (!int.TryParse(c_id, out clientId))
                            {
                                Console.Write(" Error - Enter a number for Client ID: ");
                                c_id = Console.ReadLine();
                            }

                            var cid = from client in clients
                                      where client.client_id == clientId
                                      select client;

                            if (!cid.Any())
                            {
                                Console.WriteLine("  No Client Records Found");
                            }
                            else
                            {
                                foreach (var qid in cid)
                                {
                                    Console.WriteLine(qid);
                                }
                            }

                            Console.Write("\n  Enter a lookup option: ");
                            lookup_option = Console.ReadLine();

                            break;
                        case "0":
                            exit_lookup = true;
                            Console.WriteLine("  Returning to main menu...\n");

                            break;
                        default:
                            while (!lookup_option.Equals("1") && !lookup_option.Equals("2") && 
                                !lookup_option.Equals("3") && !lookup_option.Equals("4") && 
                                !lookup_option.Equals("5") && !lookup_option.Equals("0"))
                            {
                                Console.Write(" Error: Please select a valid option: ");
                                lookup_option = Console.ReadLine();
                            }

                            break;
                    }
                }
            }
            else
            {
                // Returns to main menu if no records are available
                Console.WriteLine("  No records available to lookup. Exitting client lookup...");
            }
        }
    }
}
