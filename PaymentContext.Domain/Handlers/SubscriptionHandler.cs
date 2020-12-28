using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Entities;
using System;
using PaymentContext.Domain.Services;

namespace PaymentContext.Domain.Handlers
{
	public class SubscriptionHandler : Notifiable,
		IHandler<CreateBoletoSubscriptionCommand>,
		IHandler<CreatePaypalSubscriptionCommand>,
		IHandler<CreateCreditCardSubscriptionCommand>
	{
		private readonly IStudentRepository _repository;
		private readonly IEmailService _emailService;

		public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
		{
			_repository = repository;
			_emailService = emailService;
		}

		public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
		{
			// Fail Fast Validations
			command.Validate();
			if (command.Invalid)
			{
				AddNotifications(command);
				return new CommandResult(false, "Não foi possível realizar sua assinatura");
			}

			// Verificar se Documento está cadastrado
			if (_repository.DocumentExists(command.Document))
				AddNotification("Document", "Este documento já está em uso");

			// Verificar se Email está cadastrado
			if (_repository.EmailExists(command.Email))
				AddNotification("Email", "Este email já está em uso");

			// Gerar VOs
			var name = new Name(command.FirstName, command.LastName);
			var document = new Document(command.Document, EDocumentType.CPF);
			var payerDocument = new Document(command.PayerDocument, command.PayerDocumentType);
			var email = new Email(command.Email);
			var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

			// Gerar Entidades
			var student = new Student(name, document, email);
			var subscription = new Subscription(DateTime.Now.AddMonths(1));
			var payment = new BoletoPayment(
				command.BarCode,
				command.BoletoNumber,
				command.PaidDate,
				command.ExpireDate,
				command.Total,
				command.TotalPaid,
				command.Payer,
				payerDocument,
				email,
				address);

			// Relacionamentos
			subscription.AddPayment(payment);
			student.AddSubscription(subscription);

			// Agrupar as validações
			AddNotifications(name, document, email, address, student, subscription, payment);

			// Salvar informações
			_repository.CreateSubscription(student);

			// Enviar email de boas vindas
			_emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao balta.io", "Sua assinatura foi criada");

			// Retornar informações
			return new CommandResult(true, "Assinatura realizada com sucesso");
		}

		public ICommandResult Handle(CreatePaypalSubscriptionCommand command)
		{
			// Fail Fast Validations
			command.Validate();
			if (command.Invalid)
			{
				AddNotifications(command);
				return new CommandResult(false, "Não foi possível realizar sua assinatura");
			}

			// Verificar se Documento está cadastrado
			if (_repository.DocumentExists(command.Document))
				AddNotification("Document", "Este documento já está em uso");

			// Verificar se Email está cadastrado
			if (_repository.EmailExists(command.Email))
				AddNotification("Email", "Este email já está em uso");

			// Gerar VOs
			var name = new Name(command.FirstName, command.LastName);
			var document = new Document(command.Document, EDocumentType.CPF);
			var payerDocument = new Document(command.PayerDocument, command.PayerDocumentType);
			var email = new Email(command.Email);
			var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

			// Gerar Entidades
			var student = new Student(name, document, email);
			var subscription = new Subscription(DateTime.Now.AddMonths(1));
			var payment = new PayPalPayment(
				command.TransactionCode,
				command.PaidDate,
				command.ExpireDate,
				command.Total,
				command.TotalPaid,
				command.Payer,
				payerDocument,
				email,
				address);

			// Relacionamentos
			subscription.AddPayment(payment);
			student.AddSubscription(subscription);

			// Agrupar as validações
			AddNotifications(name, document, email, address, student, subscription, payment);

			// Salvar informações
			_repository.CreateSubscription(student);

			// Enviar email de boas vindas
			_emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao balta.io", "Sua assinatura foi criada");

			// Retornar informações
			return new CommandResult(true, "Assinatura realizada com sucesso");
		}

		public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
		{
			// Fail Fast Validations
			command.Validate();
			if (command.Invalid)
			{
				AddNotifications(command);
				return new CommandResult(false, "Não foi possível realizar sua assinatura");
			}

			// Verificar se Documento está cadastrado
			if (_repository.DocumentExists(command.Document))
				AddNotification("Document", "Este documento já está em uso");

			// Verificar se Email está cadastrado
			if (_repository.EmailExists(command.Email))
				AddNotification("Email", "Este email já está em uso");

			// Gerar VOs
			var name = new Name(command.FirstName, command.LastName);
			var document = new Document(command.Document, EDocumentType.CPF);
			var payerDocument = new Document(command.PayerDocument, command.PayerDocumentType);
			var email = new Email(command.Email);
			var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

			// Gerar Entidades
			var student = new Student(name, document, email);
			var subscription = new Subscription(DateTime.Now.AddMonths(1));
			var payment = new CreditCardPayment(
				command.CardHolderNamer,
				command.CardNumber,
				command.LastTransactionNumber,
				command.PaidDate,
				command.ExpireDate,
				command.Total,
				command.TotalPaid,
				command.Payer,
				payerDocument,
				email,
				address);

			// Relacionamentos
			subscription.AddPayment(payment);
			student.AddSubscription(subscription);

			// Agrupar as validações
			AddNotifications(name, document, email, address, student, subscription, payment);

			// Salvar informações
			_repository.CreateSubscription(student);

			// Enviar email de boas vindas
			_emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao balta.io", "Sua assinatura foi criada");

			// Retornar informações
			return new CommandResult(true, "Assinatura realizada com sucesso");
		}
	}
}