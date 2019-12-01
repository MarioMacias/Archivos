using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivos
{
    public class ClaveBusqueda
    {
        private object dato = -1;
        private long direccionD = -1;
        private long direccionI = -1;

        public ClaveBusqueda(long direccionI, long direccionD, object dato)
        {
            this.direccionD = direccionD;
            this.direccionI = direccionI;
            this.dato = dato;
        }


        public object Clave
        {
            get { return dato; }
            set { dato = value; }
        }
        
        public long DireccionDerecha
        {
            get { return direccionD; }
            set { direccionD = value; }
        }

        public long DireccionIzquierda
        {
            get { return direccionI; }
            set { direccionI = value; }
        }
    }
}
