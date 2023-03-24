using System;

namespace HW_5
{

    /****************************************************
    *                    Flight Attendent Class
    *****************************************************/

    /* Class Invariants
     * 
     * This Flight attendent class is observer class that has inherited from Observer class
     * It has a name of attendent as an attribute
     * 
     * There are getType() and OnNext() abstract function
     * 
     * Assume that flight attendent is not Passenger, so I only have a string name as 
     *  constructors' arguments.
     */

    /* Interface Invariants
         * 
         * getType():
            *  return type Flight_Attendent to support for categorize subsriber in WatchedFlight class
         * OnNext():
            * print out the list of passenger and their condition, also print frequent passenger 
            *                                                                      who get free upgraded
    */

    public class FlightAttendentMonitor : Observer
    {
        private string name;

        public FlightAttendentMonitor(string name, TextWriter text)
        {
            this.name = name;
            this.text = text;
            type = Monitor_Type.Flight_Attandent;
        }

        public FlightAttendentMonitor(string name, string file)
        {
            this.name = name;
            text = new StreamWriter(file);
            type = Monitor_Type.Parent;

        }

        public override void OnNext(Flight flight)
        {
            string res = "List of Specical-Condition Passenger for attendant " + name + ":\n";
            res += flight.print_special_pass();
            text.WriteLine(res);
            text.Flush();
        }
    }
}

/* Implementation Invariants
    * Public Functions:
    * OnNext():
        * call the function in flight class to get the list a passenger need to take care
        * use TextWriter to produce output to terminal
    * getType():
        * return type of flight_attendent 

    */