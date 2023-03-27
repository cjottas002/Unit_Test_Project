using System;
using LibreriaJose;
using Moq;

namespace LibreriaJoseNUnitTest
{
	public class ProductoNUnitTest
	{
		[Test]
		public void GetPrecio_PremiumCliente_ReturnsPrecio80()
		{
			var producto = new Producto()
			{
				Precio = 50
			};
						
			var resultado = producto.GetPrecio(new Cliente { IsPremium = true });

			Assert.That(resultado, Is.EqualTo(40));

        }
        
        [Test]
		public void GetPrecio_PremiumClienteMoq_ReturnsPrecio80()
		{
			var producto = new Producto()
			{
				Precio = 50
			};

			var mocking = new Mock<ICliente>();
			mocking.Setup(s => s.IsPremium).Returns(true);

			var resultado = producto.GetPrecio(mocking.Object);

			Assert.That(resultado, Is.EqualTo(40));

        }
    }
}

