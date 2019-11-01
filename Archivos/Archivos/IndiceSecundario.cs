using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivos
{
    //ISecunadrio
    public class IndiceSecundario
    {
        private object clave;
        private long direccion;
        private long direccionArchivo;

        public IndiceSecundario(long direccion)
        {
            this.direccion = direccion;
            direccionArchivo = -1;
            clave = -1;
        }
        public IndiceSecundario()
        {
            direccion = -1;
            clave = -1;
        }

        public long getDireccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public long getdireccionArchivo
        {
            get { return direccionArchivo; }
            set { direccionArchivo = value; }
        }
        public object getClave
        {
            get { return clave; }
            set { clave = value; }
        }
    }
}
