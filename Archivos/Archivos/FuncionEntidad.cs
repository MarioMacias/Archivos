using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Archivos
{
    public class FuncionEntidad
    {
        private Entidad entidad;
        private Atributo atributo;

        private FileStream Fichero;
        public string nombreArchivo;
        private long cab; //Variable para la cabezera de las entidades;
        public string sCabe;

        BinaryWriter binaryWriter;
        BinaryReader binaryReader;

        private bool leeArchivo = true;
        private bool leeArchAux = true;

        List<Entidad> entidades;

        /*Forma de crear un nuevo arhcivo*/
        public bool crearArchivo()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                nombreArchivo = saveFileDialog.FileName;

                Fichero = new FileStream(nombreArchivo, FileMode.Create);
                Fichero.Close();
                return true;
            }
            else
            {
                MessageBox.Show("El archivo no se pudo crear...");
                return false;
            }
        }

        /*Asignamos la cabezera en -1 por se la primera*/
        public string asignarCabecera(long cab)
        {
            Fichero = File.Open(nombreArchivo, FileMode.Open, FileAccess.Write);
            binaryWriter = new BinaryWriter(Fichero);
            binaryWriter.Write(cab);
            
            Fichero.Close();
            string cabecera = "Cabecera  " + cab.ToString();
            return cabecera;
        }

        /*Metodo para poder asignar la direccion de la entidad*/
        public List<Entidad> asigrarDatos(List<Entidad> entidades)
        {
            this.entidades = entidades;
            Fichero = File.Open(nombreArchivo, FileMode.Open);
             entidades.Last().direccion_Entidad = Fichero.Length; // le asigno el tamaño en bytes a la ultima entidad
            Fichero.Close();

            if (escribirArchivo())//escribimos el archivo para los nuevos datos
            {
                return entidades;
            }
            else
            {
                return null;
            }
        }

        /*Asigna una nueva direccion random a la identidad*/
        public void nuevaDireccionEntidad()
        {
            foreach (Entidad en in entidades)
            {
                if (en.Id_Entidad != null)
                {
                    byte[] auxByte = conseguirID();
                    foreach (Entidad en2 in entidades)
                    {
                        if (en2.Id_Entidad == auxByte)
                        {
                            nuevaDireccionEntidad();
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    en.Id_Entidad = conseguirID();
                }
            }
        }

        /*Metodo para Conseguir un ID aleatorio*/
        public byte[] conseguirID()
        {
            byte[] id = new byte[5];
            new Random().NextBytes(id);
            return id;
        }

        /*Escribimos en el nuevo archivo los valores correspondientes*/
        public bool escribirArchivo()
        {
            Fichero = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Write);
            Fichero.Position = Fichero.Length;
            binaryWriter = new BinaryWriter(Fichero);

            nuevaDireccionEntidad();
            binaryWriter.Write(entidades.Last().Id_Entidad); //Last() el ultimo de la columna
            binaryWriter.Write(entidades.Last().nombre_Entidad);
            binaryWriter.Write(entidades.Last().direccion_Entidad);
            binaryWriter.Write(entidades.Last().direccion_Atributo);
            binaryWriter.Write(entidades.Last().direccion_Dato);
            binaryWriter.Write(entidades.Last().direccion_Siguiente);
            Fichero.Close();
            return ordenarDatos(); //ordenamos los datos dependiendo del nombre
        }

        /*Ordenar los datos de forma alfabetica, donde apunta*/
        public bool ordenarDatos()
        {
            entidades = entidades.OrderBy(var => var.string_Nombre).ToList(); //Ordebanis dependiendo del nombre

            for (int i = 0; i < entidades.Count - 1; i++)
            {
                entidades[i].direccion_Siguiente = entidades[i + 1].direccion_Entidad; // apunta a la siguiente entidad
                nuevosDatosArchivo(entidades[i]); //Sobreescribir en los datos. todos alv
            }

            entidades.Last().direccion_Siguiente = -1; //La ultima entidad no deberia apuntar a nada
            nuevosDatosArchivo(entidades.Last()); //Lo sobreescribimos.
            sCabe = nuevaCabecera(entidades);
            return true;
        }

        /*Se escribe una nueva cabecera*/
        public string nuevaCabecera(List<Entidad> entiCab)
        {
            Fichero = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Write);
            Fichero.Seek(0, SeekOrigin.Begin);
            binaryWriter = new BinaryWriter(Fichero);
            binaryWriter.Write(entiCab.First().direccion_Entidad);
            Fichero.Close();
            string cabe = "Cabecera  " + entiCab.First().direccion_Entidad;
            //MessageBox.Show(cabe);
            return cabe;
        }

        /*Metodo para escribir sobre el archivo original*/
        public void nuevosDatosArchivo(Entidad entidad)
        {
            Fichero = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Write);
            Fichero.Seek(entidad.direccion_Entidad, SeekOrigin.Begin); //Posicionar en la direccion de la entidad para sobresciribir.

            binaryWriter = new BinaryWriter(Fichero);

            binaryWriter.Write(entidad.Id_Entidad);
            binaryWriter.Write(entidad.nombre_Entidad);
            binaryWriter.Write(entidad.direccion_Entidad);
            binaryWriter.Write(entidad.direccion_Atributo);
            binaryWriter.Write(entidad.direccion_Dato);
            binaryWriter.Write(entidad.direccion_Siguiente);
            Fichero.Close();
        }

        /*Metodo para modificar los datos*/
        public bool modificaElemento(string texto, int pos, List<Entidad> entidades)
        {
            this.entidades = entidades;
            char[] c = new char[35];
            int i = 0;
            foreach (char c2 in texto)
            {
                c[i] = c2;
                i++;
            }

            entidades.ElementAt(pos).nombre_Entidad = c;
            entidades.ElementAt(pos).string_Nombre = texto;

            return ordenarDatos();
        }

        /*Metodo para elimimnar*/
        public string eliminarEntidad(int pos, List<Entidad> entidades)
        {
            this.entidades = entidades;
            if (!entidades.Any())
            {
                MessageBox.Show("No hay entidades en este momento");
            }

            if (entidades.Count == 1)
            {
                entidades.ElementAt(pos).direccion_Siguiente = -1; //Se elimina el ultimo la cabecera es -1.
                nuevosDatosArchivo(entidades.ElementAt(pos)); // se manda a escribir el archivo

                entidades.RemoveAt(pos); // se quita el de la posicion

                escribirArchivo(); //lo volvemos a escribir

                Fichero = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Write);
                Fichero.Seek(0, SeekOrigin.Begin);
                binaryWriter = new BinaryWriter(Fichero);
                cab = -1;
                binaryWriter.Write(cab);
                Fichero.Close();

                return "Cabecera " + cab;
            }
            else
            {
                entidades.RemoveAt(pos);
                ordenarDatos();
                return null;
            }
        }

        /*Metodo para abrir un archivo*/
        public string abrirArchivo(List<Entidad> entidades)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.entidades = entidades;
                nombreArchivo = openFileDialog.FileName;
                Fichero = File.Open(nombreArchivo, FileMode.Open, FileAccess.ReadWrite);
                Fichero.Close();
            }
            return LeerCabecera();
        }

        /*Empezamos a leer el archivo*/
        private string LeerCabecera()
        {
            try
            {
                Fichero = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Read);
                binaryReader = new BinaryReader(Fichero);
                binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);
                cab = binaryReader.ReadInt64();
                Fichero.Close();
                string c = "Cabecera  " + cab; //retornalra

                if (cab != -1)
                {
                    asignarMemoria();
                    return c;
                }
                else
                {
                    MessageBox.Show("Diccionario vacio");
                    return "";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("Es aqui el error");
                return "";
            }
        }

        /*Lee los archivos y crea las entidades y atributos con sus respectivos datos*/
        private bool asignarMemoria()
        {
            long dat = 0;
            Fichero = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Read);
            binaryReader = new BinaryReader(Fichero);
            binaryReader.BaseStream.Seek(cab, SeekOrigin.Begin);//cabecera 

            while (leeArchivo)
            {
                byte[] id = new byte[5]; //para el id
                id = binaryReader.ReadBytes(5); //la madre de el id
                char[] c = binaryReader.ReadChars(35); //Obtiene la cadena de la entidad
                entidad = new Entidad(c, id);
                entidades.Add(entidad);
                dat = binaryReader.ReadInt64(); //Obtiene la direccion de la entidad
                entidades.Last().direccion_Entidad = dat;
                dat = binaryReader.ReadInt64();//Obtiene la direccion del 1.er atributo
                entidades.Last().direccion_Atributo = dat;
                long temp = Fichero.Position; //Obtiene la ultima posicion desde donde se leyo  
                
                if (entidades.Last().direccion_Atributo != -1)
                {
                    char c2;
                    long aux;
                    int i;
                    
                    binaryReader.BaseStream.Seek(entidades.Last().direccion_Atributo, SeekOrigin.Begin);
                    while (leeArchAux)
                    {
                        byte[] id2 = new byte[5]; //para el id
                        id2 = binaryReader.ReadBytes(5); //la madre de el id
                        char[] caux = binaryReader.ReadChars(35);
                        c2 = binaryReader.ReadChar();// no puede leeer mas-----------------------------
                        i = binaryReader.ReadInt32();

                        //Se crea el atributo
                        atributo = new Atributo(id2, caux, c2, i);
                        entidades.Last().agregarAtributo(atributo);
                        aux = binaryReader.ReadInt64(); //Obtiene la direccion del atributo
                        entidades.Last().atributos.Last().direccion_Atributo = aux;
                        i = binaryReader.ReadInt32();//Obtiene el tipo de indice
                        entidades.Last().atributos.Last().tipo_Indice = i;
                        aux = binaryReader.ReadInt64(); //Obtiene la direccion del indice
                        entidades.Last().atributos.Last().direccion_Indice = aux;
                        aux = binaryReader.ReadInt64(); //Obtiene la direccion del siguiente atributo
                        entidades.Last().atributos.Last().direccion_sigAtributo = aux;
                        if (aux == -1)
                        {
                            leeArchAux = false;
                        }
                        if (entidades.Last().atributos.Last().direccion_sigAtributo != -1)
                        {
                            binaryReader.BaseStream.Seek(aux, SeekOrigin.Begin);
                        }
                    }
                }
                leeArchAux = true;
                binaryReader.BaseStream.Seek(temp, SeekOrigin.Begin); ///posiciona la lectura desde antes de entrar a los atributos
                dat = binaryReader.ReadInt64();///Obtiene la direccion de los registros 
                entidades.Last().direccion_Dato = dat;
                dat = binaryReader.ReadInt64();///Obtiene la direccion de la siguiente entidad
                entidades.Last().direccion_Siguiente = dat;
                if (dat == -1) //Si el ultimo es un -1 deja de leer las entidades
                {
                    leeArchivo = false;
                }
                if (entidades.Last().direccion_Siguiente != -1)
                {
                    binaryReader.BaseStream.Seek(entidades.Last().direccion_Siguiente, SeekOrigin.Begin); //Se posiciona en apuntar siguiente de la siguiente entidad 
                }
            }
            Fichero.Close();
            return true;
        }

        /*Cerrar el archivo */
        public bool cerrarArchivo()
        {
            Fichero.Close();
            entidades.RemoveRange(0,entidades.Count);
            cab = -1;
            nombreArchivo = "";
            leeArchivo = leeArchAux = true;
            return true;
        }

        /*Get and set*/
        public FileStream fileS
        {
            get { return Fichero; }
        }

        /*Get nombre del archivo*/
        public string nombreDelArchivo
        {
            get { return nombreArchivo; }
        }
    }
}
