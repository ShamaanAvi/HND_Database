
namespace FinalProject
{
    partial class fUser
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
            this.btnBackStu = new System.Windows.Forms.Button();
            this.dataG = new System.Windows.Forms.DataGridView();
            this.btnProductions = new System.Windows.Forms.Button();
            this.btnEditProd = new System.Windows.Forms.Button();
            this.btnStaff = new System.Windows.Forms.Button();
            this.btnEditStaff = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.adminBox = new System.Windows.Forms.GroupBox();
            this.btnEditLocation = new System.Windows.Forms.Button();
            this.btnEditProperty = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnPropLoc = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
            this.btnProp = new System.Windows.Forms.Button();
            this.btnAllocation = new System.Windows.Forms.Button();
            this.btnClient = new System.Windows.Forms.Button();
            this.btnEditClients = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataG)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.adminBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBackStu
            // 
            this.btnBackStu.Location = new System.Drawing.Point(654, 410);
            this.btnBackStu.Margin = new System.Windows.Forms.Padding(2);
            this.btnBackStu.Name = "btnBackStu";
            this.btnBackStu.Size = new System.Drawing.Size(71, 25);
            this.btnBackStu.TabIndex = 0;
            this.btnBackStu.Text = "Back";
            this.btnBackStu.UseVisualStyleBackColor = true;
            this.btnBackStu.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // dataG
            // 
            this.dataG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataG.Location = new System.Drawing.Point(113, 36);
            this.dataG.Margin = new System.Windows.Forms.Padding(2);
            this.dataG.Name = "dataG";
            this.dataG.RowHeadersWidth = 62;
            this.dataG.RowTemplate.Height = 28;
            this.dataG.Size = new System.Drawing.Size(612, 370);
            this.dataG.TabIndex = 1;
            this.dataG.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnProductions
            // 
            this.btnProductions.Location = new System.Drawing.Point(5, 36);
            this.btnProductions.Margin = new System.Windows.Forms.Padding(2);
            this.btnProductions.Name = "btnProductions";
            this.btnProductions.Size = new System.Drawing.Size(73, 23);
            this.btnProductions.TabIndex = 2;
            this.btnProductions.Text = "Productions";
            this.btnProductions.UseVisualStyleBackColor = true;
            this.btnProductions.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnEditProd
            // 
            this.btnEditProd.Location = new System.Drawing.Point(14, 18);
            this.btnEditProd.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditProd.Name = "btnEditProd";
            this.btnEditProd.Size = new System.Drawing.Size(73, 23);
            this.btnEditProd.TabIndex = 3;
            this.btnEditProd.Text = "Edit Prod";
            this.btnEditProd.UseVisualStyleBackColor = true;
            this.btnEditProd.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnStaff
            // 
            this.btnStaff.Location = new System.Drawing.Point(5, 63);
            this.btnStaff.Margin = new System.Windows.Forms.Padding(2);
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.Size = new System.Drawing.Size(73, 23);
            this.btnStaff.TabIndex = 5;
            this.btnStaff.Text = "Staff";
            this.btnStaff.UseVisualStyleBackColor = true;
            this.btnStaff.Click += new System.EventHandler(this.btnStaff_Click);
            // 
            // btnEditStaff
            // 
            this.btnEditStaff.Location = new System.Drawing.Point(14, 45);
            this.btnEditStaff.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditStaff.Name = "btnEditStaff";
            this.btnEditStaff.Size = new System.Drawing.Size(73, 23);
            this.btnEditStaff.TabIndex = 6;
            this.btnEditStaff.Text = "Edit Staff";
            this.btnEditStaff.UseVisualStyleBackColor = true;
            this.btnEditStaff.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.adminBox);
            this.groupBox1.Controls.Add(this.btnPropLoc);
            this.groupBox1.Controls.Add(this.btnLoc);
            this.groupBox1.Controls.Add(this.btnProp);
            this.groupBox1.Controls.Add(this.btnAllocation);
            this.groupBox1.Controls.Add(this.btnClient);
            this.groupBox1.Controls.Add(this.dataG);
            this.groupBox1.Controls.Add(this.btnBackStu);
            this.groupBox1.Controls.Add(this.btnProductions);
            this.groupBox1.Controls.Add(this.btnStaff);
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(730, 440);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Action";
            // 
            // adminBox
            // 
            this.adminBox.Controls.Add(this.btnEditProd);
            this.adminBox.Controls.Add(this.btnEditLocation);
            this.adminBox.Controls.Add(this.btnEditStaff);
            this.adminBox.Controls.Add(this.btnEditProperty);
            this.adminBox.Controls.Add(this.button1);
            this.adminBox.Location = new System.Drawing.Point(6, 254);
            this.adminBox.Name = "adminBox";
            this.adminBox.Size = new System.Drawing.Size(102, 159);
            this.adminBox.TabIndex = 16;
            this.adminBox.TabStop = false;
            this.adminBox.Text = "Admin Rights";
            // 
            // btnEditLocation
            // 
            this.btnEditLocation.Location = new System.Drawing.Point(14, 126);
            this.btnEditLocation.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditLocation.Name = "btnEditLocation";
            this.btnEditLocation.Size = new System.Drawing.Size(73, 23);
            this.btnEditLocation.TabIndex = 15;
            this.btnEditLocation.Text = "Edit Loc";
            this.btnEditLocation.UseVisualStyleBackColor = true;
            this.btnEditLocation.Click += new System.EventHandler(this.btnEditLocation_Click);
            // 
            // btnEditProperty
            // 
            this.btnEditProperty.Location = new System.Drawing.Point(14, 99);
            this.btnEditProperty.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditProperty.Name = "btnEditProperty";
            this.btnEditProperty.Size = new System.Drawing.Size(73, 23);
            this.btnEditProperty.TabIndex = 14;
            this.btnEditProperty.Text = " Edit Prop";
            this.btnEditProperty.UseVisualStyleBackColor = true;
            this.btnEditProperty.Click += new System.EventHandler(this.btnEditProperty_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 72);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "ProdStaff";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnPropLoc
            // 
            this.btnPropLoc.Location = new System.Drawing.Point(5, 198);
            this.btnPropLoc.Margin = new System.Windows.Forms.Padding(2);
            this.btnPropLoc.Name = "btnPropLoc";
            this.btnPropLoc.Size = new System.Drawing.Size(73, 23);
            this.btnPropLoc.TabIndex = 13;
            this.btnPropLoc.Text = "PropLoc";
            this.btnPropLoc.UseVisualStyleBackColor = true;
            this.btnPropLoc.Click += new System.EventHandler(this.btnPropLoc_Click);
            // 
            // btnLoc
            // 
            this.btnLoc.Location = new System.Drawing.Point(5, 171);
            this.btnLoc.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(73, 23);
            this.btnLoc.TabIndex = 12;
            this.btnLoc.Text = "Location";
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // btnProp
            // 
            this.btnProp.Location = new System.Drawing.Point(5, 144);
            this.btnProp.Margin = new System.Windows.Forms.Padding(2);
            this.btnProp.Name = "btnProp";
            this.btnProp.Size = new System.Drawing.Size(73, 23);
            this.btnProp.TabIndex = 11;
            this.btnProp.Text = "Property";
            this.btnProp.UseVisualStyleBackColor = true;
            this.btnProp.Click += new System.EventHandler(this.button2_Click_2);
            // 
            // btnAllocation
            // 
            this.btnAllocation.Location = new System.Drawing.Point(5, 117);
            this.btnAllocation.Margin = new System.Windows.Forms.Padding(2);
            this.btnAllocation.Name = "btnAllocation";
            this.btnAllocation.Size = new System.Drawing.Size(73, 23);
            this.btnAllocation.TabIndex = 10;
            this.btnAllocation.Text = "Allocation";
            this.btnAllocation.UseVisualStyleBackColor = true;
            this.btnAllocation.Click += new System.EventHandler(this.btnAllocation_Click);
            // 
            // btnClient
            // 
            this.btnClient.Location = new System.Drawing.Point(5, 90);
            this.btnClient.Margin = new System.Windows.Forms.Padding(2);
            this.btnClient.Name = "btnClient";
            this.btnClient.Size = new System.Drawing.Size(73, 23);
            this.btnClient.TabIndex = 7;
            this.btnClient.Text = "Clients";
            this.btnClient.UseVisualStyleBackColor = true;
            this.btnClient.Click += new System.EventHandler(this.btnClient_Click);
            // 
            // btnEditClients
            // 
            this.btnEditClients.Location = new System.Drawing.Point(95, 421);
            this.btnEditClients.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditClients.Name = "btnEditClients";
            this.btnEditClients.Size = new System.Drawing.Size(73, 23);
            this.btnEditClients.TabIndex = 8;
            this.btnEditClients.Text = "Edit Clients";
            this.btnEditClients.UseVisualStyleBackColor = true;
            this.btnEditClients.Click += new System.EventHandler(this.btnEditClients_Click);
            // 
            // fUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(744, 455);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnEditClients);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "fUser";
            this.Text = "User";
            this.Load += new System.EventHandler(this.Student_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataG)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.adminBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBackStu;
        private System.Windows.Forms.DataGridView dataG;
        private System.Windows.Forms.Button btnProductions;
        private System.Windows.Forms.Button btnEditProd;
        private System.Windows.Forms.Button btnStaff;
        private System.Windows.Forms.Button btnEditStaff;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClient;
        private System.Windows.Forms.Button btnEditClients;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAllocation;
        private System.Windows.Forms.Button btnProp;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnPropLoc;
        private System.Windows.Forms.Button btnEditLocation;
        private System.Windows.Forms.Button btnEditProperty;
        private System.Windows.Forms.GroupBox adminBox;
    }
}