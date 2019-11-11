using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Archivos
{
    public class ArbolB
    {
        private List<Entidad> entidades;
        private List<Nodo> Arbol = new List<Nodo>();
        private ClaveBusqueda claveB;
        private Nodo nodo;
        private Nodo nodoAux;
        private Nodo raiz;
        private int grado = 4;
        private int mitad = 3;

        private int pos;
        private int indiceA1;
        private int posNodo;

        private string nombreArchivoIDX;
        private string nombreArchivo;
        private FileStream FicheroArPri, FicheroArSec, Fichero;
        BinaryWriter binaryWriter;
        BinaryReader binaryReader;
        bool claux = false;


        public ArbolB(List<Entidad> entidades, int pos, int indiceA1)
        {
            this.entidades = entidades;
            this.pos = pos;
            this.indiceA1 = indiceA1;
        }

        public void insertar()
        {
            if (Arbol.Count == 0) //No existe raiz, se crea la primera hoja
            {
                nuevoNodo('H');
                if (nuevaClabeBusqueda() == false) //no se repite
                {
                    for (int i = 0; i < grado; i++)
                    {
                        if (Arbol.First().clavesBusqueda[i].Direccion == -1)
                        {
                            Arbol.First().clavesBusqueda[i].Clave = claveB.Clave;
                            Arbol.First().clavesBusqueda[i].Direccion = claveB.Direccion;
                            break;
                        }
                    }
                    entidades[pos].atributos[indiceA1].direccion_Indice = Arbol.First().clavesBusqueda.First().Direccion;
                    asignaMemoriaAtributo();
                }
            }
            else if (Arbol.Count == 1) //Si aun no se crea la raiz, pero ya existe una hoja
            {
                if (totalClaves() <= grado)
                {
                    if (nuevaClabeBusqueda() == false)
                    {
                        for (int i = 0; i < grado; i++)
                        {
                            if (Arbol.First().clavesBusqueda[i].Direccion == -1)
                            {
                                Arbol.First().clavesBusqueda[i].Clave = claveB.Clave;
                                Arbol.First().clavesBusqueda[i].Direccion = claveB.Direccion;
                                break;
                            }
                        }
                        if (entidades[pos].atributos[indiceA1].tipo_Dato == 'E')
                        {
                            Arbol.First().clavesBusqueda = Arbol.First().clavesBusqueda.OrderBy(x => Convert.ToInt32(x.Clave) == -1).ToList();
                        }
                        else
                        {
                            Arbol.First().clavesBusqueda = Arbol.First().clavesBusqueda.OrderBy(x => x.Clave.ToString()).ToList();
                        }
                        
                        escribeArchivoIDX();
                    }
                }
                else //se debe separar y crear una raiz
                {
                    if (nuevaClabeBusqueda() == false)
                    {
                        separaNodo();
                    }
                }
            }
            else if(Arbol.Count > 1) //Si ya existe una raiz
            {
                if (nuevaClabeBusqueda() == false)
                {
                    if (buscarNodo()) //Si se encontro el nodo
                    {
                        ///Cuando se encuentre tengo que verificar si existe espacio en el nodo
                        ///Si hay se inserta y listo
                        ///si no existe, se separa del nodo
                        ///verificando indices que no he creado.
                    }
                    else
                    {
                        MessageBox.Show("No se encontro el nodo", "ERROR");
                    }
                }
            }
        }

        private bool buscarNodo()
        {
            bool res = false;
            long direccion = -1;

            for (int i = 0; i < raiz.clavesBusqueda.Count; i++)
            {
                if (entidades[pos].atributos[indiceA1].tipo_Dato == 'E')
                {
                    if (Convert.ToInt32(raiz.clavesBusqueda[i].Clave) != -1)
                    {
                        if (Convert.ToInt32(raiz.clavesBusqueda[i].Clave) >= Convert.ToInt32(claveB.Clave))
                        {
                            direccion = raiz.clavesBusqueda[i].DireccionDerecha;
                        }
                        else
                        {
                            direccion = raiz.clavesBusqueda[i].DireccionIzquierda;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            foreach (Nodo nod in Arbol)
            {
                if (nodo.TipoDeNodo == 'H')
                {
                    if (nodo.Direccion == direccion)
                    {
                        nodoAux = nod;
                        res = true;
                        break;
                    }
                }
            }

            return res;
        }

        private void asignaMemoriaAtributo()
        {
            Fichero = File.Open(nombreArchivo, FileMode.Open);
            Fichero.Seek(entidades[pos].atributos[indiceA1].direccion_Atributo, SeekOrigin.Begin); //direccion del atributo
            binaryWriter = new BinaryWriter(Fichero);

            binaryWriter.Write(entidades[pos].atributos[indiceA1].id_Atributo);
            binaryWriter.Write(entidades[pos].atributos[indiceA1].nombre_Atributo);
            binaryWriter.Write(entidades[pos].atributos[indiceA1].tipo_Dato);
            binaryWriter.Write(entidades[pos].atributos[indiceA1].longitud_Tipo);
            binaryWriter.Write(entidades[pos].atributos[indiceA1].direccion_Atributo);
            binaryWriter.Write(entidades[pos].atributos[indiceA1].tipo_Indice);
            binaryWriter.Write(entidades[pos].atributos[indiceA1].direccion_Indice);
            binaryWriter.Write(entidades[pos].atributos[indiceA1].direccion_sigAtributo);
            Fichero.Close();
        }

        private int totalClaves()
        {
            int count = grado + 1;
            for (int i = 0; i < grado; i++)
            {
                if (Arbol.First().clavesBusqueda[i].Direccion == -1)
                {
                    count = i + 1;
                    break;
                }
            }
            return count;
        }

        private void separaNodo()
        {
            if (Arbol.Count == 1) //Primer nodo, creacion de primera raiz
            {
                bool posi = false; //Si es falso va a la izquierda si es true va a la derecha
                
                if (entidades[pos].atributos[indiceA1].tipo_Dato == 'E')
                {
                    if ( Convert.ToInt32(claveB.Clave) >= Convert.ToInt32(Arbol.First().clavesBusqueda[mitad].Clave))
                    {
                        posi = true;
                    }
                }
                else
                {
                    //string
                }
                
                nuevoNodo('H');
                Arbol.First().Direccion_Siguiente = Arbol.Last().Direccion;

                if (posi) // la nueva clave se va con el nuevo nodo
                {
                    int j = 0;
                    for (int i = mitad - 1; i < grado; i++) //Las agrego al nuevo nodo
                    {
                        Arbol.Last().clavesBusqueda[j].Clave = Arbol.First().clavesBusqueda[i].Clave;
                        Arbol.Last().clavesBusqueda[j].Direccion = Arbol.First().clavesBusqueda[i].Direccion;
                        j++;
                    }

                    for (int i = mitad - 1; i < grado; i++) //Las quito del nodo
                    {
                        Arbol.First().clavesBusqueda[i].Clave = -1;
                        Arbol.First().clavesBusqueda[i].Direccion = -1;
                    }
                    
                    for (int i = 0; i < grado; i++) //Agrego la ultima clave que se asigna
                    {
                        if (Arbol.Last().clavesBusqueda[i].Direccion == -1)
                        {
                            Arbol.Last().clavesBusqueda[i].Clave = claveB.Clave;
                            Arbol.Last().clavesBusqueda[i].Direccion = claveB.Direccion;
                            break;
                        }
                    }

                    if (entidades[pos].atributos[indiceA1].tipo_Dato == 'E')
                    {
                        Arbol.Last().clavesBusqueda = Arbol.Last().clavesBusqueda.OrderBy(x => Convert.ToInt32(x.Clave) == -1).ToList(); //los ordeno
                    }
                    else
                    {
                        Arbol.Last().clavesBusqueda = Arbol.Last().clavesBusqueda.OrderBy(x => x.Clave.ToString() == "-1").ToList(); //los ordeno
                    }
                    
                   posNodo = 0;

                    //Creamos la raiz
                    nuevoNodo('R');
                    //nodo.clavesBusqueda.Add(cb);
                    if (!nuevaClabeBusqueda())
                    {
                        for (int i = 0; i < grado; i++) //Agrego la ultima clave que se asigna
                        {
                            if (Arbol.Last().clavesBusqueda[i].Direccion == -1)
                            {
                                Arbol.Last().clavesBusqueda[i].Clave = claveB.Clave;
                                Arbol.Last().clavesBusqueda[i].DireccionIzquierda = claveB.DireccionIzquierda;
                                Arbol.Last().clavesBusqueda[i].DireccionDerecha = claveB.DireccionDerecha;
                                break;
                            }
                        }
                        raiz = Arbol.Last();
                        entidades[pos].atributos[indiceA1].direccion_Indice = Arbol.Last().clavesBusqueda.First().Direccion;
                        asignaMemoriaAtributo();
                        escribeArchivoIDX();
                    }
                }
            }
            else if (Arbol.Count > 1) //Si ya se creo la raiz
            {

            }
        }

        private void escribeArchivoIDX()
        {
            FicheroArPri = new FileStream(nombreArchivoIDX, FileMode.Open, FileAccess.Write);
            //FicheroArPri.Position = FicheroArPri.Length;
            //MessageBox.Show("tamaño archivo inicio: " + FicheroArPri.Length);
            binaryWriter = new BinaryWriter(FicheroArPri);
            

            foreach (Nodo no in entidades[pos].Arboles.Last().Arbol)
            {
                binaryWriter.Write(no.TipoDeNodo);
                binaryWriter.Write(no.Direccion);
                foreach (ClaveBusqueda cb in no.clavesBusqueda)
                {
                    if (no.TipoDeNodo == 'H')
                    {
                        binaryWriter.Write(cb.Direccion);
                    }
                    else
                    {
                        if (claux == false)
                        {
                            //MessageBox.Show("Izquieda a: " + cb.DireccionIzquierda.ToString());
                            binaryWriter.Write(cb.DireccionIzquierda);
                        }
                        else
                        {
                            //MessageBox.Show("Direcha a: " + cb.DireccionDerecha.ToString());
                            binaryWriter.Write(cb.DireccionDerecha);
                        }
                    }

                    string vs = cb.Clave.ToString(); ;

                    if (entidades[pos].atributos[indiceA1].tipo_Dato == 'C')
                    {
                        char[] caracter = new char[entidades[pos].atributos[indiceA1].longitud_Tipo];
                        int j = 0;

                        foreach (char c in vs)
                        {
                            caracter[j] = c;
                            j++;
                        }

                        binaryWriter.Write(caracter);
                    }
                    else if (entidades[pos].atributos[indiceA1].tipo_Dato == 'E')
                    {

                        int entero = int.Parse(vs);

                        binaryWriter.Write(entero);
                    }

                    if (no.TipoDeNodo == 'R' && claux == false)
                    {
                        if (cb.DireccionIzquierda != -1)
                        {
                            //MessageBox.Show("derehca: " + cb.DireccionDerecha.ToString());
                            binaryWriter.Write(cb.DireccionDerecha);
                            claux = true;
                        }
                    }
                }
                
                if (no.TipoDeNodo == 'H')
                {
                    binaryWriter.Write(no.Direccion_Siguiente);
                }
            }
            //MessageBox.Show("tamaño archivo: " + FicheroArPri.Length);
            FicheroArPri.Close();
        }

        private void nuevoNodo(char tipo)
        {
            nodo = new Nodo(tipo, getDireccionNodo());
            nodo = asignaMemoriaNodo(tipo, nodo);
            Arbol.Add(nodo);
            escribeArchivoIDX();
        }

        private Nodo asignaMemoriaNodo(char tipo, Nodo nod)
        {
            if (tipo == 'H')
            {
                for (int i = 0; i < grado; i++)
                {
                    ClaveBusqueda cb = new ClaveBusqueda(-1, -1);
                    nod.clavesBusqueda.Add(cb);
                }
            }
            else if (tipo == 'R')
            {
                for (int i = 0; i < grado; i++)
                {
                    ClaveBusqueda cb = new ClaveBusqueda(-1, -1, -1);
                    nod.clavesBusqueda.Add(cb);
                }
                //MessageBox.Show("total: " + nod.clavesBusqueda.Count());
            }

            return nod;
        }

        private bool nuevaClabeBusqueda()
        {
            bool repite = false;

            long direc = entidades[pos].registros.Last().dir_Registro;
            object obj = entidades[pos].registros.Last().element_Registro[indiceA1];

            if (entidades[pos].Arboles.Last().Arbol.Count == 0)
            {
                claveB = new ClaveBusqueda(direc, obj);
                return repite;
            }
            
            if (Arbol.Last().TipoDeNodo == 'R')
            {
                claveB = new ClaveBusqueda(Arbol[posNodo].Direccion, Arbol[posNodo + 1].Direccion, Arbol[posNodo + 1].clavesBusqueda.First().Clave);
                
            }
            else
            {
                claveB = new ClaveBusqueda(direc, obj);
            }
            
            return repite;
        }

        private long getDireccionNodo()
        {
            FicheroArPri = File.Open(nombreArchivoIDX, FileMode.Open);
            long direc = FicheroArPri.Length;
            FicheroArPri.Close();
            return direc;
        }

        public FileStream setFichero
        {
            set { Fichero = value; }
        }

        public FileStream setFicheroPrimario
        {
            set { FicheroArPri = value; }
        }

        public string setNombreArchivoPrimario
        {
            set { nombreArchivoIDX = value; }
        }

        public string setNombreArchivo
        {
            set { nombreArchivo = value; }
        }

        public List<Entidad> listEntidad
        {
            get { return entidades; }
            set { entidades = value; }
        }

        public List<Nodo> getListNodo
        {
            get { return Arbol; }
        }

        public int getGrado
        {
            get { return grado; }
        }
    }
}
