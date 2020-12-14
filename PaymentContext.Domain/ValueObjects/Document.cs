using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
	public class Document : ValueObject
	{
		public Document(string number, EDocumentType type)
		{
			Number = number;
			Type = type;

			AddNotifications(new Contract()
				.Requires()
				.IsTrue(Validate(), "Document.Number", "Documento inválido")
			);
		}

		public string Number { get; private set; }
		public EDocumentType Type { get; private set; }

		private bool Validate()
		{
			if (Type == EDocumentType.CNPJ)
				return IsCnpj();
			if (Type == EDocumentType.CPF && Number.Length == 11)
				return IsCpf();

			return false;
		}

		private bool IsCnpj()
		{
			int[] multiplierOne = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplierTwo = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			int sum;
			int rest;
			string digit;
			string tempCnpj;
			Number = Number.Trim();
			Number = Number.Replace(".", "").Replace("-", "").Replace("/", "");
			if (Number.Length != 14 || Number.Count(s => s == char.Parse(Number.Substring(0, 1))) == 14)
				return false;
			tempCnpj = Number.Substring(0, 12);
			sum = 0;
			for (int i = 0; i < 12; i++)
				sum += int.Parse(tempCnpj[i].ToString()) * multiplierOne[i];
			rest = (sum % 11);
			if (rest < 2)
				rest = 0;
			else
				rest = 11 - rest;
			digit = rest.ToString();
			tempCnpj = tempCnpj + digit;
			sum = 0;
			for (int i = 0; i < 13; i++)
				sum += int.Parse(tempCnpj[i].ToString()) * multiplierTwo[i];
			rest = (sum % 11);
			if (rest < 2)
				rest = 0;
			else
				rest = 11 - rest;
			digit = digit + rest.ToString();
			return Number.EndsWith(digit);
		}

		private bool IsCpf()
		{
			int[] multiplierOne = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplierTwo = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			string tempCpf;
			string digit;
			int sum;
			int rest;
			Number = Number.Trim();
			Number = Number.Replace(".", "").Replace("-", "");
			if (Number.Length != 11 || Number.Count(s => s == char.Parse(Number.Substring(0, 1))) == 11)
				return false;
			tempCpf = Number.Substring(0, 9);
			sum = 0;

			for (int i = 0; i < 9; i++)
				sum += int.Parse(tempCpf[i].ToString()) * multiplierOne[i];
			rest = sum % 11;
			if (rest < 2)
				rest = 0;
			else
				rest = 11 - rest;
			digit = rest.ToString();
			tempCpf = tempCpf + digit;
			sum = 0;
			for (int i = 0; i < 10; i++)
				sum += int.Parse(tempCpf[i].ToString()) * multiplierTwo[i];
			rest = sum % 11;
			if (rest < 2)
				rest = 0;
			else
				rest = 11 - rest;
			digit = digit + rest.ToString();
			return Number.EndsWith(digit);
		}
	}
}