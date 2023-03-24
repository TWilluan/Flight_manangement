//Tuan Vo
//CPSC 3200
//Assignment 3 - Flight

/****************************************************
*                   Airplane Class
*****************************************************/
namespace HW_5
{
    /****************************************************
    *                   Flight Class
    *****************************************************/

    /* ----o---- Class Variants ----o----
     * 
     * This Flight class is designed encapsulated and contractual. This class contains
     *      Flight basic information:
     *          + num_pass , num_board , num_wait : the number of passenger in certain list
     *          + pass_list, board_list, wait_list: the certain list of passengers
     *          + plane         : plane type and number of seats
     *          + seats[][]     : used to check if the seat is avalable
     *          + flight_no     : Cannot be blanked
     *          + origin        : Cannot be blanked
     *          + destination   : Cannot be blanked
     *          + status        : None, Boarding, In-flight, Landed
     * 
     *  There are some private supporting functions to help the main operation
     *       
    */

    /**----o---- Class Variants ----o----
     * 
     *  + addPassenger(Passenger pass):
     *      This function is used to add more Passenger to the flight list.
     *          If the passenger's status is boarding and has a seat, the passenger
     *          will be also added to BoardList, if not, they will also add wait_list.
     * 
     *  + removePassenger(Passenger pass):
     *      This function is used to remove Passenger who want to the cancel the flight.
     *          The status of the class has to be None to successfully remove the passenger
     * 
     *  + changeSeat(Passenger pass, Seat seat):
     *      This function is used to change Passenger seat to certain seat if that seat
     *          is available. This function is allowed when the Flight status is None and Boarding
     * 
     *  + departurePrep():
     *      This function is used to process the wait_list.
     *      3 phases:
     *          phase 1: add wait_list that has seat and status: Checked-in to board_list
     *          phase 2: Assign seat to passenger that has no seat
     *          phase 3: Assign seat to Standbypassenger
     * 
     *  + boardFlight();
     *      This function is used to open the ability for board flight. 
     *      It process pass_list to produce board_list and wait_list. Passengers have boarding 
     *      status and valid seats will be add to board_list, otherwise, they will add to wait_list;
     *          Wait_list include StandbyPassenger, Checked-in, no seat, has same seat.
     * 
     *  + lastCallBoarding():
     *      2 phases:
     *          add passenger in wait_list in board_list
     *          add StandByPassenger to board_list
     *
     *  + departAirport():
     *      This function is used to change the flight status to in-flight. Also change the passenger
     *          status to in-flight
     * 
     *  + flightLanded():
     *      This function is used to change the flight status to Landed. Also change the passenger
     *          status to Landed
     * 
     *  + shareFlightInfo(Passenger pass) const:
     *      This function is used to provdie flight information to passenger who is on the flight;
     * 
    */

    public class Flight
    {
        public enum Plane_Status { None, Boarding, In_flight, Landed }

        private const int MAX_CAPACITY = 160;

        private const int ROW = 8;
        private const int COL = 30;

        private const int FREE_FIRST      = 7;
        private const int FIRST_CLASS_COL = 8;

        private const int FRONT_EXIT    = 0;
        private const int MIDDLE_EXIT   = ROW / 2;
        private const int BACK_EXIT     = ROW - 1;

        private Passenger[] pass_list;  //list of passenger
        private Passenger[] board_list; //list of boarding passenger
        private Passenger[] wait_list;  //list of wating-list passenger

        //num_pass : number of passenger in the list of that flight
        //num_board: number of passenger that are boarding of that flight
        //num_wait : number of passenger that are in flight's waiting list
        private int num_pass;
        private int num_board = 0, num_wait = 0;

        //seat 2D array: False (available to assigned to passenger)
        //               True  (already taken)
        private bool[,] seats;

        private readonly string flight_no; //flight number
        private readonly string origin, destination;
        private Plane_Status status; //status of flight (None, Boarding, In-flight, Landed)

