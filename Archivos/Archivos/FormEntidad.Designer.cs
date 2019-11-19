namespace Archivos
{
    partial class FormEntidad
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEntidad));
            this.dgv_Entidad = new System.Windows.Forms.DataGridView();
            this.Id_Entidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dir_Entidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dir_Atributo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dir_Datos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dir_SigEntidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.nuevoArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoRegistroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.primarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secundarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.árbolBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indicePrimarioBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indiceSecundarioBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_Cabecera = new System.Windows.Forms.Label();
            this.btn_CrearEntidad = new System.Windows.Forms.Button();
            this.btn_Modificar = new System.Windows.Forms.Button();
            this.btn_Borrar = new System.Windows.Forms.Button();
            this.tb_entidad = new System.Windows.Forms.TextBox();
            this.btn_Atributo = new System.Windows.Forms.Button();
            this.lb_atributo = new System.Windows.Forms.Label();
            this.tb_modificar = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Entidad)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_Entidad
            // 
            this.dgv_Entidad.BackgroundColor = System.Drawing.SystemColors.WindowFrame;
            this.dgv_Entidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_Entidad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Entidad.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id_Entidad,
            this.Nombre,
            this.Dir_Entidad,
            this.Dir_Atributo,
            this.Dir_Datos,
            this.Dir_SigEntidad});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Entidad.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_Entidad.Location = new System.Drawing.Point(10, 57);
            this.dgv_Entidad.Name = "dgv_Entidad";
            this.dgv_Entidad.ReadOnly = true;
            this.dgv_Entidad.Size = new System.Drawing.Size(699, 354);
            this.dgv_Entidad.TabIndex = 0;
            this.dgv_Entidad.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_Entidad_CellMouseDown);
            // 
            // Id_Entidad
            // 
            this.Id_Entidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Id_Entidad.DefaultCellStyle = dataGridViewCellStyle1;
            this.Id_Entidad.HeaderText = "ID Entidad";
            this.Id_Entidad.Name = "Id_Entidad";
            this.Id_Entidad.ReadOnly = true;
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
            // Dir_Entidad
            // 
            this.Dir_Entidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Dir_Entidad.DefaultCellStyle = dataGridViewCellStyle3;
            this.Dir_Entidad.HeaderText = "Dir. Entidad";
            this.Dir_Entidad.Name = "Dir_Entidad";
            this.Dir_Entidad.ReadOnly = true;
            // 
            // Dir_Atributo
            // 
            this.Dir_Atributo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Dir_Atributo.DefaultCellStyle = dataGridViewCellStyle4;
            this.Dir_Atributo.HeaderText = "Dir. Atributo";
            this.Dir_Atributo.Name = "Dir_Atributo";
            this.Dir_Atributo.ReadOnly = true;
            // 
            // Dir_Datos
            // 
            this.Dir_Datos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Dir_Datos.DefaultCellStyle = dataGridViewCellStyle5;
            this.Dir_Datos.HeaderText = "Dir. Datos";
            this.Dir_Datos.Name = "Dir_Datos";
            this.Dir_Datos.ReadOnly = true;
            // 
            // Dir_SigEntidad
            // 
            this.Dir_SigEntidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Dir_SigEntidad.DefaultCellStyle = dataGridViewCellStyle6;
            this.Dir_SigEntidad.HeaderText = "Dir. Siguiente Entidad";
            this.Dir_SigEntidad.Name = "Dir_SigEntidad";
            this.Dir_SigEntidad.ReadOnly = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.DimGray;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoArchivoToolStripMenuItem,
            this.abrirArchivoToolStripMenuItem,
            this.cerrarArchivoToolStripMenuItem,
            this.nuevoRegistroToolStripMenuItem,
            this.indiceToolStripMenuItem,
            this.árbolBToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(721, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // nuevoArchivoToolStripMenuItem
            // 
            this.nuevoArchivoToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.nuevoArchivoToolStripMenuItem.Name = "nuevoArchivoToolStripMenuItem";
            this.nuevoArchivoToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.nuevoArchivoToolStripMenuItem.Text = "Nuevo archivo";
            this.nuevoArchivoToolStripMenuItem.Click += new System.EventHandler(this.nuevoArchivoToolStripMenuItem_Click);
            // 
            // abrirArchivoToolStripMenuItem
            // 
            this.abrirArchivoToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.abrirArchivoToolStripMenuItem.Name = "abrirArchivoToolStripMenuItem";
            this.abrirArchivoToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.abrirArchivoToolStripMenuItem.Text = "Abrir archivo";
            this.abrirArchivoToolStripMenuItem.Click += new System.EventHandler(this.abrirArchivoToolStripMenuItem_Click);
            // 
            // cerrarArchivoToolStripMenuItem
            // 
            this.cerrarArchivoToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cerrarArchivoToolStripMenuItem.Name = "cerrarArchivoToolStripMenuItem";
            this.cerrarArchivoToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.cerrarArchivoToolStripMenuItem.Text = "Cerrar archivo";
            this.cerrarArchivoToolStripMenuItem.Click += new System.EventHandler(this.cerrarArchivoToolStripMenuItem_Click);
            // 
            // nuevoRegistroToolStripMenuItem
            // 
            this.nuevoRegistroToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.nuevoRegistroToolStripMenuItem.Name = "nuevoRegistroToolStripMenuItem";
            this.nuevoRegistroToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.nuevoRegistroToolStripMenuItem.Text = "Datos";
            this.nuevoRegistroToolStripMenuItem.Click += new System.EventHandler(this.nuevoRegistroToolStripMenuItem_Click);
            // 
            // indiceToolStripMenuItem
            // 
            this.indiceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.primarioToolStripMenuItem,
            this.secundarioToolStripMenuItem});
            this.indiceToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.indiceToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Gray;
            this.indiceToolStripMenuItem.Name = "indiceToolStripMenuItem";
            this.indiceToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.indiceToolStripMenuItem.Text = "Indice";
            // 
            // primarioToolStripMenuItem
            // 
            this.primarioToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.primarioToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.primarioToolStripMenuItem.Name = "primarioToolStripMenuItem";
            this.primarioToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.primarioToolStripMenuItem.Text = "Primario";
            this.primarioToolStripMenuItem.Click += new System.EventHandler(this.primarioToolStripMenuItem_Click);
            // 
            // secundarioToolStripMenuItem
            // 
            this.secundarioToolStripMenuItem.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.secundarioToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.secundarioToolStripMenuItem.Name = "secundarioToolStripMenuItem";
            this.secundarioToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.secundarioToolStripMenuItem.Text = "Secundario";
            this.secundarioToolStripMenuItem.Click += new System.EventHandler(this.secundarioToolStripMenuItem_Click);
            // 
            // árbolBToolStripMenuItem
            // 
            this.árbolBToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.indicePrimarioBToolStripMenuItem,
            this.indiceSecundarioBToolStripMenuItem});
            this.árbolBToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.árbolBToolStripMenuItem.Name = "árbolBToolStripMenuItem";
            this.árbolBToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.árbolBToolStripMenuItem.Text = "Árbol B+";
            // 
            // indicePrimarioBToolStripMenuItem
            // 
            this.indicePrimarioBToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.indicePrimarioBToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.indicePrimarioBToolStripMenuItem.Name = "indicePrimarioBToolStripMenuItem";
            this.indicePrimarioBToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.indicePrimarioBToolStripMenuItem.Text = "Primario B+";
            this.indicePrimarioBToolStripMenuItem.Click += new System.EventHandler(this.indicePrimarioBToolStripMenuItem_Click);
            // 
            // indiceSecundarioBToolStripMenuItem
            // 
            this.indiceSecundarioBToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.indiceSecundarioBToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.indiceSecundarioBToolStripMenuItem.Name = "indiceSecundarioBToolStripMenuItem";
            this.indiceSecundarioBToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.indiceSecundarioBToolStripMenuItem.Text = "Secundario B+";
            // 
            // lbl_Cabecera
            // 
            this.lbl_Cabecera.AutoSize = true;
            this.lbl_Cabecera.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Cabecera.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_Cabecera.Location = new System.Drawing.Point(6, 34);
            this.lbl_Cabecera.Name = "lbl_Cabecera";
            this.lbl_Cabecera.Size = new System.Drawing.Size(96, 20);
            this.lbl_Cabecera.TabIndex = 2;
            this.lbl_Cabecera.Text = "Cabecera: ";
            // 
            // btn_CrearEntidad
            // 
            this.btn_CrearEntidad.BackColor = System.Drawing.Color.Black;
            this.btn_CrearEntidad.FlatAppearance.BorderSize = 0;
            this.btn_CrearEntidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CrearEntidad.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_CrearEntidad.Location = new System.Drawing.Point(10, 415);
            this.btn_CrearEntidad.Margin = new System.Windows.Forms.Padding(1);
            this.btn_CrearEntidad.Name = "btn_CrearEntidad";
            this.btn_CrearEntidad.Size = new System.Drawing.Size(92, 23);
            this.btn_CrearEntidad.TabIndex = 3;
            this.btn_CrearEntidad.Text = "Nueva entidad";
            this.btn_CrearEntidad.UseVisualStyleBackColor = false;
            this.btn_CrearEntidad.Click += new System.EventHandler(this.btn_CrearEntidad_Click);
            // 
            // btn_Modificar
            // 
            this.btn_Modificar.BackColor = System.Drawing.Color.Black;
            this.btn_Modificar.FlatAppearance.BorderSize = 0;
            this.btn_Modificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Modificar.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Modificar.Location = new System.Drawing.Point(10, 440);
            this.btn_Modificar.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Modificar.Name = "btn_Modificar";
            this.btn_Modificar.Size = new System.Drawing.Size(92, 23);
            this.btn_Modificar.TabIndex = 4;
            this.btn_Modificar.Text = "Modificar";
            this.btn_Modificar.UseVisualStyleBackColor = false;
            this.btn_Modificar.Click += new System.EventHandler(this.btn_Modificar_Click);
            // 
            // btn_Borrar
            // 
            this.btn_Borrar.BackColor = System.Drawing.Color.Black;
            this.btn_Borrar.FlatAppearance.BorderSize = 0;
            this.btn_Borrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Borrar.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Borrar.Location = new System.Drawing.Point(10, 465);
            this.btn_Borrar.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Borrar.Name = "btn_Borrar";
            this.btn_Borrar.Size = new System.Drawing.Size(92, 23);
            this.btn_Borrar.TabIndex = 5;
            this.btn_Borrar.Text = "Borrar";
            this.btn_Borrar.UseVisualStyleBackColor = false;
            this.btn_Borrar.Click += new System.EventHandler(this.btn_Borrar_Click);
            // 
            // tb_entidad
            // 
            this.tb_entidad.BackColor = System.Drawing.Color.Gray;
            this.tb_entidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_entidad.ForeColor = System.Drawing.SystemColors.MenuBar;
            this.tb_entidad.Location = new System.Drawing.Point(107, 413);
            this.tb_entidad.Name = "tb_entidad";
            this.tb_entidad.Size = new System.Drawing.Size(100, 24);
            this.tb_entidad.TabIndex = 6;
            // 
            // btn_Atributo
            // 
            this.btn_Atributo.AutoSize = true;
            this.btn_Atributo.BackColor = System.Drawing.Color.Black;
            this.btn_Atributo.FlatAppearance.BorderSize = 0;
            this.btn_Atributo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Atributo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Atributo.Location = new System.Drawing.Point(582, 425);
            this.btn_Atributo.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Atributo.Name = "btn_Atributo";
            this.btn_Atributo.Size = new System.Drawing.Size(127, 23);
            this.btn_Atributo.TabIndex = 7;
            this.btn_Atributo.Text = "Tabla de atributos  ";
            this.btn_Atributo.UseVisualStyleBackColor = false;
            this.btn_Atributo.Click += new System.EventHandler(this.btn_Atributo_Click);
            // 
            // lb_atributo
            // 
            this.lb_atributo.AutoSize = true;
            this.lb_atributo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_atributo.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lb_atributo.Location = new System.Drawing.Point(469, 447);
            this.lb_atributo.Name = "lb_atributo";
            this.lb_atributo.Size = new System.Drawing.Size(20, 16);
            this.lb_atributo.TabIndex = 8;
            this.lb_atributo.Text = "\"\"";
            // 
            // tb_modificar
            // 
            this.tb_modificar.BackColor = System.Drawing.Color.Gray;
            this.tb_modificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_modificar.ForeColor = System.Drawing.SystemColors.MenuBar;
            this.tb_modificar.Location = new System.Drawing.Point(107, 439);
            this.tb_modificar.Name = "tb_modificar";
            this.tb_modificar.Size = new System.Drawing.Size(100, 24);
            this.tb_modificar.TabIndex = 9;
            // 
            // FormEntidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(721, 494);
            this.Controls.Add(this.tb_modificar);
            this.Controls.Add(this.lb_atributo);
            this.Controls.Add(this.btn_Atributo);
            this.Controls.Add(this.tb_entidad);
            this.Controls.Add(this.btn_Borrar);
            this.Controls.Add(this.btn_Modificar);
            this.Controls.Add(this.btn_CrearEntidad);
            this.Controls.Add(this.lbl_Cabecera);
            this.Controls.Add(this.dgv_Entidad);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FormEntidad";
            this.Text = "Proyecto de Archivos";
            this.TransparencyKey = System.Drawing.Color.Silver;
            this.Load += new System.EventHandler(this.FormEntidad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Entidad)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Entidad;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nuevoArchivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirArchivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarArchivoToolStripMenuItem;
        private System.Windows.Forms.Label lbl_Cabecera;
        private System.Windows.Forms.Button btn_CrearEntidad;
        private System.Windows.Forms.Button btn_Modificar;
        private System.Windows.Forms.Button btn_Borrar;
        private System.Windows.Forms.TextBox tb_entidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id_Entidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dir_Entidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dir_Atributo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dir_Datos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dir_SigEntidad;
        private System.Windows.Forms.Button btn_Atributo;
        private System.Windows.Forms.Label lb_atributo;
        private System.Windows.Forms.TextBox tb_modificar;
        private System.Windows.Forms.ToolStripMenuItem nuevoRegistroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem primarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem secundarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem árbolBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indicePrimarioBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indiceSecundarioBToolStripMenuItem;
    }
}

