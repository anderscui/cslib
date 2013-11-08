using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Andersc.CodeLib.Algorithm
{
    public class WordSearchPuzzle
    {
        private int rows;
        private int columns;
        // TODO: or char[][]?
        private char[,] board;
        private string[] words;

        public WordSearchPuzzle(string puzzleFileName, string wordFileName)
        {
            ReadPuzzle(puzzleFileName);
            ReadWords(wordFileName);
        }

        public int Solve()
        {
            int matches = 0;
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < columns; colIndex++)
                {
                    for (int rd = -1; rd <= 1; rd++)
                    {
                        for (int cd = -1; cd <= 1; cd++)
                        {
                            if (rd!= 0 || cd != 0)
                            {
                                matches += SolveDirection(rowIndex, colIndex, rd, cd);
                            }
                        }
                    }
                }
            }

            return matches;
        }

        private string[] OpenFile(string fileName)
        {
            return File.ReadAllLines(fileName);
        }

        private void ReadWords(string wordFileName)
        {
            string[] lines = OpenFile(wordFileName);
            List<string> wordList = new List<string>();

            string lastWord = null;
            string thisWord = null;
            for (int i = 0; i < lines.Length; i++)
            {
                thisWord = lines[i];
                if (!string.IsNullOrWhiteSpace(lastWord)
                    && (thisWord.CompareTo(lastWord) < 0))
                {
                    // Dict is not sorted, so i skip it.
                    continue;
                }
                wordList.Add(thisWord);
                lastWord = thisWord;
            }

            words = wordList.ToArray();
        }

        // TODO: how to test private/protected/internal methods?
        private void ReadPuzzle(string puzzleFileName)
        {
            string[] lines = OpenFile(puzzleFileName);
            List<string> puzzleLines = new List<string>();

            string oneLine = lines[0];
            columns = oneLine.Length;
            puzzleLines.Add(oneLine);

            for (int i = 1; i < lines.Length; i++)
            {
                oneLine = lines[i];
                if (oneLine.Length != columns)
                {
                    throw new InvalidDataException("Puzzle is not rectangular, skip it.");
                }
                puzzleLines.Add(oneLine);
            }

            rows = puzzleLines.Count;
            board = new char[rows, columns];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < puzzleLines[i].Length; j++)
                {
                    board[i, j] = puzzleLines[i][j];
                }
            }
        }

        private static int PrefixSearch(string[] a, string x)
        {
            int index = Array.BinarySearch(a, x);
            return (index >= 0) ? index : ~index;
        }

        private int SolveDirection(int baseRow, int baseCol, int rowDelta, int colDelta)
        {
            string charSequence = board[baseRow, baseCol].ToString();
            int matches = 0;
            int searchResult = 0;

            for (int i = baseRow + rowDelta, j = baseCol + colDelta; 
                 i >= 0 && j >= 0 && i < rows && j < columns;
                 i += rowDelta, j += colDelta)
            {
                charSequence += board[i, j];
                searchResult = PrefixSearch(words, charSequence);

                // No words with this prefix.
                if (searchResult == words.Length)
                {
                    break;
                }
                if (!words[searchResult].StartsWith(charSequence))
                {
                    break;
                }

                if (words[searchResult].Equals(charSequence))
                {
                    matches++;
                    Console.WriteLine("Found '{0}' at [{1}, {2}] to [{3}, {4}]",
                        charSequence, baseRow, baseCol, i, j);
                }
            }

            return matches;
        }
    }
}
