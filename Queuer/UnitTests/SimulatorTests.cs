using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueueSimulator;
using Queuer;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class SimulatorTests
    {
        [TestMethod]
        public void tworzenieSymulatora()
        {
            List<MachineDescription> md = new List<MachineDescription>();
            md.Add(new MachineDescription());
            Simulator s = new Simulator(md);
            
            Assert.IsNotNull(s);

            Assert.AreEqual(md.Count, s.getNumberOfNodes(), "liczba nodow powinna byc rowna z iloscia maszyn");

        }

        [TestMethod]
        public void testSingletona_KolejkiKomunikatow()
        {
            EventQueue eq1 = EventQueue.Instance;
            EventQueue eq2 = EventQueue.Instance;
            Assert.AreEqual(eq1, eq2, "Singleton powinnen byc jeden");
        }

        [TestMethod]
        public void testSingletona_dodawanieZdejmowanie()
        {
            EventQueue eq1 = EventQueue.Instance;
            EventQueue eq2 = EventQueue.Instance;

            Event e1 = new Event(1, 1000);
            Event e2 = new Event(2, 666);

            eq1.addEvent(e1.getTime(),e1);
            eq2.addEvent(e2.getTime(), e2);

            Event x1, x2;

            x2 = eq1.getEvent();
            x1 = eq2.getEvent();

            Assert.AreEqual(e1.getID(), x1.getID(), "pierwszy element sie nie zgadza - ten co powinien byc mniejszy");
            Assert.AreEqual(e2.getID(), x2.getID(), "drugi element sie nie zgadza - ten co powinien byc wiekszy");

            Assert.AreSame(e1, x1,"x1,e1 same?");
            Assert.AreSame(e2, x2, "x2,e2 same?");

            Assert.IsNotNull(x2, "" + x2.getTime());
            Assert.IsNotNull(x1, "" + x1.getTime());
        }
    }
}
