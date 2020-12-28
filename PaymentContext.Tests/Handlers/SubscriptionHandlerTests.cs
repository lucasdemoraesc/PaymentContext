using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests
{
	[TestClass]
	public class SubscriptionHandlerTests
	{
		// Red, Green, Refactor

		[TestMethod]
		public void ShouldReturnErrorWhenDocumentExists()
		{
			var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
			var command = new CreateBoletoSubscriptionCommand();

		command.FirstName = "Bruce";
		command.LastName = "Wayne";
		command.Document = "99999999999";
		command.Email = "test@email.com2";

		command.BarCode = "136456546159";
		command.BoletoNumber = "8848";

		command.PaymentNumber = "1";
		command.PaidDate = DateTime.Now;
		command.ExpireDate = DateTime.Now.AddMonths(1);
		command.Total = 64;
		command.TotalPaid = 64;
		command.Payer = "WAYNE CORP";
		command.PayerDocument = "123146546987";
		command.PayerDocumentType = EDocumentType.CPF;
		command.PayerEmail = "batman@dc.com";
		command.Street = "test";
		command.Number = "test";
		command.Neighborhood = "test";
		command.City = "test";
		command.State = "test";
		command.Country = "test";
		command.ZipCode = "1345664";

		handler.Handle(command);
		Assert.AreEqual(false, handler.Valid);
	}
}
}
