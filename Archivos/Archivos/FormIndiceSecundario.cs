using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Archivos
{
    public partial class FormIndiceSecundario : Form
    {
        public delegate void cambio(FormEntidad form, List<Entidad> entidades);
        public event cambio cambiar;

        int pos;
        FormEntidad form;
        FileStream Fichero;
        List<Entidad> entidades = new List<Entidad>();

        public FormIndiceSecundario(FormEntidad form, List<Entidad> entidades, int pos)
        {
            this.form = form;
            this.entidades = entidades;
            this.pos = pos;
            InitializeComponent();
        }

        private void FormIndiceSecundario_Load(object sender, EventArgs e)
        {
            escribeDataGIndice();
            //escribeDataGDirecciones();
        }

        private void escribeDataGIndice()
        {
            dgv_IndiceSecundario.DataSource = null;
            dgv_IndiceSecundario.Columns.Add("Clave", "Clave");
            dgv_IndiceSecundario.Columns.Add("Direccion", "Direccion");
            dgv_IndiceSecundario.Columns.Add("Dir. Diguiente", "Dir. Diguiente");

            int j = 0;

            foreach (Secundario s in entidades[pos].secundarios)
            {
                for (int i = 0; i < s.listSecD.Count; ++i)
                {
                    dgv_IndiceSecundario.Rows.Add(s.listSecD[i].getClave.ToString());
                    dgv_IndiceSecundario.Rows[j].Cells[1].Value = s.listSecD[i].getDireccion;
                    if (i == s.listSecD.Count - 1)
                    {
                        dgv_IndiceSecundario.Rows[j].Cells[2].Value = s.getApuntadorSig;
                    }
                    j++;
                }
            }
        }

        private void escribeDataGDirecciones()
        {
            dgv_Direcciones.DataSource = null;

            if (dgv_IndiceSecundario.CurrentRow.Index >= 0 )
            {
                if (dgv_Direcciones.Rows.Count > 0)
                {
                    dgv_Direcciones.Columns.Remove("Dirección");
                    dgv_Direcciones.Columns.Remove("Dir. Siguiente");
                }


                int pos2 = dgv_IndiceSecundario.CurrentRow.Index;
                string cl = Convert.ToString(entidades[pos].secundarios.Last().listSecD[pos2].getClave);
                lbl_direccion.Text = "DIRECCIÓN  DE: " + cl;


                dgv_Direcciones.Columns.Add("Dirección", "Dirección");
                dgv_Direcciones.Columns.Add("Dir. Siguiente", "Dir. Siguiente");

                int j = 0;

                foreach (SecundarioDir ip in entidades[pos].secundarios.Last().listSecD[pos2].listSecDirs)
                {
                    for (int i = 0; i < ip.listIndiceSecundario.Count; ++i)
                    {
                        dgv_Direcciones.Rows.Add(ip.listIndiceSecundario[i].getDireccion.ToString());
                        if (i == ip.listIndiceSecundario.Count - 1)
                        {
                            dgv_Direcciones.Rows[j].Cells[1].Value = ip.getApSiguiente;
                        }
                        j++;
                    }
                }
            }
        }

        private void tab_Direccion_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btn_regresaEntidad_Click(object sender, EventArgs e)
        {
            dgv_IndiceSecundario.Rows.Clear();
            dgv_Direcciones.Rows.Clear();
            this.Close();
            cambiar(form, entidades);
        }

        private void dgv_IndiceSecundario_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgv_IndiceSecundario.CurrentRow.Index >= 0)
            {
                escribeDataGDirecciones();
            }
        }
    }
}
