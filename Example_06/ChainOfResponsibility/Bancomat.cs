using System;
using System.Collections;

namespace Example_06.ChainOfResponsibility
{
    public enum CurrencyType
    {
        Eur,
        Dollar,
        Ruble
    }

    public interface IBanknote
    {
        CurrencyType Currency { get; }
        string Value { get; }
    } 

    public class Bancomat
    {
        private readonly BanknoteHandler _handler;

        public Bancomat()
        {
            _handler = new TenRubleHandler(null);
            _handler = new TenDollarHandler(_handler);
            _handler = new FiftyDollarHandler(_handler);
            _handler = new HundredDollarHandler(_handler);
        }

        public bool Validate(string banknote)
        {
            return _handler.Validate(banknote);
        }

        public bool CashOut(string sum)
        {
            return _handler.CashOut(sum);
        }
    }

    public abstract class BanknoteHandler
    {
        private readonly BanknoteHandler _nextHandler;

        protected BanknoteHandler(BanknoteHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public virtual bool Validate(string banknote)
        {
            return _nextHandler != null && _nextHandler.Validate(banknote);
        }

        public virtual bool CashOut(string sum)
        {
            return _nextHandler != null && _nextHandler.CashOut(sum);
        }

        protected abstract int Value { get; }
        protected abstract string Currency { get; }
    }

    public abstract class CurrencyHandlerBase : BanknoteHandler
    {
        public override bool Validate(string banknote)
        {
            if (banknote.Equals($"{Value}{Currency}"))
            {
                return true;
            }
            return base.Validate(banknote);
        }

        public override bool CashOut(string sum)
        {
            if (sum.Contains(Currency))
            {
                int x = int.Parse(sum.Substring(0, sum.IndexOf(Currency)));
                if (x >= Value)
                {
                    Console.Write($"+ {Value}");
                    return this.CashOut($"{x - Value}{Currency}");
                }
            }
            return base.CashOut(sum);
        }

        protected CurrencyHandlerBase(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public abstract class RubleHandlerBase : BanknoteHandler
    {
        protected override string Currency => " рублей";
        protected RubleHandlerBase(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class TenRubleHandler : RubleHandlerBase
    {
        protected override int Value => 10;

        public TenRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public abstract class DollarHandlerBase : BanknoteHandler
    {
        protected override string Currency => "$";

        protected DollarHandlerBase(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class HundredDollarHandler : DollarHandlerBase
    {
        protected override int Value => 100;

        public HundredDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class FiftyDollarHandler : DollarHandlerBase
    {
        protected override int Value => 50;

        public FiftyDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class TenDollarHandler : DollarHandlerBase
    {
        protected override int Value => 10;

        public TenDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }
}
