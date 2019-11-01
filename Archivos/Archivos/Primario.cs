using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivos
{
    public class Primario
    {
        public List<IndicePrimario> indice = new List<IndicePrimario>();
        private int iteracion;
        private long apuntadorSig;

        public Primario(object clave, long direccion, Atributo atributo)
        {
            IndicePrimario iPrimario = new IndicePrimario(clave, direccion, atributo); //Se crea el indice primario, suponiendo que esta en la pos 0
            indice.Add(iPrimario);//se agrega a la lista el indice primario
            iteracion = 0; //misma posicion suponiendo
            apuntadorSig = -1;
        }

        /*Esta es para agregar otros indices primarios*/
        public void AddIndice(object clave, long direccion, Atributo atributo)
        {
            IndicePrimario iPrimario = new IndicePrimario(clave, direccion, atributo);
            indice.Add(iPrimario);//se agrega a la lista el indice primario
        }

        /*Get and set necesarios*/
        public int primario_Iteracion
        {
            get { return iteracion; }
            set { iteracion = value; }
        }

        public long apuntador_Siguiente
        {
            get { return apuntadorSig; }
            set { apuntadorSig = value; }
        }
    }
}
