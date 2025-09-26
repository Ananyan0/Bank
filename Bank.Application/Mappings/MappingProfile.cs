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

            CreateMap<Director, DirectorResponseDto>();
            CreateMap<CreateDirectorRequest, Director>();

            CreateMap<CreateTransactionRequest, Transaction>();
            CreateMap<Transaction, TransactionResponse>();

            CreateMap<Customer, List<CustomerResponseDTO>>();

            CreateMap<CreateAccountRequest, Account>();

        }
    }
}