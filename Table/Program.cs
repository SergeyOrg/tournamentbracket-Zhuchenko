using System;
using System.Collections.Generic;
using System.IO;

namespace Table
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> competitors = new List<string>();
            using (var sr = new StreamReader("input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    competitors.Add(line.TrimEnd());
                }
            }

            SingleElimination(competitors);

            DoubleElimination(competitors);

            Console.ReadLine();
        }

        private static void SingleElimination(List<string> competitors)
        {
            int numberOfRound = 1;
            var tours = new List<Tour> {
                new Tour(1, competitors, numberOfRound)
            };
            var previousTour = tours[0];

            while (previousTour.winners.Count != 1)
            {
                numberOfRound += previousTour.rounds.Count;

                var currentTour = new Tour(previousTour.number + 1, previousTour.winners, numberOfRound);
                tours.Add(currentTour);

                previousTour = currentTour;
            }

            ShowWinners(tours);
        }

        private static void DoubleElimination(List<string> competitors)
        {
            var builder = new DoubleEliminationTableBuilder();

            var waitQueue = builder.AddWinnersTour(competitors);

            builder.AddLosersTour(waitQueue);
            
            while (builder.GetWinnersFromLastWinnersTour().Count != 1 || builder.GetWinnersFromLastLosersTour().Count != 1)
            {
                waitQueue = builder.AddWinnersTour(builder.GetWinnersFromLastWinnersTour());

                while (builder.GetWinnersFromLastLosersTour().Count > waitQueue.Count)
                {
                    builder.AddLosersTour(builder.GetWinnersFromLastLosersTour());
                }

                var currentCompetitors = builder.MixCompetitors(builder.GetWinnersFromLastLosersTour(), waitQueue);
                builder.AddLosersTour(currentCompetitors);
            }
        }
        
        private static void ShowWinners(List<Tour> tours)
        {
            var numberOfStr = tours[0].competitors.Count * 2;
            var output = new string[numberOfStr];

            for (int i = 0; i < numberOfStr; i++)
            {
                output[i] = "";
            }

            var numberOfStartStr = 0;
            var numberOfSpaces = 2;

            foreach(var tour in tours)
            {
                var maxStrLength = int.MinValue;
                for(int i = numberOfStartStr, numberOfRound = 0; 
                    i < numberOfStr && numberOfRound < tour.rounds.Count; 
                    i += numberOfSpaces, numberOfRound++)
                {
                    output[i] += tour.rounds[numberOfRound].number + ". " + tour.rounds[numberOfRound].firtstCompetitor;
                    if (output[i].Length > maxStrLength)
                        maxStrLength = output[i].Length;

                    i += numberOfSpaces;

                    output[i] += tour.rounds[numberOfRound].number + ". " + tour.rounds[numberOfRound].secondCompetitor;
                    if (output[i].Length > maxStrLength)
                        maxStrLength = output[i].Length;
                }

                maxStrLength += 3;

                for (int i = 0; i < numberOfStr; i++)
                {
                    while(output[i].Length < maxStrLength)
                    {
                        output[i] += " ";
                    }
                }

                numberOfStartStr = numberOfSpaces - 1;
                numberOfSpaces = (int)Math.Pow(2, tour.number + 1);
            }

            var numberOfWinnersStr = (numberOfStr - 1) / 2;
            output[numberOfWinnersStr] += tours[tours.Count - 1].winners[0];

            for (int i = 0; i < numberOfStr; i++)
            {
                Console.WriteLine(output[i]);
            }
        }
    }
}