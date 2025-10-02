using AutoMapper;
using Bank.Application.DTOs;
using Bank.Application.DTOs.CreateDTOs;
using Bank.Application.DTOs.ResponseDTOs;
using Bank.Domain.Entities;

namespace Bank.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCustomerRequest, Customer>();
            CreateMap<CustomerUpdateDTO, Customer>();

            CreateMap<Customer, CustomerResponseDTO>();
            CreateMap<Customer, CustomerWithAccountsResponse>();


            CreateMap<CreateBranchRequest, Branch>();
            CreateMap<Branch, BranchResponseDto>();

            CreateMap<BranchUpdateDto, Branch>();

            CreateMap<Account, AccountResponseDto>();

            CreateMap<Director, DirectorResponseDto>()
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src =>
                        src.Email.Contains("@") ? src.Email : src.PhoneNumber)) 
                    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src =>
                        src.PhoneNumber.All(char.IsDigit) ? src.PhoneNumber : src.Email));

            CreateMap<CreateDirectorRequest, Director>();

            CreateMap<CreateTransactionRequest, Transaction>()
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => "Deposit"));


            CreateMap<Transaction, TransactionResponse>();
            CreateMap<Account, AccountTransResponse>();



            CreateMap<Customer, List<CustomerResponseDTO>>();

            CreateMap<CreateAccountRequest, Account>();

        }
    }
}