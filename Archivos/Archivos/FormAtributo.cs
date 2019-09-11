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
    public partial class FormAtributo : Form
    {
        public delegate void pasar(FormEntidad fEntidad, List<Entidad> enti);
        public event pasar cambia;

        int pos; //posicion de la fila seleccionada
        FormEntidad formEntidad; // form de entidades
        Atributo atributo; //variable para el atributo 
        FuncionAtributo fa; //variable para acceder a las funciones de los atributos
        List<Entidad> entidades;

        /*Nombre del archivo*/
        string nombreArchivo;

        public FormAtributo()
        {
            InitializeComponent();
        }

        /*Constructor del form*/
        public FormAtributo(FormEntidad formEntidad, List<Entidad> entidades, string nombreArchivo, int pos)
        {
            this.pos = pos;
            this.formEntidad = formEntidad;
            this.entidades = entidades;
            this.nombreArchivo = nombreArchivo;
            entidades = new List<Entidad>();
            fa = new FuncionAtributo();
            InitializeComponent();
        }

        /*Regresar a las entidades*/
        private void btn_Regreso_Click(object sender, EventArgs e)
        {
            dgv_Atributo.Rows.Clear();
            this.Close();
        }

        /*Si se quiere crear un nuevo atributo se habilitan los campos necesarios*/
        private void btn_CrearAtributo_Click(object sender, EventArgs e)
        {
            botonesVisibles(true);
        }

        /*Metodo para hacer visible o no los textbox o botones del form*/
        private void botonesVisibles(bool visible)
        {
            lb_Nombre.Visible = visible;
            lb_indice.Visible = visible;
            lb_Longitud.Visible = visible;
            lb_Tipo.Visible = visible;
            tb_Nombre.Visible = visible;
            tb_Longitud.Visible = visible;
            cb_TipoDato.Visible = visible;
            cb_Indice.Visible = visible;
            btn_Aceptar.Visible = visible;
        }

        /*Crear un nuevo atributo*/
        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
            if (tb_Nombre != null || tb_Nombre.Text != "")
            {
                if (cb_TipoDato.Text != "")
                {
                    atributo = new Atributo(tb_Nombre.Text, 
                                            Convert.ToChar(cb_TipoDato.Text), 
                                            Convert.ToInt16(tb_Longitud.Text), 
                                            Convert.ToInt16(cb_Indice.Text));

                    entidades.ElementAt(pos).agregarAtributo(atributo); //Agregamos la entidad seleccionada.
                    //MessageBox.Show(nombreArchivo);
                    if (fa.agregaAtributoArchivo(nombreArchivo, pos, entidades))
                    {
                        llenaDataG();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Verifica si los campos estan completos.");
                }
            }
            else
            {
                MessageBox.Show("Verifica si los campos estan completos.");
            }
        }

        private void FormAtributo_Load(object sender, EventArgs e)
        {
            botonesVisibles(false);
            tb_Nombre.Text = "";
            tb_Longitud.Text = "";
            //llenaDataG();
        }

        /*Llenar con los datos la tabla*/
        private void llenaDataG()
        {
            dgv_Atributo.Rows.Clear();

            foreach (Atributo at in entidades.ElementAt(pos).atributos)
            {
                int identid = BitConverter.ToInt16(at.id_Atributo, 0);
                dgv_Atributo.Rows.Add(identid, at.string_Nombre, at.tipo_Dato, at.longitud_Tipo, at.direccion_Atributo, at.tipo_Indice, at.direccion_Indice, at.direccion_sigAtributo);
            }
        }

        /*Boton para modificar los atributos*/
        private void btn_modifAtributo_Click(object sender, EventArgs e)
        {
            int posAt = dgv_Atributo.CurrentRow.Index;

            if (fa.modificaAtributoSel(tb_Nombre.Text, cb_Indice.Text, cb_TipoDato.Text, tb_Longitud.Text,posAt, entidades))
            {
                tb_Nombre.Text = "";
                tb_Longitud.Text = "";
                cb_TipoDato.Text = "";
                cb_Indice.Text = "";

                llenaDataG();
            }
        }
        /*Boton para eliminar el atributo seleccionado*/
        private void btn_eliminarAtributo_Click(object sender, EventArgs e)
        {
            int posAt = dgv_Atributo.CurrentRow.Index;

            if (fa.eliminarAtributo(posAt, entidades))
            {
                llenaDataG();
            }
        }

        /*Metodo para acompletar la longitud, dependiendo del tipo de dato*/
        private void cb_TipoDato_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_TipoDato.Text.CompareTo("E") == 0) //si es igual
            {
                tb_Longitud.Text = "4";
            }
            else if(cb_TipoDato.Text.CompareTo("C") == 0)
            {
                tb_Longitud.Text = "25";
            }
        }
    }
}
