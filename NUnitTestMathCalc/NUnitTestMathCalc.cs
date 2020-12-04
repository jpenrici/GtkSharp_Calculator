using NUnit.Framework;
using System;

namespace TestMathCalc
{
    [TestFixture]
    public class Test
    {
        MathCalc.Calculator t = new MathCalc.Calculator();

        [Test]
        public void Error()
        {
            Assert.AreEqual(t.ERROR, "ERROR");
        }

        [Test]
        public void IsNullOrEmpty()
        {
            // Nulo ou vazio
            Assert.AreEqual(true, t.IsNullOrEmpty(""));
            Assert.AreEqual(true, t.IsNullOrEmpty(" "));
            Assert.AreEqual(true, t.IsNullOrEmpty(null));
            // Preenchido com string
            Assert.AreEqual(false, t.IsNullOrEmpty("a"));
            Assert.AreEqual(false, t.IsNullOrEmpty("-"));
            Assert.AreEqual(false, t.IsNullOrEmpty("0"));
            Assert.AreEqual(false, t.IsNullOrEmpty("-1"));
            Assert.AreEqual(false, t.IsNullOrEmpty("1+1"));
            Assert.AreEqual(false, t.IsNullOrEmpty("(10+10.1)"));
            // Preenchido por conversão
            Assert.AreEqual(false, t.IsNullOrEmpty(0));           // valor inteiro
            Assert.AreEqual(false, t.IsNullOrEmpty(-1));          // valor negativo
            Assert.AreEqual(false, t.IsNullOrEmpty(10.1));        // valor fracionado
            Assert.AreEqual(false, t.IsNullOrEmpty(10 + 10.1));   // cálculo            
        }

        [Test]
        public void Numbers()
        {
            // Não numéricos
            Assert.AreEqual(false, t.IsNumber(""));
            Assert.AreEqual(false, t.IsNumber(" "));
            Assert.AreEqual(false, t.IsNumber(null));
            Assert.AreEqual(false, t.IsNumber("A"));
            Assert.AreEqual(false, t.IsNumber("-"));
            Assert.AreEqual(false, t.IsNumber("+"));
            Assert.AreEqual(false, t.IsNumber(","));
            Assert.AreEqual(false, t.IsNumber("-,"));
            Assert.AreEqual(false, t.IsNumber("(10+10.1)"));     // expressão
            // Numéricos com erros
            Assert.AreEqual(false, t.IsNumber("0.1"));           // separador decimal != ','
            Assert.AreEqual(false, t.IsNumber(".1"));
            Assert.AreEqual(false, t.IsNumber("1."));
            Assert.AreEqual(false, t.IsNumber(",1"));            // erro digitação
            Assert.AreEqual(false, t.IsNumber("1,"));
            Assert.AreEqual(false, t.IsNumber("-1,"));
            Assert.AreEqual(false, t.IsNumber("-,1"));
            Assert.AreEqual(false, t.IsNumber("0,,1"));
            Assert.AreEqual(false, t.IsNumber("0,0,1"));
            Assert.AreEqual(false, t.IsNumber(",,1"));
            Assert.AreEqual(false, t.IsNumber("1 "));
            Assert.AreEqual(false, t.IsNumber(" 1"));
            Assert.AreEqual(false, t.IsNumber("1A"));
            Assert.AreEqual(false, t.IsNumber("0,A"));
            // String numéricas
            Assert.AreEqual(true, t.IsNumber("0"));
            Assert.AreEqual(true, t.IsNumber("1"));
            Assert.AreEqual(true, t.IsNumber("-1"));
            Assert.AreEqual(true, t.IsNumber("+1"));
            Assert.AreEqual(true, t.IsNumber("10"));
            Assert.AreEqual(true, t.IsNumber("0,1"));
            // Numéricos
            Assert.AreEqual(true, t.IsNumber(-10));
            Assert.AreEqual(true, t.IsNumber(10.1));
            Assert.AreEqual(true, t.IsNumber(10 + 10.1));
        }

