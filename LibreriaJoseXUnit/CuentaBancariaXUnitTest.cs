using System;
using LibreriaJose;
using Moq;
using Xunit;

namespace LibreriaJoseXUnitTest
{
	
	public class CuentaBancariaXUnitTest
	{
		private CuentaBancaria cuenta;

		
		[Fact]
		public void Deposito_InputMonto100LoggerFake_ReturnsTrue()
		{

			// arrange
			var cuentaBancaria = new CuentaBancaria(new LoggerFake());

			// act
			var resultado = cuentaBancaria.Deposito(100);


			// assert
			Assert.True(resultado);
			Assert.Equal(100, cuentaBancaria.GetBalance());

		}

		[Fact]
		public void Deposito_InputMonto100Mocking_ReturnsTrue()
		{
			var mocking = new Mock<ILoggerGeneral>();
			// arrange
			var cuentaBancaria = new CuentaBancaria(mocking.Object);

			// act
			var resultado = cuentaBancaria.Deposito(100);


            // assert
            Assert.True(resultado);
            Assert.Equal(100, cuentaBancaria.GetBalance());

        }

		[Theory]
		[InlineData(200, 100)]
		[InlineData(200, 150)]
		public void Retiro_Retiro100ConBalance200_ReturnsTrue(int balance, int retiro)
		{
			var loggerMock = new Mock<ILoggerGeneral>();
			loggerMock.Setup(u => u.LogDatabase(It.IsAny<string>())).Returns(true);
			loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.Is<int>(x => x > 0))).Returns(true);


			var cuentaBancaria = new CuentaBancaria(loggerMock.Object);
			cuentaBancaria.Deposito(200);

			var resultado = cuentaBancaria.Retiro(100);

			Assert.True(resultado);

		}

		[Theory]
		[InlineData(200, 300)]
		public void Retiro_Retiro300ConBalance200_ReturnsFalse(int balance, int retiro)
		{
			var loggerMock = new Mock<ILoggerGeneral>();
			//loggerMock.Setup(u => u.LogDatabase(It.IsAny<string>())).Returns(true);
			//loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.Is<int>(x => x < 0))).Returns(false);		
            loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive ))).Returns(false);
			
            
            var cuentaBancaria = new CuentaBancaria(loggerMock.Object);
			cuentaBancaria.Deposito(200);

			var resultado = cuentaBancaria.Retiro(100);

			Assert.False(resultado);
             
        }


		[Fact]
		public void CuentaBancariaLoggerGeneral_LogMocking_ReturnsTrue()
		{
			var loggerGeneralMock = new Mock<ILoggerGeneral>();
			string textoPrueba = "hola mundo";

			loggerGeneralMock.Setup(u => u.MessageConReturnStr(It.IsAny<string>())).Returns<string>(str => str.ToLower());

			var resultado = loggerGeneralMock.Object.MessageConReturnStr("holA MUndo");

			Assert.Equal(textoPrueba, resultado);
            
             
        }

		[Fact]
		public void CuentaBancariaLoggerGeneral_LogMockingOutPut_ReturnsTrue() 
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();
            string textoPrueba = "hola";

            loggerGeneralMock.Setup(u => u.MessageConOutParametroReturnBoolean(It.IsAny<string>(), out textoPrueba)).Returns(true);

            string parametroOut = string.Empty;
			var resultado = loggerGeneralMock.Object.MessageConOutParametroReturnBoolean("Jose", out parametroOut);

			Assert.True(resultado);

        }

		[Fact]
		public void CuentaBancariaLoggerGeneral_LogMockingObjectRef_ReturnTrue()
		{
			var loggerGeneralMock = new Mock<ILoggerGeneral>();

			var cliente = new Cliente();
			var clienteNoUsado = new Cliente();

			loggerGeneralMock.Setup(u => u.MessageConObjetoReferenciaReturnBoolean(ref cliente)).Returns(true);

			Assert.True(loggerGeneralMock.Object.MessageConObjetoReferenciaReturnBoolean(ref cliente));

			Assert.False(loggerGeneralMock.Object.MessageConObjetoReferenciaReturnBoolean(ref clienteNoUsado));
        }

		[Fact]
		public void CuentaBancariaLoggerGeneral_LogMockingPropiedadPrioridadTipo_ReturnsTrue()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();
			// Para que una propiedad de un objeto mock pueda ser seteada directamente se ha de colocar el metodo SetupAllProperties
			loggerGeneralMock.SetupAllProperties();

			loggerGeneralMock.Setup(u => u.TipoLogger).Returns("warning");
			loggerGeneralMock.Setup(u => u.PrioridadLogger).Returns(10);

			loggerGeneralMock.Object.PrioridadLogger = 100;
			Assert.Multiple(() =>
            {
                Assert.Equal("warning", loggerGeneralMock.Object.TipoLogger);
                Assert.Equal(10, loggerGeneralMock.Object.PrioridadLogger);
            });

            // CALLBACKS       

            string textoTemporal = "jose";
			loggerGeneralMock.Setup(u => u.LogDatabase(It.IsAny<string>())).Returns(true).Callback((string parametro) => textoTemporal += parametro);

			loggerGeneralMock.Object.LogDatabase("luis"); // joseluis

			Assert.Equal("joseluis", textoTemporal);
            
            int contador = 5;
			loggerGeneralMock.Setup(u => u.LogDatabase(It.IsAny<string>())).Returns(true).Callback(() => contador++);

			loggerGeneralMock.Object.LogDatabase("luis");

			Assert.Equal(6, contador);
        }

		[Fact]
		public void CuentaBancariaLogger_VerifyEjemplo()
		{
			var loggerGeneralMock = new Mock<ILoggerGeneral>();

			var cuentaBancaria = new CuentaBancaria(loggerGeneralMock.Object);
			cuentaBancaria.Deposito(100);

			Assert.Equal(100, cuentaBancaria.GetBalance());

			// Verifica cuantas veces el mock esta llamando al metodo message 

			loggerGeneralMock.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(3));

			loggerGeneralMock.Verify(u => u.Message("localhost.com"), Times.AtLeastOnce);

			loggerGeneralMock.VerifySet(u => u.PrioridadLogger = 100, Times.Once);

			loggerGeneralMock.VerifyGet(u => u.PrioridadLogger, Times.Once);
        }
    }
}

