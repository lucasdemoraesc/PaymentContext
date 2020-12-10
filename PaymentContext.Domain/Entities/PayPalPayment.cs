using System;

namespace PaymentContext.Domain.Entities
{
	public class PayPalPayment : Payment
	{
		public PayPalPayment(string lastTransactionCode,
			DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string payer, string document, string address, string email)
			: base(paidDate, expireDate, total, totalPaid, payer, document, address, email)
		{
			LastTransactionCode = lastTransactionCode;
		}

		public string LastTransactionCode { get; private set; }
	}
}