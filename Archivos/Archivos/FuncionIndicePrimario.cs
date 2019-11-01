using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Archivos
{
    public class FuncionIndicePrimario
    {
        private List<Entidad> entidades;
        private int pos;
        private int indice1;
        private Primario primario;

        FileStream Fichero;
        BinaryWriter binaryWriter;
        BinaryReader binaryReader;

        private string nombreArchivoIndice;
        private string nombreArchivo;

        /*Metodo para signar los valores necesarios*/
        public void asignaDatosNecesarios(List<Entidad> entidades, int pos, int indice1, FileStream Fichero, string nombreArchivoIndice, string nombreArchivo)
        {
            this.entidades = entidades;
            this.pos = pos;
            this.indice1 = indice1;
            this.Fichero = Fichero;
            this.nombreArchivoIndice = nombreArchivoIndice;
            this.nombreArchivo = nombreArchivo;
        }

        /*Asignamos las direccion de indice, el tamapo del archivo*/
        public void asignarDireccionIndice()
        {
            Fichero = File.Open(nombreArchivoIndice, FileMode.Open);
            entidades[pos].atributos[indice1].direccion_Indice = Fichero.Length;
            Fichero.Close();
        }

        /*Escribir en el archivo de entidades, las direcciones y datos del indice*/
        public void escribirDireccionesIndice(int ind)
        {
            Fichero = File.Open(nombreArchivo, FileMode.Open);
            Fichero.Seek(entidades[pos].atributos[ind].direccion_Atributo, SeekOrigin.Begin); //direccion del atributo
            binaryWriter = new BinaryWriter(Fichero);

            binaryWriter.Write(entidades[pos].atributos[ind].id_Atributo);
            binaryWriter.Write(entidades[pos].atributos[ind].nombre_Atributo);
            binaryWriter.Write(entidades[pos].atributos[ind].tipo_Dato);
            binaryWriter.Write(entidades[pos].atributos[ind].longitud_Tipo);
            binaryWriter.Write(entidades[pos].atributos[ind].direccion_Atributo);
            binaryWriter.Write(entidades[pos].atributos[ind].tipo_Indice);
            binaryWriter.Write(entidades[pos].atributos[ind].direccion_Indice);
            binaryWriter.Write(entidades[pos].atributos[ind].direccion_sigAtributo);
            Fichero.Close();
        }

        /*Escribir los nuevos datos al archivo de indice*/
        public void escribirDatosArchivoIndice()
        {
            Fichero = new FileStream(nombreArchivoIndice, FileMode.Open, FileAccess.Write);
            Fichero.Seek(entidades[pos].atributos[indice1].direccion_Indice, SeekOrigin.Begin);

            binaryWriter = new BinaryWriter(Fichero);

            for (int p = 0; p < entidades[pos].primarios.Count; ++p)
            {
                for (int ip = 0; ip < entidades[pos].primarios[p].indice.Count; ++ip)
                {
                    string vs = entidades[pos].primarios[p].indice[ip].IndiceP_Clave.ToString();

                    if (entidades[pos].atributos[indice1].tipo_Dato == 'C' || entidades[pos].atributos[indice1].tipo_Dato == 'c')
                    {
                        char[] caracter = new char[entidades[pos].atributos[indice1].longitud_Tipo];
                        int j = 0;
                        foreach (char c in vs)
                        {
                            caracter[j] = c;
                            j++;
                        }
                        binaryWriter.Write(caracter);
                    }
                    else
                    {
                        if (entidades[pos].atributos[indice1].tipo_Dato == 'E' || entidades[pos].atributos[indice1].tipo_Dato == 'e')
                        {
                            int entero = int.Parse(vs);
                            binaryWriter.Write(entero);
                        }
                    }
                    //MessageBox.Show("segun yo dat: " + entidades[pos].primarios[p].indice[ip].IndiceP_Direccion);

                    binaryWriter.Write(entidades[pos].primarios[p].indice[ip].IndiceP_Direccion);
                }
                binaryWriter.Write(entidades[pos].primarios[p].apuntador_Siguiente);
            }
            Fichero.Close();
        }

        /*Escribir en memoria los datos del indice*/
        public void asignaMemoriaDatosIndice()
        {
            bool ban = true;
            object o = new object();
            long dat = 0;
            Fichero = new FileStream(nombreArchivoIndice, FileMode.Open, FileAccess.Read);
            binaryReader = new BinaryReader(Fichero);
            binaryReader.BaseStream.Seek(entidades[pos].atributos[indice1].direccion_Indice, SeekOrigin.Begin);

            while (ban)
            {
                switch (entidades[pos].atributos[indice1].tipo_Dato)
                {
                    case 'E':
                        o = binaryReader.ReadInt32();
                        break;
                    case 'C':
                        char[] c = binaryReader.ReadChars(entidades[pos].atributos[indice1].longitud_Tipo);
                        o = new string(c);
                        break;
                }
                dat = binaryReader.ReadInt64();
                primario = new Primario(o, dat, entidades[pos].atributos[indice1]);
                entidades[pos].primarios.Add(primario);
                entidades[pos].primarios.Last().primario_Iteracion += 1;
                for (int i = 1; i < numeroDeIteracion(entidades[pos].atributos[indice1].longitud_Tipo); ++i)
                {
                    switch (entidades[pos].atributos[indice1].tipo_Dato)
                    {
                        case 'E':
                            o = binaryReader.ReadInt32();
                            break;
                        case 'C':
                            char[] c = binaryReader.ReadChars(entidades[pos].atributos[indice1].longitud_Tipo);
                            o = new string(c);
                            break;
                    }

                    dat = binaryReader.ReadInt64();

                    if (dat != -1)
                    {
                        entidades[pos].primarios.Last().primario_Iteracion += 1;
                    }
                    entidades[pos].primarios.Last().AddIndice(o, dat, entidades[pos].atributos[indice1]);
                }

                dat = binaryReader.ReadInt64();
                entidades[pos].primarios.Last().apuntador_Siguiente = dat;
               // MessageBox.Show("DAT asigna memoria indice primario: " + dat);
                if (dat == -1)
                {
                    ban = false;
                }
                else {
                    binaryReader.BaseStream.Seek(dat, SeekOrigin.Begin); }
            }
            Fichero.Close();
        }

        /*Metodo para obtener el numero de iteracion que existe en el momento*/
        public int numeroDeIteracion(int ite)
        {
            int numIte = 0;
            //MessageBox.Show("ite: " + ite);
            if (ite != 8)
            {
                numIte = 2040 / (8 + ite);

                // MessageBox.Show("num de ite: " + numIte); // Se muestra cuantos cajones puede haber

                //int tamA = 8 + (numIte * (ite + 8));

                // MessageBox.Show("Tam total: " + tamA);// Se muestra el tamaño total del archivo
            }
            /*
            if (ite == 8)
            {
                //MessageBox.Show("entro?");
                numIte = 2040 / ite;
            }
            */
            return numIte;
        }

        /*get de entidades*/
        public List<Entidad> dameEntidades
        {
            get { return entidades; }
        }
    }
}
