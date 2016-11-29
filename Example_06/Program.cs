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
            bancomat.CashOut("100000 рублей");

            Console.ReadKey();
        }
    }
}
