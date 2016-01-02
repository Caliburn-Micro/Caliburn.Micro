using System;

namespace Features.CrossPlatform
{
    public static class Lipsum
    {
        private const string Base = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent ullamcorper ligula quis nisl varius, vitae tincidunt libero dapibus. Nunc vel nunc elit. Etiam aliquet nec ligula eget pretium. Ut non arcu sem. Fusce et auctor dui. Mauris in lorem sit amet massa varius finibus et at metus. Aliquam non mi non justo malesuada suscipit. Etiam tincidunt ullamcorper sodales. Suspendisse potenti. Etiam ut fringilla risus. Cras varius neque metus, ac laoreet mauris commodo a.";
        private static readonly Random Random = new Random();

        public static string Get(int? length = null)
        {
            return Base.Substring(0, Random.Next(length ?? Base.Length));
        }
    }
}
