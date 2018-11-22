using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace Cerizza.Mauricio._2C
{
    public partial class frmPpal : Form
    {
        private LosHilos hilos;

        public frmPpal()
        {
            InitializeComponent();
            this.hilos = new LosHilos();
            this.hilos.AvisoFin += this.MostrarMensajeFin;
        }

        private void btnLanzar_Click(object sender, EventArgs e)
        {
            try
            {
               this.hilos += 1;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(this.hilos.Bitacora);
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR: Aún no se ha registrado el lanzamiento de ningún hilo.");
            }
            
        }

        public void MostrarMensajeFin(string mensaje)
        {
            MessageBox.Show(mensaje);
        }
    }
}
