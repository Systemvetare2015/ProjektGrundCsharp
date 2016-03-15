
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

        }
        /// <summary>
        /// Metoden skriver till filen scores.csv
        /// </summary>
        private void SavetoFile()
        {
            // skriva till fil
            using (var utfil = new StreamWriter(filename,false))
            {
                foreach (var player in Scores)
                {
                    utfil.WriteLine("{0};{1};{2};{3}", player.PlayerName,ConvertToInt(player.Game), player.Score, player.TimePlayed);
                }
            }

        }
        /// <summary>
        /// Metoden läser från filen
        /// </summary>
        private void ReadFromFile()
        {
            if (!File.Exists(filename))
            {
                return;
            }
            using (var reader = new StreamReader(File.OpenRead(filename)))
            {
                int index = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    if (values.Length == 4)
                    {
                        var newplayer = new Player(values[0]);
                        newplayer.Game = ConvertToEnum(int.Parse(values[1]));
                        newplayer.Score = int.Parse(values[2]);
                        newplayer.TimePlayed = DateTime.Parse(values[3]);
                        Scores = ExpandArray(Scores, newplayer);
                        index++;
                    }
                   
                }
            }

        }
        /// <summary>
        /// Get all player in database
        /// </summary>
        /// <returns>List of players</returns>
        public Player[] GetAllPlayerScores()
        {
            return Scores;
        }

        /// <summary>
        /// Search for player in database
        /// </summary>
        /// <param name="search">Search string</param>
        /// <returns>Matched players</returns>
        public Player[] SearchPlayers(string search)
        {
            var searchResult = new Player[0];
            foreach (var player in Scores)
            {
                if (player.PlayerName.ToLower().Contains(search.ToLower()))
                {
                    searchResult = ExpandArray(searchResult, player);
                }
            }
            return searchResult;
        }
        /// <summary>
        /// adds player to top list if he can compete with score
        /// </summary>
        /// <param name="newPlayer"></param>
        public void AddPlayer(Player newPlayer)
        {
            Scores = ExpandArray(Scores, newPlayer);
            Sort(Scores);
            Scores = ShortnedArrayBy(Scores, Scores.Length - 5);
            SavetoFile();
        }
        /// <summary>
        /// sorts players
        /// </summary>
        /// <param name="scores">sorted playerscores</param>
        private void Sort(Player[] scores)
        {
            var unsorted = true;
            if (scores.Length <2)
            {
                return;
            }
            var endIndex = scores.Length - 1;
            while (unsorted)
            {
                unsorted = false;
                for (var i = 0; i < endIndex; i++)
                {
                    if (scores[i].Score < scores[i + 1].Score)
                    {
                        Swap(scores, i, i + 1);
                        unsorted = true;
                    }
                }
                endIndex--;
            }
        }
        /// <summary>
        /// swap method
        /// </summary>
        /// <param name="vektor"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
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
        /// <summary>
        /// Remove last int objects from array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vektor">array to remove from</param>
        /// <param name="removeAmountOfElements">amount of objects to remove</param>
        /// <returns>new shortened array</returns>
        private T[] ShortnedArrayBy<T>(T[] vektor, int removeAmountOfElements)
        {

            if (removeAmountOfElements < 0)
            {
                return vektor;
            }
            var newVektor = new T[vektor.Length -removeAmountOfElements];
            for (var i = 0; i < vektor.Length - removeAmountOfElements; i++)
            {
                newVektor[i] = vektor[i];
            }
            return newVektor;
        }
        /// <summary>
        /// Converts int to enum
        /// </summary>
        /// <param name="GameType"></param>
        /// <returns>Enum type</returns>
        private Player.GameType ConvertToEnum(int GameType)
        {
            return GameType == 1 ? Player.GameType.Snake : Player.GameType.Tetris;
        }
        /// <summary>
        /// Converts enum to int for db
        /// </summary>
        /// <param name="GameType"></param>
        /// <returns>Int</returns>
        private int ConvertToInt(Player.GameType GameType)
        {
            return GameType == Player.GameType.Snake ? 1 : 2;
        }
    }
}
