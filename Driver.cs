// Tuan Vo
// CPSC 3200
// Assignment 2

using System.Runtime.ConstrainedExecution;
using System.Text;

namespace HW_5
{

    class Driver
    {

        static void Main(String[] args)
        {
            Console.WriteLine("Welcome to the Flight & Passenger Program!\n");

            ///******************************************
            // *              Test Flight Class
            // *******************************************/

            Console.WriteLine("\n---o--- Testing Flight Class Section: ---o---");
            Flight flight_1 = new Flight(initialize_Pass_array_1(), "BOE_1234", "Ho Chi Minh", "Seattle");

            ///****************************************************
            // *        Test pass_list after constructing
            // ***************************************************/
            Test_pass_list_constructed(flight_1);

            ///****************************************************
            // *        Testing Flight function
            // ***************************************************/
            Test_open_boarding(flight_1);

            Test_departure_prep(flight_1);

            Test_last_call_boarding(flight_1);

            Test_depart_airport(flight_1);

            Test_landing(flight_1);

            ///******************************************
            // *              Test Deep Copy Class
            // *******************************************/
            Test_deep_copy(flight_1);


            ///******************************************
            // *              Test Observer
            // *******************************************/
            Test_Observer_ParentMonitor();
            Test_Observer_Flight_Attendents();

            //Console.WriteLine("Thank you for using my program");
        }

        static void Test_pass_list_constructed(Flight flight)
        {
            Console.WriteLine("Testing printing passenger list after constructing: ");
            Console.WriteLine(flight.print_pass_list());
        }

        static void Test_remove_passenger(Flight flight)
        {
            Console.WriteLine("---o--- Testing remove passenger function: ---o---\n");
            flight.removePassenger(new Passenger("Thinh", Passenger.Condition.None, new Seat(), "561YHN132", "pass8@gmail.com", Passenger.Pass_Status.Invalid));
            Console.WriteLine(flight.print_pass_list());
        }

        static void Test_open_boarding(Flight flight)
        {
            Console.WriteLine("\n-----o---- Opening Boarding Ability ----o----");
            flight.boardFlight();
            Console.Write("Testing board_list and wait_list after opening boarding\n");
            Console.WriteLine(flight.print_board_list());
            Console.WriteLine(flight.print_wait_list());
        }

        static void Test_departure_prep(Flight flight)
        {
            Console.WriteLine("-----o---- Preparing Departure ----o----\n");
            flight.departurePrep();
            Console.WriteLine("Testing Wait_list after calling departurePrep(): \n");
            Console.WriteLine(flight.print_board_list());
            Console.WriteLine(flight.print_wait_list());
        }

        static void Test_last_call_boarding(Flight flight)
        {
            Console.WriteLine("-----o---- Last Call Boarding ----o----\n");
            flight.lastCallBoarding();
            Console.WriteLine("Testing board_list and Wait_list after LastCallBoarding()\n");

            //board_list must have 7 passengers since there are 3 passengers has status None
            Console.WriteLine(flight.print_board_list());
            Console.WriteLine(flight.print_wait_list());
        }

        static void Test_depart_airport(Flight flight)
        {
            Console.WriteLine("-----o---- Depart Airport ----o----\n");
            Console.WriteLine("Checking status of passengers of boarding list: (All In-Flight)\n");
            flight.departAirport();
            Console.WriteLine(flight.print_board_list());
            Console.WriteLine("The current status of the flight is: " + flight.get_Status().ToString() + "\n");
        }

        static void Test_landing(Flight flight)
        {
            Console.WriteLine("-----o---- Landed ----o----");
            Console.WriteLine("Checking status of passengers of boarding list: (All Landed)");
            flight.flightLanded();
            Console.WriteLine(flight.print_board_list());
            Console.WriteLine("The current status of the flight is: " + flight.get_Status().ToString() + "\n");
        }

        static void Test_deep_copy(Flight flight)
        {
            Console.WriteLine("-----o---- Deep Copy ----o----");
            Console.WriteLine("Printing Flight_1 before copying");
            Console.WriteLine(flight.print_pass_list());
            Console.WriteLine("Printing Flight_1 after copying");
            Flight flight_2 = new Flight(initialize_Pass_array_2(), "Airbus", "HCM", "Seattle");
            flight = new Flight(flight_2);
            Console.WriteLine(flight.print_pass_list());
        }

        static void Test_Observer_ParentMonitor()
        {
            Console.WriteLine("-----o---- Observer _ Parent Monitor ----o----");
            Passenger[] pass_list = initialize_Pass_array_2();

            Flight flight = new WatchedFlight(pass_list, "Boeing", "HCM", "Seattle");

            Observer parent_1 = new ParentMonitor(pass_list[0], "pass0@gmail.com", Console.Out);

            // please change the directory when you test it
            Observer parent_2 = new ParentMonitor(pass_list[1], "pass1@gmail.com", "/Users/tuanvo/Documents/Seattleu/CPSC3200/HW_5/parent_monitor.txt");
            Observer parent_3 = new ParentMonitor(pass_list[2], "pass2@gmail.com", Console.Out);

            parent_1.Subscribe((WatchedFlight) flight);
            parent_2.Subscribe((WatchedFlight) flight);
            parent_3.Subscribe((WatchedFlight)flight);

            Console.WriteLine("---****Test notify Parent Monitor\n");
            flight.boardFlight();

            flight.departurePrep();

            flight.lastCallBoarding();

            Console.WriteLine("---****Test notify Parent Monitor\n");
            flight.departAirport();
        }

