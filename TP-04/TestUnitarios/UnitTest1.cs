using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntidadesHechas;
using Excepciones;
namespace TestUnitarios
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ValidaListaPaquetesNoEsNull()
        {
            Correo correo = new Correo();

            Assert.IsNotNull(correo.Paquetes);
        }
        [TestMethod]
        public void ValidaNoIngresoPaqueteRepetido()
        {
            Correo correo = new Correo();
            Paquete paquete1 = new Paquete("corvalan", "100-110-001");
            Paquete paquete2 = new Paquete("corvalan", "100-110-001");
            try
            {
                correo += paquete1;
                correo += paquete2;
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(TrackingRepetidoException));
            }
        }
    }

}
