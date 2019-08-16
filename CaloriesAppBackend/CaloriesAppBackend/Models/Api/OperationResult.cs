namespace CaloriesAppBackend.Models.Api
{
    public class OperationResult
    {
        public OperationResult(bool success)
        {
            Success = success;
        }
        public OperationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
