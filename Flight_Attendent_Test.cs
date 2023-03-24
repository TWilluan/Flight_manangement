namespace HW_5;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void Attendent_getType_Test()
    {
        Observer parent = new FlightAttendentMonitor("Tuan", new StringWriter());
        Assert.AreEqual(parent.getType(), Observer.Monitor_Type.Flight_Attandent);
    }
}
