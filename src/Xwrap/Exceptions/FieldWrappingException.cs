namespace Xwrap.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	[Serializable]
	public class FieldWrappingException : Exception
	{
		public FieldWrappingException()
		{
		}

		public FieldWrappingException(string message) : base(message)
		{
		}

		public FieldWrappingException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected FieldWrappingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
