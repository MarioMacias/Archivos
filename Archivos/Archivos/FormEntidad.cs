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
    public partial class FormEntidad : Form
    {
        FuncionEntidad fa; //Variable para las funciones de un archivo, crear, guardar...
        Entidad entidad; //Variable para las entidades.
        List<Entidad> entidades; //Lista para poder guardar todas las entidades que se vayan creando.

        private long cab; //Variable para la cabezera de las entidades;

        public FormEntidad()
        {
            InitializeComponent();
            fa = new FuncionEntidad(); // variable para las funciones de un archivo.
            entidades = new List<Entidad>(); // Lista para guardar todas las entidades.

            cab = -1; //Asignacion para la primera cabezera.

            //iniciar con los botones en enable.
            botonVisible(false);
        }

        /*Creamos un nuevo archivo, para las entidades de fomra binaria*/
        private void nuevoArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fa.crearArchivo())
            {
                lbl_Cabecera.Text = fa.asignarCabecera(cab);  // forma visual de mostrar la cabezera.

                //Mostrar los botones.
                botonVisible(true);
            }   // verifica si se pudo crear el archivo.
        }

        /*Creación de una nueva entidad*/
        private void btn_CrearEntidad_Click(object sender, EventArgs e)
        {
            if (tb_entidad != null || tb_entidad.Text != "")
            {
                entidad = new Entidad(tb_entidad.Text); //Nueva entidad, con el nombre del tb.
                entidades.Add(entidad); //se agrega la entidad creada a la lista
                List<Entidad> auxListEntidad;
                auxListEntidad = fa.asigrarDatos(entidades); //Le paso la lista de entidades para poder asignar la direccion de la entidad.
                if (auxListEntidad != null) // si se pudo hacer todos los pasos para guardar la nueva entidad y no fue null
                {
                    entidades = auxListEntidad;
                    lbl_Cabecera.Text = fa.nuevaCabecera(entidades); //mostramos la nueva cabecera
                    datosDataG(); //Poner los datos en el data
                }
                else
                {
                    MessageBox.Show("Ocurrio un error");
                }
            }
            else
            {
                MessageBox.Show("¿Tiene nombre la identidad?");
            }
            tb_entidad.Text = "";
        }

        /*Ponemos las identidades en el data grid view*/
        public void datosDataG()
        {
            dgv_Entidad.Rows.Clear();
            foreach(Entidad en in entidades){
                int identid= BitConverter.ToInt16(en.Id_Entidad,0);
                dgv_Entidad.Rows.Add(identid, en.string_Nombre, en.direccion_Entidad, en.direccion_Atributo, en.direccion_Dato, en.direccion_Siguiente);
            }
        }

        /*Cambiar la visibilidad de los botones de crear, borrar, modificar, tb entidad*/
        private void botonVisible(bool visible)
        {
            btn_CrearEntidad.Enabled = visible;
            btn_Borrar.Enabled = visible;
            btn_Modificar.Enabled = visible;
            tb_entidad.Enabled = visible;
            btn_Atributo.Enabled = visible;
            lb_atributo.Visible = visible;
            tb_modificar.Visible = visible;
            tb_modificar.Text = "";
        }

        /*Si se quiere agregar atributos a cierta entidad*/
        private void btn_Atributo_Click(object sender, EventArgs e)
        {
            if (dgv_Entidad.SelectedCells != null)
            {
                int posicion = dgv_Entidad.CurrentRow.Index; //saber la pos de la fila que se selecciono

                this.Hide();
                FormAtributo fAtributo = new FormAtributo(this, entidades, fa.nombreArchivo, posicion);
                fAtributo.cambia += new FormAtributo.pasar(regresa);
                fAtributo.Show();
            }
            else
            {
                MessageBox.Show("Seleccione una entidad para ver sus atributos.");
            }
        }

        /*Evento para poder pasar de un form a otro*/
        private void regresa(FormEntidad fed, List<Entidad> enti)
        {
            entidades = enti;
            fed.Show();
            datosDataG();
        }

        private void FormEntidad_Load(object sender, EventArgs e)
        {

        }

        /*Boton para modificar el archivo*/
        private void btn_Modificar_Click(object sender, EventArgs e)
        {
            if (tb_modificar != null || tb_modificar.Text != "")
            {
                int pos = dgv_Entidad.CurrentRow.Index;

                if(fa.modificaElemento(tb_modificar.Text, pos, entidades))
                {
                    tb_modificar.Text = "";
                    datosDataG();
                }
            }
        }

        /*Boton para borrar una parte*/
        private void btn_Borrar_Click(object sender, EventArgs e)
        {
            int pos = dgv_Entidad.CurrentRow.Index;

            string res = fa.eliminarEntidad(pos, entidades);

            if (res != null)
            {
                lbl_Cabecera.Text = res;
            }
            else
            {
                datosDataG();
            }
        }
    }
}
