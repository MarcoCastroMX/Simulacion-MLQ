using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colas_Multiples
{
    class Proceso
    {
        int ID;
        int Tiempo;
        int TiempoTranscurrido;

        public Proceso(int id,int tiempo)
        {
            this.ID = id;
            this.Tiempo = tiempo;
            this.TiempoTranscurrido = 0;
        }

        public Proceso()
        {

        }

        public int GetID()
        {
            return ID;
        }

        public int GetTiempo()
        {
            return Tiempo;
        }

        public int GetTiempoTranscurrido()
        {
            return TiempoTranscurrido;
        }

        public void SetTiempoTranscurrido(int TT)
        {
            TiempoTranscurrido = TT;
        }

        public void SetID(int id)
        {
            ID = id;
        }

        public void SetTiempo(int tiempo)
        {
            Tiempo = tiempo;
        }
    }
}
