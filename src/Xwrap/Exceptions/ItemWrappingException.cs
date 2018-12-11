namespace Xwrap.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	/// <summary>
	/// Occurs if the item can not be wrapped into target item wrapper type.
	/// </summary>
	[Serializable]
	public class ItemWrappingException : Exception
	{
		public ItemWrappingException()
		{
		}

		public ItemWrappingException(string message) : base(message)
		{
		}

		public ItemWrappingException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ItemWrappingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}