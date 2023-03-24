namespace HW_5
{
    [TestClass]
    public class FrePassenger_Test
    {
        [TestMethod]
        public void Test_setCount_NegativeInput()
        {
            Passenger pass_1 = new FreqPassenger("Tuan Vo", Passenger.Condition.Disability, new Seat(Seat.Seat_Row.B, 8),
                                               "asd213asd", "asd", Passenger.Pass_Status.Boarding, 3);
            FreqPassenger pass_2 = new FreqPassenger("Tuan Vo", Passenger.Condition.Disability, new Seat(Seat.Seat_Row.B, 8),
                                               "asd213asd", "asd", Passenger.Pass_Status.Boarding, 1);

            pass_1.setCount(-3);
            pass_2.setCount(-3);

            Assert.AreEqual(pass_1.getFlight_count(), 0);
            Assert.AreEqual(pass_2.getFlight_count(), 0);
        }

        [TestMethod]
        public void Test_setCount_ValidInput()
        {
            FreqPassenger pass = new FreqPassenger("Tuan Vo", Passenger.Condition.Disability, new Seat(Seat.Seat_Row.B, 8),
                                               "asd213asd", "asd", Passenger.Pass_Status.Boarding, 5);
            pass.setCount(pass.getFlight_count() - 3);
            Assert.AreEqual(pass.getFlight_count(), 2);
        }

    }
}
