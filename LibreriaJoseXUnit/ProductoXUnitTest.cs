using System;
using LibreriaJose;
using Moq;
using Xunit;

namespace LibreriaJoseXUnitTest
{
    public class ProductoXUnitTest
    {
        [Fact]
        public void GetPrecio_PremiumCliente_ReturnsPrecio80()
        {
            var producto = new Producto()
            {
                Precio = 50
            };

            var resultado = producto.GetPrecio(new Cliente { IsPremium = true });

            Assert.Equal(40, resultado);

        }

        [Fact]
        public void GetPrecio_PremiumClienteMoq_ReturnsPrecio80()
        {
            var producto = new Producto()
            {
                Precio = 50
            };

            var mocking = new Mock<ICliente>();
            mocking.Setup(s => s.IsPremium).Returns(true);

            var resultado = producto.GetPrecio(mocking.Object);

            Assert.Equal(40, resultado);

        }
    }
}