        [Test]
        public void Validate()
        {
            // Nulo ou vazio
            Assert.AreEqual(false, t.Validate(""));
            Assert.AreEqual(false, t.Validate(" "));
            Assert.AreEqual(false, t.Validate(null));
            // Caracteres inválidos
            Assert.AreEqual(false, t.Validate("a"));
            Assert.AreEqual(false, t.Validate("(0,1+-,2/5.)"));  // separador decimal != ','
            Assert.AreEqual(false, t.Validate("(01,23+-,4*5,6/7-8+9,)+A"));
            // Parênteses sem par
            Assert.AreEqual(false, t.Validate("(((1+2))"));      // '(' > ')'
            Assert.AreEqual(false, t.Validate("(((01,23+-,4*5,6/7-8+9,))))"));
            // Somente caracteres válidos
            Assert.AreEqual(true, t.Validate("0"));
            Assert.AreEqual(true, t.Validate("-"));
            Assert.AreEqual(true, t.Validate("-1"));
            Assert.AreEqual(true, t.Validate("0+1"));
            Assert.AreEqual(true, t.Validate("+(-1)"));
            Assert.AreEqual(true, t.Validate("+ ( - 1 )"));
        }

        [Test]
        public void Prepare()
        {
            // Soma de número negativo '+-'
            Assert.AreEqual("1-1", t.Prepare("1+-1"));
            // Operador '+' junto ao '-'
            Assert.AreEqual("0-1", t.Prepare("+-1"));
            // Operador '+' no início
            Assert.AreEqual("0+1-2", t.Prepare("+1+-2"));
            // Operador '-' no início
            Assert.AreEqual("0-1-1", t.Prepare("-1-1"));
            // Multiplicação de número negativo '*-'
            Assert.AreEqual("1*(0-1)*1", t.Prepare("1*-1"));
            // Multiplicação de número positivo '*+'
            Assert.AreEqual("1*1", t.Prepare("1*+1"));
            // Divisão de número negativo '*-'
            Assert.AreEqual("1*(0-1)/1", t.Prepare("1/-1"));
            // Divisão de número negativo '*+'
            Assert.AreEqual("1/1", t.Prepare("1/+1"));
            // Operador '-' próximo a parênteses
            Assert.AreEqual("0-(0-(0-15*(0-1)*10)))", t.Prepare("-(-(-15*-10)))"));
        }

