using System;
using System.Collections.Generic;

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
            return token == "+" || token == "-" || token == "*" || token == "/" || token == "^";
        }

        private double ApplyOperator(double leftOperand, double rightOperand, string op)
        {
            switch (op)
            {
                case "+": return leftOperand + rightOperand;
                case "-": return leftOperand - rightOperand;
                case "*": return leftOperand * rightOperand;
                case "/": return leftOperand / rightOperand;
                case "^": return Math.Pow(leftOperand, rightOperand);
                default: throw new Exception("Invalid operator.");
            }
        }
    }
}

public static void Main()
{
    var calculator = new RPNCalculator();
    var result = calculator.Calculate("3 4 + 2 * 1 +");
    Console.WriteLine(result);  // Output: 15
}


