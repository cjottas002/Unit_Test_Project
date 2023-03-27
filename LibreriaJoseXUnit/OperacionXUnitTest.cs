namespace LibreriaJoseXUnitTest
{
    using System;
    using System.Collections.Generic;
    using LibreriaJose;
    using Xunit;

    public class OperacionXUnitTest
    {
        [Fact]
        public void SumarNumeros_InputDosNumeros_GetValorCorrectoTest()
        {
            //arrange => inicializar las variables o componentes que ejecutaran el test
            var op = new Operacion();
            int numero1Test = 50;
            int numero2Test = 69;

            //2.Act
            int resultado = op.SumarNumeros(numero1Test, numero2Test);


            //3. Assert
            Assert.Equal(119, resultado);


        }


        [Theory]
        [InlineData(3, false)]
        [InlineData(5, false)]
        [InlineData(7, false)]
        public void IsValorPar_InputNumeroPar_ReturnFalse(int numeroImpar, bool expectedResult)
        {
            var op = new Operacion();

            var resultado =  op.IsValorPar(numeroImpar);

            Assert.Equal(expectedResult, resultado);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(20)]
        public void IsValorPar_InputNumeroPar_ReturnTrue(int numeroPar)
        {
            var op = new Operacion();

            bool isPar = op.IsValorPar(numeroPar);

            Assert.True(isPar);
            

        }

        [Theory]
        [InlineData(2.2, 1.2)]
        [InlineData(2.23, 1.24)]
        public void SumarDecimal_InputDosNumeros_GetValorCorrectoTest(double decimal1Test, double decimal2Test)
        {
            //arrange => inicializar las variables o componentes que ejecutaran el test
            var op = new Operacion();

            //2.Act
            double resultado = op.SumarDecimales(decimal1Test, decimal2Test);


            //3. Assert
            // Intervalo esperado 3.3 - 3.5 
            Assert.Equal(3.4, resultado, 0);


        }

        [Fact]
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

            Assert.Equal(numerosImparesEsperados, resultados);
            Assert.Contains(5, resultados);
            
            Assert.Contains(5, resultados);
            Assert.NotEmpty(resultados);
            Assert.Equal(3, resultados.Count);
            Assert.DoesNotContain(100, resultados);
            Assert.Equal(resultados.OrderBy(u => u), resultados);
        }

    }
}

