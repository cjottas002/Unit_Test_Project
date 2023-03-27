using System;
using LibreriaJose;
using Xunit;

namespace LibreriaJoseXUnitTest
{

    public class ClienteUnitTest
    {
        private Cliente client;


        public ClienteUnitTest()
        {
            client = new Cliente();

        }

        [Fact]
        public void CrearNombreCompleto_InputNombreApellido_ReturnNombreCompleto()
        {
            // arrange


            // act
            client.CrearNombreCompleto("Jose", "Luis");



            // asserts
            Assert.Equal("Jose Luis", client.ClientNombre);
            Assert.Contains("Luis", client.ClientNombre);
            Assert.StartsWith("Jose", client.ClientNombre);
            Assert.EndsWith("Luis", client.ClientNombre);
        }

        [Fact]
        public void ClientNombre_NoValue_ReturnsNull()
        {
            // arrange


            // asserts
            Assert.Null(client.ClientNombre);

        }

        [Fact]
        public void DescuentoEvaluacion_DefaultClient_ReturnsDescuentoIntervalo()
        {
            int descuento = client.Descuento;
            Assert.InRange(descuento, 5, 24);
        }


        [Fact]
        public void CrearNombreCompleto_InputNombre_ReturnsNotNull()
        {
            client.CrearNombreCompleto("Jose", "");
            Assert.NotNull(client.ClientNombre);
            Assert.False(string.IsNullOrEmpty(client.ClientNombre));
        }

        [Fact]
        public void ClienteNombre_InputNombreEnBlanco_ThrowException()
        {
            var exceptionDetalle = Assert.Throws<ArgumentException>(() => client.CrearNombreCompleto("", "Luis"));
            Assert.Equal("El nombre esta en blanco", exceptionDetalle.Message);

            Assert.Throws<ArgumentException>(() => client.CrearNombreCompleto("", "Luis"));

        }

        [Fact]
        public void GetClienteDetalle_CrearClienteConMenos500TotalOrder_ReturnsClienteBasico()
        {
            client.OrderTotal = 150;
            var result = client.GetClienteDetalle();
            Assert.IsType<ClienteBasico>(result);
        }

        [Fact]
        public void GetClienteDetalle_CrearClienteConMas500TotalOrder_ReturnsClienteBasico()
        {
            client.OrderTotal = 700;
            var result = client.GetClienteDetalle();
            Assert.IsType<ClientePremium>(result);
        }


    }
}

