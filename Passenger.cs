// Name: Tuan Vo
// CPSC 3200
// Assignment 3 - Passenger

/****************************************************
*                   Seat Class
*****************************************************/

namespace HW_5
{

    public class Seat
    {
        public enum Seat_Row
        { //include character represent seat in one row
          // start with index 0 (A) to 7 (H)
            A, B, C,
            D, E,
            F, G, H,
            None
        }

        private Seat_Row row;
        private int num;

        public Seat() { row = Seat_Row.None; num = 0; }
        public Seat(Seat_Row row, int num)
        {
            if (row != Seat_Row.A && row != Seat_Row.B && row != Seat_Row.C &&
                row != Seat_Row.D && row != Seat_Row.E && row != Seat_Row.F &&
                row != Seat_Row.G && row != Seat_Row.H)
                throw new Exception("Please enter valid seat_row (0-7)");
            this.row = row;

            if (num < 0)
                throw new Exception("number of row has to start by 1");
            this.num = num;
        }

        // Getters function for row and num attribute
        public Seat_Row get_row() { return row; }
        public int get_num() { return num; }
    };

    /****************************************************
    *                   Passenger Class
    *****************************************************/

    public class Passenger
    {

        /* Class Invariants
             * 
             * This Passenger class is degined encapsulated and contractual. This class contains
             * basic information of a passenger:
             *			
             *			name;    // cannot be blanked
                        cond;    // There are 4 passenger condition: (None, Disability, Age, Infant)
                        seat;    // 
                        group;   // cannot be blanked
                        contact; // cannot be blanked
                        status;  // There are 7 status: (None,Invalid,Checked_in,Boarding,In_flight,Landed)
                        type;    // There are 3 type Passenger (Regular, Frequent, Standby)
             * 
             * There are 3 constructors(default, contructor for Passenger without Seat, and Passenger with Seat)
             * There are also getters and setters function which help Passenger get and set their information.
             */

        /* Interface Invariants
             * 
             * changeStatus() - @param: Pass_Status
                 * change the passenger's status to different status if it is valid
                 *		Valid output: status will be changed from None to Checked-in
                 *					  status will be changed from Checked-in to Boarding
                 *					  status will be changed from Boarding to In-flight
                 *					  status will be changed from In-flight to Landed
                 *		Invalid output: Status will change to Invalid if undone Checked-in and Boarding
                 *	The function will not do anything if the input is not in the enum Pass_status
                 *	
             * changeSeat() - @param: Seat
                 * this function is virtual function since children class has different condition to change seat
                 * the other condition will be checked in Flight class when assign seat to Passenger
             */


        public enum Pass_Status
        {
            None,
            Invalid,
            Checked_in,
            Boarding,
            In_flight,
            Landed
        }

        public enum Condition
        {
            None,       // normal
            Disability,
            Infant,     // have infant
            Age         // old passenger
        }

        public enum Type
        {
            Regular,
            Frequent,
            Standby
        }

        private readonly string name;
        private Condition cond;
        protected Seat seat;   //need to set Seat in child class
        private readonly string group;
        private string contact;
        protected Pass_Status status; //need to set Status in child class
        protected Type type;   //need to set Type in child class

        public Passenger() //default parameter for Passenger
        {
            name = "";
            cond = Condition.None;
            seat = new Seat();
            group = "";
            contact = "";
            status = Pass_Status.None;
            type = Type.Regular;
        }


        // Paremterized Passenger constructor with no Seat
        public Passenger(string name, Condition cond, string group, string contact, Pass_Status status)
        {
            if (name == "")         //name cannot be blanked
                throw new Exception("Name cannot be blanked");
            this.name = name;

            if (cond != Condition.None && cond != Condition.Disability && cond != Condition.Infant &&
                                                        cond != Condition.Age)
                throw new Exception("Please enter an valid exception");
            this.cond = cond;

            this.seat = new Seat();//seat is assigned later

            if (group == "")        //confirmation number cannot be blanked
                throw new Exception("Confirmation number cannot be blanked");
            this.group = group;

            if (contact == "")      //contact cannot be blanked
                throw new Exception("Contact information cannot be blanked");
            this.contact = contact;

            if (status != Pass_Status.Invalid && status != Pass_Status.Checked_in &&
                status != Pass_Status.Boarding && status != Pass_Status.In_flight && status != Pass_Status.Landed)
                throw new Exception("Please enter valid passeger status");
            this.status = status;

            this.type = Type.Regular;
        }

