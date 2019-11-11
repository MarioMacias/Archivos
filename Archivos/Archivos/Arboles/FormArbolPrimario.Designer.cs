namespace Archivos
{
    partial class FormArbolPrimario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormArbolPrimario));
            this.btn_regresaEntidad = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dgv_IndicePrimario = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_IndicePrimario)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_regresaEntidad
            // 
            this.btn_regresaEntidad.AutoSize = true;
            this.btn_regresaEntidad.BackColor = System.Drawing.Color.Black;
            this.btn_regresaEntidad.FlatAppearance.BorderSize = 0;
            this.btn_regresaEntidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_regresaEntidad.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_regresaEntidad.Location = new System.Drawing.Point(607, 428);
            this.btn_regresaEntidad.Margin = new System.Windows.Forms.Padding(1);
            this.btn_regresaEntidad.Name = "btn_regresaEntidad";
            this.btn_regresaEntidad.Size = new System.Drawing.Size(127, 23);
            this.btn_regresaEntidad.TabIndex = 12;
            this.btn_regresaEntidad.Text = "Tabla de entidades";
            this.btn_regresaEntidad.UseVisualStyleBackColor = false;
            this.btn_regresaEntidad.Click += new System.EventHandler(this.btn_regresaEntidad_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.DimGray;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(746, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dgv_IndicePrimario
            // 
            this.dgv_IndicePrimario.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_IndicePrimario.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dgv_IndicePrimario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_IndicePrimario.Location = new System.Drawing.Point(12, 36);
            this.dgv_IndicePrimario.Name = "dgv_IndicePrimario";
            this.dgv_IndicePrimario.ReadOnly = true;
            this.dgv_IndicePrimario.Size = new System.Drawing.Size(722, 386);
            this.dgv_IndicePrimario.TabIndex = 10;
            // 
            // FormArbolPrimario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(746, 461);
            this.Controls.Add(this.btn_regresaEntidad);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.dgv_IndicePrimario);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormArbolPrimario";
            this.Text = "Árbol Primario";
            this.Load += new System.EventHandler(this.FormArbolPrimario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_IndicePrimario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_regresaEntidad;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.DataGridView dgv_IndicePrimario;
    }
}