        //default constructor
        public Flight()
        {
            this.flight_no  = "";
            this.origin     = "";
            this.destination= "";
            this.status     = Plane_Status.None;
            pass_list       = new Passenger[] { };
            board_list      = new Passenger[] { };
            wait_list       = new Passenger[] { };
            seats           = new bool[,] { };
        }

        //Parameterized constructor
        public Flight(Passenger[] others, string flight_no, string origin, string destination)
        {
            if (flight_no == "")
                throw new Exception("Flight-no cannot be blanked");
            this.flight_no = flight_no;

            if (origin == "" || destination == "")
                throw new Exception("Orgin and Destination cannot be blanked");
            this.origin = origin;
            this.destination = destination;

            this.status = Plane_Status.None;

            ////////////////////////////////////////////////////
            //              Initialize array
            ///////////////////////////////////////////////////

            num_pass = others.Length; // initialize passenger list
            pass_list = new Passenger[num_pass];
            for (int i = 0; i < num_pass; i++)
                pass_list[i] = others[i];

            board_list = new Passenger[num_board];

            wait_list = new Passenger[num_wait];

            //default is false for array initilization
            seats = new bool[COL, ROW];
        }

        //Copy constructor
        public Flight(Flight other)
        {
            // normal attribute copy
            flight_no   = other.flight_no;
            origin      = other.flight_no;
            destination = other.destination;
            status      = other.status;
            num_pass    = other.num_pass;
            num_board   = other.num_board;
            num_wait    = other.num_wait;

            // Deep copy dynamic array
            pass_list = new Passenger[num_pass];
            for (int i = 0; i < num_pass; i++)
                pass_list[i] = other.pass_list[i];

            board_list = new Passenger[num_board];
            for (int i = 0; i < num_board; i++)
                board_list[i] = other.board_list[i];

            wait_list = new Passenger[num_wait];
            for (int i = 0; i < num_wait; i++)
                wait_list[i] = other.wait_list[i];

            // initialize and copy seats[][]
            seats = new bool[COL, ROW];
            for (int i = 0; i < COL; i++)
            {
                for (int j = 0; j < ROW; j++)
                    seats[i, j] = other.seats[i, j];
            }
        }

        // addPassenger: add Passenger to pass_list
        // if the plane is boarding, the passenger will be add
        // to board_list or wait_list
        public void addPassenger(Passenger pass)
        {
            //create a new passenger
            Passenger[] temp = new Passenger[num_pass + 1];
            for (int i = 0; i < num_pass; i++)
                temp[i] = pass_list[i]; //copy content
            temp[num_pass++] = pass;    //increase size and add passenger
            pass_list = temp;

            // Check if added passenger should belong to which list
            if (this.status == Plane_Status.Boarding)
            {
                if (isPass_Can_Board(pass))
                    addPassenger_board(pass);
                else
                    addPassenger_wait(pass);
            }
        }

        // supporting function for addPassenger()
        // check if certian passenger satisfy the condition to be added to board_list
        private bool isPass_Can_Board(Passenger pass)
        {
            if (pass.getType() != Passenger.Type.Standby &&
                pass.getStatus() == Passenger.Pass_Status.Boarding &&
                pass.getSeat().get_row() != Seat.Seat_Row.None &&
                !seats[pass.getSeat().get_num() - 1, (int)pass.getSeat().get_row()])
                return true;
            return false;
        }

        //Supporting function for addPassenger()
        //implemented to be used to add passenger to Board_list
        //      mark the seats is unavailable
        private void addPassenger_board(Passenger pass)
        {
            //check if the number of board_list exceeds the plane's capacity
            if (num_board + 1 < MAX_CAPACITY && pass.getStatus() != Passenger.Pass_Status.Invalid)
            {
                Passenger[] temp = new Passenger[num_board + 1];
                for (int i = 0; i < num_board; i++)
                    temp[i] = board_list[i];
                temp[num_board++] = pass;
                board_list = temp;

                //set seat to be unavailable
                int num = pass.getSeat().get_num() - 1;
                int row = (int)pass.getSeat().get_row();
                seats[num, row] = true;
            }
        }

