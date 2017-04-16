using Conference.Common;
using Conference.Interfaces;
using Conference.TalkCreator;
using Conference.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IList<string> inputArr = new List<string>();
                Console.WriteLine(@"Please enter full path file location containing talks. \n e.g. C:\Personal\Talks.txt and press enter");
                string filePath = Console.ReadLine();
                ReadFileContents(filePath, ref inputArr);

                IConferenceCreator conferenceCreator = new ConferenceCreator();
                IConference conference = new ConferenceType(conferenceCreator, inputArr, new TimeSpan(9, 0, 0));
                foreach (ITrack item in conference.Track)
                {
                    foreach (KeyValuePair<SessionEnum, ITalkList> talkList in item)
                    {
                        foreach (ITalk talk in talkList.Value)
                        {
                            Console.WriteLine(talk.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogInfo(ex.Message);
            }
          

            Console.ReadLine();
        }

        private static void ReadFileContents(string filePath, ref IList<string> inputArr)
        {
            inputArr = File.ReadAllLines(filePath);
        }
    }
}
