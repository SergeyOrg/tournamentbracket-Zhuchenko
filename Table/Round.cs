namespace Table
{
    public class Round
    {
        public int number;

        public string firtstCompetitor;
        public string secondCompetitor;

        private string _winner;
        private string _loser;

        public string Winner
        {
            get => _winner ?? "W" + number;
            set => _winner = _winner ?? value;
        }

        public string Loser
        {
            get => _loser ?? "L" + number;
            set => _loser = _loser ?? value;
        }

        public Round(int n, string firtst, string second)
        {
            number = n;

            firtstCompetitor = firtst;
            secondCompetitor = second;
        }
    }
}