namespace CodeValidator.DTO.Models
{
    public class ValidationDto
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public JsonDto jsonDto { get; set; }

    }
}
