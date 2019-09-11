using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Archivos
{
    public class Entidad
    {
        private static int TAM = 35; //tamaño del array de nombre
        private string sNombre; //nombre de la entidad
        private char[] Nombre; //nombre de la entidad por caracter
        private long dirEntidad, dirAtributo, dirDato, dirSigEntidad; //direcciones
        private string datos; //string de los datos de las entidades
        private byte[] IdEntidad; //id de las identidades aleatoriamente

        /*Para los atributos*/
        public List<Atributo> atributos;

        /*Constructor de una nueva identidad*/
        public Entidad(string sNombre)
        {
            this.sNombre = sNombre;
            Nombre = new char[TAM];
            dirEntidad = dirAtributo = dirDato = dirSigEntidad = -1;
            int i = 0;

            /*Se separa por caracter el nombre de la entidad*/
            foreach (char c in sNombre)
            {
                this.Nombre[i] = c;////////////
                i++;
            }

            /*Se crea su lista de atributos*/
            atributos = new List<Atributo>();
        }
        
        /*Propiedades GET SET para los diferentes variables de la entidad (Clase)*/
        public long direccion_Entidad
        {
            get { return dirEntidad; }
            set { dirEntidad = value; }
        }

        public long direccion_Atributo
        {
            get { return dirAtributo; }
            set { dirAtributo = value; }
        }

        public long direccion_Dato
        {
            get { return dirDato; }
            set { dirDato = value; }
        }

        public long direccion_Siguiente
        {
            get { return dirSigEntidad; }
            set { dirSigEntidad = value; }
        }
        
        public char[] nombre_Entidad
        {
            get { return Nombre; }
            set { Nombre = value; }
        }
        
        public string string_Nombre
        {
            get { return sNombre; }
            set { sNombre = value; }
        }
        
        public string datos_Entidad
        {
            get { return datos; }
            set { datos = value; }
        }

        public byte[] Id_Entidad
        {
            get { return IdEntidad; }
            set { IdEntidad = value; }
        }

        /*Añadir atributos a la lista*/
        public void agregarAtributo(Atributo atribut)
        {
            atributos.Add(atribut);
        }
    }
}
