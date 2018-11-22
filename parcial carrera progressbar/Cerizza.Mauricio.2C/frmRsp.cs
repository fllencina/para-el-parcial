using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Entidades;

namespace Cerizza.Mauricio._2C
{
    public partial class frmRsp : Form
    {
        public delegate void CorrenCallback(int avance, Corredor corredor);
        private List<Persona> _corredores;
        private List<Thread> _corredoresActivos;
        private bool _hayGanador;

        public frmRsp()
        {
            InitializeComponent();
            this._corredores = new List<Persona>();
            this._corredoresActivos = new List<Thread>();
            this._corredores.Add(new Persona("Fernando", 15, Corredor.Carril.Carril_1));
            this._corredores.Add(new Persona("Fernando", 15, Corredor.Carril.Carril_2));
            this._hayGanador = false;
        }

        private void AnalizarCarrera(ProgressBar carril, int avance, Corredor corredor)
        {
            int nuevoValor = carril.Value + avance;
            if(nuevoValor < 100 && this._hayGanador == false)
            {
                carril.Value = nuevoValor;
            }
            else if(this._hayGanador == false)
            {
                carril.Value = 100;
                this._hayGanador = true;
                this.HayGanador(corredor);
            }
        }
        

        private void HayGanador(Corredor corredor)
        {
            foreach (Thread thread in this._corredoresActivos)
            {
                thread.Abort();
            }
            corredor.Guardar("Ganadores");
            MessageBox.Show("HAY UN GANADOR: "+ corredor.ToString());
            LimpiarCarriles();
            btnCorrer.Enabled = true;
        }

        private void LimpiarCarriles()
        {
            pgbCarril1.Value = 0;
            pgbCarril2.Value = 0;
        }

        private void PersonaCorriendo(int avance, Corredor corredor)
        {
            if (pgbCarril1.InvokeRequired || pgbCarril2.InvokeRequired)
            {
                CorrenCallback d = new CorrenCallback(PersonaCorriendo);
                this.Invoke(d, new object[] { avance, corredor });
            }
            else
            {
                if(corredor.CarrilElegido == Corredor.Carril.Carril_1)
                {
                    this.AnalizarCarrera(pgbCarril1, avance, corredor);
                }
                else
                {
                    this.AnalizarCarrera(pgbCarril2, avance, corredor);
                }               
            }
        }
        
        private void btnCorrer_Click(object sender, EventArgs e)
        {
            this._hayGanador = false;
            Thread c1 = new Thread(this._corredores[0].Correr);
            Thread c2 = new Thread(this._corredores[1].Correr);
            this._corredoresActivos.Clear();
            this._corredoresActivos.Add(c1);
            this._corredoresActivos.Add(c2);

            foreach (Thread thread in this._corredoresActivos)
            {
                try
                {
                    thread.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }             
            }

            this._corredores[1].Corriendo += PersonaCorriendo;
            this._corredores[0].Corriendo += PersonaCorriendo;

            btnCorrer.Enabled = false;
        }

        private void frmRsp_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Thread thread in this._corredoresActivos)
            {
                thread.Abort();
            }
        }

        private void frmRsp_Load(object sender, EventArgs e)
        {

        }
    }
}
