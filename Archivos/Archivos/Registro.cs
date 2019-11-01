using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivos
{
    public class Registro
    {
        private long direccion_Registro, apuntador_sigRegistro; //Siempre estaran estas dos direcciones
        private List<object> elementos_atributo;
        private List<Atributo> atributos;
        private static int iteraReg = 0;

        public Registro(List<Atributo> atributos)
        {
            this.atributos = atributos;
            elementos_atributo = new List<object>();
            direccion_Registro = apuntador_sigRegistro = -1;
        }

        /*Constructor para cuando leemos los datos y asignamos en memoria*/
        public Registro(long direccion_Registro)
        {
            elementos_atributo = new List<object>();
            this.direccion_Registro = direccion_Registro;
        }

        /*Agrega una nueva lista de objetos*/
        public List<object> element_Registro
        {
            get { return elementos_atributo; }
            set { elementos_atributo = value; }
        }

        /*Agrega la direccion del atributo*/
        public long dir_Registro
        {
            get { return direccion_Registro; }
            set { direccion_Registro = value; }
        }

        /*Agrega la direccion del atributo*/
        public long dir_sigRegistro
        {
            get { return apuntador_sigRegistro; }
            set { apuntador_sigRegistro = value; }
        }

        /*Agrega numero de iteracion del registro*/
        public int iteraREG
        {
            get { return iteraReg; }
            set { iteraReg = value; }
        }
    }
}
