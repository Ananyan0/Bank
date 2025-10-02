namespace Bank.Application.DTOs.ResponseDTOs
{
    public class DirectorResponseDto
    {


        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int BranchId { get; set; }
    }
}
