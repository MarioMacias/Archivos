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
        private FileStream Fichero;
        public string nombreArchivo;
        private long cab; //Variable para la cabezera de las entidades;

        BinaryWriter binaryWriter;
        //BinaryReader binaryReader;

        List<Entidad> entidades;
        private static char[] abecedario = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', };

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
             entidades.Last().direccion_Entidad = Fichero.Length;
            //entidades.Last().Id_Entidad = Fichero.Length;
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
            char letra = letraRandom();
            int numero = numeroEnteroRandom();
            byte[] id = new byte[5];
            //byte[] conc = { Convert.ToByte(letra), Convert.ToByte(numero)};
              byte[] conc = { Convert.ToByte(letra), Convert.ToByte(numeroEnteroRandom()),
              Convert.ToByte(numeroEnteroRandom()),
              Convert.ToByte(numeroEnteroRandom()),
              Convert.ToByte(numeroEnteroRandom())};
            id = conc;
            //MessageBox.Show(id.);
            return id;
        }

        /*Letra random*/
        public char letraRandom()
        {
            Random r = new Random();
            int aleatorio = r.Next(0,15);
            return abecedario[aleatorio];
        }

        /*Numero random*/
        public int numeroEnteroRandom()
        {
            Random r = new Random();
            int aleatorio = r.Next(0, 9);
            return aleatorio;
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
            return cabe;
        }

        /*Metodo para escribir sobre el archivo original*/
        public void nuevosDatosArchivo(Entidad entidad)
        {
            Fichero = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Write);
            //long ide = BitConverter.ToInt16(entidad.Id_Entidad,0);
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
    }
}
