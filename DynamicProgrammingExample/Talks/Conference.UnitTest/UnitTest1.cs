using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Conference.Interfaces;
using Conference.Engine;
using Conference.Concrete;
using System.Collections.Generic;
using System.IO;
using Conference.TalkCreator;

namespace Conference.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void EngineTest()
        {
            IItem item = new Talk("Talk1", 1);
            IItem item2 = new Talk("Talk2", 3);
            IItem item3 = new Talk("Talk3", 4);
            IEngine<IItem> engine = new GenericEngine<IItem>();
            var actual = engine.GetTheBestSuitableItem(5, new List<IItem>() { item, item2, item3 });

            //CollectionAssert.AreEqual(new List<IItem>() {  item, item3}, actual);

            Assert.IsTrue(actual.Count == 2 ? true : false, "COunt returned is true");
        }

        [TestMethod]
        public void TalksRetievalFromStringList()
        {

            string Talks = "Writing Fast Tests Against Enterprise Rails 60min\n Overdoing it in Python 45min\n Lua for the Masses 30min\n Ruby Errors from Mismatched Gem Versions 45min\nCommon Ruby Errors 45min\n Rails for Python Developers lightning \n Communicating Over Distance 60min \nAccounting - Driven Development 45min \nWoah 30min \nSit Down and Write 30min \nPair Programming vs Noise 45min \nRails Magic 60min \nRuby on Rails: Why We Should Move On 60min \nClojure Ate Scala(on my project) 45min \nProgramming in the Boondocks of Seattle 30min \nRuby vs.Clojure for Back - End Development 30min \nRuby on Rails Legacy App Maintenance 60min \nA World Without HackerNews 30min \nUser Interface CSS in Rails Apps 30min \n";
            IList<string> inputArr = Talks.Split('\n');

            IConferenceCreator conferenceCreator = new ConferenceCreator();
            var talksScheduled = conferenceCreator.CreateTalksFromInput(inputArr);
           
            Assert.IsTrue(talksScheduled.Count == 19 ? true: false , "Talks are retieved from file" );
            
        }


    }
}
