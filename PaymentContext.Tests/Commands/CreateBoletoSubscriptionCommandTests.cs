using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
	[TestClass]
	public class CreateBoletoSubscriptionCommandTests
	{
		// Red, Green, Refactor

		[TestMethod]
		public void ShouldReturnErrorWhenNameIsInvalid(string cnpj)
		{
			var command = new CreateBoletoSubscriptionCommand();
			command.FirstName = "";

			command.Validate();
		}
	}
}
