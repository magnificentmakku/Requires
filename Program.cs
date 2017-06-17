namespace Requires
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Action<object> requiresNotNull = (object o) =>
            {
                Requires.NotNull(() => o);
            };

            requiresNotNull(new object());
            try
            {
                requiresNotNull(null);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(string.Format("Requires.NotNull(...) threw ArgumentNullException.\nMessage: {0}\nParamName: {1}", ex.Message, ex.ParamName));
            }

            Action<int, int> requiresGreaterThan = (int x, int y) =>
            {
                Requires.Require(() => x).GreaterThan(() => y);
            };

            requiresGreaterThan(3, 2);
            try
            {
                requiresGreaterThan(3, 42);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(string.Format("Requires.GreaterThan(...) threw ArgumentOutOfRangeException.\nMessage: {0}\nParamName: {1}", ex.Message, ex.ParamName));
            }

            Action<int, int, int> requiresGreaterThanAndLessThan = (int x, int y, int z) =>
            {
                Requires.Require(() => x).GreaterThan(() => y).AndLessThan(() => z);
            };
            requiresGreaterThanAndLessThan(3,2,4);
            try
            {
                requiresGreaterThanAndLessThan(3,2,1);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(string.Format("Requires.AndLessThan(...) threw ArgumentOutOfRangeException.\nMessage: {0}\nParamName: {1}", ex.Message, ex.ParamName));
            }

            Console.ReadKey();
        }

    }
}
