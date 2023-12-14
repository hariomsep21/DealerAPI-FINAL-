using Dealer.Model;
using DealerAPI.Models;
using DealerAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace DealerAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<UserInfo> Userstbl { get; set; }
        public DbSet<CustomerSupport> CustomerSupporttbl { get; set; }
        public DbSet<ProfileInformation> ProfileInformationtbl { get; set; }
        public DbSet<AccountDetails> AccountDetailstbl { get; set; }
        public DbSet<Notification> Notificationstbl { get; set; }
        public DbSet<State> Statetbl { get; set; }
        public DbSet<PV_Aggregator> PV_Aggregatorstbl { get; set; }
        public DbSet<PVA_Make> PVA_Maketbl { get; set; }
        public DbSet<PVA_Model> PVA_Modeltbl { get; set; }
        public DbSet<PVA_Variant> PVA_Varianttbl { get; set; }
        public DbSet<PVA_YearOfReg> PVA_YearOfRegtbl { get; set; }
        public DbSet<PV_OpenMarket> PV_OpenMarketstbl { get; set; }
        public DbSet<PV_NewCarDealer> PV_NewCarDealerstbl { get; set; }
        public DbSet<StockAudit_Purpose> StockAudit_Purposestbl { get; set; }
        public DbSet<Order_StockAudit> Order_StockAuditstbl { get; set; }
       
       
        public DbSet<Car> Cars { get; set; }
        public DbSet<Payment> Payment { get; set; }
      
        public DbSet<BankDetail> BankDetails { get; set; }
        public DbSet<Vehiclerecord> VehicleRecords { get; set; }
        public DbSet<ProcurementFilter> ProcurementFilters { get; set; }
        public DbSet<ProcDetails> procDetails { get; set; }
        public DbSet<StockAudit> StockAudits { get; set; }

       
    }
}
