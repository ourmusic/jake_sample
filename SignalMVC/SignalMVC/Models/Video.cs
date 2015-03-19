using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalMVC.Models
{
    public class Video
    {
        //may need to change these variables to be public
        public string title;        //title of the video 
        public string url;          //url of the video
        //private string artist;       //artist of the video
        //private string album;        //album of the video
        public int votes;           //votes to determine play order

<<<<<<< HEAD
        //constructor for video class, initially all unknown
=======
        /// <summary>
        /// constructor for video class, initially all unknown
        /// </summary>
>>>>>>> origin/master
        public Video()
        {
            this.title = "";
            this.url = "";
            //this.artist = "";
            //this.album = "";
            this.votes = 0;
        }

<<<<<<< HEAD
        public Video(string t, string u, int v)
        {
            this.title = t;
            this.url = u;
            this.votes = v;
        }

        /*Title Helper Functions*/
        public string getTitle()
=======
        /// <summary>
        /// Title Helper Functions
        /// </summary>
        /// <returns></returns>
        public string GetTitle()
>>>>>>> origin/master
        {
            return this.title;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        public void SetTitle(string title)
        {
            this.title = title;
        }

<<<<<<< HEAD
        /*Artist Helper Functions*/
        /*public string getArtist()
=======
        /// <summary>
        /// Artist Helper Functions
        /// </summary>
        /// <returns></returns>
        public string GetArtist()
>>>>>>> origin/master
        {
            return this.artist;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="artist"></param>
        public void SetArtist(string artist)
        {
            this.artist = artist;
        }
<<<<<<< HEAD
        */
        /*Album Helper Functions*/
        /*
        public string getAlbum()
=======

        /// <summary>
        /// Album Helper Functions
        /// </summary>
        /// <returns></returns>
        public string GetAlbum()
>>>>>>> origin/master
        {
            return this.album;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="album"></param>
        public void SetAlbum(string album)
        {
            this.album = album;
        }
<<<<<<< HEAD
        */
        /*URL Helper Functions*/
        public string getUrl()
=======

        /// <summary>
        /// URL Helper Functions
        /// </summary>
        /// <returns></returns>
        public string GetUrl()
>>>>>>> origin/master
        {
            return this.url;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public void SetUrl(string url)
        {
            this.url = url;
        }

        /// <summary>
        /// Votes Helper Functions
        /// </summary>
        /// <returns></returns>
        public int GetVotes()
        {
            return this.votes;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Upvote()
        {
            this.votes++;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Downvote()
        {
            this.votes--;
        }
    }

}