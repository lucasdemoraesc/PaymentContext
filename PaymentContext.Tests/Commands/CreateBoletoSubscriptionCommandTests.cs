using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests
{
	[TestClass]
	public class CreateBoletoSubscriptionCommandTests
	{
		// Red, Green, Refactor

		[TestMethod]
		public void ShouldReturnErrorWhenNameIsInvalid()
		{
			var command = new CreateBoletoSubscriptionCommand();
			command.FirstName = "test";

			command.Validate();
			Assert.AreEqual(false, command.Valid);
		}
	}
}
