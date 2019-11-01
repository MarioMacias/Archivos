using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivos
{
    //Secundario
    public class SecundarioDir
    {
        private List<IndiceSecundario> indiceSecundario = new List<IndiceSecundario>();
        private long apuntadorSiguiente;
        private int secDirIteracion;

        public SecundarioDir(long dirreccion)
        {
            IndiceSecundario i = new IndiceSecundario(dirreccion);
            indiceSecundario.Add(i);
            secDirIteracion = 0;
            apuntadorSiguiente = -1;
        }

        public void addIndice(long direccion)
        {
            IndiceSecundario i = new IndiceSecundario(direccion);
            indiceSecundario.Add(i);
        }

        public int getIteracion
        {
            get { return secDirIteracion; }
            set { secDirIteracion = value; }
        }

        public long getApSiguiente
        {
            get { return apuntadorSiguiente; }
            set { apuntadorSiguiente = value; }
        }

        public List<IndiceSecundario> listIndiceSecundario
        {
            get { return indiceSecundario; }
            set { indiceSecundario = value; }
        }
    }
}