        // Supporting function for addPassenger()
        // implemented to be used to add passenger to wait_list
        private void addPassenger_wait(Passenger pass)
        {
            if (pass.getStatus() != Passenger.Pass_Status.Invalid)
            {
                Passenger[] temp = new Passenger[num_wait + 1];
                for (int i = 0; i < num_wait; i++)
                    temp[i] = wait_list[i];
                temp[num_wait++] = pass;
                wait_list = temp;
            }
        }

        // removePassenger: @Passenger
        // remove passenger out of pass_list
        // 
        // pre-condition: the status of plane must be None
        // post-condition: decrease the size of pass_list
        public void removePassenger(Passenger pass)
        {
            if (this.status == Plane_Status.None) // only available before boarding
            {
                for (int i = 0; i < num_pass; i++)
                {
                    if (pass_list[i].getName() == pass.getName() &&
                        pass_list[i].getGroup() == pass.getGroup())
                    {
                        // shifting element
                        for (int j = i; j < num_pass - 1; j++)
                            pass_list[j] = pass_list[j + 1];
                        num_pass--;
                    }
                }
            }
        }

        //departurePrep():
        //      phase1: priotize processing people that already have seats and checked-in
        //      phase2: assign seat to Regualr and Frequent Passenger
        //      phase3: assign seat to Standby Passenger
        //  precondition: plane's status = boarding
        public virtual void departurePrep()
        {
            if (status == Plane_Status.Boarding)
            {
                departurePrep_have_seat();
                departurePrep_not_have_seat();
                depareturePrep_Standby();
            }
        }

        // Supporting function
        // This function is the phase 1 of departurePrep()
        //      it will the valid Passenger (checked-in and having seat)
        //      to board_list and remove he/she from wait_list
        private void departurePrep_have_seat()
        {
            for (int i = 0; i < num_wait; i++)
            { //check if that passenger have seat and is checked-in
                if (wait_list[i].getSeat().get_row() != Seat.Seat_Row.None &&
                    wait_list[i].getStatus() == Passenger.Pass_Status.Checked_in)
                {
                    wait_list[i].change_status(Passenger.Pass_Status.Boarding);
                    addPassenger_board(wait_list[i]);
                    removePassenger_wait(wait_list[i--]);
                }
            }
        }

        // Supporting function
        // This function is the phase 2 of departurePrep()
        //      it will assgin seat to Regular and Frequent Passenger
        //      Healthy (Condition.None) Passenger will be considered to be assigned
        //          seat at exit row first. Frequent passenger will be get first-class
        //          seat if they meet requirement
        private void departurePrep_not_have_seat()
        {
            for (int i = 0; i < num_wait; i++)
            {
                //checking if that Passenger is the Frequent Passenger
                if (wait_list[i].getType() == Passenger.Type.Frequent &&
                    wait_list[i].getStatus() != Passenger.Pass_Status.None)
                {
                    // number of flight count must be >= plane minimum requirement
                    //      to get a free upgrades to first-class
                    if (wait_list[i].getFlight_count() >= FREE_FIRST)
                    {
                        assign_seat_first(wait_list[i]);
                        if (wait_list[i].getSeat().get_row() == Seat.Seat_Row.None)
                            assign_seat_empty(wait_list[i]);
                        else
                        {
                            int count = wait_list[i].getFlight_count() - FREE_FIRST;
                            wait_list[i].setUpgrade();
                            wait_list[i].setCount(count); //adjust the flight_count
                        }
                    }
                    wait_list[i].change_status(Passenger.Pass_Status.Boarding);
                }
                if (wait_list[i].getType() == Passenger.Type.Regular &&
                    wait_list[i].getStatus() != Passenger.Pass_Status.None)
                {
                    wait_list[i].change_status(Passenger.Pass_Status.Boarding);
                    assign_seat_empty(wait_list[i]);
                }
            }
        }

