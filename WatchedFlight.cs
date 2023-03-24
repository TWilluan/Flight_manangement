using System;



namespace HW_5
{

    /****************************************************
    *                    Observer Class
    *****************************************************/

    /* Class Invariants
     * 
     * This Observer abtract class is degined encapsulated and contractual. This class contains
     * basic information of an observer:
     *			
     *      subscriber = person who subsribe
     *      text       = used to produce the output
     *      
     *      
     * Since it is an abstract class, I will add Interface and Implementation invariants in its child class
     * 
     * Assume both ParentMonitor and FlightAttendents are not Passengers
     *  ParentMonitor: parent who want to get notify about thier children's flights
     *  Flight Attedent: who want to get list of passenger who needs special care
     */
    public abstract class Observer : IObserver<Flight>
    {

        public enum Monitor_Type { Parent, Flight_Attandent }

        protected IDisposable   subscriber;
        protected TextWriter    text;
        protected Monitor_Type  type;

        public void Subscribe(WatchedFlight provider) { subscriber = provider.Subscribe(this);  }

        public void OnCompleted()        { }
        public void OnError(Exception e) { }
        public abstract void OnNext(Flight flight);

        public Monitor_Type getType() { return type;  }
    }

    /****************************************************
    *                    Observable Class
    *****************************************************/

    /* Class Invariants
     * 
     * This is WatchedFlight class which is a Derived class of Flight class. It allows for parents and 
     *  flight attendent to get notified when there is a certain changes related to a flight. It contains
     *  two list of observers named:		
        *	parents: parent of passenger in the flight - change flight status
        *	attendents: flight attendents on that flight - get passengers' condition
     */

    /* Interface Invariants
         * 
         * Subscribe() - @param: Observer observer
             * use to add observer to correct list
         * 
         * boardFlight()
             * change the flight status to boarding and process pass_list
             * notify parent about the change of the status
         * departAirport()
             * change the flight status to in-flight
             * notify parent about the change of the status
         * flightLanded()
             * change the flight status to landed
             * notify parent about the change of the status
         * departurePrep()
             * this function will process wait-list, assign seat, and add certain passenger to board_list
             * notify flight attendents about the passenger's condition
         * lastCallBoarding()
             * this function will add passenger from waitlist ot board_list
             * notify flight_attendents again about the passenger's condition
    */

    public class WatchedFlight : Flight, IObservable<Flight>
    {
        private List<Observer> parents;
        private List<Observer> attendants;

        public WatchedFlight(Passenger[] others, string flight_no, string origin, string destination)
            : base (others, flight_no, origin, destination)
        {
            parents = new List<Observer>();
            attendants = new List<Observer>();
        }

        public IDisposable Subscribe(IObserver<Flight> observer)
        {
            Observer temp = (Observer) observer;
            if (temp.getType() == Observer.Monitor_Type.Parent)
            {
                if (!parents.Contains(temp))
                {
                    parents.Add(temp);
                    parents[parents.Count - 1].OnNext(this);
                }
            }
            else
            {
                if (!attendants.Contains(temp))
                {
                    attendants.Add(temp);
                    attendants[attendants.Count - 1].OnNext(this);
                }
            }
            return new NoOpUnsubscriber();
        }

        public override void boardFlight()
        {
            base.boardFlight();
            foreach (Observer parent in parents)
                parent.OnNext(this);
        }

        public override void departAirport()
        {
            base.departAirport();
            foreach (Observer parent in parents)
                parent.OnNext(this);
        }

        public override void flightLanded()
        {
            base.flightLanded();
            foreach (Observer parent in parents)
                parent.OnNext(this);
        }

        public override void departurePrep()
        {
            base.departurePrep();
            foreach (Observer attendant in attendants)
                attendant.OnNext(this);
        }

        public override void lastCallBoarding()
        {
            base.lastCallBoarding();
            foreach (Observer attendant in attendants)
                attendant.OnNext(this);
        }

        /****************************************************
        *          Function to Support Unit Testing
        *****************************************************/
        public List<Observer> getParents()    { return parents;    }
        public List<Observer> getAttendents() { return attendants; }
    }

    public class NoOpUnsubscriber : IDisposable { public void Dispose() { } }
}

/* Implementation Invariants
    * Public Functions:
         * boardFlight()
             * call the function in Flight class and notify the ParentMonitor
         * departAirport()
             * call the function in Flight class and notify the ParentMonitor
         * flightLanded()
             * call the function in Flight class and notify the ParentMonitor
         * departurePrep()
             * call the function in Flight class and notify the FlightAttendentMonitor
         * lastCallBoarding()
             * call the function in Flight class and notify the FlightAttendentMonitor
    */

