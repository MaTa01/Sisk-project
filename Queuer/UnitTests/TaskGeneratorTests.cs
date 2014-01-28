using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueueSimulator;
using Queuer;

namespace UnitTests
{
    [TestClass]
    public class TaskGeneratorTests
    {
        [TestMethod]
        public void newTaskGeneartor()
        {
            TaskGenerator tg = new TaskGenerator(60);

            Assert.IsNotNull(tg);
        }
        [TestMethod]
        public void generujTaskiCoTanSamOdstepCzasu()
        {
            int constTime = 10;
            TaskGenerator tg = new TaskGenerator(constTime);
            Task t;
            t = tg.getTask();


            for (int i = 0; i < 10; i++)
            {
                t = tg.getTask();
                Assert.AreEqual((i+1 ) * 10, t.getCreationTime(), "Wygenerowalo task w zlym czasie"+i);
            }
        }
    }
}
