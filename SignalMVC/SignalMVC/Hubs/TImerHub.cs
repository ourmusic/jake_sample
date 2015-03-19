using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalMVC.Models;
using System.Timers;

namespace SignalMVC.Hubs
{
    //[HubName("timerHub")]
    public class TimerHub : Hub
    {
        private static Timer _timer = new Timer();

        private VideoQueue videoQueue = new VideoQueue();

        /// <summary>
        /// Starts the countdown timer for the video to finish.
        /// Used by the play.js file which handles the client-server communication functions
        /// </summary>
        /// <param name="seconds">Video duration</param>
        public void StartCountDown(int seconds)
        {
            _timer = new Timer((seconds + 2) * 1000);
            _timer.Elapsed += new ElapsedEventHandler(_timer_Done);
            _timer.Start();
        }

        /// <summary>
        /// Event handler for when the timer is done
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void _timer_Done(Object source, ElapsedEventArgs e)
        {
            _timer.Stop();
            String video = GetNextVideo(); 
            Clients.All.change(video);
        }

        /// <summary>
        /// This method is incomplete and needs to be work on by Jake and Fidel
        /// It will pull the next video from the queue and add play it.
        /// 
        /// This function gets used by the Timer event handler.
        /// </summary>
        /// <returns>The next video ID in the queue</returns>
        public String GetNextVideo()
        {
            Video toPlay = videoQueue.RemoveFirstVideo();
            return toPlay.GetUrl();
        }

        public void updateQueue()
        {
            Send();
        }

        public void Send()
        {
            //var context = GlobalHost.ConnectionManager.GetHubContext<TimerHub>();
            string jsonQueue = videoQueue.jsonQueue();
            Clients.All.allVideos(jsonQueue);
        }




    }

}