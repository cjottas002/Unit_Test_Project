using System;
using LibreriaJose;
using Moq;

namespace LibreriaJoseNUnitTest
{
	[TestFixture]
	public class CuentaBancariaNUnitTest
	{
		//private CuentaBancaria cuenta;

		[SetUp]
		public void SetUp()
		{

		}


		[Test]
		public void Deposito_InputMonto100LoggerFake_ReturnsTrue()
		{

			// arrange
			var cuentaBancaria = new CuentaBancaria(new LoggerFake());

			// act
			var resultado = cuentaBancaria.Deposito(100);


			// assert
			Assert.IsTrue(resultado);
			Assert.That(cuentaBancaria.GetBalance, Is.EqualTo(100));

		}

		[Test]
		public void Deposito_InputMonto100Mocking_ReturnsTrue()
        {
            var mocking = new Mock<ILoggerGeneral>();
			// arrange
			var cuentaBancaria = new CuentaBancaria(mocking.Object);

			// act
			var resultado = cuentaBancaria.Deposito(100);
            Assert.Multiple(() =>
            {


                // assert
                Assert.That(resultado, Is.True);
                Assert.That(cuentaBancaria.GetBalance, Is.EqualTo(100));
            });
        }

        [Test]
		[TestCase(200, 100)]
		[TestCase(200, 150)]
		public void Retiro_Retiro100ConBalance200_ReturnsTrue(int balance, int retiro)
		{
			var loggerMock = new Mock<ILoggerGeneral>();
			loggerMock.Setup(u => u.LogDatabase(It.IsAny<string>())).Returns(true);
			loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.Is<int>(x => x > 0))).Returns(true);


			var cuentaBancaria = new CuentaBancaria(loggerMock.Object);
			cuentaBancaria.Deposito(200);

			var resultado = cuentaBancaria.Retiro(100);

			Assert.IsTrue(resultado);

		}

		[Test]
		[TestCase(200, 300)]
		public void Retiro_Retiro300ConBalance200_ReturnsFalse(int balance, int retiro)
		{
			var loggerMock = new Mock<ILoggerGeneral>();
			//loggerMock.Setup(u => u.LogDatabase(It.IsAny<string>())).Returns(true);
			//loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.Is<int>(x => x < 0))).Returns(false);		
            loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive ))).Returns(false);
			
            
            var cuentaBancaria = new CuentaBancaria(loggerMock.Object);
			cuentaBancaria.Deposito(200);

			var resultado = cuentaBancaria.Retiro(100);

			Assert.IsFalse(resultado);
             
        }


		[Test]
		public void CuentaBancariaLoggerGeneral_LogMocking_ReturnsTrue()
		{
			var loggerGeneralMock = new Mock<ILoggerGeneral>();
			string textoPrueba = "hola mundo";

			loggerGeneralMock.Setup(u => u.MessageConReturnStr(It.IsAny<string>())).Returns<string>(str => str.ToLower());

			var resultado = loggerGeneralMock.Object.MessageConReturnStr("holA MUndo");

			Assert.That(resultado, Is.EqualTo(textoPrueba));
            
             
        }

		[Test]
		public void CuentaBancariaLoggerGeneral_LogMockingOutPut_ReturnsTrue() 
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();
            string textoPrueba = "hola";

            loggerGeneralMock.Setup(u => u.MessageConOutParametroReturnBoolean(It.IsAny<string>(), out textoPrueba)).Returns(true);

            string parametroOut = string.Empty;
			var resultado = loggerGeneralMock.Object.MessageConOutParametroReturnBoolean("Jose", out parametroOut);

			Assert.IsTrue(resultado);

        }

		[Test]
		public void CuentaBancariaLoggerGeneral_LogMockingObjectRef_ReturnTrue()
		{
			var loggerGeneralMock = new Mock<ILoggerGeneral>();

			var cliente = new Cliente();
			var clienteNoUsado = new Cliente();

			loggerGeneralMock.Setup(u => u.MessageConObjetoReferenciaReturnBoolean(ref cliente)).Returns(true);

			Assert.IsTrue(loggerGeneralMock.Object.MessageConObjetoReferenciaReturnBoolean(ref cliente));

			Assert.IsFalse(loggerGeneralMock.Object.MessageConObjetoReferenciaReturnBoolean(ref clienteNoUsado));
        }

		[Test]
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
                Assert.That(loggerGeneralMock.Object.TipoLogger, Is.EqualTo("warning"));
                Assert.That(loggerGeneralMock.Object.PrioridadLogger, Is.EqualTo(10));
            });

            // CALLBACKS       

            string textoTemporal = "jose";
			loggerGeneralMock.Setup(u => u.LogDatabase(It.IsAny<string>())).Returns(true).Callback((string parametro) => textoTemporal += parametro);

			loggerGeneralMock.Object.LogDatabase("luis"); // joseluis

			Assert.That(textoTemporal, Is.EqualTo("joseluis"));
            
            int contador = 5;
			loggerGeneralMock.Setup(u => u.LogDatabase(It.IsAny<string>())).Returns(true).Callback(() => contador++);

			loggerGeneralMock.Object.LogDatabase("luis");

			Assert.That(contador, Is.EqualTo(6));
        }

		[Test]
		public void CuentaBancariaLogger_VerifyEjemplo()
		{
			var loggerGeneralMock = new Mock<ILoggerGeneral>();

			var cuentaBancaria = new CuentaBancaria(loggerGeneralMock.Object);
			cuentaBancaria.Deposito(100);

			Assert.That(cuentaBancaria.GetBalance, Is.EqualTo(100));

			// Verifica cuantas veces el mock esta llamando al metodo message 

			loggerGeneralMock.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(3));

			loggerGeneralMock.Verify(u => u.Message("localhost.com"), Times.AtLeastOnce);

			loggerGeneralMock.VerifySet(u => u.PrioridadLogger = 100, Times.Once);

			loggerGeneralMock.VerifyGet(u => u.PrioridadLogger, Times.Once);
        }
    }
}

