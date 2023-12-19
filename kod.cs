namespace RPNCalculator
{
    public class RPNCalculator
    {
        private Stack<string> stack = new Stack<string>();

        public double Calculate(string expression)
        {
            var tokens = expression.Split(' ');

            foreach (var token in tokens)
            {
                if (IsOperator(token))
                {
                    var rightOperand = double.Parse(stack.Pop());
                    var leftOperand = double.Parse(stack.Pop());
                    var result = ApplyOperator(leftOperand, rightOperand, token);
                    stack.Push(result.ToString());
                }
                else
                {
                    stack.Push(token);
                }
            }

            return double.Parse(stack.Pop());
        }

        private bool IsOperator(string token)
        {
            return token == "+" || token == "-";
        }

        private double ApplyOperator(double leftOperand, double rightOperand, string op)
        {
            switch (op)
            {
                case "+": return leftOperand + rightOperand;
                case "-": return leftOperand - rightOperand;
                default: throw new Exception("Invalid operator.");
            }
        }
        //wyskakuje mi bład "Input string was not in a correct format." wszystko sie zmienia jak wpisze dane talk jak na dole

        public static void Main()
        {
            var calculator = new RPNCalculator();
            var result = calculator.Calculate("3 4 2 + -");//-3
            Console.WriteLine(result);
        }


        /*public static void Main()
        {
            Console.WriteLine("podaj wyrazebnie do oblicznia");
            var calculator = new RPNCalculator();
            var dane = Console.ReadLine();
            var result = calculator.Calculate("$dane");
            Console.WriteLine(result);
        }*/
    }
}
