using AutoMapper;
using DealerAPI.Models;
using DealerAPI.Models.DTO;

namespace DealerAPI.Config
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AccountDetails, AccountDetailsDTO>();
            CreateMap<CustomerSupport, CustomerSupportDTO>();
            CreateMap<Notification, NotificationDTO>();
            CreateMap<ProfileInformation, ProfileInformationDTO>();
            CreateMap<PV_Aggregator, PV_AggregatorDTO>().ReverseMap();
            CreateMap<PV_NewCarDealer, PV_NewCarDealerDTO>().ReverseMap();
            CreateMap<PV_OpenMarket, PV_OpenMarketDTO>().ReverseMap();
            CreateMap<PV_OpenMarketDTO, PV_OpenMarket>().ReverseMap();
            CreateMap<PVA_Make, PVA_MakeDTO>();
            CreateMap<PVA_Model, PVA_ModelDTO>();
            CreateMap<PVA_Variant, PVA_VariantDTO>();
            CreateMap<PVA_YearOfReg, PVA_YearOfRegDTO>();
            CreateMap<State, StateDTO>();
            CreateMap<Order_StockAudit,Order_StockAuditDTO>().ReverseMap();
            CreateMap<StockAudit_Purpose, StockAudit_PurposeDTO>();
            CreateMap<UserPhone, UserPhoneDTO>().ReverseMap();
            CreateMap<UserInfo, UserInfoDTO>().ReverseMap();
            CreateMap<UserInfo, UserAccountDTO>().ReverseMap();
            CreateMap<LastUser, LastUserDTO>().ReverseMap();
            CreateMap<Status, StatusDTO>().ReverseMap();  
        }

    }
}
