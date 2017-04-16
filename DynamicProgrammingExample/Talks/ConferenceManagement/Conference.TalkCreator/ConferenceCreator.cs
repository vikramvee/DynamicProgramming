using Conference.Common;
using Conference.Concrete;
using Conference.Engine;
using Conference.Interfaces;
using Conference.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.TalkCreator
{
    public class ConferenceCreator : IConferenceCreator
    {
        private IEngine<ITalk> engine = new GenericEngine<ITalk>();
        public IList<ITalk> CreateTalksFromInput(IList<string> talkForConference)
        {
            try
            {
                if (talkForConference == null || talkForConference.Count == 0)
                    throw new Exception("No Talks");

                List<ITalk> validTalksList = new List<ITalk>();
                int talkCount = -1;
                String minSuffix = "min";
                String lightningSuffix = "lightning";

                // Iterate list and validate time.
                foreach (string talk in talkForConference)
                {
                    string talk1 = talk.Trim();
                    if (string.IsNullOrEmpty(talk1))
                        continue;

                    int lastSpaceIndex = talk1.LastIndexOf(" ");
                    // if talk does not have any space, means either title or time is missing.
                    if (lastSpaceIndex == -1)
                        throw new InvalidTalkException("Invalid talk, " + talk1 + ". Talk time must be specify.");

                    String name = talk1.Substring(0, lastSpaceIndex);
                    String timeStr = talk1.Substring(lastSpaceIndex + 1);

                    // If title is missing or blank.
                    if (name == null || "".Equals(name.Trim()))
                        throw new InvalidTalkException("Invalid talk name, " + talk1);

                    // If time is not ended with min or lightning.
                    else if (!timeStr.EndsWith(minSuffix) && !timeStr.EndsWith(lightningSuffix))
                        throw new InvalidTalkException("Invalid talk time, " + talk1 + ". Time must be in min or in lightning");

                    talkCount++;
                    int time = 0;
                    // Parse time from the time string .
                    try
                    {
                        if (timeStr.EndsWith(minSuffix))
                        {
                            time = Int32.Parse(timeStr.Substring(0, timeStr.IndexOf(minSuffix)));
                        }
                        else if (timeStr.EndsWith(lightningSuffix))
                        {
                            String lightningTime = timeStr.Substring(0, timeStr.IndexOf(lightningSuffix));
                            if ("".Equals(lightningTime))
                                time = 5;
                            else
                                time = Int32.Parse(lightningTime) * 5;
                        }
                    }
                    catch (FormatException)
                    {
                        throw new InvalidTalkException("Unbale to parse time " + timeStr + " for talk " + talk1);
                    }

                    // Add talk to the valid talk List.
                    validTalksList.Add(new Talk(name, time));
                }

                return validTalksList;
            }
            catch (Exception ex)
            {
                Log.LogInfo(ex.StackTrace);
                throw ex;
            }           
        }

        /// <summary>
        /// This is the inner method which would be called by the outer layer to schedule the talks using engine
        /// </summary>
        /// <param name="sessionDuration">This is the session duration for the particular session, this is the capacity for the knapsack</param>
        /// <param name="talks">These are the items wih duration as values. The engine will return the best matching items using engine</param>
        /// <returns></returns>
        public ITalkList ScheduleTalksForEachTrack(int sessionDuration, IList<ITalk> talks)
        {
            try
            {
                ITalkList talkList = new TalkList();
                List<ITalk> talksFromEngine = engine.GetTheBestSuitableItem(sessionDuration, talks);

                foreach (var item in talksFromEngine)
                {
                    talkList.Add(item);
                }

                ChangeTheTalkStatusToSchedule(talkList, talks);
                return talkList;
            }
            catch (Exception ex)
            {
                Log.LogInfo(ex.StackTrace);
                throw;
            }
          
        }


        /// <summary>
        /// The method is used to change the status returned from the Engine to IsScheduled  true.
        /// </summary>
        /// <param name="talkList">The talk list recieved from the engine</param>
        /// <param name="talks">The parent talk list</param>
        private void ChangeTheTalkStatusToSchedule(ITalkList talkList, IList<ITalk> talks)
        {
            try
            {
                talks = talks.Select(item =>
                {
                    if (talkList.Contains(item))
                    {
                        item.IsScheduled = true;
                    }
                    return item;
                }).ToList();
            }
            catch (Exception ex)
            {
                Log.LogInfo(ex.StackTrace);
                throw ex;
            }
            
        }
    }
}
