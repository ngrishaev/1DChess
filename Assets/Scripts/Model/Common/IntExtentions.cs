namespace Common
{
    public static class IntExtensions
    {
        public static bool Odd(this int number)
            => number.Even() == false;

        public static bool Even(this int number)
            => number % 2 == 0;
    }
}