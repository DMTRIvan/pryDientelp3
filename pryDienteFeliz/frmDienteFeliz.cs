using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryDienteFeliz
{
    public partial class frmDienteFeliz : Form
    {
        odontologos objO;
        turnos objT;

        public frmDienteFeliz()
        {
            InitializeComponent();
        }

        private void frmDienteFeliz_Load(object sender, EventArgs e)
        {
            try
            {
                objO = new odontologos();

                objO.ver(cbOdontologo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los odontólogos", "Error");
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (dtpFecha.Value.DayOfWeek == DayOfWeek.Saturday || dtpFecha.Value.DayOfWeek == DayOfWeek.Sunday)
            {
                MessageBox.Show("Error: solo se permiten consultas para días de Lunes a Viernes");
                return;
            }

            try
            {
                int matricula = Convert.ToInt32(cbOdontologo.SelectedValue);
                
                DateTime fecha = dtpFecha.Value;

                objT = new turnos();

                objT.graficar(matricula, fecha, dgvTurnos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Asegurese de haber completado todos los campos. Codigo de error:  " + ex, "ERROR");
            }
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
