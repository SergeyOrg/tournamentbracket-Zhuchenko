using System.Collections.Generic;

namespace Table
{
    public class Tour
    {
        public int number;

        public List<string> competitors;
        public List<Round> rounds;

        public List<string> winners;
        public List<string> losers;

        public Tour(int n, List<string> c, int r)
        {
            number = n;

            competitors = c;
            rounds = new List<Round>();

            winners = new List<string>();
            losers = new List<string>();

            GenerateRounds(r);
        }

        private void GenerateRounds(int r)
        {
            for(int i = 0; i < competitors.Count; i += 2)
            {
                var round = new Round(r, competitors[i], competitors[i + 1]);
                rounds.Add(round);
                r++;
            }

            for (int i = 0; i < rounds.Count; i++)
            {
                winners.Add(rounds[i].Winner);
                losers.Add(rounds[i].Loser);
            }
        }
    }
}