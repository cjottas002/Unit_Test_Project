using System;
namespace LibreriaJose
{

	public interface ILoggerGeneral
	{
        public int PrioridadLogger { get; set; }
        public string TipoLogger { get; set; }

        void Message(string message);
        bool LogDatabase(string message);
        bool LogBalanceDespuesRetiro(int balancedespuesRetiro);
        string MessageConReturnStr(string message);
        bool MessageConOutParametroReturnBoolean(string str, out string outputStr);
        bool MessageConObjetoReferenciaReturnBoolean(ref Cliente cliente);
    }

	public class LoggerGeneral : ILoggerGeneral
	{
        public int PrioridadLogger { get; set; }
        public string TipoLogger { get; set; }

        public bool LogBalanceDespuesRetiro(int balancedespuesRetiro)
        {
            if(balancedespuesRetiro >= 0)
            {
                Console.WriteLine("exito");
                return true; 
            }

            Console.WriteLine("error");
            return true;
        }

        public bool LogDatabase(string message)
        {
            Console.WriteLine("Error");
            return true;
        }

        public void Message(string message)
        {
			Console.WriteLine(message);
        }

        public bool MessageConObjetoReferenciaReturnBoolean(ref Cliente cliente)
        {
            return true; 
        }

        public bool MessageConOutParametroReturnBoolean(string str, out string outputStr)
        {
            outputStr = "Hola" + str;
            return true;
        }

        public string MessageConReturnStr(string message)
        {
            Console.WriteLine(message);
            return message.ToLower();
        }
    }

    public class LoggerFake : ILoggerGeneral
    {
        public int PrioridadLogger { get; set; }
        public string TipoLogger { get; set; }

        public bool LogBalanceDespuesRetiro(int balancedespuesRetiro)
        {
            return false;
        }

        public bool LogDatabase(string message)
        {
            return false;
        }

        public void Message(string message)
        {
          
        }

        public bool MessageConObjetoReferenciaReturnBoolean(ref Cliente cliente)
        {
            return true;
        }

        public bool MessageConOutParametroReturnBoolean(string message, out string outputStr)
        {
            outputStr = string.Empty;

            return false;
        }

        public string MessageConReturnStr(string message)
        {
            return string.Empty; 
        }
    }
}

