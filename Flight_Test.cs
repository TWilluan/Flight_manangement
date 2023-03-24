namespace HW_5;

[TestClass]
public class UnitTest1
{
    private Flight flight;

    [TestInitialize]
    public void Initialize()
    {
        Passenger pass1 = new Passenger("Tuan", Passenger.Condition.None, new Seat(Seat.Seat_Row.A, 1), "123ASD13", "pass1@gmail.com", Passenger.Pass_Status.Boarding);
        Passenger pass2 = new StandbyPassenger("Vo", Passenger.Condition.None, "456DSA123", "pass2@gmail.com", Passenger.Pass_Status.Checked_in);
        Passenger pass3 = new FreqPassenger("Anh", Passenger.Condition.Disability, new Seat(), "786SAI126", "pass3@gmail.com", Passenger.Pass_Status.Checked_in, 10);
        StandbyPassenger pass4 = new StandbyPassenger("Nguyen", Passenger.Condition.Infant, "131FDS125", "pass4@gmail.com", Passenger.Pass_Status.Boarding);

        int num_pass = 4;
        Passenger[] pass_list = new Passenger[num_pass];
        pass_list[0] = pass1;
        pass_list[1] = pass2;
        pass_list[2] = pass3;
        pass_list[3] = pass4;

        flight = new Flight(pass_list, "AIR_1124", "Seattle", "Ho Chi Minh");
    }

    [TestMethod]
    public void Test_addPassenger_Flight()
    {
        flight.addPassenger(new Passenger("Chi", Passenger.Condition.Age, new Seat(Seat.Seat_Row.C, 34), "123IUA468)", "pass10@gmail.com", Passenger.Pass_Status.Boarding));
        Assert.AreEqual(flight.getNum_pass(), 5);
    }

    [TestMethod]
    public void Test_removePassenger_Flight()
    {
        flight.removePassenger(new Passenger("Chi", Passenger.Condition.Age, new Seat(Seat.Seat_Row.C, 34), "123IUA468)", "pass10@gmail.com", Passenger.Pass_Status.Boarding));
        Assert.AreEqual(flight.getNum_pass(), 4);
    }

    [TestMethod]
    public void Test_Flight_functions_Flight()
    {
        flight.boardFlight();
        Assert.AreEqual(flight.getNum_board(), 1);
        Assert.AreEqual(flight.getNum_wait(), 3);
        Assert.AreEqual(flight.get_Status(), Flight.Plane_Status.Boarding);

        flight.departurePrep();
        Assert.AreEqual(flight.getNum_board(), 1);
        Assert.AreEqual(flight.getNum_wait(), 3);

        flight.lastCallBoarding();
        Assert.AreEqual(flight.getNum_board(), 4);
        Assert.AreEqual(flight.getNum_wait(), 0);
    }

    public void Test_departAirport_Flight()
    {
        flight.departAirport();
        Assert.AreEqual(flight.get_Status(), Flight.Plane_Status.In_flight);
    }

    public void Test_Landing_Flight()
    {
        flight.departAirport();
        Assert.AreEqual(flight.get_Status(), Flight.Plane_Status.Landed);
    }
}
