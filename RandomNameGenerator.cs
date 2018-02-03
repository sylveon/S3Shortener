using System;
using System.Globalization;
using System.IO;

namespace Sylveon.S3Shortener
{
    static class RandomNameGenerator
    {
        private static Random _rng = new Random();
        private static string[] _adjectives = File.ReadAllLines("adjectives.txt");
        private static string[] _animals = File.ReadAllLines("animals.txt");

        public static string GetRandomLinkName()
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(_adjectives[_rng.Next(0,_adjectives.Length)]) +
                   CultureInfo.InvariantCulture.TextInfo.ToTitleCase(_adjectives[_rng.Next(0,_adjectives.Length)]) +
                   CultureInfo.InvariantCulture.TextInfo.ToTitleCase(_animals[_rng.Next(0,_animals.Length)]);
        }
    }
}