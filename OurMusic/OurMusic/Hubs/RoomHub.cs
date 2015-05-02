using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using OurMusic.Models;
using System.Timers;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OurMusic.Hubs
{
    //[HubName("roomHub")]
    public class RoomHub : Hub
    {
        private OurMusicEntities db = new OurMusicEntities();
        public static UserManager<ApplicationUser> umanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        private static ConcurrentDictionary<string, PublicRoom> rooms = new ConcurrentDictionary<string, PublicRoom>();
        private static ConcurrentDictionary<string, Tuple<string, string>> connections2usersANDgroups = new ConcurrentDictionary<string, Tuple<string, string>>();

        public static void alertRoomHasBeenDeleted(string roomName)
        {
            var notifyContext = GlobalHost.ConnectionManager.GetHubContext<RoomHub>();
            notifyContext.Clients.Group(roomName).alertRoomHasBeenDeleted();
        }

        /// StartInitCountDown COMMENTS OUT OF DATE AS OF 4/20
        /// <summary>
        /// Room admin starts the counter.
        /// Also adds the ElapsedEventHandler
        /// </summary>
        /// <param name="seconds">Number of seconds for the first video</param>
        public void StartInitCountDown(int seconds, string roomName)
        {
            PublicRoom clientsRoom = rooms.GetOrAdd(roomName, new PublicRoom(roomName));
            clientsRoom.CountDown(seconds);
            
        }


        public void addToQueue(string vidTitle, string vidUrl, string roomName)
        {
            System.Diagnostics.Debug.WriteLine("addToQueue(" + vidTitle + ", " + vidUrl + ", " + roomName + ")");
            rooms[roomName].AddToQueue(vidTitle, vidUrl);
        }



        /**
         * called by a client, must specify roomName of existing room.
         * changes the vote score for specified video by voteChange
         * updates the queue, and then tells all members of that group what to change in their queues
         **/
        public void voteByTitleAndUrl(string vidTitle, string vidUrl, int voteChange, string roomName)
        {
            System.Diagnostics.Debug.WriteLine("in voteByTitleAndUrl, roomname = " + roomName + ".");
            int movement = rooms[roomName].voteAndUpdate(vidTitle, vidUrl, voteChange);
            System.Diagnostics.Debug.WriteLine("vote " + vidTitle + ", movement = " + movement);
            Clients.Group(roomName).adjustVotesAndPlacement(vidUrl, voteChange, movement);

        }

        public void deleteVideo(string videoTitle, string videoURL, string roomName)
        {
            rooms[roomName].deleteVideo(videoTitle, videoURL);
            Clients.Group(roomName).deleteVideo(videoURL);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (stopCalled)
            {
                Tuple<string, string> IdRoomNameTuple;
                if (connections2usersANDgroups.TryRemove(Context.ConnectionId, out IdRoomNameTuple))
                {
                    string userID = IdRoomNameTuple.Item1;
                    string roomName = IdRoomNameTuple.Item2;
                    Clients.Group(roomName).userRemoved(userID);
                }
            }
            return base.OnDisconnected(stopCalled);
        }


        /**
         * called by room administrator's javascript
         * updates removed user from db, and notifies room that user was removed
         * the removed user will have to make a separate call to remove himself from the group
         **/
        public void removeUser(Guid uID, string roomName)
        {
            Clients.Group(roomName).userRemoved(uID.ToString());

            var user = db.People.Find(uID);
            user.activeRoom = null;
            db.SaveChangesAsync();
        }

        public Task leaveRoom(string roomName)
        {
            return Groups.Remove(Context.ConnectionId, roomName);
        }

        public void Send(String name, String message, string roomName)
        {
            Clients.Group(roomName).addNewMessageToPage(name, message);
        }


        /**
         * This implements the generic JoinRoom function
         * If room already exists, then retrieves that room's queue, and sends back a json of it to requesting client.
         * If room doesn't exist, adds it to dictionary and groups
         **/
        public async Task refreshClient(string roomName, string userID)
        {

            await Groups.Add(Context.ConnectionId, roomName);
            connections2usersANDgroups.TryAdd(Context.ConnectionId, new Tuple<string, string>(userID, roomName));
            PublicRoom clientsRoom = rooms.GetOrAdd(roomName, new PublicRoom(roomName));
            string jsonOfQueue = clientsRoom.jsonRoomsQueue();
            clientsRoom.updateContext();
            Clients.Caller.refreshList(jsonOfQueue);

        }


        /**
         * Each client calls this upon entering a room.  
         * Other users in that room then know to add this user to their list of room members.
         **/
        public void announceEntranceToRoom(string roomName, string firstName, string lastName, string userID)
        {
            Clients.OthersInGroup(roomName).addNewUser(firstName, lastName, userID);
        }



        //NEW MOTHODS FOR ROOMS
        public void InitRoom(String guid)
        {
            //Need to check if this guid had already been used
            //This part relies on how you implement the data structure, Jake.
            System.Diagnostics.Debug.WriteLine("In InitRoom(" + guid + ")");
            if (!rooms.ContainsKey(guid))
            {
                PublicRoom pub = new PublicRoom(guid);
                rooms.TryAdd(guid, pub);
                System.Diagnostics.Debug.WriteLine("Value added for key = " + guid);
            }


        }

        public void CountDown(int seconds, String guid)
        {
            //Find room with guid and start the countdown.
            System.Diagnostics.Debug.WriteLine("THub CountDown(" + guid + ", " + seconds + ")");
            PublicRoom room;
            if(rooms.TryGetValue(guid, out room)){
                room.CountDown(seconds);
            }
        }


    }
}