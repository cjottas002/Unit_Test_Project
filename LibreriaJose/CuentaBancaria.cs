using System;
namespace LibreriaJose
{
	public class CuentaBancaria
	{
		public int Balance { get; set; }
		private readonly ILoggerGeneral logger;
		
        public CuentaBancaria(ILoggerGeneral logger) 
        {
			Balance = 0;
			this.logger = logger;
        }

		public bool Deposito(int monto)
		{
			this.logger.Message("Esta depositando la cantidad de: " + monto);
			this.logger.Message("Es otro texto");
			this.logger.Message("localhost.com");
			this.logger.PrioridadLogger = 100;
			var prioridad = this.logger.PrioridadLogger;
		
			Balance += monto;
			return true; 
        }

		public bool Retiro(int monto)
		{ 
			if(monto <= Balance)
			{
				logger.LogDatabase($"Monto de retiro: {monto}");
				Balance -= monto;
				return logger.LogBalanceDespuesRetiro(Balance); 
            }

			return logger.LogBalanceDespuesRetiro(Balance-monto);
        }

		public int GetBalance()
		{
			return Balance; 
        }

	}
}

