using System;
using System.Collections.Generic;

namespace Calculator
{
   public class RPNCalculator
   {
       private MyStack<string> operatorStack;
       private MyQueue<string> outputQueue;

       public RPNCalculator()
       {
           operatorStack = new MyStack<string>(100);
           outputQueue = new MyQueue<string>(100);
       }

       public void EvaluateExpression()
       {
           Console.WriteLine("Enter a mathematical expression:");
           string expression = Console.ReadLine();

           string[] tokens = expression.Split(' ');

           foreach (string token in tokens)
           {
               if (IsNumber(token))
               {
                  outputQueue.Push(token);
               }
               else if (IsOperator(token))
               {
                  HandleOperator(token);
               }
           }

           while (!operatorStack.IsEmpty())
           {
               outputQueue.Push(operatorStack.Pop());
           }

           double result = EvaluateRPNExpression();

           Console.WriteLine($"Result: {result}");
       }

       private void HandleOperator(string operatorToken)
       {
           while (!operatorStack.IsEmpty() && IsHigherOrEqualPriority(operatorStack.Top(), operatorToken))
           {
               outputQueue.Push(operatorStack.Pop());
           }

           operatorStack.Push(operatorToken);
       }

       private double EvaluateRPNExpression()
       {
           MyStack<double> operandStack = new MyStack<double>(100);

           while (!outputQueue.IsEmpty())
           {
               string token = outputQueue.Pop();

               if (IsNumber(token))
               {
                  operandStack.Push(double.Parse(token));
               }
               else if (IsOperator(token))
               {
                  double operand2 = operandStack.Pop();
                  double operand1 = operandStack.Pop();
                  double result = PerformOperation(token, operand1, operand2);
                  operandStack.Push(result);
               }
           }

           if (operandStack.Size() != 1)
           {
               throw new Exception("Invalid expression.");
           }

           return operandStack.Pop();
       }

       private double PerformOperation(string operatorToken, double operand1, double operand2)
       {
           switch (operatorToken)
           {
               case "+":
                  return operand1 + operand2;
               case "-":
                  return operand1 - operand2;
               default:
                  throw new Exception("Invalid operator.");
           }
       }

       private bool IsNumber(string token)
       {
           double number;
           return double.TryParse(token, out number);
       }

       private bool IsOperator(string token)
       {
           return token == "+" || token == "-";
       }

       private bool IsHigherOrEqualPriority(string operator1, string operator2)
       {
           return false;
       }
   }

   public class Program
   {
       public static void Main()
       {
           RPNCalculator calculator = new RPNCalculator();
           calculator.EvaluateExpression();
       }
   }
}
