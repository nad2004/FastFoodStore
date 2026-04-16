namespace Application.DTOs
{
    /// <summary>
    /// Generic API response wrapper for all API responses
    /// </summary>
    /// <typeparam name="T">The type of data being returned</typeparam>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int? ErrorCode { get; set; }

        /// <summary>
        /// Creates a successful API response
        /// </summary>
        public static ApiResponse<T> SuccessResponse(T data, string message = "Success")
            => new() { Success = true, Data = data, Message = message };

        /// <summary>
        /// Creates an error API response
        /// </summary>
        public static ApiResponse<T> ErrorResponse(string message, int? errorCode = null)
            => new() { Success = false, Message = message, ErrorCode = errorCode };
    }
}
