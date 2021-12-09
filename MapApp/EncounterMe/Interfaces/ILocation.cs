using System;
using System.Collections.Generic;
using System.Text;

namespace EncounterMe.Interfaces
{
    public interface IGameLogic
    {

        public float distanceToUser(float lat, float lon);



        public int CompareTo(object obj);

        public void upvote();


        public void downvote();
        

        public float getRating();
        
        
    }
}
