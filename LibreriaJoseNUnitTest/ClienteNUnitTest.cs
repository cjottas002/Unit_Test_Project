using System;
using LibreriaJose;

namespace LibreriaJoseNUnitTest
{
	[TestFixture]
	public class ClienteNUnitTest
	{
		private Cliente client;

		[SetUp]		
		public void Setup() 
        {
			client = new Cliente();
        
        }

		[Test]
		public void CrearNombreCompleto_InputNombreApellido_ReturnNombreCompleto()
		{
			// arrange
			

			// act
			client.CrearNombreCompleto("Jose", "Luis");
			
			//Esto sirve para que lance todos los errores que existen en los asserts y no se detenga en el primero
			//Assert.Multiple(() =>
			//{
   //             // asserts
   //             Assert.That(client.ClientNombre, Is.EqualTo("XJose Luis"));
   //             Assert.AreEqual(client.ClientNombre, "XJose Luis");
   //             Assert.That(client.ClientNombre, Does.Contain("XLuis"));
   //             Assert.That(client.ClientNombre, Does.Contain("Xluis").IgnoreCase);
   //             Assert.That(client.ClientNombre, Does.StartWith("XJose"));
   //             Assert.That(client.ClientNombre, Does.EndWith("XLuis"));

   //         });

			// asserts
			Assert.That(client.ClientNombre, Is.EqualTo("Jose Luis"));
			Assert.AreEqual(client.ClientNombre, "Jose Luis");
			Assert.That(client.ClientNombre, Does.Contain("Luis"));
			Assert.That(client.ClientNombre, Does.Contain("luis").IgnoreCase);
			Assert.That(client.ClientNombre, Does.StartWith("Jose"));
			Assert.That(client.ClientNombre, Does.EndWith("Luis"));
		}
        
        [Test]
		public void ClientNombre_NoValue_ReturnsNull()
		{
			// arrange
			

			// asserts
			Assert.IsNull(client.ClientNombre);
			      
		}

		[Test]
		public void DescuentoEvaluacion_DefaultClient_ReturnsDescuentoIntervalo()
		{
			int descuento = client.Descuento;
			Assert.That(descuento, Is.InRange(5, 24)); 
        }


		[Test]
		public void CrearNombreCompleto_InputNombre_ReturnsNotNull()
		{
			client.CrearNombreCompleto("Jose", "");
			Assert.IsNotNull(client.ClientNombre);
			Assert.IsFalse(string.IsNullOrEmpty(client.ClientNombre)); 
        }

		[Test]
		public void ClienteNombre_InputNombreEnBlanco_ThrowException()
		{
			var exceptionDetalle = Assert.Throws<ArgumentException>(() => client.CrearNombreCompleto("", "Luis"));
			Assert.AreEqual("El nombre esta en blanco", exceptionDetalle.Message);

			Assert.That(() =>
				client.CrearNombreCompleto("", "Drez"), Throws.ArgumentException.With.Message.EqualTo("El nombre esta en blanco")

			);

            Assert.Throws<ArgumentException>(() => client.CrearNombreCompleto("", "Luis"));
            Assert.That( () =>
				client.CrearNombreCompleto("", "Luis"), Throws.ArgumentException 
            );
        }

		[Test]
        public void GetClienteDetalle_CrearClienteConMenos500TotalOrder_ReturnsClienteBasico()
        {
			client.OrderTotal = 150;
			var result = client.GetClienteDetalle();
			Assert.That(result, Is.TypeOf<ClienteBasico>());
        }
        
        [Test]
        public void GetClienteDetalle_CrearClienteConMas500TotalOrder_ReturnsClienteBasico()
        {
			client.OrderTotal = 700;
			var result = client.GetClienteDetalle();
			Assert.That(result, Is.TypeOf<ClientePremium>());
        }

		
	}
}

