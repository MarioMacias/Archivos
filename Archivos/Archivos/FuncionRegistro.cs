using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Archivos
{
    public class FuncionRegistro
    {
        private List<Entidad> entidades;
        private int pos = 0;
        private int indice1 = -1; //Primario
        private int indiceCB = -1; //Clave de busqueda
        private int indice0 = -1; //Sin clave
        private int indice2 = -1; //Secundario
        private int indiceA1 = -1; //arbol primario
        private int indiceA2 = -1; //Arbol secundario

        private Registro registro;
        List<object> datos_registro;
        private string nombreArchivoDAT, nombreArchivoIDX;
        private string nombreArchivo;

        FileStream Fichero;
        BinaryWriter binaryWriter;
        BinaryReader binaryReader;

        /*Nuevo registro a crear*/
        public Registro creaNuevoRegistro(List<object> datos_registro)
        {
            this.datos_registro = datos_registro;

            registro = new Registro(entidades.ElementAt(pos).atributos); //se pasa como parametro los atributos
            registro.element_Registro = datos_registro; //se manda la lista de los datos del registro

            return registro;
        }

        /*Se busca en los atributos el que tenga el indice primario arbol*/
        public int buscaIndiceAprimario()
        {
            int i = 0;

            foreach (Atributo a in entidades.ElementAt(pos).atributos)
            {
                if (a.tipo_Indice == 4)
                {
                    indiceA1 = i;
                }
                else { i++; }
            }
            return indiceA1;
        }

        /*Se busca en los atributos el que tenga el indice primario arbol*/
        public int buscaIndiceAsecundario()
        {
            int i = 0;

            foreach (Atributo a in entidades.ElementAt(pos).atributos)
            {
                if (a.tipo_Indice == 5)
                {
                    indiceA2 = i;
                }
                else { i++; }
            }
            return indiceA2;
        }

        /*Se busca en los atributos el que tenga el indice primario*/
        public int buscaIndiceSecundario()
        {
            int i = 0;

            foreach (Atributo a in entidades.ElementAt(pos).atributos)
            {
                if (a.tipo_Indice == 3)
                {
                    indice2 = i;
                }
                else { i++; }
            }
            return indice2;
        }

        /*Se busca en los atributos el que tenga el indice primario*/
        public int buscaIndicePrimario()
        {
            int i = 0;

            foreach (Atributo a in entidades.ElementAt(pos).atributos)
            {
                if (a.tipo_Indice == 2)
                {
                    indice1 = i;
                }
                else { i++; }
            }
            return indice1;
        }

        /*Se busca en los atributos el que tenga el indice de clave de busqueda*/
        public int buscaClaveBusqueda()
        {
            int i = 0;

            foreach (Atributo a in entidades.ElementAt(pos).atributos)
            {
                if (a.tipo_Indice == 1)
                {
                    indiceCB = i;
                }
                else { i++; }
            }
            return indiceCB;
        }
        
        /*Se busca si todos los indices son 0*/
        public bool verificaSinClave()
        {
            foreach (Atributo a in entidades.ElementAt(pos).atributos)
            {
                if (a.tipo_Indice != 0)
                {
                    indice0 = a.tipo_Indice;
                    return false;
                }
            }
            indice0 = 0;
            return true;
        }

        /*Metodo para crear un nuevo archivo*/
        private void creaArchivo()
        {
        }

        /*Asignar una nueva posicion*/
        public int nuevaPosicion
        {
            get { return pos; }
            set { pos = value; }
        }

        /*Asignar una nueva posicion*/
        public List<Entidad> lisEntidades
        {
            get { return entidades; }
            set { entidades = value; }
        }

        /*Buscar si la clave de busqueda ya existe*/
        public bool Compara(Registro regis)
        {
            bool res = false;

            string vs2 = regis.element_Registro[indice1].ToString(); //tipo object

            for (int i = 0; i < entidades[pos].primarios.Last().primario_Iteracion; ++i) //recorrer todas las iteraciones
            {
                string vs = entidades[pos].primarios.Last().indice[i].IndiceP_Clave.ToString(); //tipo object

                if (entidades[pos].atributos[indice1].tipo_Dato == 'C') //Si es de tipo char
                {
                    if (vs == vs2)
                    {
                        res = true;
                    }
                }
                else
                {
                    if (entidades[pos].atributos[indice1].tipo_Dato == 'E') //Tipo de entero
                    {
                        int entero = int.Parse(vs);
                        int entero2 = int.Parse(vs2);

                        if (entero == entero2)
                        {
                            res = true;
                        }
                    }
                }
            }
            return res;
        }

        /*Asignar los valores en el archivo.*/
        public void asignaDatos()
        {
            Fichero = File.Open(nombreArchivoDAT, FileMode.Open);
            entidades.ElementAt(pos).registros.Last().dir_Registro = Fichero.Length;
            Fichero.Close();
        }

        /*Ponemos el FilseStream y el nombre del archivo en las variables globales*/
        public void setNameFichero(FileStream Fichero, string nombreArchivoDAT, string nombreArchivoIDX, string nombreArchivo)
        {
            this.Fichero = Fichero;
            this.nombreArchivoDAT = nombreArchivoDAT;
            this.nombreArchivoIDX = nombreArchivoIDX;
            this.nombreArchivo = nombreArchivo;
        }

        /*Metodo para poder ordenar los datos*/
        public void ordenarIndice() //donde ordena
        {
            int cajones = entidades[pos].primarios.Last().indice.Count;
            entidades[pos].primarios.Last().indice.RemoveRange(entidades[pos].primarios.Last().primario_Iteracion, entidades[pos].primarios.Last().indice.Count - entidades[pos].primarios.Last().primario_Iteracion);
            //MessageBox.Show("mierda esa: " + entidades[pos].primarios.Last().indice.Last().IndiceP_Clave.ToString());
            /* foreach (IndicePrimario ip in entidades[pos].primarios.Last().indice)
             {
                 MessageBox.Show("mierda esa: " + ip.IndiceP_Clave);
             }*/
            //entidades[pos].primarios.Last().indice = entidades[pos].primarios.Last().indice.OrderBy(x => x.IndiceP_Clave).ToList();
            if (entidades[pos].atributos[indice1].tipo_Dato == 'E')
            {
                entidades[pos].primarios.Last().indice = entidades[pos].primarios.Last().indice.OrderBy(x => Convert.ToInt32(x.IndiceP_Clave)).ToList();
            }
            else
            {
                entidades[pos].primarios.Last().indice = entidades[pos].primarios.Last().indice.OrderBy(x => x.IndiceP_Clave.ToString()).ToList();
            }
            int cajones2 = entidades[pos].primarios.Last().indice.Count;

            for (int i = cajones2; i < cajones; ++i)
            {
                entidades[pos].primarios.Last().AddIndice(-1, -1, entidades[pos].atributos[indice1]);
            }
        }

        /*Aqui ponemos los nuevos datos del archivo, el archivo de indices, verificando el tipo de dato*/
        public void escribirDatosNuevosArchivoIndice()
        {
            Fichero = new FileStream(nombreArchivoIDX, FileMode.Open, FileAccess.Write); //path da null
            Fichero.Seek(entidades[pos].atributos[indice1].direccion_Indice, SeekOrigin.Begin);

            binaryWriter = new BinaryWriter(Fichero);

            for (int p = 0; p < entidades[pos].primarios.Count; ++p)
            {
                for (int ip = 0; ip < entidades[pos].primarios[p].indice.Count; ++ip)
                {
                    string vs = entidades[pos].primarios[p].indice[ip].IndiceP_Clave.ToString();

                    if (entidades[pos].atributos[indice1].tipo_Dato == 'c' || entidades[pos].atributos[indice1].tipo_Dato == 'C')
                    {
                        char[] caracter = new char[entidades[pos].atributos[indice1].longitud_Tipo]; //nuevo char con la longitud del tipo de dato
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
                        if (entidades[pos].atributos[indice1].tipo_Dato == 'e' || entidades[pos].atributos[indice1].tipo_Dato == 'E')
                        {
                            int entero = int.Parse(vs);
                            binaryWriter.Write(entero);
                        }
                    }
                    binaryWriter.Write(entidades[pos].primarios[p].indice[ip].IndiceP_Direccion);
                }
                binaryWriter.Write(entidades[pos].primarios[p].apuntador_Siguiente);
            }
            Fichero.Close();
        }

        /*Escribir los datos en el archivo DAT*/
        public void escribirArchivoDat()
        {
            Fichero = new FileStream(nombreArchivoDAT, FileMode.Open, FileAccess.Write);
            Fichero.Position = Fichero.Length; // Hasta el final del archivo
            //MessageBox.Show("primer final: " + Fichero.Length);
            binaryWriter = new BinaryWriter(Fichero);
            binaryWriter.Write(entidades[pos].registros.Last().dir_Registro);
            //MessageBox.Show("Tamaño: 8");

            for (int i = 0; i < entidades[pos].atributos.Count; ++i)
            {
                string vs = entidades[pos].registros.Last().element_Registro[i].ToString();

                if (entidades[pos].atributos[i].tipo_Dato == 'C') //Si es de tipo char
                {
                    char[] caracter = new char[entidades[pos].atributos[i].longitud_Tipo];
                    int j = 0;

                    foreach (char c in vs)
                    {
                        caracter[j] = c;
                        j++;
                    }
                    //MessageBox.Show("Tamaño: " + caracter.Length);
                    binaryWriter.Write(caracter);
                }
                else//Si es de tipo entero
                {
                    if (entidades[pos].atributos[i].tipo_Dato == 'E') //Si es de tipo entero
                    {
                        int entero = int.Parse(vs);
                        //MessageBox.Show("Tamaño: 4");
                        binaryWriter.Write(entero);
                    }
                }
            }

            binaryWriter.Write(entidades.ElementAt(pos).registros.Last().dir_sigRegistro);
            //MessageBox.Show("Tamaño: 8");
            //MessageBox.Show("segundo final: " + Fichero.Length);
            Fichero.Close();
        }

        /*Ordenamos los datos conforme al clave de busqueda*/
        public void ordenarDatosXcB()
        {
            if (indiceCB != -1)
            {
                try
                {
                    entidades.ElementAt(pos).registros = entidades.ElementAt(pos).registros.OrderBy(p => p.element_Registro[indiceCB]).ToList(); //Ordeno los elementos del registro
                }
                catch (Exception e)
                {
                    //MessageBox.Show(e.Message);
                }
                
                for (int i = 0; i < entidades.ElementAt(pos).registros.Count - 1; i++)
                {
                    entidades.ElementAt(pos).registros[i].dir_sigRegistro = entidades.ElementAt(pos).registros[i + 1].dir_Registro;
                    ordenarArchivoRegistro(entidades.ElementAt(pos).registros[i]);
                }
                entidades.ElementAt(pos).registros.Last().dir_sigRegistro = -1;
                ordenarArchivoRegistro(entidades.ElementAt(pos).registros.Last());
            }
            if (indiceCB == -1)
            {
                for (int i = 0; i < entidades.ElementAt(pos).registros.Count - 1; i++)
                {
                    entidades.ElementAt(pos).registros[i].dir_sigRegistro = entidades.ElementAt(pos).registros[i + 1].dir_Registro;
                    ordenarArchivoRegistro(entidades.ElementAt(pos).registros[i]);
                }
                entidades.ElementAt(pos).registros.Last().dir_sigRegistro = -1;
                ordenarArchivoRegistro(entidades.ElementAt(pos).registros.Last());
            }
        }

        /*Escribir en el archivo del .DAT los nuevos valores dependiendo del tipo de dato*/
        private void ordenarArchivoRegistro(Registro re)
        {
            Fichero = new FileStream(nombreArchivoDAT, FileMode.Open, FileAccess.Write);
            Fichero.Seek(re.dir_Registro, SeekOrigin.Begin); //Posicionar en una direccion del archivo
            binaryWriter = new BinaryWriter(Fichero);

            binaryWriter.Write(re.dir_Registro);

            for (int i = 0; i < entidades[pos].atributos.Count; ++i)
            {
                string vs = re.element_Registro[i].ToString();

                if (entidades[pos].atributos[i].tipo_Dato == 'C' || entidades[pos].atributos[i].tipo_Dato == 'c')
                {
                    char[] caracter = new char[entidades.ElementAt(pos).atributos[i].longitud_Tipo];
                    int j = 0;
                    foreach (char c in vs)
                    {
                        caracter[j] = c;
                        j++;
                    }
                    binaryWriter.Write(caracter);
                }
                else if (entidades[pos].atributos[i].tipo_Dato == 'E' || entidades[pos].atributos[i].tipo_Dato == 'e')
                {
                    int entero = int.Parse(vs);
                    binaryWriter.Write(entero);
                }
            }

            binaryWriter.Write(re.dir_sigRegistro);
            Fichero.Close();
        }

        /*Escribir en el archivo, las nuevas direcciones de datos*/
        public void direccionNuevaDatos()
        {
            entidades[pos].direccion_Dato = entidades[pos].registros.First().dir_Registro;
            Fichero = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Write);
            Fichero.Seek(entidades[pos].direccion_Entidad, SeekOrigin.Begin);
            binaryWriter = new BinaryWriter(Fichero);

            binaryWriter.Write(entidades.ElementAt(pos).Id_Entidad);
            binaryWriter.Write(entidades.ElementAt(pos).nombre_Entidad);
            binaryWriter.Write(entidades.ElementAt(pos).direccion_Entidad);
            binaryWriter.Write(entidades.ElementAt(pos).direccion_Atributo);
            binaryWriter.Write(entidades.ElementAt(pos).direccion_Dato);
            binaryWriter.Write(entidades.ElementAt(pos).direccion_Siguiente);
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

        /*Asignar datos en la memoria*/
        public void asignaDatosDat()
        {
            try
            {
                bool ban = true;
                bool banA;
                long dat = 0;
                int nA = entidades.ElementAt(pos).atributos.Count() - 1;
                Fichero = new FileStream(nombreArchivoDAT, FileMode.Open, FileAccess.Read);
                binaryReader = new BinaryReader(Fichero);
                binaryReader.BaseStream.Seek(entidades.ElementAt(pos).direccion_Dato, SeekOrigin.Begin);
                while (ban)
                {
                    dat = binaryReader.ReadInt64();
                    registro = new Registro(dat);
                    entidades.ElementAt(pos).registros.Add(registro);
                    banA = true;
                    int i = 0;
                    while (banA)
                    {
                        switch (entidades.ElementAt(pos).atributos[i].tipo_Dato)
                        {
                            case 'E':
                                int en = binaryReader.ReadInt32();
                                entidades.ElementAt(pos).registros.Last().element_Registro.Add(en);
                                break;
                            case 'C':
                                char[] c = binaryReader.ReadChars(entidades.ElementAt(pos).atributos[i].longitud_Tipo);
                                string cadena = new string(c);
                                entidades.ElementAt(pos).registros.Last().element_Registro.Add(cadena);
                                break;
                        }
                        if (i < nA)
                        {
                            i++;
                        }
                        else { banA = false; }
                    }
                    dat = binaryReader.ReadInt64();
                    entidades.ElementAt(pos).registros.Last().dir_sigRegistro = dat;

                    if (dat == -1)
                    {
                        ban = false;
                    }
                    else { binaryReader.BaseStream.Seek(dat, SeekOrigin.Begin); }
                }
                Fichero.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        
    }
}
