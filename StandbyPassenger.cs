// Name: Tuan Vo
// CPSC 3200
// Assignment 3 - StandByPassenger

/* Class Invariants
     * 
     * This StandbyPassenger class is degined encapsulated and contractual. This class is derived class
     * of Passenger class. Hence, it contains basic information about Passenger and it doesnt allow
     * Passenger to have seat until departurePrep
     *			
     *			name;    // cannot be blanked
                cond;    // There are 4 passenger condition: (None, Disability, Age, Infant)
                seat;    // always be nullptr at default
                group;   // cannot be blanked
                contact; // cannot be blanked
                status;  // There are 7 status: (None,Invalid,Checked_in,Boarding,In_flight,Landed)
                type;    // There are 3 type Passenger (Regular, Frequent, Standby)
                state;
     *
     * There are also getters and setters function which help StandbyPassenger get and set their information.
     */

/* Interface Invariants
     * 
     * changeState() - @param: State
         * This function is used to change the state of StandbyPassenger to unlock to ability
         * to be assigned Seat for StandbyPassenger
         *      Valid call: State.None   -> State->Depart
         *                  State.Depart -> State->Boarding
         *	
     * changeSeat() - @param: Seat
         * StandByPassenger has to have Depart State to changeSeat.
         * the other condition will be checked in Flight class when assign seat to StandbyPassenger
     */

namespace HW_5
{
    //StandbyPassenger's State
    public enum State { Depart, Boarding, None }

    public class StandbyPassenger : Passenger
    {
        private State state;

        //default constructor
        public StandbyPassenger()
            : base("", Condition.None, new Seat(), "", "", Pass_Status.None)
        {
            state = State.None;
            this.type = Type.Standby;
        }

        //paramterized contructor
        public StandbyPassenger(string name, Condition cond, string group, string contact, Pass_Status status)
            : base(name, cond, group, contact, status)
        {
            state = State.None;
            this.type = Type.Standby;
        }

        //getters and setters
        public override State getState() { return state; }

        //change state of StandbyPassenger
        //pre-condition: input state must be valid
        //post-condition: StandbyPassenger's state will change to other if it
        //                  satisfies the condition
        //                  if it not satisfy condition, this function will not operate
        public override void changeState(State other)
        {
            if (this.state == State.None && other == State.Depart)
                this.state = other;
            if (this.state == State.Depart && other == State.Boarding)
                this.state = other;
            if (this.state == State.Boarding && other == State.None)
                this.state = other;
        }

        //change seat of StandbyPassenger
        //pre-condition: StandbyPassenger has to has Depart State to change Seat
        //post-condition: if valid, it will change, if not, it doens't do anything.
        public override void change_seat(Seat other)
        {
            if (this.state == State.Depart)
                base.change_seat(other);
        }

        public override string boarding_pass()
        { // add state infromation to boarding pass
            string res = base.boarding_pass();
            res += "State: " + getState() + "\n";
            return res;
        }
    }
}

/* Implementation Invariants
    * Public Functions:
    * changeState(): @param: State
        *  This function is implemented to change the passenger's state
        *  
        *  Passenger have to change status in certain process:
                * None      -> Depart
                * Depart -> Boarding
                * Boarding  -> None
        * 
        *  In pass mode, the function will change passenger state. 
        *  Also, it will not do anything if input status is invalid. 
    *
    *
    * changeSeat(): @param: Seat
        *  This function is implemented to change the seat of StandByPassenger
        *  This function is override since Standbypassenger has to have Depart state
        *  to change seat
    * 
    * 
    *  Getters and Setters function():
        *  Those function is implemented to provide set StandByPassenger's state  
        *  In pass mode, those function will return needed information.
    *
    *
 */