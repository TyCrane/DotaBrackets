using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotaBrackets_WEB_2016.Models
{
    public class ViewModel
    {
        public ViewModel()
        {
            gamer = new Gamer();
            steamUser = new RootObject1();
            rootObject = new RootObject();
            dotaUser = new RootObject3();
            matchDetails = new RootObject4();
        }
        public Gamer gamer { get; set; }
        public RootObject rootObject { get; set; }
        public RootObject1 steamUser { get; set; }
        public RootObject3 dotaUser { get; set; }
        public RootObject4 matchDetails { get; set; }
    }
}