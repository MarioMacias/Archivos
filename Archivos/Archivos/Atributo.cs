using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivos
{
    public class Atributo
    {
        private static int TAM = 35;
        private byte[] IdAtributo; //id de las identidades aleatoriamente
        private char[] nombre; //nombre del archivo para separar
        private string sNombre; //nombre del archivo en string
        private char tipoDato; //tipo de dato a asignar
        private int longitud; //longitud que tiene ese dato
        private long direccionAtributo; //direccion del atributo actual
        private int tipoIndice; //tipo de indice
        private long direccionIndice; //direccion del indice
        private long direccionSigAtributo; //direccion siguiente del atributo


        /*Constructor principal*/
        public Atributo(string sNombre, char tipoDato, int longitud, int tipoIndice)
        {
            this.sNombre = sNombre;
            this.tipoDato = tipoDato;
            this.longitud = longitud;
            direccionAtributo = -1;
            this.tipoIndice = tipoIndice;
            direccionIndice = -1;
            direccionSigAtributo = -1;

            this.nombre = new char[TAM];
            int i = 0;
            foreach (char c in sNombre)
            {
                this.nombre[i] = c;
                i++;
            }
        }

        /*Propiedades GET SET para los diferentes variables de los atributos (Clase)*/
        public byte[] id_Atributo
        {
            get { return IdAtributo; }
            set { IdAtributo = value; }
        }

        public char[] nombre_Atributo
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string string_Nombre
        {
            get { return sNombre; }
            set { sNombre = value; }
        }

        public char tipo_Dato
        {
            get { return tipoDato; }
            set { tipoDato = value; }
        }

        public long direccion_Atributo
        {
            get { return direccionAtributo; }
            set { direccionAtributo = value; }
        }

        public int longitud_Tipo
        {
            get { return longitud; }
            set { longitud = value; }
        }

        public int tipo_Indice
        {
            get { return tipoIndice; }
            set { tipoIndice = value; }
        }

        public long direccion_Indice
        {
            get { return direccionIndice; }
            set { direccionIndice = value; }
        }

        public long direccion_sigAtributo
        {
            get { return direccionSigAtributo; }
            set { direccionSigAtributo = value; }
        }
    }
}
