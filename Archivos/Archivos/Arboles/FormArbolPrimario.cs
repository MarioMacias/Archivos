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
    public partial class FormArbolPrimario : Form
    {
        public delegate void cambio(FormEntidad formEntidad, List<Entidad> entidades);
        public event cambio cambiar;

        private FormEntidad formEntidad;
        private List<Entidad> entidades;
        private int pos;
        private int posListNodo = 0;
        private int ii = 0;
        private int j = 0;
        private bool raizIter;
        private bool termino = false;
        int numColumn = 0;

        public FormArbolPrimario(FormEntidad formEntidad, List<Entidad> entidades, int pos)
        {
            this.formEntidad = formEntidad;
            this.entidades = entidades;
            this.pos = pos;

            InitializeComponent();
        }

        private void FormArbolPrimario_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.HeaderText = "Tipo";
            columna.ReadOnly = false;
            dgv_IndicePrimario.Columns.Add(columna);

            columna = new DataGridViewTextBoxColumn();
            columna.HeaderText = "Direccion Nodo";
            columna.ReadOnly = false;
            dgv_IndicePrimario.Columns.Add(columna);

            for (int i = 0; i < entidades[pos].Arboles.Last().getGrado; i++)
            {
                columna = new DataGridViewTextBoxColumn();
                columna.HeaderText = "P" + (i + 1).ToString();
                columna.ReadOnly = false;
                dgv_IndicePrimario.Columns.Add(columna);

                columna = new DataGridViewTextBoxColumn();
                columna.HeaderText = "K" + (i + 1).ToString();
                columna.ReadOnly = false;
                dgv_IndicePrimario.Columns.Add(columna);
            }

            columna = new DataGridViewTextBoxColumn();
            columna.HeaderText = "Apuntador Siguiente";
            columna.ReadOnly = false;
            dgv_IndicePrimario.Columns.Add(columna);

            numColumn = dgv_IndicePrimario.ColumnCount;
            entidades[pos].Arboles.Last().acomodaLista();
            llenaData();
            llenaDataPrueba();
        }

        /*Lista acomodad*/
        private void llenaData()
        {
            int i = 0;
            j = 0;
            raizIter = false;
            int numColumn = dgv_IndicePrimario.ColumnCount;
            //foreach (Nodo nodo in entidades[pos].Arboles.Last().getListNodo)
            foreach (Nodo nodo in entidades[pos].Arboles.Last().getListNodoAcomodado)
            {
                i = 0;
                raizIter = false;
                dgv_IndicePrimario.Rows.Add(nodo.TipoDeNodo.ToString());
                i++;
                dgv_IndicePrimario.Rows[j].Cells[i].Value = nodo.Direccion.ToString();
                i++;
                foreach (ClaveBusqueda cb in nodo.clavesBusqueda)
                {
                    if (cb.Clave.ToString() != "-1")
                    {
                        if (nodo.TipoDeNodo == 'R' || nodo.TipoDeNodo == 'I')
                        {
                            if (!raizIter)
                            {
                                dgv_IndicePrimario.Rows[j].Cells[i].Value = cb.DireccionIzquierda.ToString();
                                i++;
                                dgv_IndicePrimario.Rows[j].Cells[i].Value = cb.Clave.ToString();
                                i++;
                                dgv_IndicePrimario.Rows[j].Cells[i].Value = cb.DireccionDerecha.ToString();
                                i++;
                                raizIter = true;
                            }
                            else
                            {
                                dgv_IndicePrimario.Rows[j].Cells[i].Value = cb.Clave.ToString();
                                i++;
                                dgv_IndicePrimario.Rows[j].Cells[i].Value = cb.DireccionDerecha.ToString();
                                i++;
                            }
                        }
                        else
                        {
                            dgv_IndicePrimario.Rows[j].Cells[i].Value = cb.DireccionIzquierda.ToString();
                            i++;
                            dgv_IndicePrimario.Rows[j].Cells[i].Value = cb.Clave.ToString();
                            i++;
                        }
                    }
                }

                if (nodo.TipoDeNodo != 'R' && nodo.TipoDeNodo != 'I')
                {
                    dgv_IndicePrimario.Rows[j].Cells[numColumn - 1].Value = nodo.Direccion_Siguiente.ToString();
                }
                j++;
            }
        }

        /*Lista normal*/
        private void llenaDataPrueba()
        {
            int i = 0;
            j = 0;
            raizIter = false;
            int numColumn = Data_prueba.ColumnCount;
            //foreach (Nodo nodo in entidades[pos].Arboles.Last().getListNodo)
            foreach (Nodo nodo in entidades[pos].Arboles.Last().getListNodo)
            {
                i = 0;
                raizIter = false;
                Data_prueba.Rows.Add(nodo.TipoDeNodo.ToString());
                i++;
                Data_prueba.Rows[j].Cells[i].Value = nodo.Direccion.ToString();
                i++;
                foreach (ClaveBusqueda cb in nodo.clavesBusqueda)
                {
                    if (cb.Clave.ToString() != "-1")
                    {
                        if (nodo.TipoDeNodo == 'R' || nodo.TipoDeNodo == 'I')
                        {
                            if (!raizIter)
                            {
                                Data_prueba.Rows[j].Cells[i].Value = cb.DireccionIzquierda.ToString();
                                i++;
                                Data_prueba.Rows[j].Cells[i].Value = cb.Clave.ToString();
                                i++;
                                Data_prueba.Rows[j].Cells[i].Value = cb.DireccionDerecha.ToString();
                                i++;
                                raizIter = true;
                            }
                            else
                            {
                                Data_prueba.Rows[j].Cells[i].Value = cb.Clave.ToString();
                                i++;
                                Data_prueba.Rows[j].Cells[i].Value = cb.DireccionDerecha.ToString();
                                i++;
                            }
                        }
                        else
                        {
                            Data_prueba.Rows[j].Cells[i].Value = cb.DireccionIzquierda.ToString();
                            i++;
                            Data_prueba.Rows[j].Cells[i].Value = cb.Clave.ToString();
                            i++;
                        }
                    }
                }

                if (nodo.TipoDeNodo != 'R' && nodo.TipoDeNodo != 'I')
                {
                    Data_prueba.Rows[j].Cells[numColumn - 1].Value = nodo.Direccion_Siguiente.ToString();
                }
                j++;
            }
        }
        
        private void btn_regresaEntidad_Click(object sender, EventArgs e)
        {
            dgv_IndicePrimario.Rows.Clear();
            this.Close();
            cambiar(formEntidad, entidades);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
