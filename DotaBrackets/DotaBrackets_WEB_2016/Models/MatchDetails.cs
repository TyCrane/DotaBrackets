using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotaBrackets_WEB_2016.Models
{
    public class AbilityUpgrade
    {
        public int ability { get; set; }
        public int time { get; set; }
        public int level { get; set; }
    }

    public class MatchPlayer
    {
        public MatchPlayer()
        {
            this.ability_upgrades = new List<AbilityUpgrade>();
        }
        public object account_id { get; set; }
        public int player_slot { get; set; }
        public int hero_id { get; set; }
        public int item_0 { get; set; }
        public int item_1 { get; set; }
        public int item_2 { get; set; }
        public int item_3 { get; set; }
        public int item_4 { get; set; }
        public int item_5 { get; set; }
        public int kills { get; set; }
        public int deaths { get; set; }
        public int assists { get; set; }
        public int leaver_status { get; set; }
        public int last_hits { get; set; }
        public int denies { get; set; }
        public int gold_per_min { get; set; }
        public int xp_per_min { get; set; }
        public int level { get; set; }
        public int gold { get; set; }
        public int gold_spent { get; set; }
        public int hero_damage { get; set; }
        public int tower_damage { get; set; }
        public int hero_healing { get; set; }
        public List<AbilityUpgrade> ability_upgrades { get; set; }
    }

    public class Results
    {
        public Results()
        {
            players = new List<MatchPlayer>();
        }
        public List<MatchPlayer> players { get; set; }
        public bool radiant_win { get; set; }
        public int duration { get; set; }
        public int pre_game_duration { get; set; }
        public int start_time { get; set; }
        public long match_id { get; set; }
        public long match_seq_num { get; set; }
        public int tower_status_radiant { get; set; }
        public int tower_status_dire { get; set; }
        public int barracks_status_radiant { get; set; }
        public int barracks_status_dire { get; set; }
        public int cluster { get; set; }
        public int first_blood_time { get; set; }
        public int lobby_type { get; set; }
        public int human_players { get; set; }
        public int leagueid { get; set; }
        public int positive_votes { get; set; }
        public int negative_votes { get; set; }
        public int game_mode { get; set; }
        public int flags { get; set; }
        public int engine { get; set; }
        public int radiant_score { get; set; }
        public int dire_score { get; set; }
    }

    public class RootObject4
    {
        public RootObject4()
        {
            result = new Results();
        }
        public Results result { get; set; }
    }
}