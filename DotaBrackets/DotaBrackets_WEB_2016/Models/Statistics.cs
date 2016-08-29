using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotaBrackets_WEB_2016.Models
{
    public class Statistics
    {
        public int visitors { get; set; }
        public float allMic { get; set; }
        public float hasMicNA { get; set; }
        public float hasMic { get; set; }
        public float hasMicNo { get; set; }
        public float preferMic { get; set; }
        public float preferHasMicNA { get; set; }
        public string mostCommonLanguage { get; set; }
        public float allLang { get; set; }
        public float speakEnglish { get; set; }
        public float langNoPref { get; set; }
        public float preferEnglish { get; set; }
        public string avgMmr { get; set; }
        public float allMmr { get; set; }
        public float mmrHigh { get; set; }
        public float mmrMed { get; set; }
        public float mmrLow { get; set; }
        public float prefMmrHigh { get; set; }
        public float prefMmrLow { get; set; }
        public float prefMmrMed { get; set; }
        public string mostCommonServer { get; set; }
        public float serverUse { get; set; }
        public float serverUsw { get; set; }
        public float serverEuw { get; set; }
        public float serverEue { get; set; }
        public float serverSea { get; set; }
        public float serverSa { get; set; }
        public float serverChina { get; set; }
        public float serverRussia { get; set; }
        public float serverAustralia { get; set; }
         
    }

    public class StatsLogic
    {
        public float mmr1 { get; set; }
        public float mmr2 { get; set; }
        public float mmr3 { get; set; }
        public float mmr4 { get; set; }
        public float hasMicNA { get; set; }
        public float hasMicYes { get; set; }
        public float hasMicNo { get; set; }
        public float langNoPref { get; set; }
        public float langEnglish { get; set; }
    }
}