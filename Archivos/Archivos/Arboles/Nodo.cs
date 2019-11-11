using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archivos
{
    public class Nodo
    {
        public List<ClaveBusqueda> clavesBusqueda = new List<ClaveBusqueda>();
        private char tipo;
        private long direccion = -1;
        private long direccion_siguiente = -1;

        public Nodo(char tipo, long direccion) //para hojas
        {
            this.tipo = tipo;
            this.direccion = direccion;
            this.direccion_siguiente = -1;
        }

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
    }
}