        // Supporting function
        // This function is the phase 3 of depareturePrep()
        // It will assign seat to StandByPassenger
        private void depareturePrep_Standby()
        {
            for (int i = 0; i < num_wait; i++)
            {
                if (wait_list[i].getType() == Passenger.Type.Standby &&
                    wait_list[i].getStatus() != Passenger.Pass_Status.None)
                {
                    wait_list[i].changeState(State.Depart);
                    wait_list[i].change_status(Passenger.Pass_Status.Boarding);
                    assign_seat_empty(wait_list[i]);
                }
            }
        }

        // Supporting function
        // this function is used to assgined first_class seat to Frequent passenger
        private void assign_seat_first(Passenger pass)
        {
            bool finding = true;
            if (pass.getCondition() == Passenger.Condition.None)
            {
                for (int i = 0; i < COL && finding; i++)
                { // assign exit row
                    if (!seats[0, i])
                    {
                        pass.change_seat(new Seat((Seat.Seat_Row)5, 5));
                        seats[0, i] = true;
                        finding = false;
                    }
                }
            }

            for (int i = 1; i < FIRST_CLASS_COL && finding; i++)
            {   // assign normal row
                for (int j = 0; j < ROW && finding; i++)
                {
                    if (!seats[i, j])
                    {
                        pass.change_seat(new Seat((Seat.Seat_Row)j, i));
                        seats[i, j] = true;
                        finding = false;
                    }
                }
            }
        }

        // Supporting function
        // This function is used to assign seat to Regular and Standby Passenger
        private void assign_seat_empty(Passenger pass)
        {
            bool finding = true;
            if (pass.getCondition() == Passenger.Condition.None)
            { //prioritize exit row
                for (int i = 0; i < ROW && finding; i++)
                {
                    if (!seats[MIDDLE_EXIT, i])
                    {
                        pass.change_seat(new Seat((Seat.Seat_Row)i, MIDDLE_EXIT));
                        seats[MIDDLE_EXIT, i] = true;
                        finding = false;
                    }

                    if (!seats[BACK_EXIT, i])
                    {
                        pass.change_seat(new Seat((Seat.Seat_Row)i, BACK_EXIT));
                        seats[BACK_EXIT, i] = true;
                        finding = false;
                    }
                }
            }

            for (int i = FIRST_CLASS_COL; i < COL && finding; i++)
            { // assign normal seat
                {
                    for (int j = 0; j < ROW && finding; i++)
                    {
                        if (!seats[i, j])
                        {
                            pass.change_seat(new Seat((Seat.Seat_Row)j, i));
                            seats[i, j] = true;
                            finding = false;
                        }
                    }
                }
            }
        }

        // boardFlight() : open boarding ability for the flgiht
        // Process the pass_list to produce the board_list and wait_list
        //      requirement to add board_list: status == boarding
        //                                     seat   != none
        //                                     type   != StandBy
        public virtual void boardFlight()
        {
            status = Plane_Status.Boarding;

            for (int i = 0; i < num_pass; i++)
            {
                if (pass_list[i].getStatus() != Passenger.Pass_Status.Invalid)
                {
                    if (pass_list[i].getStatus() == Passenger.Pass_Status.Boarding &&
                        pass_list[i].getSeat().get_row() != Seat.Seat_Row.None &&
                        pass_list[i].getType() != Passenger.Type.Standby)
                    {
                        int num = pass_list[i].getSeat().get_num() - 1;
                        int row = (int)pass_list[i].getSeat().get_row();

                        if (!seats[num, row]) // if seat is available
                        {
                            addPassenger_board(pass_list[i]);
                            seats[num, row] = true;
                        }
                        else
                        {   //wait until departure prep to be assigned new seat
                            pass_list[i].change_seat(new Seat());
                            addPassenger_wait(pass_list[i]);
                        }
                    }
                    else // no seat or haven't boarding yet
                        addPassenger_wait(pass_list[i]);
                }
            }
        }

