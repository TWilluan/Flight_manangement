namespace HW_5
{
    [TestClass]
    public class StandbyPassenger_Test
    {
        [TestMethod]
        public void test_changeState_N_to_D()
        {
            Passenger pass = new StandbyPassenger("Tuan Vo", Passenger.Condition.Disability, "asd213asd", "asd", Passenger.Pass_Status.Boarding);
            pass.changeState(State.Depart);
            Assert.AreEqual(pass.getState(), State.Depart);
        }

        [TestMethod]
        public void test_changeState_D_to_B()
        {
            Passenger pass = new StandbyPassenger("Tuan Vo", Passenger.Condition.Disability, "asd213asd", "asd", Passenger.Pass_Status.Boarding);
            pass.changeState(State.Depart);
            pass.changeState(State.Boarding);
            Assert.AreEqual(pass.getState(), State.Boarding);
        }

        [TestMethod]
        public void test_changeState_B_to_N()
        {
            Passenger pass = new StandbyPassenger("Tuan Vo", Passenger.Condition.Disability, "asd213asd", "asd", Passenger.Pass_Status.Boarding);
            pass.changeState(State.Depart);
            pass.changeState(State.Boarding);
            pass.changeState(State.None);
            Assert.AreEqual(pass.getState(), State.None);
        }
    }
}
