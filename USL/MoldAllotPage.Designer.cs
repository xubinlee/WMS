﻿namespace USL
{
    partial class MoldAllotPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer dockingContainer2 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer();
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dpSupplier = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.vSupplierBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.winExplorerView = new DevExpress.XtraGrid.Views.WinExplorer.WinExplorerView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col代码 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col名称 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col地址 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col邮编 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col联系人 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col联系电话 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col联系手机 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col传真 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col开户行 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col帐号 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col税号 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col付款地址 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col付款抬头 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col发票地址 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col备注 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dpBOM = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.bOMBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colParentGoodsID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGoodsID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLueGoods = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.goodsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSPEC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPCS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInnerBox = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceNoTax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAMTNoTax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.documentManager = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.documentGroup1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup(this.components);
            this.document1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            this.dpSupplier.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vSupplierBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.winExplorerView)).BeginInit();
            this.dpBOM.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bOMBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLueGoods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.goodsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.document1)).BeginInit();
            this.SuspendLayout();
            // 
            // dockManager
            // 
            this.dockManager.Form = this;
            this.dockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpSupplier,
            this.dpBOM});
            this.dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar"});
            // 
            // dpSupplier
            // 
            this.dpSupplier.Controls.Add(this.dockPanel1_Container);
            this.dpSupplier.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpSupplier.FloatVertical = true;
            this.dpSupplier.ID = new System.Guid("fde4d5c5-7ae7-48f4-bcb7-8c3b4c543394");
            this.dpSupplier.Location = new System.Drawing.Point(0, 0);
            this.dpSupplier.Name = "dpSupplier";
            this.dpSupplier.Options.ShowCloseButton = false;
            this.dpSupplier.OriginalSize = new System.Drawing.Size(250, 300);
            this.dpSupplier.Size = new System.Drawing.Size(250, 480);
            this.dpSupplier.Text = "供应商列表";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.gridControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(241, 453);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl1.DataSource = this.vSupplierBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.winExplorerView;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(241, 453);
            this.gridControl1.TabIndex = 22;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.winExplorerView});
            // 
            // vSupplierBindingSource
            // 
            //this.vSupplierBindingSource.DataSource = typeof(EDMX.VSupplier);
            // 
            // winExplorerView
            // 
            this.winExplorerView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.col代码,
            this.col名称,
            this.col地址,
            this.col邮编,
            this.col联系人,
            this.col联系电话,
            this.col联系手机,
            this.col传真,
            this.col开户行,
            this.col帐号,
            this.col税号,
            this.col付款地址,
            this.col付款抬头,
            this.col发票地址,
            this.col备注});
            this.winExplorerView.ColumnSet.DescriptionColumn = this.col名称;
            this.winExplorerView.ColumnSet.TextColumn = this.col代码;
            this.winExplorerView.GridControl = this.gridControl1;
            this.winExplorerView.Name = "winExplorerView";
            this.winExplorerView.OptionsBehavior.Editable = false;
            this.winExplorerView.OptionsFind.FindNullPrompt = "请输入查找内容...";
            this.winExplorerView.OptionsFind.ShowCloseButton = false;
            this.winExplorerView.OptionsFind.ShowFindButton = false;
            this.winExplorerView.OptionsView.Style = DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewStyle.Tiles;
            this.winExplorerView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.winExplorerView_FocusedRowChanged);
            this.winExplorerView.RowCountChanged += new System.EventHandler(this.winExplorerView_RowCountChanged);
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.Visible = true;
            this.colID.VisibleIndex = 0;
            // 
            // col代码
            // 
            this.col代码.FieldName = "代码";
            this.col代码.Name = "col代码";
            this.col代码.Visible = true;
            this.col代码.VisibleIndex = 1;
            // 
            // col名称
            // 
            this.col名称.FieldName = "名称";
            this.col名称.Name = "col名称";
            this.col名称.Visible = true;
            this.col名称.VisibleIndex = 2;
            // 
            // col地址
            // 
            this.col地址.FieldName = "地址";
            this.col地址.Name = "col地址";
            this.col地址.Visible = true;
            this.col地址.VisibleIndex = 3;
            // 
            // col邮编
            // 
            this.col邮编.FieldName = "邮编";
            this.col邮编.Name = "col邮编";
            this.col邮编.Visible = true;
            this.col邮编.VisibleIndex = 4;
            // 
            // col联系人
            // 
            this.col联系人.FieldName = "联系人";
            this.col联系人.Name = "col联系人";
            this.col联系人.Visible = true;
            this.col联系人.VisibleIndex = 5;
            // 
            // col联系电话
            // 
            this.col联系电话.FieldName = "联系电话";
            this.col联系电话.Name = "col联系电话";
            this.col联系电话.Visible = true;
            this.col联系电话.VisibleIndex = 6;
            // 
            // col联系手机
            // 
            this.col联系手机.FieldName = "联系手机";
            this.col联系手机.Name = "col联系手机";
            this.col联系手机.Visible = true;
            this.col联系手机.VisibleIndex = 7;
            // 
            // col传真
            // 
            this.col传真.FieldName = "传真";
            this.col传真.Name = "col传真";
            this.col传真.Visible = true;
            this.col传真.VisibleIndex = 8;
            // 
            // col开户行
            // 
            this.col开户行.FieldName = "开户行";
            this.col开户行.Name = "col开户行";
            this.col开户行.Visible = true;
            this.col开户行.VisibleIndex = 9;
            // 
            // col帐号
            // 
            this.col帐号.FieldName = "帐号";
            this.col帐号.Name = "col帐号";
            this.col帐号.Visible = true;
            this.col帐号.VisibleIndex = 10;
            // 
            // col税号
            // 
            this.col税号.FieldName = "税号";
            this.col税号.Name = "col税号";
            this.col税号.Visible = true;
            this.col税号.VisibleIndex = 11;
            // 
            // col付款地址
            // 
            this.col付款地址.FieldName = "付款地址";
            this.col付款地址.Name = "col付款地址";
            this.col付款地址.Visible = true;
            this.col付款地址.VisibleIndex = 12;
            // 
            // col付款抬头
            // 
            this.col付款抬头.FieldName = "付款抬头";
            this.col付款抬头.Name = "col付款抬头";
            this.col付款抬头.Visible = true;
            this.col付款抬头.VisibleIndex = 13;
            // 
            // col发票地址
            // 
            this.col发票地址.FieldName = "发票地址";
            this.col发票地址.Name = "col发票地址";
            this.col发票地址.Visible = true;
            this.col发票地址.VisibleIndex = 14;
            // 
            // col备注
            // 
            this.col备注.FieldName = "备注";
            this.col备注.Name = "col备注";
            this.col备注.Visible = true;
            this.col备注.VisibleIndex = 15;
            // 
            // dpBOM
            // 
            this.dpBOM.Controls.Add(this.dockPanel2_Container);
            this.dpBOM.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
            this.dpBOM.DockedAsTabbedDocument = true;
            this.dpBOM.FloatLocation = new System.Drawing.Point(272, 531);
            this.dpBOM.FloatVertical = true;
            this.dpBOM.ID = new System.Guid("599518cb-584c-4130-ae32-4e3ae94f2bf7");
            this.dpBOM.Location = new System.Drawing.Point(0, 0);
            this.dpBOM.Name = "dpBOM";
            this.dpBOM.Options.ShowCloseButton = false;
            this.dpBOM.OriginalSize = new System.Drawing.Size(300, 300);
            this.dpBOM.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dpBOM.SavedIndex = 1;
            this.dpBOM.Size = new System.Drawing.Size(384, 451);
            this.dpBOM.Text = "模具信息";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.gridControl);
            this.dockPanel2_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(384, 451);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // gridControl
            // 
            this.gridControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl.DataSource = this.bOMBindingSource;
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLueGoods});
            this.gridControl.Size = new System.Drawing.Size(384, 451);
            this.gridControl.TabIndex = 22;
            this.gridControl.UseEmbeddedNavigator = true;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // bOMBindingSource
            // 
            this.bOMBindingSource.DataSource = typeof(EDMX.BOM);
            // 
            // gridView
            // 
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colParentGoodsID,
            this.colGoodsID,
            this.colName,
            this.colSPEC,
            this.colQty,
            this.colUnit,
            this.colPCS,
            this.colInnerBox,
            this.colTotalQty,
            this.colPrice,
            this.colAMT,
            this.colPriceNoTax,
            this.colAMTNoTax,
            this.colRemark});
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsNavigation.AutoFocusNewRow = true;
            this.gridView.OptionsNavigation.AutoMoveRowFocus = false;
            this.gridView.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridView.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView.OptionsView.EnableAppearanceOddRow = true;
            this.gridView.OptionsView.ShowFooter = true;
            this.gridView.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.gridView_FocusedColumnChanged);
            this.gridView.RowDeleted += new DevExpress.Data.RowDeletedEventHandler(this.gridView_RowDeleted);
            this.gridView.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView_RowUpdated);
            this.gridView.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gridView_CustomUnboundColumnData);
            // 
            // colParentGoodsID
            // 
            this.colParentGoodsID.FieldName = "ParentGoodsID";
            this.colParentGoodsID.Name = "colParentGoodsID";
            this.colParentGoodsID.OptionsColumn.ReadOnly = true;
            // 
            // colGoodsID
            // 
            this.colGoodsID.AppearanceHeader.ForeColor = System.Drawing.Color.Green;
            this.colGoodsID.AppearanceHeader.Options.UseForeColor = true;
            this.colGoodsID.Caption = "货号";
            this.colGoodsID.ColumnEdit = this.repositoryItemLueGoods;
            this.colGoodsID.FieldName = "GoodsID";
            this.colGoodsID.Name = "colGoodsID";
            this.colGoodsID.Visible = true;
            this.colGoodsID.VisibleIndex = 0;
            // 
            // repositoryItemLueGoods
            // 
            this.repositoryItemLueGoods.AutoHeight = false;
            this.repositoryItemLueGoods.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.repositoryItemLueGoods.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLueGoods.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Code", "货号", 34, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "品名", 34, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Price", "单价", 34, DevExpress.Utils.FormatType.Numeric, "", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Unit", "单位", 34, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SPEC", "规格", 34, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NWeight", "净重")});
            this.repositoryItemLueGoods.DataSource = this.goodsBindingSource;
            this.repositoryItemLueGoods.DisplayMember = "Code";
            this.repositoryItemLueGoods.DropDownRows = 15;
            this.repositoryItemLueGoods.Name = "repositoryItemLueGoods";
            this.repositoryItemLueGoods.PopupWidth = 600;
            this.repositoryItemLueGoods.ValueMember = "ID";
            this.repositoryItemLueGoods.EditValueChanged += new System.EventHandler(this.repositoryItemLueGoods_EditValueChanged);
            // 
            // goodsBindingSource
            // 
            this.goodsBindingSource.DataSource = typeof(EDMX.Goods);
            // 
            // colName
            // 
            this.colName.Caption = "品名";
            this.colName.FieldName = "colName";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.OptionsColumn.ReadOnly = true;
            this.colName.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            // 
            // colSPEC
            // 
            this.colSPEC.Caption = "规格";
            this.colSPEC.FieldName = "colSPEC";
            this.colSPEC.Name = "colSPEC";
            this.colSPEC.OptionsColumn.AllowEdit = false;
            this.colSPEC.OptionsColumn.ReadOnly = true;
            this.colSPEC.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.colSPEC.Visible = true;
            this.colSPEC.VisibleIndex = 2;
            // 
            // colQty
            // 
            this.colQty.AppearanceHeader.ForeColor = System.Drawing.Color.Green;
            this.colQty.AppearanceHeader.Options.UseForeColor = true;
            this.colQty.Caption = "数量";
            this.colQty.FieldName = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Qty", "{0}")});
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 3;
            // 
            // colUnit
            // 
            this.colUnit.Caption = "单位";
            this.colUnit.FieldName = "colUnit";
            this.colUnit.Name = "colUnit";
            this.colUnit.OptionsColumn.AllowEdit = false;
            this.colUnit.OptionsColumn.ReadOnly = true;
            this.colUnit.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.colUnit.Visible = true;
            this.colUnit.VisibleIndex = 4;
            // 
            // colPCS
            // 
            this.colPCS.Caption = "装箱数";
            this.colPCS.FieldName = "PCS";
            this.colPCS.Name = "colPCS";
            this.colPCS.OptionsColumn.ReadOnly = true;
            // 
            // colInnerBox
            // 
            this.colInnerBox.Caption = "内盒";
            this.colInnerBox.FieldName = "InnerBox";
            this.colInnerBox.Name = "colInnerBox";
            this.colInnerBox.OptionsColumn.ReadOnly = true;
            // 
            // colTotalQty
            // 
            this.colTotalQty.Caption = "总数量";
            this.colTotalQty.FieldName = "colTotalQty";
            this.colTotalQty.Name = "colTotalQty";
            this.colTotalQty.OptionsColumn.ReadOnly = true;
            this.colTotalQty.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "colTotalQty", "{0}")});
            this.colTotalQty.UnboundExpression = "[Qty] * [PCS]";
            this.colTotalQty.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            // 
            // colPrice
            // 
            this.colPrice.AppearanceHeader.ForeColor = System.Drawing.Color.Green;
            this.colPrice.AppearanceHeader.Options.UseForeColor = true;
            this.colPrice.Caption = "单价";
            this.colPrice.DisplayFormat.FormatString = "c5";
            this.colPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPrice.FieldName = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 5;
            // 
            // colAMT
            // 
            this.colAMT.Caption = "金额";
            this.colAMT.FieldName = "colAMT";
            this.colAMT.Name = "colAMT";
            this.colAMT.OptionsColumn.AllowEdit = false;
            this.colAMT.OptionsColumn.ReadOnly = true;
            this.colAMT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "colAMT", "{0}")});
            this.colAMT.UnboundExpression = "[Qty] * [PCS] * [Price]";
            this.colAMT.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.colAMT.Visible = true;
            this.colAMT.VisibleIndex = 6;
            // 
            // colPriceNoTax
            // 
            this.colPriceNoTax.Caption = "去税单价";
            this.colPriceNoTax.FieldName = "PriceNoTax";
            this.colPriceNoTax.Name = "colPriceNoTax";
            // 
            // colAMTNoTax
            // 
            this.colAMTNoTax.Caption = "去税金额";
            this.colAMTNoTax.FieldName = "colAMTNoTax";
            this.colAMTNoTax.Name = "colAMTNoTax";
            this.colAMTNoTax.OptionsColumn.ReadOnly = true;
            this.colAMTNoTax.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "colAMTNoTax", "{0}")});
            this.colAMTNoTax.UnboundExpression = "[Qty] * [PCS] * [PriceNoTax]";
            this.colAMTNoTax.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "备注";
            this.colRemark.FieldName = "colRemark";
            this.colRemark.Name = "colRemark";
            this.colRemark.OptionsColumn.AllowEdit = false;
            this.colRemark.OptionsColumn.ReadOnly = true;
            this.colRemark.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 7;
            // 
            // documentManager
            // 
            this.documentManager.ContainerControl = this;
            this.documentManager.View = this.tabbedView1;
            this.documentManager.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView1});
            // 
            // tabbedView1
            // 
            this.tabbedView1.DocumentGroups.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup[] {
            this.documentGroup1});
            this.tabbedView1.Documents.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseDocument[] {
            this.document1});
            this.tabbedView1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tabbedView1.RootContainer.Element = null;
            dockingContainer2.Element = this.documentGroup1;
            this.tabbedView1.RootContainer.Nodes.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer[] {
            dockingContainer2});
            this.tabbedView1.RootContainer.Orientation = System.Windows.Forms.Orientation.Vertical;
            // 
            // documentGroup1
            // 
            this.documentGroup1.Items.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document[] {
            this.document1});
            // 
            // document1
            // 
            this.document1.Caption = "模具信息";
            this.document1.ControlName = "dpBOM";
            this.document1.FloatLocation = new System.Drawing.Point(272, 531);
            this.document1.FloatSize = new System.Drawing.Size(200, 200);
            this.document1.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.False;
            this.document1.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.True;
            this.document1.Properties.AllowFloatOnDoubleClick = DevExpress.Utils.DefaultBoolean.True;
            // 
            // MoldAllotPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dpSupplier);
            this.Name = "MoldAllotPage";
            this.Size = new System.Drawing.Size(640, 480);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            this.dpSupplier.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vSupplierBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.winExplorerView)).EndInit();
            this.dpBOM.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bOMBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLueGoods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.goodsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.document1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager;
        private DevExpress.XtraBars.Docking.DockPanel dpSupplier;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockPanel dpBOM;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup documentGroup1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.Document document1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.WinExplorer.WinExplorerView winExplorerView;
        private System.Windows.Forms.BindingSource bOMBindingSource;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn colParentGoodsID;
        private DevExpress.XtraGrid.Columns.GridColumn colGoodsID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLueGoods;
        private DevExpress.XtraGrid.Columns.GridColumn colQty;
        private DevExpress.XtraGrid.Columns.GridColumn colPCS;
        private DevExpress.XtraGrid.Columns.GridColumn colInnerBox;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalQty;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colAMT;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceNoTax;
        private DevExpress.XtraGrid.Columns.GridColumn colAMTNoTax;
        private System.Windows.Forms.BindingSource vSupplierBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn col代码;
        private DevExpress.XtraGrid.Columns.GridColumn col名称;
        private DevExpress.XtraGrid.Columns.GridColumn col地址;
        private DevExpress.XtraGrid.Columns.GridColumn col邮编;
        private DevExpress.XtraGrid.Columns.GridColumn col联系人;
        private DevExpress.XtraGrid.Columns.GridColumn col联系电话;
        private DevExpress.XtraGrid.Columns.GridColumn col联系手机;
        private DevExpress.XtraGrid.Columns.GridColumn col传真;
        private DevExpress.XtraGrid.Columns.GridColumn col开户行;
        private DevExpress.XtraGrid.Columns.GridColumn col帐号;
        private DevExpress.XtraGrid.Columns.GridColumn col税号;
        private DevExpress.XtraGrid.Columns.GridColumn col付款地址;
        private DevExpress.XtraGrid.Columns.GridColumn col付款抬头;
        private DevExpress.XtraGrid.Columns.GridColumn col发票地址;
        private DevExpress.XtraGrid.Columns.GridColumn col备注;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colSPEC;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private System.Windows.Forms.BindingSource goodsBindingSource;

    }
}
