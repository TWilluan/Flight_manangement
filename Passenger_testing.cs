using Microsoft.VisualStudio.TestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HW_5
{
    [TestClass]
    public class Passenger_Test
    {
        [TestMethod]
        // Test if status will change to Invalid if the passenger's status is checked-in or boarded
        public void Test_changeStatus_Passenger_Invalid()
        {
            //initialize
            Passenger pass_boarded = new Passenger("Tuan Vo", Passenger.Condition.Disability, new Seat(Seat.Seat_Row.B, 8),
                                               "asd213asd", "asd", Passenger.Pass_Status.Boarding);
            Passenger pass_checked = new Passenger("Tuan Vo", Passenger.Condition.Age, new Seat(Seat.Seat_Row.C, 2),
                                                "asd213asd", "asd", Passenger.Pass_Status.Checked_in);

            //Call changeStatus()
            pass_boarded.change_status(Passenger.Pass_Status.None);
            pass_checked.change_status(Passenger.Pass_Status.None);

            Passenger.Pass_Status pass_boarded_expected = pass_boarded.getStatus();
            Passenger.Pass_Status pass_checked_expected = pass_checked.getStatus();

            //Test Results
            Assert.AreEqual(pass_boarded_expected, Passenger.Pass_Status.Invalid);
            Assert.AreEqual(pass_checked_expected, Passenger.Pass_Status.Invalid);
        }

        [TestMethod]
        //Test if status will change from N to C
        public void Test_changeStatus_Passenger_N_to_C()
        {
            //Call changeStatus() to chang status to C
            Passenger pass = new Passenger("Tuan Vo", Passenger.Condition.Disability, new Seat(Seat.Seat_Row.A, 25),
                                    "asd213asd", "Tuan@gmail.com", Passenger.Pass_Status.None);
            pass.change_status(Passenger.Pass_Status.Checked_in);
            Assert.AreEqual(pass.getStatus(), Passenger.Pass_Status.Checked_in);
        }

        [TestMethod]
        //Test if status will change from C to B
        public void Test_changeStatus_Passenger_C_To_B()
        {
            Passenger pass = new Passenger("Tuan Vo", Passenger.Condition.Disability, new Seat(Seat.Seat_Row.A, 25),
                                    "asd213asd", "Tuan@gmail.com", Passenger.Pass_Status.Checked_in);
            pass.change_status(Passenger.Pass_Status.Boarding);
            Assert.AreEqual(pass.getStatus(), Passenger.Pass_Status.Boarding);
        }

        [TestMethod]
        //Test if status will change from B to F
        public void Test_changeStatus_Passenger_B_to_F()
        {
            Passenger pass = new Passenger("Tuan Vo", Passenger.Condition.Disability, new Seat(Seat.Seat_Row.A, 25),
                                    "asd213asd", "Tuan@gmail.com", Passenger.Pass_Status.Boarding);
            //Call changeStatus() to chang status to C
            pass.change_status(Passenger.Pass_Status.In_flight);
            Assert.AreEqual(pass.getStatus(), Passenger.Pass_Status.In_flight);
        }

        [TestMethod]
        //Test if status will change from F to L
        public void Test_changeStatus_Passenger_F_to_L()
        {
            Passenger pass = new Passenger("Tuan Vo", Passenger.Condition.Disability, new Seat(Seat.Seat_Row.A, 25),
                                    "asd213asd", "Tuan@gmail.com", Passenger.Pass_Status.In_flight);
            //Call changeStatus() to chang status to C
            pass.change_status(Passenger.Pass_Status.Landed);
            Assert.AreEqual(pass.getStatus(), Passenger.Pass_Status.Landed);
        }
    }
}