        static void Test_Observer_Flight_Attendents()
        {
            Console.WriteLine("-----o---- Observer _ Flight Attendent Monitor ----o----");
            Passenger[] pass_list = initialize_Pass_array_2();

            Flight flight = new WatchedFlight(pass_list, "Boeing", "HCM", "Seattle");
            Observer attendent_1 = new FlightAttendentMonitor("Tuan", Console.Out);
            Observer attendent_2 = new FlightAttendentMonitor("Nga", "/Users/tuanvo/Documents/Seattleu/CPSC3200/HW_5/attendent_monitor.txt");

            attendent_1.Subscribe((WatchedFlight) flight);
            attendent_2.Subscribe((WatchedFlight)flight);

            flight.boardFlight();

            Console.WriteLine("---****Test departurePrep() notify Attedent Monitor\n");
            flight.departurePrep();

            Console.WriteLine("---****Test LastCallBoarding() notify Attedent Monitor\n");
            flight.lastCallBoarding();
        }

        static Passenger[] initialize_Pass_array_1()
        {
            Passenger pass1 = new Passenger("Tuan", Passenger.Condition.None, new Seat(), "123ASD13", "pass1@gmail.com", Passenger.Pass_Status.Boarding);
            Passenger pass2 = new StandbyPassenger("Vo", Passenger.Condition.None, "456DSA123", "pass2@gmail.com", Passenger.Pass_Status.Boarding);
            Passenger pass3 = new StandbyPassenger("Anh", Passenger.Condition.Disability, "786SAI126", "pass3@gmail.com", Passenger.Pass_Status.Checked_in);
            Passenger pass4 = new StandbyPassenger("Nguyen", Passenger.Condition.Infant, "131FDS125", "pass4@gmail.com", Passenger.Pass_Status.Boarding);
            Passenger pass5 = new FreqPassenger("Thuy", Passenger.Condition.Age, new Seat(), "131FDS138", "pass5@gmail.com", Passenger.Pass_Status.Checked_in, 10);
            Passenger pass6 = new FreqPassenger("Nga", Passenger.Condition.None, new Seat(), "453UHU543", "pass6@gmail.com", Passenger.Pass_Status.Checked_in, 10);
            Passenger pass7 = new Passenger("Le", Passenger.Condition.None, new Seat(), "890WER123", "pass7@gmail.com", Passenger.Pass_Status.None);
            Passenger pass8 = new Passenger("Thi", Passenger.Condition.None, new Seat(), "561YHN132", "pass8@gmail.com", Passenger.Pass_Status.Boarding);
            Passenger pass9 = new Passenger("Kim", Passenger.Condition.Infant, new Seat(Seat.Seat_Row.D, 12), "825KMI654", "pass9@gmail.com", Passenger.Pass_Status.Checked_in);
            Passenger pass10 = new Passenger("Chi", Passenger.Condition.Age, new Seat(Seat.Seat_Row.C, 20), "123IUA468)", "pass10@gmail.com", Passenger.Pass_Status.Boarding);
            Passenger pass11 = new Passenger("Thinh", Passenger.Condition.None, new Seat(), "561YHN132", "pass8@gmail.com", Passenger.Pass_Status.Invalid);

            int num_pass = 11;
            Passenger[] pass_list = new Passenger[num_pass];
            pass_list[0] = pass1;
            pass_list[1] = pass2;
            pass_list[2] = pass3;
            pass_list[3] = pass4;
            pass_list[4] = pass5;
            pass_list[5] = pass6;
            pass_list[6] = pass7;
            pass_list[7] = pass8;
            pass_list[8] = pass9;
            pass_list[9] = pass10;
            pass_list[10] = pass11;

            return pass_list;
        }

        static Passenger[] initialize_Pass_array_2()
        {
            Passenger pass1 = new Passenger("Tuan", Passenger.Condition.None, new Seat(Seat.Seat_Row.A, 12), "123ASD13", "pass0@gmail.com", Passenger.Pass_Status.Boarding);
            Passenger pass2 = new Passenger("Vo", Passenger.Condition.None, new Seat(Seat.Seat_Row.B, 2), "456DSA123", "pass1@gmail.com", Passenger.Pass_Status.Boarding);
            Passenger pass3 = new Passenger("Anh", Passenger.Condition.Disability, new Seat(Seat.Seat_Row.C, 25), "786SAI126", "pass2@gmail.com", Passenger.Pass_Status.None);
            Passenger pass4 = new Passenger("Nguyen", Passenger.Condition.Infant, new Seat(Seat.Seat_Row.D, 18), "131FDS125", "pass3@gmail.com", Passenger.Pass_Status.Boarding);
            int num_pass = 4;
            Passenger[] pass_list = new Passenger[num_pass];
            pass_list[0] = pass1;
            pass_list[1] = pass2;
            pass_list[2] = pass3;
            pass_list[3] = pass4;
            return pass_list;
        }
    }
}