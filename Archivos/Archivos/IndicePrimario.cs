using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivos
{
    public class IndicePrimario
    {
        private object clave;
        private long direccion;
        private int iteracion;
        private Atributo atributo;

        public IndicePrimario(object clave, long direccion, Atributo atributo)
        {
            this.clave = clave;
            this.direccion = direccion;
            this.atributo = atributo;
            iteracion = 0; //suponemos que es el primero alv
        }

        /*Get and set necesarios */
        public object IndiceP_Clave
        {
            get { return clave; }
            set { clave = value; }
        }

        public long IndiceP_Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public int IndiceP_Iteracion
        {
            get { return iteracion; }
            set { iteracion = value; }
        }
    }
}