        [Test]
        public void PostFix()
        {
            //Inválido
            Assert.AreEqual("ERROR", t.PostFix(""));
            Assert.AreEqual("ERROR", t.PostFix(" "));
            Assert.AreEqual("ERROR", t.PostFix(null));
            Assert.AreEqual("ERROR", t.PostFix("expression"));
            //Número
            Assert.AreEqual("10", t.PostFix(10));                //valor inteiro
            Assert.AreEqual("0", t.PostFix("0"));
            Assert.AreEqual("0 1 -", t.PostFix(-1));             //valor negativo
            Assert.AreEqual("0 1 -", t.PostFix("-1"));
            Assert.AreEqual("0 0,1 -", t.PostFix("-0,1"));       //valor fracionado
            //Expressão
            Assert.AreEqual("0 1 - 1 +", t.PostFix("- 1 + 1"));  //espaço
            Assert.AreEqual("0 1 - 1 +", t.PostFix("-1+1"));
            Assert.AreEqual("0 1 + 2 -", t.PostFix("0+1-2"));
            Assert.AreEqual("1 2 + 3 +", t.PostFix("1+2+3"));
            Assert.AreEqual("1,1 1,1 +", t.PostFix("1,1+1,1"));
            Assert.AreEqual("1 1 1 + +", t.PostFix("1+(1+1)"));
            Assert.AreEqual("1 0,1 + 2 +", t.PostFix("(1+0,1)+2"));
            Assert.AreEqual("1 0,1 * 15 -", t.PostFix("(1*0,1)-15"));
            Assert.AreEqual("1 0,1 / 1 -", t.PostFix("(1/0,1)+-1"));
            Assert.AreEqual("1 0,1 / 0 1 - * 1 *", t.PostFix("(1/0,1)*-1"));
            Assert.AreEqual("1 0,1 / 0 1 - * 1 * 15 +", t.PostFix("(1/0,1)*-1+15"));
            Assert.AreEqual("1 0,1 / 0 1 - * 5 /", t.PostFix("(1/0,1)/-5"));
            Assert.AreEqual("1 0,1 / 0 1 - 1 + /", t.PostFix("(1/0,1)/(-1+1)"));
            Assert.AreEqual("1 1,1 - 0 5 - 1 + -", t.PostFix("((1+-1,1))+-((-5+1))"));
            Assert.AreEqual("0 0 1 - - 0 2 - 3 + -", t.PostFix("-(((-1)))+-(((-2+3)))"));
            Assert.AreEqual("0 0 1 - + 0 2 - 3 + -", t.PostFix("+(((-1)))+-(((-2+3)))"));
            Assert.AreEqual("1,0 4,0 +", t.PostFix("1,0+4,0"));
            Assert.AreEqual("1,0 4,0 + 2,0 + 3 +", t.PostFix("1,0+4,0+2,0+3"));
            Assert.AreEqual("5,0 1,0 -", t.PostFix("5,0-1,0"));
            Assert.AreEqual("5,0 2,0 - 2 -", t.PostFix("5,0-2,0-2"));
            Assert.AreEqual("5,0 2,0 *", t.PostFix("5,0*2,0"));
            Assert.AreEqual("5,0 2,0 * 2 *", t.PostFix("5,0*2,0*2"));
            Assert.AreEqual("10,0 2,0 /", t.PostFix("10,0/2,0"));
            Assert.AreEqual("10,0 2,0 / 2 / 10 /", t.PostFix("10,0/2,0/2/10"));
            Assert.AreEqual("10 10 + 5 2 * -", t.PostFix("(10+10)-(5*2)"));
            Assert.AreEqual("1,5 2,5 + 3 + 2,5 2,5 + -", t.PostFix("(1,5+2,5+3)-(2,5+2,5)"));
            Assert.AreEqual("5 0 1 - * 10 * 20 + 5 -", t.PostFix("5*-10+20-5"));
            Assert.AreEqual("2000 1 2 / +", t.PostFix("2000+1/2"));
            Assert.AreEqual("2 2 + 4 5 * + 1 1000 / +", t.PostFix("2+2+4*5+1/1000"));
        }

