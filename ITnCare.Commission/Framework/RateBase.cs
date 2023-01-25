using ITnCare.Commission.Enums;

namespace ITnCare.Commission.Framework
{
	/// <summary>
	/// Commission Rate Base Structure
	/// </summary>
	public abstract class RateBase : ICommission
	{
		protected readonly List<FeeItem> _feeItems = new List<FeeItem>();
		protected readonly List<TaxItem> _taxItems = new List<TaxItem>();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		public virtual Models.Commission Calculate(OrderSideEnum orderSide, decimal price, long quantity, CustomerOriginEnum customerOrigin)
		{
			return Calculate(orderSide, (price * quantity), customerOrigin);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="orderSide"></param>
		/// <param name="amount"></param>
		/// <param name="customerOrigin"></param>
		/// <returns></returns>
		public virtual Models.Commission Calculate(OrderSideEnum orderSide, decimal amount, CustomerOriginEnum customerOrigin)
		{
			//
			// creating set of tax and fee sets based on OrderSide
			var ts = _taxItems.Where(o => o.OrderSide == orderSide).ToList();
			var fs = _feeItems.Where(o => o.OrderSide == orderSide).ToList();

			//
			// calculating tax summation
			var tax = (decimal)0;
			foreach (var ti in ts)
			{
				tax += (decimal)ti.Percent * amount;
			}

			//
			// calculating fee summation (considering maximum value)
			var fee = (decimal)0;
			foreach (var fi in fs)
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
