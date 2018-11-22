using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public abstract class Corredor
    {
        public enum Carril {Carril_1, Carril_2}
        protected static Random _avance;
        private Carril _carrilElegido;
        private short _velocidadMax;

        public short VelocidadMaxima
        {
            get
            {
                return this._velocidadMax;
            }
            set
            {
                if(value < 10)
                {
                    this._velocidadMax = value;
                }
                else
                {
                    this._velocidadMax = 10;
                }
            }
        }

        public Carril CarrilElegido
        {
            get
            {
                return this._carrilElegido;
            }
        }

        static Corredor()
        {
            _avance = new Random(Guid.NewGuid().GetHashCode());
        }

        public Corredor(short velocidadMax, Carril carril)
        {
            this._velocidadMax = velocidadMax;
            this._carrilElegido = carril;
        }

        public abstract void Correr();
        public virtual void Guardar(string path)
        {
            throw new NotImplementedException();
        }
    }
}
