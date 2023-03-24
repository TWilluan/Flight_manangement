using System;

/****************************************************
*                    ParentMonitor Class
*****************************************************/

/* Class Invariants
 * 
 * This parent monitor class is observer class that has inherited from Observer class
 * It has a name of attendent as an attribute
 * 
 * There are getType() and OnNext() abstract function
 * 
 * Assume that argument Passenger pass in ParentMonitor consutructor is children of
 *  this parents.
 */

/* Interface Invariants
     * 
     * getType():
        *  return type Flight_Attendent to support for categorize subsriber in WatchedFlight class
     * OnNext():
        * print the flightInfor if the there is a change flight status
*/
namespace HW_5
{
	public class ParentMonitor : Observer
    {
        private Passenger pass;
        private string contact;

        public ParentMonitor(Passenger pass, string contact, TextWriter text)
		{
			this.pass = pass;
			this.text = text;
            this.contact = contact;
            type = Monitor_Type.Parent;

        }

        public ParentMonitor(Passenger pass, string contact, string file)
        {
            this.pass = pass;
            text = new StreamWriter(file);
            this.contact = contact;
            this.contact = contact;
            type = Monitor_Type.Parent;

        }

        public override void OnNext(Flight flight)
		{
			if (flight.pass_in_list(this.pass, contact))
			{
				string res = "---Notify parents of " + pass.getName() + "\n";
				res += flight.shareFlightInfo() + "\n";
				text.WriteLine(res);
				text.Flush();
			}
            // Since boarding time last from BoardFlight() to DepartAirport()
            //  so I set a condition to check if flight status is in_flight but
            //  the children still not on the flight
            if (flight.get_Status() == Flight.Plane_Status.In_flight &&
                pass.getStatus() != Passenger.Pass_Status.In_flight &&
                pass.getContact() == this.contact)
                text.WriteLine("Urgent! Your children: " + pass.getName() + " didn't boarding as planned");
        }
    }
}

/* Implementation Invariants
    * Public Functions:
    * OnNext():
        * call the function in flight class to get flight info
        * use TextWriter to produce output to terminal
    * getType():
        * return type of parentMonitor 
*/