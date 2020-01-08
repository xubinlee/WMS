namespace EDMX
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WmsContext : DbContext
    {
        public WmsContext()
            : base("name=WmsContext")
        {
        }

        public virtual DbSet<AccountBook> AccountBook { get; set; }
        public virtual DbSet<Alert> Alert { get; set; }
        public virtual DbSet<Appointments> Appointments { get; set; }
        public virtual DbSet<AttAppointments> AttAppointments { get; set; }
        public virtual DbSet<AttGeneralLog> AttGeneralLog { get; set; }
        public virtual DbSet<AttWageBillDtl> AttWageBillDtl { get; set; }
        public virtual DbSet<AttWageBillHd> AttWageBillHd { get; set; }
        public virtual DbSet<BOM> BOM { get; set; }
        public virtual DbSet<ButtonPermission> ButtonPermission { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Goods> Goods { get; set; }
        public virtual DbSet<GoodsType> GoodsType { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<InventoryLog> InventoryLog { get; set; }
        public virtual DbSet<InventoryLoss> InventoryLoss { get; set; }
        public virtual DbSet<InventoryLossLog> InventoryLossLog { get; set; }
        public virtual DbSet<MainMenu> MainMenu { get; set; }
        public virtual DbSet<MoldAllot> MoldAllot { get; set; }
        public virtual DbSet<OrderDtl> OrderDtl { get; set; }
        public virtual DbSet<OrderHd> OrderHd { get; set; }
        public virtual DbSet<Packaging> Packaging { get; set; }
        public virtual DbSet<PaymentBillDtl> PaymentBillDtl { get; set; }
        public virtual DbSet<PaymentBillHd> PaymentBillHd { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<ProfitAndLoss> ProfitAndLoss { get; set; }
        public virtual DbSet<ProfitAndLossLog> ProfitAndLossLog { get; set; }
        public virtual DbSet<ReceiptBillDtl> ReceiptBillDtl { get; set; }
        public virtual DbSet<ReceiptBillHd> ReceiptBillHd { get; set; }
        public virtual DbSet<Resources> Resources { get; set; }
        public virtual DbSet<SchClass> SchClass { get; set; }
        public virtual DbSet<SLSalePrice> SLSalePrice { get; set; }
        public virtual DbSet<StaffSchClass> StaffSchClass { get; set; }
        public virtual DbSet<StockInBillDtl> StockInBillDtl { get; set; }
        public virtual DbSet<StockInBillHd> StockInBillHd { get; set; }
        public virtual DbSet<StockOutBillDtl> StockOutBillDtl { get; set; }
        public virtual DbSet<StockOutBillHd> StockOutBillHd { get; set; }
        public virtual DbSet<Stocktaking> Stocktaking { get; set; }
        public virtual DbSet<StocktakingLogDtl> StocktakingLogDtl { get; set; }
        public virtual DbSet<StocktakingLogHd> StocktakingLogHd { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<SystemInfo> SystemInfo { get; set; }
        public virtual DbSet<SystemStatus> SystemStatus { get; set; }
        public virtual DbSet<TonerLabel> TonerLabel { get; set; }
        public virtual DbSet<TypesList> TypesList { get; set; }
        public virtual DbSet<UnlistedGoods> UnlistedGoods { get; set; }
        public virtual DbSet<UnlistedGoodsLog> UnlistedGoodsLog { get; set; }
        public virtual DbSet<UsersInfo> UsersInfo { get; set; }
        public virtual DbSet<WageBillDtl> WageBillDtl { get; set; }
        public virtual DbSet<WageBillHd> WageBillHd { get; set; }
        public virtual DbSet<WageDesign> WageDesign { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
        public virtual DbSet<AttParameters> AttParameters { get; set; }
        public virtual DbSet<USPAttWageBillDtl> USPAttWageBillDtl { get; set; }
        public virtual DbSet<VInventoryLog> VInventoryLog { get; set; }
        public virtual DbSet<VInventoryLossLog> VInventoryLossLog { get; set; }
        public virtual DbSet<VProfitAndLossLog> VProfitAndLossLog { get; set; }
        public virtual DbSet<VStocktakingLog> VStocktakingLog { get; set; }
        public virtual DbSet<VUnlistedGoodsLog> VUnlistedGoodsLog { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountBook>()
                .Property(e => e.MEAS)
                .IsUnicode(false);

            modelBuilder.Entity<AccountBook>()
                .Property(e => e.Price)
                .HasPrecision(12, 5);

            modelBuilder.Entity<AccountBook>()
                .Property(e => e.Discount)
                .HasPrecision(5, 2);

            modelBuilder.Entity<AccountBook>()
                .Property(e => e.OtherFee)
                .HasPrecision(12, 2);

            modelBuilder.Entity<AccountBook>()
                .Property(e => e.InQty)
                .HasPrecision(12, 2);

            modelBuilder.Entity<AccountBook>()
                .Property(e => e.OutQty)
                .HasPrecision(12, 2);

            modelBuilder.Entity<AccountBook>()
                .Property(e => e.BalanceQty)
                .HasPrecision(12, 2);

            modelBuilder.Entity<AccountBook>()
                .Property(e => e.BalanceCost)
                .HasPrecision(12, 2);

            modelBuilder.Entity<AccountBook>()
                .Property(e => e.BillNo)
                .IsUnicode(false);

            modelBuilder.Entity<AccountBook>()
                .Property(e => e.NWeight)
                .HasPrecision(12, 3);

            modelBuilder.Entity<Alert>()
                .Property(e => e.Caption)
                .IsUnicode(false);

            modelBuilder.Entity<Appointments>()
                .Property(e => e.Weight)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Appointments>()
                .Property(e => e.NWeight)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Appointments>()
                .Property(e => e.Machine)
                .IsUnicode(false);

            modelBuilder.Entity<Appointments>()
                .Property(e => e.Cycle)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Appointments>()
                .Property(e => e.ManHour)
                .HasPrecision(3, 1);

            modelBuilder.Entity<Appointments>()
                .Property(e => e.ErrorRate)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Appointments>()
                .Property(e => e.Price)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Appointments>()
                .Property(e => e.Wages)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Appointments>()
                .Property(e => e.AMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Appointments>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            modelBuilder.Entity<Appointments>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<Appointments>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Appointments>()
                .Property(e => e.CustomField1)
                .IsUnicode(false);

            modelBuilder.Entity<AttAppointments>()
                .Property(e => e.SchClassName)
                .IsUnicode(false);

            modelBuilder.Entity<AttAppointments>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            modelBuilder.Entity<AttAppointments>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<AttAppointments>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AttGeneralLog>()
                .Property(e => e.EnrollNumber)
                .IsUnicode(false);

            modelBuilder.Entity<AttGeneralLog>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AttWageBillDtl>()
                .Property(e => e.Wages)
                .HasPrecision(12, 2);

            modelBuilder.Entity<AttWageBillDtl>()
                .Property(e => e.Welfare)
                .HasPrecision(12, 2);

            modelBuilder.Entity<AttWageBillDtl>()
                .Property(e => e.Deduction)
                .HasPrecision(12, 2);

            modelBuilder.Entity<AttWageBillDtl>()
                .Property(e => e.SocialSecurity)
                .HasPrecision(12, 2);

            modelBuilder.Entity<AttWageBillDtl>()
                .Property(e => e.IndividualIncomeTax)
                .HasPrecision(12, 2);

            modelBuilder.Entity<AttWageBillDtl>()
                .Property(e => e.AMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<AttWageBillHd>()
                .Property(e => e.BillNo)
                .IsUnicode(false);

            modelBuilder.Entity<AttWageBillHd>()
                .Property(e => e.YearMonth)
                .IsUnicode(false);

            modelBuilder.Entity<AttWageBillHd>()
                .Property(e => e.Balance)
                .HasPrecision(12, 2);

            modelBuilder.Entity<BOM>()
                .Property(e => e.Qty)
                .HasPrecision(12, 2);

            modelBuilder.Entity<BOM>()
                .Property(e => e.Price)
                .HasPrecision(12, 5);

            modelBuilder.Entity<BOM>()
                .Property(e => e.PriceNoTax)
                .HasPrecision(12, 6);

            modelBuilder.Entity<BOM>()
                .Property(e => e.Discount)
                .HasPrecision(5, 2);

            modelBuilder.Entity<BOM>()
                .Property(e => e.OtherFee)
                .HasPrecision(12, 2);

            modelBuilder.Entity<BOM>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<ButtonPermission>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ButtonPermission>()
                .Property(e => e.Caption)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.PostCode)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Contacts)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.ContactTel)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.ContactCellPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.QQ)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.ACBank)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.ACNo)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Tax)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.InvoiceTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.InvoiceAddr)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.InvoiceTel)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.LogisticsTel)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.LogisticsAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.StationCode)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.Province)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.County)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.Contacts)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.BWAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.ContactTel)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.ContactCellPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.QQ)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.Level)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<Goods>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Goods>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Goods>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<Goods>()
                .Property(e => e.Unit)
                .IsUnicode(false);

            modelBuilder.Entity<Goods>()
                .Property(e => e.SPEC)
                .IsUnicode(false);

            modelBuilder.Entity<Goods>()
                .Property(e => e.Price)
                .HasPrecision(12, 5);

            modelBuilder.Entity<Goods>()
                .Property(e => e.PriceNoTax)
                .HasPrecision(12, 6);

            modelBuilder.Entity<Goods>()
                .Property(e => e.Barcode)
                .IsUnicode(false);

            modelBuilder.Entity<Goods>()
                .Property(e => e.InPutVAT)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Goods>()
                .Property(e => e.OutPutVAT)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Goods>()
                .Property(e => e.UpperLimit)
                .HasPrecision(12, 3);

            modelBuilder.Entity<Goods>()
                .Property(e => e.LowerLimit)
                .HasPrecision(12, 3);

            modelBuilder.Entity<Goods>()
                .Property(e => e.CavityNumber)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Goods>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<GoodsType>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<GoodsType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Inventory>()
                .Property(e => e.GoodsCode)
                .IsUnicode(false);

            modelBuilder.Entity<Inventory>()
                .Property(e => e.GoodsName)
                .IsUnicode(false);

            modelBuilder.Entity<Inventory>()
                .Property(e => e.Barcode)
                .IsUnicode(false);

            modelBuilder.Entity<Inventory>()
                .Property(e => e.SPEC)
                .IsUnicode(false);

            modelBuilder.Entity<Inventory>()
                .Property(e => e.Unit)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLog>()
                .Property(e => e.DeptCode)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLog>()
                .Property(e => e.DeptName)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLog>()
                .Property(e => e.GoodsCode)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLog>()
                .Property(e => e.GoodsName)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLog>()
                .Property(e => e.Barcode)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLog>()
                .Property(e => e.SPEC)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLog>()
                .Property(e => e.Unit)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLoss>()
                .Property(e => e.DeptCode)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLoss>()
                .Property(e => e.DeptName)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLoss>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLoss>()
                .Property(e => e.GoodsCode)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLoss>()
                .Property(e => e.GoodsName)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLoss>()
                .Property(e => e.Price)
                .HasPrecision(12, 2);

            modelBuilder.Entity<InventoryLoss>()
                .Property(e => e.StockAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<InventoryLoss>()
                .Property(e => e.CheckAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<InventoryLoss>()
                .Property(e => e.DiffAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<InventoryLoss>()
                .Property(e => e.DiffRate)
                .HasPrecision(5, 2);

            modelBuilder.Entity<InventoryLoss>()
                .Property(e => e.PreCheckSellAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<InventoryLoss>()
                .Property(e => e.LossAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<InventoryLoss>()
                .Property(e => e.LossRate)
                .HasPrecision(5, 2);

            modelBuilder.Entity<InventoryLoss>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLossLog>()
                .Property(e => e.DeptCode)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLossLog>()
                .Property(e => e.DeptName)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLossLog>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLossLog>()
                .Property(e => e.GoodsCode)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLossLog>()
                .Property(e => e.GoodsName)
                .IsUnicode(false);

            modelBuilder.Entity<InventoryLossLog>()
                .Property(e => e.Price)
                .HasPrecision(12, 2);

            modelBuilder.Entity<InventoryLossLog>()
                .Property(e => e.StockAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<InventoryLossLog>()
                .Property(e => e.CheckAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<InventoryLossLog>()
                .Property(e => e.DiffAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<InventoryLossLog>()
                .Property(e => e.DiffRate)
                .HasPrecision(5, 2);

            modelBuilder.Entity<InventoryLossLog>()
                .Property(e => e.PreCheckSellAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<InventoryLossLog>()
                .Property(e => e.LossAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<InventoryLossLog>()
                .Property(e => e.LossRate)
                .HasPrecision(5, 2);

            modelBuilder.Entity<InventoryLossLog>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<MainMenu>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MoldAllot>()
                .Property(e => e.Qty)
                .HasPrecision(12, 2);

            modelBuilder.Entity<MoldAllot>()
                .Property(e => e.Price)
                .HasPrecision(12, 5);

            modelBuilder.Entity<MoldAllot>()
                .Property(e => e.PriceNoTax)
                .HasPrecision(12, 6);

            modelBuilder.Entity<OrderDtl>()
                .Property(e => e.Qty)
                .HasPrecision(12, 2);

            modelBuilder.Entity<OrderDtl>()
                .Property(e => e.MEAS)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDtl>()
                .Property(e => e.Price)
                .HasPrecision(12, 5);

            modelBuilder.Entity<OrderDtl>()
                .Property(e => e.PriceNoTax)
                .HasPrecision(12, 6);

            modelBuilder.Entity<OrderDtl>()
                .Property(e => e.Discount)
                .HasPrecision(5, 2);

            modelBuilder.Entity<OrderDtl>()
                .Property(e => e.OtherFee)
                .HasPrecision(12, 2);

            modelBuilder.Entity<OrderDtl>()
                .Property(e => e.NWeight)
                .HasPrecision(12, 3);

            modelBuilder.Entity<OrderDtl>()
                .Property(e => e.Modulus)
                .HasPrecision(12, 2);

            modelBuilder.Entity<OrderDtl>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<OrderHd>()
                .Property(e => e.BillNo)
                .IsUnicode(false);

            modelBuilder.Entity<OrderHd>()
                .Property(e => e.Contacts)
                .IsUnicode(false);

            modelBuilder.Entity<OrderHd>()
                .Property(e => e.BillAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<OrderHd>()
                .Property(e => e.UnReceiptedAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Packaging>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentBillDtl>()
                .Property(e => e.BillAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<PaymentBillDtl>()
                .Property(e => e.PaidAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<PaymentBillDtl>()
                .Property(e => e.UnPaidAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<PaymentBillDtl>()
                .Property(e => e.LastPaidAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<PaymentBillHd>()
                .Property(e => e.BillNo)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentBillHd>()
                .Property(e => e.Contacts)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentBillHd>()
                .Property(e => e.Balance)
                .HasPrecision(12, 2);

            modelBuilder.Entity<PaymentBillHd>()
                .Property(e => e.PaidAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<PaymentBillHd>()
                .Property(e => e.UnPaidAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.DeptCode)
                .IsUnicode(false);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.DeptName)
                .IsUnicode(false);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.GoodsCode)
                .IsUnicode(false);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.GoodsName)
                .IsUnicode(false);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.Price)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.StockAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.CheckAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.DiffAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.TransitAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.ReturnedAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.NonInStoreAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.NonArrivalAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.ExtraPresellAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.ExtraSoldAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.IntraPresellAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.IntraSoldAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.NonChargeOffAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.NonRecordedAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.GroupBuyingAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.DisasterAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.OtherAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.FinalDiffAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLoss>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.DeptCode)
                .IsUnicode(false);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.DeptName)
                .IsUnicode(false);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.GoodsCode)
                .IsUnicode(false);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.GoodsName)
                .IsUnicode(false);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.Price)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.StockAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.CheckAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.DiffAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.TransitAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.ReturnedAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.NonInStoreAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.NonArrivalAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.ExtraPresellAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.ExtraSoldAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.IntraPresellAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.IntraSoldAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.NonChargeOffAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.NonRecordedAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.GroupBuyingAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.DisasterAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.OtherAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.FinalDiffAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProfitAndLossLog>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<ReceiptBillDtl>()
                .Property(e => e.BillAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ReceiptBillDtl>()
                .Property(e => e.ReceiptedAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ReceiptBillDtl>()
                .Property(e => e.UnReceiptedAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ReceiptBillDtl>()
                .Property(e => e.LastReceiptedAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ReceiptBillHd>()
                .Property(e => e.BillNo)
                .IsUnicode(false);

            modelBuilder.Entity<ReceiptBillHd>()
                .Property(e => e.Contacts)
                .IsUnicode(false);

            modelBuilder.Entity<ReceiptBillHd>()
                .Property(e => e.Balance)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ReceiptBillHd>()
                .Property(e => e.ReceiptedAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ReceiptBillHd>()
                .Property(e => e.UnReceiptedAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Resources>()
                .Property(e => e.ResourceName)
                .IsUnicode(false);

            modelBuilder.Entity<Resources>()
                .Property(e => e.CustomField1)
                .IsUnicode(false);

            modelBuilder.Entity<SchClass>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<SLSalePrice>()
                .Property(e => e.Price)
                .HasPrecision(12, 5);

            modelBuilder.Entity<SLSalePrice>()
                .Property(e => e.PriceNoTax)
                .HasPrecision(12, 6);

            modelBuilder.Entity<SLSalePrice>()
                .Property(e => e.Discount)
                .HasPrecision(5, 2);

            modelBuilder.Entity<SLSalePrice>()
                .Property(e => e.OtherFee)
                .HasPrecision(12, 2);

            modelBuilder.Entity<StockInBillDtl>()
                .Property(e => e.Qty)
                .HasPrecision(12, 2);

            modelBuilder.Entity<StockInBillDtl>()
                .Property(e => e.MEAS)
                .IsUnicode(false);

            modelBuilder.Entity<StockInBillDtl>()
                .Property(e => e.Price)
                .HasPrecision(12, 5);

            modelBuilder.Entity<StockInBillDtl>()
                .Property(e => e.PriceNoTax)
                .HasPrecision(12, 6);

            modelBuilder.Entity<StockInBillDtl>()
                .Property(e => e.Discount)
                .HasPrecision(5, 2);

            modelBuilder.Entity<StockInBillDtl>()
                .Property(e => e.OtherFee)
                .HasPrecision(12, 2);

            modelBuilder.Entity<StockInBillDtl>()
                .Property(e => e.NWeight)
                .HasPrecision(12, 3);

            modelBuilder.Entity<StockInBillDtl>()
                .Property(e => e.TonsQty)
                .HasPrecision(12, 3);

            modelBuilder.Entity<StockInBillDtl>()
                .Property(e => e.TonsPrice)
                .HasPrecision(12, 2);

            modelBuilder.Entity<StockInBillDtl>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<StockInBillHd>()
                .Property(e => e.BillNo)
                .IsUnicode(false);

            modelBuilder.Entity<StockInBillHd>()
                .Property(e => e.Contacts)
                .IsUnicode(false);

            modelBuilder.Entity<StockInBillHd>()
                .Property(e => e.BillAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<StockInBillHd>()
                .Property(e => e.UnPaidAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<StockOutBillDtl>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<StockOutBillDtl>()
                .Property(e => e.Weight)
                .HasPrecision(12, 2);

            modelBuilder.Entity<StockOutBillDtl>()
                .Property(e => e.Qty)
                .HasPrecision(12, 2);

            modelBuilder.Entity<StockOutBillDtl>()
                .Property(e => e.MEAS)
                .IsUnicode(false);

            modelBuilder.Entity<StockOutBillDtl>()
                .Property(e => e.Price)
                .HasPrecision(12, 5);

            modelBuilder.Entity<StockOutBillDtl>()
                .Property(e => e.PriceNoTax)
                .HasPrecision(12, 6);

            modelBuilder.Entity<StockOutBillDtl>()
                .Property(e => e.Discount)
                .HasPrecision(5, 2);

            modelBuilder.Entity<StockOutBillDtl>()
                .Property(e => e.OtherFee)
                .HasPrecision(12, 2);

            modelBuilder.Entity<StockOutBillDtl>()
                .Property(e => e.NWeight)
                .HasPrecision(12, 3);

            modelBuilder.Entity<StockOutBillDtl>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<StockOutBillDtl>()
                .Property(e => e.TonsQty)
                .HasPrecision(12, 3);

            modelBuilder.Entity<StockOutBillDtl>()
                .Property(e => e.TonsPrice)
                .HasPrecision(12, 2);

            modelBuilder.Entity<StockOutBillHd>()
                .Property(e => e.BillNo)
                .IsUnicode(false);

            modelBuilder.Entity<StockOutBillHd>()
                .Property(e => e.Contacts)
                .IsUnicode(false);

            modelBuilder.Entity<StockOutBillHd>()
                .Property(e => e.BillAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<StockOutBillHd>()
                .Property(e => e.UnReceiptedAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Stocktaking>()
                .Property(e => e.DeptCode)
                .IsUnicode(false);

            modelBuilder.Entity<Stocktaking>()
                .Property(e => e.DeptName)
                .IsUnicode(false);

            modelBuilder.Entity<Stocktaking>()
                .Property(e => e.WarehouseCode)
                .IsUnicode(false);

            modelBuilder.Entity<Stocktaking>()
                .Property(e => e.WarehouseName)
                .IsUnicode(false);

            modelBuilder.Entity<Stocktaking>()
                .Property(e => e.GoodsCode)
                .IsUnicode(false);

            modelBuilder.Entity<Stocktaking>()
                .Property(e => e.GoodsName)
                .IsUnicode(false);

            modelBuilder.Entity<Stocktaking>()
                .Property(e => e.Barcode)
                .IsUnicode(false);

            modelBuilder.Entity<Stocktaking>()
                .Property(e => e.SPEC)
                .IsUnicode(false);

            modelBuilder.Entity<Stocktaking>()
                .Property(e => e.Unit)
                .IsUnicode(false);

            modelBuilder.Entity<Stocktaking>()
                .Property(e => e.Price)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Stocktaking>()
                .Property(e => e.ScanBill)
                .IsUnicode(false);

            modelBuilder.Entity<Stocktaking>()
                .Property(e => e.Checker)
                .IsUnicode(false);

            modelBuilder.Entity<StocktakingLogDtl>()
                .Property(e => e.DeptCode)
                .IsUnicode(false);

            modelBuilder.Entity<StocktakingLogDtl>()
                .Property(e => e.DeptName)
                .IsUnicode(false);

            modelBuilder.Entity<StocktakingLogDtl>()
                .Property(e => e.WarehouseCode)
                .IsUnicode(false);

            modelBuilder.Entity<StocktakingLogDtl>()
                .Property(e => e.WarehouseName)
                .IsUnicode(false);

            modelBuilder.Entity<StocktakingLogDtl>()
                .Property(e => e.GoodsCode)
                .IsUnicode(false);

            modelBuilder.Entity<StocktakingLogDtl>()
                .Property(e => e.GoodsName)
                .IsUnicode(false);

            modelBuilder.Entity<StocktakingLogDtl>()
                .Property(e => e.Barcode)
                .IsUnicode(false);

            modelBuilder.Entity<StocktakingLogDtl>()
                .Property(e => e.SPEC)
                .IsUnicode(false);

            modelBuilder.Entity<StocktakingLogDtl>()
                .Property(e => e.Unit)
                .IsUnicode(false);

            modelBuilder.Entity<StocktakingLogDtl>()
                .Property(e => e.Price)
                .HasPrecision(12, 2);

            modelBuilder.Entity<StocktakingLogDtl>()
                .Property(e => e.ScanBill)
                .IsUnicode(false);

            modelBuilder.Entity<StocktakingLogDtl>()
                .Property(e => e.Checker)
                .IsUnicode(false);

            modelBuilder.Entity<StocktakingLogHd>()
                .Property(e => e.BillNo)
                .IsUnicode(false);

            modelBuilder.Entity<StocktakingLogHd>()
                .Property(e => e.Checker)
                .IsUnicode(false);

            modelBuilder.Entity<StocktakingLogHd>()
                .Property(e => e.CheckingTime)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.PostCode)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Contacts)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.ContactTel)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.ContactCellPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.QQ)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.ACBank)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.ACNo)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Tax)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.PaymentAddr)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.PaymentTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.InvoiceAddr)
                .IsUnicode(false);

            modelBuilder.Entity<SystemInfo>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<SystemInfo>()
                .Property(e => e.Contracts)
                .IsUnicode(false);

            modelBuilder.Entity<SystemInfo>()
                .Property(e => e.Accounts)
                .IsUnicode(false);

            modelBuilder.Entity<SystemInfo>()
                .Property(e => e.FtpPath)
                .IsUnicode(false);

            modelBuilder.Entity<SystemInfo>()
                .Property(e => e.ServerUserName)
                .IsUnicode(false);

            modelBuilder.Entity<SystemInfo>()
                .Property(e => e.ServerPassword)
                .IsUnicode(false);

            modelBuilder.Entity<SystemInfo>()
                .Property(e => e.Version)
                .IsUnicode(false);

            modelBuilder.Entity<SystemStatus>()
                .Property(e => e.MainMenuName)
                .IsUnicode(false);

            modelBuilder.Entity<SystemStatus>()
                .Property(e => e.MaxBillNo)
                .IsUnicode(false);

            modelBuilder.Entity<TonerLabel>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<TonerLabel>()
                .Property(e => e.Goods)
                .IsUnicode(false);

            modelBuilder.Entity<TonerLabel>()
                .Property(e => e.Material)
                .IsUnicode(false);

            modelBuilder.Entity<TonerLabel>()
                .Property(e => e.Dosage)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TonerLabel>()
                .Property(e => e.Subpackage)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TonerLabel>()
                .Property(e => e.TotalQty)
                .HasPrecision(12, 2);

            modelBuilder.Entity<TypesList>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<TypesList>()
                .Property(e => e.SubType)
                .IsUnicode(false);

            modelBuilder.Entity<TypesList>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<UnlistedGoods>()
                .Property(e => e.DeptCode)
                .IsUnicode(false);

            modelBuilder.Entity<UnlistedGoods>()
                .Property(e => e.DeptName)
                .IsUnicode(false);

            modelBuilder.Entity<UnlistedGoods>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<UnlistedGoods>()
                .Property(e => e.GoodsCode)
                .IsUnicode(false);

            modelBuilder.Entity<UnlistedGoods>()
                .Property(e => e.GoodsName)
                .IsUnicode(false);

            modelBuilder.Entity<UnlistedGoods>()
                .Property(e => e.Price)
                .HasPrecision(12, 2);

            modelBuilder.Entity<UnlistedGoods>()
                .Property(e => e.StockAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<UnlistedGoods>()
                .Property(e => e.UnlistedAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<UnlistedGoods>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<UnlistedGoodsLog>()
                .Property(e => e.DeptCode)
                .IsUnicode(false);

            modelBuilder.Entity<UnlistedGoodsLog>()
                .Property(e => e.DeptName)
                .IsUnicode(false);

            modelBuilder.Entity<UnlistedGoodsLog>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<UnlistedGoodsLog>()
                .Property(e => e.GoodsCode)
                .IsUnicode(false);

            modelBuilder.Entity<UnlistedGoodsLog>()
                .Property(e => e.GoodsName)
                .IsUnicode(false);

            modelBuilder.Entity<UnlistedGoodsLog>()
                .Property(e => e.Price)
                .HasPrecision(12, 2);

            modelBuilder.Entity<UnlistedGoodsLog>()
                .Property(e => e.StockAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<UnlistedGoodsLog>()
                .Property(e => e.UnlistedAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<UnlistedGoodsLog>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<UsersInfo>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<UsersInfo>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<UsersInfo>()
                .Property(e => e.Tel)
                .IsUnicode(false);

            modelBuilder.Entity<UsersInfo>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<UsersInfo>()
                .Property(e => e.DormNo)
                .IsUnicode(false);

            modelBuilder.Entity<UsersInfo>()
                .Property(e => e.AttCardnumber)
                .IsUnicode(false);

            modelBuilder.Entity<UsersInfo>()
                .Property(e => e.Wages)
                .HasPrecision(12, 2);

            modelBuilder.Entity<UsersInfo>()
                .Property(e => e.SchClassWage)
                .HasPrecision(12, 2);

            modelBuilder.Entity<UsersInfo>()
                .Property(e => e.TimeWage)
                .HasPrecision(12, 2);

            modelBuilder.Entity<WageBillDtl>()
                .Property(e => e.YearMonth)
                .IsUnicode(false);

            modelBuilder.Entity<WageBillDtl>()
                .Property(e => e.Wages)
                .HasPrecision(12, 2);

            modelBuilder.Entity<WageBillDtl>()
                .Property(e => e.Welfare)
                .HasPrecision(12, 2);

            modelBuilder.Entity<WageBillDtl>()
                .Property(e => e.Deduction)
                .HasPrecision(12, 2);

            modelBuilder.Entity<WageBillDtl>()
                .Property(e => e.SocialSecurity)
                .HasPrecision(12, 2);

            modelBuilder.Entity<WageBillDtl>()
                .Property(e => e.IndividualIncomeTax)
                .HasPrecision(12, 2);

            modelBuilder.Entity<WageBillDtl>()
                .Property(e => e.AMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<WageBillHd>()
                .Property(e => e.BillNo)
                .IsUnicode(false);

            modelBuilder.Entity<WageBillHd>()
                .Property(e => e.Balance)
                .HasPrecision(12, 2);

            modelBuilder.Entity<WageDesign>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<WageDesign>()
                .Property(e => e.Wages)
                .HasPrecision(12, 2);

            modelBuilder.Entity<WageDesign>()
                .Property(e => e.Price)
                .HasPrecision(12, 2);

            modelBuilder.Entity<WageDesign>()
                .Property(e => e.ErrorRate)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Warehouse>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Warehouse>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AttParameters>()
                .Property(e => e.CommMode)
                .IsUnicode(false);

            modelBuilder.Entity<AttParameters>()
                .Property(e => e.BaudRate)
                .IsUnicode(false);

            modelBuilder.Entity<AttParameters>()
                .Property(e => e.AttIP)
                .IsUnicode(false);

            modelBuilder.Entity<AttParameters>()
                .Property(e => e.AttPort)
                .IsUnicode(false);

            modelBuilder.Entity<USPAttWageBillDtl>()
                .Property(e => e.YearMonth)
                .IsUnicode(false);

            modelBuilder.Entity<USPAttWageBillDtl>()
                .Property(e => e.Wages)
                .HasPrecision(12, 2);

            modelBuilder.Entity<USPAttWageBillDtl>()
                .Property(e => e.Welfare)
                .HasPrecision(12, 2);

            modelBuilder.Entity<USPAttWageBillDtl>()
                .Property(e => e.Deduction)
                .HasPrecision(12, 2);

            modelBuilder.Entity<USPAttWageBillDtl>()
                .Property(e => e.SocialSecurity)
                .HasPrecision(12, 2);

            modelBuilder.Entity<USPAttWageBillDtl>()
                .Property(e => e.IndividualIncomeTax)
                .HasPrecision(12, 2);

            modelBuilder.Entity<USPAttWageBillDtl>()
                .Property(e => e.AMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VInventoryLog>()
                .Property(e => e.BillNo)
                .IsUnicode(false);

            modelBuilder.Entity<VInventoryLog>()
                .Property(e => e.DeptCode)
                .IsUnicode(false);

            modelBuilder.Entity<VInventoryLog>()
                .Property(e => e.DeptName)
                .IsUnicode(false);

            modelBuilder.Entity<VInventoryLog>()
                .Property(e => e.GoodsCode)
                .IsUnicode(false);

            modelBuilder.Entity<VInventoryLog>()
                .Property(e => e.GoodsName)
                .IsUnicode(false);

            modelBuilder.Entity<VInventoryLog>()
                .Property(e => e.Barcode)
                .IsUnicode(false);

            modelBuilder.Entity<VInventoryLog>()
                .Property(e => e.SPEC)
                .IsUnicode(false);

            modelBuilder.Entity<VInventoryLog>()
                .Property(e => e.Unit)
                .IsUnicode(false);

            modelBuilder.Entity<VInventoryLossLog>()
                .Property(e => e.BillNo)
                .IsUnicode(false);

            modelBuilder.Entity<VInventoryLossLog>()
                .Property(e => e.DeptCode)
                .IsUnicode(false);

            modelBuilder.Entity<VInventoryLossLog>()
                .Property(e => e.DeptName)
                .IsUnicode(false);

            modelBuilder.Entity<VInventoryLossLog>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<VInventoryLossLog>()
                .Property(e => e.GoodsCode)
                .IsUnicode(false);

            modelBuilder.Entity<VInventoryLossLog>()
                .Property(e => e.GoodsName)
                .IsUnicode(false);

            modelBuilder.Entity<VInventoryLossLog>()
                .Property(e => e.Price)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VInventoryLossLog>()
                .Property(e => e.StockAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VInventoryLossLog>()
                .Property(e => e.CheckAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VInventoryLossLog>()
                .Property(e => e.DiffAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VInventoryLossLog>()
                .Property(e => e.DiffRate)
                .HasPrecision(5, 2);

            modelBuilder.Entity<VInventoryLossLog>()
                .Property(e => e.PreCheckSellAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VInventoryLossLog>()
                .Property(e => e.LossAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VInventoryLossLog>()
                .Property(e => e.LossRate)
                .HasPrecision(5, 2);

            modelBuilder.Entity<VInventoryLossLog>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.BillNo)
                .IsUnicode(false);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.DeptCode)
                .IsUnicode(false);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.DeptName)
                .IsUnicode(false);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.GoodsCode)
                .IsUnicode(false);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.GoodsName)
                .IsUnicode(false);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.Price)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.StockAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.CheckAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.DiffAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.TransitAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.ReturnedAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.NonInStoreAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.NonArrivalAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.ExtraPresellAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.ExtraSoldAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.IntraPresellAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.IntraSoldAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.NonChargeOffAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.NonRecordedAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.GroupBuyingAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.DisasterAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.OtherAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.FinalDiffAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VProfitAndLossLog>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<VStocktakingLog>()
                .Property(e => e.BillNo)
                .IsUnicode(false);

            modelBuilder.Entity<VStocktakingLog>()
                .Property(e => e.DeptCode)
                .IsUnicode(false);

            modelBuilder.Entity<VStocktakingLog>()
                .Property(e => e.DeptName)
                .IsUnicode(false);

            modelBuilder.Entity<VStocktakingLog>()
                .Property(e => e.WarehouseCode)
                .IsUnicode(false);

            modelBuilder.Entity<VStocktakingLog>()
                .Property(e => e.WarehouseName)
                .IsUnicode(false);

            modelBuilder.Entity<VStocktakingLog>()
                .Property(e => e.GoodsCode)
                .IsUnicode(false);

            modelBuilder.Entity<VStocktakingLog>()
                .Property(e => e.GoodsName)
                .IsUnicode(false);

            modelBuilder.Entity<VStocktakingLog>()
                .Property(e => e.Barcode)
                .IsUnicode(false);

            modelBuilder.Entity<VStocktakingLog>()
                .Property(e => e.SPEC)
                .IsUnicode(false);

            modelBuilder.Entity<VStocktakingLog>()
                .Property(e => e.Unit)
                .IsUnicode(false);

            modelBuilder.Entity<VStocktakingLog>()
                .Property(e => e.Price)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VStocktakingLog>()
                .Property(e => e.ScanBill)
                .IsUnicode(false);

            modelBuilder.Entity<VStocktakingLog>()
                .Property(e => e.Checker)
                .IsUnicode(false);

            modelBuilder.Entity<VUnlistedGoodsLog>()
                .Property(e => e.BillNo)
                .IsUnicode(false);

            modelBuilder.Entity<VUnlistedGoodsLog>()
                .Property(e => e.DeptCode)
                .IsUnicode(false);

            modelBuilder.Entity<VUnlistedGoodsLog>()
                .Property(e => e.DeptName)
                .IsUnicode(false);

            modelBuilder.Entity<VUnlistedGoodsLog>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<VUnlistedGoodsLog>()
                .Property(e => e.GoodsCode)
                .IsUnicode(false);

            modelBuilder.Entity<VUnlistedGoodsLog>()
                .Property(e => e.GoodsName)
                .IsUnicode(false);

            modelBuilder.Entity<VUnlistedGoodsLog>()
                .Property(e => e.Price)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VUnlistedGoodsLog>()
                .Property(e => e.StockAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VUnlistedGoodsLog>()
                .Property(e => e.UnlistedAMT)
                .HasPrecision(12, 2);

            modelBuilder.Entity<VUnlistedGoodsLog>()
                .Property(e => e.Remark)
                .IsUnicode(false);
        }
    }
}
