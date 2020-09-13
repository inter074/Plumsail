namespace Infrastructure.ErrorResult
{
    public struct ErrorModel
    {
        public string Message { get; set; }

        public ErrorModel(string message)
        {
            Message = message;
        }
    }
}