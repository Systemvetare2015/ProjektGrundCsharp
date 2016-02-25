
using System;
using System.IO;

namespace Tetris
{
    class PlayerScores
    {
        private Player[] Scores = new Player[0];
        private string filename = "scores.csv";

        public PlayerScores()
        {
            ReadFromFile();
            SavetoFile();

        }

        private void SavetoFile()
        {
            // skriva till fil
            using (var utfil = new StreamWriter(filename,false))
            {
                foreach (var player in Scores)
                {
                    utfil.WriteLine("{0};{1};{2};{3}", player.PlayerName, player.Score, player.TimePlayed, ConvertToInt(player.Game));
                }
                utfil.Close();
            }

        }

        private void ReadFromFile()
        {
            using (var reader = new StreamReader(File.OpenRead(filename)))
            {
                int index = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    var newplayer = new Player(values[0]);
                    newplayer.Game = ConvertToEnum(int.Parse(values[1]));
                    newplayer.Score = int.Parse(values[2]);
                    newplayer.TimePlayed = DateTime.Parse(values[3]);
                    Scores[index] = newplayer;
                    Scores = ExpandArray(Scores, newplayer);
                    index++;
                }
            }

        }

        public void AddPlayer(Player newPlayer)
        {
            Scores = ExpandArray(Scores, newPlayer);
            Sort(Scores);
            SavetoFile();
        }

        private void Sort(Player[] scores)
        {
            var unsorted = true;
            var endIndex = scores.Length;
            while (unsorted)
            {
                unsorted = false;
                for (var i = 0; i < endIndex; i++)
                {
                    if (scores[i].Score > scores[i + 1].Score)
                    {
                        Swap(scores, i, i + 1);
                        unsorted = true;
                    }
                }
            }
        }

        private void Swap(Player[] vektor, int a, int b)
        {
            var r = vektor[a];
            vektor[a] = vektor[b];
            vektor[b] = r;
        }
        /// <summary>
        /// Expands genric vektor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vektor"></param>
        /// <param name="newObject"></param>
        /// <returns>New Expanded vektor</returns>
        private T[] ExpandArray<T>(T[] vektor, T newObject)
        {
            var newVektor = new T[vektor.Length + 1];
            for (var i = 0; i < vektor.Length; i++)
            {
                newVektor[i] = vektor[i];
            }
            newVektor[newVektor.Length - 1] = newObject;
            return newVektor;
        }
        private Player.GameType ConvertToEnum(int GameType)
        {
            return GameType == 1 ? Player.GameType.Snake : Player.GameType.Tetris;
        }
        private int ConvertToInt(Player.GameType GameType)
        {
            return GameType == Player.GameType.Snake ? 1 : 2;
        }
    }
}
