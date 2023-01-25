using ITnCare.Commission.Enums;

namespace ITnCare.Commission.Framework
{
	/// <summary>
	/// <c>FeeItem</c> in <c>Commission</c> structure
	/// </summary>
	public class FeeItem : ItemBase
	{
		/// <summary>
		/// 
		/// </summary>
		public FeeKindEnum FeeKind { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public long Maximum { get; set; }
	}
}
