using System.Collections.Generic;
using System.Linq;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
	public class Student : Entity
	{
		private IList<Subscription> _subscriptions;

		public Student(Name name, Document document, Email email)
		{
			Name = name;
			Document = document;
			Email = email;
			_subscriptions = new List<Subscription>();
		}

		public Name Name { get; set; }
		public Document Document { get; private set; }
		public Email Email { get; private set; }
		public Address Address { get; private set; }
		public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

		public void AddSubscription(Subscription subscription)
		{
			// Cancela as outras assinaturas, e define esta como padrão
			foreach (var sub in Subscriptions)
			{
				sub.Activate(false);
			}

			_subscriptions.Add(subscription);
		}
	}
}