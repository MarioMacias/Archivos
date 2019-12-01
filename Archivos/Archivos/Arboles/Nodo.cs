using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Archivos
{
    public class Nodo
    {
        public List<ClaveBusqueda> clavesBusqueda = new List<ClaveBusqueda>();
        private char tipo;
        private long direccion = -1;
        private long direccion_siguiente = -1;
        private int grado = 4;
        private Nodo nodoPadre;
        public List<Nodo> Hijos;
        private int posLista = 0;

        public Nodo(char tipo, long direccion) //para hojas
        {
            nodoPadre = null;
            Hijos = new List<Nodo>();
            this.tipo = tipo;
            this.direccion = direccion;
            this.direccion_siguiente = -1;
        }

        /*Devuelve el numero total de claves del nodo*/
        public int totalClaves()
        {
            int count = grado + 1;
            for (int i = 0; i < grado; i++)
            {
                if (TipoDeNodo == 'H')
                {
                    if (clavesBusqueda[i].DireccionIzquierda == -1)
                    {
                        count = i + 1;
                        break;
                    }
                }
                else if (TipoDeNodo == 'R' || TipoDeNodo == 'I')
                {
                    if (clavesBusqueda[i].DireccionDerecha == -1)
                    {
                        count = i + 1;
                        break;
                    }
                }
            }
            return count;
        }

        /*Quitar de la lista de claves, la clave a borrar*/
        public void eliminaClave(object K)
        {
           // MessageBox.Show("Clave: " + K.ToString());
            for (int i = 0; i < clavesBusqueda.Count; i++)
            {
                if (K == clavesBusqueda[i].Clave)
                {
                  //  MessageBox.Show("dentro clave borrar " + clavesBusqueda[i].Clave.ToString());
                    clavesBusqueda[i].Clave = -1;
                    clavesBusqueda[i].DireccionDerecha = -1;
                    clavesBusqueda[i].DireccionIzquierda = -1;
                    break;
                }
            }
        }

        /*Agregar clave en el ultimo*/
        public void agregaClave(ClaveBusqueda K)
        {
            for (int i = 0; i < grado; i++)
            {
                if (Convert.ToInt32(clavesBusqueda[i].Clave) == -1)
                {
                    clavesBusqueda[i].Clave = K.Clave;
                    clavesBusqueda[i].DireccionDerecha = K.DireccionDerecha;
                    clavesBusqueda[i].DireccionIzquierda = K.DireccionIzquierda;
                    break;
                }
            }
        }

        /*Reemplaza valores de clave de busqueda*/
        public void reemplazaClaveBusqueda(long izquierda, object K, long derecha)
        {
            for (int i = 0; i < grado; i++)
            {
                if (clavesBusqueda[i].DireccionIzquierda == izquierda && clavesBusqueda[i].DireccionDerecha == derecha)
                {
                    clavesBusqueda[i].Clave = K;
                    break;
                }
            }
        }

        /*Retorna la clave de busqueda*/
        public ClaveBusqueda getClaveBusqueda(object K)
        {
            ClaveBusqueda c1 = null;

            foreach (ClaveBusqueda cb in clavesBusqueda)
            {
                if (Convert.ToInt32(cb.Clave) <= Convert.ToInt32(K))
                {
                    c1 = cb;
                }
                else
                {
                    break;
                }
            }
            return c1;
        }

        /*Retorna la ultima clave que contiene el nodo, es para eliminacion*/
        public ClaveBusqueda getUltimaClave()
        {
            ClaveBusqueda cbReturn = null;

            foreach (ClaveBusqueda cb in clavesBusqueda)
            {
                if (Convert.ToInt32(cb.Clave) != -1)
                {
                    cbReturn = cb;
                }
                else
                {
                    break;
                }
            }

            return cbReturn;
        }

        /*GET AND SET*/
        public long Direccion_Siguiente
        {
            get { return direccion_siguiente; }
            set { direccion_siguiente = value; }
        }

        public long Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public char TipoDeNodo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public int posicionListaNodo
        {
            get { return posLista; }
            set { posLista = value; }
        }
        
        public Nodo NodoPadre
        {
            get { return nodoPadre; }
            set { nodoPadre = value; }
        }

        public void setHijo(Nodo hijo)
        {
            Hijos.Add(hijo);
        }
        
    }
}
