using System;
using MathCalc;

namespace CalculatorGui
{
    public class Calc
    {
        public Calc(string expression)
        {
            Calculator c = new Calculator();

            // Vazios
            expression = expression.Replace(Environment.NewLine, String.Empty);
            expression = expression.Replace(Calculator.WSPACE, String.Empty);

            // Armazenar
            Formula = expression;

            // Outros parênteses
            expression = expression.Replace('{', Calculator.LPARENTHESES);
            expression = expression.Replace('}', Calculator.RPARENTHESES);
            expression = expression.Replace('[', Calculator.LPARENTHESES);
            expression = expression.Replace(']', Calculator.RPARENTHESES);

            // Calcular
            Result = c.ExpressionCalc(expression);

            // Mensagem
            Message = "Click Clear to enter a new expression.";
            if (Result.Equals("ERROR"))
                Message = "Check the expression and try again.";
        }

        public string Formula { get; private set; }
        public string Result { get; private set; }
        public string Message { get; private set; }
    }
}