        // lastCallBoarding() : process the wait_list and add all valid passenger
        //      to board_list and remove them the waitList
        //          has 2 phase:
        //              boarding the Regular and Frequent Passenger
        //              boarding the Standby Passenger
        public virtual void lastCallBoarding()
        {
            if (status == Plane_Status.Boarding) //if flight is boarding
            {
                lastCallBoarding_notStandby();
                lastCallBoarding_Standby();
            }
        }

        // Suporting function
        // This function is used to boarding the Regular and Frequent Passenger
        // Pre-condition: it will not board the Passenger that has status None
        private void lastCallBoarding_notStandby()
        {
            for (int i = 0; i < num_wait; i++)
            { //add to board_list
                if (wait_list[i].getType() != Passenger.Type.Standby &&
                    wait_list[i].getStatus() != Passenger.Pass_Status.None)
                {
                    addPassenger_board(wait_list[i]);
                    removePassenger_wait(wait_list[i--]);
                }
            }
        }

        // Suporting function
        // This function is used to boarding the StandbyPassenger
        // Pre-condition: it will not board the Passenger that has status None
        private void lastCallBoarding_Standby()
        {
            for (int i = 0; i < num_wait; i++)
                if (wait_list[i].getStatus() != Passenger.Pass_Status.None)
                {
                    addPassenger_board(wait_list[i]);
                    removePassenger_wait(wait_list[i--]);
                }
        }

        // supporting function
        // This function is used to remove Passenger out of wait_list
        private void removePassenger_wait(Passenger pass)
        {
            for (int i = 0; i < num_wait; i++)
            {
                if (wait_list[i].getName() == pass.getName() &&
                    wait_list[i].getGroup() == pass.getGroup())
                { // shifting element
                    for (int j = i; j < num_wait - 1; j++)
                        wait_list[j] = wait_list[j + 1];
                    num_wait--;
                }
            }
        }

        // departAirPort() - used to change the Passenger and Flight status to In-Flight
        public virtual void departAirport()
        {
            for (int i = 0; i < num_board; i++)
            {
                if (board_list[i].getType() == Passenger.Type.Standby)
                    board_list[i].changeState(State.Boarding);
                board_list[i].change_status(Passenger.Pass_Status.In_flight);
            }
            this.status = Plane_Status.In_flight;
        }

        // departAirPort() - used to change the Passenger and Flight status to Landed
        public virtual void flightLanded()
        {
            for (int i = 0; i < num_board; i++)
            {
                if (board_list[i].getType() == Passenger.Type.Standby)
                    board_list[i].changeState(State.None);
                board_list[i].change_status(Passenger.Pass_Status.Landed);
            }
            this.status = Plane_Status.Landed;
        }

        public string shareFlightInfo()
        {
            string res = "Flight: " + flight_no + "\tStatus: " + status.ToString() + "\n";
            res += "From: " + origin + "\tTo: " + destination + "\n";
            return res;
        }

        public bool pass_in_list(Passenger other, string parent_contact)
        {
            if (status == Plane_Status.None)
            {
                foreach (Passenger pass in pass_list)
                {
                    if (pass.getName() == other.getName() &&
                        pass.getGroup() == other.getGroup())
                        return true;
                }
            }

            if (status == Plane_Status.Boarding)
            {
                foreach (Passenger pass in board_list)
                {
                    if (pass.getName() == other.getName() &&
                        pass.getGroup() == other.getGroup())
                        return true;
                }

                foreach (Passenger pass in wait_list)
                {
                    if (pass.getName() == other.getName() &&
                        pass.getGroup() == other.getGroup())
                        return true;
                }
            }
            if (status == Plane_Status.In_flight || status == Plane_Status.Landed)
            {
                foreach (Passenger pass in board_list)
                {
                    if (pass.getName() == other.getName() &&
                        pass.getGroup() == other.getGroup())
                        return true;
                }
            }
            return false;
        }

