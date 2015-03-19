using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalMVC.Models
{
    public class VideoQueue
    {
        private LinkedList<Video> videoList = new LinkedList<Video>();

<<<<<<< HEAD
        public int getLength()
        {
            return videoList.Count;
        }
        public void addVideo(Video v)
        {
            if(!videoList.Contains(v)) videoList.AddLast(v);
        }
        public Video removeFirstVideo()
=======
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        void AddVideo(Video v)
        {
            if(!videoList.Contains(v)) videoList.AddLast(v);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Video RemoveFirstVideo()
>>>>>>> origin/master
        {
            Video ret = videoList.First();
            videoList.RemoveFirst();
            return ret;
        }
<<<<<<< HEAD
        public void removeUnwantedVideo(Video v)
=======

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        public void RemoveUnwantedVideo(Video v)
>>>>>>> origin/master
        {
            LinkedListNode<Video> l = videoList.Last;
            while(l.Previous.Value != v) l = l.Previous;
            
        }
<<<<<<< HEAD
        public void upvote(Video v)
        {
            LinkedListNode<Video> vid = videoList.Find(v);
            vid.Value.upvote();
            //checkOrder(vid);
            if((vid.Previous != null) && (vid.Previous.Value.getVotes() < vid.Value.getVotes()))
            {
                LinkedListNode<Video> seeker = vid.Previous;
                while((seeker.Previous != null) && (seeker.Previous.Value.getVotes() < vid.Value.getVotes()))
                {
                    seeker = seeker.Previous;
                }
                videoList.Remove(vid);
                videoList.AddBefore(seeker, vid);
            }
        }
        public void downvote(Video v)
        {
            LinkedListNode<Video> vid = videoList.Find(v);
            vid.Value.downvote();
            //checkOrder(vid);
            if ((vid.Next != null) && (vid.Next.Value.getVotes() > vid.Value.getVotes()))
            {
                LinkedListNode<Video> seeker = vid.Next;
                while ((seeker.Next != null) && (seeker.Next.Value.getVotes() < vid.Value.getVotes()))
                {
                    seeker = seeker.Next;
                }
                videoList.Remove(vid);
                videoList.AddAfter(seeker, vid);
            }
=======

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        public void Upvote(Video v)
        {
            LinkedListNode<Video> vid = videoList.Find(v);
            vid.Value.Upvote();
            CheckOrder(vid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        public void Downvote(Video v)
        {
            LinkedListNode<Video> vid = videoList.Find(v);
            vid.Value.Downvote();
            CheckOrder(vid);
>>>>>>> origin/master
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public void CheckOrder(LinkedListNode<Video> node)
        {
            LinkedListNode<Video> temp = node;
<<<<<<< HEAD
            if ((node.Next != null) && (node.Value.getVotes() < node.Next.Value.getVotes()))
            {
                while ((temp != null) && (temp.Value.getVotes() < temp.Next.Value.getVotes()))
=======
            if (node.Value.GetVotes() < node.Next.Value.GetVotes())
            {
                while(temp.Value.GetVotes() < temp.Next.Value.GetVotes())
>>>>>>> origin/master
                {
                    temp = node.Next;
                }
                videoList.Remove(node);
                videoList.AddBefore(temp, node);
            }
<<<<<<< HEAD
            else if((node.Previous != null) && (node.Value.getVotes() > node.Previous.Value.getVotes()))
            {
                while((temp != null) && (temp.Value.getVotes() > temp.Previous.Value.getVotes()))
=======
            else if(node.Value.GetVotes() > node.Previous.Value.GetVotes())
            {
                while(temp.Value.GetVotes() > temp.Previous.Value.GetVotes())
>>>>>>> origin/master
                {
                    temp = node.Previous;
                }
                videoList.Remove(node);
                videoList.AddAfter(temp, node);
            }
        }

        public string jsonQueue()
        {
            
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            settings.NullValueHandling = NullValueHandling.Ignore;
            //settings.Formatting = Formatting.Indented;


            string json = JsonConvert.SerializeObject(videoList, settings);
            Console.WriteLine(json);
            return json;
        }

    }
}