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
        private List<Nodo> Hojas = new List<Nodo>();
        private List<Nodo> Intermedios = new List<Nodo>();
        private ClaveBusqueda claveB;
        private Nodo nodo, nodoAux, nodoAux2;
        private Nodo raiz;
        private int grado = 4;
        private int mitad = 3;
        private int posIntermedio = 0;

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

        /*inserta una nueva clave*/
        public void insertar()
        {
            //MessageBox.Show("total; " + Arbol.Count);
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
                            break;
                        }
                    }
                    entidades[pos].atributos[indiceA1].direccion_Indice = Arbol.First().clavesBusqueda.First().DireccionIzquierda;
                    asignaMemoriaAtributo();
                }
            }
            else if (Arbol.Count == 1) //Si aun no se crea la raiz, pero ya existe una hoja
            {
                if (totalClaves(Arbol.First()) <= grado)
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
                }
                else //se debe separar y crear una raiz
                {
                    if (nuevaClabeBusqueda('H') == false)
                    {
                        separaNodo();
                    }
                }
            }
            else if(Arbol.Count > 1) //Si ya existe una raiz
            {
                if (nuevaClabeBusqueda('H') == false)
                {
                    posNodo = buscarNodo();
                    //MessageBox.Show("posNodo: " + posNodo);

                    ///Cuando se encuentre tengo que verificar si existe espacio en el nodo hoja
                    if (totalClaves(Arbol[posNodo]) <= grado)
                    {
                        ///Si hay se inserta y listo
                        //MessageBox.Show("Agrega sin separar, nodo: " + Arbol[posNodo].Direccion.ToString());
                        agregaClaveBusquedaAlNodo(Arbol[posNodo]);
                    }
                    else
                    {
                        ///si no existe, se separa del nodo
                        ///verificando indices que no he creado.
                        separaNodoCon(Arbol[posNodo]);
                        //MessageBox.Show("separo el nodo con");
                    }
                }
            }
            escribeArchivoIDX();
        }

        /*Agrega y pordena las claves de busquedas de un nodo en especifico*/
        private void agregaClaveBusquedaAlNodo(Nodo nod)
        {
            foreach (Nodo no in Arbol)
            {
                if (no == nod)
                {
                    for (int i = 0; i < grado; i++)
                    {
                        if (no.clavesBusqueda[i].DireccionIzquierda == -1)
                        {
                           // MessageBox.Show("clave: " + no.clavesBusqueda[i - 1].Clave + " dire: " + no.clavesBusqueda[i - 1].Direccion);
                            //MessageBox.Show("claveB: " + claveB.Clave + " direB: " + claveB.Direccion);
                            no.clavesBusqueda[i].Clave = claveB.Clave;
                            no.clavesBusqueda[i].DireccionIzquierda = claveB.DireccionIzquierda;
                            
                            if (entidades[pos].atributos[indiceA1].tipo_Dato == 'E')
                            {
                                no.clavesBusqueda = no.clavesBusqueda.OrderBy(x => Convert.ToInt32(x.Clave)).ToList();
                            }
                            else
                            {
                                no.clavesBusqueda = no.clavesBusqueda.OrderBy(x => x.Clave.ToString()).ToList();
                            }
                            
                            break;
                        }
                    }
                }
            }
        }
        

        /*Busca el el nodo en la raiz, utilizando la ultima clave de busqueda creada*/
        private int buscarNodo()
        {
            int res = 0;
            posIntermedio = -1;
            long direccion = -1;

            for (int i = 0; i < raiz.clavesBusqueda.Count; i++)
            {
                if (entidades[pos].atributos[indiceA1].tipo_Dato == 'E')
                {
                    if (Convert.ToInt32(raiz.clavesBusqueda[i].Clave) != -1)
                    {
                        //MessageBox.Show("raiz: " + raiz.clavesBusqueda[i].Clave.ToString() + " clave: " + claveB.Clave.ToString());
                        if (Convert.ToInt32(raiz.clavesBusqueda[i].Clave) <= Convert.ToInt32(claveB.Clave)) //para que lado iria de la raiz
                        {
                            direccion = raiz.clavesBusqueda[i].DireccionDerecha;
                            //MessageBox.Show("derecha fuera: " + direccion.ToString());
                            posIntermedio = -1;
                            foreach (Nodo no in Arbol)
                            {
                                if (no.TipoDeNodo == 'I') // si existen indices se ira a buscar el nodo hoja por ahi
                                {
                                    posIntermedio++;
                                    // MessageBox.Show("Si existe indice con direccion: " + no.Direccion + " dire: " + direccion);
                                    if (no.Direccion == direccion)
                                    {
                                       // MessageBox.Show("direcciones iguales");
                                        foreach (ClaveBusqueda cb in no.clavesBusqueda)
                                        {
                                            if (Convert.ToInt32(cb.Clave) != -1)
                                            {
                                                if (Convert.ToInt32(cb.Clave) <= Convert.ToInt32(claveB.Clave))
                                                {
                                                    direccion = cb.DireccionDerecha;
                                                    //MessageBox.Show("derecha: " + direccion.ToString());
                                                }
                                            }
                                        }
                                    }
                                }
                                 //para conocer en que intermedio esta
                            }
                        }
                        else
                        {
                            direccion = raiz.clavesBusqueda[i].DireccionIzquierda;
                            //  MessageBox.Show("izquierda: " + direccion.ToString());
                            posIntermedio = -1;
                            foreach (Nodo no in Arbol)
                            {
                                if (no.TipoDeNodo == 'I') // si existen indices se ira a buscar el nodo hoja por ahi
                                {
                                    posIntermedio++;
                                    if (no.Direccion == direccion)
                                    {
                                        direccion = no.clavesBusqueda[i].DireccionIzquierda;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (Convert.ToString(raiz.clavesBusqueda[i].Clave) != "-1")
                    {
                        MessageBox.Show("raiz: " + raiz.clavesBusqueda[i].Clave.ToString() + " clave: " + claveB.Clave.ToString());
                        //if (Convert.ToString(raiz.clavesBusqueda[i].Clave) <= Convert.ToString(claveB.Clave))
                        List<string> palabra = new List<string>();
                        palabra.Add(raiz.clavesBusqueda[i].Clave.ToString());
                        palabra.Add(claveB.Clave.ToString());
                        palabra = palabra.OrderBy(p => p).ToList();

                        if (palabra[1].CompareTo(claveB.Clave.ToString()) == 0)
                        {
                            direccion = raiz.clavesBusqueda[i].DireccionDerecha;
                            MessageBox.Show("derecha: " + direccion.ToString());
                        }
                        else
                        {
                            direccion = raiz.clavesBusqueda[i].DireccionIzquierda;
                            MessageBox.Show("izquierda: " + direccion.ToString());
                            break;
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
                if (nod.TipoDeNodo == 'H')
                {
                    //MessageBox.Show("dirnod: " + nod.Direccion + " dir: " + direccion);
                    if (nod.Direccion == direccion) //cuando sea la misma direccion retornara el numero del nodo
                        break;
                }
                res++;
            }
            return res;
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

        /*Devuelve el numero total de claves del nodo*/
        private int totalClaves(Nodo nod)
        {
            int count = grado + 1;
            for (int i = 0; i < grado; i++)
            {
                if (nod.TipoDeNodo == 'H')
                {
                    if (nod.clavesBusqueda[i].DireccionIzquierda == -1)
                    {
                        count = i + 1;
                        break;
                    }
                }
                else if (nod.TipoDeNodo == 'R' || nod.TipoDeNodo == 'I')
                {
                    if (nod.clavesBusqueda[i].DireccionDerecha == -1)
                    {
                        count = i + 1;
                        break;
                    }
                }
            }
            return count;
        }

        /*Este separa el nodo chido, mandando el nodo en cuestion*/
        private void separaNodoCon(Nodo no)
        {
            bool direccion = false; //Si es falso va a la izquierda si es true va a la derecha
            int cont = 0;
            direccion = ladoNodo(no);

            nuevoNodo('H');

            foreach (Nodo n in Arbol)
            {
                if (n.Direccion == no.Direccion)
                {
                    n.Direccion_Siguiente = Arbol.Last().Direccion;
                    Nodo res = splitIt(n, direccion); //se separa el nodo y devuelve el nodo nuevo creado

                    //Verificas si existen indices
                    if (posIntermedio != -1)
                    {
                        foreach (Nodo nodoIn in Arbol)
                        {
                            if (nodoIn.TipoDeNodo == 'I')
                            {
                                if (cont == posIntermedio)
                                {
                                    agregaClaveIntermedio(res, nodoIn);
                                    break;
                                }
                                cont++;
                            }
                        }
                        break;
                    }
                    else
                    {
                        //agregar a la raiz, la clave
                        agregaClaveRaiz(res, n);
                        break;
                    }
                }
            }
        }

        /*Agregar la clave a intermedio*/
        private void agregaClaveIntermedio(Nodo nuevoNodoCreado, Nodo inter)
        {
            ///si no existe, se separa del nodo
            int gradoTotal = totalClaves(inter);

            if (gradoTotal <= grado) //aun hay espacio en el nodo
            {
                foreach (Nodo n in Arbol)
                {
                    if (n == inter)
                    {
                        for (int i = 0; i < grado; i++) //Agrego la ultima clave que se asigna
                        {
                            if (n.clavesBusqueda[i].DireccionIzquierda == -1)
                            {
                                if (i == 0)//no estoy seguro, la primera raiz no la guardo de aqui
                                {
                                    n.clavesBusqueda[i].Clave = nuevoNodoCreado.clavesBusqueda.First().Clave;
                                    n.clavesBusqueda[i].DireccionIzquierda = nodo.clavesBusqueda.First().DireccionIzquierda;
                                    n.clavesBusqueda[i].DireccionDerecha = nuevoNodoCreado.clavesBusqueda.First().DireccionDerecha;
                                }
                                else
                                {
                                   // MessageBox.Show("direccion nodo: " + n.Direccion);
                                  //  MessageBox.Show("clave: " + nuevoNodoCreado.clavesBusqueda.First().Clave);
                                    n.clavesBusqueda[i].Clave = nuevoNodoCreado.clavesBusqueda.First().Clave;
                                    n.clavesBusqueda[i].DireccionIzquierda = n.clavesBusqueda[i - 1].DireccionDerecha;
                                   // MessageBox.Show("DireccionIzquierda: " + n.clavesBusqueda[i - 1].DireccionDerecha);
                                    n.clavesBusqueda[i].DireccionDerecha = nuevoNodoCreado.Direccion;
                                   // MessageBox.Show("DireccionDerecha: " + nuevoNodoCreado.Direccion);
                                }
                                break;
                            }
                        }
                        //raiz = n; //actualizo el nodo
                        break;
                    }
                }
            }
            else
            {
                nodoAux = nuevoNodoCreado; //copia del nodo hoja que se separo

                separaNodoIntermedio(inter, nuevoNodoCreado);

                // Se crean las dos nuevas
                MessageBox.Show("Se separo el Intermedio");
                //se hacen los indices
            }
        }

        private void separaNodoIntermedio(Nodo intermedioI, Nodo intermedioD)
        {
            nuevoNodo('I');
            Nodo intermedio2 = Arbol.Last(); // el ultimo nodo creado I es el intermedio 2

            Nodo[] noA = splitRoot(intermedioI, intermedio2);

            for (int i = 0; i < Arbol.Count; i++)
            {
                if (Arbol[i].Direccion == noA[0].Direccion) // el que antes era la raiz
                {
                    Arbol[i] = noA[0];
                }

                if (Arbol[i].Direccion == noA[1].Direccion) // el intermedio
                {
                    Arbol[i] = noA[1];
                }
            }

            Arbol.Last().clavesBusqueda[0].Clave = noA[1].clavesBusqueda.First().Clave;
            Arbol.Last().clavesBusqueda[0].DireccionIzquierda = noA[0].Direccion;
            Arbol.Last().clavesBusqueda[0].DireccionDerecha = noA[1].Direccion;

            //Agrego la clave a la raiz
            agregaClaveRaiz(Arbol.Last(), noA[0]);
        }

        /*Separa el nodo raiz para convertir indices*/
        private void separaNodoRaiz()
        {
            Nodo intermedio1, intermedio2;
            intermedio1 = raiz; //copia la raiz
            nuevoNodo('I');
            intermedio2 = Arbol.Last(); // el ultimo nodo creado I es el intermedio 2

            foreach (Nodo n in Arbol)
            {
                if (n == intermedio1) //si es la raiz vieja
                {
                    n.TipoDeNodo = 'I'; //Se cambia a intermedio
                    break;
                }
            }

            Nodo[] noA = splitRoot(intermedio1, intermedio2);
            
            //despues de crear los indices se agrega la nueva raiz con la clave correspondiente
            nuevoNodo('R');

            for (int i = 0; i < Arbol.Count; i++)
            {
                if (Arbol[i].Direccion == noA[0].Direccion) // el que antes era la raiz
                {
                    Arbol[i] = noA[0];
                }

                if (Arbol[i].Direccion == noA[1].Direccion) // el intermedio
                {
                    Arbol[i] = noA[1];
                }
            }
            Arbol.Last().clavesBusqueda[0].Clave = noA[1].clavesBusqueda.First().Clave;
            Arbol.Last().clavesBusqueda[0].DireccionIzquierda = noA[0].Direccion;
            Arbol.Last().clavesBusqueda[0].DireccionDerecha = noA[1].Direccion;
            raiz = Arbol.Last(); //nueva raiz
        }

        /*Saber si el dato a insertar se ira con el nuevo nodo o se queda en el otro*/
        private bool ladoNodo(Nodo no)
        {
            bool direccion = false;
            if (entidades[pos].atributos[indiceA1].tipo_Dato == 'E')
            {
                if (Convert.ToInt32(claveB.Clave) >= Convert.ToInt32(no.clavesBusqueda[mitad].Clave))
                {
                    direccion = true;
                }
                else
                {
                    direccion = false;
                }
            }
            else
            {
                //string
            }

            return direccion;
        }

        /*Agregar la clave a la raiz, una vez que se haya separado.*/
        private void agregaClaveRaiz(Nodo nod, Nodo izq)
        {
            ///si no existe, se separa del nodo
            int gradoTotal = totalClaves(raiz);

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
                                if (i == 0)//no estoy seguro, la primera raiz no la guardo de aqui
                                {
                                    n.clavesBusqueda[i].Clave = nod.clavesBusqueda.First().Clave;
                                    n.clavesBusqueda[i].DireccionIzquierda = nodo.clavesBusqueda.First().DireccionIzquierda;
                                    n.clavesBusqueda[i].DireccionDerecha = nod.clavesBusqueda.First().DireccionDerecha;
                                }
                                else
                                {
                                    n.clavesBusqueda[i].Clave = nod.clavesBusqueda.First().Clave;
                                    //n.clavesBusqueda[i].DireccionIzquierda = n.clavesBusqueda[i - 1].DireccionDerecha;
                                    n.clavesBusqueda[i].DireccionIzquierda = izq.Direccion;
                                    n.clavesBusqueda[i].DireccionDerecha = nod.Direccion;
                                }
                                break;
                            }
                        }
                        raiz = n; //actualizo el nodo
                        break;
                    }
                }
            }
            else
            {
                nodoAux = nod; //copia del nodo hoja que se separo
                
                separaNodoRaiz();

                // Se crean las dos nuevas
                MessageBox.Show("Se crean los indices");
                //se hacen los indices
            }
        }

        /*Separa los nodos, una vez agregado el nuevo nodo a la lista de arbol*/
        private Nodo splitIt(Nodo nodoA, bool lado)
        {
            int j = 0;
            bool bandera = false;
            Nodo res = null;
            for (int i = mitad - 1; i < grado; i++) //Las agrego al nuevo nodo
            {
                Arbol.Last().clavesBusqueda[j].Clave = nodoA.clavesBusqueda[i].Clave;
                Arbol.Last().clavesBusqueda[j].DireccionIzquierda = nodoA.clavesBusqueda[i].DireccionIzquierda;
                j++;
            }

            foreach (Nodo nA in Arbol)
            {
                if (nA.Direccion == nodoA.Direccion)
                {
                    for (int i = mitad - 1; i < grado; i++) //Las quito del nodo
                    {
                        nA.clavesBusqueda[i].Clave = -1;
                        nA.clavesBusqueda[i].DireccionIzquierda = -1;
                    }
                    nodoAux2 = nA;
                    break;
                }
            }
            
            //Se agrega la ultima clave dependiendo del lado correspondiente
            for (int i = 0; i < grado; i++) //Agrego la ultima clave que se asigna
            {
                if (lado)//derecha
                {
                    if (Arbol.Last().clavesBusqueda[i].DireccionIzquierda == -1)
                    {
                        Arbol.Last().clavesBusqueda[i].Clave = claveB.Clave;
                        Arbol.Last().clavesBusqueda[i].DireccionIzquierda = claveB.DireccionIzquierda;

                        for (int k = 0; k < Arbol.Count; k++)
                        {
                            if (Arbol.Last() == Arbol[k])
                            {
                                //MessageBox.Show("clave con arbol[k]: " + Arbol[k].clavesBusqueda[0].Clave.ToString());
                                //MessageBox.Show("clave con arbol.last: " + Arbol.Last().clavesBusqueda[0].Clave.ToString());
                                Arbol[k] = ordenaNodo(Arbol[k]);
                                res = Arbol[k];
                                //MessageBox.Show("clave con res adentro: " + res.clavesBusqueda[0].Clave.ToString());
                                break;
                            }
                        }
                        break;
                    }
                }
                else //izquierda creo que esta mal
                {
                    foreach (Nodo nA in Arbol)
                    {
                        if (nA.Direccion == nodoA.Direccion)
                        {
                            if (nA.clavesBusqueda[i].DireccionIzquierda == -1)
                            {
                                nA.clavesBusqueda[i].Clave = claveB.Clave;
                                nA.clavesBusqueda[i].DireccionIzquierda = claveB.DireccionIzquierda;

                                for (int k = 0; k < Arbol.Count; k++)
                                {
                                    if (nA == Arbol[k])
                                    {
                                        Arbol[k] = ordenaNodo(Arbol[k]);
                                        res = Arbol[k];
                                        break;
                                    }
                                }
                                bandera = true;
                                break;
                            }
                        }
                    }

                    if (bandera)
                    {
                        break;
                    }
                }
            }
            return res;
        }

        /*Separa los nodos, mandando la raiz y el indice*/
        private Nodo[] splitRoot(Nodo nodoR, Nodo nodoI)
        {
            int j = 0;
            Nodo[] res = new Nodo[2];
            for (int i = mitad - 1; i < grado; i++) //Las agrego al nuevo nodo
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
                    for (int i = mitad - 1; i < grado; i++) //Las quito del nodo
                    {
                        nA.clavesBusqueda[i].Clave = -1;
                        nA.clavesBusqueda[i].DireccionDerecha = -1;
                        nA.clavesBusqueda[i].DireccionIzquierda = -1;
                    }
                    break;
                }
            }

            //Se agrega la ultima clave dependiendo del lado correspondiente
            for (int i = 0; i < grado; i++) //Agrego la ultima clave que se asigna
            {
                if (nodoI.clavesBusqueda[i].DireccionIzquierda == -1)
                {
                    nodoI.clavesBusqueda[i].Clave = nodoAux.clavesBusqueda.First().Clave;
                    nodoI.clavesBusqueda[i].DireccionDerecha = nodoAux.Direccion;
                    nodoI.clavesBusqueda[i].DireccionIzquierda = nodoAux2.Direccion;

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
            
            return res;
        }

        /*Ordena el nodo mandado*/
        private Nodo ordenaNodo(Nodo ordeN)
        {
            if (entidades[pos].atributos[indiceA1].tipo_Dato == 'E')
            {
                ordeN.clavesBusqueda = ordeN.clavesBusqueda.OrderBy(x => Convert.ToInt32(x.Clave) == -1).ToList(); //los ordeno
            }
            else
            {
                ordeN.clavesBusqueda = ordeN.clavesBusqueda.OrderBy(x => x.Clave.ToString()).ToList(); //los ordeno
            }

            return ordeN;
        }

        /*Separa el primer nodo*/
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
                    List<string> palabra = new List<string>();
                    palabra.Add(Arbol.First().clavesBusqueda[mitad].Clave.ToString());
                    palabra.Add(claveB.Clave.ToString());
                    palabra = palabra.OrderBy(p => p).ToList();

                    if (palabra[1].CompareTo(claveB.Clave.ToString()) == 0)
                    {
                        posi = true;
                    }
                }
                
                nuevoNodo('H');
                Arbol.First().Direccion_Siguiente = Arbol.Last().Direccion;

                if (posi) // la nueva clave se va con el nuevo nodo
                {
                    int j = 0;
                    for (int i = mitad - 1; i < grado; i++) //Las agrego al nuevo nodo
                    {
                        Arbol.Last().clavesBusqueda[j].Clave = Arbol.First().clavesBusqueda[i].Clave;
                        Arbol.Last().clavesBusqueda[j].DireccionIzquierda = Arbol.First().clavesBusqueda[i].DireccionIzquierda;
                        j++;
                    }

                    for (int i = mitad - 1; i < grado; i++) //Las quito del nodo
                    {
                        Arbol.First().clavesBusqueda[i].Clave = -1;
                        Arbol.First().clavesBusqueda[i].DireccionIzquierda = -1;
                    }
                    
                    for (int i = 0; i < grado; i++) //Agrego la ultima clave que se asigna
                    {
                        if (Arbol.Last().clavesBusqueda[i].DireccionIzquierda == -1)
                        {
                            Arbol.Last().clavesBusqueda[i].Clave = claveB.Clave;
                            Arbol.Last().clavesBusqueda[i].DireccionIzquierda = claveB.DireccionIzquierda;
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
                    if (!nuevaClabeBusqueda('R'))
                    {
                        for (int i = 0; i < grado; i++) //Agrego la ultima clave que se asigna
                        {
                            if (Arbol.Last().clavesBusqueda[i].DireccionIzquierda == -1)
                            {
                                Arbol.Last().clavesBusqueda[i].Clave = claveB.Clave;
                                Arbol.Last().clavesBusqueda[i].DireccionIzquierda = claveB.DireccionIzquierda;
                                Arbol.Last().clavesBusqueda[i].DireccionDerecha = claveB.DireccionDerecha;
                                break;
                            }
                        }
                        raiz = Arbol.Last();
                    }
                }
            }
            escribeArchivoIDX();
        }

        /*Guarda todo los valores de principio a fin, sobrescritura*/
        private void escribeArchivoIDX()
        {
            FicheroArPri = new FileStream(nombreArchivoIDX, FileMode.Open, FileAccess.Write);
            //FicheroArPri.Position = FicheroArPri.Length;
            //MessageBox.Show("tamaño archivo inicio: " + FicheroArPri.Length);
            binaryWriter = new BinaryWriter(FicheroArPri);
            

            foreach (Nodo no in entidades[pos].Arboles.Last().Arbol)
            {
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
                            //MessageBox.Show("Izquieda a: " + cb.DireccionIzquierda.ToString());
                            binaryWriter.Write(cb.DireccionIzquierda);
                        }
                        else
                        {
                            //MessageBox.Show("Direcha a: " + cb.DireccionDerecha.ToString());
                            binaryWriter.Write(cb.DireccionDerecha);
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
                            if (cb.DireccionIzquierda != -1)
                            {
                                binaryWriter.Write(cb.DireccionDerecha);
                                claux = true;
                            }
                        }
                    }
                }
                
                if (no.TipoDeNodo == 'H')
                {
                    binaryWriter.Write(no.Direccion_Siguiente);
                }
            }
            FicheroArPri.Close();
        }

        /*Crea un nodo con su respectiva direccion y de un cierto tipo dado*/
        private void nuevoNodo(char tipo)
        {
            nodo = new Nodo(tipo, getDireccionNodo());
            nodo = asignaMemoriaNodo(tipo, nodo);
            switch (tipo)
            {
                case 'H':
                    Hojas.Add(nodo);
                    break;
                case 'I':
                    Intermedios.Add(nodo);
                    break;
            }

            Arbol.Add(nodo);
            escribeArchivoIDX();
        }

        /*Crea el espacio adecuado del nodo 65*/
        private Nodo asignaMemoriaNodo(char tipo, Nodo nod)
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
            bool repite = false;

            long direc = entidades[pos].registros.Last().dir_Registro;
            object obj = entidades[pos].registros.Last().element_Registro[indiceA1];

            switch (tipo)
            {
                case 'H':
                    claveB = new ClaveBusqueda(direc, -1,  obj);
                    break;
                case 'R':
                    claveB = new ClaveBusqueda(Arbol[posNodo].Direccion, Arbol[posNodo + 1].Direccion, Arbol[posNodo + 1].clavesBusqueda.First().Clave);
                    break;
            }
            
            return repite;
        }

        /*Direccion del nodo al crear en el archivo*/
        private long getDireccionNodo()
        {
            FicheroArPri = File.Open(nombreArchivoIDX, FileMode.Open);
            long direc = FicheroArPri.Length;
            FicheroArPri.Close();
            return direc;
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
