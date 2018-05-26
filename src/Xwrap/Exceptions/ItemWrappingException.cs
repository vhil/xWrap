namespace Xwrap.Exceptions
{
	using System;
	using System.Runtime.Serialization;

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