using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
	public class CreditCardPayment : Payment
	{
		public CreditCardPayment(string cardHolderNamer, string cardNumber, string lastTransactionNumber,
			DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string payer, Document document, Email email, Address address)
			: base(paidDate, expireDate, total, totalPaid, payer, document, email, address)
		{
			CardHolderNamer = cardHolderNamer;
			CardNumber = cardNumber;
			LastTransactionNumber = lastTransactionNumber;
		}

		public string CardHolderNamer { get; private set; }
		public string CardNumber { get; private set; }
		public string LastTransactionNumber { get; private set; }
	}
}