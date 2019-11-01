using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivos
{
    //Secundario2
    public class Secundario
    {
        private List<SecundarioCve> secundarioD = new List<SecundarioCve>();
        private long direccion;
        private int secIteracion;
        private long apuntadorSig;

        /*Constructor*/
        public Secundario(object clave)
        {
            SecundarioCve secD = new SecundarioCve(clave);
            secundarioD.Add(secD);
            secIteracion = 0;
            direccion = -1;
            apuntadorSig = -1;
        }

        public void AddIndice(object clave)
        {
            SecundarioCve p = new SecundarioCve(clave);
            secundarioD.Add(p);
        }

        public int getIteracion
        {
            get { return secIteracion; }
            set { secIteracion = value; }
        }

        public long getApuntadorSig
        {
            get { return apuntadorSig; }
            set { apuntadorSig = value; }
        }
        public long getDireccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public List<SecundarioCve> listSecD
        {
            get { return secundarioD; }
            set { secundarioD = value; }
        }
    }
}
