using System.Collections.Generic;
using System.Linq;

namespace Table
{
    public class DoubleEliminationTableBuilder
    {
        public List<Tour> WinnersTours;
        public List<Tour> LosersTours;

        private int numberOfTour;
        private int numberOfRound;

        public DoubleEliminationTableBuilder()
        {
            WinnersTours = new List<Tour>();
            LosersTours = new List<Tour>();

            numberOfTour = 1;
            numberOfRound = 1;
        }

        public List<string> AddWinnersTour(List<string> competitors)
        {
            WinnersTours.Add(
                new Tour(numberOfTour, competitors, numberOfRound)
                );

            numberOfTour++;
            numberOfRound += WinnersTours.Last().rounds.Count;

            return WinnersTours.Last().losers;
        }

        public void AddLosersTour(List<string> competitors)
        {
            LosersTours.Add(new Tour(numberOfTour, competitors, numberOfRound));

            numberOfTour++;
            numberOfRound += LosersTours.Last().rounds.Count;
        }

        public List<string> GetWinnersFromLastWinnersTour()
        {
            return WinnersTours.Last().winners;
        }

        public List<string> GetWinnersFromLastLosersTour()
        {
            return LosersTours.Last().winners;
        }

        public List<string> MixCompetitors(List<string> firstPart, List<string> secondPart)
        {
            for (int i = 0; i < secondPart.Count; i++)
            {
                firstPart.Insert(i, secondPart[i]);
            }

            return firstPart;
        }
    }
}