namespace HW_5
{
    [TestClass]
    public class WatchedFlight_Test
    {
        [TestMethod]
        public void WatchedFlight_Subscribe_Test()
        {
            Passenger[] pass = new Passenger[2];
            pass[0] = new Passenger("Le", Passenger.Condition.None, new Seat(), "890WER123", "pass7@gmail.com", Passenger.Pass_Status.None);
            pass[1] = new FreqPassenger("Thuy", Passenger.Condition.Age, new Seat(), "131FDS138", "pass5@gmail.com", Passenger.Pass_Status.Checked_in, 10);
            WatchedFlight test = new WatchedFlight(pass, "Flight", "Seattle", "HCM");

            ParentMonitor parent_1 = new ParentMonitor(pass[0]);
            ParentMonitor parent_2 = new ParentMonitor(pass[1]);
            FlightAttendentMonitor attendent = new FlightAttendentMonitor("Will");


            test.Subscribe(parent_1);
            test.Subscribe(parent_2);
            test.Subscribe(attendent);

            Assert.AreEqual(test.getParents().Count, 2);
            Assert.AreEqual(test.getAttendents().Count, 1);
        }
    }

    //since other function in WatchedFlight is belongs to its Base Class and are also
    // tested in Flight_unit_test unit testing file
}
