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
    public partial class FormRegistro : Form
    {
        public delegate void regresar(FormEntidad formEntidad, List<Entidad> enidades); //para regresar a la ventada de entidades
        public event regresar cambia;

        private FormEntidad formEntidad;
        public List<Entidad> entidades;
        private List<object> datos_registro;

        private int pos;
        private int auxPrimario;
        private Registro Raux;

        /*Variables para el tipo de indice*/
        private int indice1 = -1; //Primario
        private int indiceCB = -1; //Clave de busqueda
        private bool indice0 = false; //Sin Indice
        private int indice2 = -1; // Secundario
        private int indiceA1 = -1; //Arbol primario
        private int indiceA2 = -1; //Arbol secundario


        private Registro registro;
        private Primario primario;
        private static ArbolB arbolB = null;

        private static bool bandera = false;
        private static bool banderaIDX = false;
        private static bool banderaIDXar = false;

        public FuncionRegistro fr = new FuncionRegistro();
        public FuncionIndicePrimario fip = new FuncionIndicePrimario();
        public FuncionSecundario fs = new FuncionSecundario();

        private FileStream Fichero, Fichero2;
        private FileStream FicheroArPri, FicheroArSec;

        private static bool se = false;

        private string nombreArchivoDAT, nombreArchivoIDX, nombreArchivo, nombreArchivoIDXsecundario, nombreArchivoArPri, nombreArchivoArSec;

        /*Constructor para la nueva creacion de un registro*/
        public FormRegistro(FormEntidad formEntidad, FileStream Fichero,string nombreArchivo, List<Entidad> entidades, int pos)
        {
            this.nombreArchivo = nombreArchivo;
            this.Fichero = Fichero;
            this.Fichero2 = Fichero;
            FicheroArPri = Fichero;
            FicheroArSec = Fichero;
            this.formEntidad = formEntidad;
            this.entidades = entidades;
            this.pos = pos;
            fr = new FuncionRegistro();
            fr.nuevaPosicion = pos;
            fr.lisEntidades = entidades;
            InitializeComponent();
        }

        private void pruebaArbolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Nodo nodo in entidades[pos].Arboles.Last().getListNodo)
            {
                MessageBox.Show("tipo: " + nodo.TipoDeNodo.ToString());
                MessageBox.Show("Direccion: " + nodo.Direccion.ToString());
                foreach (ClaveBusqueda cb in nodo.clavesBusqueda)
                {
                    if (Convert.ToInt32(cb.Clave) != -1)
                    {
                        if (nodo.TipoDeNodo == 'I' || nodo.TipoDeNodo == 'R')
                        {
                            MessageBox.Show("Direccion izquierda: " + cb.DireccionIzquierda.ToString());
                            MessageBox.Show("clave: " + cb.Clave.ToString());
                            MessageBox.Show("Direccion derecha: " + cb.DireccionDerecha.ToString());
                        }
                        else
                        {
                            MessageBox.Show("Direccion cb: " + cb.DireccionIzquierda.ToString());
                            MessageBox.Show("clave cb: " + cb.Clave.ToString());
                        }
                    }
                }
                
                MessageBox.Show("Dir siguiente:" + nodo.Direccion_Siguiente.ToString());
            }
        }

        private void FormRegistro_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        /*Aqui genera el nombre de cada columna y crea los archivos .dat, .idx. Se guarda en la clase de las funciones*/
        private void FormRegistro_Load(object sender, EventArgs e)
        {
            //Primero agregamos la columna de la direccion del registro
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.HeaderText = "Dir. Registro";
            columna.ReadOnly = true;
            dgv_Registro.Columns.Add(columna);

            //Despues todos los nombres de los atributos
            foreach (Atributo en in entidades.ElementAt(pos).atributos)
            {
                columna = new DataGridViewTextBoxColumn();
                columna.HeaderText = en.string_Nombre;
                columna.ReadOnly = false;
                dgv_Registro.Columns.Add(columna);
            }

            //Al final la columna del apuntador siguiente del registro
            columna = new DataGridViewTextBoxColumn();
            columna.HeaderText = "Apuntador siguiente";
            columna.ReadOnly = true;
            dgv_Registro.Columns.Add(columna);

            //Aqui empiezo a crear el archivo, dependiendo de la direccion de dato, si ya existe o no, para todos los indices se ocupa.
            
            if (entidades[pos].direccion_Dato == -1)
            {
                string ex = ".dat";
                string aux2 = BitConverter.ToString(entidades[pos].Id_Entidad);

                nombreArchivoDAT = aux2;
                nombreArchivoDAT += ex;
                bandera = true;
                Fichero = new FileStream(nombreArchivoDAT, FileMode.Create);
                Fichero.Close();
            }
            else
            {
                string ex = ".dat";
                string aux2 = BitConverter.ToString(entidades[pos].Id_Entidad);

                nombreArchivoDAT = aux2;
                nombreArchivoDAT += ex;
                fr.setNameFichero(Fichero,nombreArchivoDAT, nombreArchivoIDX, nombreArchivo);
                 if (bandera == false)
                 {
                    fr.lisEntidades = entidades;
                     fr.asignaDatosDat();
                     bandera = true;
                 }
            } 

            //Empezamos a buscar los indices 
            indiceCB = fr.buscaClaveBusqueda();
            indice1 = fr.buscaIndicePrimario();
            indice0 = fr.verificaSinClave();
            indice2 = fr.buscaIndiceSecundario();
            indiceA1 = fr.buscaIndiceAprimario();
            indiceA2 = fr.buscaIndiceAsecundario();

            //Obtenemos el archivo indice, una vez buscado
            if (indice1 != -1)
            {
                if (entidades[pos].atributos[indice1].direccion_Indice == -1) //Verificamos si la direccion de indice es -1, osea que no existe ninguno
                {
                    string ex = ".idx";
                    string aux2 = BitConverter.ToString(entidades[pos].atributos[indice1].id_Atributo);
                    
                    nombreArchivoIDX = aux2;
                    nombreArchivoIDX += ex;
                    Fichero = new FileStream(nombreArchivoIDX, FileMode.Create);

                    if (indice2 != -1)
                    {
                        string aux3 = BitConverter.ToString(entidades[pos].atributos[indice2].id_Atributo);

                        nombreArchivoIDXsecundario = aux3;
                        nombreArchivoIDXsecundario += ex;
                        Fichero2 = new FileStream(nombreArchivoIDXsecundario, FileMode.Create);
                        fs.listEntidades = entidades;
                        fs.posicionEntidad = pos;
                        fs.posicionIndice2 = indice2;

                        fs.setNameFichero(Fichero2, nombreArchivoDAT, nombreArchivoIDXsecundario, nombreArchivo); //dando nombre y fichero para guardar los archivos
                        Fichero2.Close();
                    }
                    banderaIDX = true;
                    Fichero.Close();
                }
                else
                {
                    string ex = ".idx";
                    string aux2 = BitConverter.ToString(entidades[pos].atributos[indice1].id_Atributo);
                    
                    nombreArchivoIDX = aux2;
                    nombreArchivoIDX += ex;
                    fr.setNameFichero(Fichero, nombreArchivoDAT, nombreArchivoIDX, nombreArchivo); //dando nombre y fichero para guardar los archivos

                    if (indice2 != -1)
                    {
                        string aux3 = BitConverter.ToString(entidades[pos].atributos[indice2].id_Atributo);

                        nombreArchivoIDXsecundario = aux3;
                        nombreArchivoIDXsecundario += ex;

                        fs.listEntidades = entidades;
                        fs.posicionEntidad = pos;
                        fs.posicionIndice2 = indice2;

                        fs.setNameFichero(Fichero2, nombreArchivoDAT, nombreArchivoIDXsecundario, nombreArchivo); //dando nombre y fichero para guardar los archivos

                    }

                    //Ya no lee el archivo de nuevo
                    if (banderaIDX == false)
                    {
                        // MessageBox.Show("hola");
                        if (entidades[pos].primarios.Count == 0 || entidades[pos].primarios.Last().primario_Iteracion == entidades[pos].primarios.Last().indice.Count)
                        {
                            banderaIDX = true;
                            fip.asignaDatosNecesarios(entidades, pos, indice1, Fichero, nombreArchivoIDX, nombreArchivo);
                            fip.asignaMemoriaDatosIndice();
                            entidades = fip.dameEntidades;
                            //fr.asignaDatosDat();

                            if (indice2 != -1)
                            {
                                fs.listEntidades = entidades;
                                fs.posicionEntidad = pos;
                                fs.posicionIndice2 = indice2;
                                fs.setNameFichero(Fichero2, nombreArchivoDAT, nombreArchivoIDXsecundario, nombreArchivo); //dando nombre y fichero para guardar los archivos

                                if (entidades[pos].secundarios.Count == 0 || (entidades[pos].secundarios.Last().getIteracion == entidades[pos].secundarios.Last().listSecD.Count))
                                {
                                    fs.asignarMemoriaIndice2();
                                    entidades = fs.listEntidades;
                                }
                            }

                        }
                    }
                } //Mensaje de error
            }
            else if (indice2 != -1)
            {
                if (entidades[pos].atributos[indice2].direccion_Indice == -1)
                {
                    string ex = ".idx";
                    string aux2 = BitConverter.ToString(entidades[pos].atributos[indice2].id_Atributo);

                    nombreArchivoIDXsecundario = aux2;
                    nombreArchivoIDXsecundario += ex;
                    Fichero2 = new FileStream(nombreArchivoIDXsecundario, FileMode.Create);

                    fs.listEntidades = entidades;
                    fs.setNameFichero(Fichero2, nombreArchivoDAT, nombreArchivoIDXsecundario, nombreArchivo); //dando nombre y fichero para guardar los archivos

                    Fichero2.Close();
                }
                else
                {
                    string ex = ".idx";
                    string aux2 = BitConverter.ToString(entidades[pos].atributos[indice2].id_Atributo);

                    nombreArchivoIDXsecundario = aux2;
                    nombreArchivoIDXsecundario += ex;
                    fs.listEntidades = entidades;
                    fs.setNameFichero(Fichero2, nombreArchivoDAT, nombreArchivoIDXsecundario, nombreArchivo); //dando nombre y fichero para guardar los archivos
                    
                    ///Para no vuelva a leer el archivo
                    if (entidades[pos].secundarios.Count == 0 || (entidades[pos].secundarios.Last().getIteracion == entidades[pos].secundarios.Last().listSecD.Count))
                    {
                        fs.asignarMemoriaIndice2();
                        entidades = fs.listEntidades;
                    }
                }
            }

            if (indiceA1 != -1)
            {
                if (entidades[pos].atributos[indiceA1].direccion_Indice == -1) //Verificamos si la direccion de indice es -1, osea que no existe ninguno
                {
                    string ex = ".idx";
                    string aux2 = BitConverter.ToString(entidades[pos].atributos[indiceA1].id_Atributo);

                    nombreArchivoArPri = aux2;
                    nombreArchivoArPri += ex;
                    FicheroArPri = new FileStream(nombreArchivoArPri, FileMode.Create);

                    if (indiceA2 != -1)
                    {
                        string aux3 = BitConverter.ToString(entidades[pos].atributos[indiceA2].id_Atributo);

                        nombreArchivoArSec = aux3;
                        nombreArchivoArSec += ex;
                        FicheroArSec = new FileStream(nombreArchivoArSec, FileMode.Create);
                        FicheroArSec.Close();
                    }
                    banderaIDXar = true;
                    FicheroArPri.Close();
                }
                else
                {
                    string ex = ".idx";
                    string aux2 = BitConverter.ToString(entidades[pos].atributos[indiceA1].id_Atributo);

                    nombreArchivoArPri = aux2;
                    nombreArchivoArPri += ex;
                    if (indice2 != -1)
                    {
                        string aux3 = BitConverter.ToString(entidades[pos].atributos[indice2].id_Atributo);

                        nombreArchivoArSec = aux3;
                        nombreArchivoArSec += ex;
                    }

                   /* //Ya no lee el archivo de nuevo
                    if (banderaIDX == false)
                    {
                        // MessageBox.Show("hola");
                        if (entidades[pos].primarios.Count == 0 || entidades[pos].primarios.Last().primario_Iteracion == entidades[pos].primarios.Last().indice.Count)
                        {
                            banderaIDX = true;
                            fip.asignaDatosNecesarios(entidades, pos, indice1, Fichero, nombreArchivoIDX, nombreArchivo);
                            fip.asignaMemoriaDatosIndice();
                            entidades = fip.dameEntidades;
                            //fr.asignaDatosDat();

                            if (indice2 != -1)
                            {
                                fs.listEntidades = entidades;
                                fs.posicionEntidad = pos;
                                fs.posicionIndice2 = indice2;
                                fs.setNameFichero(Fichero2, nombreArchivoDAT, nombreArchivoIDXsecundario, nombreArchivo); //dando nombre y fichero para guardar los archivos

                                if (entidades[pos].secundarios.Count == 0 || (entidades[pos].secundarios.Last().getIteracion == entidades[pos].secundarios.Last().listSecD.Count))
                                {
                                    fs.asignarMemoriaIndice2();
                                    entidades = fs.listEntidades;
                                }
                            }

                        }
                    }*/
                }
            }

            escribirDatosData();
            fr.setNameFichero(Fichero, nombreArchivoDAT, nombreArchivoIDX, nombreArchivo); //dando nombre y fichero para guardar los archivos
        }

        private void dgv_Registro_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        /*Metodo para poder regresar a la tabla de entidades*/
        private void btn_regresaEntidad_Click(object sender, EventArgs e)
        {
            dgv_Registro.Rows.Clear();

            this.Close();
            cambia(formEntidad, entidades);
        }

        /*Evento para agregar la nueva */
        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void guardar()
        {
            fr.lisEntidades = entidades; //pasando la lista de entidades al primario
            fs.listEntidades = entidades; //pasando la lista de entidades al funcion secundario

            if (indice1 != -1) //Si es la primera vez, indice primario = -1;
            {
                if (creaListaObjetos()) //se crea la lista con todos los objetos a guardar
                {
                    registro = fr.creaNuevoRegistro(datos_registro); //creamos el registro guardando los datos
                    registro.iteraREG++;
                    if (entidades[pos].atributos[indice1].direccion_Indice != -1) //Verificamos si el indice existe
                    {
                        for (int i = 0; i < entidades[pos].primarios.Count; ++i) //recorremos la lista de primarios
                        {
                            if (entidades[pos].primarios[i].primario_Iteracion < entidades[pos].primarios[i].indice.Count) //saber cual va primero
                            {
                                auxPrimario = i; //saber la posicion de la madre esa
                            }
                        }
                        registro = fr.creaNuevoRegistro(datos_registro); //creamos el registro guardando los datos
                        //Aqui se crea el registro?

                        if (fr.Compara(registro)) //Comparar para ingresar uno nuevo en el archivo de datos
                        {
                            MessageBox.Show("Ya existe una clave igual.");
                        }
                        else //Si no, lo ponemos en el archivo, ya que no es igual
                        {
                            entidades[pos].registros.Add(registro); //Agrega el registro a la lista que esta en las entidades
                            //Refresh();
                            fr.lisEntidades = entidades;
                            fr.asignaDatos(); //ponemos los datos en el archivos
                            entidades = fr.lisEntidades;
                            entidades[pos].primarios[auxPrimario].indice[entidades[pos].primarios[auxPrimario].primario_Iteracion].IndiceP_Clave = entidades[pos].registros.Last().element_Registro[indice1];
                            entidades[pos].primarios[auxPrimario].indice[entidades[pos].primarios[auxPrimario].primario_Iteracion].IndiceP_Direccion = entidades[pos].registros.Last().dir_Registro;

                            entidades[pos].primarios[auxPrimario].primario_Iteracion += 1;//se suma a la posicion

                            fr.lisEntidades = entidades;
                            //Refresh();
                            fr.ordenarIndice();
                            fr.escribirDatosNuevosArchivoIndice();
                            entidades = fr.lisEntidades;

                            //Se verifica si existe algun indice secundario
                            if (indice2 != -1)
                            {
                                //MessageBox.Show("entro en el segundo");
                                fs.listEntidades = entidades;
                                fs.posicionIndice2 = indice2;
                                //MessageBox.Show("Entre en indice dos");
                                ///Si encuentra un bloque con la misma clave insertara la direccion
                                if (fs.buscarBloque(registro) != -1)
                                {
                                    int index = fs.buscarBloque(registro);
                                    //MessageBox.Show("bloque en primero: " + index);
                                    entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().listIndiceSecundario[entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().getIteracion].getDireccion = entidades[pos].registros.Last().dir_Registro;
                                    entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().getIteracion += 1;
                                    fs.listEntidades = entidades;
                                    fs.reescribirDirecciones(index);
                                    entidades = fs.listEntidades;
                                }
                                ///Si no la encuentra creara un nuevo bloque e insertara la direccion en su bloque de direcciones
                                else if (fs.buscarBloque(registro) == -1)
                                {
                                    int n = fs.numeroDeIteracion(8);
                                    //MessageBox.Show("espacio: " + n);
                                    entidades[pos].secundarios.Last().listSecD[entidades[pos].secundarios.Last().getIteracion].agregarBloquesDirecciones(-1);

                                    for (int i = 1; i < n; ++i)
                                    {
                                        entidades[pos].secundarios.Last().listSecD[entidades[pos].secundarios.Last().getIteracion].listSecDirs.First().addIndice(-1);
                                    }

                                    entidades[pos].secundarios.Last().listSecD[entidades[pos].secundarios.Last().getIteracion].getClave = entidades[pos].registros.Last().element_Registro[indice2];

                                    entidades[pos].secundarios.Last().listSecD[entidades[pos].secundarios.Last().getIteracion].listSecDirs.Last().listIndiceSecundario.First().getDireccion = entidades[pos].registros.Last().dir_Registro;

                                    entidades[pos].secundarios.Last().listSecD[entidades[pos].secundarios.Last().getIteracion].listSecDirs.Last().getIteracion += 1;

                                    fs.listEntidades = entidades;
                                    fs.escribirIndice2Exist(entidades[pos].secundarios.Last().getIteracion);
                                    entidades = fs.listEntidades;
                                    entidades[pos].secundarios.Last().getIteracion += 1;
                                    fs.listEntidades = entidades;
                                    fs.ordenarIndiceSecundario();
                                    fs.reescribirCajones();
                                    entidades = fs.listEntidades;
                                }
                                entidades = fs.listEntidades;
                                MessageBox.Show("Se guardo correctamente con indice 3 y 2.");
                            }

                            //escribir el archivo 
                            fr.escribirArchivoDat();
                            //ordenar
                            fr.ordenarDatosXcB();
                            //las direcciones de los datos
                            fr.direccionNuevaDatos();
                            entidades = fr.lisEntidades;
                            //escribir el registro, data
                            escribirDatosData();
                            if (indice2 == -1)
                                MessageBox.Show("Se guardo correctamente.");
                        }
                    } //si ya existe el archivo
                    else //Si no existe el archivo de indice, se agregara el primero, osea que en los atributos esta en -1
                    {
                        entidades.ElementAt(pos).registros.Add(registro);
                        fr.lisEntidades = entidades;
                        fr.asignaDatos();
                        entidades = fr.lisEntidades;
                        int ite = fr.numeroDeIteracion(entidades[pos].atributos[indice1].longitud_Tipo);

                        primario = new Primario(-1, -1, entidades[pos].atributos[indice1]); //clave y direccion en -1
                        entidades[pos].primarios.Add(primario);

                        for (int i = 1; i < ite; ++i)
                        {
                            entidades[pos].primarios.Last().AddIndice(-1, -1, entidades[pos].atributos[indice1]); // agregar los espacios con -1
                        }

                        entidades[pos].primarios.Last().indice[entidades[pos].primarios.Last().primario_Iteracion].IndiceP_Clave = entidades[pos].registros.Last().element_Registro[indice1];
                        entidades[pos].primarios.Last().indice[entidades[pos].primarios.Last().primario_Iteracion].IndiceP_Direccion = entidades[pos].registros.Last().dir_Registro;
                        entidades[pos].primarios.Last().primario_Iteracion += 1;

                        fip.asignaDatosNecesarios(entidades, pos, indice1, Fichero, nombreArchivoIDX, nombreArchivo);

                        fip.asignarDireccionIndice();
                        fip.escribirDireccionesIndice(indice1);
                        fip.escribirDatosArchivoIndice();
                        //escribir el archivo 
                        fr.escribirArchivoDat();
                        //ordenar
                        fr.ordenarDatosXcB();
                        //las direcciones de los datos
                        fr.direccionNuevaDatos();
                        //escribir el registro, data
                        escribirDatosData();

                        //Buscamos un indice secundario
                        if (indice2 != -1)
                        {
                            fs.listEntidades = entidades;
                            fs.setNameFichero(Fichero2, nombreArchivoDAT, nombreArchivoIDXsecundario, nombreArchivo); //dando nombre y fichero para guardar los archivos
                            fs.posicionIndice2 = indice2;

                            ite = 0;
                            ite = fs.numeroDeIteracion(entidades[pos].atributos[indice2].longitud_Tipo);
                            //MessageBox.Show("longitud: " + entidades[pos].atributos[indice2].longitud_Tipo.ToString());
                            Secundario s = new Secundario(-1);

                            entidades[pos].secundarios.Add(s);

                            for (int i = 1; i < ite; ++i)
                            {
                                entidades[pos].secundarios.Last().AddIndice(-1);
                            }

                            entidades[pos].secundarios.Last().listSecD[entidades[pos].secundarios.Last().getIteracion].getClave = entidades[pos].registros.Last().element_Registro[indice2];
                            //MessageBox.Show("El dato del registro del indice dos es");
                            //MessageBox.Show(entidades[pos].registros.Last().element_Registro[indice2].ToString());
                            /// Para el archivo de indices, indice secundario

                            fs.listEntidades = entidades;

                            fs.asignarDireccionIndiceSecundario();
                            fs.ponerDireccionIndice(indice2);

                            entidades = fs.listEntidades;

                            ite = 0;
                            ite = fs.numeroDeIteracion(8);

                            SecundarioCve s1 = new SecundarioCve(-1);

                            entidades[pos].secundarios.Last().listSecD.First().agregarBloquesDirecciones(-1);

                            ///Llenamos nuestros cajones 
                            for (int i = 1; i < ite; ++i)
                            {
                                entidades[pos].secundarios.Last().listSecD.First().listSecDirs.First().addIndice(-1);
                            }
                            ///Le damos valor al primer cajon
                            //MessageBox.Show("Ultima direccion: " + entidades[pos].registros.Last().dir_Registro.ToString());
                            entidades[pos].secundarios.First().listSecD.First().listSecDirs.Last().listIndiceSecundario.First().getDireccion = entidades[pos].registros.Last().dir_Registro;

                            ///Por ultimo actualizamos los cajones para saber donde sera el siguiente lugar a escribir
                            ///Tanto de el cajon principal como para los otros cajones 
                            entidades[pos].secundarios.Last().getIteracion += 1;
                            entidades[pos].secundarios.Last().listSecD.First().listSecDirs.Last().getIteracion += 1;
                            fs.listEntidades = entidades;
                            fs.escribirArchivoInddiceSecundario();
                            entidades = fs.listEntidades;
                            MessageBox.Show("Se guardo correctamente con indice 3 y 2.");
                        }
                        if (indice2 == -1)
                            MessageBox.Show("Se guardo correctamente.");
                    }
                }
                else
                {
                    MessageBox.Show("Selecciona una fila con el apuntador.");
                }//mensaje de error
            } //Indice primario
            else if (indice2 != -1)
            {
                if (creaListaObjetos()) //se crea la lista con todos los objetos a guardar
                {
                    registro = fr.creaNuevoRegistro(datos_registro); //creamos el registro guardando los datos
                    registro.iteraREG++;
                    fs.setNameFichero(Fichero2, nombreArchivoDAT, nombreArchivoIDXsecundario, nombreArchivo); //dando nombre y fichero para guardar los archivos
                    fs.posicionIndice2 = indice2;

                    entidades[pos].registros.Add(registro);
                    fr.lisEntidades = entidades;
                    fr.asignaDatos();
                    entidades = fr.lisEntidades;
                    fs.listEntidades = entidades;
                    fs.posicionEntidad = pos;
                    fs.posicionIndice2 = indice2;

                    if (entidades[pos].atributos[indice2].direccion_Indice != -1)
                    {
                        if (fs.buscarBloque(registro) != -1)
                        {
                            int index = fs.buscarBloque(registro);

                            entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().listIndiceSecundario[entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().getIteracion].getDireccion = entidades[pos].registros.Last().dir_Registro;
                            entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().getIteracion += 1;
                            fs.listEntidades = entidades;
                            fs.reescribirDirecciones(index);
                            entidades = fs.listEntidades;
                        }
                        else if (fs.buscarBloque(registro) == -1)
                        {
                            int n = fs.numeroDeIteracion(8);

                            entidades[pos].secundarios.Last().listSecD[entidades[pos].secundarios.Last().getIteracion].agregarBloquesDirecciones(-1);

                            for (int i = 1; i < n; ++i)
                            {
                                entidades[pos].secundarios.Last().listSecD[entidades[pos].secundarios.Last().getIteracion].listSecDirs.First().addIndice(-1);
                            }

                            entidades[pos].secundarios.Last().listSecD[entidades[pos].secundarios.Last().getIteracion].getClave = entidades[pos].registros.Last().element_Registro[indice2];

                            entidades[pos].secundarios.Last().listSecD[entidades[pos].secundarios.Last().getIteracion].listSecDirs.Last().listIndiceSecundario.First().getDireccion = entidades[pos].registros.Last().dir_Registro;

                            entidades[pos].secundarios.Last().listSecD[entidades[pos].secundarios.Last().getIteracion].listSecDirs.Last().getIteracion += 1;

                            fs.listEntidades = entidades;

                            fs.escribirIndice2Exist(entidades[pos].secundarios.Last().getIteracion);

                            entidades = fs.listEntidades;

                            entidades[pos].secundarios.Last().getIteracion += 1;
                            fs.listEntidades = entidades;
                            fs.ordenarIndiceSecundario();
                            fs.reescribirCajones();
                            entidades = fs.listEntidades;
                        }
                    }
                    else
                    {
                        int n = 0;
                        n = fs.numeroDeIteracion(entidades[pos].atributos[indice2].longitud_Tipo);

                        Secundario s = new Secundario(-1);

                        entidades[pos].secundarios.Add(s);

                        for (int i = 1; i < n; ++i)
                        {
                            entidades[pos].secundarios.Last().AddIndice(-1);
                        }

                        entidades[pos].secundarios.Last().listSecD[entidades[pos].secundarios.Last().getIteracion].getClave = entidades[pos].registros.Last().element_Registro[indice2];
                        //MessageBox.Show("El dato del registro del indice dos es");
                        /// Para el archivo de indices, indice secundario
                        fs.listEntidades = entidades;

                        fs.asignarDireccionIndiceSecundario();
                        fs.ponerDireccionIndice(indice2);

                        entidades = fs.listEntidades;

                        n = 0;
                        n = fs.numeroDeIteracion(8);

                        SecundarioCve s1 = new SecundarioCve(-1);

                        entidades[pos].secundarios.Last().listSecD.First().agregarBloquesDirecciones(-1);

                        ///Llenamos nuestros cajones 
                        for (int i = 1; i < n; ++i)
                        {
                            entidades[pos].secundarios.Last().listSecD.First().listSecDirs.First().addIndice(-1);
                        }
                        ///Le damos valor al primer cajon
                        entidades[pos].secundarios.First().listSecD.First().listSecDirs.Last().listIndiceSecundario.First().getDireccion = entidades[pos].registros.Last().dir_Registro;

                        ///Por ultimo actualizamos los cajones para saber donde sera el siguiente lugar a escribir
                        ///Tanto de el cajon principal como para los otros cajones 
                        entidades[pos].secundarios.Last().getIteracion += 1;
                        entidades[pos].secundarios.Last().listSecD.First().listSecDirs.Last().getIteracion += 1;

                        fs.listEntidades = entidades;
                        fs.escribirArchivoInddiceSecundario();
                        entidades = fs.listEntidades;
                    }
                    fr.lisEntidades = entidades;
                    //escribir el archivo 
                    fr.escribirArchivoDat();
                    //ordenar
                    fr.ordenarDatosXcB();
                    //las direcciones de los datos
                    fr.direccionNuevaDatos();
                    //escribir el registro, data
                    escribirDatosData();
                    MessageBox.Show("Se guardo con un solo indice secundario.");
                }
            }
            else if (indiceA1 != -1) //Indice primario B+
            {
                if (creaListaObjetos())
                {
                    registro = fr.creaNuevoRegistro(datos_registro);
                    
                    if (entidades[pos].atributos[indiceA1].direccion_Indice != -1)
                    {
                        if (fr.ComparaArbol(registro, indiceA1)) //Comparar para ingresar uno nuevo en el archivo de datos
                        {
                            MessageBox.Show("Ya existe una clave igual.");
                            return;
                        }

                        registro = fr.creaNuevoRegistro(datos_registro); //creamos el registro guardando los datos
                        registro.iteraREG++;
                        entidades[pos].registros.Add(registro);
                        fr.asignaDatos();
                        //escribir el archivo 
                        fr.escribirArchivoDat();
                        //ordenar
                        fr.ordenarDatosXcB();
                        //las direcciones de los datos
                        fr.direccionNuevaDatos();

                        arbolB.insertar();
                        entidades = arbolB.listEntidad;

                        //escribir el registro, data
                        escribirDatosData();
                        MessageBox.Show("Se guardo correctamente el dato del árbol.");
                    }
                    else //es el primero
                    {
                        registro = fr.creaNuevoRegistro(datos_registro); //creamos el registro guardando los datos
                        registro.iteraREG++;
                        entidades[pos].registros.Add(registro);
                        fr.asignaDatos();
                        //escribir el archivo 
                        fr.escribirArchivoDat();
                        //ordenar
                        fr.ordenarDatosXcB();
                        //las direcciones de los datos
                        fr.direccionNuevaDatos();
                        
                        arbolB = new ArbolB(entidades, pos, indiceA1);
                        entidades[pos].Arboles.Add(arbolB);
                        arbolB.setFicheroPrimario = FicheroArPri;
                        arbolB.setNombreArchivo = nombreArchivo;
                        arbolB.setFichero = Fichero;
                        arbolB.setNombreArchivoPrimario = nombreArchivoArPri;
                        MessageBox.Show("nuevo arbol");
                        arbolB.insertar();
                        entidades = arbolB.listEntidad;

                        escribirDatosData();
                        MessageBox.Show("Se guardo correctamente");
                    }
                }
            }
            else if (indiceCB != -1)
            {
                if (creaListaObjetos()) //se crea la lista con todos los objetos a guardar
                {
                    registro = fr.creaNuevoRegistro(datos_registro); //creamos el registro guardando los datos
                    registro.iteraREG++;
                    entidades[pos].registros.Add(registro);
                    fr.asignaDatos();
                    //escribir el archivo 
                    fr.escribirArchivoDat();
                    //ordenar
                    fr.ordenarDatosXcB();
                    //las direcciones de los datos
                    fr.direccionNuevaDatos();
                    //escribir el registro, data
                    escribirDatosData();
                    MessageBox.Show("Se guardo correctamente solo con clave de busqueda.");
                }
            } //Indice clave de busqueda
            else if (indice0 == true)
            {
                if (creaListaObjetos()) //se crea la lista con todos los objetos a guardar
                {
                    registro = fr.creaNuevoRegistro(datos_registro); //creamos el registro guardando los datos
                    registro.iteraREG++;
                    entidades[pos].registros.Add(registro);
                    fr.asignaDatos();
                    //escribir el archivo 
                    fr.escribirArchivoDat();
                    //ordenar
                    fr.ordenarDatosXcB();
                    //las direcciones de los datos
                    fr.direccionNuevaDatos();
                    //escribir el registro, data
                    escribirDatosData();
                    MessageBox.Show("Se guardo correctamente.");
                }
            }//Sin clave de busqueda

            entidades = fr.lisEntidades;
        }

        /*Metodo para poder modificar los datos de los registros*/
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ///Se Modificar Archivo de datos 
            int elemento = dgv_Registro.CurrentRow.Index;

            if (creaListaObjetos())
            {
                int index = 0;
                Raux = fr.creaNuevoRegistro(datos_registro);
                //int cont = Raux.element_Registro.Count;

                //If para modificar del indice secundario
                if (indice2 != -1)
                {
                    string vs2 = entidades[pos].registros[elemento].element_Registro[indice2].ToString();

                    for (int i = 0; i < entidades[pos].secundarios.Last().getIteracion; ++i)
                    {
                        string aux2 = entidades[pos].secundarios.Last().listSecD[i].getClave.ToString();

                        if (entidades[pos].atributos[indice2].tipo_Dato == 'C')
                        {
                            if (vs2 == aux2)
                            {
                                index = i;
                            }
                        }
                        else
                        {
                            int entero = int.Parse(vs2);
                            int entero2 = int.Parse(aux2);

                            if (entero == entero2)
                            {
                                index = i;
                            }
                        }
                    }

                    ///Ahora buscar la direccion que se inserto ahi y se eliminara para pasarla a otro indice

                    long long1 = entidades[pos].registros[elemento].dir_Registro;
                    long long2 = 0;
                    for (int i = 0; i < entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().getIteracion; ++i)
                    {
                        long2 = entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().listIndiceSecundario[i].getDireccion;

                        if (long1 == long2)
                        {
                            entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().listIndiceSecundario.RemoveAt(i);
                        }
                    }

                    entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().addIndice(-1);
                    entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().getIteracion -= 1;

                    fs.listEntidades = entidades;
                    fs.reescribirDirecciones(index);
                    entidades = fs.listEntidades;

                    if (entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().getIteracion == 0)
                    {
                        entidades[pos].secundarios.Last().listSecD.RemoveAt(index);
                        entidades[pos].secundarios.Last().getIteracion -= 1;
                        entidades[pos].secundarios.Last().AddIndice(-1);
                        fs.listEntidades = entidades;
                        fs.reescribirCajones();
                        entidades = fs.listEntidades;
                    }

                    ///Como ya se elimino ahora buscaremos donde debe insertarse
                    
                    vs2 = Raux.element_Registro[indice2].ToString();
                    index = -1;
                    for (int i = 0; i < entidades[pos].secundarios.Last().getIteracion; ++i)
                    {
                        string aux2 = entidades[pos].secundarios.Last().listSecD[i].getClave.ToString();

                        if (entidades[pos].atributos[indice2].tipo_Dato == 'C')
                        {
                            if (vs2 == aux2)
                            {
                                index = i;
                            }
                        }
                        else
                        {
                            int entero = int.Parse(vs2);
                            int entero2 = int.Parse(aux2);

                            if (entero == entero2)
                            {
                                index = i;
                            }
                        }
                    }

                    if (index != -1)
                    {
                        entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().listIndiceSecundario[entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().getIteracion].getDireccion = entidades[pos].registros[elemento].dir_Registro;

                        entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().getIteracion += 1;
                        fs.listEntidades = entidades;
                        fs.reescribirDirecciones(index);
                        entidades = fs.listEntidades;
                    }
                    else
                    {
                        int n = fs.numeroDeIteracion(8);

                        entidades[pos].secundarios.Last().listSecD[entidades[pos].secundarios.Last().getIteracion].agregarBloquesDirecciones(-1);

                        for (int i = 1; i < n; ++i)
                        {
                            entidades[pos].secundarios.Last().listSecD[entidades[pos].secundarios.Last().getIteracion].listSecDirs.First().addIndice(-1);
                        }

                        entidades[pos].secundarios.Last().listSecD[entidades[pos].secundarios.Last().getIteracion].getClave = Raux.element_Registro[indice2];

                        entidades[pos].secundarios.Last().listSecD[entidades[pos].secundarios.Last().getIteracion].listSecDirs.Last().listIndiceSecundario.First().getDireccion = entidades[pos].registros[elemento].dir_Registro;

                        entidades[pos].secundarios.Last().listSecD[entidades[pos].secundarios.Last().getIteracion].listSecDirs.Last().getIteracion += 1;

                        fs.listEntidades = entidades;
                        fs.escribirIndice2Exist(entidades[pos].secundarios.Last().getIteracion);
                        entidades = fs.listEntidades;
                        entidades[pos].secundarios.Last().getIteracion += 1;

                        fs.listEntidades = entidades;

                        fs.ordenarIndiceSecundario();
                        fs.reescribirCajones();

                        entidades = fs.listEntidades;
                    }
                }

                if (indice1 != -1) //Si existe un indice primairo
                {
                    string vs2 = entidades[pos].registros[elemento].element_Registro[indice1].ToString();
                    //MessageBox.Show("num_ elementos: " + entidades[pos].primarios.Count);
                    for (int i = 0; i < entidades[pos].primarios.Last().primario_Iteracion; ++i)
                    {
                        string vs = entidades[pos].primarios.Last().indice[i].IndiceP_Clave.ToString();
                        if (entidades[pos].atributos[indice1].tipo_Dato == 'C')
                        {
                            if (vs == vs2)
                            {
                                entidades[pos].primarios[auxPrimario].indice[i].IndiceP_Clave = Raux.element_Registro[indice1];
                            }
                        }
                        else
                        {
                            int entero = int.Parse(vs);
                            int entero2 = int.Parse(vs2);

                            if (entero == entero2)
                            {
                                entidades[pos].primarios[auxPrimario].indice[i].IndiceP_Clave = Raux.element_Registro[indice1];
                            }
                        }
                    }
                    fr.ordenarIndice();
                    fr.escribirDatosNuevosArchivoIndice();
                }

                for (int i = 0; i < entidades[pos].atributos.Count; ++i)
                {
                    entidades[pos].registros[elemento].element_Registro[i] = Raux.element_Registro[i];
                }

                fr.lisEntidades = entidades; //pasando la lista de entidades

                //ordenar
                fr.ordenarDatosXcB();
                //las direcciones de los datos
                fr.direccionNuevaDatos();
                entidades = fr.lisEntidades;
                MessageBox.Show("Modificado");
                //escribir el registro, data
                escribirDatosData();
            }
        }

        /*Metodo para eliminar datos*/
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int elemento = dgv_Registro.CurrentRow.Index;

            ///Eliminar indice secundario

            if (indice2 != -1)
            {
                ///Lo primero sera buscar la clave

                string aux = entidades[pos].registros[elemento].element_Registro[indice2].ToString();
                int index = 0;
                for (int i = 0; i < entidades[pos].secundarios.Last().getIteracion; ++i)
                {
                    string aux2 = entidades[pos].secundarios.Last().listSecD[i].getClave.ToString();

                    if (entidades[pos].atributos[indice2].tipo_Dato == 'C')
                    {
                        if (aux == aux2)
                        {
                            index = i;
                        }
                    }
                    else
                    {
                        int entero = int.Parse(aux);
                        int entero2 = int.Parse(aux2);

                        if (entero == entero2)
                        {
                            index = i;
                        }
                    }
                }

                ///Ahora buscar la direccion que se inserto ahi

                long long1 = entidades[pos].registros[elemento].dir_Registro;
                long long2 = 0;
                for (int i = 0; i < entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().getIteracion; ++i)
                {
                    long2 = entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().listIndiceSecundario[i].getDireccion;

                    if (long1 == long2)
                    {
                        entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().listIndiceSecundario.RemoveAt(i);
                    }
                }

                entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().addIndice(-1);
                entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().getIteracion -= 1;

                fs.listEntidades = entidades;
                fs.reescribirDirecciones(index);
                entidades = fs.listEntidades;

                if (entidades[pos].secundarios.Last().listSecD[index].listSecDirs.Last().getIteracion == 0)
                {
                    entidades[pos].secundarios.Last().listSecD.RemoveAt(index);
                    entidades[pos].secundarios.Last().getIteracion -= 1;
                    entidades[pos].secundarios.Last().AddIndice(-1);
                    fs.listEntidades = entidades;
                    fs.reescribirCajones();
                    entidades = fs.listEntidades;
                }
            }

            //Eliminar inidice primario
            if (indice1 != -1)
            {
                string vs2 = entidades[pos].registros[elemento].element_Registro[indice1].ToString();
                for (int i = 0; i < entidades[pos].primarios.Last().primario_Iteracion; ++i)
                {
                    string vs = entidades[pos].primarios[auxPrimario].indice[i].IndiceP_Clave.ToString();
                    if (entidades[pos].atributos[indice1].tipo_Dato == 'C')
                    {
                        if (vs == vs2)
                        {
                            entidades[pos].primarios[auxPrimario].indice.RemoveAt(i);
                        }
                    }
                    else
                    {
                        int entero = int.Parse(vs);
                        int entero2 = int.Parse(vs2);

                        if (entero == entero2)
                        {
                            entidades[pos].primarios[auxPrimario].indice.RemoveAt(i);
                        }
                    }
                }

                entidades[pos].primarios[auxPrimario].AddIndice(-1, -1, entidades[pos].atributos[indice1]);
                entidades[pos].primarios[auxPrimario].primario_Iteracion -= 1;

                //LeerOrdenacion();
                fr.lisEntidades = entidades;
                fr.ordenarIndice();
                //OrdenarInd();
                //LeerOrdenacion();
                fr.escribirDatosNuevosArchivoIndice();
                //EscribirArchivoInd();
                entidades = fr.lisEntidades;
            }

            //Eliminar en ARCHIVO DE DATOS
            entidades[pos].registros.RemoveAt(elemento);
            if (elemento == 0 && entidades[pos].registros.Count == 0)
            {
                entidades[pos].direccion_Dato = -1;
            }

            if (elemento == 0 && entidades[pos].registros.Count > 0)
            {
                entidades[pos].direccion_Dato = entidades[pos].registros.First().dir_Registro;
            }

            if (entidades[pos].registros.Count > 0)
            {
                fr.lisEntidades = entidades;
                //ordenar
                fr.ordenarDatosXcB();
                //las direcciones de los datos
                fr.direccionNuevaDatos();
                entidades = fr.lisEntidades;
                //escribir el registro, data
                escribirDatosData();
            }
        }

        /*metodo para agregar los valores a una lista e objetos*/
        private bool creaListaObjetos()
        {
            int pRe = dgv_Registro.RowCount - 1;//la posicion del ultimo elemento
            datos_registro = new List<object>();

            if ((pRe) >= 0)
            {
                int i = 1;

                if (dgv_Registro.SelectedCells.Count > 0)
                {
                    foreach (Atributo atributo in entidades[pos].atributos)
                    {
                        try {
                            datos_registro.Add(dgv_Registro.SelectedCells[i].Value);
                            //string id = dgv_Registro.SelectedCells[i].Value.ToString().Length.ToString();
                            //MessageBox.Show(id);
                            //MessageBox.Show("total: " + datos_registro[0].ToString().Length);
                            i++;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                    //MessageBox.Show("termino");
                    return true;
                }
                else
                {
                    MessageBox.Show("Selecciona la fila a agregar ");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("No hay elementos");
                return false; //no hay elementos
            }

        }
        
        /*Metodo para agregar los valores en el data grid view, una vez agregado datos*/
        private void escribirDatosData()
        {
            dgv_Registro.Rows.Clear();

            int j = 0;
            //MessageBox.Show("ToTAL: " + entidades[pos].registros.Count);
            //MessageBox.Show("atributos: " + entidades.ElementAt(pos).atributos.Count);
            foreach (Registro regis in entidades[pos].registros)
            {
                int aux = 0;
                dgv_Registro.Rows.Add(regis.dir_Registro); //direccion del dato, no del registro idiota
                
                for (int i = 0; i < entidades.ElementAt(pos).atributos.Count; ++i)
                {
                    dgv_Registro.Rows[j].Cells[i + 1].Value = regis.element_Registro[i].ToString();
                    aux = i + 1;
                }
                dgv_Registro.Rows[j].Cells[aux + 1].Value = regis.dir_sigRegistro;
                j++;
            }
        }

        /*--------------------------------------Aqui empieza arboles----------------------------------------------*/
    }
}
