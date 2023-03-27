namespace LibreriaJoseNUnitTest
{
    using System;
    using System.Collections.Generic;
    using LibreriaJose;

    [TestFixture]
    public class OperacionNUnitTest
    {
        [Test]
        public void SumarNumeros_InputDosNumeros_GetValorCorrectoTest()
        {
            //arrange => inicializar las variables o componentes que ejecutaran el test
            var op = new Operacion();
            int numero1Test = 50;
            int numero2Test = 69;

            //2.Act
            int resultado = op.SumarNumeros(numero1Test, numero2Test);


            //3. Assert
            Assert.That(resultado, Is.EqualTo(119));


        }


        [Test]
        [TestCase(3, ExpectedResult = false)]
        [TestCase(5, ExpectedResult = false)]
        [TestCase(7, ExpectedResult = false)]
        public bool IsValorPar_InputNumeroPar_ReturnFalse(int numeroImpar)
        {
            var op = new Operacion();

            return op.IsValorPar(numeroImpar);
        }

        [Test]
        [TestCase(4)]
        [TestCase(6)]
        [TestCase(20)]
        public void IsValorPar_InputNumeroPar_ReturnTrue(int numeroPar)
        {
            var op = new Operacion();

            bool isPar = op.IsValorPar(numeroPar);

            Assert.That(isPar, Is.True);
            Assert.That(isPar, Is.EqualTo(true));

        }

        [Test]
        [TestCase(2.2, 1.2)]
        [TestCase(2.23, 1.24)]
        public void SumarDecimal_InputDosNumeros_GetValorCorrectoTest(double decimal1Test, double decimal2Test)
        {
            //arrange => inicializar las variables o componentes que ejecutaran el test
            var op = new Operacion();

            //2.Act
            double resultado = op.SumarDecimales(decimal1Test, decimal2Test);


            //3. Assert
            // Intervalo esperado 3.3 - 3.5, el intervalo es de 0.1 por eso se coloca en el tercer parametro
            Assert.AreEqual(3.4, resultado, 0.1);


        }

        [Test]
        public void GetListaNumerosImpares_InputIntervalosMinMax_ReturnsListaImpares()
        {

            var op = new Operacion();

            List<int> numerosImparesEsperados = new()
            {
                5,
                7,
                9
            };

            List<int> resultados = op.GetListaNumerosImpares(5, 10);

            Assert.That(resultados, Is.EquivalentTo(numerosImparesEsperados));
            Assert.AreEqual(numerosImparesEsperados, resultados);
            Assert.That(resultados, Does.Contain(5));
            Assert.Contains(5, resultados);
            Assert.That(resultados, Is.Not.Empty);
            Assert.That(resultados.Count, Is.EqualTo(3));
            Assert.That(resultados, Has.No.Member(100));
            Assert.That(resultados, Is.Ordered.Ascending);
        }

    }
}

