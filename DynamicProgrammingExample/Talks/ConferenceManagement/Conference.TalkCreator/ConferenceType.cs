using Conference.Common;
using Conference.Concrete;
using Conference.Interfaces;
using Conference.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Conference.TalkCreator
{
    public class ConferenceType : IConference
    {
        private IConferenceCreator creator;
        private IList<ITrack> tracks;

        private IList<ITalk> localtalks;
        private int allTalksTime;     
        private TimeSpan startTime;

        public ConferenceType(IConferenceCreator creator, IList<string> talks, TimeSpan startTime)
        {
            this.creator = creator;
            this.startTime = startTime;

            CreateTalksFromInput(talks);
            GetTotalTalksTime(localtalks);
           
        }

        /// <summary>
        /// The method get the track and respective talks for each session in the track.
        /// </summary>
        /// <returns></returns>
        private IList<ITrack> GetTracks()
        {
            try
            {
                //Get all the session present in the Track for the day
                SessionEnum[] array = (SessionEnum[])Enum.GetValues(typeof(SessionEnum));

                //Get the sum of all the sessions time excluding the Lunch Time
                double allSessionTime = array.Where(item => ((SessionEnum)item) != SessionEnum.Lunch).Sum(item =>
                ((TimeSpan)SessionHelper.GetSesionAttributes(item, Constants.Duration)).TotalMinutes);


                //Get the number of tracks by dividing the all talks time by the session time available
                double numberOfTracks = allTalksTime / allSessionTime;

                tracks = new List<ITrack>();
                for (int i = 0; i < Convert.ToInt32(numberOfTracks); i++)
                {
                    //create a track 
                    Track track = new Concrete.Track();

                    //for each session in the track, get the number of talks using the Generic engine
                    foreach (SessionEnum item in (SessionEnum[])Enum.GetValues(typeof(SessionEnum)))
                    {
                        ITalkList talkList = null;
                        int sessionTime = Convert.ToInt32(((TimeSpan)SessionHelper.GetSesionAttributes(item, Constants.Duration)).TotalMinutes);

                        //If the session are Lunch and Netowrking add the blank talk reference
                        bool isExtraTime = (bool)SessionHelper.GetSesionAttributes(item, Constants.IsExtraTime);
                        bool isTalkSession = (bool)SessionHelper.GetSesionAttributes(item, Constants.IsTalkSession);

                        if (isExtraTime || !isTalkSession)
                        {
                            track.Add(item, new TalkList() { new Talk(item.ToString(), sessionTime) });
                        }
                        else
                        {
                            //For each session we will pass the parameter to the Engine the total number of the minutes as capacity and all the talks                       
                            talkList = creator.ScheduleTalksForEachTrack(sessionTime, localtalks.Where(talk => talk.IsScheduled == false).ToList());

                            track.Add(item, talkList);
                        }
                    }
                    tracks.Add(track);
                }

                //Find the talks which are not yet scheduled
                CheckForRaminingTalks();

                AssignTimeToTalks(tracks);

                return tracks;

            }
            catch (Exception ex)
            {
                Log.LogInfo(ex.StackTrace);
                throw;
            }
          
        }

        private void CheckForRaminingTalks()
        {
            //find the reaming talks total time which are not yet scheduled.
            double remainingTalksDuration = localtalks.Where(item => item.IsScheduled == false).Sum(item1 => item1.Value);

            //if the remaining time is more then 0, try to adjust the talks in the Networkin session
            if (remainingTalksDuration > 0)
            {
                int i = 0;
                while (i < tracks.Count)
                {
                    ITrack track = tracks[i];
                    ITalkList talkList = null;
                    foreach (SessionEnum item in (SessionEnum[])Enum.GetValues(typeof(SessionEnum)))
                    {
                        int sessionTime = Convert.ToInt32(((TimeSpan)SessionHelper.GetSesionAttributes(item, Constants.Duration)).TotalMinutes);

                        if ((SessionEnum)item == SessionEnum.Networking)
                        {
                            talkList = creator.ScheduleTalksForEachTrack(sessionTime, localtalks.Where(talk => talk.IsScheduled == false).ToList());
                            track[item] = talkList;
                            track[item].Add(new Talk(item.ToString(), 0));
                        }
                    }
                    i++;
                }
            }
        }


        /// <summary>
        /// The method is used to assign time for all the scheduled talks
        /// </summary>
        /// <param name="tracks"></param>
        private void AssignTimeToTalks(IList<ITrack> tracks)
        {            
            DateTime date = new DateTime();
            TimeSpan ts = startTime;
            date = date.Date + ts;           
            TimeSpan scheduledTime = date.TimeOfDay;

            foreach (var item in tracks)
            {
                //Order the sessions presnet in the SessionEnum
                IOrderedEnumerable<KeyValuePair<SessionEnum, ITalkList>> orderedTracks = item.OrderBy(keyValuePair => 
                SessionHelper.GetSesionAttributes((SessionEnum)keyValuePair.Key, Constants.Order));

                ITalk previousTalk = null;
                foreach (KeyValuePair<SessionEnum, ITalkList> talkList in orderedTracks)
                {
                    //If the session is the lunch time add an empty talk to it with schedule time
                    bool isTalkSession = (bool)SessionHelper.GetSesionAttributes(talkList.Key, Constants.IsTalkSession);
                    //if (talkList.Key == SessionEnum.Lunch)
                    if(!isTalkSession)
                    {
                        talkList.Value[0].ScheduledTime = previousTalk.ScheduledTime.Add(new TimeSpan(0, previousTalk.Value, 0));
                        previousTalk = talkList.Value[0];
                        continue;
                    }

                    foreach (ITalk talk in talkList.Value)
                    {
                        //If it is the first talk of the track start it with the start time provided while initilizing this class
                        if (previousTalk == null)
                        {
                            talk.ScheduledTime = scheduledTime;
                        }
                        //Else add the duration of the previous talk to the previous talks scheduled time 
                        else
                        {
                            talk.ScheduledTime = previousTalk.ScheduledTime.Add(new TimeSpan(0, previousTalk.Value, 0));
                        }

                        previousTalk = talk;
                    }
                }
            }
        }

        private void GetTotalTalksTime(IList<ITalk> localtalks)
        {
            allTalksTime = localtalks.Sum(item => item.Value);
        }

        private void CreateTalksFromInput(IList<string> talks)
        {
            localtalks = creator.CreateTalksFromInput(talks);
        }

        public IList<ITrack> Track
        {
            get
            {
                return GetTracks();
            }           
        }
       
    }
}
