namespace HW_5;

[TestClass]
public class ParentMonitor_Test
{
    [TestMethod]
    public void ParentMonitor_GetType_Test()
    {
        Observer parent = new ParentMonitor(new Passenger("Tuan", Passenger.Condition.None,
                        new Seat(), "123ASD13", "pass1@gmail.com", Passenger.Pass_Status.Boarding), Console.Out);
        Assert.AreEqual(parent.getType(), Observer.Monitor_Type.Parent);
    }
}
