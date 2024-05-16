namespace PharmaStock___API.Helpers
{
    public class ServiceResponse<T>
    {
        public T? dados { get; set; }
        public string mensagem { get; set; } = string.Empty;
        public bool sucesso { get; set; } = true;
    }
}
