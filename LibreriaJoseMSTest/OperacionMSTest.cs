namespace LibreriaJoseMSTest
{
    using System;
    using LibreriaJose;

    [TestClass]
    public class OperacionMSTest
	{
		

		[TestMethod]
		public void SumarNumeros_InputDosNumeros_GetValorCorrectoTest()
		{
			//arrange => inicializar las variables o componentes que ejecutaran el test
			var op = new Operacion();
			int numero1Test = 50;
			int numero2Test = 69;

			//2.Act
			int resultado = op.SumarNumeros(numero1Test, numero2Test);


			//3. Assert
			Assert.AreEqual(119, resultado);


		}
	}
}

