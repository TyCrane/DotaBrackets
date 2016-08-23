using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotaBrackets_WEB_2016.Models
{
    public class DotaPlayer
    {
        public object account_id { get; set; }
        public int player_slot { get; set; }
        public int hero_id { get; set; }
    }

    public class Match
    {
        public Match()
        {
            this.dotaPlayers = new List<DotaPlayer>();
        }
        public long match_id { get; set; }
        public long match_seq_num { get; set; }
        public int start_time { get; set; }
        public int lobby_type { get; set; }
        public int radiant_team_id { get; set; }
        public int dire_team_id { get; set; }
        public List<DotaPlayer> dotaPlayers { get; set; }
    }

    public class Result
    {
        public Result()
        {
            this.matches = new List<Match>();
        }
        public int status { get; set; }
        public int num_results { get; set; }
        public int total_results { get; set; }
        public int results_remaining { get; set; }
        public List<Match> matches { get; set; }
    }

    public class RootObject3
    {
        public RootObject3()
        {
            this.result = new Result();
        }
        public Result result { get; set; }
    }
}