using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Archivos
{
    class FuncionAtributo
    {
        private List<Atributo> atributos;

        /*Variables para los archivos*/
        List<Entidad> entidades;
        BinaryWriter binaryWriter;
        FileStream Fichero;

        int pos;

        string nombreArchivo;

        /*Asigna una nueva direccion random a la identidad*/
        public void nuevaDireccionAtributo(List<Atributo> atributos)
        {
            this.atributos = atributos;
            foreach (Atributo at in atributos)
            {
                if (at.id_Atributo != null)
                {
                    byte[] auxByte = conseguirID();
                    foreach (Atributo at2 in atributos)
                    {
                        if (at2.id_Atributo == auxByte)
                        {
                            nuevaDireccionAtributo(atributos);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    at.id_Atributo = conseguirID();
                }
            }
        }

        /*Metodo para añadir un nuevo atributo al archivo*/
        public bool agregaAtributoArchivo(string nombreArchivo, int pos, List<Entidad> entidades)
        {
            this.pos = pos;
            this.nombreArchivo = nombreArchivo;
            this.entidades = entidades;
            Fichero = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Write);
            entidades.ElementAt(pos).atributos.Last().direccion_Atributo = Fichero.Length;
            Fichero.Position = Fichero.Length;
            binaryWriter = new BinaryWriter(Fichero);

            nuevaDireccionAtributo(entidades.ElementAt(pos).atributos);//asignar una direcccion al atributo

            binaryWriter.Write(entidades.ElementAt(pos).atributos.Last().id_Atributo);
            binaryWriter.Write(entidades.ElementAt(pos).atributos.Last().nombre_Atributo);
            binaryWriter.Write(entidades.ElementAt(pos).atributos.Last().tipo_Dato);
            binaryWriter.Write(entidades.ElementAt(pos).atributos.Last().longitud_Tipo);
            binaryWriter.Write(entidades.ElementAt(pos).atributos.Last().direccion_Atributo);
            binaryWriter.Write(entidades.ElementAt(pos).atributos.Last().tipo_Indice);
            binaryWriter.Write(entidades.ElementAt(pos).atributos.Last().direccion_Indice);
            binaryWriter.Write(entidades.ElementAt(pos).atributos.Last().direccion_sigAtributo);
            Fichero.Close();

            return apuntaSiguiente();
        }

        /*Apuntador que se asignara al siguiente atributo*/
        public bool apuntaSiguiente()
        {
            if (entidades.ElementAt(pos).atributos.Count > 1)
            {
                for (int i = 0; i < entidades.ElementAt(pos).atributos.Count - 1; ++i)
                {
                    entidades.ElementAt(pos).atributos[i].direccion_sigAtributo = entidades.ElementAt(pos).atributos[i + 1].direccion_Atributo;
                    ordenarAtributos(entidades.ElementAt(pos).atributos.ElementAt(i));
                }
                entidades.ElementAt(pos).atributos.Last().direccion_sigAtributo = -1;
                ordenarAtributos(entidades.ElementAt(pos).atributos.Last());
            }

            return primerAtributo(); //obtiene el primer atributo de la lista, y se le modifica el apuntador de los atributos de la entidad
        }

        /*obtiene el primer atributo de la lista, y se le modifica el apuntador de los atributos de la entidad*/
        private bool primerAtributo()
        {
            entidades.ElementAt(pos).direccion_Atributo = entidades.ElementAt(pos).atributos.First().direccion_Atributo;

            Fichero = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Write);

            Fichero.Seek(entidades.ElementAt(pos).direccion_Entidad, SeekOrigin.Begin);
            binaryWriter = new BinaryWriter(Fichero);

            binaryWriter.Write(entidades.ElementAt(pos).Id_Entidad);
            binaryWriter.Write(entidades.ElementAt(pos).nombre_Entidad);
            binaryWriter.Write(entidades.ElementAt(pos).direccion_Entidad);
            binaryWriter.Write(entidades.ElementAt(pos).direccion_Atributo);
            binaryWriter.Write(entidades.ElementAt(pos).direccion_Dato);
            binaryWriter.Write(entidades.ElementAt(pos).direccion_Siguiente);

            Fichero.Close();
            return true;
        }

        /*Metodo para escribir los datos del archivo*/
        private void ordenarAtributos(Atributo atrib)
        {
            Fichero = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Write);
            Fichero.Seek(atrib.direccion_Atributo, SeekOrigin.Begin);

            binaryWriter = new BinaryWriter(Fichero);

            binaryWriter.Write(atrib.id_Atributo);
            binaryWriter.Write(atrib.nombre_Atributo);
            binaryWriter.Write(atrib.tipo_Dato);
            binaryWriter.Write(atrib.longitud_Tipo);
            binaryWriter.Write(atrib.direccion_Atributo);
            binaryWriter.Write(atrib.tipo_Indice);
            binaryWriter.Write(atrib.direccion_Indice);
            binaryWriter.Write(atrib.direccion_sigAtributo);
            Fichero.Close();
        }

        /*Metodo para Conseguir un ID aleatorio*/
        private byte[] conseguirID()
        {
            byte[] id = new byte[5];
            new Random().NextBytes(id);
            return id;
        }
        
        /*Metodo para eliminar un atributo*/
        public bool eliminarAtributo(int pos, List<Entidad> entidades, string nombreArchivo)
        {
            this.nombreArchivo = nombreArchivo;
            this.pos = pos;
            this.entidades = entidades;
            //MessageBox.Show("dentro " + entidades.ElementAt(pos).atributos.Count.ToString() + "pos " + pos);
            if (entidades.ElementAt(pos).atributos == null)
            {
                MessageBox.Show("No hay mas atributos");
            }

            if (entidades.ElementAt(pos).atributos.Count == 0 || entidades.ElementAt(pos).atributos.Count == 1)
            {
                entidades.ElementAt(pos).atributos.Last().direccion_sigAtributo = -1;
                ordenarAtributos(entidades.ElementAt(pos).atributos.Last());

                sinAtributos();
                entidades.ElementAt(pos).atributos.RemoveAt(pos);
                return true;//escribit de nuevo la tabla
            }

            if (entidades.ElementAt(pos).atributos.Count > 1)
            {
                entidades.ElementAt(pos).atributos.RemoveAt(pos);

                for (int i = 0; i < entidades.ElementAt(pos).atributos.Count; i++)
                {
                    entidades.ElementAt(pos).atributos.ElementAt(i).direccion_sigAtributo = -1;
                }

                for (int i = 0; i < entidades.ElementAt(pos).atributos.Count - 1; i++)
                {
                    entidades.ElementAt(pos).atributos[i].direccion_sigAtributo = entidades.ElementAt(pos).atributos[i + 1].direccion_Atributo;
                    ordenarAtributos(entidades.ElementAt(pos).atributos[i]);
                }
                entidades.ElementAt(pos).atributos.Last().direccion_sigAtributo = -1;
                ordenarAtributos(entidades.ElementAt(pos).atributos.Last());
                primerAtributo();
                return true;
            }
            else
            {
                return false;
            }
        }

        /*Si no hay mas atributos*/
        private void sinAtributos()
        {
            entidades.ElementAt(pos).direccion_Atributo = -1;

            Fichero = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Write);
            Fichero.Seek(entidades.ElementAt(pos).direccion_Entidad, SeekOrigin.Begin);
            binaryWriter = new BinaryWriter(Fichero);

            binaryWriter.Write(entidades.ElementAt(pos).Id_Entidad);
            binaryWriter.Write(entidades.ElementAt(pos).nombre_Entidad);
            binaryWriter.Write(entidades.ElementAt(pos).direccion_Entidad);
            binaryWriter.Write(entidades.ElementAt(pos).direccion_Atributo);
            binaryWriter.Write(entidades.ElementAt(pos).direccion_Dato);
            binaryWriter.Write(entidades.ElementAt(pos).direccion_Siguiente);

            Fichero.Close();
        }

        /*Modificar los atributos seleccionados*/
        public bool modificaAtributoSel(string nom, string indice, string tipo, string longi, int pos, List<Entidad> entidades, int op)
        {
            char[] c = new char[35];
            int i = 0;
            foreach (char c2 in nom)
            {
                c[i] = c2;
                i++;
            }

            switch (op)
            {
                case 0: //para modificar todos
                    if (nom != "" && indice != "" && tipo != "" && longi != "")
                    {
                        entidades.ElementAt(pos).atributos.ElementAt(pos).nombre_Atributo = c;
                        entidades.ElementAt(pos).atributos.ElementAt(pos).string_Nombre = nom;
                        entidades.ElementAt(pos).atributos.ElementAt(pos).tipo_Indice = Convert.ToInt16(indice);
                        entidades.ElementAt(pos).atributos.ElementAt(pos).tipo_Dato = Convert.ToChar(tipo);
                        entidades.ElementAt(pos).atributos.ElementAt(pos).longitud_Tipo = Convert.ToInt16(longi);
                    }
                    else
                    {
                        MessageBox.Show("Verifica si estan todos los datos");
                    }
                    break;
                case 1:// solo el nombre
                    if (nom != "")
                    {
                        entidades.ElementAt(pos).atributos.ElementAt(pos).nombre_Atributo = c;
                        entidades.ElementAt(pos).atributos.ElementAt(pos).string_Nombre = nom;
                    }
                    else
                    {
                        MessageBox.Show("Verifica si esta escrito el nombre en el campo adecuado");
                    }
                    
                    break;
                case 2://tipo de dato
                    if (tipo != "")
                    {
                        entidades.ElementAt(pos).atributos.ElementAt(pos).tipo_Dato = Convert.ToChar(tipo);
                    }
                    else
                    {
                        MessageBox.Show("Verifica si hay un tipo");
                    }
                    break;
                case 3://la longitud
                    if (longi != "")
                    {
                        entidades.ElementAt(pos).atributos.ElementAt(pos).longitud_Tipo = Convert.ToInt16(longi);
                    }
                    else
                    {
                        MessageBox.Show("Veridica si existe una longitud");
                    }
                    
                    break;
                case 4: //tipo de indice
                    if (tipo != "")
                    {
                        entidades.ElementAt(pos).atributos.ElementAt(pos).tipo_Indice = Convert.ToInt16(indice);
                    }
                    else
                    {
                        MessageBox.Show("Verifica si existe un tipo de indice");
                    }
                    break;
            }

            

            apuntaSiguiente();
            return true;
        }
    }
}
