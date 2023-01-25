using ITnCare.Commission.Enums;

namespace ITnCare.Commission.Framework
{
	/// <summary>
	/// 
	/// </summary>
	public class ItemBase
	{
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public OrderSideEnum OrderSide { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public double Percent { get; set; }
	}
}
