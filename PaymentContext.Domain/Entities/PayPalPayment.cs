using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
	public class PayPalPayment : Payment
	{
		public PayPalPayment(string lastTransactionCode,
			DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string payer, Document document, Email email, Address address)
			: base(paidDate, expireDate, total, totalPaid, payer, document, email, address)
		{
			LastTransactionCode = lastTransactionCode;
		}

		public string LastTransactionCode { get; private set; }
	}
}