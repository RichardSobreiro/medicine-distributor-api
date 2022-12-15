namespace MedicinesDistributorApi.Constants
{
    public static class PaymentStatus
    {
        public static string AUTHORIZED = "Cobrança está Pré-Autorizada";
        public static string PAID = "Pago";
        public static string IN_ANALYSIS = "Analisando o Risco da Transação";
        public static string DECLINED = "Cobrança foi Negada";
        public static string CANCELED = "Cobrança foi Cancelada";
        public static string PROCESSING = "Em Processamento";
        public static string ERROR = "Erro";
        public static string COMPLETED = "Completed";
        public static string WAITING = "Aguardando Pagamento";
    }
}
