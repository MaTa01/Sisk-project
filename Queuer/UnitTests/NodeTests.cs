using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queuer;
using QueueSimulator;

namespace UnitTests
{
    [TestClass]
    public class NodeTests
    {
        int SizeOne;
        
        MachineDescription mDesc;
       
        [TestMethod]
        public void czyTworzySieNode()
        {

            SizeOne = 1;
            mDesc = new MachineDescription();
            Node n = new Node(mDesc);
            Assert.IsNotNull(n);

        }
        [TestMethod]
        public void nodeZjednymMiejscem()
        {
            SizeOne = 1;
            mDesc = new MachineDescription();
            mDesc.Id = 1;
            mDesc.QueueSize = SizeOne;
            mDesc.SlotsNumber = 1;
            Node n = new Node(mDesc);
            Assert.IsTrue(n.isBufferEmpty());
            Assert.IsFalse(n.isBufferFull());
            n.addToBuffer(new Task(0));
            Assert.IsFalse(n.isBufferEmpty());
            Assert.IsTrue(n.isBufferFull());

            Task t = n.getFromBuffer();
            Assert.IsTrue(n.isBufferEmpty());
            Assert.IsFalse(n.isBufferFull());
        }


        [TestMethod]
        public void czyMAszynyTworzaSieWolne()
        {
            mDesc = new MachineDescription();
            mDesc.Id = 1;
            mDesc.QueueSize = 0; //infinity queue
            mDesc.SlotsNumber = 1;
            Node n;
            for (int i = 1; i < 3; i++)
            {
                mDesc.SlotsNumber = 1;
                n = new Node(mDesc);
                Assert.IsTrue(n.isAnyFreeMachine(),"cos nie dziala dla i="+i);
                Assert.IsFalse(n.isAnyInMachine(), "cos nie dziala dla i=" + i);
            }
          
            

            
        }
        [TestMethod]
        public void czyDodawanieMaszynyDziala()
        {
            mDesc = new MachineDescription();
            mDesc.Id = 1;
            mDesc.QueueSize = 0; //infinity queue
            mDesc.SlotsNumber = 10;

            Node n = new Node(mDesc);
            Task t = new Task(0); // zadanie
            
            Assert.IsFalse(n.isAnyInMachine(), "w maszynach mialo nic nie byc");
            for (int i = 0; i < 10; i++)
            {
                
                n.addToMachine(t);
                if (i == 9)
                {

                    Assert.IsFalse(n.isAnyFreeMachine(), "cos nie dziala dla i=" + i);
                    Assert.IsTrue(n.isAnyInMachine(), "cos nie dziala dla i=" + i);
                }
                else
                {
                    Assert.IsTrue(n.isAnyFreeMachine(), "a)cos nie dziala dla i=" + i);
                    Assert.IsTrue(n.isAnyInMachine(), "b)cos nie dziala dla i=" + i);
                }

            }
            
        }

        [TestMethod]
        public void pobieranieZMaszyn()
        {
            mDesc = new MachineDescription();
            mDesc.Id = 1;
            mDesc.QueueSize = 0; //infinity queue
            mDesc.SlotsNumber = 3;

            Node n = new Node(mDesc);
            Task t;
            Task x;

            for(int i=0; i<3;i++){ // dodajemy taski
                t = new Task((i+1)*10);
                Assert.IsTrue(n.isAnyFreeMachine(), "dodawanie taskow do maszyn");
                n.addToMachine(t);
                Assert.IsTrue(n.isAnyInMachine(), "b)cos nie dziala dla i=" + i);
            }

            Assert.IsFalse(n.isAnyFreeMachine(), "koniec dodawania - przed pobieraniem");

            /**/for (int i = 1; i <= 3; i++)
            {
                
                x = n.getReadyTaskFromMachine(i * 10+60);
                Assert.IsNotNull(x,"nic nie pobralo");
                Assert.AreEqual((float)(x.getReadyTime()), i * 10.0 + 60.0,0.0,"pobrano zly task dla i ="+i);
                
            }
            Assert.IsFalse(n.isAnyInMachine(), "maszyny powinny byc puste");
        }

        [TestMethod]
        public void test()
        {
            Assert.IsNull(null);
        }
    }
}
