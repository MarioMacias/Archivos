using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivos
{
    //Secundario1
    public class SecundarioCve
    {
        private List<SecundarioDir> secundarioDirs = new List<SecundarioDir>();
        private object clave;
        private long direccion;
        private long apuntadorSig;

        public SecundarioCve(object clave)
        {
            this.clave = -1;
            direccion = -1;
            apuntadorSig = -1;
        }

        public object getClave
        {
            get { return clave; }
            set { clave = value; }
        }

        public long getDireccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public void agregarBloquesDirecciones(long direccion)
        {
            SecundarioDir i = new SecundarioDir(direccion);
            secundarioDirs.Add(i);
        }

        public long getApSiguiente
        {
            get { return apuntadorSig; }
            set { apuntadorSig = value; }
        }

        public List<SecundarioDir> listSecDirs
        {
            get { return secundarioDirs; }
            set { secundarioDirs = value; }
        }
    }
}