        public string print_special_pass()
        {
            string res = "Name\tType\t\tCondition\n";
            foreach (Passenger pass in board_list)
            {
                if (pass.getCondition() != Passenger.Condition.None ||
                    (pass.getType() == Passenger.Type.Frequent && pass.getUpgrade()))
                    res += pass.getName() + "\t" + pass.getType().ToString() +
                                            "\t\t" + pass.getCondition() + "\n";
            }

            foreach (Passenger pass in wait_list)
            {
                if (pass.getCondition() != Passenger.Condition.None ||
                    (pass.getType() == Passenger.Type.Frequent && pass.getUpgrade()))
                    res += pass.getName() + "\t" + pass.getType().ToString() +
                                            "\t\t" + pass.getCondition() + "\n";
            }

            return res;
        }

        /********************************************************
         *             Printing Function to Test Function
         *********************************************************/
        public string print_pass_list()
        {
            string res = "*** Passenger List";
            for (int i = 0; i < num_pass; i++)
                res += (i + 1) + "/\n" + pass_list[i].boarding_pass() + "\n";
            return res;
        }


        public string print_board_list()
        {
            string res = "*** Boarding Passenger List";
            for (int i = 0; i < num_board; i++)
                res += (i + 1) + "/\n" + board_list[i].boarding_pass() + "\n";
            return res;
        }


        public string print_wait_list()
        {
            string res = "*** Waiting Passenger List";
            for (int i = 0; i < num_wait; i++)
                res += (i + 1) + "/\n" + wait_list[i].boarding_pass() + "\n";
            return res;
        }

        // Getters function
        public int getNum_pass() { return num_pass; }
        public int getNum_board() { return num_board; }
        public int getNum_wait() { return num_wait; }
        public Plane_Status get_Status() { return this.status; }
    }
    /*----Implementation Variants----
        * 
        * Public Functions:
        *    + addPassenger(Passenger pass):
        *          add Passenger to pass_list. Also call addPassenger_board() 
        *          and addPassenger_wait() to add Passenger to suitatble list.
        *          increase the size of num_pass, num_board, and num_wait relatively.
        *          
        *    + removePassenger(Passenger pass):
        *          remove passenger from pass_list. Also call removePassenger_board() 
        *          and removePassenger_wait() to remove Passenger from suitatble list.
        *          decrease the size of num_pass, num_board, and num_wait relatively.
        * 
        *    + changeSeat(Passenger pass, Seat seat):
        *          change the passenger seat if valid, if not, don't change anything.
        * 
        *    + departurePrep():
        *          change the Passenger status and seats and make sure there are passengers
        *          at the exit row;
        *          There are 3 phases of function to make sure Standby Passenger is proccesed lastly
     *                  phase 1: add wait_list that has seat and status: Checked-in to board_list
     *                  phase 2: Assign seat to passenger that has no seat
     *                  phase 3: Assign seat to Standbypassenger
        * 
        *    + boardFlight():
        *          open the ability for passenger to board. Produce board_list and wait_list
        * 
        *    + void lastCallBoarding():
        *          process the waitlist. Add passenger to board_list and remove passenger from
        *          wait_list.
        *          There are 2 phases of function to make sure Standby Passenger is proccesed lastly
        *               phase1: boarding all Passenger that is not Standby
        *               phase2: boarding StandBy Passengers
        * 
        *    + void departAirport();
        *          change flight and passenger status to in-flight
        * 
        *    + void flightLanded();
        *          change flight and passenger status to landed
        * 
        *    + string shareFlightInfo(Passenger pass) const
        *          provide flight information for passenger on the flight
        *      
    */
}