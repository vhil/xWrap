namespace Xwrap.FieldWrappers
{
	using System;
	using Abstractions;

	/// <summary>
	/// LazyLoadField
	/// </summary>
	/// <typeparam name="TFieldWrapper"></typeparam>
	public class LazyLoadField<TFieldWrapper> where TFieldWrapper : IFieldWrapper
	{
		private readonly Func<TFieldWrapper> getFieldDelegate;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="getFieldDelegate"></param>
		public LazyLoadField(Func<TFieldWrapper> getFieldDelegate)
		{
			this.getFieldDelegate = getFieldDelegate;
		}

		protected TFieldWrapper fieldWrapper = default(TFieldWrapper);

		/// <summary>
		/// 
		/// </summary>
		public TFieldWrapper Value
		{
			get
			{
				if (Equals(this.fieldWrapper, default(TFieldWrapper)))
				{
					this.fieldWrapper = this.getFieldDelegate();
				}

				return this.fieldWrapper;
			}
		}

		/// <summary>
		/// implicit cast
		/// </summary>
		/// <param name="lazyLoadWrapper"></param>
		public static implicit operator TFieldWrapper(LazyLoadField<TFieldWrapper> lazyLoadWrapper)
		{
			return lazyLoadWrapper.Value;
		}
	}
}
