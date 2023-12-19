using System;
using System.Collections.Generic;

namespace Kalkulator
{
  public class KalkulatorRPN
  {
      private Stos<string> stosOperatorow;
      private Kolejka<string> kolejkaWynikow;

      public KalkulatorRPN()
      {
          stosOperatorow = new Stos<string>(100);
          kolejkaWynikow = new Kolejka<string>(100);
      }

      public void ObliczWyrazenie()
      {
          Console.WriteLine("Wprowadź wyrażenie matematyczne:");
          string wyrazenie = Console.ReadLine();

          string[] tokeny = wyrazenie.Split(' ');

          foreach (string token in tokeny)
          {
              if (CzyJestLiczba(token))
              {
                kolejkaWynikow.Dodaj(token);
              }
              else if (CzyJestOperator(token))
              {
                ObsluzOperator(token);
              }
          }

          while (!stosOperatorow.CzyPusty())
          {
              kolejkaWynikow.Dodaj(stosOperatorow.Pop());
          }

          double wynik = ObliczWyrazenieRPN();

          Console.WriteLine($"Wynik: {wynik}");
      }

      private void ObsluzOperator(string operatorToken)
      {
          while (!stosOperatorow.CzyPusty() && CzyMaWyzszyAleJednakRownyPriorytet(stosOperatorow.Top(), operatorToken))
          {
              kolejkaWynikow.Dodaj(stosOperatorow.Pop());
          }

          stosOperatorow.Dodaj(operatorToken);
      }

      private double ObliczWyrazenieRPN()
      {
          Stos<double> stosOperandow = new Stos<double>(100);

          while (!kolejkaWynikow.CzyPusty())
          {
              string token = kolejkaWynikow.Pop();

              if (CzyJestLiczba(token))
              {
                stosOperandow.Dodaj(double.Parse(token));
              }
              else if (CzyJestOperator(token))
              {
                double operand2 = stosOperandow.Pop();
                double operand1 = stosOperandow.Pop();
                double wynik = WykonajOperacje(token, operand1, operand2);
                stosOperandow.Dodaj(wynik);
              }
          }

          if (stosOperandow.Rozmiar() != 1)
          {
              throw new Exception("Nieprawidłowe wyrażenie.");
          }

          return stosOperandow.Pop();
      }

      private double WykonajOperacje(string operatorToken, double operand1, double operand2)
      {
          switch (operatorToken)
          {
              case "+":
                return operand1 + operand2;
              case "-":
                return operand1 - operand2;
              default:
                throw new Exception("Nieprawidłowy operator.");
          }
      }

      private bool CzyJestLiczba(string token)
      {
          double liczba;
          return double.TryParse(token, out liczba);
      }

      private bool CzyJestOperator(string token)
      {
          return token == "+" || token == "-";
      }

      private bool CzyMaWyzszyAleJednakRownyPriorytet(string operator1, string operator2)
      {
          return false;
      }
  }

  public class Program
  {
      public static void Main()
      {
          KalkulatorRPN kalkulator = new KalkulatorRPN();
          kalkulator.ObliczWyrazenie();
      }
  }
}
