using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryDienteFeliz
{
    class turnos
    {
        private OleDbConnection conector;
        private OleDbCommand comando;
        private OleDbDataAdapter adaptador;
        private DataTable tabla;

        public turnos()
        {
            conector = new OleDbConnection(Properties.Settings.Default.CADENA);
            comando = new OleDbCommand();

            comando.Connection = conector;
            comando.CommandType = CommandType.TableDirect;
            comando.CommandText = "Turnos";

            adaptador = new OleDbDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);

            DataColumn[] dc = new DataColumn[3];
            dc[0] = tabla.Columns["matricula"];
            dc[1] = tabla.Columns["fecha"];
            dc[2] = tabla.Columns["hora"];
            tabla.PrimaryKey = dc;
        }

        public void graficar(int matricula, DateTime fecha, DataGridView dgv)
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            dgv.Columns.Add("hora", "Hora");
            dgv.Columns.Add("paciente", "Paciente");

            // Obtener los horarios de la tabla
            horarios objH = new horarios();
            DataTable tHorarios = objH.getHorarios();

            // Agregar horarios al DataGridView
            foreach (DataRow filaHorarios in tHorarios.Rows)
            {
                string horaHorario = filaHorarios["hora"].ToString();
                dgv.Rows.Add(horaHorario, ""); // Agregar fila con el horario y celda de paciente vacía
            }

            // Asignar pacientes a los horarios correspondientes
            foreach (DataRow fila in tabla.Rows)
            {
                int matriculaTurno = Convert.ToInt32(fila["matricula"]);
                DateTime dtFecha = DateTime.Parse(fila["fecha"].ToString()).Date;

                if (matricula == matriculaTurno && fecha.Date == dtFecha)
                {
                    string horaTurno = fila["hora"].ToString();
                    foreach (DataGridViewRow filaDataGridView in dgv.Rows)
                    {
                        if (filaDataGridView.Cells["hora"].Value.ToString() == horaTurno)
                        {
                            filaDataGridView.Cells["paciente"].Value = fila["paciente"];
                            break; // Salir del bucle una vez asignado el paciente
                        }
                    }
                }
            }
        }





        public DataTable getTurnos()
        {
            return tabla;
        }
    }
}