        // Paramterized contructor
        public Passenger(string name, Condition cond, Seat seat, string group,
                                                string contact, Pass_Status status)
        {
            if (name == "")         //name cannot be blanked
                throw new Exception("Name cannot be blanked");
            this.name = name;

            if (cond != Condition.None && cond != Condition.Disability && cond != Condition.Infant &&
                                                        cond != Condition.Age)
                throw new Exception("Please enter an valid exception");
            this.cond = cond;

            this.seat = seat;       // error-handling is occuring in Seat contructor

            if (group == "")        //confirmation number cannot be blanked
                throw new Exception("Confirmation number cannot be blanked");
            this.group = group;

            if (contact == "")      //contact cannot be blanked
                throw new Exception("Contact information cannot be blanked");
            this.contact = contact;

            //Check if valid status in 2 first line
            //third line: passenger's status is not allowed to be created as in-flight or landed
            if (status != Pass_Status.None && status != Pass_Status.Invalid && status != Pass_Status.Checked_in &&
                status != Pass_Status.Boarding && status != Pass_Status.In_flight && status != Pass_Status.Landed)
                throw new Exception("Please enter valid Passenger status");
            this.status = status;

            this.type = Type.Regular;
        }

        // Getters Functions
        public Seat getSeat() { return this.seat; }
        public string getGroup() { return this.group; }
        public string getName() { return this.name; }
        public string getContact() { return this.contact; }
        public Condition getCondition() { return this.cond; }
        public Pass_Status getStatus() { return this.status; }
        public Type getType() { return this.type; }

        //Getters and Setters function supporting for children class
        public virtual int  getFlight_count() { return 0; }
        public virtual void setCount(int count) { }
        public virtual void changeState(State other) { }
        public virtual State getState() { return State.None; }
        public virtual bool getUpgrade() { return false; }
        public virtual void setUpgrade() { }

        // Print seat in a certain format
        public string getSeat_toString()
        {
            if (seat.get_row() == Seat.Seat_Row.None && seat.get_num() == 0)
                return "N/A";
            return "" + seat.get_num() + seat.get_row().ToString();
        }

        //Change status of passenger
        //pre-condition:  input status must be valid
        //pre-condition:  passenger's status is not INVALID
        //pre-condition:  passenger's status cannot be checked-in or boarded
        //post-condition: set passenger's status to Checked-in (C) or Boarded (B) if valid

        //                set passenger's status to Invalid (I) if invalid
        public void change_status(Pass_Status status)
        {
            // Invalid state
            if (status == Pass_Status.None && (this.status == Pass_Status.Checked_in
                                                    || this.status == Pass_Status.Boarding))
                this.status = Pass_Status.Invalid; // passenger's status cannot be undone

            // Change from None to Checked-in
            if (this.status == Pass_Status.None && status == Pass_Status.Checked_in)
                this.status = status;

            // Change from checked-in to Boarded
            if (this.status == Pass_Status.Checked_in && status == Pass_Status.Boarding)
                this.status = status;

            // Change from Boarded to In-Flight
            if (this.status == Pass_Status.Boarding && status == Pass_Status.In_flight)
                this.status = status;

            // Change from In-Flight to Arrived
            if (this.status == Pass_Status.In_flight && status == Pass_Status.Landed)
                this.status = status;
        }

        //Change Passenger's seat
        public virtual void change_seat(Seat other)
        {
            this.seat = other;
        }

        // Providing boarding pass for passenger
        public virtual string boarding_pass()
        {
            string res = "---Boarding Pass---\n";
            res += "Name: " + getName() + "\t\tSeat: " + getSeat_toString() + "\n";
            res += "Status: " + getStatus().ToString() + "\t\tType: " + getType().ToString() + "\n";
            res += "Condition: " + getCondition().ToString() + "\n";
            return res;
        }
    };

    /* Implementation Invariants
        * Public Functions:
        * changeStatus(): @param: Pass_Status
            *  This function is implemented to change the passenger's status
            *  
            *  Passenger have to change status in certain process:
                    * None      -> Checked-in
                    * Checkedin -> Boarding
                    * Boarding  -> In-FLight
                    * In_flight -> Landed
            * 
            *  In pass mode, the function will change passenger status. Change 
            *  passenger's status to Invalid if the fucntion call is invalid. Also, it will
            *  not do anything if passenger's status is Invalid. 
        *
        *
        * changeSeat(): @param: Seat
            *  This function is implemented to change the seat of passenger
            *  Other condition such as economy passenger cannot change seat to first-class
            *  will be managed in Flight class  
            *  This function is virtual since children class has different condition
            *  to change seat
        * 
        * 
        *  Getters and Setters function():
            *  Those function is implemented to provide information to compare and 
            *  set passenger's information  
            *  In pass mode, those function will return needed information.
        */
}