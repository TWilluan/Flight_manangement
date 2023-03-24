// Name: Tuan Vo
// CPSC 3200
// Assignment 5 - Freqpassenger

/* Class Invariants
     * 
     * This Frepassenger class is degined encapsulated and contractual. This class is derived class from
     * Passenger class. Hence, this class contains basic information of a passenger from Passenger class
     * and Flight_count the count the flight that has booked
     *			
     *			name;        // cannot be blanked
                cond;        // There are 4 passenger condition: (None, Disability, Age, Infant)
                seat;        // 
                group;       // cannot be blanked
                contact;     // cannot be blanked
                status;      // There are 7 status: (None,Invalid,Checked_in,Boarding,In_flight,Landed)
                type;        // There are 3 type Passenger (Regular, Frequent, Standby)
                flight_count // default value will be 0 for default constructor
                                                      1 for parameterized contrutor 
     * 
     * There are also getter and setter function to get the number of flight_count of Passenger
     */

/* Interface Invariants
     * 
     * changeSeat() - @param: Seat
         * This function is virtual function since children class has different condition to change seat
         * The other condition will be checked in Flight class when assign seat to Passenger
     *
     *
     * setCount() - @param: int
         * This function is used to set the flight count and subtract the flight_count after being upgrade
         * to the first class free
     */

namespace HW_5
{
    public class FreqPassenger : Passenger
    {
        private int flight_count; //number of flight count
        private bool upgraded = false;

        //default contructor
        public FreqPassenger()
            : base("", Condition.None, new Seat(), "", "", Pass_Status.None)
        {
            type = Type.Frequent;
            flight_count = 0;
        }

        public FreqPassenger(string name, Condition cond, Seat seat, string group,
                                                string contact, Pass_Status status, int count)
            : base(name, cond, seat, group, contact, status) //call base constructor
        {
            type = Type.Frequent;
            flight_count = count;
        }

        //Getter and Setter
        public override int getFlight_count() { return flight_count; }
        public override void setCount(int count)
        {
            if (count < 0) //check if the count is negative
                count = 0;
            this.flight_count = count;
        }

        public override bool getUpgrade() { return upgraded; }
        public override void setUpgrade() { upgraded = true; }

        //Derived boarding_pass function
        public override string boarding_pass()
        { //add more information to base boarding_pass()
            string res = base.boarding_pass();
            res += "Current flight count: " + getFlight_count() + "\n";
            return res;
        }
    }
}
/* Implementation Invariants
    * Public Functions:
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
        *  Those functions are implemented to provide the number of flight count
*/