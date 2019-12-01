using System;
using System.Collections;
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
        private List<Nodo> ArbolSecunario = new List<Nodo>();
        private List<Nodo> ArbolAcomodado = new List<Nodo>();
        public List<Nodo> Hojas = new List<Nodo>();
        public List<Nodo> Intermedios = new List<Nodo>();
        private ClaveBusqueda claveB;
        private Nodo nodoUltimo, nodoEncontrado, nodoIntermedioEncontrado;
        private Nodo raiz;
        private int grado = 4;
        private int mitad = 2;
        private int posListNodo = 0;
        ClaveBusqueda m; //Es clave de busqueda
        private int pos;
        private int indiceA1 = -1;
        public int indiceA2 = -1;
        private int posNodo;
        private int NivelAtbol = 0;
        public int direccionA2, claveA2;

        private string nombreArchivoIDX, nombreArchivoIDX2;
        private string nombreArchivo;
        private FileStream FicheroArPri, FicheroArSec, Fichero;
        BinaryWriter binaryWriter;
        BinaryReader binaryReader;
        bool claux = false;
        bool encontrado = false;
        
        public ArbolB(List<Entidad> entidades, int pos, int indiceA1)
        {
            this.entidades = entidades;
            this.pos = pos;
            this.indiceA1 = indiceA1;
        }

        /*inserta una nueva clave*/
        public void insertar()
        {
            if (Arbol.Count == 0) //No existe raiz, se crea la primera hoja
            {
                nuevoNodo('H');
                if (nuevaClabeBusqueda('H') == false) //no se repite
                {
                    for (int i = 0; i < grado; i++)
                    {
                        if (Arbol.First().clavesBusqueda[i].DireccionIzquierda == -1)
                        {
                            Arbol.First().clavesBusqueda[i].Clave = claveB.Clave;
                            Arbol.First().clavesBusqueda[i].DireccionIzquierda = claveB.DireccionIzquierda;
                            escribeArchivoIDX(Arbol.First());
                            break;
                        }
                    }
                    entidades[pos].atributos[indiceA1].direccion_Indice = Arbol.First().clavesBusqueda.First().DireccionIzquierda;
                    asignaMemoriaAtributo();
                }
                raiz = Arbol.First();
            }
            else if (Arbol.Count == 1) //Si aun no se crea la raiz, pero ya existe una hoja
            {
                if (Arbol.First().totalClaves() <= grado)
                {
                    if (nuevaClabeBusqueda('H') == false)
                    {
                        //MessageBox.Show("igual1claveB dire: " + claveB.Direccion.ToString() + " obj: " + claveB.Clave.ToString());
                        for (int i = 0; i < grado; i++)
                        {
                            if (Arbol.First().clavesBusqueda[i].DireccionIzquierda == -1)
                            {
                                Arbol.First().clavesBusqueda[i].Clave = claveB.Clave;
                                Arbol.First().clavesBusqueda[i].DireccionIzquierda = claveB.DireccionIzquierda;
                                break;
                            }
                        }

                        if (entidades[pos].atributos[indiceA1].tipo_Dato == 'E')
                        {
                            Arbol.First().clavesBusqueda = Arbol.First().clavesBusqueda.OrderBy(x => Convert.ToInt32(x.Clave) == -1).ToList();
                        }
                        else
                        {
                            Arbol.First().clavesBusqueda = Arbol.First().clavesBusqueda.OrderBy(x => x.Clave.ToString() == "-1").ToList();
                        }
                    }
                    raiz = Arbol.First();
                    raiz = ordenaNodo(raiz);
                    escribeArchivoIDX(raiz);
                }
                else //se debe separar y crear una raiz
                {
                    if (nuevaClabeBusqueda('H') == false)
                    {
                        //separaNodo();
                        NivelAtbol++;
                        Nodo no = Arbol.Last();
                        nuevoNodo('R');
                        raiz = Arbol.Last();
                        separaNodoCon(no);
                    }
                }
            }
            else if(Arbol.Count > 1) //Si ya existe una raiz
            {
                acomodaLista();

                if (nuevaClabeBusqueda('H') == false)
                {
                    Nodo n = buscaNodoInsertar(claveB.Clave);
                    ///Cuando se encuentre tengo que verificar si existe espacio en el nodo hoja
                    //if (Arbol[posNodo].totalClaves() <= grado)
                    if (n.totalClaves() <= grado)
                    {
                        ///Si hay se inserta y listo
                        agregaClaveBusquedaAlNodo(n);
                    }
                    else
                    {
                        ///si no existe, se separa del nodo
                        ///verificando indices que no he creado.
                        separaNodoCon(n);
                    }
                }
            }
        }

        /*Elimina una clave que se encuentre en el arbol.*/
        //public void elimina(object clave, long dir)
        public void eliminar(object clave)
        {
            acomodaLista();
            encontrado = false;
            nodoEncontrado = null;
            nodoEncontrado = buscaNodoEliminar(clave); //Encuentra el nodo hoja que contiene la clave a eliminar

            if (nodoEncontrado != null)
            {
                borrar_entrada(nodoEncontrado, clave);
            }
            else
            {
                MessageBox.Show("No se encontro la clave.", "Error");
            }
        }
        
        /*Se elimina la clave en el nodo previamente encontrado*/
        private void borrar_entrada(Nodo N, object K)
        {
            acomodaLista();

            if (N.TipoDeNodo == 'R')
            {
                if ((N.totalClaves() - 2) != 0)
                {
                    N.eliminaClave(K);
                    escribeArchivoIDX(N);
                    raiz = N;
                  //  MessageBox.Show("Se elimino de la raiz : " + N.Direccion + " la clave K " + K.ToString());
                    MessageBox.Show("Se elimino en raiz", "Eliminacion: sin separar");
                }
                else
                {
                 //   MessageBox.Show("dir en raiz: " + N.Direccion);
                //    MessageBox.Show("Clave: " + K.ToString());
                    raiz = N.Hijos.First();
                    if (raiz.TipoDeNodo == 'I')
                    {
                        raiz.TipoDeNodo = 'R';
                        for (int i = 0; i < raiz.clavesBusqueda.Count; i++)
                        {
                            if (raiz.clavesBusqueda[i].Clave == K)
                            {
                                raiz.clavesBusqueda[i].Clave = N.clavesBusqueda.First().Clave;
                                long dirIz = raiz.clavesBusqueda[i].DireccionIzquierda;
                                long dirDe = raiz.clavesBusqueda[i].DireccionDerecha;
                                ClaveBusqueda cb = new ClaveBusqueda(dirIz, dirDe ,K);
                                raiz.clavesBusqueda[i].DireccionDerecha = raiz.clavesBusqueda[i].DireccionIzquierda;
                                raiz.agregaClave(cb);
                                break;
                            }
                        }
                    }
                    eliminaNodo(N);
                    MessageBox.Show("Se elimino en raiz a hoja", "Una sola hoja");
                }
            }
            else if ((N.totalClaves() - 2) >= (grado / 2)) //eliminar sin hacer desmadre
            {
                N.eliminaClave(K);
                escribeArchivoIDX(N);
                MessageBox.Show("Se elimino", "Eliminacion: sin separar");
            }
            else //N tiene  uy pocos valores / punteros
            {
                posListNodo = 0;
                bool lado = false;
                encontrado = false;

                Nodo Nprima = buscaHermano(N, out lado); //Nodo hermano, primero se asigna el derecho, si no hay, es el izquierdo
                Nodo Npadre = Nprima.NodoPadre;//Se asigna a la variable el nodo padre del nodo N
                ClaveBusqueda Kprima = Npadre.getClaveBusqueda(K); //Se busca la clave que tiene el padre, apuntadores entre K
                
                N.eliminaClave(K);

                if (entranEnElMismoNodo(N, Nprima))//Si las claves pueden estar en un solo nodo
                {
                    ClaveBusqueda cbPrima = N.clavesBusqueda.First();
                    ClaveBusqueda cb = new ClaveBusqueda(cbPrima.DireccionIzquierda, cbPrima.DireccionDerecha, cbPrima.Clave);

                    if (!lado) //Si N es predecesor de Nprima
                    {
                        intercambiar_variables(Nprima, N);
                        escribeArchivoIDX(Nprima);
                        MessageBox.Show("Intercambio", "Eliminacion");
                    }

                    ///si(N no es una hoja)
                    if (N.TipoDeNodo != 'H')
                    {
                        //Concatenar Kprima y todos los punteros y valores en N a Nprima
                        intercambiar_variables(Nprima, N);
                        MessageBox.Show("Intercambio" , "Eliminacion Intermedio");
                    }
                    
                    eliminaNodo(N);
                    escribeArchivoIDX(N);//eliminar

                    borrar_entrada(N.NodoPadre, cb.Clave);
                }
                else //Redistribucion; pedir prestada una entrada de N'
                {
                    MessageBox.Show("entro a redistribucion");
                    if (lado)
                    {
                        if (N.TipoDeNodo != 'H')
                        {
                            ClaveBusqueda m = Nprima.getUltimaClave();
                            Nprima.eliminaClave(m.Clave); // Pedir prestado la ultima clave del nodo derecho
                            N.agregaClave(m); //Se asigna esa clave al nodo izquierdo
                            ordenaNodo(N); //Se ordena el nodoCon todo y apuntadores
                            MessageBox.Show("Se elimino.", "primer caso redistribucion");
                            return;
                        }
                        else
                        {
                            //dejemos que m sea tal que (N'.Pm, N'.Km) sea el último par puntero / valor en N '
                            m = new ClaveBusqueda(-1,-1,-1);
                            ClaveBusqueda mm = Nprima.clavesBusqueda.First();// Pedir prestado la primera clave del nodo derecho
                            m.Clave = mm.Clave;
                            m.DireccionIzquierda = mm.DireccionIzquierda;
                            m.DireccionDerecha = mm.DireccionDerecha;
                            
                            Nprima.eliminaClave(mm.Clave); //eliminar (N'.Pm, N'.Km) de N '
                            Nprima = ordenaNodo(Nprima); //Se ordena
                                                         // MessageBox.Show("dir Nprima: " + Nprima.Direccion);
                                                         ///inserte(N'.Pm, N'.Km) como el primer puntero y valor en N, 
                            N.agregaClave(m); //Se asigna esa clave al nodo izquierdo
                            N = ordenaNodo(N); //Se ordena el nodoCon todo y apuntadores
                            ///reemplaza K 'en el padre (N) por N'.Km
                            Npadre.reemplazaClaveBusqueda(N.Direccion, Nprima.clavesBusqueda.First().Clave, Nprima.Direccion);
                            Npadre = ordenaNodo(Npadre);
                            MessageBox.Show("Se elimino.", "segundo caso redistribucion");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("De lo contrario ---- simetrico al caso de entonces", "Redistribuccion !lado");
                    }
                }
            }
        }

        /*Eliminar el nodo*/
        private bool eliminaNodo(Nodo nod)
        {
            for (int i = 0; i < Arbol.Count; i++)
            {
                if (Arbol[i].Direccion == nod.Direccion)
                {
                    Arbol.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /*Al eliminar intercambiar variables*/
        private void intercambiar_variables(Nodo N, Nodo Nprima)
        {
            for (int i = 0; i < N.clavesBusqueda.Count; i++)
            {
                if (Convert.ToInt32(N.clavesBusqueda[i].Clave) == -1)
                {
                    for (int j = 0; j < Nprima.clavesBusqueda.Count; j++)
                    {
                        if (Convert.ToInt32(Nprima.clavesBusqueda[j].Clave) != -1)
                        {
                            N.clavesBusqueda[i].Clave = Nprima.clavesBusqueda[j].Clave;
                            N.clavesBusqueda[i].DireccionIzquierda = Nprima.clavesBusqueda[j].DireccionIzquierda;
                            N.clavesBusqueda[i].DireccionDerecha = Nprima.clavesBusqueda[j].DireccionDerecha;
                            Nprima.clavesBusqueda[j].Clave = -1;
                            Nprima.clavesBusqueda[j].DireccionIzquierda = -1;
                            Nprima.clavesBusqueda[j].DireccionDerecha = -1;
                            break;
                        }
                    }
                }
            }
        }
        
        /*Verificar si caben en el mismo nodo*/
        private bool entranEnElMismoNodo(Nodo N, Nodo Nprima)
        {
            int n1 = N.totalClaves() - 1;
            int n2 = Nprima.totalClaves() - 1;
            int res = n1 + n2;

            if (res <= grado)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*Conocer el nodo padre del nodo*/
        private void nodoPadre(Nodo N, int K)
        {
           // MessageBox.Show("entro: " + K);
            if (encontrado)
            {
             //   MessageBox.Show("bafa");
                return;
            }

            if (N.TipoDeNodo == 'R')
            {
                raiz = N;
                nodoIntermedioEncontrado = N; //Por si no existe intermedios, esta variable la tomo como padre
                //MessageBox.Show("padre en R");
            }

            if (N.TipoDeNodo == 'I')
            {
                //MessageBox.Show("padre en I dir: " + N.Direccion);
                nodoIntermedioEncontrado = N;
            }

            if (N.TipoDeNodo == 'H')
            {
                foreach (ClaveBusqueda cb in N.clavesBusqueda)
                {
                    if (Convert.ToInt32(cb.Clave) == K)
                    {
                        encontrado = true;
                       // MessageBox.Show("encontrado");
                        nodoPadre(N, K);
                        break;
                    }
                }
            }

            //nodoPadre(Arbol[posListNodo++], K); //Hacia la derecha
            //nodoPadre(Arbol[posListNodo--], K); //Hacia la izquierda
            //MessageBox.Show("posListNodo: " + posListNodo + " total: " + ArbolAcomodado.Count + " otro: " + Arbol.Count);

          //  MessageBox.Show("derecha : " + posListNodo);
            nodoPadre(ArbolAcomodado[posListNodo++], K); //Hacia la derecha
           // MessageBox.Show("Izquierda : " + posListNodo);
            nodoPadre(ArbolAcomodado[--posListNodo], K); //Hacia la izquierda
            // MessageBox.Show("termino");
            return;
        }

        /*Busca los hermanos del nodo mandado, primero se manda el hermano de lado derecho, si no tiene, el de lado izquierdo*/
        private Nodo buscaHermano(Nodo N, out bool lado)
        {
            if (N.TipoDeNodo == 'H')
            {
                foreach (Nodo hermano in Hojas)
                {
                    if (N.Direccion_Siguiente == hermano.Direccion)
                    {
                        lado = true;
                        return hermano;
                    }
                }

                foreach (Nodo hermano in Hojas)
                {
                    if (hermano.Direccion_Siguiente == N.Direccion)
                    {
                        lado = false;
                        return hermano;
                    }
                }

                foreach (Nodo hermano in Hojas)
                {
                    if (hermano.Direccion_Siguiente == -1)
                    {
                        lado = true;
                        return hermano;
                    }
                }
            }
            else if(N.TipoDeNodo == 'I')
            {
                foreach (Nodo noI in Intermedios)
                {
                    if (noI.NodoPadre == N.NodoPadre)
                    {
                        //primero a la derecha
                        if (Convert.ToInt32(noI.clavesBusqueda.First().Clave) > Convert.ToInt32(N.clavesBusqueda.First().Clave))
                        {
                            lado = true;
                            return noI;
                        }
                    }
                }

                foreach (Nodo noI in Intermedios)
                {
                    if (noI.NodoPadre == N.NodoPadre)
                    {
                        //primero a la Izquierda
                        if (Convert.ToInt32(noI.clavesBusqueda.First().Clave) < Convert.ToInt32(N.clavesBusqueda.First().Clave))
                        {
                            lado = false;
                            return noI;
                        }
                    }
                }
                lado = true;
                return Intermedios.Last();
            }
            
            lado = false;
            return null;
        }

        /*Intento de buscar nodo de forma recursiva*/
        private Nodo buscaNodoEliminar(object clave)
        {
            foreach (Nodo no in Hojas)
            {
                foreach (ClaveBusqueda cb in no.clavesBusqueda)
                {
                    if (Convert.ToInt32(clave) == Convert.ToInt32(cb.Clave))
                    {
                        return no;
                    }
                }
            }
            return null;
        }
        
        /*Intento de buscar nodo*/
        private Nodo buscaNodoInsertar(object clave)
        {
            Nodo res = null;

            foreach (Nodo no in Hojas)
            {
                foreach (ClaveBusqueda cb in no.clavesBusqueda){
                    if (Convert.ToInt32(clave) >= Convert.ToInt32(cb.Clave))
                    {
                        res = no;
                    }
                    else
                    {
                        if (res == null)
                            res = no;
                        return res;
                    }
                }
            }
            return res;
        }
        
        /*Agrega y pordena las claves de busquedas de un nodo en especifico*/
        private void agregaClaveBusquedaAlNodo(Nodo nod)
        {
            for (int j = 0; j < Arbol.Count; j++)
            {
                if (Arbol[j] == nod)
                {
                    for (int i = 0; i < grado; i++)
                    {
                        if (Arbol[j].clavesBusqueda[i].DireccionIzquierda == -1)
                        {
                            Arbol[j].clavesBusqueda[i].Clave = claveB.Clave;
                            Arbol[j].clavesBusqueda[i].DireccionIzquierda = claveB.DireccionIzquierda;
                            
                            if (entidades[pos].atributos[indiceA1].tipo_Dato == 'E')
                            {
                                Arbol[j] = ordenaNodo(Arbol[j]);
                                escribeArchivoIDX(Arbol[j]);
                            }
                            break;
                        }
                    }
                }
            }
        }
        
            /*Cambia la direccion de indice del atributo del arbol primario*/
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
        
        /*Este separa el nodo chido, mandando el nodo en cuestion*/
        private void separaNodoCon(Nodo no)
        {
            bool direccion = false; //Si es falso va a la izquierda si es true va a la derecha
            direccion = ladoNodo(no);

            nuevoNodo('H');

            //foreach (Nodo n in Arbol)
            for(int i = 0; i < Arbol.Count; i++)
            {
                if (Arbol[i].Direccion == no.Direccion)
                {
                    Nodo[] res = splitIt(Arbol[i], nodoUltimo, direccion); //me regresa el nodo nuevo separado
                    acomodaLista();
                    escribeArchivoIDX(res[0]);
                    escribeArchivoIDX(res[1]);
                    //Verificas si existen indices
                    if (Intermedios.Count > 0)
                    {
                        Nodo noo = res[0];
                        Nodo nooP = res[0].NodoPadre;

                        agregaClaveIntermedio( res[0], res[1], nooP, res[1].clavesBusqueda.First());
                        acomodaLista();
                        break;
                    }
                    else
                    {
                        //agregar a la raiz, la clave
                        agregaClaveRaiz(res[1], Arbol[i]);
                        acomodaLista();
                        break;
                    }
                }
            }
        }
        
        private void agregaClaveIntermedio(Nodo nodI, Nodo nodD, Nodo padre, ClaveBusqueda cb)
        {
            acomodaLista();
            ///si no existe, se separa del nodo
            int gradoTotal = padre.totalClaves();

            if (gradoTotal <= grado) //aun hay espacio en el nodo
            {
                for (int i = 0; i < grado; i++) //Agrego la ultima clave que se asigna
                {
                    if (padre.clavesBusqueda[i].DireccionIzquierda == -1)
                    {
                        if (padre.clavesBusqueda[i].DireccionIzquierda == -1)
                        {
                            padre.clavesBusqueda[i].Clave = cb.Clave;
                            padre.clavesBusqueda[i].DireccionIzquierda = nodI.Direccion;
                            padre.clavesBusqueda[i].DireccionDerecha = nodD.Direccion;
                            padre = ordenaNodo(padre);
                            break;
                        }
                        escribeArchivoIDX(padre);
                    }
                }
            }
            else
            {
                separaNodoIntermedio(nodI, nodD, padre); //el padre y el nodo derecho, el foraneo
            }
        }

        //private void separaNodoIntermedio(Nodo intermedioI, Nodo intermedioD)
        private void separaNodoIntermedio(Nodo nodI, Nodo nodD, Nodo Padre)
        {
            nuevoNodo('I');
            Nodo intermedio2 = Arbol.Last(); // el ultimo nodo creado I es el intermedio 2
            intermedio2.NodoPadre = Padre.NodoPadre;
            ClaveBusqueda cb;
            Nodo[] noA = splitRoot(Padre, intermedio2, nodI, nodD, out cb);

            for (int i = 0; i < Arbol.Count; i++)
            {
                if (Arbol[i].Direccion == noA[0].Direccion) // Izquierda
                {
                    noA[0] = ordenaNodo(noA[0]);
                    Arbol[i] = noA[0];
                }

                if (Arbol[i].Direccion == noA[1].Direccion) // derecha
                {
                    noA[1] = ordenaNodo(noA[1]);
                    Arbol[i] = noA[1];
                }
            }

            noA[1].clavesBusqueda[0].Clave = nodI.clavesBusqueda.First().Clave;
            noA[1].clavesBusqueda[0].DireccionIzquierda = cb.DireccionDerecha;
            noA[1].clavesBusqueda[0].DireccionDerecha = nodI.Direccion;

            escribeArchivoIDX(noA[0]);
            escribeArchivoIDX(noA[1]);
            //Agrego la clave a la padre
            agregaClaveIntermedio(noA[0], noA[1], noA[0].NodoPadre, cb);
        }

        /*Separa el nodo raiz para convertir indices*/
        private void separaNodoRaiz(Nodo hojaI, Nodo hojaD)
        {
            Nodo intermedio1, intermedio2;
            intermedio1 = raiz; //copia la raiz
            nuevoNodo('I');
            intermedio2 = Arbol.Last(); // el ultimo nodo creado I es el intermedio 2
            NivelAtbol++;
            foreach (Nodo n in Arbol)
            {
                if (n == intermedio1) //si es la raiz vieja
                {
                    n.TipoDeNodo = 'I'; //Se cambia a intermedio
                    break;
                }
            }

            ClaveBusqueda cb;
            Nodo[] noA = splitRoot(intermedio1, intermedio2, hojaI, hojaD, out cb);
            
            //despues de crear los indices se agrega la nueva raiz con la clave correspondiente
            nuevoNodo('R');

            for (int i = 0; i < Arbol.Count; i++)
            {
                if (Arbol[i].Direccion == noA[0].Direccion) // el que antes era la raiz
                {
                    Arbol[i] = noA[0];
                    escribeArchivoIDX(noA[0]);
                }

                if (Arbol[i].Direccion == noA[1].Direccion) // el intermedio
                {
                    Arbol[i] = noA[1];
                    escribeArchivoIDX(noA[1]);
                }
            }
            Arbol.Last().clavesBusqueda[0].Clave = cb.Clave;
            Arbol.Last().clavesBusqueda[0].DireccionIzquierda = noA[0].Direccion;
            Arbol.Last().clavesBusqueda[0].DireccionDerecha = noA[1].Direccion;
            raiz = Arbol.Last(); //nueva raiz
            escribeArchivoIDX(raiz);
        }

        /*Saber si el dato a insertar se ira con el nuevo nodo o se queda en el otro*/
        private bool ladoNodo(Nodo no)
        {
            bool direccion = false; //izquierda
            List<ClaveBusqueda> cbList = new List<ClaveBusqueda>();

            foreach (ClaveBusqueda cb in no.clavesBusqueda)
            {
                cbList.Add(cb);
            }

            cbList.Add(claveB);
            cbList = cbList.OrderBy(var => Convert.ToInt32(var.Clave)).ToList();

            if (Convert.ToInt32(cbList[mitad].Clave) > Convert.ToInt32(claveB.Clave))
            {
                direccion = false;
            }
            else
            {
                direccion = true;
            }

            return direccion;
        }
    
        /*Agregar la clave a la raiz, una vez que se haya separado.*/
        private void agregaClaveRaiz(Nodo nod, Nodo izq)
        {
            ///si no existe, se separa del nodo
            int gradoTotal = raiz.totalClaves();

            if (gradoTotal <= grado) //aun hay espacio en el nodo
            {
                foreach (Nodo n in Arbol)
                {
                    if (n.TipoDeNodo == 'R')
                    {
                        for (int i = 0; i < grado; i++) //Agrego la ultima clave que se asigna
                        {
                            if (n.clavesBusqueda[i].DireccionIzquierda == -1)
                            {
                                n.clavesBusqueda[i].Clave = nod.clavesBusqueda.First().Clave;
                                n.clavesBusqueda[i].DireccionIzquierda = izq.Direccion;
                                n.clavesBusqueda[i].DireccionDerecha = nod.Direccion;
                                break;
                            }
                        }
                        raiz = n; //actualizo el nodo
                        raiz = ordenaNodo(raiz);
                        escribeArchivoIDX(raiz);
                        break;
                    }
                }
            }
            else
            {
                separaNodoRaiz(izq, nod);
                // Se crean las dos nuevas
                //se hacen los indices
            }
        }
        
        /*Separa los nodos, una vez agregado el nuevo nodo a la lista de arbol*/
        private Nodo[] splitIt(Nodo nodoI, Nodo nodoD, bool lado)
        {
            int k = 0;
            Nodo res = null;
            nodoD.NodoPadre = nodoI.NodoPadre;
            if (lado)
            {
                for (int j = mitad; j < grado; j++) //Las agrego al nuevo nodo
                {
                    nodoD.clavesBusqueda[k].Clave = nodoI.clavesBusqueda[j].Clave;
                    nodoD.clavesBusqueda[k].DireccionIzquierda = nodoI.clavesBusqueda[j].DireccionIzquierda;
                    k++;
                }

                for (int j = mitad; j < grado; j++) //Las quito del nodo
                {
                    nodoI.clavesBusqueda[j].Clave = -1;
                    nodoI.clavesBusqueda[j].DireccionIzquierda = -1;
                }
            }
            else
            {
                for (int j = mitad - 1; j < grado; j++) //Las agrego al nuevo nodo
                {
                    nodoD.clavesBusqueda[k].Clave = nodoI.clavesBusqueda[j].Clave;
                    nodoD.clavesBusqueda[k].DireccionIzquierda = nodoI.clavesBusqueda[j].DireccionIzquierda;
                    k++;
                }

                for (int j = mitad - 1; j < grado; j++) //Las quito del nodo
                {
                    nodoI.clavesBusqueda[j].Clave = -1;
                    nodoI.clavesBusqueda[j].DireccionIzquierda = -1;
                }
                
            }
            
            //Se agrega la ultima clave dependiendo del lado correspondiente
            for (int i = 0; i < grado; i++)
            {
                k = 0;
                if (lado)//derecha
                {
                    if (nodoD.clavesBusqueda[i].DireccionIzquierda == -1)
                    {
                        nodoD.clavesBusqueda[i].Clave = claveB.Clave;
                        nodoD.clavesBusqueda[i].DireccionIzquierda = claveB.DireccionIzquierda;

                        for (int j = 0; j < Arbol.Count; j++)
                        {
                            if (nodoD == Arbol[j])
                            {
                                Arbol[j] = ordenaNodo(Arbol[j]);
                                res = Arbol[j];
                                break;
                            }
                        }
                        break;
                    }
                }
                else //izquierda creo que esta mal
                {
                    if (nodoI.clavesBusqueda[i].DireccionIzquierda == -1)
                    {
                        nodoI.clavesBusqueda[i].Clave = claveB.Clave;
                        nodoI.clavesBusqueda[i].DireccionIzquierda = claveB.DireccionIzquierda;

                        for (int j = 0; j < Arbol.Count; j++)
                        {
                            if (nodoI == Arbol[j])
                            {
                                Arbol[j] = ordenaNodo(Arbol[j]);
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            acomodaLista();
            escribeArchivoIDX(nodoI);
            escribeArchivoIDX(nodoD);
            Nodo[] ArrNod = new Nodo[2];
            ArrNod[0] = nodoI;
            ArrNod[1] = nodoD;
            return ArrNod;
        }
        
        /*Separa los nodos, mandando la raiz y el indice*/
        private Nodo[] splitRoot(Nodo nodoR, Nodo nodoI, Nodo hojaI, Nodo hojaD, out ClaveBusqueda cbAu)
        {
            int j = 0;
            Nodo[] res = new Nodo[2];
            ClaveBusqueda cbAux = new ClaveBusqueda(nodoR.clavesBusqueda[mitad].DireccionIzquierda, nodoR.clavesBusqueda[mitad].DireccionDerecha, nodoR.clavesBusqueda[mitad].Clave);
            cbAu = cbAux;

            for (int i = mitad + 1; i < grado; i++) //Las agrego al nuevo nodo
            {
                nodoI.clavesBusqueda[j].Clave = nodoR.clavesBusqueda[i].Clave;
                nodoI.clavesBusqueda[j].DireccionDerecha = nodoR.clavesBusqueda[i].DireccionDerecha;
                nodoI.clavesBusqueda[j].DireccionIzquierda = nodoR.clavesBusqueda[i].DireccionIzquierda;
                j++;
            }

            foreach (Nodo nA in Arbol)
            {
                if (nA.Direccion == nodoR.Direccion)
                {
                    for (int i = mitad; i < grado; i++) //Las quito del nodo
                    {
                        nA.clavesBusqueda[i].Clave = -1;
                        nA.clavesBusqueda[i].DireccionDerecha = -1;
                        nA.clavesBusqueda[i].DireccionIzquierda = -1;
                    }
                    break;
                }
            }
            
            for (int i = 0; i < grado; i++) //Agrego la ultima clave que se asigna
            {
                if (nodoI.clavesBusqueda[i].DireccionIzquierda == -1)
                {
                    nodoI.clavesBusqueda[i].Clave = hojaD.clavesBusqueda.First().Clave;
                    nodoI.clavesBusqueda[i].DireccionDerecha = hojaD.Direccion; //aux
                    nodoI.clavesBusqueda[i].DireccionIzquierda = hojaI.Direccion; //aux2
                    
                    for (int k = 0; k < Arbol.Count; k++)
                    {
                        if (nodoI == Arbol[k])
                        {
                            Arbol[k] = ordenaNodo(Arbol[k]);
                            res[0] = nodoR;
                            res[1] = nodoI;
                            break;
                        }
                    }
                    break;
                }
            }
            escribeArchivoIDX(res[0]);
            escribeArchivoIDX(res[1]);
            return res;
        }

        /*Ordena el nodo mandado*/
        private Nodo ordenaNodo(Nodo ordeN)
        {
            List<ClaveBusqueda> cbLis = new List<ClaveBusqueda>();

            foreach(ClaveBusqueda cb in ordeN.clavesBusqueda)
            {
                if (Convert.ToInt32(cb.Clave) != -1)
                {
                    cbLis.Add(cb);
                }
                else { break; }
            }
            try
            {
                cbLis = cbLis.OrderBy(x => Convert.ToInt32(x.Clave)).ToList(); //los ordeno

                for (int i = 0; i < ordeN.clavesBusqueda.Count; i++)
                {
                    if (Convert.ToInt32(ordeN.clavesBusqueda[i].Clave) != -1)
                    {
                        ordeN.clavesBusqueda[i] = cbLis[i];
                    }
                }
            }
            catch (Exception e)
            {

            }
            
            return ordeN;
        }

        /*Guarda todo los valores de principio a fin, sobrescritura*/
        private void escribeArchivoIDX(Nodo no)
        {
                if(indiceA2 == -1)
                {
                    FicheroArPri = new FileStream(nombreArchivoIDX, FileMode.Open, FileAccess.Write);
                    // MessageBox.Show("No direccion " + no.Direccion);
                    FicheroArPri.Position = no.Direccion;

                    binaryWriter = new BinaryWriter(FicheroArPri);
                }
                else
                {
                    FicheroArSec = new FileStream(nombreArchivoIDX, FileMode.Open, FileAccess.Write);
                    // MessageBox.Show("No direccion " + no.Direccion);
                    FicheroArSec.Position = no.Direccion;

                    binaryWriter = new BinaryWriter(FicheroArSec);
                }
                

                claux = false;
                binaryWriter.Write(no.TipoDeNodo);
                binaryWriter.Write(no.Direccion);
                foreach (ClaveBusqueda cb in no.clavesBusqueda)
                {
                    if (no.TipoDeNodo == 'H')
                    {
                        binaryWriter.Write(cb.DireccionIzquierda);
                    }
                    else
                    {
                        if (claux == false)
                        {
                            binaryWriter.Write(cb.DireccionIzquierda);
                        }
                    }

                    string vs = cb.Clave.ToString();

                    if (entidades[pos].atributos[indiceA1].tipo_Dato == 'C')
                    {
                        char[] caracter = new char[entidades[pos].atributos[indiceA1].longitud_Tipo];
                        int j = 0;

                        foreach (char c in vs)
                        {
                            caracter[j] = c;
                            j++;
                        }
                        MessageBox.Show("no deberia de entrar");
                        binaryWriter.Write(caracter);
                    }
                    else if (entidades[pos].atributos[indiceA1].tipo_Dato == 'E')
                    {

                        int entero = int.Parse(vs);

                        binaryWriter.Write(entero);
                    }

                    if (no.TipoDeNodo == 'R' || no.TipoDeNodo == 'I')
                    {
                        if (claux == false)
                        {
                            claux = true;
                        }

                        binaryWriter.Write(cb.DireccionDerecha);
                    }
                }

                if (no.TipoDeNodo == 'H')
                {
                    binaryWriter.Write(no.Direccion_Siguiente);
                }
                FicheroArPri.Close();
        }

        /*Crea un nodo con su respectiva direccion y de un cierto tipo dado*/
        private void nuevoNodo(char tipo)
        {
            nodoUltimo = new Nodo(tipo, getDireccionNodo());
            nodoUltimo = asignaMemoriaNodo(nodoUltimo);

            Arbol.Add(nodoUltimo);
            escribeArchivoIDX(nodoUltimo);
        }

        /*Crea el espacio adecuado del nodo 65*/
        private Nodo asignaMemoriaNodo(Nodo nod)
        {
            for (int i = 0; i < grado; i++)
            {
                ClaveBusqueda cb = new ClaveBusqueda(-1, -1, -1);
                nod.clavesBusqueda.Add(cb);
            }

            return nod;
        }

        private bool nuevaClabeBusqueda(char tipo)
        {
            long direc = -1;
            object obj = null;
            bool band = false;
            if (indiceA2 == -1)
            {
                direc = entidades[pos].registros.Last().dir_Registro;
                obj = entidades[pos].registros.Last().element_Registro[indiceA1];
            }
            else
            {
                foreach (Secundario ls in entidades[pos].secundarios)
                {
                    foreach (SecundarioCve lsc in ls.listSecD)
                    {
                        if (lsc.getDireccion != -1)
                        {
                            direc = lsc.getDireccion;
                            obj = lsc.getClave;
                        }
                    }
                }
            }

            switch (tipo)
            {
                case 'H':
                    claveB = new ClaveBusqueda(direc, -1,  obj);
                    break;
                case 'R':
                    claveB = new ClaveBusqueda(Arbol[posNodo].Direccion, Arbol[posNodo + 1].Direccion, Arbol[posNodo + 1].clavesBusqueda.First().Clave);
                    break;
            }
            
            return false;
        }

        /*Direccion del nodo al crear en el archivo*/
        private long getDireccionNodo()
        {
            if (indiceA2 == -1)
            {
                FicheroArPri = File.Open(nombreArchivoIDX, FileMode.Open);
                long direc = FicheroArPri.Length;
                FicheroArPri.Close();
                return direc;
            }
            else
            {
                FicheroArSec = File.Open(nombreArchivoIDX2, FileMode.Open);
                long direc = FicheroArSec.Length;
                FicheroArSec.Close();
                return direc;
            }
        }

        /*Asignar a la memoria los datos del árbol, seraaaaaaaaaaaaa?*/
        public void asignaMemoriaDatosArbol()
        {
            Nodo no;
            ClaveBusqueda cb = null;
            bool ban = true;
            char tipoNodo = ' ';
            claux = false;
            object o = new object();
            long dir = -1;
            long dirSiguiente = -1;
            long dirIz, dirDer;
            long cont = 0;
            long auxDir = -1;
            dirIz = -1;
            dirDer = -1;
            
            FicheroArPri = new FileStream(nombreArchivoIDX, FileMode.Open, FileAccess.Read);
            binaryReader = new BinaryReader(FicheroArPri);
            binaryReader.BaseStream.Seek(entidades[pos].atributos[indiceA1].direccion_Indice, SeekOrigin.Begin);

            while (ban)
            {
                claux = false;
                tipoNodo = binaryReader.ReadChar(); //tipo del nodo
              //  MessageBox.Show(" tipo nodo: " + tipoNodo.ToString());
                dir = binaryReader.ReadInt64(); //direccion del nodo
              //  MessageBox.Show(" direccion nodo: " + dir);
                //creo el  nodo
                no = new Nodo(tipoNodo, dir);
                Arbol.Add(no);
                no.posicionListaNodo = Arbol.Count - 1;

                //Leer las clabes de busqueda
                for (int i = 0; i < grado; i++)
                {
                    if (tipoNodo == 'H')
                    {
                        dirIz = binaryReader.ReadInt64(); //Direccion de la clave de busqueda en hoja
                       // MessageBox.Show(" Izquierda hoja: " + dirIz);
                    }
                    else
                    {
                        if (claux == false)
                        {
                            dirIz = binaryReader.ReadInt64(); //Direccion de la clave de busqueda en hoja
                           // MessageBox.Show(" Izquierda otro: " + dirIz);
                        }
                    }

                    switch (entidades[pos].atributos[indiceA1].tipo_Dato)
                    {
                        case 'E':
                            o = binaryReader.ReadInt32();
                            cont = cont + 4;
                           // MessageBox.Show(" clave: " + o.ToString());
                            break;
                        case 'C':
                            char[] c = binaryReader.ReadChars(entidades[pos].atributos[indiceA1].longitud_Tipo);
                            cont = cont + entidades[pos].atributos[indiceA1].longitud_Tipo;
                            o = new string(c);
                            break;
                    }

                    if (tipoNodo == 'R' || tipoNodo == 'I')
                    {
                        if (claux == false)
                        {
                            claux = true;
                        }

                        dirDer = binaryReader.ReadInt64();
                       // MessageBox.Show(" derecha: " + cb.DireccionDerecha);
                        cb = new ClaveBusqueda(dirIz, dirDer, o); //para Raiz e intermedio

                        dirIz = dirDer; //copia de la direccion
                    }
                    else
                    {
                        cb = new ClaveBusqueda(dirIz, -1, o); //para hojas
                        //MessageBox.Show("clave: " + cb.Clave + " dierccion: " + cb.DireccionIzquierda);
                    }

                    Arbol.Last().clavesBusqueda.Add(cb);
                }

                if (tipoNodo == 'H')
                {
                    dirSiguiente = binaryReader.ReadInt64();
                    Arbol.Last().Direccion_Siguiente = dirSiguiente;
                    Hojas.Add(Arbol.Last());
                }else if (tipoNodo == 'R')
                {
                    raiz = Arbol.Last();
                }
                else if (tipoNodo == 'I')
                {
                    Intermedios.Add(Arbol.Last());
                }

                cont = cont + 49;
                if (cont >= FicheroArPri.Length)
                {
                    ban = false;
                }
                else { binaryReader.BaseStream.Seek(cont, SeekOrigin.Begin); }
            }
            FicheroArPri.Close();
            acomodaLista();
           // MessageBox.Show("termino de leer");
        }

        /*Acomoda la lista*/
        public void acomodaLista()
        {
            ArbolAcomodado = new List<Nodo>();
            Intermedios = new List<Nodo>();
            Hojas = new List<Nodo>();
            
            foreach (Nodo no in Arbol) //todo
            {
                if (no.TipoDeNodo == 'R')
                {
                    raiz = no;
                    escribeArchivoIDX(raiz);
                    no.NodoPadre = null;
                    agregaNodoAlaLista(raiz); //ponemos la raiz primero
                }

                if (no.TipoDeNodo == 'I')
                {
                    Intermedios.Add(no);
                }

                if (no.TipoDeNodo == 'H')
                {
                    Hojas.Add(no);
                }
            }

            Intermedios = Intermedios.OrderBy(k => Convert.ToInt32(k.clavesBusqueda.First().Clave)).ToList();
            Hojas = Hojas.OrderBy(k => Convert.ToInt32(k.clavesBusqueda.First().Clave)).ToList();

            colojaPadreHijo();
            orAr(raiz);
            //ordenaArbolRec(raiz, false);
            ordenaASHojas();

            int cont = 0;
            foreach (Nodo noA in ArbolAcomodado)
            {
                noA.posicionListaNodo = cont;
                cont++;
            }

            foreach (Nodo no in Arbol)
            {
                foreach (Nodo noA in ArbolAcomodado)
                {
                    if (no.Direccion == noA.Direccion)
                    {
                        no.posicionListaNodo = noA.posicionListaNodo;
                        break;
                    }
                }
            }
        }

        /*metodo para colocar los padres e hijos de cada nodo*/
        private void colojaPadreHijo()
        {
            //inicializa
            foreach (Nodo no in Arbol)
            {
                no.NodoPadre = null;
                no.Hijos = new List<Nodo>();
            }

            //Primero actualizo la raiz
            foreach (Nodo raizz in Arbol)
            {
                if (raizz.TipoDeNodo == 'R')
                {
                    raiz.NodoPadre = null;
                    raiz = raizz;
                    break;
                }
            }

            //asignar raiz como padre a los hijos y agregar hijos de raiz
            foreach (ClaveBusqueda cb in raiz.clavesBusqueda)
            {
                foreach (Nodo no in Arbol)
                {
                    if (cb.DireccionIzquierda == no.Direccion || cb.DireccionDerecha == no.Direccion)
                    {
                        if (!raiz.Hijos.Contains(no)) // si aun no se asigna ese hijo
                        {
                            raiz.Hijos.Add(no);
                            no.NodoPadre = raiz;
                        }
                    }
                }
            }

            //agregar a todos los indices si es que existen
            foreach (Nodo no in Intermedios)
            {
                foreach (ClaveBusqueda cbI in no.clavesBusqueda)
                {
                    foreach (Nodo nodA in Arbol)
                    {
                        if (cbI.DireccionDerecha == nodA.Direccion || cbI.DireccionIzquierda == nodA.Direccion)
                        {
                            if (!no.Hijos.Contains(nodA))
                            {
                                nodA.NodoPadre = no;
                                no.Hijos.Add(nodA);
                            }
                        }
                    }
                }
            }

        }

        private void orAr(Nodo raiz)
        {
            bool band = true;
            foreach (Nodo no in raiz.Hijos)
            {
                if (no.TipoDeNodo == 'I')
                {
                    agregaNodoAlaLista(no); // se agrega el hijo de la raiz
                    Nodo aux = no;
                    band = true;
                    while (band)
                    {
                        foreach (Nodo noI in aux.Hijos)
                        {
                            agregaNodoAlaLista(noI);

                            if (noI.TipoDeNodo == 'I')
                            {
                                aux = noI;
                            }
                            else
                            {
                                band = false; //es hoja
                            }
                        }
                    }
                }
                else
                {
                    agregaNodoAlaLista(no);
                }
            }
        }

        /*Agregar*/
        private void agregaNodoAlaLista(Nodo no)
        {
            if (!ArbolAcomodado.Contains(no))
            {
                ArbolAcomodado.Add(no);
            }
        }

        /*Ordenar apuntadores siguientes*/
        private void ordenaASHojas()
        {
            List<int> cbAux = new List<int>();

            //foreach (Nodo nh in Hojas)
            for (int i = 0; i < Hojas.Count; i++)
            {
                Hojas[i] = ordenaNodo(Hojas[i]);
                Hojas[i].Direccion_Siguiente = -1;
            }

            for (int i = 0; i < Hojas.Count; i++)
            {
                cbAux.Add(Convert.ToInt32(Hojas[i].clavesBusqueda.First().Clave));
            }

            cbAux = cbAux.OrderBy(var => var).ToList();
            Hojas = Hojas.OrderBy(var => Convert.ToInt32(var.clavesBusqueda.First().Clave)).ToList();

            for (int i = 0; i < cbAux.Count - 1; i++)
            {
                for (int j = 0; j < Hojas.Count - 1; j++)
                {
                    if (Convert.ToInt32(Hojas[j].clavesBusqueda.First().Clave) == cbAux[i])
                    {
                        Hojas[j].Direccion_Siguiente = Hojas[j + 1].Direccion;
                    }
                }
            }

            for (int j = 0; j < Hojas.Count; j++)
            {
                if (Convert.ToInt32(Hojas[j].clavesBusqueda.First().Clave) == cbAux.Last())
                {
                    Hojas.Last().Direccion_Siguiente = -1;
                }
            }
        }

        /*GET AND SET*/
        public FileStream setFichero
        {
            set { Fichero = value; }
        }

        public FileStream setFicheroPrimario
        {
            set { FicheroArPri = value; }
        }

        public FileStream setFicheroSecundario
        {
            set { FicheroArSec = value; }
        }

        public string setNombreArchivoPrimario
        {
            set { nombreArchivoIDX = value; }
        }

        public string setNombreArchivoSecundario
        {
            set { nombreArchivoIDX2 = value; }
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

        public List<Nodo> getListNodoAcomodado
        {
            get { return ArbolAcomodado; }
        }

        public int getGrado
        {
            get { return grado; }
        }

        public Nodo getNodoRaiz
        {
            get { return raiz; }
        }
        
    }
}
