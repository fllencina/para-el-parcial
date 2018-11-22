using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntidadesAbstractas;
using EntidadesInstanciables;
using Excepciones;
using System.Collections.Generic;


namespace Test_Unitarios
{
    [TestClass]
    public class Pruebas
    {
        /// <summary>
        /// valida que se lance la excepción SinProfesorException 
        /// cuando no hay un profesor que de esa clase.
        /// </summary>
        [TestMethod]
        public void TestSinProfesorException()
        {
            
            Universidad uni = new Universidad();
            try
            {
                uni += Universidad.EClases.Legislacion;
            }
            catch (SinProfesorException e)
            {
                Assert.IsInstanceOfType(e, typeof(SinProfesorException));
            }
        }

        /// <summary>
        /// valida que se lance la excepción DNIInvalidoException al ingresar un formato de DNI inválido.
        /// </summary>
        [TestMethod]
        public void TestDNIInvalidoException()
        {
             
            try
            {
                Alumno a = new Alumno(1, "Pedro", "Lopez", "70.303.12l", Persona.ENacionalidad.Argentino, Universidad.EClases.Laboratorio);
                Assert.Fail(); //Si llegó acá está mal.
            }
            catch (DniInvalidoException e)
            {
                Assert.IsInstanceOfType(e, typeof(DniInvalidoException));
            }


            //Fuera del rango permitido para cualquier nacionalidad.
            try
            {
                Alumno a = new Alumno(1, "Pedro", "Lopez", "100000000000", Persona.ENacionalidad.Argentino, Universidad.EClases.Laboratorio);
                Assert.Fail(); //Si llegó acá está mal.
            }
            catch (DniInvalidoException e)
            {
                Assert.IsInstanceOfType(e, typeof(DniInvalidoException));
            }

        }

        /// <summary>
        /// valida que se lance la excepción NacionalidadInvalidaException al ingresar 
        /// un DNI en el rango incorrecto.
        /// </summary>
        [TestMethod]
        public void TestNacionalidadInvalidaException()
        {
            //Argentino fuera de rango.
            try
            {
                Alumno a = new Alumno(1, "Pedro", "Lopez", "90000001", Persona.ENacionalidad.Argentino, Universidad.EClases.Laboratorio);
                Assert.Fail(); //Si llegó acá está mal.
            }
            catch (NacionalidadInvalidaException e)
            {
                Assert.IsInstanceOfType(e, typeof(NacionalidadInvalidaException));
            }

            try
            {      
                Alumno a = new Alumno(1, "Pedro", "Lopez", "70.303.121", Persona.ENacionalidad.Argentino, Universidad.EClases.Laboratorio);
            }
            catch (NacionalidadInvalidaException e)
            {
                Assert.Fail(); //Si llegó acá está mal.
            }

            //Extranjero fuera de rango.
            try
            {
                Alumno a = new Alumno(1, "Pedro", "Lopez", "89999999", Persona.ENacionalidad.Extranjero, Universidad.EClases.Laboratorio);
                Assert.Fail(); //Si llegó acá está mal.
            }
            catch (NacionalidadInvalidaException e)
            {
                Assert.IsInstanceOfType(e, typeof(NacionalidadInvalidaException));
            }

            
            try
            {
               
                Alumno a = new Alumno(1, "Pedro", "Lopez", "90000010", Persona.ENacionalidad.Extranjero, Universidad.EClases.Laboratorio);
            }
            catch (NacionalidadInvalidaException e)
            {
                Assert.Fail(); //Si llegó acá está mal.
            }
        }

        /// <summary>
        /// Testea que el valor de los DNI se cargue correctamente.
        /// </summary>
        [TestMethod]
        public void ValidaDNI()
        {
            Alumno a = new Alumno(1, "Pedro", "Lopez", "34.984.075", Persona.ENacionalidad.Argentino, Universidad.EClases.Laboratorio);
            Assert.IsTrue(34984075 == a.DNI);

            Alumno b = new Alumno(2, "Felipe", "Perez", "12345678", Persona.ENacionalidad.Argentino, Universidad.EClases.Programacion);
            Assert.IsTrue(12345678 == b.DNI);

            Profesor p = new Profesor(1, "Romeo", "Paz", "90.654.321", Persona.ENacionalidad.Extranjero);
            Assert.IsTrue(90654321 == p.DNI);
        }

        /// <summary>
        /// Testea que al generar una jornada, su atributo _alumnos no sea NULL.
        /// </summary>
        [TestMethod]
        public void ValidaJornadaAlumnosNoEsNull()
        {
            Jornada j = new Jornada();

            Assert.IsNotNull(j.Alumnos);
        }
    }
}
