namespace Archivos
{
    partial class FormAtributo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAtributo));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btn_CrearAtributo = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_modifAtributo = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_eliminarAtributo = new System.Windows.Forms.ToolStripMenuItem();
            this.dgv_Atributo = new System.Windows.Forms.DataGridView();
            this.Id_Atributo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipo_Dato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.long_Atributo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dir_Atributo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipo_Indice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dir_Indice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dir_sigAtributo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lb_Nombre = new System.Windows.Forms.Label();
            this.lb_Tipo = new System.Windows.Forms.Label();
            this.lb_Longitud = new System.Windows.Forms.Label();
            this.lb_indice = new System.Windows.Forms.Label();
            this.tb_Nombre = new System.Windows.Forms.TextBox();
            this.cb_TipoDato = new System.Windows.Forms.ComboBox();
            this.tb_Longitud = new System.Windows.Forms.TextBox();
            this.cb_Indice = new System.Windows.Forms.ComboBox();
            this.btn_Regreso = new System.Windows.Forms.Button();
            this.btn_Aceptar = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Atributo)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.DimGray;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_CrearAtributo,
            this.btn_modifAtributo,
            this.btn_eliminarAtributo});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(866, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btn_CrearAtributo
            // 
            this.btn_CrearAtributo.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_CrearAtributo.Name = "btn_CrearAtributo";
            this.btn_CrearAtributo.Size = new System.Drawing.Size(94, 20);
            this.btn_CrearAtributo.Text = "Crear Atributo";
            this.btn_CrearAtributo.Click += new System.EventHandler(this.btn_CrearAtributo_Click);
            // 
            // btn_modifAtributo
            // 
            this.btn_modifAtributo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_modifAtributo.Name = "btn_modifAtributo";
            this.btn_modifAtributo.Size = new System.Drawing.Size(129, 20);
            this.btn_modifAtributo.Text = "Modificar el Atributo";
            this.btn_modifAtributo.Click += new System.EventHandler(this.btn_modifAtributo_Click);
            // 
            // btn_eliminarAtributo
            // 
            this.btn_eliminarAtributo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_eliminarAtributo.Name = "btn_eliminarAtributo";
            this.btn_eliminarAtributo.Size = new System.Drawing.Size(121, 20);
            this.btn_eliminarAtributo.Text = "Eliminar el Atributo";
            this.btn_eliminarAtributo.Click += new System.EventHandler(this.btn_eliminarAtributo_Click);
            // 
            // dgv_Atributo
            // 
            this.dgv_Atributo.BackgroundColor = System.Drawing.SystemColors.WindowFrame;
            this.dgv_Atributo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_Atributo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Atributo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id_Atributo,
            this.Nombre,
            this.tipo_Dato,
            this.long_Atributo,
            this.Dir_Atributo,
            this.tipo_Indice,
            this.Dir_Indice,
            this.Dir_sigAtributo});
            this.dgv_Atributo.Location = new System.Drawing.Point(12, 40);
            this.dgv_Atributo.Name = "dgv_Atributo";
            this.dgv_Atributo.ReadOnly = true;
            this.dgv_Atributo.Size = new System.Drawing.Size(842, 330);
            this.dgv_Atributo.TabIndex = 3;
            // 
            // Id_Atributo
            // 
            this.Id_Atributo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Id_Atributo.DefaultCellStyle = dataGridViewCellStyle1;
            this.Id_Atributo.HeaderText = "ID Atributo";
            this.Id_Atributo.Name = "Id_Atributo";
            this.Id_Atributo.ReadOnly = true;
            // 
            // Nombre
            // 
            this.Nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            this.Nombre.DefaultCellStyle = dataGridViewCellStyle2;
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // tipo_Dato
            // 
            this.tipo_Dato.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tipo_Dato.DefaultCellStyle = dataGridViewCellStyle3;
            this.tipo_Dato.HeaderText = "Tipo Dato";
            this.tipo_Dato.Name = "tipo_Dato";
            this.tipo_Dato.ReadOnly = true;
            // 
            // long_Atributo
            // 
            this.long_Atributo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.long_Atributo.DefaultCellStyle = dataGridViewCellStyle4;
            this.long_Atributo.HeaderText = "Longitud";
            this.long_Atributo.Name = "long_Atributo";
            this.long_Atributo.ReadOnly = true;
            // 
            // Dir_Atributo
            // 
            this.Dir_Atributo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Dir_Atributo.DefaultCellStyle = dataGridViewCellStyle5;
            this.Dir_Atributo.HeaderText = "Dir. Atributo";
            this.Dir_Atributo.Name = "Dir_Atributo";
            this.Dir_Atributo.ReadOnly = true;
            // 
            // tipo_Indice
            // 
            this.tipo_Indice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tipo_Indice.DefaultCellStyle = dataGridViewCellStyle6;
            this.tipo_Indice.HeaderText = "Tipo de Indice";
            this.tipo_Indice.Name = "tipo_Indice";
            this.tipo_Indice.ReadOnly = true;
            // 
            // Dir_Indice
            // 
            this.Dir_Indice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Dir_Indice.HeaderText = "Dir. Indice";
            this.Dir_Indice.Name = "Dir_Indice";
            this.Dir_Indice.ReadOnly = true;
            // 
            // Dir_sigAtributo
            // 
            this.Dir_sigAtributo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Dir_sigAtributo.HeaderText = "Dir. Siguiente Atributo";
            this.Dir_sigAtributo.Name = "Dir_sigAtributo";
            this.Dir_sigAtributo.ReadOnly = true;
            // 
            // lb_Nombre
            // 
            this.lb_Nombre.AutoSize = true;
            this.lb_Nombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Nombre.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lb_Nombre.Location = new System.Drawing.Point(12, 389);
            this.lb_Nombre.Name = "lb_Nombre";
            this.lb_Nombre.Size = new System.Drawing.Size(77, 16);
            this.lb_Nombre.TabIndex = 9;
            this.lb_Nombre.Text = "NOMBRE:";
            this.lb_Nombre.Visible = false;
            // 
            // lb_Tipo
            // 
            this.lb_Tipo.AutoSize = true;
            this.lb_Tipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Tipo.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lb_Tipo.Location = new System.Drawing.Point(158, 389);
            this.lb_Tipo.Name = "lb_Tipo";
            this.lb_Tipo.Size = new System.Drawing.Size(118, 16);
            this.lb_Tipo.TabIndex = 10;
            this.lb_Tipo.Text = "TIPO DE DATO:";
            this.lb_Tipo.Visible = false;
            // 
            // lb_Longitud
            // 
            this.lb_Longitud.AutoSize = true;
            this.lb_Longitud.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Longitud.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lb_Longitud.Location = new System.Drawing.Point(282, 389);
            this.lb_Longitud.Name = "lb_Longitud";
            this.lb_Longitud.Size = new System.Drawing.Size(89, 16);
            this.lb_Longitud.TabIndex = 11;
            this.lb_Longitud.Text = "LONGITUD:";
            this.lb_Longitud.Visible = false;
            // 
            // lb_indice
            // 
            this.lb_indice.AutoSize = true;
            this.lb_indice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_indice.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lb_indice.Location = new System.Drawing.Point(377, 389);
            this.lb_indice.Name = "lb_indice";
            this.lb_indice.Size = new System.Drawing.Size(126, 16);
            this.lb_indice.TabIndex = 12;
            this.lb_indice.Text = "TIPO DE INDICE:";
            this.lb_indice.Visible = false;
            // 
            // tb_Nombre
            // 
            this.tb_Nombre.Location = new System.Drawing.Point(15, 418);
            this.tb_Nombre.Name = "tb_Nombre";
            this.tb_Nombre.Size = new System.Drawing.Size(132, 20);
            this.tb_Nombre.TabIndex = 13;
            this.tb_Nombre.Visible = false;
            // 
            // cb_TipoDato
            // 
            this.cb_TipoDato.FormattingEnabled = true;
            this.cb_TipoDato.Items.AddRange(new object[] {
            "E",
            "C"});
            this.cb_TipoDato.Location = new System.Drawing.Point(161, 417);
            this.cb_TipoDato.Name = "cb_TipoDato";
            this.cb_TipoDato.Size = new System.Drawing.Size(115, 21);
            this.cb_TipoDato.TabIndex = 14;
            this.cb_TipoDato.Visible = false;
            this.cb_TipoDato.SelectedIndexChanged += new System.EventHandler(this.cb_TipoDato_SelectedIndexChanged);
            // 
            // tb_Longitud
            // 
            this.tb_Longitud.Location = new System.Drawing.Point(285, 418);
            this.tb_Longitud.Name = "tb_Longitud";
            this.tb_Longitud.Size = new System.Drawing.Size(86, 20);
            this.tb_Longitud.TabIndex = 15;
            this.tb_Longitud.Visible = false;
            // 
            // cb_Indice
            // 
            this.cb_Indice.FormattingEnabled = true;
            this.cb_Indice.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.cb_Indice.Location = new System.Drawing.Point(380, 418);
            this.cb_Indice.Name = "cb_Indice";
            this.cb_Indice.Size = new System.Drawing.Size(123, 21);
            this.cb_Indice.TabIndex = 16;
            this.cb_Indice.Visible = false;
            // 
            // btn_Regreso
            // 
            this.btn_Regreso.BackColor = System.Drawing.Color.Black;
            this.btn_Regreso.FlatAppearance.BorderSize = 0;
            this.btn_Regreso.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Regreso.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Regreso.Location = new System.Drawing.Point(740, 418);
            this.btn_Regreso.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Regreso.Name = "btn_Regreso";
            this.btn_Regreso.Size = new System.Drawing.Size(114, 23);
            this.btn_Regreso.TabIndex = 17;
            this.btn_Regreso.Text = "Tabla de entidades";
            this.btn_Regreso.UseVisualStyleBackColor = false;
            this.btn_Regreso.Click += new System.EventHandler(this.btn_Regreso_Click);
            // 
            // btn_Aceptar
            // 
            this.btn_Aceptar.BackColor = System.Drawing.Color.Black;
            this.btn_Aceptar.FlatAppearance.BorderSize = 0;
            this.btn_Aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Aceptar.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Aceptar.Location = new System.Drawing.Point(507, 418);
            this.btn_Aceptar.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Aceptar.Name = "btn_Aceptar";
            this.btn_Aceptar.Size = new System.Drawing.Size(114, 23);
            this.btn_Aceptar.TabIndex = 18;
            this.btn_Aceptar.Text = "Agregar Atributo";
            this.btn_Aceptar.UseVisualStyleBackColor = false;
            this.btn_Aceptar.Visible = false;
            this.btn_Aceptar.Click += new System.EventHandler(this.btn_Aceptar_Click);
            // 
            // FormAtributo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(866, 450);
            this.Controls.Add(this.btn_Aceptar);
            this.Controls.Add(this.btn_Regreso);
            this.Controls.Add(this.cb_Indice);
            this.Controls.Add(this.tb_Longitud);
            this.Controls.Add(this.cb_TipoDato);
            this.Controls.Add(this.tb_Nombre);
            this.Controls.Add(this.lb_indice);
            this.Controls.Add(this.lb_Longitud);
            this.Controls.Add(this.lb_Tipo);
            this.Controls.Add(this.lb_Nombre);
            this.Controls.Add(this.dgv_Atributo);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAtributo";
            this.Text = "Tabla de atributos";
            this.Load += new System.EventHandler(this.FormAtributo_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Atributo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btn_CrearAtributo;
        private System.Windows.Forms.ToolStripMenuItem btn_modifAtributo;
        private System.Windows.Forms.ToolStripMenuItem btn_eliminarAtributo;
        private System.Windows.Forms.DataGridView dgv_Atributo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id_Atributo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipo_Dato;
        private System.Windows.Forms.DataGridViewTextBoxColumn long_Atributo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dir_Atributo;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipo_Indice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dir_Indice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dir_sigAtributo;
        private System.Windows.Forms.Label lb_Nombre;
        private System.Windows.Forms.Label lb_Tipo;
        private System.Windows.Forms.Label lb_Longitud;
        private System.Windows.Forms.Label lb_indice;
        private System.Windows.Forms.TextBox tb_Nombre;
        private System.Windows.Forms.ComboBox cb_TipoDato;
        private System.Windows.Forms.TextBox tb_Longitud;
        private System.Windows.Forms.ComboBox cb_Indice;
        private System.Windows.Forms.Button btn_Regreso;
        private System.Windows.Forms.Button btn_Aceptar;
    }
}