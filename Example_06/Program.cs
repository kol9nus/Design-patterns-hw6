using System;
using Example_06.ChainOfResponsibility;

namespace Example_06
{
    class Program
    {
        static void Main(string[] args)
        {
            Bancomat bancomat = new Bancomat();
            bancomat.Validate("10 рублей");
            bancomat.CashOut("9032 рублей");
            bancomat.CashOut("90 рублей");
            bancomat.CashOut("9032$");
            bancomat.CashOut("9070$");

            Console.ReadKey();
        }
    }
}
