namespace USL
{
    partial class DataEditForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.tileNavPane = new DevExpress.XtraBars.Navigation.TileNavPane();
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.hideContainerBottom = new DevExpress.XtraBars.Docking.AutoHideContainer();
            this.dpMoldAllot = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.vGoodsByMoldAllotBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileNavPane)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            this.hideContainerBottom.SuspendLayout();
            this.dpMoldAllot.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vGoodsByMoldAllotBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl.Location = new System.Drawing.Point(0, 40);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(784, 502);
            this.panelControl.TabIndex = 1;
            // 
            // tileNavPane
            // 
            // 
            // tileNavCategory1
            // 
            this.tileNavPane.DefaultCategory.Name = "tileNavCategory1";
            this.tileNavPane.DefaultCategory.OwnerCollection = null;
            // 
            // 
            // 
            this.tileNavPane.DefaultCategory.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            this.tileNavPane.Dock = System.Windows.Forms.DockStyle.Top;
            this.tileNavPane.Location = new System.Drawing.Point(0, 0);
            this.tileNavPane.Name = "tileNavPane";
            this.tileNavPane.Size = new System.Drawing.Size(784, 40);
            this.tileNavPane.TabIndex = 2;
            this.tileNavPane.Text = "tileNavPane1";
            // 
            // dockManager
            // 
            this.dockManager.AutoHideContainers.AddRange(new DevExpress.XtraBars.Docking.AutoHideContainer[] {
            this.hideContainerBottom});
            this.dockManager.Form = this;
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
            // hideContainerBottom
            // 
            this.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.hideContainerBottom.Controls.Add(this.dpMoldAllot);
            this.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hideContainerBottom.Location = new System.Drawing.Point(0, 542);
            this.hideContainerBottom.Name = "hideContainerBottom";
            this.hideContainerBottom.Size = new System.Drawing.Size(784, 20);
            this.hideContainerBottom.Visible = false;
            // 
            // dpMoldAllot
            // 
            this.dpMoldAllot.Controls.Add(this.dockPanel1_Container);
            this.dpMoldAllot.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dpMoldAllot.ID = new System.Guid("9d5c544a-d9a4-4831-93b0-c6491e7490ee");
            this.dpMoldAllot.Location = new System.Drawing.Point(0, 0);
            this.dpMoldAllot.Name = "dpMoldAllot";
            this.dpMoldAllot.Options.ShowCloseButton = false;
            this.dpMoldAllot.OriginalSize = new System.Drawing.Size(200, 300);
            this.dpMoldAllot.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dpMoldAllot.SavedIndex = 0;
            this.dpMoldAllot.SavedSizeFactor = 1D;
            this.dpMoldAllot.Size = new System.Drawing.Size(784, 300);
            this.dpMoldAllot.Text = "模具分配";
            this.dpMoldAllot.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.gridControl);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 24);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(776, 272);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // gridControl
            // 
            this.gridControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(776, 272);
            this.gridControl.TabIndex = 1;
            this.gridControl.UseEmbeddedNavigator = true;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsBehavior.ReadOnly = true;
            this.gridView.OptionsFind.FindNullPrompt = "请输入查找内容...";
            this.gridView.OptionsFind.ShowCloseButton = false;
            this.gridView.OptionsPrint.AutoWidth = false;
            this.gridView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.gridView.OptionsPrint.EnableAppearanceOddRow = true;
            this.gridView.OptionsView.ColumnAutoWidth = false;
            this.gridView.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView.OptionsView.EnableAppearanceOddRow = true;
            this.gridView.OptionsView.ShowFooter = true;
            // 
            // DataEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.tileNavPane);
            this.Controls.Add(this.hideContainerBottom);
            this.Name = "DataEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataEditForm";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileNavPane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            this.hideContainerBottom.ResumeLayout(false);
            this.dpMoldAllot.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vGoodsByMoldAllotBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl;
        private DevExpress.XtraBars.Navigation.TileNavPane tileNavPane;
        private DevExpress.XtraBars.Docking.DockManager dockManager;
        private DevExpress.XtraBars.Docking.AutoHideContainer hideContainerBottom;
        private DevExpress.XtraBars.Docking.DockPanel dpMoldAllot;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private System.Windows.Forms.BindingSource vGoodsByMoldAllotBindingSource;
    }
}