using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
	[TestClass]
	public class DocumentTests
	{
		// Red, Green, Refactor

		[TestMethod]
		[DataTestMethod]
		[DataRow("045136684156")]
		[DataRow("00000000000000")]
		[DataRow("35038204004586666")]
		[DataRow("99999999999999")]
		[DataRow("35038204000103")]
		public void ShouldReturnErrorWhenCNPJIsInvalid(string cnpj)
		{
			var doc = new Document(cnpj, EDocumentType.CNPJ);
			Assert.IsTrue(doc.Invalid);
		}

		[TestMethod]
		[DataTestMethod]
		[DataRow("34110468000150")]
		[DataRow("40724323000142")]
		[DataRow("35038204000104")]
		[DataRow("16690562000144")]
		[DataRow("97801626000139")]
		public void ShouldReturnSuccessWhenCNPJIsValid(string cnpj)
		{
			var doc = new Document(cnpj, EDocumentType.CNPJ);
			Assert.IsTrue(doc.Valid);
		}

		[TestMethod]
		[DataTestMethod]
		[DataRow("00000000000")]
		[DataRow("99999999999")]
		[DataRow("724643650")]
		[DataRow("339062880050")]
		[DataRow("69427694016")]
		public void ShouldReturnErrorWhenCPFIsInvalid(string cpf)
		{
			var doc = new Document(cpf, EDocumentType.CPF);
			Assert.IsTrue(doc.Invalid);
		}

		[TestMethod]
		[DataTestMethod]
		[DataRow("80368668037")]
		[DataRow("69418245060")]
		[DataRow("72464365025")]
		[DataRow("33906288005")]
		[DataRow("69427694017")]
		public void ShouldReturnSuccessWhenCPFIsValid(string cpf)
		{
			var doc = new Document(cpf, EDocumentType.CPF);
			Assert.IsTrue(doc.Valid);
		}
	}
}
