using System;
using System.Globalization;
using System.IO;

namespace Sylveon.S3Shortener.Utilities
{
    public class RandomNameGenerator
    {
        private readonly Random _random = new Random();
        private readonly string[] _adjectives;
        private readonly string[] _animals;

        public RandomNameGenerator(string folder)
        {
            _adjectives = File.ReadAllLines(Path.Combine(folder, "adjectives.txt"));
            _animals = File.ReadAllLines(Path.Combine(folder, "animals.txt"));
        }

        public string GetRandomLinkName()
        {
            return
                CultureInfo.InvariantCulture.TextInfo.ToTitleCase(_adjectives[_random.Next(0,_adjectives.Length)]) +
                CultureInfo.InvariantCulture.TextInfo.ToTitleCase(_adjectives[_random.Next(0,_adjectives.Length)]) +
                CultureInfo.InvariantCulture.TextInfo.ToTitleCase(_animals[_random.Next(0,_animals.Length)]);
        }
    }
}