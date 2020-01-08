using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace USL
{
    public enum MainMenuEnum
    {
        /// <summary>
        /// 仓库资料
        /// </summary>
        Warehouse,
        /// <summary>
        /// 公司资料（指客户）
        /// </summary>
        Company,
        /// <summary>
        /// 客户资料
        /// </summary>
        Customer,
        /// <summary>
        /// 部门资料
        /// </summary>
        Department,
        /// <summary>
        /// 供应商资料
        /// </summary>
        Supplier,
        /// <summary>
        /// 厂家资料
        /// </summary>
        Mfrs,
        /// <summary>
        /// 职工资料
        /// </summary>
        UsersInfo,
        /// <summary>
        /// 成品资料
        /// </summary>
        Goods,
        /// <summary>
        /// 材料资料
        /// </summary>
        Material,
        /// <summary>
        /// 货品类型
        /// </summary>
        GoodsType,
        /// <summary>
        /// 包装方式
        /// </summary>
        Packaging,
        /// <summary>
        /// 成品布产单
        /// </summary>
        ProductionOrder,
        /// <summary>
        /// 成品布产单查询
        /// </summary>
        ProductionOrderQuery,
        /// <summary>
        /// 成品入库单
        /// </summary>
        ProductionStockInBill,
        /// <summary>
        /// 成品入库单查询
        /// </summary>
        ProductionStockInBillQuery,
        /// <summary>
        /// 销售退货单
        /// </summary>
        SalesReturnBill,
        /// <summary>
        /// 销售退货单查询
        /// </summary>
        SalesReturnBillQuery,
        /// <summary>
        /// 外加工产品回收单
        /// </summary>
        FGStockInBill,
        /// <summary>
        /// 外加工产品回收单查询
        /// </summary>
        FGStockInBillQuery,
        /// <summary>
        /// 外加工退料单
        /// </summary>
        EMSReturnBill,
        /// <summary>
        /// 外加工退料单查询
        /// </summary>
        EMSReturnBillQuery,
        /// <summary>
        /// 外加工残次品退货单
        /// </summary>
        EMSDPReturnBill,
        /// <summary>
        /// 外加工残次品退货单查询
        /// </summary>
        EMSDPReturnBillQuery,
        /// <summary>
        /// 采购入料单
        /// </summary>
        SFGStockInBill,
        /// <summary>
        /// 采购入料单
        /// </summary>
        SFGStockInBillQuery,
        /// <summary>
        /// 自动机回收单
        /// </summary>
        FSMStockInBill,
        /// <summary>
        /// 自动机回收单查询
        /// </summary>
        FSMStockInBillQuery,
        /// <summary>
        /// 入库单查询
        /// </summary>
        //StockInBillQuery,
        /// <summary>
        /// 订货单
        /// </summary>
        Order,
        /// <summary>
        /// 订货单查询
        /// </summary>
        OrderQuery,
        /// <summary>
        /// 发货单
        /// </summary>
        FGStockOutBill,
        /// <summary>
        /// 发货单查询
        /// </summary>
        FGStockOutBillQuery,
        /// <summary>
        /// 投料单
        /// </summary>
        EMSStockOutBill,
        /// <summary>
        /// 投料查询
        /// </summary>
        EMSStockOutBillQuery,
        /// <summary>
        /// 采购退料单
        /// </summary>
        SFGStockOutBill,
        /// <summary>
        /// 采购退料单查询
        /// </summary>
        SFGStockOutBillQuery,
        /// <summary>
        /// 自动机发料单
        /// </summary>
        FSMStockOutBill,
        /// <summary>
        /// 领料出库单
        /// </summary>
        GetMaterialBill,
        /// <summary>
        /// 自动机发料单查询
        /// </summary>
        FSMStockOutBillQuery,
        /// <summary>
        /// 领料出库单查询
        /// </summary>
        GetMaterialBillQuery,
        /// <summary>
        /// 自动机退料单
        /// </summary>
        FSMReturnBill,
        /// <summary>
        /// 自动机退料单查询
        /// </summary>
        FSMReturnBillQuery,
        /// <summary>
        /// 自动机残次材料退货单
        /// </summary>
        FSMDPReturnBill,
        /// <summary>
        /// 自动机残次材料退货单查询
        /// </summary>
        FSMDPReturnBillQuery,
        /// <summary>
        /// 装配入库单
        /// </summary>
        AssembleStockInBill,
        /// <summary>
        /// 退料入库单
        /// </summary>
        ReturnedMaterialBill,
        /// <summary>
        /// 装配入库单查询
        /// </summary>
        AssembleStockInBillQuery,
        /// <summary>
        /// 退料入库单查询
        /// </summary>
        ReturnedMaterialBillQuery,
        /// <summary>
        /// 自动机生产订单
        /// </summary>
        FSMOrder,
        /// <summary>
        /// 自动机生产订单查询
        /// </summary>
        FSMOrderQuery,
        /// <summary>
        /// 成品库存明细
        /// </summary>
        InventoryQuery,
        /// <summary>
        /// 成品库存查询
        /// </summary>
        InventoryGroupByGoodsQuery,
        /// <summary>
        /// 半成品库存明细
        /// </summary>
        MaterialInventoryQuery,
        /// <summary>
        /// 半成品库存查询
        /// </summary>
        InventoryGroupByMaterialQuery,
        /// <summary>
        /// 外加工库存查询
        /// </summary>
        EMSInventoryQuery,
        /// <summary>
        /// 自动机库存查询
        /// </summary>
        FSMInventoryQuery,
        /// <summary>
        /// 账页查询
        /// </summary>
        AccountBookQuery,
        /// <summary>
        /// 账面库存
        /// </summary>
        Inventory,
        /// <summary>
        /// 账面库存日志表
        /// </summary>
        VInventoryLog,
        /// <summary>
        /// 库存盘点
        /// </summary>
        Stocktaking,
        /// <summary>
        /// 盘点盈亏表
        /// </summary>
        ProfitAndLoss,
        /// <summary>
        /// 损耗确认单
        /// </summary>
        InventoryLoss,
        /// <summary>
        /// 未上架商品确认单
        /// </summary>
        UnlistedGoods,
        /// <summary>
        /// 盘点日志表
        /// </summary>
        StocktakingLogHd,
        /// <summary>
        /// 盘点日志明细表
        /// </summary>
        VStocktakingLog,
        /// <summary>
        /// 盘点差异日志表
        /// </summary>
        VProfitAndLossLog,
        /// <summary>
        /// 盘点损耗日志表
        /// </summary>
        VInventoryLossLog,
        /// <summary>
        /// 未上架商品日志表
        /// </summary>
        VUnlistedGoodsLog,
        /// <summary>
        /// 货品装配物料清单
        /// </summary>
        BOM,
        /// <summary>
        /// 自动机模具清单
        /// </summary>
        MoldList,
        /// <summary>
        /// 自动机模具原料清单
        /// </summary>
        MoldMaterial,
        /// <summary>
        /// 材料装配物料清单
        /// </summary>
        Assemble,
        /// <summary>
        /// 自动机模具分配
        /// </summary>
        MoldAllot,
        /// <summary>
        /// 客户商品售价
        /// </summary>
        CustomerSLSalePrice,
        /// <summary>
        /// 供应商商品售价
        /// </summary>
        SupplierSLSalePrice,
        /// <summary>
        /// 收款单
        /// </summary>
        ReceiptBill,
        /// <summary>
        /// 收款单查询
        /// </summary>
        ReceiptBillQuery,
        /// <summary>
        /// 客户结算对账单
        /// </summary>
        StatementOfAccountToCustomer,
        /// <summary>
        /// 付款单
        /// </summary>
        PaymentBill,
        /// <summary>
        /// 付款单查询
        /// </summary>
        PaymentBillQuery,
        /// <summary>
        /// 供应商结算对账单
        /// </summary>
        StatementOfAccountToSupplier,
        /// <summary>
        /// 外加工货品跟踪日报表
        /// </summary>
        EMSGoodsTrackingDailyReport,
        /// <summary>
        /// 自动机货品跟踪日报表
        /// </summary>
        FSMGoodsTrackingDailyReport,
        /// <summary>
        /// 样品发放情况
        /// </summary>
        SampleStockOutReport,
        /// <summary>
        /// 销售单据汇总表
        /// </summary>
        SalesBillSummaryReport,
        /// <summary>
        /// 客户销售汇总表
        /// </summary>
        SalesSummaryByCustomerReport,
        /// <summary>
        /// 客户销售汇总月度图表
        /// </summary>
        SalesSummaryMonthlyReport,
        /// <summary>
        /// 客户销售汇总图表
        /// </summary>
        AnnualSalesSummaryByCustomerReport,
        /// <summary>
        /// 商品销售汇总表
        /// </summary>
        SalesSummaryByGoodsReport,
        /// <summary>
        /// 商品价格销量统计表
        /// </summary>
        SalesSummaryByGoodsPriceReport,
        /// <summary>
        /// 商品销售汇总图表
        /// </summary>
        AnnualSalesSummaryByGoodsReport,
        /// <summary>
        /// 客户商品销售汇总表
        /// </summary>
        GoodsSalesSummaryByCustomerReport,
        /// <summary>
        /// 入库单类型
        /// </summary>
        StockInBillType,
        /// <summary>
        /// 出库单类型
        /// </summary>
        StockOutBillType,
        /// <summary>
        /// 订货单类型
        /// </summary>
        OrderType,
        /// <summary>
        /// 提醒列表
        /// </summary>
        AlertQuery,
        /// <summary>
        /// 权限设置
        /// </summary>
        PermissionSetting,
        /// <summary>
        /// 关于
        /// </summary>
        AboutBox,
        /// <summary>
        /// 出勤记录
        /// </summary>
        AttGeneralLog,
        /// <summary>
        /// 人员考勤
        /// </summary>
        StaffAttendance,
        /// <summary>
        /// 考勤明细
        /// </summary>
        AttendanceQuery,
        /// <summary>
        /// 班次时段设置
        /// </summary>
        SchClass,
        /// <summary>
        /// 人员排班
        /// </summary>
        StaffSchClass,
        /// <summary>
        /// 生产排程
        /// </summary>
        ProductionScheduling,
        /// <summary>
        /// 日程安排查询
        /// </summary>
        SchedulingQuery,
        /// <summary>
        /// 工资结算表
        /// </summary>
        WageBill,
        /// <summary>
        /// 工资结算表查询
        /// </summary>
        WageBillQuery,
        /// <summary>
        /// 考勤工资结算表
        /// </summary>
        AttWageBill,
        /// <summary>
        /// 考勤工资结算表查询
        /// </summary>
        AttWageBillQuery,


        /// <summary>
        /// 客户择样
        /// </summary>
        CustomerSamplePick,
        /// <summary>
        /// 择样记录
        /// </summary>
        SamplePickQuery,
        /// <summary>
        /// 样品扫描
        /// </summary>
        SampleScan,

        /// <summary>
        /// 系统状态
        /// </summary>
        SystemStatus,
    }
    /// <summary>
    /// 系统版本
    /// </summary>
    public enum ISnowSoftVersion
    {
        /// <summary>
        /// 所有功能-1
        /// </summary>
        [MemberDescription("所有功能", "ALL")]
        ALL = -1,
        /// <summary>
        /// 采购功能0
        /// </summary>
        [MemberDescription("采购功能", "Procurement")]
        Procurement = 0,
        /// <summary>
        /// 销售功能1
        /// </summary>
        [MemberDescription("销功能", "Sales")]
        Sales = 1,
        /// <summary>
        /// 销售管理（带成品出入库库存）2
        /// </summary>
        [MemberDescription("销售管理", "SalesManagement")]
        SalesManagement = 2,
        /// <summary>
        /// 进销存3
        /// </summary>
        [MemberDescription("原料", "PurchaseSellStock")]
        PurchaseSellStock = 3,
        /// <summary>
        /// 外加工4
        /// </summary>
        [MemberDescription("外加工", "Mold")]
        EMS = 4,
        /// <summary>
        /// 自动机5
        /// </summary>
        [MemberDescription("自动机", "FSM")]
        FSM = 5,
    }

    /// <summary>
    /// 主菜单项
    /// </summary>
    public enum MainMenuName
    {
        /// <summary>
        /// 公司资料
        /// </summary>
        Company = 1,
        /// <summary>
        /// 职工资料
        /// </summary>
        Staff = 2,
        /// <summary>
        /// 货品资料
        /// </summary>
        Goods = 3,
        /// <summary>
        /// 入库单录入
        /// </summary>
        InStoreBill = 4,
        /// <summary>
        /// 入库单查询
        /// </summary>
        InStoreQuery = 5,
    }

    /// <summary>
    /// 统计列
    /// </summary>
    public enum SummaryItemColumns
    {
        /// <summary>
        /// 箱数
        /// </summary>
        箱数,
        /// <summary>
        /// 订货箱数
        /// </summary>
        订货箱数,
        /// <summary>
        /// 待发箱数
        /// </summary>
        待发箱数,
        /// <summary>
        /// 可用箱数
        /// </summary>
        可用箱数,
        /// <summary>
        /// 数量
        /// </summary>
        数量,
        /// <summary>
        /// 总数量
        /// </summary>
        总数量,
        /// <summary>
        /// 布产数量
        /// </summary>
        布产数量,
        /// <summary>
        /// 可用数量
        /// </summary>
        可用数量,
        /// <summary>
        /// 金额
        /// </summary>
        金额,
        /// <summary>
        /// 应收金额
        /// </summary>
        应收金额,
        /// <summary>
        /// 实收金额
        /// </summary>
        实收金额,
        /// <summary>
        /// 去税金额
        /// </summary>
        去税金额,
        /// <summary>
        /// 入库数量
        /// </summary>
        入库数量,
        /// <summary>
        /// 入库金额
        /// </summary>
        入库金额,
        /// <summary>
        /// 出库数量
        /// </summary>
        出库数量,
        /// <summary>
        /// 出库金额
        /// </summary>
        出库金额,

        /// <summary>
        /// 品名
        /// </summary>
        品名,
        /// <summary>
        /// 应收货品数量
        /// </summary>
        应收货品数量,
        /// <summary>
        /// 本日发料数量
        /// </summary>
        本日发料数量,
        /// <summary>
        /// 本日交货数量
        /// </summary>
        本日交货数量,
        /// <summary>
        /// 已用物料数量
        /// </summary>
        已用物料数量,
        /// <summary>
        /// 本日退料数量
        /// </summary>
        本日退料数量,
        /// <summary>
        /// 未收货品数量
        /// </summary>
        未收货品数量,
        /// <summary>
        /// 厂商存料数量
        /// </summary>
        厂商存料数量,

        /// <summary>
        /// 单据金额
        /// </summary>
        单据金额,
        /// <summary>
        /// 已收金额
        /// </summary>
        已收金额,
        /// <summary>
        /// 未收金额
        /// </summary>
        未收金额,
        /// <summary>
        /// 本次收款
        /// </summary>
        本次收款,
        /// <summary>
        /// 已付金额
        /// </summary>
        已付金额,
        /// <summary>
        /// 未付金额
        /// </summary>
        未付金额,
        /// <summary>
        /// 本次付款
        /// </summary>
        本次付款,
        /// <summary>
        /// 重量
        /// </summary>
        重量,
        /// <summary>
        /// 重量_斤
        /// </summary>
        重量_斤,
        /// <summary>
        /// 模数
        /// </summary>
        模数,
        /// <summary>
        /// 应产模数
        /// </summary>
        应产模数,
        /// <summary>
        /// 实产模数
        /// </summary>
        实产模数,
        /// <summary>
        /// 当班金额
        /// </summary>
        当班金额,
        /// <summary>
        /// 基本工资
        /// </summary>
        基本工资,
        /// <summary>
        /// 福利
        /// </summary>
        福利,
        /// <summary>
        /// 扣款
        /// </summary>
        扣款,
        /// <summary>
        /// 代扣社保
        /// </summary>
        代扣社保,
        /// <summary>
        /// 代扣个税
        /// </summary>
        代扣个税,
        /// <summary>
        /// 实发工资
        /// </summary>
        实发工资,
        /// <summary>
        /// 迟到分钟数
        /// </summary>
        迟到分钟数,
        /// <summary>
        /// 早退分钟数
        /// </summary>
        早退分钟数,
    }

    /// <summary>
    /// 单据按钮状态
    /// </summary>
    //public enum BillBtnStatus
    //{
    //    /// <summary>
    //    /// 添加
    //    /// </summary>
    //    Add = 0,
    //    /// <summary>
    //    /// 编辑
    //    /// </summary>
    //    Edit = 1,
    //    /// <summary>
    //    /// 保存
    //    /// </summary>
    //    Save = 2,
    //    /// <summary>
    //    /// 审核
    //    /// </summary>
    //    Audit = 3,
    //    /// <summary>
    //    /// 打印
    //    /// </summary>
    //    Print = 4,
    //    /// <summary>
    //    /// 删除
    //    /// </summary>
    //    Delete = 5,
    //}
    public enum StatusEnum
    {
        /// <summary>
        /// 有效
        /// </summary>
        [MemberDescription("有效", "Valid")]
        Valid = 0,
        /// <summary>
        /// 无效
        /// </summary>
        [MemberDescription("无效", "Invalid")]
        Invalid = 1,
    }
    /// <summary>
    /// 单据状态
    /// </summary>
    public enum BillStatus
    {
        /// <summary>
        /// 未审核
        /// </summary>
        [MemberDescription("未审核", "UnAudited")]
        UnAudited = 0,
        /// <summary>
        /// 已审核
        /// </summary>
        [MemberDescription("已审核", "Audited")]
        Audited = 1,
        /// <summary>
        /// 未结清
        /// </summary>
        [MemberDescription("未结清", "UnCleared")]
        UnCleared = 2,
        /// <summary>
        /// 已结账
        /// </summary>
        [MemberDescription("已结账", "Cleared")]
        Cleared = 3,
    }


    /// <summary>
    /// 工资状态
    /// </summary>
    public enum WageStatus
    {
        /// <summary>
        /// 未结算
        /// </summary>
        [MemberDescription("未结算", "UnCleared")]
        UnClosed = 0,
        /// <summary>
        /// 已结算
        /// </summary>
        [MemberDescription("已结算", "Cleared")]
        Closed = 1,
    }

    public enum ButtonType
    {
        /// <summary>
        /// 添加
        /// </summary>
        [MemberDescription("添加", "Add")]
        btnAdd = 0,
        /// <summary>
        /// 修改
        /// </summary>
        [MemberDescription("修改", "Edit")]
        btnEdit = 1,
        /// <summary>
        /// 保存
        /// </summary>
        [MemberDescription("保存", "Save")]
        btnSave = 2,
        /// <summary>
        /// 审核
        /// </summary>
        [MemberDescription("审核", "Audit")]
        btnAudit = 3,
        /// <summary>
        /// 打印
        /// </summary>
        [MemberDescription("打印", "Print")]
        btnPrint = 4,
        /// <summary>
        /// 删除
        /// </summary>
        [MemberDescription("删除", "Del")]
        btnDel = 5,
    }

    /// <summary>
    /// BOM清单类型
    /// </summary>
    public enum BOMType
    {
        /// <summary>
        /// 货品装配BOM0
        /// </summary>
        [MemberDescription("货品装配", "BOM")]
        BOM = 0,
        /// <summary>
        /// 模具BOM1
        /// </summary>
        [MemberDescription("模具清单", "MoldList")]
        MoldList = 1,
        /// <summary>
        /// 模具原料BOM2
        /// </summary>
        [MemberDescription("模具原料", "MoldMaterial")]
        MoldMaterial = 2,
        /// <summary>
        /// 材料装配BOM3
        /// </summary>
        [MemberDescription("材料装配", "Assemble")]
        Assemble = 3,
    }

    /// <summary>
    /// 订单类型
    /// </summary>
    public enum OrderBillType
    {
        /// <summary>
        /// 销售订单
        /// </summary>
        [MemberDescription("销售订单", "Order")]
        Order = 0,
        /// <summary>
        /// 自动机生产订单
        /// </summary>
        [MemberDescription("自动机生产订单", "FSM")]
        FSM = 1,
        /// <summary>
        /// 成品布产单
        /// </summary>
        [MemberDescription("成品布产单", "ProductionOrder")]
        ProductionOrder = 2,
    }
    /// <summary>
    /// 入库单类型
    /// </summary>
    //public enum InStoreBillType
    //{
    //    /// <summary>
    //    /// 成品入库
    //    /// </summary>
    //    FG = 0,
    //    /// <summary>
    //    /// 半成品入库
    //    /// </summary>
    //    SFG = 1,
    //    /// <summary>
    //    /// 退货入库
    //    /// </summary>
    //    Refund = 2,
    //    /// <summary>
    //    /// 补差单入库
    //    /// </summary>
    //    Variance = 3,
    //}

    /// <summary>
    /// 出库单类型
    /// </summary>
    //public enum OutStoreBillType
    //{
    //    /// <summary>
    //    /// 销售发货
    //    /// </summary>
    //    Sales = 0,
    //    /// <summary>
    //    /// 样品发货
    //    /// </summary>
    //    SampleDist = 1,
    //    /// <summary>
    //    /// 半成品出库
    //    /// </summary>
    //    SFG = 2,
    //}
    /// <summary>
    /// 订货单类型
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 紧急
        /// </summary>
        Emergency = 1,
    }
    /// <summary>
    /// 客户类型
    /// </summary>
    public enum CustomerType
    {
        /// <summary>
        /// 内销
        /// </summary>
        DomesticSales = 0,
        /// <summary>
        /// 外销
        /// </summary>
        ExportSales = 1,
    }
    /// <summary>
    /// 供应商类型
    /// </summary>
    public enum SupplierType
    {
        /// <summary>
        /// 采购
        /// </summary>
        Purchase = 0,
        /// <summary>
        /// 外加工
        /// </summary>
        EMS = 1,
        /// <summary>
        /// 自动机
        /// </summary>
        FSM = 2,
    }

    /// <summary>
    /// 业务往来类型
    /// </summary>
    public enum BusinessContactType
    {
        /// <summary>
        /// 客户
        /// </summary>
        Customer = 0,
        /// <summary>
        /// 供应商
        /// </summary>
        Supplier = 1,
    }
    /// <summary>
    /// 货品大类
    /// </summary>
    public enum GoodsBigType
    {
        /// <summary>
        /// 所有-1
        /// </summary>
        [MemberDescription("所有", "All")]
        All = -1,
        /// <summary>
        /// 成品0
        /// </summary>
        [MemberDescription("成品", "Goods")]
        Goods = 0,
        /// <summary>
        /// 包装资料1
        /// </summary>
        [MemberDescription("包装资料", "SFGoods")]
        SFGoods = 1,
        /// <summary>
        /// 装配材料2
        /// </summary>
        [MemberDescription("装配材料", "Stuff")]
        Stuff = 2,
        /// <summary>
        /// 原料3
        /// </summary>
        [MemberDescription("原料", "Material")]
        Material = 3,
        /// <summary>
        /// 模具4
        /// </summary>
        [MemberDescription("模具", "Mold")]
        Mold = 4,
        /// <summary>
        /// 筐袋5
        /// </summary>
        [MemberDescription("筐袋", "Basket")]
        Basket = 5,
    }
    /// <summary>
    /// 班次
    /// </summary>
    public enum WorkShiftsType
    {
        /// <summary>
        /// 空0
        /// </summary>
        [MemberDescription("  ", " ")]
        Empty = 0,
        /// <summary>
        /// 早班1
        /// </summary>
        [MemberDescription("早班", "ForeShift")]
        ForeShift = 1,
        /// <summary>
        /// 上午班2
        /// </summary>
        [MemberDescription("上午班", "MorningShift")]
        MorningShift = 2,
        /// <summary>
        /// 下午班3
        /// </summary>
        [MemberDescription("下午班", "AfternoonShift")]
        AfternoonShift = 3,
        /// <summary>
        /// 晚班4
        /// </summary>
        [MemberDescription("晚班", "NightShift")]
        NightShift = 4,
    }
    /// <summary>
    /// 机号
    /// </summary>
    public enum MachineType
    {
        /// <summary>
        /// 空0
        /// </summary>
        [MemberDescription(" ", " ")]
        Empty = 0,
        /// <summary>
        /// 1号机1
        /// </summary>
        [MemberDescription("1号机", "One")]
        One = 1,
        /// <summary>
        /// 2号机2
        /// </summary>
        [MemberDescription("2号机", "Two")]
        Two = 2,
        /// <summary>
        /// 3号机3
        /// </summary>
        [MemberDescription("3号机", "Three")]
        Three = 3,
        /// <summary>
        /// 4号机4
        /// </summary>
        [MemberDescription("4号机", "Four")]
        Four = 4,
        /// <summary>
        /// 5号机5
        /// </summary>
        [MemberDescription("5号机", "Five")]
        Five = 5,
        /// <summary>
        /// 6号机6
        /// </summary>
        [MemberDescription("6号机", "Six")]
        Six = 6,
        /// <summary>
        /// 7号机7
        /// </summary>
        [MemberDescription("7号机", "Seven")]
        Seven = 7,
        /// <summary>
        /// 8号机8
        /// </summary>
        [MemberDescription("8号机", "Eight")]
        Eight = 8,
        /// <summary>
        /// 9号机9
        /// </summary>
        [MemberDescription("9号机", "Nine")]
        Nine = 9,
        /// <summary>
        /// 10号机10
        /// </summary>
        [MemberDescription("10号机", "Ten")]
        Ten = 10,
        /// <summary>
        /// 两台机器11
        /// </summary>
        [MemberDescription("两台机器", "Both")]
        Both = 11,
    }
    /// <summary>
    /// 考勤状态
    /// </summary>
    public enum AttStatusType
    {
        /// <summary>
        /// 有效0
        /// </summary>
        [MemberDescription("有效", "White")]  //描述2用于表示颜色
        Valid = 0,
        /// <summary>
        /// 迟到1
        /// </summary>
        [MemberDescription("迟到", "Pink")]
        Late = 1,
        /// <summary>
        /// 早退2
        /// </summary>
        [MemberDescription("早退", "Khaki")]
        Early = 2,
        /// <summary>
        /// 迟到早退3
        /// </summary>
        [MemberDescription("迟到早退", "SalMon")]
        LateEarly = 3,
        /// <summary>
        /// 未签到4
        /// </summary>
        [MemberDescription("未签到", "SkyBlue")]
        NoCheckIn = 4,
        /// <summary>
        /// 未签退5
        /// </summary>
        [MemberDescription("未签退", "DeepSkyBlue")]
        NoCheckOut = 5,
        /// <summary>
        /// 旷工6
        /// </summary>
        [MemberDescription("旷工", "Plum")]
        Absent = 6,
        /// <summary>
        /// 忘打卡7
        /// </summary>
        [MemberDescription("忘打卡", "DarkGray")]
        Forget = 7,
        /// <summary>
        /// 请假8
        /// </summary>
        [MemberDescription("请假", "SpringGreen")]
        Leave = 8,
    }

    /// <summary>
    /// 工资类型
    /// </summary>
    public enum WageType
    {
        /// <summary>
        /// 月薪0
        /// </summary>
        [MemberDescription("月薪", "SpringGreen")]
        MonthlyWage = 0,
        /// <summary>
        /// 计时1
        /// </summary>
        [MemberDescription("计时", "Pink")]
        TimeWage = 1,
        /// <summary>
        /// 计件2
        /// </summary>
        [MemberDescription("计件", "SkyBlue")]
        PieceWage = 2,
    }
    /// <summary>
    /// 工资类型
    /// </summary>
    public enum CompanyType
    {
        /// <summary>
        /// 玩具厂0
        /// </summary>
        [MemberDescription("玩具厂", "Factory")]
        Factory = 0,
        /// <summary>
        /// 贸易公司1
        /// </summary>
        [MemberDescription("贸易公司", "Trade")]
        Trade = 1,
    }

    public enum StocktakingStatusEnum
    {
        [MemberDescription("初盘", "First")]
        First = 0,
        [MemberDescription("复盘", "Check")]
        Check = 1,
        [MemberDescription("完成", "Finish")]
        Finish = 2,
    }

    public enum WarehouseEnum
    {
        [MemberDescription("卖场", "673e26c6-41cc-4d4f-97b5-4cc2585a517e")]
        Shop = 0,
        [MemberDescription("仓库", "53dc81eb-3a58-4ab5-af1a-8cb7dca8b64c")]
        Warehouse = 1,
    }

    /// <summary>
    /// 账面库存表
    /// </summary>
    public enum InventoryEnum
    {
        [MemberDescription("商品编码", "GoodsCode")]
        GoodsCode,
        [MemberDescription("商品名称", "GoodsName")]
        GoodsName,
        [MemberDescription("规格", "SPEC")]
        SPEC,
        [MemberDescription("单位", "Unit")]
        Unit,
        [MemberDescription("条形码", "Barcode")]
        Barcode,
        [MemberDescription("库存数量", "StockQty")]
        StockQty,
    }

    /// <summary>
    /// 账面库存日志表
    /// </summary>
    public enum VInventoryLogEnum
    {
        [MemberDescription("单据编号", "BillNo")]
        BillNo,
        [MemberDescription("单据日期", "BillDate")]
        BillDate,
        [MemberDescription("门店编码", "DeptCode")]
        DeptCode,
        [MemberDescription("门店名称", "DeptName")]
        DeptName,
        [MemberDescription("商品编码", "GoodsCode")]
        GoodsCode,
        [MemberDescription("商品名称", "GoodsName")]
        GoodsName,
        [MemberDescription("条形码", "Barcode")]
        Barcode,
        [MemberDescription("规格", "SPEC")]
        SPEC,
        [MemberDescription("单位", "Unit")]
        Unit,
        [MemberDescription("库存数量", "StockQty")]
        StockQty,
    }

    /// <summary>
    /// 盘点差异表
    /// </summary>
    public enum ProfitAndLossEnum
    {
        [MemberDescription("单据编号", "BillNo")]
        BillNo,
        [MemberDescription("单据日期", "BillDate")]
        BillDate,
        [MemberDescription("门店编码", "DeptCode")]
        DeptCode,
        [MemberDescription("门店名称", "DeptName")]
        DeptName,
        [MemberDescription("仓位", "WarehouseName")]
        WarehouseName,
        [MemberDescription("商品品类", "Category")]
        Category,
        [MemberDescription("商品编码", "GoodsCode")]
        GoodsCode,
        [MemberDescription("商品名称", "GoodsName")]
        GoodsName,
        //[MemberDescription("条码", "Barcode")]
        //Barcode,
        //[MemberDescription("规格", "SPEC")]
        //SPEC,
        //[MemberDescription("单位", "Unit")]
        //Unit,
        [MemberDescription("价格", "Price")]
        Price,
        [MemberDescription("账面数量", "StockQty")]
        StockQty,
        [MemberDescription("账面金额", "StockAMT")]
        StockAMT,
        [MemberDescription("实盘数量", "CheckQty")]
        CheckQty,
        [MemberDescription("实盘金额", "CheckAMT")]
        CheckAMT,
        [MemberDescription("实盘差异数量", "DiffQty")]
        DiffQty,
        [MemberDescription("实盘差异金额", "DiffAMT")]
        DiffAMT,
        [MemberDescription("在途数量", "TransitQty")]
        TransitQty,
        [MemberDescription("在途金额", "TransitAMT")]
        TransitAMT,
        [MemberDescription("已退货未减库存数量", "ReturnedQty")]
        ReturnedQty,
        [MemberDescription("已退货未减库存金额", "ReturnedAMT")]
        ReturnedAMT,
        [MemberDescription("已到货未加库存数量", "NonInStoreQty")]
        NonInStoreQty,
        [MemberDescription("已到货未加库存金额", "NonInStoreAMT")]
        NonInStoreAMT,
        [MemberDescription("已调入未到店数量", "NonArrivalQty")]
        NonArrivalQty,
        [MemberDescription("已调入未到店金额", "NonArrivalAMT")]
        NonArrivalAMT,
        [MemberDescription("外部已提未售数量", "ExtraPresellQty")]
        ExtraPresellQty,
        [MemberDescription("外部已提未售金额", "ExtraPresellAMT")]
        ExtraPresellAMT,
        [MemberDescription("外部已售未提数量", "ExtraSoldQty")]
        ExtraSoldQty,
        [MemberDescription("内部已售未提金额", "ExtraSoldAMT")]
        ExtraSoldAMT,
        [MemberDescription("内部已提未售数量", "IntraPresellQty")]
        IntraPresellQty,
        [MemberDescription("内部已提未售金额", "IntraPresellAMT")]
        IntraPresellAMT,
        [MemberDescription("内部已售未提数量", "IntraSoldQty")]
        IntraSoldQty,
        [MemberDescription("内部已售未提金额", "IntraSoldAMT")]
        IntraSoldAMT,
        [MemberDescription("已调出未销账数量", "NonChargeOffQty")]
        NonChargeOffQty,
        [MemberDescription("已调出未销账金额", "NonChargeOffAMT")]
        NonChargeOffAMT,
        [MemberDescription("已调入未入账数量", "NonRecordedQty")]
        NonRecordedQty,
        [MemberDescription("已调入未入账金额", "NonRecordedAMT")]
        NonRecordedAMT,
        [MemberDescription("团购数量", "GroupBuyingQty")]
        GroupBuyingQty,
        [MemberDescription("团购金额", "GroupBuyingAMT")]
        GroupBuyingAMT,
        [MemberDescription("灾害数量", "DisasterQty")]
        DisasterQty,
        [MemberDescription("灾害金额", "DisasterAMT")]
        DisasterAMT,
        [MemberDescription("其他数量", "OtherQty")]
        OtherQty,
        [MemberDescription("其他金额", "OtherAMT")]
        OtherAMT,
        [MemberDescription("实际差异数量", "FinalDiffQty")]
        FinalDiffQty,
        [MemberDescription("实际差异金额", "FinalDiffAMT")]
        FinalDiffAMT,
        [MemberDescription("过期变质破损数量", "ScrapQty")]
        ScrapQty,
        [MemberDescription("备注", "Remark")]
        Remark,
    }

    /// <summary>
    /// 盘点差异日志表
    /// </summary>
    public enum VProfitAndLossLogEnum
    {
        [MemberDescription("单据编号", "BillNo")]
        BillNo,
        [MemberDescription("单据日期", "BillDate")]
        BillDate,
        [MemberDescription("门店编码", "DeptCode")]
        DeptCode,
        [MemberDescription("门店名称", "DeptName")]
        DeptName,
        //[MemberDescription("仓位", "WarehouseName")]
        //WarehouseName,
        [MemberDescription("商品品类", "Category")]
        Category,
        [MemberDescription("商品编码", "GoodsCode")]
        GoodsCode,
        [MemberDescription("商品名称", "GoodsName")]
        GoodsName,
        [MemberDescription("价格", "Price")]
        Price,
        [MemberDescription("账面数量", "StockQty")]
        StockQty,
        [MemberDescription("账面金额", "StockAMT")]
        StockAMT,
        [MemberDescription("实盘数量", "CheckQty")]
        CheckQty,
        [MemberDescription("实盘金额", "CheckAMT")]
        CheckAMT,
        [MemberDescription("实盘差异数量", "DiffQty")]
        DiffQty,
        [MemberDescription("实盘差异金额", "DiffAMT")]
        DiffAMT,
        [MemberDescription("在途数量", "TransitQty")]
        TransitQty,
        [MemberDescription("在途金额", "TransitAMT")]
        TransitAMT,
        [MemberDescription("已退货未减库存数量", "ReturnedQty")]
        ReturnedQty,
        [MemberDescription("已退货未减库存金额", "ReturnedAMT")]
        ReturnedAMT,
        [MemberDescription("已到货未加库存数量", "NonInStoreQty")]
        NonInStoreQty,
        [MemberDescription("已到货未加库存金额", "NonInStoreAMT")]
        NonInStoreAMT,
        [MemberDescription("已调入未到店数量", "NonArrivalQty")]
        NonArrivalQty,
        [MemberDescription("已调入未到店金额", "NonArrivalAMT")]
        NonArrivalAMT,
        [MemberDescription("外部已提未售数量", "ExtraPresellQty")]
        ExtraPresellQty,
        [MemberDescription("外部已提未售金额", "ExtraPresellAMT")]
        ExtraPresellAMT,
        [MemberDescription("外部已售未提数量", "ExtraSoldQty")]
        ExtraSoldQty,
        [MemberDescription("内部已售未提金额", "ExtraSoldAMT")]
        ExtraSoldAMT,
        [MemberDescription("内部已提未售数量", "IntraPresellQty")]
        IntraPresellQty,
        [MemberDescription("内部已提未售金额", "IntraPresellAMT")]
        IntraPresellAMT,
        [MemberDescription("内部已售未提数量", "IntraSoldQty")]
        IntraSoldQty,
        [MemberDescription("内部已售未提金额", "IntraSoldAMT")]
        IntraSoldAMT,
        [MemberDescription("已调出未销账数量", "NonChargeOffQty")]
        NonChargeOffQty,
        [MemberDescription("已调出未销账金额", "NonChargeOffAMT")]
        NonChargeOffAMT,
        [MemberDescription("已调入未入账数量", "NonRecordedQty")]
        NonRecordedQty,
        [MemberDescription("已调入未入账金额", "NonRecordedAMT")]
        NonRecordedAMT,
        [MemberDescription("团购数量", "GroupBuyingQty")]
        GroupBuyingQty,
        [MemberDescription("团购金额", "GroupBuyingAMT")]
        GroupBuyingAMT,
        [MemberDescription("灾害数量", "DisasterQty")]
        DisasterQty,
        [MemberDescription("灾害金额", "DisasterAMT")]
        DisasterAMT,
        [MemberDescription("其他数量", "OtherQty")]
        OtherQty,
        [MemberDescription("其他金额", "OtherAMT")]
        OtherAMT,
        [MemberDescription("实际差异数量", "FinalDiffQty")]
        FinalDiffQty,
        [MemberDescription("实际差异金额", "FinalDiffAMT")]
        FinalDiffAMT,
        [MemberDescription("过期变质破损数量", "ScrapQty")]
        ScrapQty,
        [MemberDescription("备注", "Remark")]
        Remark,
    }

    /// <summary>
    /// 盘点表
    /// </summary>
    public enum StocktakingEnum
    {
        [MemberDescription("门店编码", "DeptCode")]
        DeptCode,
        [MemberDescription("门店名称", "DeptName")]
        DeptName,
        [MemberDescription("仓位", "WarehouseName")]
        WarehouseName,
        [MemberDescription("编码", "GoodsCode")]
        GoodsCode,
        [MemberDescription("品名", "GoodsName")]
        GoodsName,
        [MemberDescription("条码", "Barcode")]
        Barcode,
        [MemberDescription("规格", "SPEC")]
        SPEC,
        [MemberDescription("单位", "Unit")]
        Unit,
        [MemberDescription("库存", "StockQty")]
        StockQty,
        [MemberDescription("实盘数", "CheckQty")]
        CheckQty,
        [MemberDescription("价格", "Price")]
        Price,
        [MemberDescription("扫描单", "ScanBill")]
        ScanBill,
        [MemberDescription("作业者", "Checker")]
        Checker,
        [MemberDescription("时间", "CheckingDate")]
        CheckingDate,
    }


    /// <summary>
    /// 盘点日志表
    /// </summary>
    public enum StocktakingLogHdEnum
    {
        [MemberDescription("单据编号", "BillNo")]
        BillNo,
        [MemberDescription("单据日期", "BillDate")]
        BillDate,
        [MemberDescription("盘点人", "Checker")]
        Checker,
        [MemberDescription("盘点时间", "CheckingTime")]
        CheckingTime,
        [MemberDescription("状态", "Status")]
        Status,
    }

    /// <summary>
    /// 盘点日志明细表
    /// </summary>
    public enum VStocktakingLogEnum
    {
        [MemberDescription("门店编码", "DeptCode")]
        DeptCode,
        [MemberDescription("门店名称", "DeptName")]
        DeptName,
        [MemberDescription("仓位", "WarehouseName")]
        WarehouseName,
        [MemberDescription("编码", "GoodsCode")]
        GoodsCode,
        [MemberDescription("品名", "GoodsName")]
        GoodsName,
        [MemberDescription("条码", "Barcode")]
        Barcode,
        [MemberDescription("规格", "SPEC")]
        SPEC,
        [MemberDescription("单位", "Unit")]
        Unit,
        [MemberDescription("库存", "StockQty")]
        StockQty,
        [MemberDescription("实盘数", "CheckQty")]
        CheckQty,
        [MemberDescription("价格", "Price")]
        Price,
        [MemberDescription("扫描单", "ScanBill")]
        ScanBill,
        [MemberDescription("作业者", "Checker")]
        Checker,
        [MemberDescription("时间", "CheckingDate")]
        CheckingDate,
    }

    /// <summary>
    /// 损耗确认表
    /// </summary>
    public enum InventoryLossEnum
    {
        [MemberDescription("门店编码", "DeptCode")]
        DeptCode,
        [MemberDescription("门店名称", "DeptName")]
        DeptName,
        [MemberDescription("商品品类", "Category")]
        Category,
        [MemberDescription("商品编码", "GoodsCode")]
        GoodsCode,
        [MemberDescription("商品名称", "GoodsName")]
        GoodsName,
        [MemberDescription("账面数量", "StockQty")]
        StockQty,
        [MemberDescription("实盘数量", "CheckQty")]
        CheckQty,
        [MemberDescription("实盘差异数量", "DiffQty")]
        DiffQty,
        [MemberDescription("损耗数量", "LossQty")]
        LossQty,
        [MemberDescription("价格", "Price")]
        Price,
        [MemberDescription("账面金额", "StockAMT")]
        StockAMT,
        [MemberDescription("实盘金额", "CheckAMT")]
        CheckAMT,
        [MemberDescription("实盘差异金额", "DiffAMT")]
        DiffAMT,
        [MemberDescription("盘点日前30天销售金额", "PreCheckSellAMT")]
        PreCheckSellAMT,
        [MemberDescription("损耗金额", "LossAMT")]
        LossAMT,
        [MemberDescription("盘点损耗率", "LossRate")]
        LossRate,
        [MemberDescription("备注", "Remark")]
        Remark,
    }

    /// <summary>
    /// 损耗确认单日志表
    /// </summary>
    public enum VInventoryLossLogEnum
    {
        [MemberDescription("单据编号", "BillNo")]
        BillNo,
        [MemberDescription("单据日期", "BillDate")]
        BillDate,
        [MemberDescription("门店编码", "DeptCode")]
        DeptCode,
        [MemberDescription("门店名称", "DeptName")]
        DeptName,
        [MemberDescription("商品品类", "Category")]
        Category,
        [MemberDescription("商品编码", "GoodsCode")]
        GoodsCode,
        [MemberDescription("商品名称", "GoodsName")]
        GoodsName,
        [MemberDescription("账面数量", "StockQty")]
        StockQty,
        [MemberDescription("实盘数量", "CheckQty")]
        CheckQty,
        [MemberDescription("实盘差异数量", "DiffQty")]
        DiffQty,
        [MemberDescription("损耗数量", "LossQty")]
        LossQty,
        [MemberDescription("价格", "Price")]
        Price,
        [MemberDescription("账面金额", "StockAMT")]
        StockAMT,
        [MemberDescription("实盘金额", "CheckAMT")]
        CheckAMT,
        [MemberDescription("实盘差异金额", "DiffAMT")]
        DiffAMT,
        [MemberDescription("盘点日前30天销售金额", "PreCheckSellAMT")]
        PreCheckSellAMT,
        [MemberDescription("损耗金额", "LossAMT")]
        LossAMT,
        [MemberDescription("盘点损耗率", "LossRate")]
        LossRate,
        [MemberDescription("备注", "Remark")]
        Remark,
    }

    /// <summary>
    /// 未上架商品确认单
    /// </summary>
    public enum UnlistedGoodsEnum
    {
        [MemberDescription("单据编号", "BillNo")]
        BillNo,
        [MemberDescription("单据日期", "BillDate")]
        BillDate,
        [MemberDescription("门店编码", "DeptCode")]
        DeptCode,
        [MemberDescription("门店名称", "DeptName")]
        DeptName,
        [MemberDescription("商品品类", "Category")]
        Category,
        [MemberDescription("商品编码", "GoodsCode")]
        GoodsCode,
        [MemberDescription("商品名称", "GoodsName")]
        GoodsName,
        [MemberDescription("价格", "Price")]
        Price,
        [MemberDescription("库存数量", "StockQty")]
        StockQty,
        [MemberDescription("库存金额", "StockAMT")]
        StockAMT,
        [MemberDescription("未上架商品数量", "UnlistedQty")]
        UnlistedQty,
        [MemberDescription("未上架商品金额", "UnlistedAMT")]
        UnlistedAMT,
        [MemberDescription("过期变质破损数量", "ScrapQty")]
        ScrapQty,
        [MemberDescription("备注", "Remark")]
        Remark,
    }

    /// <summary>
    /// 未上架商品日志表
    /// </summary>
    public enum VUnlistedGoodsLogEnum
    {
        [MemberDescription("单据编号", "BillNo")]
        BillNo,
        [MemberDescription("单据日期", "BillDate")]
        BillDate,
        [MemberDescription("门店编码", "DeptCode")]
        DeptCode,
        [MemberDescription("门店名称", "DeptName")]
        DeptName,
        [MemberDescription("商品品类", "Category")]
        Category,
        [MemberDescription("商品编码", "GoodsCode")]
        GoodsCode,
        [MemberDescription("商品名称", "GoodsName")]
        GoodsName,
        [MemberDescription("价格", "Price")]
        Price,
        [MemberDescription("库存数量", "StockQty")]
        StockQty,
        [MemberDescription("库存金额", "StockAMT")]
        StockAMT,
        [MemberDescription("未上架商品数量", "UnlistedQty")]
        UnlistedQty,
        [MemberDescription("未上架商品金额", "UnlistedAMT")]
        UnlistedAMT,
        [MemberDescription("过期变质破损数量", "ScrapQty")]
        ScrapQty,
        [MemberDescription("备注", "Remark")]
        Remark,
    }

    /// <summary>
    /// 商品资料
    /// </summary>
    public enum GoodsEnum
    {
        [MemberDescription("图片", "Pic")]
        Pic,
        [MemberDescription("商品编码", "Code")]
        Code,
        [MemberDescription("商品品类", "Category")]
        Category,
        [MemberDescription("商品名称", "Name")]
        Name,
        [MemberDescription("售价", "Price")]
        Price,
        [MemberDescription("条形码", "Barcode")]
        Barcode,
        [MemberDescription("规格", "SPEC")]
        SPEC,
        [MemberDescription("单位", "Unit")]
        Unit,
        //[MemberDescription("库存数量", "Qty")]
        //Qty,
        [MemberDescription("备注", "Remark")]
        Remark,
        [MemberDescription("已删除", "IsDel")]
        IsDel,
    }

    /// <summary>
    /// 门店资料
    /// </summary>
    public enum DepartmentEnum
    {
        [MemberDescription("门店编码", "Code")]
        Code,
        [MemberDescription("门店名称", "Name")]
        Name,
        [MemberDescription("加油站标准编码", "StationCode")]
        StationCode,
        [MemberDescription("省份", "Province")]
        Province,
        [MemberDescription("城市", "City")]
        City,
        [MemberDescription("片区", "County")]
        County,
        [MemberDescription("门店定级", "Level")]
        Level,
        [MemberDescription("地理位置", "Address")]
        Address,
        [MemberDescription("BW系统中地理位置", "BWAddress")]
        BWAddress,
        [MemberDescription("商圈分类", "Category")]
        Category,
        [MemberDescription("备注", "Remark")]
        Remark,
        [MemberDescription("已删除", "IsDel")]
        IsDel,
    }

    /// <summary>
    /// 用户资料
    /// </summary>
    public enum UsersInfoEnum
    {
        [MemberDescription("照片", "Photo")]
        Photo,
        [MemberDescription("姓名", "Name")]
        Name,
        [MemberDescription("账号", "Code")]
        Code,
        [MemberDescription("职位", "Post")]
        Post,
        [MemberDescription("电话", "Tel")]
        Tel,
        [MemberDescription("地址", "Address")]
        Address,
        [MemberDescription("备注", "Remark")]
        Remark,
        [MemberDescription("已删除", "IsDel")]
        IsDel,
    }

    /// <summary>
    /// 差异原因
    /// </summary>
    public enum ReasonEnum
    {
        [MemberDescription("编码", "Code")]
        Code,
        [MemberDescription("名称", "Name")]
        Name,
        [MemberDescription("在途数量", "TransitQty")]
        TransitQty,
        [MemberDescription("已退货未减库存", "ReturnedQty")]
        ReturnedQty,
        [MemberDescription("已到货未加库存", "NonInStoreQty")]
        NonInStoreQty,
        [MemberDescription("已调入未到店", "NonArrivalQty")]
        NonArrivalQty,
        [MemberDescription("外部已提未售", "ExtraPresellQty")]
        ExtraPresellQty,
        [MemberDescription("外部已售未提", "ExtraSoldQty")]
        ExtraSoldQty,
        [MemberDescription("内部已提未售", "IntraPresellQty")]
        IntraPresellQty,
        [MemberDescription("内部已售未提", "IntraSoldQty")]
        IntraSoldQty,
        [MemberDescription("已调出未销账", "NonChargeOffQty")]
        NonChargeOffQty,
        [MemberDescription("已调入未入账", "NonRecordedQty")]
        NonRecordedQty,
        [MemberDescription("团购", "GroupBuyingQty")]
        GroupBuyingQty,
        [MemberDescription("灾害", "DisasterQty")]
        DisasterQty,
        [MemberDescription("其他", "OtherQty")]
        OtherQty,
        [MemberDescription("过期变质破损", "ScrapQty")]
        ScrapQty,
    }
}
