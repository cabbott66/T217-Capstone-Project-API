namespace T217_Capstone_Project_API.Models
{
    public class OperationResult<T>
    {
        public string Message { get; set; } = "";
        public bool Success { get; set; }
        public T? Value { get; set; }
        public int RecordsAffected { get; set; }
    }
}