        // Inválido
        [Test]
        public void Resolve()
        {
            Assert.AreEqual("ERROR", t.Resolve(""));
            Assert.AreEqual("ERROR", t.Resolve(" "));
            Assert.AreEqual("ERROR", t.Resolve(null));
            Assert.AreEqual("ERROR", t.Resolve("A"));
            Assert.AreEqual("ERROR", t.Resolve(",1"));
            Assert.AreEqual("ERROR", t.Resolve("(1 + 2)"));  // input: infix, esperado: posfix
            // Número
            Assert.AreEqual("0", t.Resolve("0"));
            Assert.AreEqual("0", t.Resolve(" 0"));
            Assert.AreEqual("1,1", t.Resolve("1,1"));
            // Expressão
            Assert.AreEqual("0,25", t.Resolve("10,0 2,0 / 2 / 10 /"));
            Assert.AreEqual("10", t.Resolve("10 10 + 5 2 * -"));
            Assert.AreEqual("2", t.Resolve("1,5 2,5 + 3 + 2,5 2,5 + -"));
            Assert.AreEqual("-1", t.Resolve("0 1 -"));
            Assert.AreEqual("0", t.Resolve("0 1 - 1 +"));
            Assert.AreEqual("-1", t.Resolve("0 1 + 2 -"));
            Assert.AreEqual("6", t.Resolve("1 2 + 3 +"));
            Assert.AreEqual("2,2", t.Resolve("1,1 1,1 +"));
            Assert.AreEqual("3,1", t.Resolve("1 0,1 + 2 +"));
            Assert.AreEqual("-14,9", t.Resolve("1 0,1 * 15 -"));
            Assert.AreEqual("9", t.Resolve("1 0,1 / 1 -"));
            Assert.AreEqual("-10", t.Resolve("1 0,1 / 0 1 - * 1 *"));
            Assert.AreEqual("5", t.Resolve("1 0,1 / 0 1 - * 1 * 15 +"));
            Assert.AreEqual("-2", t.Resolve("1 0,1 / 0 1 - * 5 /"));
            Assert.AreEqual("ERROR", t.Resolve("1 0,1 / 0 1 - 1 + /"));  // Divisão por zero
            Assert.AreEqual("3,9", t.Resolve("1 1,1 - 0 5 - 1 + -"));
            Assert.AreEqual("0", t.Resolve("0 0 1 - - 0 2 - 3 + -"));
            Assert.AreEqual("-2", t.Resolve("0 0 1 - + 0 2 - 3 + -"));
            Assert.AreEqual("-35", t.Resolve("5 0 1 - * 10 * 20 + 5 -"));
            Assert.AreEqual("2000,5", t.Resolve("2000 1 2 / +"));
            Assert.AreEqual("24,001", t.Resolve("2 2 + 4 5 * + 1 1000 / +"));
            Assert.AreEqual("2", t.Resolve("1,5 2,5 + 3 + 2,5 2,5 + -"));
            Assert.AreEqual("757", t.Resolve("100 200 + 2 / 5 * 7 +"));
            Assert.AreEqual("-4", t.Resolve("2 3 1 * + 9 -"));
            Assert.AreEqual("23", t.Resolve("10 2 8 * + 3 -"));
        }

        [Test]
        public void Calc()
        {
            Assert.AreEqual("0,0", t.ExpressionCalc("0"));
            Assert.AreEqual("1,0", t.ExpressionCalc("1"));
            Assert.AreEqual("-1,0", t.ExpressionCalc("-1"));
            Assert.AreEqual("0,0", t.ExpressionCalc("-1+1"));
            Assert.AreEqual("-1,0", t.ExpressionCalc("0+1-2"));
            Assert.AreEqual("6,0", t.ExpressionCalc("1+2+3"));
            Assert.AreEqual("2,2", t.ExpressionCalc("1,1+1,1"));
            Assert.AreEqual("3,1", t.ExpressionCalc("(1+0,1)+2"));
            Assert.AreEqual("-14,9", t.ExpressionCalc("(1*0,1)-15"));
            Assert.AreEqual("9,0", t.ExpressionCalc("(1/0,1)+-1"));
            Assert.AreEqual("-10,0", t.ExpressionCalc("(1/0,1)*-1"));
            Assert.AreEqual("-10,0", t.ExpressionCalc("(1/0,1)/-1"));
            Assert.AreEqual("ERROR", t.ExpressionCalc("(1/0,1)/(-1+1)"));
            Assert.AreEqual("3,9", t.ExpressionCalc("((1+-1,1))+-((-5+1))"));
            Assert.AreEqual("0,0", t.ExpressionCalc("-(((-1)))+-(((-2+3)))"));
            Assert.AreEqual("-35,0", t.ExpressionCalc("5*-10+20-5"));
            Assert.AreEqual("2000,5", t.ExpressionCalc("2000+1/2"));
            Assert.AreEqual("24,001", t.ExpressionCalc("2+2+4*5+1/1000"));
            Assert.AreEqual("23,0", t.ExpressionCalc("((10 + (2 * 8)) - 3)"));
        }

        [Test]
        public void Extra()
        {
            // Outro delimitador
            var postfix = t.PostFix("1+2*5,0+-1", '|');
            Assert.AreEqual(10, Double.Parse(t.Resolve(postfix, '|')));
        }
    }
}
