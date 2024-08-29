namespace CodeSmellsRefactorExercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var calculator = new Calculator();
            calculator.Run();
        }
    }

    public class Calculator
    {
        private List<int> _numbers = new List<int>();

        public void Run()
        {
            // runtime error handling
            try
            {
                _PopulateIntList(_GetUserInput());
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Empty input was entered. Aborting.");
            }
            try
            {
                int sum = _GetSum();
                int average = _GetAverage();

                // made conditional more readable with context
                if (_IsAboveZero(sum) && _IsAboveZero(average) && _numbers.Any())
                {
                    Console.WriteLine($"The sum is: {sum}");
                    Console.WriteLine($"The rounded average is: {average}");
                }
                else
                {
                    Console.WriteLine("Both the sum and the average are 0");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("A non-integer input was entered. Aborting.");
            }
        }

        private string _GetUserInput()
        {
            Console.WriteLine("Enter integers separated by spaces:");
            string? userInput = Console.ReadLine();
            if (string.IsNullOrEmpty(userInput))
            {
                throw new ArgumentException("Empty input was entered.");
            }
            return userInput;
        }

        private void _PopulateIntList(string input)
        {
            foreach (string subString in input.Split(' '))
            {
                _numbers.Add(int.Parse(subString));
            }
        }

        private int _GetSum()
        {
            // apply no transformations to each number as the sum accumulates it
            return _numbers.Sum(number => number);
        }

        private int _GetAverage()
        {
            return _GetSum() / _numbers.Count;
        }

        private bool _IsAboveZero(int number)
        { 
            return number > 0 ? true : false;
        }


        /********************************** OLD **********************************/




        // long method
        public void OldRun()
        {
            Console.WriteLine("Enter numbers separated by spaces:");
            var input = Console.ReadLine();

            // lack of error handling: just complains and returns, no error thrown
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("No input provided.");
                return;
            }

            // duplicate code: getting average involves getting sum

            var numList = input.Split(' ');
            // inconsistent naming: n vs. number
            foreach (var n in numList)
            {
                _numbers.Add(int.Parse(n));
            }

            var sum = 0;
            // inconsistent naming: n vs. number
            foreach (var number in _numbers)
            {
                sum += number;
            }

            var average = sum / _numbers.Count;

            // conditional complexity & magic numbers: extract this/these into a validation method/s
            if (_numbers.Count > 0 && sum > 0 && average > 0)
            {
                Console.WriteLine($"Sum: {sum}");
                Console.WriteLine($"Average: {average}");
            }

            Console.WriteLine("Program finished.");
        }
    }
}
