using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archivos
{
    public partial class FormIndicePrimario : Form
    {
        public delegate void cambio(FormEntidad formEntidad, List<Entidad> entidades);
        public event cambio cambiar;

        private FormEntidad formEntidad;
        private List<Entidad> entidades;
        private int pos;

        /*Constructor*/
        public FormIndicePrimario(FormEntidad formEntidad, List<Entidad> entidades, int pos)
        {
            this.formEntidad = formEntidad;
            this.entidades = entidades;
            this.pos = pos;

            InitializeComponent();
        }

        /*Al cargar el form, ponemos las textos de cabecera correspondientes y llamamos al metodo llenaData(), para
         que llene el data grid con los datos.*/
        private void FormIndicePrimario_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.HeaderText = "Clave";
            columna.ReadOnly = false;
            dgv_IndicePrimario.Columns.Add(columna);

            columna = new DataGridViewTextBoxColumn();
            columna.HeaderText = "Direccion";
            columna.ReadOnly = false;
            dgv_IndicePrimario.Columns.Add(columna);

            columna = new DataGridViewTextBoxColumn();
            columna.HeaderText = "Apuntador";
            columna.ReadOnly = false;
            dgv_IndicePrimario.Columns.Add(columna);

            llenaData();
        }

        /*Llenamos el data con los valores adecuados.*/
        private void llenaData()
        {
            int j = 0;
            //MessageBox.Show("en form primario: " + entidades[pos].primarios.Count);
            foreach (Primario primario in entidades[pos].primarios)
            {
                for (int i = 0; i < primario.indice.Count; ++i)
                {
                    dgv_IndicePrimario.Rows.Add(primario.indice[i].IndiceP_Clave.ToString()); //mostramos la clave
                    dgv_IndicePrimario.Rows[j].Cells[1].Value = primario.indice[i].IndiceP_Direccion;
                    if (i == primario.indice.Count - 1)
                    {
                        dgv_IndicePrimario.Rows[j].Cells[2].Value = primario.apuntador_Siguiente;
                    }
                    j++;
                }
            }
        }

        /*Evento para poder regresar a las entidades*/
        private void btn_regresaEntidad_Click(object sender, EventArgs e)
        {
            dgv_IndicePrimario.Rows.Clear();
            this.Close();
            cambiar(formEntidad, entidades);
        }
    }
}
