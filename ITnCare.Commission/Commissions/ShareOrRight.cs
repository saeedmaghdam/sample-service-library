using ITnCare.Commission.Enums;
using ITnCare.Commission.Framework;

namespace ITnCare.Commission.Commissions
{
    public class ShareOrRight : RateBase
    {
        IReadOnlyDictionary<OrderSideEnum, IReadOnlyCollection<TaxItem>> _taxItemsDictionary;
        IReadOnlyDictionary<OrderSideEnum, IReadOnlyCollection<FeeItem>> _feeItemsDictionary;

        public TradeKindEnum FeeKind { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kind"></param>
        public ShareOrRight(TradeKindEnum kind)
        {
            FeeKind = kind;

            //
            // tax items
            _taxItems.Add(new TaxItem { OrderSide = OrderSideEnum.Bid, Percent = 0.0 });
            _taxItems.Add(new TaxItem { OrderSide = OrderSideEnum.Ask, Percent = 0.005 });

            //
            // fee items
            _feeItems.Add(new FeeItem { FeeKind = FeeKindEnum.Brokers, OrderSide = OrderSideEnum.Bid, Percent = 0.003040, Maximum = 300000000 });
            _feeItems.Add(new FeeItem { FeeKind = FeeKindEnum.Brokers, OrderSide = OrderSideEnum.Ask, Percent = 0.003040, Maximum = 300000000 });
            _feeItems.Add(new FeeItem { FeeKind = FeeKindEnum.CSDI, OrderSide = OrderSideEnum.Bid, Percent = 0.000096, Maximum = 160000000 });
            _feeItems.Add(new FeeItem { FeeKind = FeeKindEnum.CSDI, OrderSide = OrderSideEnum.Ask, Percent = 0.000144, Maximum = 240000000 });
            _feeItems.Add(new FeeItem { FeeKind = FeeKindEnum.BTMC, OrderSide = OrderSideEnum.Bid, Percent = 0.000080, Maximum = 80000000 });
            _feeItems.Add(new FeeItem { FeeKind = FeeKindEnum.BTMC, OrderSide = OrderSideEnum.Ask, Percent = 0.000120, Maximum = 120000000 });
            _feeItems.Add(new FeeItem { FeeKind = FeeKindEnum.SEOS, OrderSide = OrderSideEnum.Bid, Percent = 0.000240, Maximum = 100000000 });
            _feeItems.Add(new FeeItem { FeeKind = FeeKindEnum.SEOS, OrderSide = OrderSideEnum.Ask, Percent = 0.000240, Maximum = 100000000 });

            //
            // fee items
            if (FeeKind == TradeKindEnum.Retail)
            {
                _feeItems.Add(new FeeItem { FeeKind = FeeKindEnum.Bourses, OrderSide = OrderSideEnum.Bid, Percent = 0.000256, Maximum = 300000000 });
                _feeItems.Add(new FeeItem { FeeKind = FeeKindEnum.Bourses, OrderSide = OrderSideEnum.Ask, Percent = 0.000256, Maximum = 300000000 });
            }
            else
            {
                _feeItems.Add(new FeeItem { FeeKind = FeeKindEnum.Bourses, OrderSide = OrderSideEnum.Bid, Percent = 0.000256, Maximum = 500000000 });
                _feeItems.Add(new FeeItem { FeeKind = FeeKindEnum.Bourses, OrderSide = OrderSideEnum.Ask, Percent = 0.000256, Maximum = 500000000 });
            }

            var taxItemsDictionary = new Dictionary<OrderSideEnum, IReadOnlyCollection<TaxItem>>();
            taxItemsDictionary[OrderSideEnum.Bid] = _taxItems.Where(o => o.OrderSide == OrderSideEnum.Bid).ToList();
            taxItemsDictionary[OrderSideEnum.Ask] = _taxItems.Where(o => o.OrderSide == OrderSideEnum.Ask).ToList();
            _taxItemsDictionary = taxItemsDictionary;

            var feeItemDictionary = new Dictionary<OrderSideEnum, IReadOnlyCollection<FeeItem>>();
            feeItemDictionary[OrderSideEnum.Bid] = _feeItems.Where(o => o.OrderSide == OrderSideEnum.Bid).ToList();
            feeItemDictionary[OrderSideEnum.Ask] = _feeItems.Where(o => o.OrderSide == OrderSideEnum.Ask).ToList();
            _feeItemsDictionary = feeItemDictionary;
        }

        public override Models.Commission Calculate(OrderSideEnum orderSide, decimal amount, CustomerOriginEnum customerOrigin)
        {
            //
            // calculating tax summation
            var tax = (decimal)0;
            foreach (var ti in _taxItemsDictionary[orderSide])
                tax += (decimal)ti.Percent * amount;

            //
            // calculating fee summation (considering maximum value)
            var fee = (decimal)0;
            foreach (var fi in _feeItemsDictionary[orderSide])
            {
                var foo = (decimal)fi.Percent * amount;
                if (foo > fi.Maximum)
                {
                    fee += fi.Maximum;
                }
                else
                {
                    fee += foo;
                }
            }

            return new Models.Commission(fee, tax);
        }
    }
}
